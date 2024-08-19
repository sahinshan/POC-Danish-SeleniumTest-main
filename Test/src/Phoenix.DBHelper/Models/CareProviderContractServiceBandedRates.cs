using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderContractServiceBandedRates : BaseClass
    {

        public string TableName = "CareProviderContractServiceBandedRates";
        public string PrimaryKeyName = "CareProviderContractServiceBandedRatesId";


        public CareProviderContractServiceBandedRates()
        {
            AuthenticateUser();
        }

        public CareProviderContractServiceBandedRates(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateCareProviderContractServiceBandedRate(Guid careprovidercontractservicerateperiodid, Guid careprovidercontractserviceid,
            TimeSpan timefrom, TimeSpan timeto, decimal bandrate, Guid ownerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractservicerateperiodid", careprovidercontractservicerateperiodid);
            AddFieldToBusinessDataObject(buisinessDataObject, "careprovidercontractserviceid", careprovidercontractserviceid);
            AddFieldToBusinessDataObject(buisinessDataObject, "timefrom", timefrom);
            AddFieldToBusinessDataObject(buisinessDataObject, "timeto", timeto);
            AddFieldToBusinessDataObject(buisinessDataObject, "bandrate", bandrate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public void UpdateBandRate(Guid CareProviderContractServiceBandedRatesId, decimal bandrate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderContractServiceBandedRatesId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "bandrate", bandrate);

            this.UpdateRecord(buisinessDataObject);
        }
        public List<Guid> GetByContractServiceRatePeriodId(Guid careprovidercontractservicerateperiodid)
        {
            var query = this.GetDataQueryObject(TableName, true, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careprovidercontractservicerateperiodid", ConditionOperatorType.Equal, careprovidercontractservicerateperiodid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByContractServiceRatePeriodId(Guid careprovidercontractservicerateperiodid, string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "careprovidercontractservicerateperiodid", ConditionOperatorType.Equal, careprovidercontractservicerateperiodid);
            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderContractServiceBandedRatesId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderContractServiceBandedRatesId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCareProviderContractServiceBandedRates(Guid CareProviderContractServiceBandedRatesId)
        {
            this.DeleteRecord(TableName, CareProviderContractServiceBandedRatesId);
        }

    }
}
