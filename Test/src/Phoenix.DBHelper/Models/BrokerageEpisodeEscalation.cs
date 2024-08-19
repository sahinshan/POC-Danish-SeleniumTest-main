using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BrokerageEpisodeEscalation : BaseClass
    {

        public string TableName = "BrokerageEpisodeEscalation";
        public string PrimaryKeyName = "BrokerageEpisodeEscalationId";


        public BrokerageEpisodeEscalation()
        {
            AuthenticateUser();
        }

        public BrokerageEpisodeEscalation(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public Guid CreateBrokerageEpisodeEscalation(Guid ownerid, Guid caseid, Guid personid, Guid BrokerageEpisodeId,
            Guid EscalatedToId, string escalatedtoidtablename, string escalatedtoidname, DateTime EscalationDateTime, string EscalationDetails)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "BrokerageEpisodeId", BrokerageEpisodeId);
            AddFieldToBusinessDataObject(dataObject, "EscalatedToId", EscalatedToId);
            AddFieldToBusinessDataObject(dataObject, "escalatedtoidtablename", escalatedtoidtablename);
            AddFieldToBusinessDataObject(dataObject, "escalatedtoidname", escalatedtoidname);
            AddFieldToBusinessDataObject(dataObject, "EscalationDateTime", EscalationDateTime);
            AddFieldToBusinessDataObject(dataObject, "EscalationDetails", EscalationDetails);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByBrokerageEpisodeId(Guid BrokerageEpisodeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "BrokerageEpisodeId", ConditionOperatorType.Equal, BrokerageEpisodeId);

            query.Orders.Add(new OrderBy("EscalationDateTime", SortOrder.Ascending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid BrokerageEpisodeEscalationId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, BrokerageEpisodeEscalationId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteBrokerageEpisodeEscalation(Guid BrokerageEpisodeEscalationId)
        {
            this.DeleteRecord(TableName, BrokerageEpisodeEscalationId);
        }
    }
}
