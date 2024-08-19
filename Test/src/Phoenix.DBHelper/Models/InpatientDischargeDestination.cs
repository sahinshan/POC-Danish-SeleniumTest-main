using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientDischargeDestination : BaseClass
    {

        public string TableName = "InpatientDischargeDestination";
        public string PrimaryKeyName = "InpatientDischargeDestinationId";


        public InpatientDischargeDestination()
        {
            AuthenticateUser();
        }

        public InpatientDischargeDestination(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateInpatientDischargeDestination(Guid ownerid, string Name, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetInpatientDischargeDestinationByID(Guid InpatientDischargeDestinationId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientDischargeDestinationId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteInpatientDischargeDestination(Guid InpatientDischargeDestinationId)
        {
            this.DeleteRecord(TableName, InpatientDischargeDestinationId);
        }
    }
}
