using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlayerClubWebSVR.Data;
using PlayerClubWebSVR.Models;

namespace PlayerClubWebSVR.Controllers
{
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TeamController
        public IActionResult Index()
        {
            IEnumerable<Team> objTeamList = _context.Teams.ToList();
            return View(objTeamList);
        }

        // GET: TeamController/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult ShowAllPlayers(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            IEnumerable<Player> objPlayerList = _context.Players.FindAll(player => player.TeamId == id);

            if (objPlayerList == null)
            {
                return NotFound();
            }

            return View(objPlayerList);
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

        // GET: TeamController/Create
        public ActionResult Create()
        {
            ViewBag.RugbyUnionList = CreateRugbyUnionSelectList();

            return View();
        }

        // POST: TeamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,Ground,Coach,FoundedYear,Region,RugbyUnionId")] Team team)
        {
            ViewBag.RugbyUnionList = CreateRugbyUnionSelectList();
            try
            {
                int rugbyUnionId = team.RugbyUnionId;

                if (CheckNameIsValid(team.Name))
                {
                    ModelState.AddModelError("Name", "Name must be unique.");
                    return View(team);
                }
                if (ModelState.IsValid)
                {
                    if (CheckRugbyUnionIdIsValid(rugbyUnionId))
                    {
                        Team.Max_TeamId += 1;
                        team.Id = Team.Max_TeamId;

                        _context.Teams.Add(team);

                        TempData["success"] = "Created Team successfully.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("RugbyUnionId", "RugbyUnionId doe not exist in RugbyUnions.");
                    }
                }

                return View(team);
            }
            catch
            {
                TempData["error"] = "Created Team failed.";
                return View(team);
            }
        }

        // GET: TeamController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objTeam = _context.Teams.Find(Team => Team.Id == id);

            if (objTeam == null)
            {
                return NotFound();
            }

            ViewBag.RugbyUnionList = CreateRugbyUnionSelectList();

            return View(objTeam);
        }

        // POST: TeamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,Ground,Coach,FoundedYear,Region,RugbyUnionId")] Team team)
        {
            try
            {
                ViewBag.RugbyUnionList = CreateRugbyUnionSelectList();

                if (ModelState.IsValid)
                {
                    int rugbyUnionId = team.RugbyUnionId;

                    if (CheckRugbyUnionIdIsValid(rugbyUnionId))
                    {
                        var objTeam = _context.Teams.Find(t => t.Id == team.Id);
                        if (objTeam == null)
                        {
                            return NotFound();
                        }
                        objTeam.Name = team.Name;
                        objTeam.Coach = team.Coach;
                        objTeam.RugbyUnionId = rugbyUnionId;
                        objTeam.Region = team.Region;
                        objTeam.FoundedYear = team.FoundedYear;
                        objTeam.Ground = team.Ground;

                        TempData["success"] = "Updated Team successfully.";
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
                TempData["error"] = "Updated Team failed.";
                return View();
            }
        }

        // GET: TeamController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objTeam = _context.Teams.Find(Team => Team.Id == id);

            if (objTeam == null)
            {
                return NotFound();
            }

            return View(objTeam);
        }

        // POST: TeamController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var objTeam = _context.Teams.Find(Team => Team.Id == id);

                if (objTeam == null)
                {
                    return NotFound();
                }
                _context.Teams.Remove(objTeam);

                TempData["success"] = "Deleted Team successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Deleted Team failed.";
                return View();
            }
        }

        private bool CheckRugbyUnionIdIsValid(int id)
        {
            var objTeam = _context.RugbyUnions.Find(rugbyUnion => rugbyUnion.Id == id);

            if (objTeam == null)
            {
                return false;
            }
            return true;
        }

        private bool CheckNameIsValid(string name)
        {
            var objTeam = _context.Teams.Find(t => t.Name== name);

            if (objTeam == null)
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
    }
}
