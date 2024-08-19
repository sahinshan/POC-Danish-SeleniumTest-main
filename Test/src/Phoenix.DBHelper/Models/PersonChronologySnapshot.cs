using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonChronologySnapshot : BaseClass
    {

        public string TableName = "PersonChronologySnapshot";
        public string PrimaryKeyName = "PersonChronologySnapshotId";


        public PersonChronologySnapshot()
        {
            AuthenticateUser();
        }

        public PersonChronologySnapshot(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetPersonChronologySnapshotByTitle(String Title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonChronologySnapshotByID(Guid PersonChronologySnapshotId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonChronologySnapshotId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }






        public void DeletePersonChronologySnapshot(Guid PersonChronologySnapshotId)
        {
            this.DeleteRecord(TableName, PersonChronologySnapshotId);
        }
    }
}
