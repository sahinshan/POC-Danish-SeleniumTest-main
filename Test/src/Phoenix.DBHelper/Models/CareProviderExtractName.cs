using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderExtractName : BaseClass
    {

        public string TableName = "CareProviderExtractName";
        public string PrimaryKeyName = "CareProviderExtractNameId";

        public CareProviderExtractName()
        {
            AuthenticateUser();
        }

        public CareProviderExtractName(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderExtractName(Guid OwnerId, string Name, int? Code, string GovCode = null, DateTime? StartDate = null, DateTime? EndDate = null, bool Inactive = false, bool ValidForExport = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "GovCode", GovCode);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "EndDate", EndDate);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", ValidForExport);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByCode(int code)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "code", ConditionOperatorType.Equal, code);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public int GetHighestCode()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.AddReturnField(query, TableName, "code");

            query.Orders.Add(new OrderBy("code", SortOrder.Descending, TableName));

            query.PageNumber = 1;
            query.RecordsPerPage = 1;

            return ExecuteDataQueryAndExtractIntFields(query, "code").FirstOrDefault();
        }

        public Dictionary<string, object> GetByID(Guid CareProviderExtractNameId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderExtractNameId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAllCareProviderExtractName()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetCodeNumber(string code = "")
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "code", ConditionOperatorType.NotEqual, code);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteCareProviderExtractName(Guid CareProviderExtractNameId)
        {
            this.DeleteRecord(TableName, CareProviderExtractNameId);
        }


    }
}
