using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DuplicateDetectionRule : BaseClass
    {

        private string tableName = "DuplicateDetectionRule";
        private string primaryKeyName = "DuplicateDetectionRuleId";

        public DuplicateDetectionRule()
        {
            AuthenticateUser();
        }

        public DuplicateDetectionRule(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDuplicateDetectionRule(string Name, string Description, Guid RecordTypeId, bool Published, bool ExcludeInactiveMatchingRecords)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "RecordTypeId", RecordTypeId);
            AddFieldToBusinessDataObject(dataObject, "Published", Published);
            AddFieldToBusinessDataObject(dataObject, "ExcludeInactiveMatchingRecords", ExcludeInactiveMatchingRecords);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }



    }
}
