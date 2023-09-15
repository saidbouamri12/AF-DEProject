using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.ModelsView
{
    public class Bankoperatorview
    {
       
        public int Id { get; set; }
        public string BanqueName { get; set; }
        public DateTime Date { get; set; }
        public string Paytype { get; set; }
        public string payCode { get; set; }
        public string? Ste { get; set; }
        public string? Tel { get; set; }
        public string MontantTTC { get; set; }
        public string? Comercant { get; set; }
        public string? Nfacture { get; set; }
        public string? DeclarationTVA { get; set; }
        public string? FactureOrigin { get; set; }
        public string? FacturePath { get; set; }

    }
}
