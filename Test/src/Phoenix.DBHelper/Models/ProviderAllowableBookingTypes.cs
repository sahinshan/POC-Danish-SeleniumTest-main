using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ProviderAllowableBookingTypes : BaseClass
    {

        private string tableName = "ProviderAllowableBookingTypes";
        private string primaryKeyName = "ProviderAllowableBookingTypesId";

        public ProviderAllowableBookingTypes()
        {
            AuthenticateUser();
        }

        public ProviderAllowableBookingTypes(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByProviderId(Guid ProviderId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "ProviderId", ConditionOperatorType.Equal, ProviderId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByBookingTypeAndProviderId(Guid BookingTypeId, Guid providerid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "BookingTypeId", ConditionOperatorType.Equal, BookingTypeId);
            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid ProviderAllowableBookingTypesId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, ProviderAllowableBookingTypesId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateProviderAllowableBookingTypes(Guid OwnerId, Guid ProviderId, Guid BookingTypeId, bool DefaultBookingType)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderId", ProviderId);
            AddFieldToBusinessDataObject(buisinessDataObject, "BookingTypeId", BookingTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "DefaultBookingType", DefaultBookingType);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);

            return this.CreateRecord(buisinessDataObject);
        }

    }
}
