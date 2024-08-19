using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderContractServiceRatePeriod : BaseClass
    {
        public string TableName { get { return "CareProviderContractServiceRatePeriod"; } }
        public string PrimaryKeyName { get { return "CareProviderContractServiceRatePeriodid"; } }

        public CareProviderContractServiceRatePeriod()
        {
            AuthenticateUser();
        }

        public CareProviderContractServiceRatePeriod(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCareProviderContractServiceRatePeriod(Guid careprovidercontractserviceid, DateTime startdate, Guid careproviderrateunitid, int rate, Guid OwnerId, Guid? cptimebandsetid = null, DateTime? enddate = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractserviceid", careprovidercontractserviceid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderrateunitid", careproviderrateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rate", rate);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            if (enddate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            if (cptimebandsetid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "cptimebandsetid", cptimebandsetid);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateCareProviderContractServiceRatePeriod(Guid careprovidercontractserviceid, DateTime startdate, Guid careproviderrateunitid, Guid cptimebandsetid,
            Guid OwnerId, DateTime? enddate, Guid? cpbankholidaychargingcalendarid, Guid careproviderbandratetypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractserviceid", careprovidercontractserviceid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderrateunitid", careproviderrateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "cptimebandsetid", cptimebandsetid);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            if (enddate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);
            if (cpbankholidaychargingcalendarid.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "cpbankholidaychargingcalendarid", cpbankholidaychargingcalendarid.Value);
            AddFieldToBusinessDataObject(buisinessDataObject, "careproviderbandratetypeid", careproviderbandratetypeid);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderContractServiceRatePeriodid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderContractServiceRatePeriodid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByContractServiceID(Guid careprovidercontractserviceid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            AddReturnField(query, TableName, PrimaryKeyName);

            BaseClassAddTableCondition(query, "careprovidercontractserviceid", ConditionOperatorType.Equal, careprovidercontractserviceid);
            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        //update cptimebandset field
        public void UpdateCPTimebandSet(Guid CareProviderContractServiceRatePeriodid, Guid cptimebandsetid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderContractServiceRatePeriodid", DataType.UniqueIdentifier, BusinessObjectFieldType.Unknown, false, CareProviderContractServiceRatePeriodid);
            AddFieldToBusinessDataObject(buisinessDataObject, "cptimebandsetid", cptimebandsetid);

            UpdateRecord(buisinessDataObject);
        }

    }
}
