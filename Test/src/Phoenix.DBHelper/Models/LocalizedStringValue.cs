using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class LocalizedStringValue : BaseClass
    {

        private string tableName = "LocalizedStringValue";
        private string primaryKeyName = "LocalizedStringValueId";

        public LocalizedStringValue()
        {
            AuthenticateUser();
        }

        public LocalizedStringValue(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateLocalizedStringValue(Guid LocalizedStringId, Guid ProductLanguageId, string PlainText)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "LocalizedStringId", LocalizedStringId);
            AddFieldToBusinessDataObject(dataObject, "ProductLanguageId", ProductLanguageId);
            AddFieldToBusinessDataObject(dataObject, "PlainText", PlainText);

            return this.CreateRecord(dataObject);
        }

        public void UpdateLocalizedStringValue(Guid LocalizedStringValueId, string PlainText)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, LocalizedStringValueId);

            AddFieldToBusinessDataObject(buisinessDataObject, "PlainText", PlainText);


            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByLocalizedStringId(Guid LocalizedStringId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "LocalizedStringId", ConditionOperatorType.Equal, LocalizedStringId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByPlainText(string PlainText)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PlainText", ConditionOperatorType.Equal, PlainText);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid LocalizedStringValueId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, LocalizedStringValueId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteLocalizedStringValue(Guid LocalizedStringValueID)
        {
            this.DeleteRecord(tableName, LocalizedStringValueID);
        }



    }
}
