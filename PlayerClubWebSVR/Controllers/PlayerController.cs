using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlayerClubWebSVR.Data;
using PlayerClubWebSVR.Models;

namespace PlayerClubWebSVR.Controllers
{
    public class PlayerController : Controller
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PlayerController
        public IActionResult Index()
        {
            IEnumerable<Player> objPlayerList = _context.Players.ToList();
            return View(objPlayerList);
        }

        // GET: PlayerController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objPlayer = _context.Players.Find(Player => Player.Id == id);

            if (objPlayer == null)
            {
                return BadRequest();
            }

            return View(objPlayer);
        }

        // GET: TeamController/Details/5
        public ActionResult ShowTeamInfoFromPlayer(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objTeam = _context.Teams.Find(team => team.Id == id);

            if (objTeam == null)
            {
                return BadRequest();
            }

            return View(objTeam);
        }

        // GET: RugbyUnionController/Details/5
        public ActionResult ShowRugbyUnionInfoFromPlayer(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objRugbyUnion = _context.RugbyUnions.Find(r => r.Id == id);

            if (objRugbyUnion == null)
            {
                return BadRequest();
            }

            return View(objRugbyUnion);
        }

        // GET: PlayerController/Create
        public ActionResult Create()
        {
            ViewBag.TeamIdList = CreateTeamSelectList();
            ViewBag.RugbyUnionList = CreateRugbyUnionSelectList();

            return View();
        }

        // POST: PlayerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,BirthDate,Height,Weight,PlaceOfBirth,TeamId,RugbyUnionId")] Player Player)
        {
            ViewBag.TeamIdList = CreateTeamSelectList();
            ViewBag.RugbyUnionList = CreateRugbyUnionSelectList();
            try
            {
                int rugbyUnionId = Player.RugbyUnionId;

                if (ModelState.IsValid)
                {
                    if (CheckRugbyUnionIdIsValid(rugbyUnionId))
                    {
                        Player.Max_PlayerId += 1;
                        Player.Id = Player.Max_PlayerId;

                        _context.Players.Add(Player);

                        TempData["success"] = "Created Player successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("RugbyUnionId", "RugbyUnionId doe not exist in RugbyUnions.");
                    }
                }

                return View(Player);
            }
            catch
            {
                TempData["error"] = "Created Player failed.";
                return View(Player);
            }
        }

        // GET: PlayerController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objPlayer = _context.Players.Find(Player => Player.Id == id);

            if (objPlayer == null)
            {
                return NotFound();
            }
            ViewBag.TeamIdList = CreateTeamSelectList();
            ViewBag.RugbyUnionList = CreateRugbyUnionSelectList();

            return View(objPlayer);
        }

        // POST: PlayerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,BirthDate,Height,Weight,PlaceOfBirth,TeamId,RugbyUnionId")] Player Player)
        {
            try
            {
                ViewBag.TeamIdList = CreateTeamSelectList();
                ViewBag.RugbyUnionList = CreateRugbyUnionSelectList();

                if (ModelState.IsValid)
                {
                    int rugbyUnionId = Player.RugbyUnionId;

                    if (CheckRugbyUnionIdIsValid(rugbyUnionId))
                    {
                        var objPlayer = _context.Players.Find(t => t.Id == Player.Id);
                        if (objPlayer == null)
                        {
                            return NotFound();
                        }
                        objPlayer.Name = Player.Name;
                        objPlayer.BirthDate = Player.BirthDate;
                        objPlayer.Height = Player.Height;
                        objPlayer.Weight = Player.Weight;
                        objPlayer.PlaceOfBirth = Player.PlaceOfBirth;
                        objPlayer.TeamId = Player.TeamId;
                        objPlayer.RugbyUnionId = rugbyUnionId;

                        TempData["success"] = "Updated Player successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("RugbyUnionId", "RugbyUnionId doe not exist in RugbyUnions.");
                    }
                }

                return View();
            }
            catch
            {
                TempData["error"] = "Updated Player failed.";
                return View();
            }
        }

        // GET: PlayerController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objPlayer = _context.Players.Find(Player => Player.Id == id);

            if (objPlayer == null)
            {
                return NotFound();
            }

            return View(objPlayer);
        }

        // POST: PlayerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var objPlayer = _context.Players.Find(Player => Player.Id == id);

                if (objPlayer == null)
                {
                    return NotFound();
                }
                _context.Players.Remove(objPlayer);

                TempData["success"] = "Deleted Player successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Deleted Player failed.";
                return View();
            }
        }

        private bool CheckRugbyUnionIdIsValid(int id)
        {
            var objPlayer = _context.RugbyUnions.Find(rugbyUnion => rugbyUnion.Id == id);

            if (objPlayer == null)
            {
                return false;
            }
            return true;
        }

        private bool CheckNameIsValid(string name)
        {
            var objPlayer = _context.Players.Find(t => t.Name == name);

            if (objPlayer == null)
            {
                return false;
            }
            return true;
        }

        private SelectList CreateRugbyUnionSelectList()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            if (_context.RugbyUnions.Count > 0)
            {
                foreach (RugbyUnion rugbyUnion in _context.RugbyUnions)
                {
                    selectList.Add(new SelectListItem() { Text = rugbyUnion.Name, Value = rugbyUnion.Id.ToString() });
                }
            }

            return new SelectList(selectList, "Value", "Text");
        }

        private SelectList CreateTeamSelectList()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            if (_context.Teams.Count > 0)
            {
                foreach (Team team in _context.Teams)
                {
                    selectList.Add(new SelectListItem() { Text = team.Name, Value = team.Id.ToString() });
                }
            }

            return new SelectList(selectList, "Value", "Text");
        }
    }
}
