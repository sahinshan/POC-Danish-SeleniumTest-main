using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonHealthDetail : BaseClass
    {

        public string TableName = "PersonHealthDetail";
        public string PrimaryKeyName = "PersonHealthDetailId";


        public PersonHealthDetail()
        {
            AuthenticateUser();
        }

        public PersonHealthDetail(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetPersonHealthDetailIdByPersonID(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonHealthDetailByID(Guid PersonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonHealthDetail(Guid PersonHealthDetailId)
        {
            this.DeleteRecord(TableName, PersonHealthDetailId);
        }

        //Created
        public List<Guid> GetHealthDetailIdByHealthIssueType(Guid PersonHealthDetailId, Guid HealthIssueTypeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonHealthDetailId", ConditionOperatorType.Equal, PersonHealthDetailId);
            this.BaseClassAddTableCondition(query, "HealthIssueTypeID", ConditionOperatorType.Equal, HealthIssueTypeId);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);


        }

        public Guid CreatePersonHealthDetailRecord(Guid PersonID, Guid ownerid, Guid healthissuetypeid, int diagnosedid, DateTime StartDate, DateTime diagnoseddate, String notes)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonID", PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "healthissuetypeid", healthissuetypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "diagnosedid", diagnosedid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "diagnoseddate", diagnoseddate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);
            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetByHealthIssueTypeId(Guid healthissuetypeid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "healthissuetypeid", ConditionOperatorType.Equal, healthissuetypeid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



    }
}
