using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.FileSystem.FinanceObjects
{
    public class SupplierPaymentsFinanceInvoiceDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public SupplierPaymentsFinanceInvoiceDetail() { }


        /// <summary>
        /// See the extract criteria info for a supplier payments extract file: https://careworksuk.sharepoint.com/:x:/r/Development/_layouts/15/Doc.aspx?sourcedoc=%7B612F2C51-6ED8-47A8-B712-AA5FC0FDEB8D%7D&file=CD_Finance%20Processing_06%20-%20Supplier%20Payments%20Extract%20Criteria.xlsx&action=default&mobileredirect=true
        /// 
        /// </summary>
        /// <param name="FinanceInvoiceDetailCSVFileLine">A line of the finance extract file containing the finance invoice detail. This should be the line with Record Type property = "3" </param>
        public SupplierPaymentsFinanceInvoiceDetail(string FinanceInvoiceDetailCSVFileLine)
        {
            string[] elements = FinanceInvoiceDetailCSVFileLine.Split(',');

            RecordType = elements[0];
            Sequence1 = elements[1];
            Sequence2 = elements[2];
            Sequence3 = elements[3];
            BatchID = elements[4];
            BatchRunDate = elements[5];
            BatchtoDate = elements[6];
            DebitorCredit = elements[7];
            BatchWeek = elements[8];
            BatchMonth = elements[9];
            BatchYear = elements[10];
            Provider = elements[11];
            ProviderID = elements[12];
            CreditorReferenceNumber = elements[13];
            ReferenceCode = elements[14];
            InvoiceNumber = elements[15];
            ProviderInvoiceNumber = elements[16];
            InvoiceDate = elements[17];
            InvoiceReceivedDate = elements[18];
            NetAmount = elements[19];
            GrossAmount = elements[20];
            VATAmount = elements[21];
            VATCode = elements[22];
            VATReference = elements[23];
            StartDate = elements[24];
            EndDate = elements[25];
            TransactionNo = elements[26];
            TransactionClass = elements[27];
            TransactionType = elements[28];
            GLCode = elements[29];
            VATGLCode = elements[30];
            RateUnit = elements[31];
            TotalUnits = elements[32];
            ServiceProvisionID = elements[33];
            Service = elements[34];
            ServiceElement2 = elements[35];
            FinanceClientCategory = elements[36];
            PaymentType = elements[37];
            ContractType = elements[38];
            Person = elements[39];
            PersonID = elements[40];
            DateofBirth = elements[41];
            AgeGroup = elements[42];
            NHSNumber = elements[43];
            NINumber = elements[44];
            PrimarySupportReason = elements[45];
            ProviderPropertyNo = elements[46];
            ProviderStreet = elements[47];
            ProviderVillage = elements[48];
            ProviderTown = elements[49];
            ProviderCounty = elements[50];
            ProviderPostcode = elements[51];
            ExtractReference = elements[52];
            FinancialAssessmentID = elements[53];
            ChargingRule = elements[54];
            RecoveryMethod = elements[55];
            ContributionID = elements[56];
        }


        private string _RecordType;
        private string _Sequence1;
        private string _Sequence2;
        private string _Sequence3;
        private string _BatchID;
        private string _BatchRunDate;
        private string _BatchtoDate;
        private string _DebitorCredit;
        private string _BatchWeek;
        private string _BatchMonth;
        private string _BatchYear;
        private string _Provider;
        private string _ProviderID;
        private string _CreditorReferenceNumber;
        private string _ReferenceCode;
        private string _InvoiceNumber;
        private string _ProviderInvoiceNumber;
        private string _InvoiceDate;
        private string _InvoiceReceivedDate;
        private string _NetAmount;
        private string _GrossAmount;
        private string _VATAmount;
        private string _VATCode;
        private string _VATReference;
        private string _StartDate;
        private string _EndDate;
        private string _TransactionNo;
        private string _TransactionClass;
        private string _TransactionType;
        private string _GLCode;
        private string _VATGLCode;
        private string _RateUnit;
        private string _TotalUnits;
        private string _ServiceProvisionID;
        private string _Service;
        private string _ServiceElement2;
        private string _FinanceClientCategory;
        private string _PaymentType;
        private string _ContractType;
        private string _Person;
        private string _PersonID;
        private string _DateofBirth;
        private string _AgeGroup;
        private string _NHSNumber;
        private string _NINumber;
        private string _PrimarySupportReason;
        private string _ProviderPropertyNo;
        private string _ProviderStreet;
        private string _ProviderVillage;
        private string _ProviderTown;
        private string _ProviderCounty;
        private string _ProviderPostcode;
        private string _ExtractReference;
        private string _FinancialAssessmentID;
        private string _ChargingRule;
        private string _RecoveryMethod;
        private string _ContributionID;



        public string RecordType { get { return _RecordType.Replace("\"", ""); } set { _RecordType = value; } }
        public string Sequence1 { get { return _Sequence1.Replace("\"", ""); } set { _Sequence1 = value; } }
        public string Sequence2 { get { return _Sequence2.Replace("\"", ""); } set { _Sequence2 = value; } }
        public string Sequence3 { get { return _Sequence3.Replace("\"", ""); } set { _Sequence3 = value; } }
        public string BatchID { get { return _BatchID.Replace("\"", ""); } set { _BatchID = value; } }
        public string BatchRunDate { get { return _BatchRunDate.Replace("\"", ""); } set { _BatchRunDate = value; } }
        public string BatchtoDate { get { return _BatchtoDate.Replace("\"", ""); } set { _BatchtoDate = value; } }
        public string DebitorCredit { get { return _DebitorCredit.Replace("\"", ""); } set { _DebitorCredit = value; } }
        public string BatchWeek { get { return _BatchWeek.Replace("\"", ""); } set { _BatchWeek = value; } }
        public string BatchMonth { get { return _BatchMonth.Replace("\"", ""); } set { _BatchMonth = value; } }
        public string BatchYear { get { return _BatchYear.Replace("\"", ""); } set { _BatchYear = value; } }
        public string Provider { get { return _Provider.Replace("\"", ""); } set { _Provider = value; } }
        public string ProviderID { get { return _ProviderID.Replace("\"", ""); } set { _ProviderID = value; } }
        public string CreditorReferenceNumber { get { return _CreditorReferenceNumber.Replace("\"", ""); } set { _CreditorReferenceNumber = value; } }
        public string ReferenceCode { get { return _ReferenceCode.Replace("\"", ""); } set { _ReferenceCode = value; } }
        public string InvoiceNumber { get { return _InvoiceNumber.Replace("\"", ""); } set { _InvoiceNumber = value; } }
        public string ProviderInvoiceNumber { get { return _ProviderInvoiceNumber.Replace("\"", ""); } set { _ProviderInvoiceNumber = value; } }
        public string InvoiceDate { get { return _InvoiceDate.Replace("\"", ""); } set { _InvoiceDate = value; } }
        public string InvoiceReceivedDate { get { return _InvoiceReceivedDate.Replace("\"", ""); } set { _InvoiceReceivedDate = value; } }
        public string NetAmount { get { return _NetAmount.Replace("\"", ""); } set { _NetAmount = value; } }
        public string GrossAmount { get { return _GrossAmount.Replace("\"", ""); } set { _GrossAmount = value; } }
        public string VATAmount { get { return _VATAmount.Replace("\"", ""); } set { _VATAmount = value; } }
        public string VATCode { get { return _VATCode.Replace("\"", ""); } set { _VATCode = value; } }
        public string VATReference { get { return _VATReference.Replace("\"", ""); } set { _VATReference = value; } }
        public string StartDate { get { return _StartDate.Replace("\"", ""); } set { _StartDate = value; } }
        public string EndDate { get { return _EndDate.Replace("\"", ""); } set { _EndDate = value; } }
        public string TransactionNo { get { return _TransactionNo.Replace("\"", ""); } set { _TransactionNo = value; } }
        public string TransactionClass { get { return _TransactionClass.Replace("\"", ""); } set { _TransactionClass = value; } }
        public string TransactionType { get { return _TransactionType.Replace("\"", ""); } set { _TransactionType = value; } }
        public string GLCode { get { return _GLCode.Replace("\"", ""); } set { _GLCode = value; } }
        public string VATGLCode { get { return _VATGLCode.Replace("\"", ""); } set { _VATGLCode = value; } }
        public string RateUnit { get { return _RateUnit.Replace("\"", ""); } set { _RateUnit = value; } }
        public string TotalUnits { get { return _TotalUnits.Replace("\"", ""); } set { _TotalUnits = value; } }
        public string ServiceProvisionID { get { return _ServiceProvisionID.Replace("\"", ""); } set { _ServiceProvisionID = value; } }
        public string Service { get { return _Service.Replace("\"", ""); } set { _Service = value; } }
        public string ServiceElement2 { get { return _ServiceElement2.Replace("\"", ""); } set { _ServiceElement2 = value; } }
        public string FinanceClientCategory { get { return _FinanceClientCategory.Replace("\"", ""); } set { _FinanceClientCategory = value; } }
        public string PaymentType { get { return _PaymentType.Replace("\"", ""); } set { _PaymentType = value; } }
        public string ContractType { get { return _ContractType.Replace("\"", ""); } set { _ContractType = value; } }
        public string Person { get { return _Person.Replace("\"", ""); } set { _Person = value; } }
        public string PersonID { get { return _PersonID.Replace("\"", ""); } set { _PersonID = value; } }
        public string DateofBirth { get { return _DateofBirth.Replace("\"", ""); } set { _DateofBirth = value; } }
        public string AgeGroup { get { return _AgeGroup.Replace("\"", ""); } set { _AgeGroup = value; } }
        public string NHSNumber { get { return _NHSNumber.Replace("\"", ""); } set { _NHSNumber = value; } }
        public string NINumber { get { return _NINumber.Replace("\"", ""); } set { _NINumber = value; } }
        public string PrimarySupportReason { get { return _PrimarySupportReason.Replace("\"", ""); } set { _PrimarySupportReason = value; } }
        public string ProviderPropertyNo { get { return _ProviderPropertyNo.Replace("\"", ""); } set { _ProviderPropertyNo = value; } }
        public string ProviderStreet { get { return _ProviderStreet.Replace("\"", ""); } set { _ProviderStreet = value; } }
        public string ProviderVillage { get { return _ProviderVillage.Replace("\"", ""); } set { _ProviderVillage = value; } }
        public string ProviderTown { get { return _ProviderTown.Replace("\"", ""); } set { _ProviderTown = value; } }
        public string ProviderCounty { get { return _ProviderCounty.Replace("\"", ""); } set { _ProviderCounty = value; } }
        public string ProviderPostcode { get { return _ProviderPostcode.Replace("\"", ""); } set { _ProviderPostcode = value; } }
        public string ExtractReference { get { return _ExtractReference.Replace("\"", ""); } set { _ExtractReference = value; } }
        public string FinancialAssessmentID { get { return _FinancialAssessmentID.Replace("\"", ""); } set { _FinancialAssessmentID = value; } }
        public string ChargingRule { get { return _ChargingRule.Replace("\"", ""); } set { _ChargingRule = value; } }
        public string RecoveryMethod { get { return _RecoveryMethod.Replace("\"", ""); } set { _RecoveryMethod = value; } }
        public string ContributionID { get { return _ContributionID.Replace("\"", ""); } set { _ContributionID = value; } }



    }
}
