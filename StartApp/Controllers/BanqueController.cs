using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarApp.Core.Models;
using StartApp.EF.DBContext;
using System.Reflection.Metadata.Ecma335;

namespace StartApp.Controllers
{
    public class BanqueController : Controller
    {
        private ApplicationDbContext _context;
        public BanqueController(ApplicationDbContext context) 
        { 
          _context = context;
        }
        public async  Task<IActionResult> Index()
        {
            return View(await _context.Banques.ToListAsync());
        }

        public IActionResult Detail(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Banque model)
        {
            if (ModelState.IsValid)
            {
                _context.Banques.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Ne pas ajouter");
            return RedirectToAction(nameof(Index));

        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var banque = _context.Banques.Where(x=>x.Id == id).FirstOrDefault();    
            if(banque != null)
            {
                return View(banque);
            }
            ModelState.AddModelError("", "Ne pas ajouter");
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , Banque model)
        {
            if(ModelState.IsValid)
            {
                var banque = _context.Banques.Where(x=>x.Id==id).FirstOrDefault();  
                if(banque == null)
                {
                    return NotFound();
                }
               
                banque.Name = model.Name;
                banque.Rib = model.Rib;
               await _context.SaveChangesAsync();
                TempData["success"] = "Banque ete Modifier";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var banque = await _context.Banques.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(banque != null) 
            {
                return View(banque);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id,Banque model)
        {
            var banque = await _context.Banques.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (banque != null)
            {
                _context.Banques.Remove(banque);
                await _context.SaveChangesAsync();
                TempData["success"] = "Banque ete Suppremer";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

    }
}
