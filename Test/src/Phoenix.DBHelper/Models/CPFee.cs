using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPFee : BaseClass
    {
        public string TableName = "CPFee";
        public string PrimaryKeyName = "CPFeeId";

        public CPFee()
        {
            AuthenticateUser();
        }

        public CPFee(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCPFee(
            DateTime startdate,
            Guid carerid, Guid cpfeetypeid, Guid serviceelement1id,
            int finratetypeid, decimal rate, int ruletypeid, bool topay, Guid carerrateunitid, int units,
            int statusid, Guid ownerid
            )
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            //Date Range
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            //Fee Allocation Detail
            this.AddFieldToBusinessDataObject(buisinessDataObject, "carerid", carerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "cpfeetypeid", cpfeetypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "serviceelement1id", serviceelement1id);

            //Payment Information
            this.AddFieldToBusinessDataObject(buisinessDataObject, "finratetypeid", finratetypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "rate", rate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ruletypeid", ruletypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "topay", topay);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "carerrateunitid", carerrateunitid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "units", units);

            //Status Detail
            this.AddFieldToBusinessDataObject(buisinessDataObject, "statusid", statusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);



            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);


            return this.CreateRecord(buisinessDataObject);

        }

        public void UpdateStatus(Guid cpfeetypeid, int StatusID)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, cpfeetypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StatusID", StatusID);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByFeeType(Guid cpfeetypeid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "cpfeetypeid", ConditionOperatorType.Equal, cpfeetypeid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CPFeeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPFeeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCPFee(Guid CPFeeId)
        {
            this.DeleteRecord(TableName, CPFeeId);
        }

    }
}
