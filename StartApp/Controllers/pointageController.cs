using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarApp.Core.Models.Compta;
using StarApp.Core.ModelsView;
using StartApp.EF.DBContext;
using System.Security.Claims;

namespace StartApp.Controllers
{
    public class pointageController : Controller
    {
        private readonly ApplicationDbContext _Context;
        public pointageController(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var data = (from pointage in _Context.pointage
                            join emp in _Context.Employees on pointage.EmpId equals emp.Id
                            join peroid in _Context.Periods on pointage.periodId equals peroid.Id
                            join chant in _Context.Chantiers on peroid.ChaniterID equals chant.ID
                            join chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                            where chand.Userid == userId
                            select new emppointagview
                            {
                                Id = pointage.Id,
                                nom = emp.Nom,
                                prenom = emp.prenom,
                                Date = pointage.Date,
                                chantierName = chant.Name!,
                                Tel = emp.Tel!
                            }
                        ).ToList();
                return View(data);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            ViewBag.Emp = _Context.Employees.ToList();
            ViewBag.period = _Context.Periods.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(pointage model)
        {
            ModelState.Remove("Periods");
            ModelState.Remove("Employee");
            if (ModelState.IsValid)
            {
                _Context.pointage.Add(model);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Emp = _Context.Employees.ToList();
            ViewBag.period = _Context.Periods.ToList();

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointage =  _Context.pointage.Find(id);
            if (pointage == null)
            {
                return NotFound();
            }
            return View(pointage);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, pointage model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Periods");
            ModelState.Remove("Employee");
            if (ModelState.IsValid)
            {
                try
                {
                    var exist = _Context.pointage.Find(model.Id);
                    if(exist==null)
                    {
                        return NotFound();
                    }
                    exist.periodId = model.periodId;
                    exist.EmpId = model.EmpId;
                    exist.Date = model.Date;
                     _Context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        return NotFound();
                    
                   
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Emp = _Context.Employees.ToList();
            ViewBag.period = _Context.Periods.ToList();
            return View(model);
           
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointage = _Context.pointage.Find(id);
            if (pointage == null)
            {
                return NotFound();
            }
            return View(pointage);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointage = _Context.pointage.Find(id);
            if (pointage == null)
            {
                return NotFound();
            }
            return View(pointage);
            
        }


    }
}
