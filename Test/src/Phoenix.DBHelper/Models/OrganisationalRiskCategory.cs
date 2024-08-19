using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{

    public class OrganisationalRiskCategory : BaseClass
    {
        public string TableName = "OrganisationalRiskCategory";
        public string PrimaryKeyName = "OrganisationalRiskCategoryId";


        public OrganisationalRiskCategory(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateOrganisationalRiskCategory(string Name, int ValueFrom, int ValueTo, Guid ownerId)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "ValueFrom", ValueFrom);
            AddFieldToBusinessDataObject(businessDataObject, "ValueTo", ValueTo);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetOrganisationalRiskCategoryIdByCategoryName(string RiskCategoryName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, RiskCategoryName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Guid Create(Guid ownerId, string Name, string LegacyId, int ValueFrom, int ValueTo)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(buisinessDataObject, "ownerId", ownerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "LegacyId", LegacyId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValueFrom", ValueFrom);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValueTo", ValueTo);

            AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            return this.CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string Name)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetByOrganisationalRiskCategoryID(Guid OrganisationalRiskCategoryId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, OrganisationalRiskCategoryId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateName(Guid OrganisationalRiskCategoryId, string Name)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, OrganisationalRiskCategoryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactive(Guid OrganisationalRiskCategoryId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, OrganisationalRiskCategoryId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetOrganisationRiskByResponsibleUserID(Guid SystemUserAliasId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserAliasId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetByID(Guid OrganisationalRiskCategoryId, params string[] FieldsToReturn)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, CareWorks.Foundation.Enums.ConditionOperatorType.Equal, OrganisationalRiskCategoryId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void Delete(Guid OrganisationalRiskCategoryId)
        {
            this.DeleteRecord(TableName, OrganisationalRiskCategoryId);
        }


    }
}
