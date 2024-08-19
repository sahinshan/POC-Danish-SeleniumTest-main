using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AppointmentRequiredAttendee : BaseClass
    {

        public string TableName = "AppointmentRequiredAttendee";
        public string PrimaryKeyName = "AppointmentRequiredAttendeeId";


        public AppointmentRequiredAttendee()
        {
            AuthenticateUser();
        }

        public Guid CreateAppointmentRequiredAttendee(Guid appointmentid, Guid regardingid, string regardingidtablename, string regardingidname)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "appointmentid", appointmentid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "regardingidname", regardingidname);

            return this.CreateRecord(buisinessDataObject);
        }

        public AppointmentRequiredAttendee(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public Dictionary<string, object> GetByID(Guid AppointmentRequiredAttendeeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AppointmentRequiredAttendeeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



        public void DeleteAppointmentRequiredAttendee(Guid AppointmentRequiredAttendeeId)
        {
            this.DeleteRecord(TableName, AppointmentRequiredAttendeeId);
        }
    }
}
