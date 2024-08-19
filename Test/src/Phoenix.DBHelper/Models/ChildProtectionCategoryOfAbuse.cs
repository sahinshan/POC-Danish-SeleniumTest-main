using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ChildProtectionCategoryOfAbuse : BaseClass
    {

        private string tableName = "ChildProtectionCategoryOfAbuse";
        private string primaryKeyName = "ChildProtectionCategoryOfAbuseId";

        public ChildProtectionCategoryOfAbuse()
        {
            AuthenticateUser();
        }

        public ChildProtectionCategoryOfAbuse(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateChildProtectionCategoryOfAbuse(Guid ownerid, string name, DateTime startdate, string Code, string GovCode)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "GovCode", GovCode);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateChildProtectionCategoryOfAbuse(Guid ownerid, string name, DateTime startdate)
        {
            return CreateChildProtectionCategoryOfAbuse(ownerid, name, startdate, "", "");
        }

        public List<Guid> GetChildProtectionCategoryOfAbuseIdByChildProtectionID(Guid ChildProtectionCategoryOfAbuseId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ChildProtectionCategoryOfAbuseId", ConditionOperatorType.Equal, ChildProtectionCategoryOfAbuseId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetChildProtectionCategoryOfAbuseIdByID(Guid ChildProtectionId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ChildProtectionId", ConditionOperatorType.Equal, ChildProtectionId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteChildProtection(Guid ChildProtectionID)
        {
            this.DeleteRecord(tableName, ChildProtectionID);
        }



    }
}
