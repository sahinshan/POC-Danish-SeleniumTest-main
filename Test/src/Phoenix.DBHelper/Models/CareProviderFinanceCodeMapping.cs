using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderFinanceCodeMapping : BaseClass
    {

        public string TableName = "CareProviderFinanceCodeMapping";
        public string PrimaryKeyName = "CareProviderFinanceCodeMappingId";

        public CareProviderFinanceCodeMapping()
        {
            AuthenticateUser();
        }

        public CareProviderFinanceCodeMapping(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByTitle(string Title)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

        public Guid CreateCareProviderFinanceCodeMapping(Guid OwnerId, Guid CareProviderContractSchemeId, Guid Level1Id, Guid? Level2Id = null, Guid? Level3Id = null, Guid? Level4Id = null, string Level1Constant = null, string Level2Constant = null, string Level3Constant = null, string Level4Constant = null, bool IsUpdateableOnPersonContractService = false, bool Inactive = false, bool ValidForExport = false)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CareProviderContractSchemeId", CareProviderContractSchemeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsUpdateableOnPersonContractService", IsUpdateableOnPersonContractService);
            AddFieldToBusinessDataObject(buisinessDataObject, "Level1Id", Level1Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "Level2Id", Level2Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "Level3Id", Level3Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "Level4Id", Level4Id);
            AddFieldToBusinessDataObject(buisinessDataObject, "Level1Constant", Level1Constant);
            AddFieldToBusinessDataObject(buisinessDataObject, "Level2Constant", Level2Constant);
            AddFieldToBusinessDataObject(buisinessDataObject, "Level3Constant", Level3Constant);
            AddFieldToBusinessDataObject(buisinessDataObject, "Level4Constant", Level4Constant);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", ValidForExport);

            return CreateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByCareProviderFinanceCodeMappingId(Guid CareProviderFinanceCodeMappingId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CareProviderFinanceCodeMappingId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
