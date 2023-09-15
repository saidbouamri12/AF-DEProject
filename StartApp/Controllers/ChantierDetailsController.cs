using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarApp.Core.Models;
using StarApp.Core.Models.Compta;
using StarApp.Core.ModelsView;
using StartApp.EF.DBContext;

namespace StartApp.Controllers
{
    public class ChantierDetailsController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ChantierDetailsController(ApplicationDbContext Context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) 
        {
            _Context = Context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var data = await (from Chantd in _Context.ChantierDetails
                           join use in _Context.Users on Chantd.Userid equals user.Id
                           join chant in _Context.Chantiers on Chantd.Chantierid equals chant.ID
                           where user.Id == id
                            select new chantieruserindexview
                           {
                               ID= Chantd.Id,
                               chantierId = chant.ID,
                               Name = chant.Name,
                               DebitDate = chant.DebitDate,
                               DateFin = chant.DateFin,
                               userid = user.Id,
                               fullname = user.fullname
 
                            }).ToListAsync();
           // var t= _Context.ChantierDetails.
           // var indexpage = _Context.
            return View(data);
        }

      
    }
}
