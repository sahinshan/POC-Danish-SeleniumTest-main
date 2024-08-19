using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RecurringAppointmentOptionalAttendee : BaseClass
    {

        public string TableName = "RecurringAppointmentOptionalAttendee";
        public string PrimaryKeyName = "RecurringAppointmentOptionalAttendeeId";


        public RecurringAppointmentOptionalAttendee()
        {
            AuthenticateUser();
        }

        public RecurringAppointmentOptionalAttendee(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateRecurringAppointmentOptionalAttendee(Guid appointmentid, Guid regardingid, string regardingidtablename, string regardingidname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "appointmentid", appointmentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByRecurringAppointmentId(Guid RecurringAppointmentId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RecurringAppointmentId", ConditionOperatorType.Equal, RecurringAppointmentId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByRecurringAppointmentIdAndRegardingID(Guid RecurringAppointmentId, Guid regardingid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RecurringAppointmentId", ConditionOperatorType.Equal, RecurringAppointmentId);
            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, regardingid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid RecurringAppointmentOptionalAttendeeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, RecurringAppointmentOptionalAttendeeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteRecurringAppointmentOptionalAttendee(Guid RecurringAppointmentOptionalAttendeeId)
        {
            this.DeleteRecord(TableName, RecurringAppointmentOptionalAttendeeId);
        }
    }
}
