using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderService : BaseClass
    {

        public string TableName = "CareProviderService";
        public string PrimaryKeyName = "CareProviderServiceId";

        public CareProviderService()
        {
            AuthenticateUser();
        }

        public CareProviderService(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string CareProviderServiceName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, CareProviderServiceName);

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

        public Guid CreateCareProviderService(Guid ownerid, string Name, DateTime startdate, int code, DateTime? enddate = null, bool IsScheduledService = false, bool validforexport = false, bool inactive = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", validforexport);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsScheduledService", IsScheduledService);


            return CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderServiceId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderServiceId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAllCareProviderService()
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

        public int GetHighestCode()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.AddReturnField(query, TableName, "code");

            query.Orders.Add(new OrderBy("code", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractIntFields(query, "code").FirstOrDefault();
        }

        public void DeleteCareProviderService(Guid CareProviderServiceId)
        {
            this.DeleteRecord(TableName, CareProviderServiceId);
        }

        public List<Guid> GetAllSortedByName(bool Ascending, bool Inactive = false)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (Ascending)
            {
                query.Orders.Add(new OrderBy("Name", SortOrder.Ascending, TableName));
            }
            else
            {
                query.Orders.Add(new OrderBy("Name", SortOrder.Descending, TableName));
            }

            var filter1 = new DataFilter(LogicalOperator.Or);
            if (!Inactive)
            {
                this.BaseClassAddTableCondition(query, "inactive", ConditionOperatorType.Equal, Inactive);
                this.BaseClassAddTableCondition(query, "startdate", ConditionOperatorType.LessEqual, dateTime);

                filter1.AddCondition(TableName, "enddate", ConditionOperatorType.Null);
                filter1.AddCondition(TableName, "enddate", ConditionOperatorType.GreaterEqual, dateTime);
            }
            else
            {
                filter1.AddCondition(TableName, "inactive", ConditionOperatorType.Equal, Inactive);
                filter1.AddCondition(TableName, "enddate", ConditionOperatorType.LessThan, dateTime);
                filter1.AddCondition(TableName, "startdate", ConditionOperatorType.GreaterThan, dateTime);
            }

            query.Filter.Filters.Add(filter1);

            query.RecordsPerPage = 10;
            query.PageNumber = 1;

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
    }
}
