using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageEpisodePausePeriod : BaseClass
    {

        public string TableName = "BrokerageEpisodePausePeriod";
        public string PrimaryKeyName = "BrokerageEpisodePausePeriodId";


        public BrokerageEpisodePausePeriod()
        {
            AuthenticateUser();
        }

        public BrokerageEpisodePausePeriod(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }







        public List<Guid> GetByBrokerageEpisodeId(Guid BrokerageEpisodeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BrokerageEpisodeId", ConditionOperatorType.Equal, BrokerageEpisodeId);

            query.Orders.Add(new OrderBy("PauseDateTime", SortOrder.Ascending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetBrokerageEpisodePausePeriodByCaseID(Guid CaseId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid BrokerageEpisodePausePeriodId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodePausePeriodId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public void UpdateBrokerageEpisode(Guid BrokerageEpisodeId, bool inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BrokerageEpisodeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);

            this.UpdateRecord(buisinessDataObject);
        }



        public void DeleteBrokerageEpisodePausePeriod(Guid BrokerageEpisodePausePeriodId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodePausePeriodId);
        }
    }
}
