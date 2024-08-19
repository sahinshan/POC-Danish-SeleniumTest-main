using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ScheduleBookingToPeople : BaseClass
    {

        public string TableName = "ScheduleBookingToPeople";
        public string PrimaryKeyName = "ScheduleBookingToPeopleId";


        public ScheduleBookingToPeople()
        {
            AuthenticateUser();
        }

        public ScheduleBookingToPeople(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateScheduleBookingToPeople(Guid OwnerId, Guid ScheduleId, Guid PersonId, Guid CareProviderPersonContractId, Guid ContractServiceId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "ScheduleId", ScheduleId);
            AddFieldToBusinessDataObject(dataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(dataObject, "CareProviderPersonContractId", CareProviderPersonContractId);
            AddFieldToBusinessDataObject(dataObject, "ContractServiceId", ContractServiceId);

            AddFieldToBusinessDataObject(dataObject, "OverrideFinanceCode", false);
            AddFieldToBusinessDataObject(dataObject, "OverrideBookingCharge", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetBySystemUserId(Guid SystemUserId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", ConditionOperatorType.Equal, SystemUserId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetById(Guid ScheduleBookingToPeopleId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ScheduleBookingToPeopleId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteScheduleBookingToPeople(Guid ScheduleBookingToPeopleId)
        {
            this.DeleteRecord(TableName, ScheduleBookingToPeopleId);
        }

    }
}
