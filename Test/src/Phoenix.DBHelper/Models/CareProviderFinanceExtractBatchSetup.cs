using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceExtractBatchSetup : BaseClass
    {
        private string TableName = "CareProviderFinanceExtractBatchSetup";
        private string PrimaryKeyName = "CareProviderFinanceExtractBatchSetupId";

        public CareProviderFinanceExtractBatchSetup()
        {
            AuthenticateUser();
        }

        public CareProviderFinanceExtractBatchSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderFinanceExtractBatchSetup(Guid ownerid,
            DateTime startdate, TimeSpan starttime, DateTime? enddate, Guid careproviderextractnameid, int extractfrequencyid, Guid careproviderextracttypeid,
            Guid mailmergeid, Guid? EmailSenderId = null, string EmailSenderIdTableName = "", string EmailSenderIdName = "", bool extractonmonday = false, bool extractontuesday = false, bool extractonwednesday = false, bool extractonthursday = false,
            bool extractonfriday = false, bool extractonsaturday = false, bool extractonsunday = false,
            bool excludezerotransactions = true, bool excludezerotransactionsfrominvoice = true, bool generateandsendinvoicesautomatically = false,
            int CPFinanceExtractBatchPaymentTermsId = 1, int PaymentTermUnits = 1, bool showroomnumber = false, bool showinvoicetext = true, bool showweeklybreakdown = true,
            int cpfinanceextractbatchpaymentdetailid = 1, string vatglcode = "", int invoicetransactionsgroupingid = 2, string extractreference = "")
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(businessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(businessDataObject, "starttime", starttime);
            AddFieldToBusinessDataObject(businessDataObject, "enddate", enddate);

            AddFieldToBusinessDataObject(businessDataObject, "careproviderextractnameid", careproviderextractnameid);
            AddFieldToBusinessDataObject(businessDataObject, "extractfrequencyid", extractfrequencyid);
            AddFieldToBusinessDataObject(businessDataObject, "careproviderextracttypeid", careproviderextracttypeid);

            AddFieldToBusinessDataObject(businessDataObject, "extractonmonday", extractonmonday);
            AddFieldToBusinessDataObject(businessDataObject, "extractontuesday", extractontuesday);
            AddFieldToBusinessDataObject(businessDataObject, "extractonwednesday", extractonwednesday);

            AddFieldToBusinessDataObject(businessDataObject, "extractonthursday", extractonthursday);
            AddFieldToBusinessDataObject(businessDataObject, "extractonfriday", extractonfriday);
            AddFieldToBusinessDataObject(businessDataObject, "extractonsaturday", extractonsaturday);
            AddFieldToBusinessDataObject(businessDataObject, "extractonsunday", extractonsunday);

            AddFieldToBusinessDataObject(businessDataObject, "excludezerotransactions", excludezerotransactions);
            AddFieldToBusinessDataObject(businessDataObject, "excludezerotransactionsfrom", excludezerotransactionsfrominvoice);
            AddFieldToBusinessDataObject(businessDataObject, "generateandsendinvoicesautomatically", generateandsendinvoicesautomatically);
            AddFieldToBusinessDataObject(businessDataObject, "vatglcode", vatglcode);
            AddFieldToBusinessDataObject(businessDataObject, "mailmergeid", mailmergeid);
            AddFieldToBusinessDataObject(businessDataObject, "invoicetransactionsgroupingid", invoicetransactionsgroupingid);
            AddFieldToBusinessDataObject(businessDataObject, "cpfinanceextractbatchpaymenttermsid", CPFinanceExtractBatchPaymentTermsId);
            AddFieldToBusinessDataObject(businessDataObject, "paymenttermunits", PaymentTermUnits);
            AddFieldToBusinessDataObject(businessDataObject, "showroomnumber", showroomnumber);
            AddFieldToBusinessDataObject(businessDataObject, "showinvoicetext", showinvoicetext);
            AddFieldToBusinessDataObject(businessDataObject, "showweeklybreakdown", showweeklybreakdown);
            AddFieldToBusinessDataObject(businessDataObject, "cpfinanceextractbatchpaymentdetailid", cpfinanceextractbatchpaymentdetailid);
            AddFieldToBusinessDataObject(businessDataObject, "extractreference", extractreference);

            AddFieldToBusinessDataObject(businessDataObject, "emailsenderid", EmailSenderId);
            AddFieldToBusinessDataObject(businessDataObject, "emailsenderidtablename", EmailSenderIdTableName);
            AddFieldToBusinessDataObject(businessDataObject, "emailsenderidname", EmailSenderIdName);

            AddFieldToBusinessDataObject(businessDataObject, "validforexport", false);
            AddFieldToBusinessDataObject(businessDataObject, "inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetByCareProviderExtractNameId(Guid CareProviderExtractNameId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careproviderextractnameid", ConditionOperatorType.Equal, CareProviderExtractNameId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateCareProviderFinanceExtractBatchSetupExcludeZeroTransactions(Guid CareProviderFinanceExtractBatchSetupId, bool ExcludeZeroTransactions)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("CareProviderFinanceExtractBatchSetup", "CareProviderFinanceExtractBatchSetupid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "careproviderfinanceextractbatchsetupid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceExtractBatchSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "excludezerotransactions", DataType.Boolean, BusinessObjectFieldType.Unknown, false, ExcludeZeroTransactions);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCareProviderFinanceExtractBatchSetupExtractOnDayFlagToYesByCurrentDayOfWeek(Guid CareProviderFinanceExtractBatchSetupId, DateTime date)
        {
            var businessDataObject = GetBusinessDataBaseObject("CareProviderFinanceExtractBatchSetup", "CareProviderFinanceExtractBatchSetupid");

            this.AddFieldToBusinessDataObject(businessDataObject, "careproviderfinanceextractbatchsetupid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceExtractBatchSetupId);

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    AddFieldToBusinessDataObject(businessDataObject, "extractonsunday", true);
                    break;

                case DayOfWeek.Monday:
                    AddFieldToBusinessDataObject(businessDataObject, "extractonmonday", true);
                    break;

                case DayOfWeek.Tuesday:
                    AddFieldToBusinessDataObject(businessDataObject, "extractontuesday", true);
                    break;

                case DayOfWeek.Wednesday:
                    AddFieldToBusinessDataObject(businessDataObject, "extractonwednesday", true);
                    break;

                case DayOfWeek.Thursday:
                    AddFieldToBusinessDataObject(businessDataObject, "extractonthursday", true);
                    break;

                case DayOfWeek.Friday:
                    AddFieldToBusinessDataObject(businessDataObject, "extractonfriday", true);
                    break;

                case DayOfWeek.Saturday:
                    AddFieldToBusinessDataObject(businessDataObject, "extractonsaturday", true);
                    break;

                default:
                    break;
            }



            this.UpdateRecord(businessDataObject);
        }

        public void UpdateCareProviderFinanceExtractBatchSetupSeparateCreditExtract(Guid CareProviderFinanceExtractBatchSetupId, bool SeparateCreditExtract)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("CareProviderFinanceExtractBatchSetup", "CareProviderFinanceExtractBatchSetupid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderFinanceExtractBatchSetupid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceExtractBatchSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "SeparateCreditExtract", DataType.Boolean, BusinessObjectFieldType.Unknown, false, SeparateCreditExtract);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateGenerateAndSendInvoicesAutomaticallyFlag(Guid CareProviderFinanceExtractBatchSetupId, bool generateandsendinvoicesautomatically)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("CareProviderFinanceExtractBatchSetup", "CareProviderFinanceExtractBatchSetupid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderFinanceExtractBatchSetupid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceExtractBatchSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "generateandsendinvoicesautomatically", DataType.Boolean, BusinessObjectFieldType.Unknown, false, generateandsendinvoicesautomatically);

            this.UpdateRecord(buisinessDataObject);
        }


        public void DeleteCareProviderFinanceExtractBatchSetup(Guid CareProviderFinanceExtractBatchSetupId)
        {
            this.DeleteRecord(TableName, CareProviderFinanceExtractBatchSetupId);
        }

        public void UpdateEmailSender(Guid CareProviderFinanceExtractBatchSetupId, Guid EmailSenderId, string EmailSenderIdTableName, string EmailSenderIdName)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("CareProviderFinanceExtractBatchSetup", "CareProviderFinanceExtractBatchSetupid");

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderFinanceExtractBatchSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "emailsenderid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, EmailSenderId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "emailsenderidtablename", DataType.Text, BusinessObjectFieldType.Unknown, false, EmailSenderIdTableName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "emailsenderidname", DataType.Text, BusinessObjectFieldType.Unknown, false, EmailSenderIdName);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
