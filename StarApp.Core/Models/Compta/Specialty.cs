using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.Models.Compta
{
    public class Specialty
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string label { get; set; }
        [ForeignKey("Specialtyid")]
        public ICollection<Employee> Employees { get; set; }
    }
}
