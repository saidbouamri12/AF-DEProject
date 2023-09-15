using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.Models.Compta
{
    public class Chantier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Name { get; set; }

        public DateTime? DebitDate { get; set; }

        public DateTime? DateFin { get; set; }
        public bool Complete { get; set; }

        [ForeignKey("Chantierid")]
        public ICollection<ChantierDetails> ChantierDetails { get; set; }
        [ForeignKey("ChaniterID")]
        public ICollection<Periods> Periods { get; set; }

    }
}
