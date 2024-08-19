using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderServiceMapping : BaseClass
    {

        public string TableName = "CareProviderServiceMapping";
        public string PrimaryKeyName = "CareProviderServiceMappingId";

        public CareProviderServiceMapping()
        {
            AuthenticateUser();
        }

        public CareProviderServiceMapping(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string CareProviderServiceMappingName)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, CareProviderServiceMappingName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByCareProviderServiceId(Guid ServiceId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "serviceid", ConditionOperatorType.Equal, ServiceId);

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

        public List<Guid> GetByServiceAndServiceDetailAndBookingType(Guid ServiceId, Guid? servicedetailid, Guid? BookingTypeId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ServiceId", ConditionOperatorType.Equal, ServiceId);

            if (servicedetailid.HasValue)
                this.BaseClassAddTableCondition(query, "servicedetailid", ConditionOperatorType.Equal, servicedetailid.Value);

            if (BookingTypeId.HasValue)
                this.BaseClassAddTableCondition(query, "bookingtypeid", ConditionOperatorType.Equal, BookingTypeId.Value);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByCareProviderServiceIdAndBookingTypeId(Guid ServiceId, Guid BookingTypeId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ServiceId", ConditionOperatorType.Equal, ServiceId);
            this.BaseClassAddTableCondition(query, "BookingTypeId", ConditionOperatorType.Equal, BookingTypeId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateCareProviderServiceMapping(Guid ServiceId, Guid ownerid, Guid? ServiceDetailId, Guid? BookingTypeId, Guid? FinanceCodeId, string NoteText, bool inactive = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "serviceid", ServiceId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "servicedetailid", ServiceDetailId);
            AddFieldToBusinessDataObject(buisinessDataObject, "bookingtypeid", BookingTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "financecodeid", FinanceCodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "notetext", NoteText);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            return CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid CareProviderServiceMappingId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderServiceMappingId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetAllCareProviderServiceMappingId()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateServiceIdField(Guid CareProviderServiceMappingId, Guid ServiceDetailId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderServiceMappingId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "servicedetailid", ServiceDetailId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateBookingTypeIdField(Guid CareProviderServiceMappingId, Guid BookingTypeId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CareProviderServiceMappingId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "bookingtypeid", BookingTypeId);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
