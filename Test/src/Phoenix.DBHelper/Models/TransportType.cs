using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class TransportType : BaseClass
    {

        private string tableName = "TransportType";
        private string primaryKeyName = "TransportTypeId";

        public TransportType()
        {
            AuthenticateUser();
        }

        public TransportType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetTransportTypeByName(string Name)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Contains, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }
        public List<Guid> GetTransportTypeByLegacyId(string LegacyId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "LegacyId", ConditionOperatorType.Equal, LegacyId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetTransportTypeByID(Guid TransportTypeId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, TransportTypeId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteTransportType(Guid TransportTypeID)
        {
            this.DeleteRecord(tableName, TransportTypeID);
        }

        public Guid CreateTransportType(Guid OwnerId, string Name, DateTime StartDate, int traveltimecalculationid, string speed, int transporttypeiconid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "Inactive", 0);
            AddFieldToBusinessDataObject(dataObject, "traveltimecalculationid", traveltimecalculationid);
            AddFieldToBusinessDataObject(dataObject, "speed", speed);
            AddFieldToBusinessDataObject(dataObject, "transporttypeiconid", transporttypeiconid);

            return this.CreateRecord(dataObject);
        }



    }
}
