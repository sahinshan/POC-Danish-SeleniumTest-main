using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class FinancialAssessmentChargeDetail : BaseClass
    {
        public string TableName { get { return "FinancialAssessmentChargeDetail"; } }
        public string PrimaryKeyName { get { return "FinancialAssessmentChargeDetailid"; } }

        public FinancialAssessmentChargeDetail()
        {
            AuthenticateUser();
        }

        public FinancialAssessmentChargeDetail(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinancialAssessmentChargeDetail(Guid OwnerId, Guid FinancialAssessmentId, Guid FinancialAssessmentChargeId, Guid FrequencyOfReceiptId, decimal GrossValue, decimal RegardedPercent, decimal RegardedValue, int DetailTypeId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "FinancialAssessmentId", FinancialAssessmentId);
            AddFieldToBusinessDataObject(dataObject, "FinancialAssessmentChargeId", FinancialAssessmentChargeId);
            AddFieldToBusinessDataObject(dataObject, "FrequencyOfReceiptId", FrequencyOfReceiptId);
            AddFieldToBusinessDataObject(dataObject, "GrossValue", GrossValue);
            AddFieldToBusinessDataObject(dataObject, "RegardedPercent", RegardedPercent);
            AddFieldToBusinessDataObject(dataObject, "RegardedValue", RegardedValue);
            AddFieldToBusinessDataObject(dataObject, "DetailTypeId", DetailTypeId);
            AddFieldToBusinessDataObject(dataObject, "IsPersonalAllowance", false);
            AddFieldToBusinessDataObject(dataObject, "IsDisposableIncomeAllowance", false);
            AddFieldToBusinessDataObject(dataObject, "IsSavingCreditDisregarded", false);
            AddFieldToBusinessDataObject(dataObject, "IsDisabilityRelated", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return CreateRecord(dataObject);
        }



        public List<Guid> GetFinancialAssessmentChargeDetailByFinancialAssessmentChargeID(string FinancialAssessmentChargeId)
        {
            DataQuery query = this.GetDataQueryObject("FinancialAssessmentChargeDetail", false, "FinancialAssessmentChargeDetailId");

            BaseClassAddTableCondition(query, "FinancialAssessmentChargeId", ConditionOperatorType.Equal, FinancialAssessmentChargeId);

            AddReturnField(query, "FinancialAssessmentChargeDetail", "FinancialAssessmentChargeDetailid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialAssessmentChargeDetailid");
        }


        public List<Guid> GetFinancialAssessmentChargeDetailForFinancialAssessmentCharge(Guid FinancialAssessmentChargeID, int? DetailTypeId = null)
        {
            DataQuery query = new DataQuery("FinancialAssessmentChargeDetail", false);
            query.PrimaryKeyName = "FinancialAssessmentChargeDetailId";


            query.AddThisTableCondition("FinancialAssessmentChargeID", ConditionOperatorType.Equal, FinancialAssessmentChargeID);

            if (DetailTypeId.HasValue)
                query.AddThisTableCondition("DetailTypeId", ConditionOperatorType.Equal, DetailTypeId.Value);

            query.AddField("FinancialAssessmentChargeDetail", "FinancialAssessmentChargeDetailId", "FinancialAssessmentChargeDetailId");

            var response = ExecuteDataQuery(query);

            if (response.HasErrors) throw new Exception(response.Error);

            return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["FinancialAssessmentChargeDetailId"]).ToList();
        }

        public List<Guid> GetFinancialAssessmentChargeDetailForFinancialAssessmentCharge(Guid FinancialAssessmentChargeID, int? DetailTypeId = null, decimal? GrossValue = null, decimal? RegardedValue = null)
        {
            DataQuery query = new DataQuery("FinancialAssessmentChargeDetail", false);
            query.PrimaryKeyName = "FinancialAssessmentChargeDetailId";


            query.AddThisTableCondition("FinancialAssessmentChargeID", ConditionOperatorType.Equal, FinancialAssessmentChargeID);

            if (DetailTypeId.HasValue)
                query.AddThisTableCondition("DetailTypeId", ConditionOperatorType.Equal, DetailTypeId.Value);

            if (GrossValue.HasValue)
                query.AddThisTableCondition("GrossValue", ConditionOperatorType.Equal, GrossValue.Value);

            if (RegardedValue.HasValue)
                query.AddThisTableCondition("RegardedValue", ConditionOperatorType.Equal, RegardedValue.Value);

            query.AddField("FinancialAssessmentChargeDetail", "FinancialAssessmentChargeDetailId", "FinancialAssessmentChargeDetailId");

            var response = ExecuteDataQuery(query);

            if (response.HasErrors) throw new Exception(response.Error);

            return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["FinancialAssessmentChargeDetailId"]).ToList();
        }

        public Dictionary<string, object> GetFinancialAssessmentChargeDetailByID(Guid FinancialAssessmentChargeDetailId, params string[] fields)
        {
            var query = new DataQuery("FinancialAssessmentChargeDetail", true, fields);
            query.PrimaryKeyName = "FinancialAssessmentChargeDetailId";

            query.Filter.AddCondition("FinancialAssessmentChargeDetail", "FinancialAssessmentChargeDetailId", ConditionOperatorType.Equal, FinancialAssessmentChargeDetailId);

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

        public void DeleteFinancialAssessmentChargeDetail(Guid FinancialAssessmentChargeDetailID)
        {
            this.DeleteRecord("FinancialAssessmentChargeDetail", FinancialAssessmentChargeDetailID);
        }


    }
}
