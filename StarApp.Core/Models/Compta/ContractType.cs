using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.Models.Compta
{
    public class ContractType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string label { get; set; }
        [ForeignKey("ContractTypeid")]
        public ICollection<Employee> Employees { get; set; }
    }
}
