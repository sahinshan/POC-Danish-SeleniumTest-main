using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AllegationInvestigator : BaseClass
    {

        private string tableName = "AllegationInvestigator";
        private string primaryKeyName = "AllegationInvestigatorId";

        public AllegationInvestigator()
        {
            AuthenticateUser();
        }

        public AllegationInvestigator(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateAllegationInvestigator(Guid OwnerId, Guid ResponsibleUserId, Guid AllegationId, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "ResponsibleUserId", ResponsibleUserId);
            AddFieldToBusinessDataObject(dataObject, "AllegationId", AllegationId);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "InvestigatorId", 2);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetAllegationInvestigatorByAllegationID(Guid AllegationId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "AllegationId", ConditionOperatorType.Equal, AllegationId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetAllegationInvestigatorByID(Guid AllegationInvestigatorId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, AllegationInvestigatorId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteAllegationInvestigator(Guid AllegationInvestigatorID)
        {
            this.DeleteRecord(tableName, AllegationInvestigatorID);
        }



    }
}
