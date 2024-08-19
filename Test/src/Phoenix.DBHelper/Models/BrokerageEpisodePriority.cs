using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageEpisodePriority : BaseClass
    {

        private string TableName = "BrokerageEpisodePriority";
        private string PrimaryKeyName = "BrokerageEpisodePriorityId";

        public BrokerageEpisodePriority()
        {
            AuthenticateUser();
        }

        public BrokerageEpisodePriority(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageEpisodePriority(string Name, DateTime StartDate, Guid OwnerId, bool ValidForExport = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", ValidForExport);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateBrokerageEpisodePriority(Guid BrokerageEpisodePriorityId, string Name, DateTime StartDate, Guid OwnerId, bool ValidForExport = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, BrokerageEpisodePriorityId);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", ValidForExport);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByBrokerageEpisodePriorityId(Guid BrokerageEpisodePriorityId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodePriorityId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisodePriorityRecord(Guid BrokerageEpisodePriorityId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodePriorityId);
        }

    }
}
