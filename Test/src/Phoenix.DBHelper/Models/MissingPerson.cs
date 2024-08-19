using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MissingPerson : BaseClass
    {
        public string TableName { get { return "MissingPerson"; } }
        public string PrimaryKeyName { get { return "MissingPersonid"; } }


        public MissingPerson()
        {
            AuthenticateUser();
        }

        public MissingPerson(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonId(Guid Personid)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, Personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid MissingPersonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, MissingPersonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetMissingPersonByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid CreateMissingPerson(Guid ownerid, Guid OwningBusinessUnitId, DateTime missingdatetime, Guid personid, Guid personabsencetypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "missingdatetime", missingdatetime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personabsencetypeid", personabsencetypeid);

            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteMissingPerson(Guid MissingPersonid)
        {
            this.DeleteRecord(TableName, MissingPersonid);
        }
    }
}
