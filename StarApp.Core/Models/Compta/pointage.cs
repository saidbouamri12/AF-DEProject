using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.Models.Compta
{
    public class pointage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int EmpId { get; set; }
        public int periodId  { get; set; }
        public DateTime Date { get; set; }    
        public virtual Employee Employee { get; set; }
        public virtual Periods Periods { get; set; }
    }
}
