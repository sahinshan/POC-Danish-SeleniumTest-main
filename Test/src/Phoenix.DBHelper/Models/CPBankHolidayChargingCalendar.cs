using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBankHolidayChargingCalendar : BaseClass
    {

        public string TableName = "CPBankHolidayChargingCalendar";
        public string PrimaryKeyName = "CPBankHolidayChargingCalendarId";

        public CPBankHolidayChargingCalendar()
        {
            AuthenticateUser();
        }

        public CPBankHolidayChargingCalendar(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public Guid CreateCPBankHolidayChargingCalendar(Guid ownerid, string Name, string code)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "code", code);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "validforexport", false);

            return CreateRecord(buisinessDataObject);
        }

        public void DeleteCPBankHolidayChargingCalendarRecord(Guid CPBankHolidayChargingCalendarId)
        {
            this.DeleteRecord(TableName, CPBankHolidayChargingCalendarId);
        }
    }
}
