using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ProviderComplaintFeedback : BaseClass
    {

        public string TableName = "ProviderComplaintFeedback";
        public string PrimaryKeyName = "ProviderComplaintFeedbackId";


        public ProviderComplaintFeedback()
        {
            AuthenticateUser();
        }

        public ProviderComplaintFeedback(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }



        public Guid CreateProfessionalProviderComplaintFeedback(DateTime ComplaintFeedbackDate, Guid MadeById, string madebyidtablename
            , string madebyidname, Guid ProviderComplaintFeedBackTypeId, Guid? ProviderComplaintStageId, Guid? ProviderComplaintOutcomeId
            , string ComplaintFeedbackDetails, Guid ResponsibleUserId, Guid ProviderId, string FreeTextMadeBy, Guid? ProviderComplaintNatureId
            , DateTime? ResolutionDueDate, DateTime? OutcomeDate, string InvestigationDetails, Guid OwnerId
)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ComplaintFeedbackDate", ComplaintFeedbackDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "MadeById", MadeById);
            AddFieldToBusinessDataObject(buisinessDataObject, "madebyidtablename", madebyidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "madebyidname", madebyidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderComplaintFeedBackTypeId", ProviderComplaintFeedBackTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderComplaintStageId", ProviderComplaintStageId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderComplaintOutcomeId", ProviderComplaintOutcomeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ComplaintFeedbackDetails", ComplaintFeedbackDetails);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderId", ProviderId);
            AddFieldToBusinessDataObject(buisinessDataObject, "FreeTextMadeBy", FreeTextMadeBy);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProviderComplaintNatureId", ProviderComplaintNatureId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ResolutionDueDate", ResolutionDueDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "OutcomeDate", OutcomeDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "InvestigationDetails", InvestigationDetails);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);


            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByProviderId(Guid ProviderId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ProviderId", ConditionOperatorType.Equal, ProviderId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ProviderComplaintFeedbackId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ProviderComplaintFeedbackId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteProviderComplaintFeedback(Guid ProviderComplaintFeedbackId)
        {
            this.DeleteRecord(TableName, ProviderComplaintFeedbackId);
        }

    }
}
