using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ChildProtectionEndReasonType : BaseClass
    {

        private string tableName = "ChildProtectionEndReasonType";
        private string primaryKeyName = "ChildProtectionEndReasonTypeId";

        public ChildProtectionEndReasonType()
        {
            AuthenticateUser();
        }

        public ChildProtectionEndReasonType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateChildProtectionEndReasonType(Guid ownerid, string name, DateTime startdate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }
        public List<Guid> GetChildProtectionEndReasonTypeByTypeID(Guid ChildProtectionEndReasonTypeId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ChildProtectionEndReasonTypeId", ConditionOperatorType.Equal, ChildProtectionEndReasonTypeId);

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

        public Dictionary<string, object> GetChildProtectionEndReasonTypeByID(Guid ChildProtectionEndReasonTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ChildProtectionStatusTypeId", ConditionOperatorType.Equal, ChildProtectionEndReasonTypeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteChildProtectionEndReasonTypeID(Guid ChildProtectionEndReasonTypeId)
        {
            this.DeleteRecord(tableName, ChildProtectionEndReasonTypeId);
        }



    }
}
