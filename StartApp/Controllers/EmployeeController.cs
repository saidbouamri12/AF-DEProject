using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarApp.Core.Models.Compta;
using StarApp.Core.ModelsView;
using StartApp.EF.DBContext;

namespace StartApp.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public EmployeeController(ApplicationDbContext context, IWebHostEnvironment environment) 
        { 
          _context = context;
          _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            // var Employee = await _context.Employees.ToListAsync();
            var Employee = await (from Emp in _context.Employees
                                  join Spec in _context.Specialty on Emp.Specialtyid equals Spec.id
                                  join status in _context.RelationshipStatus on Emp.RelationshipStatusid equals status.Id
                                  join contracttype in _context.ContractType on Emp.ContractTypeid equals contracttype.Id
                                  select new EmployeeDetails
                                  {
                                      Id= Emp.Id,
                                      Nom = Emp.Nom,
                                      Prenom = Emp.prenom,
                                      Cin = Emp.cin!,
                                      Tel = Emp.Tel!,
                                      Relationship = status.Label,
                                      NomberEnfant = (int)Emp.Nomberenfant,
                                      NumCnss = Emp.Num_Cnss!,
                                      DateEmboche = (DateTime)Emp.Dateemboche!,
                                      DateEnd = (DateTime)Emp.Dateend!,
                                      Salaire = (decimal)Emp.Salaire!,
                                      Specialty = Spec.label,
                                      ContractType = contracttype.label,

                                  }).ToListAsync();
            
            return View(Employee);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Specialty = await _context.Specialty.ToListAsync();
            ViewBag.ContractType = await _context.ContractType.ToListAsync();
            ViewBag.RelationshipStatus = await _context.RelationshipStatus.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            try
            {

                
                ModelState.Remove("CarteCin");
                ModelState.Remove("Contract");
                ModelState.Remove("Cartecnss");
                ModelState.Remove("Specialty");
                ModelState.Remove("RelationshipStatus");
                ModelState.Remove("ContractTypes");

                if(ModelState.IsValid)
                {
                    var exist = await _context.Employees.Where(x => x.cin == model.cin || x.Num_Cnss == model.Num_Cnss).FirstOrDefaultAsync();
                    if(exist != null)
                    {
                        TempData["Error"] = "Employee ete exist";
                        return RedirectToAction("Index");
                    }

                    if(model.CarteCin != null)
                    {
                        string ImageFolder = Path.Combine(_environment.WebRootPath, "images");
                        string imagepath = Path.Combine(ImageFolder, model.CarteCin.FileName);
                        await model.CarteCin.CopyToAsync(new FileStream(imagepath, FileMode.Create));
                        model.CarteCinPath = model.CarteCin.FileName;
                    }

                    if(model.Cartecnss !=null)
                    {
                        string ImageFolder = Path.Combine(_environment.WebRootPath, "images");
                        string imagepath = Path.Combine(ImageFolder, model.Cartecnss.FileName);
                        await model.Cartecnss.CopyToAsync(new FileStream(imagepath, FileMode.Create));
                        model.CartecnssPath = model.Cartecnss.FileName;
                    }
                    if(model.Contract != null)
                    {
                        string ImageFolder = Path.Combine(_environment.WebRootPath, "images");
                        string imagepath = Path.Combine(ImageFolder, model.Contract.FileName);
                        await model.Contract.CopyToAsync(new FileStream(imagepath, FileMode.Create));
                        model.ContractPath = model.Contract.FileName;
                    }
                    _context.Employees.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.Specialty = await _context.Specialty.ToListAsync();
                ViewBag.ContractType = await _context.ContractType.ToListAsync();
                ViewBag.RelationshipStatus = await _context.RelationshipStatus.ToListAsync();
                return View(model);
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Something wrong";
                ViewBag.Specialty = await _context.Specialty.ToListAsync();
                ViewBag.ContractType = await _context.ContractType.ToListAsync();
                ViewBag.RelationshipStatus = await _context.RelationshipStatus.ToListAsync();
                return View(model);
            }
            
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _context.Employees.SingleOrDefaultAsync(x=>x.Id == id);
            if(model == null)
            {
                return NotFound();
            }
            ViewBag.Specialty = await _context.Specialty.ToListAsync();
            ViewBag.ContractType = await _context.ContractType.ToListAsync();
            ViewBag.RelationshipStatus = await _context.RelationshipStatus.ToListAsync();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee model) 
        {
            var model1 = await _context.Employees.SingleOrDefaultAsync(x => x.Id == id);
            if (model1 == null)
            {
                return NotFound();
            }
            ModelState.Remove("CarteCin");
            ModelState.Remove("Contract");
            ModelState.Remove("Cartecnss");
            ModelState.Remove("Specialty");
            ModelState.Remove("RelationshipStatus");
            ModelState.Remove("ContractTypes");
            if (ModelState.IsValid)
            {
                model1.prenom = model.prenom;
                model1.Nom = model.Nom;
                model1.Dateemboche = model.Dateemboche;
                model1.Dateend = model.Dateend;
                model1.RelationshipStatusid = model.RelationshipStatusid;
                model1.Nomberenfant = model.Nomberenfant;
                model1.Num_Cnss = model.Num_Cnss;
                model1.Salaire = model.Salaire;
                model1.Specialtyid = model.Specialtyid;
                model1.Tel = model.Tel;
                model1.cin = model.cin;
                model1.ContractTypeid = model.ContractTypeid;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Specialty = await _context.Specialty.ToListAsync();
            ViewBag.ContractType = await _context.ContractType.ToListAsync();
            ViewBag.RelationshipStatus = await _context.RelationshipStatus.ToListAsync();
            return View(model);
        }

        public async Task< IActionResult> Details(int Id)
        {
            var Employee = await(from Emp in _context.Employees
                                 join Spec in _context.Specialty on Emp.Specialtyid equals Spec.id
                                 join status in _context.RelationshipStatus on Emp.RelationshipStatusid equals status.Id
                                 join contracttype in _context.ContractType on Emp.ContractTypeid equals contracttype.Id
                                 where Emp.Id == Id
                                 select new EmployeeDetails
                                 {
                                     Id = Emp.Id,
                                     Nom = Emp.Nom,
                                     Prenom = Emp.prenom,
                                     Cin = Emp.cin,
                                     Tel = Emp.Tel!,
                                     Relationship = status.Label,
                                     NomberEnfant = (int)Emp.Nomberenfant,
                                     NumCnss = Emp.Num_Cnss,
                                     DateEmboche = (DateTime)Emp.Dateemboche,
                                     DateEnd = (DateTime)Emp.Dateend,
                                     Salaire = (decimal)Emp.Salaire,
                                     Specialty = Spec.label,
                                     ContractType = contracttype.label,
                                     ContractPath = Emp.ContractPath,
                                     CarteCinPath = Emp.CarteCinPath,
                                     CartecnssPath = Emp.CartecnssPath
                                 }).ToListAsync();

            return View(Employee);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        { 
            var emp = _context.Employees.FirstOrDefault(x => x.Id == Id);
            if(emp == null)
            {
                return NotFound();
            }
            return View();
        }

        
    }
}
