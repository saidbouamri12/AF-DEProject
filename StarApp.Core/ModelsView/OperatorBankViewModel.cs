using Microsoft.AspNetCore.Http;
using StarApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarApp.Core.ModelsView
{
    public class OperatorBankViewModel
    {
        public OperatorBank OperatorBank { get; set; }
        public IFormFile File { get; set; }
    }
}
