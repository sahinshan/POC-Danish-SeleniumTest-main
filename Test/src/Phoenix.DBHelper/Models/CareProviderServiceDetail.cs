using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderServiceDetail : BaseClass
    {

        public string TableName = "CareProviderServiceDetail";
        public string PrimaryKeyName = "CareProviderServiceDetailId";

        public CareProviderServiceDetail()
        {
            AuthenticateUser();
        }

        public CareProviderServiceDetail(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public List<Guid> GetByCode(int Code)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Code", ConditionOperatorType.Equal, Code);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public int GetHighestCode()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.AddReturnField(query, TableName, "code");

            query.Orders.Add(new OrderBy("code", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractIntFields(query, "code").FirstOrDefault();
        }

        public Guid CreateCareProviderServiceDetail(Guid ownerid, string Name, int? code, int? govcode, DateTime startdate, DateTime? enddate = null, bool inactive = false, bool validforexport = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "govcode", govcode);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", validforexport);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            return CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderServiceDetailId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderServiceDetailId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAllCareProviderServiceDetail()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("code", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteCareProviderServiceDetailRecord(Guid CareProviderServiceDetailId)
        {
            this.DeleteRecord(TableName, CareProviderServiceDetailId);
        }
    }
}
