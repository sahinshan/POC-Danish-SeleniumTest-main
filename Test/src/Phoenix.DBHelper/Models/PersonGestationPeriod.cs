using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonGestationPeriod : BaseClass
    {

        public string TableName = "PersonGestationPeriod";
        public string PrimaryKeyName = "PersonGestationPeriodId";


        public PersonGestationPeriod()
        {
            AuthenticateUser();
        }

        public PersonGestationPeriod(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetPersonGestationPeriodIdByPersonId(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonGestationPeriodByID(Guid PersonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonGestationPeriod(Guid PersonGestationPeriodId)
        {
            this.DeleteRecord(TableName, PersonGestationPeriodId);
        }

        //Created
        public List<Guid> GetHealthDetailIdByHealthIssueType(Guid PersonGestationPeriodId, Guid HealthIssueTypeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonGestationPeriodId", ConditionOperatorType.Equal, PersonGestationPeriodId);
            this.BaseClassAddTableCondition(query, "HealthIssueTypeID", ConditionOperatorType.Equal, HealthIssueTypeId);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);


        }

        public Guid CreatePersonGestationPeriodRecord(Guid PersonID, Guid ownerid, Guid? childid, int totaldaysorweeks, int gestationperiodtypeid, DateTime StartDate, Guid gestationendreasonid, String notes)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonID", PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "childid", childid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "totaldaysorweeks", totaldaysorweeks);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "gestationperiodtypeid", gestationperiodtypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "gestationendreasonid", gestationendreasonid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "notes", notes);

            return this.CreateRecord(buisinessDataObject);

        }

        public Guid CreatePersonGestationPeriodRecordEndDate(Guid PersonID, Guid ownerid, Guid? childid, int totaldaysorweeks, int gestationperiodtypeid, DateTime StartDate, DateTime? endDate, Guid gestationendreasonid, String notes)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonID", PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "childid", childid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "totaldaysorweeks", totaldaysorweeks);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "gestationperiodtypeid", gestationperiodtypeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "endDate", endDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "gestationendreasonid", gestationendreasonid);
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
