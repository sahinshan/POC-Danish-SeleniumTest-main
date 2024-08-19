using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RecurringAppointmentRequiredAttendee : BaseClass
    {

        public string TableName = "RecurringAppointmentRequiredAttendee";
        public string PrimaryKeyName = "RecurringAppointmentRequiredAttendeeId";


        public RecurringAppointmentRequiredAttendee()
        {
            AuthenticateUser();
        }

        public RecurringAppointmentRequiredAttendee(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateRecurringAppointmentRequiredAttendee(Guid RecurringAppointmentId, Guid regardingid, string regardingidtablename, string regardingidname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "RecurringAppointmentId", RecurringAppointmentId);
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

        public List<Guid> GetByRegardingID(Guid regardingid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, regardingid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid RecurringAppointmentRequiredAttendeeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, RecurringAppointmentRequiredAttendeeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteRecurringAppointmentRequiredAttendee(Guid RecurringAppointmentRequiredAttendeeId)
        {
            this.DeleteRecord(TableName, RecurringAppointmentRequiredAttendeeId);
        }
    }
}
