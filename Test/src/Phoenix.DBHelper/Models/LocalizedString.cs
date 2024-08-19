using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class LocalizedString : BaseClass
    {

        private string tableName = "LocalizedString";
        private string primaryKeyName = "LocalizedStringId";

        public LocalizedString()
        {
            AuthenticateUser();
        }

        public LocalizedString(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateLocalizedString(string SimpleText, Guid ObjectId, string ObjectTableName, string ObjectFieldName)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "SimpleText", SimpleText);
            AddFieldToBusinessDataObject(dataObject, "ObjectId", ObjectId);
            AddFieldToBusinessDataObject(dataObject, "ObjectTableName", ObjectTableName);
            AddFieldToBusinessDataObject(dataObject, "ObjectFieldName", ObjectFieldName);

            return this.CreateRecord(dataObject);
        }

        public void UpdateLocalizedString(Guid LocalizedStringId, string SimpleText)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, LocalizedStringId);

            AddFieldToBusinessDataObject(buisinessDataObject, "SimpleText", SimpleText);


            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByObjectIdID(Guid ObjectId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ObjectId", ConditionOperatorType.Equal, ObjectId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid LocalizedStringId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, LocalizedStringId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteLocalizedString(Guid LocalizedStringID)
        {
            this.DeleteRecord(tableName, LocalizedStringID);
        }



    }
}
