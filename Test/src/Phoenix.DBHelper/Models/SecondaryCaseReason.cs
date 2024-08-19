using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SecondaryCaseReason : BaseClass
    {

        public string TableName = "SecondaryCaseReason";
        public string PrimaryKeyName = "SecondaryCaseReasonId";


        public SecondaryCaseReason()
        {
            AuthenticateUser();
        }

        public SecondaryCaseReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetSecondaryCaseReasonById(Guid Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetSecondaryCaseReasonByInpatientBayId(Guid InpatientBayId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "InpatientBayId", ConditionOperatorType.Equal, InpatientBayId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetSecondaryCaseReasonByBednumber(string Bednumber)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Bednumber", ConditionOperatorType.Equal, Bednumber);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetSecondaryCaseReasonByID(Guid SecondaryCaseReasonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SecondaryCaseReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateSecondaryCaseReason(Guid ownerid, string name, DateTime startdate, int businesstypeid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "businesstypeid", businesstypeid);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public void DeleteSecondaryCaseReason(Guid SecondaryCaseReasonId)
        {
            this.DeleteRecord(TableName, SecondaryCaseReasonId);
        }
    }
}