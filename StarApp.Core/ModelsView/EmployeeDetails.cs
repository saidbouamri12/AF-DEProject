using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.ModelsView
{
    public class EmployeeDetails
    {
        public int? Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Cin { get; set; }
        public string? Tel { get; set; }
        
        public string? Relationship { get; set; }
        public int? NomberEnfant { get; set; }
        public string? NumCnss { get; set; }
        public DateTime? DateEmboche { get; set; }
        public DateTime? DateEnd { get; set; }
        public decimal? Salaire { get; set; }
        public string? Specialty { get; set; }
        public string? ContractType { get; set; }
        public string? ContractPath { get; set; }
        public string? CarteCinPath { get; set; }
        public string? CartecnssPath { get; set; }
    }
}
