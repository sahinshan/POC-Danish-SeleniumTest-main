using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HealthAppointmentOutcomeType : BaseClass
    {
        public string TableName { get { return "HealthAppointmentOutcomeType"; } }
        public string PrimaryKeyName { get { return "HealthAppointmentOutcomeTypeid"; } }


        public HealthAppointmentOutcomeType()
        {
            AuthenticateUser();
        }

        public HealthAppointmentOutcomeType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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

        public Dictionary<string, object> GetByID(Guid HealthAppointmentOutcomeTypeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentOutcomeTypeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateHealthAppointmentOutcomeType(Guid OwnerId, string Name, DateTime StartDate, string LegacyId = "", string Code = "", Guid? RTTTreatmentStatusId = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "LegacyId", LegacyId);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "RTTTreatmentStatusId", RTTTreatmentStatusId);

            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public void DeleteHealthAppointmentOutcomeType(Guid HealthAppointmentOutcomeTypeid)
        {
            this.DeleteRecord(TableName, HealthAppointmentOutcomeTypeid);
        }

        public void UpdateRTTTreatmentStatusId(Guid HealthAppointmentOutcomeTypeid, Guid RTTTreatmentStatusId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, HealthAppointmentOutcomeTypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "RTTTreatmentStatusId", RTTTreatmentStatusId);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
