using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ApplicantLanguage : BaseClass
    {

        public string TableName = "ApplicantLanguage";
        public string PrimaryKeyName = "ApplicantLanguageId";

        public ApplicantLanguage()
        {
            AuthenticateUser();
        }

        public ApplicantLanguage(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateApplicantLanguage(Guid OwnerId, Guid ApplicantId, Guid LanguageId, Guid? FluencyId, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(buisinessDataObject, "ApplicantId", ApplicantId);
            AddFieldToBusinessDataObject(buisinessDataObject, "LanguageId", LanguageId);
            AddFieldToBusinessDataObject(buisinessDataObject, "FluencyId", FluencyId);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);

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

        public Dictionary<string, object> GetApplicantLanguageByID(Guid ApplicantLanguageId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnFields(query, TableName, FieldsToReturn);

            BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ApplicantLanguageId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteApplicantLanguage(Guid ApplicantLanguageId)
        {
            DeleteRecord(TableName, ApplicantLanguageId);
        }
    }
}
