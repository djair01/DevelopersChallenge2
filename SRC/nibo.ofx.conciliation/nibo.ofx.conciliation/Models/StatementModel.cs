using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nibo.ofx.conciliation.Models
{
    public class StatementModel
    {
        public AccountModel account { get; set; }
        public List<StatementTransationItemModel> banktranlist { get; set; }

        public StatementModel()
        {
            account = new AccountModel();
            banktranlist = new List<StatementTransationItemModel>();
        }
    }
}
