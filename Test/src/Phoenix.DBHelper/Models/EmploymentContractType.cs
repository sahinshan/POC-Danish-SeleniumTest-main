using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class EmploymentContractType : BaseClass
    {

        public string TableName = "EmploymentContractType";
        public string PrimaryKeyName = "EmploymentContractTypeId";


        public EmploymentContractType()
        {
            AuthenticateUser();
        }

        public EmploymentContractType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateEmploymentContractType(Guid ownerid, string name, string code, string govcode, DateTime StartDate)
        {
            return CreateEmploymentContractType(ownerid, name, code, govcode, StartDate, 2);
        }

        public Guid CreateEmploymentContractType(Guid ownerid, string name, string code, string govcode, DateTime StartDate, int employmentContractTypePayCategoryId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "name", name);
            AddFieldToBusinessDataObject(dataObject, "code", code);
            AddFieldToBusinessDataObject(dataObject, "govcode", govcode);
            AddFieldToBusinessDataObject(dataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "employmentContractTypePayCategoryId", employmentContractTypePayCategoryId);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetEmploymentContractTypeByID(Guid EmploymentContractTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, EmploymentContractTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteEmploymentContractType(Guid EmploymentContractTypeId)
        {
            this.DeleteRecord(TableName, EmploymentContractTypeId);
        }
    }
}
