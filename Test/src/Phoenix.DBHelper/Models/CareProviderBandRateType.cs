using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderBandRateType : BaseClass
    {

        public string TableName = "CareProviderBandRateType";
        public string PrimaryKeyName = "CareProviderBandRateTypeId";

        public CareProviderBandRateType()
        {
            AuthenticateUser();
        }

        public CareProviderBandRateType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public List<Guid> GetHighestCode()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            query.Orders.Add(new OrderBy("code", SortOrder.Descending, TableName));
            query.RecordsPerPage = 1;
            query.PageNumber = 1;
            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateCareProviderBandRateType(Guid ownerid, string Name, string code, DateTime startdate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public void DeleteCareProviderBandRateTypeRecord(Guid CareProviderBandRateTypeId)
        {
            this.DeleteRecord(TableName, CareProviderBandRateTypeId);
        }
    }
}
