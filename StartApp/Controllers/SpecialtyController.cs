using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarApp.Core.Models.Compta;
using StartApp.EF.DBContext;

namespace StartApp.Controllers
{
    public class SpecialtyController : Controller
    {
        private ApplicationDbContext _Context;
        
        public SpecialtyController(ApplicationDbContext context )
        {
            _Context = context;
            
        }
        public IActionResult Index()
        {
            var specialties = _Context.Specialty.ToList();
            return View(specialties);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Specialty model)
        {
            ModelState.Remove("Employees");
            if(ModelState.IsValid)
            {
                _Context.Specialty.Add(model);
                _Context.SaveChanges();
                return RedirectToAction("Index");
                
            }
            return View(model);

        }

        public IActionResult Edit(int id)
        {
            var model = _Context.Specialty.FirstOrDefault(x=>x.id == id);   
            if(model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Specialty model)
        {
            ModelState.Remove("Employees");
            if (ModelState.IsValid)
            {
                var exist = _Context.Specialty.FirstOrDefault(x => x.id == id);
                if(exist == null)
                {
                    return NotFound();
                }

                exist.label = model.label;
                _Context.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var exist = _Context.Specialty.FirstOrDefault(x => x.id == id);
            if (exist == null)
            {
                return NotFound();
            }
            _Context.Specialty.Remove(exist);
            _Context.SaveChanges();
            return RedirectToAction("index");
        }
        
        public IActionResult Details(int id)
        {
            var exist = _Context.Specialty.FirstOrDefault(x => x.id == id);
            if (exist == null)
            {
                return NotFound();
            }
            return View(exist);
        }
    }
}
