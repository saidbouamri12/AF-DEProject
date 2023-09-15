using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.ModelsView
{
    public class chantieruserindexview
    {
       
            public int ID { get; set; }
            public int chantierId { get; set; } 
            public string? Name { get; set; }

            public DateTime? DebitDate { get; set; }

            public DateTime? DateFin { get; set; }
            public bool? Complete { get; set; }

            public string? userid { get; set; }
            public string? fullname { get; set; }   
        
    }
}
