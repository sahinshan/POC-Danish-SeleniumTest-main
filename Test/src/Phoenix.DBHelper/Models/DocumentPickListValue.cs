using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DocumentPickListValue : BaseClass
    {
        public string TableName { get { return "DocumentPickListValue"; } }
        public string PrimaryKeyName { get { return "DocumentPickListValueid"; } }


        public DocumentPickListValue()
        {
            AuthenticateUser();
        }

        public DocumentPickListValue(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByTextAndDocumentPickListId(string Text, Guid DocumentPickListId)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Text", ConditionOperatorType.Equal, Text);
            this.BaseClassAddTableCondition(query, "DocumentPickListId", ConditionOperatorType.Equal, DocumentPickListId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
