using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HealthAppointmentContactType : BaseClass
    {
        public string TableName { get { return "HealthAppointmentContactType"; } }
        public string PrimaryKeyName { get { return "HealthAppointmentContactTypeid"; } }


        public HealthAppointmentContactType()
        {
            AuthenticateUser();
        }

        public HealthAppointmentContactType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public Dictionary<string, object> GetByID(Guid HealthAppointmentContactTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentContactTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateHealthAppointmentContactType(Guid OwnerId, string Name, DateTime StartDate, string contacttypeid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "contacttypeid", contacttypeid);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }


        public void DeleteHealthAppointmentContactType(Guid HealthAppointmentContactTypeid)
        {
            this.DeleteRecord(TableName, HealthAppointmentContactTypeid);
        }
    }
}
