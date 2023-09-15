using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.Models.Compta
{
    public class Periods
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public int ChaniterID { get; set; }
        public DateTime datedebit { get; set; }  
        public DateTime Datefin { get; set; }
        public int CountDay { get; set; } 
        public virtual Chantier Chantier { get; set; }
        [ForeignKey("periodId")]
        public ICollection<AttendanceRecords> AttendanceRecords { get; set; }
        [ForeignKey("periodId")]
        public ICollection<pointage> pointage { get; set; }
    }
}
