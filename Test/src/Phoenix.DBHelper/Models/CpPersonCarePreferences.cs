using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CpPersonCarePreferences : BaseClass
    {

        public string TableName = "CpPersonCarePreferences";
        public string PrimaryKeyName = "CpPersonCarePreferencesId";


        public CpPersonCarePreferences()
        {
            AuthenticateUser();
        }

        public CpPersonCarePreferences(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCpPersonCarePreferences(Guid PersonId, Guid OwnerId, int DailyCareRecordId, string CarePreferences, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "personid", PersonId);
            AddFieldToBusinessDataObject(dataObject, "ownerid", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "dailycarerecordid", DailyCareRecordId);
            AddFieldToBusinessDataObject(dataObject, "carepreferences", CarePreferences);
            AddFieldToBusinessDataObject(dataObject, "inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

        public void UpdateCarePreferences(Guid CpPersonCarePreferencesId, string CarePreferences)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CpPersonCarePreferencesId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "carepreferences", CarePreferences);

            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByTitle(string Title)
        {

            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, Title);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetById(Guid CpPersonCarePreferencesId, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, fields);
            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CpPersonCarePreferencesId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
