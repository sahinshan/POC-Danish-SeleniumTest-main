using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonNameHistory : BaseClass
    {

        public string TableName = "PersonNameHistory";
        public string PrimaryKeyName = "PersonNameHistoryId";


        public PersonNameHistory()
        {
            AuthenticateUser();
        }

        public PersonNameHistory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetPersonNameHistoryIdByPersonID(Guid PersonId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonNameHistoryByID(Guid PersonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonNameHistory(Guid PersonNameHistoryId)
        {
            this.DeleteRecord(TableName, PersonNameHistoryId);
        }



        public Guid CreatePersonNameHistoryRecord(Guid PersonID, Guid ownerid, Guid healthissuetypeid, int diagnosedid, DateTime StartDate, DateTime diagnoseddate, String notes)
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




    }
}
