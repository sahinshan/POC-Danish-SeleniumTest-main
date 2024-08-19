using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HealthAppointmentAttendingAdvocate : BaseClass
    {
        public string TableName { get { return "HealthAppointmentAttendingAdvocate"; } }
        public string PrimaryKeyName { get { return "HealthAppointmentAttendingAdvocateid"; } }


        public HealthAppointmentAttendingAdvocate()
        {
            AuthenticateUser();
        }

        public HealthAppointmentAttendingAdvocate(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public Dictionary<string, object> GetByID(Guid HealthAppointmentAttendingAdvocateId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentAttendingAdvocateId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByHealthAppointmentAttendeeAdvocateTypeId(Guid HealthAppointmentAttendeeAdvocateTypeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "HealthAppointmentAttendeeAdvocateTypeId", ConditionOperatorType.Equal, HealthAppointmentAttendeeAdvocateTypeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Guid CreateHealthAppointmentAttendingAdvocate(Guid OwnerId, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);


            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public void DeleteHealthAppointmentAttendingAdvocate(Guid HealthAppointmentAttendingAdvocateid)
        {
            this.DeleteRecord(TableName, HealthAppointmentAttendingAdvocateid);
        }
    }
}
