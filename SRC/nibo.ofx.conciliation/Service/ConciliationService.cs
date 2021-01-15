using nibo.ofx.conciliation.Extensions;
using nibo.ofx.conciliation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace nibo.ofx.conciliation.Service
{
    public class ConciliationService
    {
        /// <summary>
        /// Conciliate OTX Files
        /// </summary>
        /// <returns></returns>
        public static StatementModel ConciliateFiles()
        {
            //TODO: Create a section to load OTX files, besides read it from directory

            StatementModel result = new StatementModel();
            List<StatementModel> statements = new List<StatementModel>();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "temp");

            foreach (var f in files)
            {
                StatementModel statement = new StatementModel();
                statement.account = new AccountModel();
                statement.banktranlist = new List<StatementTransationItemModel>();
                StatementTransationItemModel itemAtual = new StatementTransationItemModel();

                using (StreamReader sr = new StreamReader(f))
                {
                    while (sr.Peek() >= 0)
                    {
                        var line = sr.ReadLine();
                        line = line.Trim();

                        if (line.Contains("<BANKID>"))
                        { statement.account.AccountId = line.Replace("<BANKID>", string.Empty); }

                        if (line.Contains("<ACCTID>"))
                        { statement.account.AccountId = line.Replace("<ACCTID>", string.Empty); }

                        if (line.Contains("<TRNTYPE>"))
                        { itemAtual.trntype = line.Replace("<TRNTYPE>", string.Empty); }

                        if (line.Contains("<DTPOSTED>"))
                        { itemAtual.dtposted = DataUtils.DataEstConvert(line.Replace("<DTPOSTED>", string.Empty)); }

                        if (line.Contains("<TRNAMT>"))
                        { itemAtual.trnamt = Decimal.Parse(line.Replace("<TRNAMT>", string.Empty)); }

                        if (line.Contains("<MEMO>"))
                        { itemAtual.memo = line.Replace("<MEMO>", string.Empty); }

                        if (line.Contains("</STMTTRN>"))
                        {
                            statement.banktranlist.Add(itemAtual);
                            itemAtual = new StatementTransationItemModel();
                        }
                    }
                }

                statements.Add(statement);
            }

            foreach (var s in statements)
            {
                result.account.AccountId = s.account.AccountId;
                result.account.BankId = s.account.BankId;

                foreach (var item in s.banktranlist)
                {
                    var dupl = result.banktranlist.Where(t => t.dtposted == item.dtposted &&
                                                              t.memo == item.memo &&
                                                              t.trnamt == item.trnamt &&
                                                              t.trntype == item.trntype).FirstOrDefault();
                    if (dupl == null)
                    { result.banktranlist.Add(item); }
                }
            }

            result.banktranlist = result.banktranlist.OrderBy(t => t.dtposted).ToList();

            return result;
        }
    }
}
