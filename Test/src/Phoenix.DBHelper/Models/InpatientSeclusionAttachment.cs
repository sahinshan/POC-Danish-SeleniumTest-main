using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientSeclusionAttachment : BaseClass
    {

        public string TableName = "InpatientSeclusionAttachment";
        public string PrimaryKeyName = "InpatientSeclusionAttachmentId";


        public InpatientSeclusionAttachment()
        {
            AuthenticateUser();
        }

        public InpatientSeclusionAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }



        public Dictionary<string, object> GetInpatientSeclusionAttachmentByPersonID(Guid personid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteInpatientSeclusionAttachment(Guid InpatientSeclusionAttachmentId)
        {
            this.DeleteRecord(TableName, InpatientSeclusionAttachmentId);
        }

        public List<Guid> GetByPersonId(Guid personId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "personId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, personId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


    }
}
