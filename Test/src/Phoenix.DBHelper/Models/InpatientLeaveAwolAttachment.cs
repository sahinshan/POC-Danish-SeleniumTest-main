using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientLeaveAwolAttachment : BaseClass
    {

        public string TableName = "InpatientLeaveAwolAttachment";
        public string PrimaryKeyName = "InpatientLeaveAwolAttachmentId";


        public InpatientLeaveAwolAttachment()
        {
            AuthenticateUser();
        }

        public InpatientLeaveAwolAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }



        public Dictionary<string, object> GetInpatientLeaveAwolAttachmentByPersonID(Guid personid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, personid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteInpatientLeaveAwolAttachment(Guid InpatientLeaveAwolAttachmentId)
        {
            this.DeleteRecord(TableName, InpatientLeaveAwolAttachmentId);
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
