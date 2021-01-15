using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nibo.ofx.conciliation.Models
{
    public class StatementTransationItemModel
    {
        public string trntype { get; set; }
        public DateTime dtposted { get; set; }
        public decimal trnamt { get; set; }
        public string memo { get; set; }
    }
}
