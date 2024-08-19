using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceElement1ValidRateUnits : BaseClass
    {
        public string TableName { get { return "ServiceElement1ValidRateUnits"; } }
        public string PrimaryKeyName { get { return "ServiceElement1ValidRateUnitsid"; } }

        public ServiceElement1ValidRateUnits()
        {
            AuthenticateUser();
        }

        public ServiceElement1ValidRateUnits(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceElement1ValidRateUnits(Guid ServiceElement1Id, Guid RateUnitId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceElement1Id", ServiceElement1Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "RateUnitId", RateUnitId);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByServiceElement1Id(Guid ServiceElement1Id)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public List<Guid> GetByServiceElement1AndRateUnit(Guid ServiceElement1Id, Guid RateUnitId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ServiceElement1Id", ConditionOperatorType.Equal, ServiceElement1Id);
            this.BaseClassAddTableCondition(query, "RateUnitId", ConditionOperatorType.Equal, RateUnitId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServiceElement1ValidRateUnitsid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceElement1ValidRateUnitsid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteServiceElement1ValidRateUnits(Guid ServiceElement1ValidRateUnitsID)
        {
            this.DeleteRecord(TableName, ServiceElement1ValidRateUnitsID);
        }

    }
}
