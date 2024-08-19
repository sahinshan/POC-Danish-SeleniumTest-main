using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ApplicantAlias : BaseClass
    {

        public string TableName = "ApplicantAlias";
        public string PrimaryKeyName = "ApplicantAliasId";

        public ApplicantAlias()
        {
            AuthenticateUser();
        }

        public ApplicantAlias(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateApplicantAlias(Guid OwnerId, Guid ApplicantId, string FirstName, string LastName, bool PreferredName = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(buisinessDataObject, "ApplicantId", ApplicantId);
            AddFieldToBusinessDataObject(buisinessDataObject, "FirstName", FirstName);
            AddFieldToBusinessDataObject(buisinessDataObject, "LastName", LastName);
            AddFieldToBusinessDataObject(buisinessDataObject, "PreferredName", PreferredName);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByApplicantId(Guid ApplicantId)
        {
            var query = GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnField(query, TableName, PrimaryKeyName);

            BaseClassAddTableCondition(query, "ApplicantId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, ApplicantId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetApplicantAliasByID(Guid ApplicantAliasId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnFields(query, TableName, FieldsToReturn);

            BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ApplicantAliasId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteApplicantAlias(Guid ApplicantAliasId)
        {
            DeleteRecord(TableName, ApplicantAliasId);
        }
    }
}
