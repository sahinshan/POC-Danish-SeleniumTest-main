using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageTargetSetup : BaseClass
    {

        private string TableName = "BrokerageTargetSetup";
        private string PrimaryKeyName = "BrokerageTargetSetupId";

        public BrokerageTargetSetup()
        {
            AuthenticateUser();
        }

        public BrokerageTargetSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateBrokerageEpisodeTargetSetup(Guid BrokerageRequestSourceId, Guid BrokerageEpisodePriorityId, int TargetTypeId, DateTime StartDate, Guid OwnerId, int BrokerageTarget, DateTime? EndDate = null, bool ValidForExport = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "BrokerageRequestSourceId", BrokerageRequestSourceId);
            AddFieldToBusinessDataObject(dataObject, "BrokerageEpisodePriorityId", BrokerageEpisodePriorityId);
            AddFieldToBusinessDataObject(dataObject, "TargetTypeId", TargetTypeId);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "BrokerageTarget", BrokerageTarget);
            AddFieldToBusinessDataObject(dataObject, "EndDate", EndDate);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", ValidForExport);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateBrokerageEpisodeTargetSetup(Guid BrokerageTargetSetupId, Guid BrokerageRequestSourceId, Guid BrokerageEpisodePriorityId, int TargetTypeId, DateTime StartDate, Guid OwnerId, int BrokerageTarget, DateTime? EndDate = null, bool ValidForExport = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, PrimaryKeyName, BrokerageTargetSetupId);

            AddFieldToBusinessDataObject(dataObject, "BrokerageRequestSourceId", BrokerageRequestSourceId);
            AddFieldToBusinessDataObject(dataObject, "BrokerageEpisodePriorityId", BrokerageEpisodePriorityId);
            AddFieldToBusinessDataObject(dataObject, "TargetTypeId", TargetTypeId);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "BrokerageTarget", BrokerageTarget);
            AddFieldToBusinessDataObject(dataObject, "EndDate", EndDate);
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

        public List<Guid> GetById(Guid BrokerageTargetSetupId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageTargetSetupId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByBrokerageTargetSetupId(Guid BrokerageTargetSetupId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageTargetSetupId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageTargetSetupRecord(Guid BrokerageTargetSetupId)
        {
            this.DeleteRecord(TableName, BrokerageTargetSetupId);
        }

    }
}
