using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RTTPathwaySetup : BaseClass
    {

        public string TableName = "RTTPathwaySetup".ToLower();
        public string PrimaryKeyName = "RTTPathwaySetupId".ToLower();

        public RTTPathwaySetup()
        {
            AuthenticateUser();
        }

        public RTTPathwaySetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateRTTPathwaySetup(Guid OwnerId, string Name, DateTime StartDate, int firstappointmentnolaterthan, int warnafter, int breachoccursafter)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "firstappointmentnolaterthan", firstappointmentnolaterthan);
            AddFieldToBusinessDataObject(dataObject, "warnafter", warnafter);
            AddFieldToBusinessDataObject(dataObject, "breachoccursafter", breachoccursafter);

            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void UpdateBreachOccursAfter(Guid RTTPathwaySetupId, int breachoccursafter)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, RTTPathwaySetupId);
            AddFieldToBusinessDataObject(dataObject, "breachoccursafter", breachoccursafter);

            this.UpdateRecord(dataObject);
        }

        public void DeleteRTTPathwaySetupRecord(Guid RTTPathwaySetupId)
        {
            this.DeleteRecord(TableName, RTTPathwaySetupId);
        }
    }
}
