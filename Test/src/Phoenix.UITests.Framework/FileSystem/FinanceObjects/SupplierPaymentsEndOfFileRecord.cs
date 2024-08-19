using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.FileSystem.FinanceObjects
{
    public class SupplierPaymentsEndOfFileRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public SupplierPaymentsEndOfFileRecord() { }


        /// <summary>
        /// See the extract criteria info for a supplier payments extract file: https://careworksuk.sharepoint.com/:x:/r/Development/_layouts/15/Doc.aspx?sourcedoc=%7B612F2C51-6ED8-47A8-B712-AA5FC0FDEB8D%7D&file=CD_Finance%20Processing_06%20-%20Supplier%20Payments%20Extract%20Criteria.xlsx&action=default&mobileredirect=true
        /// 
        /// </summary>
        /// <param name="FinanceInvoiceDetailCSVFileLine">A line of the finance extract file containing the End of File Record information. This should be the line with Record Type property = "4" </param>
        public SupplierPaymentsEndOfFileRecord(string FinanceInvoiceDetailCSVFileLine)
        {
            string[] elements = FinanceInvoiceDetailCSVFileLine.Split(',');

            RecordType = elements[0];
            Sequence1 = elements[1];



        }

        public string RecordType { get; set; }
        public string Sequence1 { get; set; }


    }
}
