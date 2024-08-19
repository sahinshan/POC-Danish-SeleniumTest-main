using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HealthAppointmentAttendeeAdvocateType : BaseClass
    {
        public string TableName { get { return "HealthAppointmentAttendeeAdvocateType"; } }
        public string PrimaryKeyName { get { return "HealthAppointmentAttendeeAdvocateTypeid"; } }


        public HealthAppointmentAttendeeAdvocateType()
        {
            AuthenticateUser();
        }

        public HealthAppointmentAttendeeAdvocateType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public Dictionary<string, object> GetByID(Guid HealthAppointmentAttendeeAdvocateTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentAttendeeAdvocateTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateHealthAppointmentAttendeeAdvocateType(Guid OwnerId, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);


            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public void DeleteHealthAppointmentAttendeeAdvocateType(Guid HealthAppointmentAttendeeAdvocateTypeid)
        {
            this.DeleteRecord(TableName, HealthAppointmentAttendeeAdvocateTypeid);
        }
    }
}
