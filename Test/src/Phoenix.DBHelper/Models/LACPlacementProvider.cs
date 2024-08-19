using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class LACPlacementProvider : BaseClass
    {

        private string tableName = "LACPlacementProvider";
        private string primaryKeyName = "LACPlacementProviderId";

        public LACPlacementProvider()
        {
            AuthenticateUser();
        }

        public LACPlacementProvider(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateLACPlacementProvider(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);

            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "GovCode", GovCode);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetLACPlacementProviderByID(Guid LACPlacementProviderId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "LACPlacementProviderId", ConditionOperatorType.Equal, LACPlacementProviderId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteLACPlacementProvider(Guid LACPlacementProviderID)
        {
            this.DeleteRecord(tableName, LACPlacementProviderID);
        }



    }
}
