using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class SDEMap : BaseClass
    {
        public string TableName { get { return "SDEMap"; } }
        public string PrimaryKeyName { get { return "SDEMapid"; } }


        public SDEMap()
        {
            AuthenticateUser();
        }

        public SDEMap(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateSDEMap(Guid QuestionFromId, Guid QuestionToId, bool IsConditional, int SDEExecutionModeId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "QuestionFromId", QuestionFromId);
            AddFieldToBusinessDataObject(dataObject, "QuestionToId", QuestionToId);
            AddFieldToBusinessDataObject(dataObject, "IsConditional", IsConditional);
            AddFieldToBusinessDataObject(dataObject, "SDEExecutionModeId", SDEExecutionModeId);

            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetSDEMApsForDocumentQuestionIdentifier(Guid DocumentQuestionIdentifierID)
        {
            DataQuery query = new DataQuery(TableName, false);
            query.PrimaryKeyName = PrimaryKeyName;

            var datafilter = query.Filter.AddFilter(LogicalOperator.Or);

            datafilter.AddCondition(TableName, "QuestionFromId", ConditionOperatorType.Equal, DocumentQuestionIdentifierID);
            datafilter.AddCondition(TableName, "QuestionToId", ConditionOperatorType.Equal, DocumentQuestionIdentifierID);

            var response = ExecuteDataQuery(query);

            if (response.HasErrors) throw new Exception(response.Error);

            return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection[PrimaryKeyName]).ToList();
        }

        public Dictionary<string, object> GetSDEMapByID(Guid SDEMapId, params string[] fields)
        {
            var query = new DataQuery(TableName, true, fields);
            query.PrimaryKeyName = PrimaryKeyName;

            query.Filter.AddCondition(TableName, PrimaryKeyName, ConditionOperatorType.Equal, SDEMapId);

            var response = ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                (from fc in response.BusinessDataCollection[0].FieldCollection
                 select new
                 {
                     key = fc.Key,
                     value = fc.Value
                 })
                 .ToList()
                 .ForEach(c =>
                 {
                     dic.Add(c.key, c.value);
                 });

                return dic;
            }
            else
                return new Dictionary<string, object>();
        }

        public void DeleteSDEMap(Guid SDEMapID)
        {
            DeleteRecord(TableName, SDEMapID);

        }

    }
}
