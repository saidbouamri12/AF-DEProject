using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarApp.Core.Models;
using StarApp.Core.Models.Compta;
using StarApp.Core.ModelsView;
using StartApp.EF.DBContext;

namespace StartApp.Controllers
{
    public class ChantierController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChantierController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) 
        {
            _Context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var index = await _Context.Chantiers.ToListAsync();
            return View(index);
        }

        public async  Task<IActionResult> Create()
        {
            ViewBag.user = await _userManager.Users.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Chantier model)
        {
            if(ModelState.IsValid)
            {
                _Context.Chantiers.Add(model);
                await  _Context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var exist = await _Context.Chantiers.FirstOrDefaultAsync(x => x.ID == Id);
            if(exist == null)
            {
                return NotFound();
            }
            return View(exist);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id,Chantier model)
        {
            var exist = await _Context.Chantiers.FirstOrDefaultAsync(x => x.ID == Id);
            if (exist == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                exist.Name = model.Name;
                exist.DebitDate = model.DebitDate;
                exist.DateFin = model.DateFin;
                if(exist.DateFin != null)
                {
                    exist.Complete = true;
                }
              //  exist.Complete = model.Complete;
                await _Context.SaveChangesAsync();
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var exist = await _Context.Chantiers.FirstOrDefaultAsync(x => x.ID == Id);
            if (exist == null)
            {
                return NotFound();
            }
            return View(exist);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            var exist = await _Context.Chantiers.FirstOrDefaultAsync(x=>x.ID == Id);    
            if(exist == null)
            {
                return NotFound();
            }
            _Context.Chantiers.Remove(exist);
            await _Context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> ChantierUser(string userid)
        {
            ViewBag.Chantier = await _Context.Chantiers.ToListAsync();
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChantierUser(string userid,chantierUsercreateView model)
        {

            ChantierDetails data = new ChantierDetails();

            var chant = await _Context.Chantiers.FirstOrDefaultAsync(x => x.ID == model.ChantierId);
            var user = await _Context.Users.FirstOrDefaultAsync(x => x.Id == userid);

            if (chant == null || user == null)
            {
                return NotFound();
            }
            var exist = await (from cha in _Context.ChantierDetails
                               join
                               utili in _Context.Users on cha.Userid equals utili.Id
                               where utili.Id == userid && cha.Id == model.ChantierId
                               select new
                               {

                               }).ToListAsync();
            if(exist != null)
            {
                //add message display in 
                return RedirectToAction("Usermanage", "Account");
            }
            if (ModelState.IsValid)
            {
                data.Chantierid = model.ChantierId;
                data.Userid = model.userid;

                await _Context.ChantierDetails.AddAsync(data);
                await _Context.SaveChangesAsync();

                return RedirectToAction("Index","ChantierDetails", new { id = userid });
            }

            ViewBag.Chantier = await _Context.Chantiers.ToListAsync();

            return View(model);
        }

    }
}
