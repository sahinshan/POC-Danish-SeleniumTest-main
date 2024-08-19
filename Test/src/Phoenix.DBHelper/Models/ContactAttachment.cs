using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ContactAttachment : BaseClass
    {
        public string TableName = "ContactAttachment";
        public string PrimaryKeyName = "ContactAttachmentId";


        public ContactAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Dictionary<string, object> GetByID(Guid ContactAttachmentId, params string[] FieldsToReturn)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, CareWorks.Foundation.Enums.ConditionOperatorType.Equal, ContactAttachmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void Delete(Guid ContactAttachmentId)
        {
            this.DeleteRecord(TableName, ContactAttachmentId);
        }


    }
}
