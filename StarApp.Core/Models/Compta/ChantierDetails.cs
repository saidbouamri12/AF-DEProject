using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.Models.Compta
{
    public class ChantierDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Userid { get; set; }
        public int Chantierid { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Chantier chantier { get; set; }  
    }
}
