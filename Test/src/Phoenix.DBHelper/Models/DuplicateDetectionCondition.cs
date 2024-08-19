using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class DuplicateDetectionCondition : BaseClass
    {

        private string tableName = "DuplicateDetectionCondition";
        private string primaryKeyName = "DuplicateDetectionConditionId";

        public DuplicateDetectionCondition()
        {
            AuthenticateUser();
        }

        public DuplicateDetectionCondition(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateDuplicateDetectionCondition(string name, Guid duplicatedetectionruleid, int CriterionId, int? nofcharacters, bool IgnoreBlankValues, Guid BusinessObjectFieldId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "duplicatedetectionruleid", duplicatedetectionruleid);
            AddFieldToBusinessDataObject(dataObject, "CriterionId", CriterionId);
            AddFieldToBusinessDataObject(dataObject, "nofcharacters", nofcharacters);
            AddFieldToBusinessDataObject(dataObject, "IgnoreBlankValues", IgnoreBlankValues);
            AddFieldToBusinessDataObject(dataObject, "BusinessObjectFieldId", BusinessObjectFieldId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name, Guid duplicatedetectionruleid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);
            this.BaseClassAddTableCondition(query, "duplicatedetectionruleid", ConditionOperatorType.Equal, duplicatedetectionruleid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public void UpdateAdministrationInformation(Guid DuplicateDetectionConditionID, bool ignoreblankvalues)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, DuplicateDetectionConditionID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ignoreblankvalues", ignoreblankvalues);

            this.UpdateRecord(buisinessDataObject);
        }



    }
}
