using Microsoft.AspNetCore.Mvc;
using StarApp.Core.Models.Compta;
using StartApp.EF.DBContext;

namespace StartApp.Controllers
{
    public class ContractController : Controller
    {
        private ApplicationDbContext _Context;
        public ContractController(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public IActionResult Index()
        {
            var specialties = _Context.ContractType.ToList();
            return View(specialties);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContractType model)
        {
            ModelState.Remove("Employees");
            if (ModelState.IsValid)
            {
                _Context.ContractType.Add(model);
                _Context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);

        }

        public IActionResult Edit(int id)
        {
            var model = _Context.ContractType.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, ContractType model)
        {
            ModelState.Remove("Employees");
            if (ModelState.IsValid)
            {
                var exist = _Context.ContractType.FirstOrDefault(x => x.Id == id);
                if (exist == null)
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
            var exist = _Context.ContractType.FirstOrDefault(x => x.Id == id);
            if (exist == null)
            {
                return NotFound();
            }
            _Context.ContractType.Remove(exist);
            _Context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Details(int id)
        {
            var exist = _Context.ContractType.FirstOrDefault(x => x.Id == id);
            if (exist == null)
            {
                return NotFound();
            }
            return View(exist);
        }
    }
}
