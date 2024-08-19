using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.FileSystem.FinanceObjects
{
    public class SupplierPaymentsFinanceInvoiceHeader
    {
        /// <summary>
        /// 
        /// </summary>
        public SupplierPaymentsFinanceInvoiceHeader() { }


        /// <summary>
        /// See the extract criteria info for a supplier payments extract file: https://careworksuk.sharepoint.com/:x:/r/Development/_layouts/15/Doc.aspx?sourcedoc=%7B612F2C51-6ED8-47A8-B712-AA5FC0FDEB8D%7D&file=CD_Finance%20Processing_06%20-%20Supplier%20Payments%20Extract%20Criteria.xlsx&action=default&mobileredirect=true
        /// 
        /// </summary>
        /// <param name="FinanceInvoiceDetailCSVFileLine">A line of the finance extract file containing the Finance Invoice Header information. This should be the line with Record Type property = "2" </param>
        public SupplierPaymentsFinanceInvoiceHeader(string FinanceInvoiceDetailCSVFileLine)
        {
            string[] elements = FinanceInvoiceDetailCSVFileLine.Split(',');

            RecordType = elements[0];
            Sequence1 = elements[1];
            Sequence2 = elements[2];
            InvoiceDetails = elements[3];
            BatchID = elements[4];
            BatchRunDate = elements[5];
            BatchtoDate = elements[6];
            DebitorCredit = elements[7];
            NetAmount = elements[8];
            GrossAmount = elements[9];
            VATAmount = elements[10];
            VATCode1 = elements[11];
            VATReference1 = elements[12];
            VATAmount1 = elements[13];
            VATCode2 = elements[14];
            VATReference2 = elements[15];
            VATAmount2 = elements[16];
            VATCode3 = elements[17];
            VATReference3 = elements[18];
            VATAmount3 = elements[19];
            Service = elements[20];
            PaymentType = elements[21];
            Provider = elements[22];
            ProviderID = elements[23];
            CreditorReferenceNumber = elements[24];
            ReferenceCode = elements[25];
            InvoiceNumber = elements[26];
            ProviderInvoiceNumber = elements[27];
            InvoiceDate = elements[28];
            InvoiceReceivedDate = elements[29];
            ProviderPropertyNo = elements[30];
            ProviderStreet = elements[31];
            ProviderVillage = elements[32];
            ProviderTown = elements[33];
            ProviderCounty = elements[34];
            ProviderPostcode = elements[35];


        }

        private string _RecordType;
        private string _Sequence1;
        private string _Sequence2;
        private string _InvoiceDetails;
        private string _BatchID;
        private string _BatchRunDate;
        private string _BatchtoDate;
        private string _DebitorCredit;
        private string _NetAmount;
        private string _GrossAmount;
        private string _VATAmount;
        private string _VATCode1;
        private string _VATReference1;
        private string _VATAmount1;
        private string _VATCode2;
        private string _VATReference2;
        private string _VATAmount2;
        private string _VATCode3;
        private string _VATReference3;
        private string _VATAmount3;
        private string _Service;
        private string _PaymentType;
        private string _Provider;
        private string _ProviderID;
        private string _CreditorReferenceNumber;
        private string _ReferenceCode;
        private string _InvoiceNumber;
        private string _ProviderInvoiceNumber;
        private string _InvoiceDate;
        private string _InvoiceReceivedDate;
        private string _ProviderPropertyNo;
        private string _ProviderStreet;
        private string _ProviderVillage;
        private string _ProviderTown;
        private string _ProviderCounty;
        private string _ProviderPostcode;


        public string RecordType { get { return _RecordType.Replace("\"", ""); } set { _RecordType = value; } }
        public string Sequence1 { get { return _Sequence1.Replace("\"", ""); } set { _Sequence1 = value; } }
        public string Sequence2 { get { return _Sequence2.Replace("\"", ""); } set { _Sequence2 = value; } }
        public string InvoiceDetails { get { return _InvoiceDetails.Replace("\"", ""); } set { _InvoiceDetails = value; } }
        public string BatchID { get { return _BatchID.Replace("\"", ""); } set { _BatchID = value; } }
        public string BatchRunDate { get { return _BatchRunDate.Replace("\"", ""); } set { _BatchRunDate = value; } }
        public string BatchtoDate { get { return _BatchtoDate.Replace("\"", ""); } set { _BatchtoDate = value; } }
        public string DebitorCredit { get { return _DebitorCredit.Replace("\"", ""); } set { _DebitorCredit = value; } }
        public string NetAmount { get { return _NetAmount.Replace("\"", ""); } set { _NetAmount = value; } }
        public string GrossAmount { get { return _GrossAmount.Replace("\"", ""); } set { _GrossAmount = value; } }
        public string VATAmount { get { return _VATAmount.Replace("\"", ""); } set { _VATAmount = value; } }
        public string VATCode1 { get { return _VATCode1.Replace("\"", ""); } set { _VATCode1 = value; } }
        public string VATReference1 { get { return _VATReference1.Replace("\"", ""); } set { _VATReference1 = value; } }
        public string VATAmount1 { get { return _VATAmount1.Replace("\"", ""); } set { _VATAmount1 = value; } }
        public string VATCode2 { get { return _VATCode2.Replace("\"", ""); } set { _VATCode2 = value; } }
        public string VATReference2 { get { return _VATReference2.Replace("\"", ""); } set { _VATReference2 = value; } }
        public string VATAmount2 { get { return _VATAmount2.Replace("\"", ""); } set { _VATAmount2 = value; } }
        public string VATCode3 { get { return _VATCode3.Replace("\"", ""); } set { _VATCode3 = value; } }
        public string VATReference3 { get { return _VATReference3.Replace("\"", ""); } set { _VATReference3 = value; } }
        public string VATAmount3 { get { return _VATAmount3.Replace("\"", ""); } set { _VATAmount3 = value; } }
        public string Service { get { return _Service.Replace("\"", ""); } set { _Service = value; } }
        public string PaymentType { get { return _PaymentType.Replace("\"", ""); } set { _PaymentType = value; } }
        public string Provider { get { return _Provider.Replace("\"", ""); } set { _Provider = value; } }
        public string ProviderID { get { return _ProviderID.Replace("\"", ""); } set { _ProviderID = value; } }
        public string CreditorReferenceNumber { get { return _CreditorReferenceNumber.Replace("\"", ""); } set { _CreditorReferenceNumber = value; } }
        public string ReferenceCode { get { return _ReferenceCode.Replace("\"", ""); } set { _ReferenceCode = value; } }
        public string InvoiceNumber { get { return _InvoiceNumber.Replace("\"", ""); } set { _InvoiceNumber = value; } }
        public string ProviderInvoiceNumber { get { return _ProviderInvoiceNumber.Replace("\"", ""); } set { _ProviderInvoiceNumber = value; } }
        public string InvoiceDate { get { return _InvoiceDate.Replace("\"", ""); } set { _InvoiceDate = value; } }
        public string InvoiceReceivedDate { get { return _InvoiceReceivedDate.Replace("\"", ""); } set { _InvoiceReceivedDate = value; } }
        public string ProviderPropertyNo { get { return _ProviderPropertyNo.Replace("\"", ""); } set { _ProviderPropertyNo = value; } }
        public string ProviderStreet { get { return _ProviderStreet.Replace("\"", ""); } set { _ProviderStreet = value; } }
        public string ProviderVillage { get { return _ProviderVillage.Replace("\"", ""); } set { _ProviderVillage = value; } }
        public string ProviderTown { get { return _ProviderTown.Replace("\"", ""); } set { _ProviderTown = value; } }
        public string ProviderCounty { get { return _ProviderCounty.Replace("\"", ""); } set { _ProviderCounty = value; } }
        public string ProviderPostcode { get { return _ProviderPostcode.Replace("\"", ""); } set { _ProviderPostcode = value; } }


    }
}
