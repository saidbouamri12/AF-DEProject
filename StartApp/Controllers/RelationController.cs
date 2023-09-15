using Microsoft.AspNetCore.Mvc;
using StarApp.Core.Models.Compta;
using StartApp.EF.DBContext;

namespace StartApp.Controllers
{
    public class RelationController : Controller
    {
        private ApplicationDbContext _Context;
        public RelationController(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public IActionResult Index()
        {
            var specialties = _Context.RelationshipStatus.ToList();
            return View(specialties);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RelationshipStatus model)
        {
            ModelState.Remove("Employees");
            if (ModelState.IsValid)
            {
                _Context.RelationshipStatus.Add(model);
                _Context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);

        }

        public IActionResult Edit(int id)
        {
            var model = _Context.RelationshipStatus.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, RelationshipStatus model)
        {
            ModelState.Remove("Employees");
            if (ModelState.IsValid)
            {
                var exist = _Context.RelationshipStatus.FirstOrDefault(x => x.Id == id);
                if (exist == null)
                {
                    return NotFound();
                }

                exist.Label = model.Label;
                _Context.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var exist = _Context.RelationshipStatus.FirstOrDefault(x => x.Id == id);
            if (exist == null)
            {
                return NotFound();
            }
            _Context.RelationshipStatus.Remove(exist);
            _Context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Details(int id)
        {
            var exist = _Context.RelationshipStatus.FirstOrDefault(x => x.Id == id);
            if (exist == null)
            {
                return NotFound();
            }
            return View(exist);
        }
    }
}
