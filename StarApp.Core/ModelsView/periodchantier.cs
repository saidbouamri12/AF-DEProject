using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.ModelsView
{
    public class periodchantier
    {
        public int Id { get; set; }
        public int ChaniterID { get; set; }
        public string chantiername { get; set; }
        public DateTime datedebit { get; set; }
        public DateTime Datefin { get; set; }
        public int CountDay { get; set; }
    }
}
