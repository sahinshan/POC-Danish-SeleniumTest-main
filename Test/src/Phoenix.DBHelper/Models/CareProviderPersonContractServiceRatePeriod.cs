using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonContractServiceRatePeriod : BaseClass
    {

        public string TableName = "CareProviderPersonContractServiceRatePeriod";
        public string PrimaryKeyName = "CareProviderPersonContractServiceRatePeriodId";

        public CareProviderPersonContractServiceRatePeriod()
        {
            AuthenticateUser();
        }

        public CareProviderPersonContractServiceRatePeriod(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string CareProviderPersonContractServiceEndReasonName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, CareProviderPersonContractServiceEndReasonName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByCareProviderPersonContractServiceId(Guid CareProviderPersonContractServiceId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderPersonContractServiceId", ConditionOperatorType.Equal, CareProviderPersonContractServiceId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateCareProviderPersonContractServiceRatePeriod(Guid ownerid, Guid CareProviderPersonContractServiceId, Guid CareProviderRateUnitId, DateTime startdate, int Rate, string NoteText, DateTime? enddate = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderPersonContractServiceId", CareProviderPersonContractServiceId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderRateUnitId", CareProviderRateUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);

            if (enddate.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate.Value);

            AddFieldToBusinessDataObject(buisinessDataObject, "Rate", Rate);
            AddFieldToBusinessDataObject(buisinessDataObject, "NoteText", NoteText);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);


            return CreateRecord(buisinessDataObject);
        }

        public void UpdateInactiveStatus(Guid CareProviderPersonContractServiceRatePeriodId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderPersonContractServiceRatePeriodId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByCareProviderPersonContractServiceIdAndRate(Guid CareProviderPersonContractServiceId, string Rate)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderPersonContractServiceId", ConditionOperatorType.Equal, CareProviderPersonContractServiceId);
            this.BaseClassAddTableCondition(query, "rate", ConditionOperatorType.Equal, Rate);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

    }
}
