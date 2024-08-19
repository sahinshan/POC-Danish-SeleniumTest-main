using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class FinancialAssessmentChargeTotal : BaseClass
    {
        public string TableName { get { return "FinancialAssessmentChargeTotal"; } }
        public string PrimaryKeyName { get { return "FinancialAssessmentChargeTotalid"; } }

        public FinancialAssessmentChargeTotal()
        {
            AuthenticateUser();
        }

        public FinancialAssessmentChargeTotal(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateFinancialAssessmentChargeTotal(Guid OwnerId, Guid FinancialAssessmentId, Guid FinancialAssessmentChargeId, int TotalTypeId, decimal TotalAmount)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "FinancialAssessmentId", FinancialAssessmentId);
            AddFieldToBusinessDataObject(dataObject, "FinancialAssessmentChargeId", FinancialAssessmentChargeId);
            AddFieldToBusinessDataObject(dataObject, "TotalTypeId", TotalTypeId);
            AddFieldToBusinessDataObject(dataObject, "TotalAmount", TotalAmount);
            AddFieldToBusinessDataObject(dataObject, "IsFullCost", false);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);

            return CreateRecord(dataObject);
        }



        public List<Guid> GetFinancialAssessmentChargeTotalByFinancialAssessmentChargeID(string FinancialAssessmentChargeId)
        {
            DataQuery query = this.GetDataQueryObject("FinancialAssessmentChargeTotal", false, "FinancialAssessmentChargeTotalId");

            BaseClassAddTableCondition(query, "FinancialAssessmentChargeId", ConditionOperatorType.Equal, FinancialAssessmentChargeId);

            AddReturnField(query, "FinancialAssessmentChargeTotal", "FinancialAssessmentChargeTotalid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "FinancialAssessmentChargeTotalid");
        }

        public List<Guid> GetFinancialAssessmentChargeTotalForFinancialAssessmentCharge(Guid FinancialAssessmentChargeID, int? TotalTypeId = null)
        {
            DataQuery query = new DataQuery("FinancialAssessmentChargeTotal", false);
            query.PrimaryKeyName = "FinancialAssessmentChargeTotalId";


            query.AddThisTableCondition("FinancialAssessmentChargeID", ConditionOperatorType.Equal, FinancialAssessmentChargeID);

            if (TotalTypeId.HasValue)
                query.AddThisTableCondition("TotalTypeId", ConditionOperatorType.Equal, TotalTypeId.Value);

            query.AddField("FinancialAssessmentChargeTotal", "FinancialAssessmentChargeTotalId", "FinancialAssessmentChargeTotalId");

            var response = ExecuteDataQuery(query);

            if (response.HasErrors) throw new Exception(response.Error);

            return response.BusinessDataCollection.Select(c => (Guid)c.FieldCollection["FinancialAssessmentChargeTotalId"]).ToList();
        }

        public Dictionary<string, object> GetFinancialAssessmentChargeTotalByID(Guid FinancialAssessmentChargeTotalId, params string[] fields)
        {
            var query = new DataQuery("FinancialAssessmentChargeTotal", true, fields);
            query.PrimaryKeyName = "FinancialAssessmentChargeTotalId";

            query.Filter.AddCondition("FinancialAssessmentChargeTotal", "FinancialAssessmentChargeTotalId", ConditionOperatorType.Equal, FinancialAssessmentChargeTotalId);

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


        public void DeleteFinancialAssessmentChargeTotal(Guid FinancialAssessmentChargeTotalID)
        {
            this.DeleteRecord("FinancialAssessmentChargeTotal", FinancialAssessmentChargeTotalID);
        }



    }
}
