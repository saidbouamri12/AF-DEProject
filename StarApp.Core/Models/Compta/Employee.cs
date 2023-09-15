using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StarApp.Core.Models.Compta
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string prenom { get; set; }
        public string?  cin { get; set; }
        public string? Tel { get; set; }
        public int RelationshipStatusid { get; set;}
        public virtual RelationshipStatus RelationshipStatus { get; set; }
        public int? Nomberenfant { get; set;}
        public string? Num_Cnss { get; set;}
        public DateTime? Dateemboche { get; set;}
        public DateTime? Dateend { get;set;}
        public  decimal? Salaire { get; set;}
        public int Specialtyid { get; set;}
        public virtual Specialty Specialty { get; set; }
        public string? ContractPath { get; set;}
        [NotMapped]
        public IFormFile Contract { get; set;}

        
        public int ContractTypeid { get; set;}
        public virtual ContractType ContractTypes { get; set; }
        public string? CartecnssPath { get; set; }
        [NotMapped]
        public IFormFile Cartecnss {  get; set;}
        public string? CarteCinPath { get; set; }
        [NotMapped]
        public IFormFile CarteCin { get; set; }
        [ForeignKey("EmployeeId")]
        public ICollection<AttendanceRecords> AttendanceRecords { get; set; }
        [ForeignKey("EmpId")]
        public ICollection<pointage> pointage { get; set; }



    }
}
