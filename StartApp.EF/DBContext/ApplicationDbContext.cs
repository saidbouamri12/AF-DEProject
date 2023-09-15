using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StarApp.Core.Models;
using StarApp.Core.Models.Compta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartApp.EF.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Banque> Banques { get; set; }
        public DbSet<OperatorBank> OperatorBanks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RelationshipStatus> RelationshipStatus { get; set; }
        public DbSet<Specialty> Specialty { get; set; }
        public DbSet<ContractType> ContractType { get; set; }
        public DbSet<Chantier> Chantiers { get; set; }
        public DbSet<ChantierDetails> ChantierDetails { get; set; }
        public DbSet<Periods> Periods { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
