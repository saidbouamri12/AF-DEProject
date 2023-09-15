using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarApp.Core.Models;
using StarApp.Core.ModelsView;
using StartApp.EF.DBContext;

namespace StartApp.Controllers
{
    public class ComptaController : Controller
    {
        private ApplicationDbContext _Context;
        private readonly IWebHostEnvironment _environment;
        public ComptaController(ApplicationDbContext context , IWebHostEnvironment environment)
        {
            _Context = context;
            _environment = environment;
        }

        public async Task<IActionResult> index()
        {
            ViewBag.Banque = await _Context.Banques.ToListAsync();
            var results = _Context.OperatorBanks.ToList();

            var groupedResults =  results
                .GroupBy(ob => ob.Date.Year)
                .OrderBy(group => group.Key)
                .Select(group => new
                {
                    Year = group.Key,
                    AllMonthsData = group.GroupBy(ob => ob.Date.Month)
                                         .OrderBy(monthGroup => monthGroup.Key)
                                         .Select(monthGroup => new
                                         {
                                             Month = monthGroup.Key,
                                             Data = monthGroup.ToList()
                                         })
                }).ToList();
            ViewBag.Data = groupedResults;

            ViewBag.Banque = _Context.Banques.ToList();
            return View();
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Banque = await _Context.Banques.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OperatorBankViewModel model)
        {
            ModelState.Remove("OperatorBank.banques");
            ModelState.Remove("File");
            if(ModelState.IsValid)
            {
                var existop = await _Context.OperatorBanks.Where(x => x.BanqueId == model.OperatorBank.BanqueId && x.payCode == model.OperatorBank.payCode && x.Paytype == model.OperatorBank.Paytype).FirstOrDefaultAsync();
                if(existop != null)
                {
                    TempData["Error"] = "Operation ete exist ";
                    return RedirectToAction("index");
                }
                _Context.OperatorBanks.Add(model.OperatorBank);
                if(model.File.FileName != null)
                {
                    string ImageFolder = Path.Combine(_environment.WebRootPath, "images");
                    string imagepath = Path.Combine(ImageFolder, model.File.FileName);
                    await model.File.CopyToAsync(new FileStream(imagepath, FileMode.Create));
                    model.OperatorBank.FacturePath = model.File.FileName;
                    
                }
                await _Context.SaveChangesAsync();
                TempData["success"] = "Operation ete ajouter";
            }
            else
            {
                TempData["Error"] = "Something Rowng";
            }
            return RedirectToAction("index");
        }

