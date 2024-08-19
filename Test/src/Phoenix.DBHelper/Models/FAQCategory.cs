using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class FAQCategory : BaseClass
    {
        public string TableName { get { return "FAQCategory"; } }
        public string PrimaryKeyName { get { return "FAQCategoryid"; } }


        public FAQCategory()
        {
            AuthenticateUser();
        }

        public FAQCategory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetFAQCategoryByID(Guid FAQCategoryId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("FAQCategory", false, "FAQCategoryId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "FAQCategoryId", ConditionOperatorType.Equal, FAQCategoryId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
