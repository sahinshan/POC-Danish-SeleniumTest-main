using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CarerApprovalDecision : BaseClass
    {
        public string TableName = "CarerApprovalDecision";
        public string PrimaryKeyName = "CarerApprovalDecisionId";

        public CarerApprovalDecision()
        {
            AuthenticateUser();
        }

        public CarerApprovalDecision(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCarerApprovalDecision(Guid providerid, Guid ownerid, DateTime decisiondate, Guid decisionid, int decisionbyid = 6)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "decisiondate", decisiondate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "decisionid", decisionid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "decisionbyid", decisionbyid);


            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetByProvider(Guid providerid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerid", ConditionOperatorType.Equal, providerid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCarerApprovalDecisionByID(Guid CarerApprovalDecisionId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CarerApprovalDecisionId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCarerApprovalDecision(Guid CarerApprovalDecisionId)
        {
            this.DeleteRecord(TableName, CarerApprovalDecisionId);
        }

    }
}