        public async Task<IActionResult> TableGlobale()
        {
            var query = await (from oper in _Context.OperatorBanks
                        join banque in _Context.Banques on oper.BanqueId equals banque.Id
                               orderby oper.Id descending
                               select new Bankoperatorview
                        {
                            Id = oper.Id,
                            BanqueName = banque.Name,
                            Date = oper.Date,
                            Paytype = oper.Paytype,
                            payCode = oper.payCode,
                            Ste = oper.Ste,
                            Tel = oper.Tel,
                            MontantTTC = oper.MontantTTC,
                            Comercant = oper.Comercant,
                            Nfacture = oper.Nfacture,
                            DeclarationTVA = oper.DeclarationTVA,
                            FactureOrigin = oper.FactureOrigin,
                            FacturePath = oper.FacturePath,
                        }).ToListAsync();
            return View(query);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _Context.OperatorBanks.FirstOrDefaultAsync(x => x.Id == id);
            if(data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OperatorBankViewModel model)
        {
            try
            {
                var data = await _Context.OperatorBanks.FirstOrDefaultAsync(x => x.Id == id);
                if (data == null)
                {
                    return NotFound();
                }
                data.BanqueId = model.OperatorBank.BanqueId;
                data.Date = model.OperatorBank.Date;
                data.Paytype = model.OperatorBank.Paytype;
                data.payCode = model.OperatorBank.payCode;
                data.Ste = model.OperatorBank.Ste;
                data.Tel = model.OperatorBank.Tel;
                data.MontantTTC = model.OperatorBank.MontantTTC;

                if (model.File.FileName != null)
                {
                    string ImageFolder = Path.Combine(_environment.WebRootPath, "images");
                    string imagepath = Path.Combine(ImageFolder, model.File.FileName);
                    await model.File.CopyToAsync(new FileStream(imagepath, FileMode.Create));
                    model.OperatorBank.FacturePath = model.File.FileName;
                }
                await _Context.SaveChangesAsync();
                TempData["success"] = "Operation été Modifier";
                return RedirectToAction("index");
            }
            catch(Exception )
            {
                TempData["error"] = "Something Rwong";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _Context.OperatorBanks.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            _Context.OperatorBanks.Remove(data);
            await _Context.SaveChangesAsync();
            TempData["success"] = "Operation ete Superimer";
            return RedirectToAction("index");
        }

        public async Task<IActionResult> TableaDemand()
        {
            var query = await (from oper in _Context.OperatorBanks
                               join banque in _Context.Banques on oper.BanqueId equals banque.Id
                               orderby oper.Id descending where oper.Nfacture == null 
                               select new Bankoperatorview
                               {
                                   Id = oper.Id,
                                   BanqueName = banque.Name,
                                   Date = oper.Date,
                                   Paytype = oper.Paytype,
                                   payCode = oper.payCode,
                                   Ste = oper.Ste,
                                   Tel = oper.Tel,
                                   MontantTTC = oper.MontantTTC,
                                   Comercant = oper.Comercant,
                                   Nfacture = oper.Nfacture,
                                   DeclarationTVA = oper.DeclarationTVA,
                                   FactureOrigin = oper.FactureOrigin,
                                   FacturePath = oper.FacturePath,
                               }).ToListAsync();
            return View(query);
        }

        public async Task<IActionResult> Table_Banquemois(int Year , int Mouth , int id)
        {
            var query = await (from oper in _Context.OperatorBanks
                               join banque in _Context.Banques on oper.BanqueId equals banque.Id
                               orderby oper.Date descending
                               where oper.Date.Year == Year && oper.Date.Month == Mouth && oper.BanqueId == id && oper.DeclarationTVA !=null
                               select new Bankoperatorview
                               {
                                   Id = oper.Id,
                                   BanqueName = banque.Name,
                                   Date = oper.Date,
                                   Paytype = oper.Paytype,
                                   payCode = oper.payCode,
                                   Ste = oper.Ste,
                                   Tel = oper.Tel,
                                   MontantTTC = oper.MontantTTC,
                                   Comercant = oper.Comercant,
                                   Nfacture = oper.Nfacture,
                                   DeclarationTVA = oper.DeclarationTVA,
                                   FactureOrigin = oper.FactureOrigin,
                                   FacturePath = oper.FacturePath,
                               }).ToListAsync();
            return View(query);
        }

        public async Task<IActionResult> TableArchive()
        {
            var query = await (from oper in _Context.OperatorBanks
                               join banque in _Context.Banques on oper.BanqueId equals banque.Id
                               orderby oper.Date descending
                               where oper.DeclarationTVA == null && oper.Nfacture !=null
                               select new Bankoperatorview
                               {
                                   Id = oper.Id,
                                   BanqueName = banque.Name,
                                   Date = oper.Date,
                                   Paytype = oper.Paytype,
                                   payCode = oper.payCode,
                                   Ste = oper.Ste,
                                   Tel = oper.Tel,
                                   MontantTTC = oper.MontantTTC,
                                   Comercant = oper.Comercant,
                                   Nfacture = oper.Nfacture,
                                   DeclarationTVA = oper.DeclarationTVA,
                                   FactureOrigin = oper.FactureOrigin,
                                   FacturePath = oper.FacturePath,
                               }).ToListAsync();
            return View(query);
        }

        public async Task<IActionResult> TableBanque(int id)
        {
            var query = await (from oper in _Context.OperatorBanks
                               join banque in _Context.Banques on oper.BanqueId equals banque.Id
                               orderby oper.Date descending
                               where oper.BanqueId == id
                               select new Bankoperatorview
                               {
                                   Id = oper.Id,
                                   BanqueName = banque.Name,
                                   Date = oper.Date,
                                   Paytype = oper.Paytype,
                                   payCode = oper.payCode,
                                   Ste = oper.Ste,
                                   Tel = oper.Tel,
                                   MontantTTC = oper.MontantTTC,
                                   Comercant = oper.Comercant,
                                   Nfacture = oper.Nfacture,
                                   DeclarationTVA = oper.DeclarationTVA,
                                   FactureOrigin = oper.FactureOrigin,
                                   FacturePath = oper.FacturePath,
                               }).ToListAsync();
            return View(query);
        }

        public async Task<IActionResult> Details(int id)
        {
            var query = await (from oper in _Context.OperatorBanks
                               join banque in _Context.Banques on oper.BanqueId equals banque.Id
                               orderby oper.Date descending
                               where oper.Id == id
                               select new Bankoperatorview
                               {
                                   Id = oper.Id,
                                   BanqueName = banque.Name,
                                   Date = oper.Date,
                                   Paytype = oper.Paytype,
                                   payCode = oper.payCode,
                                   Ste = oper.Ste,
                                   Tel = oper.Tel,
                                   MontantTTC = oper.MontantTTC,
                                   Comercant = oper.Comercant,
                                   Nfacture = oper.Nfacture,
                                   DeclarationTVA = oper.DeclarationTVA,
                                   FactureOrigin = oper.FactureOrigin,
                                   FacturePath = oper.FacturePath,
                               }).FirstOrDefaultAsync();
            return View(query);
        }
        
    }
}
