using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.Models
{
    public  class OperatorBank
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("BanqueId")]
        public int BanqueId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Paytype { get; set; }
        [Required]
        public string payCode { get; set; }
        public string? Ste { get; set; }
        public string? Tel { get; set; }
        [Required]
        public string MontantTTC { get; set; }
        public string?  Comercant { get; set; }
        public string? Nfacture { get; set; }
        public string? DeclarationTVA { get; set; }
        public string? FactureOrigin { get; set; }
        public string? FacturePath { get; set; }
        //[NotMapped]
        //public IFormFile FacturePath { get; set; }
        public virtual Banque banques { get; set; }
        
    }
}
