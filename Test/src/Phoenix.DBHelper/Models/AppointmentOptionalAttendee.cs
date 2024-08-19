using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AppointmentOptionalAttendee : BaseClass
    {

        public string TableName = "AppointmentOptionalAttendee";
        public string PrimaryKeyName = "AppointmentOptionalAttendeeId";


        public AppointmentOptionalAttendee()
        {
            AuthenticateUser();
        }

        public Guid CreateAppointmentOptionalAttendee(Guid appointmentid, Guid regardingid, string regardingidtablename, string regardingidname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "appointmentid", appointmentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);

            return this.CreateRecord(buisinessDataObject);
        }

        public AppointmentOptionalAttendee(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByAppointmentID(Guid appointmentid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "appointmentid", ConditionOperatorType.Equal, appointmentid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByAppointmentIDAndRegardingID(Guid appointmentid, Guid regardingid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "appointmentid", ConditionOperatorType.Equal, appointmentid);
            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, regardingid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid AppointmentOptionalAttendeeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AppointmentOptionalAttendeeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



        public void DeleteAppointmentOptionalAttendee(Guid AppointmentOptionalAttendeeId)
        {
            this.DeleteRecord(TableName, AppointmentOptionalAttendeeId);
        }
    }
}
