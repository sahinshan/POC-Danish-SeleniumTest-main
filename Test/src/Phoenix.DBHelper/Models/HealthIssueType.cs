using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HealthIssueType : BaseClass
    {

        public string TableName = "HealthIssueType";
        public string PrimaryKeyName = "HealthIssueTypeId";


        public HealthIssueType()
        {
            AuthenticateUser();
        }

        public HealthIssueType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByHealthIssueTypeName(String HealthIsuueTypeName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, HealthIsuueTypeName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetHealthIssueTypeByID(Guid PersonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteHealthIssueType(Guid HealthIssueTypeId)
        {
            this.DeleteRecord(TableName, HealthIssueTypeId);
        }



        public Guid CreateHealthIssueTypeRecord(Guid OwnerId, String Name, int Inactive, int ValidForExport, DateTime StartDate, int? HealthIssueCategoryId = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", ValidForExport);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "HealthIssueCategoryId", HealthIssueCategoryId);

            return this.CreateRecord(buisinessDataObject);

        }




    }
}
