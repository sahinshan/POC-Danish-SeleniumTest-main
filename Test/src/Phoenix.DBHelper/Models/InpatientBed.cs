using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientBed : BaseClass
    {

        public string TableName = "InpatientBed";
        public string PrimaryKeyName = "InpatientBedId";


        public InpatientBed()
        {
            AuthenticateUser();
        }

        public InpatientBed(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetInpatientBedById(Guid Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetInpatientBedByInpatientBayId(Guid InpatientBayId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "InpatientBayId", ConditionOperatorType.Equal, InpatientBayId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetInpatientBedByBednumber(string Bednumber)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Bednumber", ConditionOperatorType.Equal, Bednumber);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetInpatientBedByID(Guid InpatientBedId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientBedId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public Guid CreateInpatientBed(Guid ownerid, string serialnumber, string rowpositionid, string position, Guid inpatientbayid, int statusid, Guid inpatientbedtypeid, string bednumber)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "rowpositionid", rowpositionid);
            AddFieldToBusinessDataObject(dataObject, "position", position);
            AddFieldToBusinessDataObject(dataObject, "serialnumber", serialnumber);
            AddFieldToBusinessDataObject(dataObject, "inpatientbayid", inpatientbayid);
            AddFieldToBusinessDataObject(dataObject, "statusid", statusid);
            AddFieldToBusinessDataObject(dataObject, "inpatientbedtypeid", inpatientbedtypeid);
            AddFieldToBusinessDataObject(dataObject, "bednumber", bednumber);


            return this.CreateRecord(dataObject);
        }

        public void DeleteInpatientBed(Guid InpatientBedId)
        {
            this.DeleteRecord(TableName, InpatientBedId);
        }
    }
}
