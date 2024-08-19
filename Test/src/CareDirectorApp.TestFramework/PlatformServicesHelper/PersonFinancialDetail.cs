using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class PersonFinancialDetail : BaseClass
    {

        public string TableName = "PersonFinancialDetail";
        public string PrimaryKeyName = "PersonFinancialDetailId";
        

        public PersonFinancialDetail(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Guid CreatePersonFinancialDetail(Guid ownerid, Guid personid, Guid financialdetailid, Guid? frequencyofreceiptid,
            DateTime startdate, DateTime? enddate,
            int financialdetailtypeid, string reference,
            decimal amount, decimal jointamount, 
            bool beingreceived, bool excludefromdwpcalculation, bool showreferenceinschedule, bool deferredpaymentschemesecurity)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialdetailid", financialdetailid);
            if(frequencyofreceiptid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "frequencyofreceiptid", frequencyofreceiptid.Value);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            if(enddate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialdetailtypeid", financialdetailtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "reference", reference);
            AddFieldToBusinessDataObject(buisinessDataObject, "amount", amount);
            AddFieldToBusinessDataObject(buisinessDataObject, "jointamount", jointamount);
            AddFieldToBusinessDataObject(buisinessDataObject, "beingreceived", beingreceived);
            AddFieldToBusinessDataObject(buisinessDataObject, "excludefromdwpcalculation", excludefromdwpcalculation);
            AddFieldToBusinessDataObject(buisinessDataObject, "showreferenceinschedule", showreferenceinschedule);
            AddFieldToBusinessDataObject(buisinessDataObject, "deferredpaymentschemesecurity", deferredpaymentschemesecurity);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public Guid CreatePersonFinancialDetail(Guid ownerid, Guid personid, Guid financialdetailid, Guid? frequencyofreceiptid,
            DateTime startdate, DateTime? enddate, DateTime? applicationdate,
            int financialdetailtypeid, string reference,
            decimal amount, decimal? jointamount, decimal arrears,
            bool beingreceived, bool excludefromdwpcalculation, bool showreferenceinschedule, bool deferredpaymentschemesecurity)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialdetailid", financialdetailid);
            if (frequencyofreceiptid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "frequencyofreceiptid", frequencyofreceiptid.Value);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            if (enddate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);
            if (applicationdate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "applicationdate", applicationdate.Value);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialdetailtypeid", financialdetailtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "reference", reference);
            AddFieldToBusinessDataObject(buisinessDataObject, "amount", amount);
            if(jointamount.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "jointamount", jointamount.Value);
            AddFieldToBusinessDataObject(buisinessDataObject, "arrears", arrears);
            AddFieldToBusinessDataObject(buisinessDataObject, "beingreceived", beingreceived);
            AddFieldToBusinessDataObject(buisinessDataObject, "excludefromdwpcalculation", excludefromdwpcalculation);
            AddFieldToBusinessDataObject(buisinessDataObject, "showreferenceinschedule", showreferenceinschedule);
            AddFieldToBusinessDataObject(buisinessDataObject, "deferredpaymentschemesecurity", deferredpaymentschemesecurity);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        
        /// <summary>
        /// Use this method to create a person financial detail of type "Property"
        /// </summary>
        /// <param name="ownerid"></param>
        /// <param name="personid"></param>
        /// <param name="financialdetailtypeid"></param>
        /// <param name="financialdetailid"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="address"></param>
        /// <param name="propertydisregardtypeid"></param>
        /// <param name="excludefromdwpcalculation"></param>
        /// <param name="GrossValue"></param>
        /// <param name="OutstandingLoan"></param>
        /// <param name="Equity"></param>
        /// <param name="PercentageOwnership"></param>
        /// <param name="reference"></param>
        /// <param name="showreferenceinschedule"></param>
        /// <returns></returns>
        public Guid CreatePersonFinancialDetailForProperty(Guid ownerid, Guid personid, int financialdetailtypeid, Guid financialdetailid,
            DateTime startdate, DateTime? enddate,
            string address, Guid propertydisregardtypeid, bool excludefromdwpcalculation, decimal grossvalue, decimal outstandingloan, decimal equity, int percentageownership,
            string reference, bool showreferenceinschedule)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialdetailtypeid", financialdetailtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "financialdetailid", financialdetailid);

            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            if (enddate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);

            AddFieldToBusinessDataObject(buisinessDataObject, "address", address);
            AddFieldToBusinessDataObject(buisinessDataObject, "propertydisregardtypeid", propertydisregardtypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "excludefromdwpcalculation", excludefromdwpcalculation);
            AddFieldToBusinessDataObject(buisinessDataObject, "grossvalue", grossvalue);
            AddFieldToBusinessDataObject(buisinessDataObject, "outstandingloan", outstandingloan);
            AddFieldToBusinessDataObject(buisinessDataObject, "equity", equity);
            AddFieldToBusinessDataObject(buisinessDataObject, "percentageownership", percentageownership);


            AddFieldToBusinessDataObject(buisinessDataObject, "reference", reference);
            AddFieldToBusinessDataObject(buisinessDataObject, "showreferenceinschedule", showreferenceinschedule);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetPersonFinancialDetailByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.AddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonFinancialDetailByID(Guid PersonFinancialDetailId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonFinancialDetailId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonFinancialDetail(Guid PersonFinancialDetailId)
        {
            this.DeleteRecord(TableName, PersonFinancialDetailId);
        }
    }
}
