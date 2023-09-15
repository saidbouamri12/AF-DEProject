using Microsoft.AspNetCore.Identity;
using StarApp.Core.Models.Compta;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string fullname { get; set; }
        //public string username { get; set; }

        [ForeignKey("Userid")]
        public ICollection<ChantierDetails> ChantierDetails { get; set; }

        //public ICollection<AttendanceRecords> AttendanceRecords { get; set; }

    }
}
