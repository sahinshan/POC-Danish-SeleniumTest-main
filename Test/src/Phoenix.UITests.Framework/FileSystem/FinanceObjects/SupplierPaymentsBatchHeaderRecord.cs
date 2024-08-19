using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.FileSystem.FinanceObjects
{
    public class SupplierPaymentsBatchHeaderRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public SupplierPaymentsBatchHeaderRecord() { }


        /// <summary>
        /// See the extract criteria info for a supplier payments extract file: https://careworksuk.sharepoint.com/:x:/r/Development/_layouts/15/Doc.aspx?sourcedoc=%7B612F2C51-6ED8-47A8-B712-AA5FC0FDEB8D%7D&file=CD_Finance%20Processing_06%20-%20Supplier%20Payments%20Extract%20Criteria.xlsx&action=default&mobileredirect=true
        /// 
        /// </summary>
        /// <param name="FinanceInvoiceDetailCSVFileLine">A line of the finance extract file containing the Batch Header Record information. This should be the line with Record Type property = "1" </param>
        public SupplierPaymentsBatchHeaderRecord(string FinanceInvoiceDetailCSVFileLine)
        {
            string[] elements = FinanceInvoiceDetailCSVFileLine.Split(',');

            RecordType = elements[0];
            Sequence1 = elements[1];
            InvoiceHeaders = elements[2];
            InvoiceDetails = elements[3];
            BatchID = elements[4];
            BatchRunDate = elements[5];
            BatchtoDate = elements[6];
            BatchWeek = elements[7];
            BatchMonth = elements[8];
            BatchYear = elements[9];
            NetAmount = elements[10];
            GrossAmount = elements[11];
            NetAmountCredit = elements[12];
            GrossAmountCredit = elements[13];
            NetAmountDebit = elements[14];
            GrossAmountDebit = elements[15];
            TotalVATAmount = elements[16];
            VATCode1 = elements[17];
            VATReference1 = elements[18];
            VATAmount1 = elements[19];
            VATCode2 = elements[20];
            VATReference2 = elements[21];
            VATAmount2 = elements[22];
            VATCode3 = elements[23];
            VATReference3 = elements[24];
            VATAmount3 = elements[25];
            ExtractReference = elements[26];

        }


        private string _RecordType;
        private string _Sequence1;
        private string _InvoiceHeaders;
        private string _InvoiceDetails;
        private string _BatchID;
        private string _BatchRunDate;
        private string _BatchtoDate;
        private string _BatchWeek;
        private string _BatchMonth;
        private string _BatchYear;
        private string _NetAmount;
        private string _GrossAmount;
        private string _NetAmountCredit;
        private string _GrossAmountCredit;
        private string _NetAmountDebit;
        private string _GrossAmountDebit;
        private string _TotalVATAmount;
        private string _VATCode1;
        private string _VATReference1;
        private string _VATAmount1;
        private string _VATCode2;
        private string _VATReference2;
        private string _VATAmount2;
        private string _VATCode3;
        private string _VATReference3;
        private string _VATAmount3;
        private string _ExtractReference;


        public string RecordType { get { return _RecordType.Replace("\"", ""); } set { _RecordType = value; } }
        public string Sequence1 { get { return _Sequence1.Replace("\"", ""); } set { _Sequence1 = value; } }
        public string InvoiceHeaders { get { return _InvoiceHeaders.Replace("\"", ""); } set { _InvoiceHeaders = value; } }
        public string InvoiceDetails { get { return _InvoiceDetails.Replace("\"", ""); } set { _InvoiceDetails = value; } }
        public string BatchID { get { return _BatchID.Replace("\"", ""); } set { _BatchID = value; } }
        public string BatchRunDate { get { return _BatchRunDate.Replace("\"", ""); } set { _BatchRunDate = value; } }
        public string BatchtoDate { get { return _BatchtoDate.Replace("\"", ""); } set { _BatchtoDate = value; } }
        public string BatchWeek { get { return _BatchWeek.Replace("\"", ""); } set { _BatchWeek = value; } }
        public string BatchMonth { get { return _BatchMonth.Replace("\"", ""); } set { _BatchMonth = value; } }
        public string BatchYear { get { return _BatchYear.Replace("\"", ""); } set { _BatchYear = value; } }
        public string NetAmount { get { return _NetAmount.Replace("\"", ""); } set { _NetAmount = value; } }
        public string GrossAmount { get { return _GrossAmount.Replace("\"", ""); } set { _GrossAmount = value; } }
        public string NetAmountCredit { get { return _NetAmountCredit.Replace("\"", ""); } set { _NetAmountCredit = value; } }
        public string GrossAmountCredit { get { return _GrossAmountCredit.Replace("\"", ""); } set { _GrossAmountCredit = value; } }
        public string NetAmountDebit { get { return _NetAmountDebit.Replace("\"", ""); } set { _NetAmountDebit = value; } }
        public string GrossAmountDebit { get { return _GrossAmountDebit.Replace("\"", ""); } set { _GrossAmountDebit = value; } }
        public string TotalVATAmount { get { return _TotalVATAmount.Replace("\"", ""); } set { _TotalVATAmount = value; } }
        public string VATCode1 { get { return _VATCode1.Replace("\"", ""); } set { _VATCode1 = value; } }
        public string VATReference1 { get { return _VATReference1.Replace("\"", ""); } set { _VATReference1 = value; } }
        public string VATAmount1 { get { return _VATAmount1.Replace("\"", ""); } set { _VATAmount1 = value; } }
        public string VATCode2 { get { return _VATCode2.Replace("\"", ""); } set { _VATCode2 = value; } }
        public string VATReference2 { get { return _VATReference2.Replace("\"", ""); } set { _VATReference2 = value; } }
        public string VATAmount2 { get { return _VATAmount2.Replace("\"", ""); } set { _VATAmount2 = value; } }
        public string VATCode3 { get { return _VATCode3.Replace("\"", ""); } set { _VATCode3 = value; } }
        public string VATReference3 { get { return _VATReference3.Replace("\"", ""); } set { _VATReference3 = value; } }
        public string VATAmount3 { get { return _VATAmount3.Replace("\"", ""); } set { _VATAmount3 = value; } }
        public string ExtractReference { get { return _ExtractReference.Replace("\"", ""); } set { _ExtractReference = value; } }




    }
}
