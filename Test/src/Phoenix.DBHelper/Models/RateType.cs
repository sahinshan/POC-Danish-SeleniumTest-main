using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RateType : BaseClass
    {

        public string TableName = "RateType";
        public string PrimaryKeyName = "RateTypeId";

        public RateType()
        {
            AuthenticateUser();
        }

        public RateType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string RateTypeName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, RateTypeName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByCode(string code)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "code", ConditionOperatorType.Equal, code);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateRateType(Guid ownerid, Guid OwningBusinessUnitId, string name, Int32 code, DateTime startdate, int UnitDivisor, int MinimumAllowed, int MaximumAllowed)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "name", name);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);
            AddFieldToBusinessDataObject(buisinessDataObject, "UnitDivisor", UnitDivisor);
            AddFieldToBusinessDataObject(buisinessDataObject, "MinimumAllowed", MinimumAllowed);
            AddFieldToBusinessDataObject(buisinessDataObject, "MaximumAllowed", MaximumAllowed);
            AddFieldToBusinessDataObject(buisinessDataObject, "DecimalPlaces", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceDelivery", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartTime", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "UsedInFinance", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "BandedRates", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public void UpdateDecimalPlaces(Guid RateTypeId, bool DecimalPlaces)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, RateTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DecimalPlaces", DecimalPlaces);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateServiceDelivery(Guid RateTypeId, bool ServiceDelivery)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, RateTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ServiceDelivery", ServiceDelivery);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStartTime(Guid RateTypeId, bool StartTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, RateTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "StartTime", StartTime);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
