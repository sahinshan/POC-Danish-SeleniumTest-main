using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientConsultantEpisodeEndReason : BaseClass
    {

        public string TableName = "InpatientConsultantEpisodeEndReason";
        public string PrimaryKeyName = "InpatientConsultantEpisodeEndReasonId";


        public InpatientConsultantEpisodeEndReason()
        {
            AuthenticateUser();
        }

        public InpatientConsultantEpisodeEndReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateInpatientConsultantEpisodeEndReason(Guid ownerid, string Name, DateTime startdate, int code, bool inactive = false, bool DefaultForConsultantChange = false, bool DefaultForDischarge = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultForConsultantChange", DefaultForConsultantChange);
            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultForDischarge", DefaultForDischarge);

            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnField(query, TableName, PrimaryKeyName);

            BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetInpatientConsultantEpisodeEndReasonByID(Guid InpatientConsultantEpisodeEndReasonId, params string[] FieldsToReturn)
        {
            var query = GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnFields(query, TableName, FieldsToReturn);

            BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientConsultantEpisodeEndReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
