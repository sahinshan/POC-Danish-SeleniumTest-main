using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBankHolidayDate : BaseClass
    {

        public string TableName = "CPBankHolidayDate";
        public string PrimaryKeyName = "CPBankHolidayDateId";

        public CPBankHolidayDate()
        {
            AuthenticateUser();
        }

        public CPBankHolidayDate(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCPBankHolidayChargingCalendarId(Guid CPBankHolidayChargingCalendarId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CPBankHolidayChargingCalendarId", ConditionOperatorType.Equal, CPBankHolidayChargingCalendarId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public List<Guid> GetByCPBankHolidayChargingCalendarId(Guid CPBankHolidayChargingCalendarId, Guid bankholidayid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CPBankHolidayChargingCalendarId", ConditionOperatorType.Equal, CPBankHolidayChargingCalendarId);
            this.BaseClassAddTableCondition(query, "bankholidayid", ConditionOperatorType.Equal, bankholidayid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateCPBankHolidayDate(Guid OwnerId, Guid CPBankHolidayChargingCalendarId, string Name, Guid BankHolidayId, Guid CareProviderBankHolidayTypeId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CPBankHolidayChargingCalendarId", CPBankHolidayChargingCalendarId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "BankHolidayId", BankHolidayId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderBankHolidayTypeId", CareProviderBankHolidayTypeId);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "UsedInFinance", false);
            return CreateRecord(buisinessDataObject);
        }

        public void DeleteCPBankHolidayDateRecord(Guid CPBankHolidayDateId)
        {
            this.DeleteRecord(TableName, CPBankHolidayDateId);
        }
    }
}
