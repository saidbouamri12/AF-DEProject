using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarApp.Core.Models.Compta;
using StarApp.Core.ModelsView;
using StartApp.EF.DBContext;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace StartApp.Controllers
{
    public class PeriodsController : Controller
    {
        private readonly ApplicationDbContext _Context;
        public PeriodsController(ApplicationDbContext context) 
        { 
           _Context = context;
        }
        public IActionResult Index()
        {
           // int t = 1;
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var data = (from period in _Context.Periods
                            join chant in _Context.Chantiers on period.ChaniterID equals chant.ID
                            join chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                            join Use in _Context.Users on chand.Userid equals Use.Id
                            where Use.Id == userId
                            select new periodchantier
                            {
                                Id = period.Id,
                                ChaniterID = period.ChaniterID,
                                datedebit = period.datedebit,
                                Datefin = period.Datefin,
                                CountDay = period.CountDay,
                                chantiername = chant.Name!
                            }).ToList();

                return View(data);
            }
            else
            {
                return NotFound();
            }



            
        }

        public IActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var data = (from period in _Context.Periods
                            join chant in _Context.Chantiers on period.ChaniterID equals chant.ID
                            join chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                            join Use in _Context.Users on chand.Userid equals Use.Id
                            where Use.Id == userId && period.Id == id
                            select new periodchantier
                            {
                                Id = period.Id,
                                ChaniterID = period.ChaniterID,
                                datedebit = period.datedebit,
                                Datefin = period.Datefin,
                                CountDay = period.CountDay,
                                chantiername = chant.Name!
                            }).FirstOrDefault();
                if(data==null)
                {
                    return NotFound();
                }
                return View(data);
            }
            return NotFound();
        }


        public IActionResult Create()
        {

            ViewBag.chantier = (from chant in _Context.Chantiers
                                join
                                chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                                select chant
                                ).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Periods model)
        {
           
            ModelState.Remove("CountDay");
            ModelState.Remove("pointage");
            ModelState.Remove("Chantier");
            ModelState.Remove("AttendanceRecords");
            if (ModelState.IsValid)
            {
                int t = (int)(model.Datefin - model.datedebit).TotalDays;
                model.CountDay = t;
                _Context.Periods.Add(model);
                _Context.SaveChanges();
                return RedirectToAction("Index", "Periods");
            }
            ViewBag.chantier = (from chant in _Context.Chantiers
                                join
                                chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                                select chant
                    ).ToList();
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            var exist = _Context.Periods.FirstOrDefault(x => x.Id == Id);
            ViewBag.chantier = (from chant in _Context.Chantiers
                                join
                                chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                                select chant
                                ).ToList();
            if (exist== null)
            {
                return NotFound();
            }
            return View(exist);
        }

        public IActionResult Edit(int Id , Periods model)
        {
            ViewBag.chantier = (from chant in _Context.Chantiers
                                join
                                chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                                select chant
                                ).ToList();
            if(Id != model.Id)
            {
                return NotFound();
            }
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
                 userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            
            var data = (from period in _Context.Periods
                        join chant in _Context.Chantiers on period.ChaniterID equals chant.ID
                        join chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                        join Use in _Context.Users on chand.Userid equals Use.Id
                        where Use.Id == userId && period.Id == Id
                        select new periodchantier
                        {
                            Id = period.Id,
                            ChaniterID = period.ChaniterID,
                            datedebit = period.datedebit,
                            Datefin = period.Datefin,
                            CountDay = period.CountDay,
                            chantiername = chant.Name!
                        }).FirstOrDefault();
            if (data == null) 
            {
                return NotFound();
            }
            var up = _Context.Periods.FirstOrDefault(x=>x.Id == Id);
            if (ModelState.IsValid)
            {
                up.datedebit = model.datedebit;
                up.Datefin = model.Datefin;
                int t = (int)(model.Datefin - model.datedebit).TotalDays;
                up.CountDay = t;
                _Context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }

        public IActionResult Delete(int Id)
        {
            
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var data = (from period in _Context.Periods
                        join chant in _Context.Chantiers on period.ChaniterID equals chant.ID
                        join chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                        join Use in _Context.Users on chand.Userid equals Use.Id
                        where Use.Id == userId && period.Id == Id
                        select new periodchantier
                        {
                            Id = period.Id,
                            ChaniterID = period.ChaniterID,
                            datedebit = period.datedebit,
                            Datefin = period.Datefin,
                            CountDay = period.CountDay,
                            chantiername = chant.Name!
                        }).FirstOrDefault();
            if (data == null)
            {
                return NotFound();
            }

            var exist = _Context.Periods.FirstOrDefault(x=>x.Id == Id);
            if(exist==null)
            {
                return NotFound();
            }
            return View(exist);
        }
        [HttpPost]
        public IActionResult Deletepost(int Id)
        {
            string userId = "";
            if (User.Identity.IsAuthenticated)
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var data = (from period in _Context.Periods
                        join chant in _Context.Chantiers on period.ChaniterID equals chant.ID
                        join chand in _Context.ChantierDetails on chant.ID equals chand.Chantierid
                        join Use in _Context.Users on chand.Userid equals Use.Id
                        where Use.Id == userId && period.Id == Id
                        select new periodchantier
                        {
                            Id = period.Id,
                            ChaniterID = period.ChaniterID,
                            datedebit = period.datedebit,
                            Datefin = period.Datefin,
                            CountDay = period.CountDay,
                            chantiername = chant.Name!
                        }).FirstOrDefault();
            if (data == null)
            {
                return NotFound();
            }

            var exist = _Context.Periods.FirstOrDefault(x => x.Id == Id);
            if (exist == null)
            {
                return NotFound();
            }
            _Context.Periods.Remove(exist);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
