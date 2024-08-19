using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class HealthAppointmentReason : BaseClass
    {
        public string TableName { get { return "HealthAppointmentReason"; } }
        public string PrimaryKeyName { get { return "HealthAppointmentReasonid"; } }


        public HealthAppointmentReason()
        {
            AuthenticateUser();
        }

        public HealthAppointmentReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateHealthAppointmentReason(Guid HealthAppointmentReasonid, Guid OwnerId, string Name, DateTime StartDate, string Code, string GovCode)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, HealthAppointmentReasonid);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "GovCode", GovCode);

            AddFieldToBusinessDataObject(dataObject, "IsECT", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateHealthAppointmentReason(Guid OwnerId, string Name, DateTime StartDate, string Code, string GovCode)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "GovCode", GovCode);

            AddFieldToBusinessDataObject(dataObject, "IsECT", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid HealthAppointmentReasonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, HealthAppointmentReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteHealthAppointmentReason(Guid HealthAppointmentReasonid)
        {
            this.DeleteRecord(TableName, HealthAppointmentReasonid);
        }
    }
}
