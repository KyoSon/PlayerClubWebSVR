using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlayerClubWebSVR.Data;
using PlayerClubWebSVR.Models;
using System.Collections.Generic;

namespace PlayerClubWebSVR.Controllers
{
    public class RugbyUnionController : Controller
    {
        private readonly AppDbContext _context;

        public RugbyUnionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RugbyUnionController
        public IActionResult Index()
        {
            IEnumerable<RugbyUnion> objRugbyUnionList = _context.RugbyUnions.ToList();
            return View(objRugbyUnionList);
        }

        // GET: RugbyUnionController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objRugbyUnion = _context.RugbyUnions.Find(rugbyUnion => rugbyUnion.Id == id);

            if (objRugbyUnion == null)
            {
                return BadRequest();
            }

            return View(objRugbyUnion);
        }

        // GET: RugbyUnionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RugbyUnionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RugbyUnion objRugbyUnion = new RugbyUnion();
                    RugbyUnion.Max_RugbyUnionId += 1;
                    objRugbyUnion.Id = RugbyUnion.Max_RugbyUnionId;
                    objRugbyUnion.Name = collection["Name"];

                    _context.RugbyUnions.Add(objRugbyUnion);
                    TempData["success"] = "Created RugbyUnion successfully.";

                    return RedirectToAction(nameof(Index));
                }
                return View(collection);
            }
            catch
            {
                TempData["error"] = "Created RugbyUnion failed.";
                return View(collection);
            }
        }

        // GET: RugbyUnionController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objRugbyUnion = _context.RugbyUnions.Find(rugbyUnion => rugbyUnion.Id == id);

            if (objRugbyUnion == null)
            {
                return NotFound();
            }

            return View(objRugbyUnion);
        }

        // POST: RugbyUnionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name")] IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objRugbyUnion = _context.RugbyUnions.Find(rugbyUnion => rugbyUnion.Id == id);

                    if (objRugbyUnion == null)
                    {
                        return NotFound();
                    }
                    objRugbyUnion.Name = collection["Name"];
                    TempData["success"] = "Updated RugbyUnion successfully.";
                    return RedirectToAction(nameof(Index));
                }

                return View(collection);
            }
            catch
            {
                TempData["error"] = "Updated RugbyUnion failed.";
                return View(collection);
            }
        }

        // GET: RugbyUnionController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objRugbyUnion = _context.RugbyUnions.Find(rugbyUnion => rugbyUnion.Id == id);

            if (objRugbyUnion == null)
            {
                return NotFound();
            }

            return View(objRugbyUnion);
        }

        // POST: RugbyUnionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var objRugbyUnion = _context.RugbyUnions.Find(rugbyUnion => rugbyUnion.Id == id);

                if (objRugbyUnion == null)
                {
                    return NotFound();
                }
                _context.RugbyUnions.Remove(objRugbyUnion);

                TempData["success"] = "Deleted RugbyUnion successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["error"] = "Deleted RugbyUnion failed.";
                return View();
            }
        }
    }
}
