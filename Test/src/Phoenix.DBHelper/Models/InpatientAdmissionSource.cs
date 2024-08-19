using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class InpatientConsultantEpisode : BaseClass
    {
        public string TableName { get { return "InpatientConsultantEpisode"; } }
        public string PrimaryKeyName { get { return "InpatientConsultantEpisodeid"; } }


        public InpatientConsultantEpisode()
        {
            AuthenticateUser();
        }

        public InpatientConsultantEpisode(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonId(Guid Personid)

        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, Personid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid InpatientConsultantEpisodeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, InpatientConsultantEpisodeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetBySystemUserAddressId(Guid SystemUserId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "SystemUserId", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, SystemUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteInpatientConsultantEpisode(Guid InpatientConsultantEpisodeid)
        {
            this.DeleteRecord(TableName, InpatientConsultantEpisodeid);
        }

        public Guid CreateInpatientConsultantEpisode(Guid ownerid, Guid CaseId, Guid PersonId, Guid ConsultantId, Guid? InpatientConsultantEpisodeEndReasonId, Guid RTTTreatmentStatusId, Guid? TransferProviderId, DateTime StartDateTime, DateTime? EndDateTime, string Description = "Events", bool Autoupdated = false, bool Inactive = false)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "CaseId", CaseId);
            AddFieldToBusinessDataObject(dataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(dataObject, "ConsultantId", ConsultantId);
            AddFieldToBusinessDataObject(dataObject, "InpatientConsultantEpisodeEndReasonId", InpatientConsultantEpisodeEndReasonId);
            AddFieldToBusinessDataObject(dataObject, "RTTTreatmentStatusId", RTTTreatmentStatusId);
            AddFieldToBusinessDataObject(dataObject, "TransferProviderId", TransferProviderId);

            AddFieldToBusinessDataObject(dataObject, "StartDateTime", StartDateTime);
            AddFieldToBusinessDataObject(dataObject, "EndDateTime", EndDateTime);
            AddFieldToBusinessDataObject(dataObject, "Description", Description);
            AddFieldToBusinessDataObject(dataObject, "Autoupdated", Autoupdated);
            AddFieldToBusinessDataObject(dataObject, "Inactive", Inactive);

            return this.CreateRecord(dataObject);
        }
    }
}
