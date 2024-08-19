using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonAboutMe : BaseClass
    {
        public string TableName = "PersonAboutMe";
        public string PrimaryKeyName = "PersonAboutMeId";


        public PersonAboutMe(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonAboutMe(DateTime date, Guid responsibleUserId, Guid owningBusinessUnitId, Guid ownerId, Guid supportedToWriteThisById,
            Guid personId, bool capacityEstablished, bool consentGranted, string supportedtowritethisbyidtablename,
            string supportedtowritethisbyidname, Guid aboutMeSetupId)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "Date", date);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", responsibleUserId);
            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            AddFieldToBusinessDataObject(businessDataObject, "OwningBusinessUnitId", owningBusinessUnitId);
            AddFieldToBusinessDataObject(businessDataObject, "SupportedToWriteThisById", supportedToWriteThisById);
            AddFieldToBusinessDataObject(businessDataObject, "PersonId", personId);
            AddFieldToBusinessDataObject(businessDataObject, "AboutMeSetupId", aboutMeSetupId);
            AddFieldToBusinessDataObject(businessDataObject, "CapacityEstablished", capacityEstablished);
            AddFieldToBusinessDataObject(businessDataObject, "ConsentGranted", consentGranted);

            AddFieldToBusinessDataObject(businessDataObject, "supportedtowritethisbyidname", supportedtowritethisbyidname);
            AddFieldToBusinessDataObject(businessDataObject, "supportedtowritethisbyidtablename", supportedtowritethisbyidtablename);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public Guid CreatePersonAboutMe(DateTime date, Guid responsibleUserId, Guid ownerId, Guid supportedToWriteThisById,
                                        Guid personId, Guid aboutMeSetupId, bool capacityEstablished, bool consentGranted,
                                        string AboutMe, string WhatIsMostImportant, string peopleWhoAreImportant,
                                        string howICommunicate, string pleaseDoAndPleaseDoNot,
                                        string myWellness, string howAndWhenToSupport,
                                        string alsoWorthKnowingAboutMe, string physicalCharacteristics)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "Date", date);
            AddFieldToBusinessDataObject(businessDataObject, "ResponsibleUserId", responsibleUserId);
            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            //AddFieldToBusinessDataObject(businessDataObject, "OwningBusinessUnitId", owningBusinessUnitId);
            AddFieldToBusinessDataObject(businessDataObject, "SupportedToWriteThisById", supportedToWriteThisById);
            AddFieldToBusinessDataObject(businessDataObject, "PersonId", personId);
            AddFieldToBusinessDataObject(businessDataObject, "AboutMeSetupId", aboutMeSetupId);
            AddFieldToBusinessDataObject(businessDataObject, "CapacityEstablished", capacityEstablished);
            AddFieldToBusinessDataObject(businessDataObject, "ConsentGranted", consentGranted);

            AddFieldToBusinessDataObject(businessDataObject, "AboutMe", AboutMe);
            AddFieldToBusinessDataObject(businessDataObject, "WhatIsMostImportant", WhatIsMostImportant);
            AddFieldToBusinessDataObject(businessDataObject, "PeopleWhoAreImportant", peopleWhoAreImportant);
            AddFieldToBusinessDataObject(businessDataObject, "HowICommunicate", howICommunicate);
            AddFieldToBusinessDataObject(businessDataObject, "PleaseDoAndPleaseDoNot", pleaseDoAndPleaseDoNot);
            AddFieldToBusinessDataObject(businessDataObject, "MyWellness", myWellness);
            AddFieldToBusinessDataObject(businessDataObject, "HowAndWhenToSupport", howAndWhenToSupport);
            AddFieldToBusinessDataObject(businessDataObject, "AlsoWorthKnowingAboutMe", alsoWorthKnowingAboutMe);
            AddFieldToBusinessDataObject(businessDataObject, "PhysicalCharacteristics", physicalCharacteristics);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);

            return this.CreateRecord(businessDataObject);
        }




        public void UpdateDate(Guid PersonAboutMeId, DateTime date)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonAboutMeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Date", date);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactive(Guid PersonAboutMeId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonAboutMeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetPersonAboutMeResponsibleUserID(Guid SystemUserAliasId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserAliasId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetByID(Guid PersonAboutMeId, params string[] FieldsToReturn)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, CareWorks.Foundation.Enums.ConditionOperatorType.Equal, PersonAboutMeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void Delete(Guid PersonAboutMeId)
        {
            this.DeleteRecord(TableName, PersonAboutMeId);
        }

        public List<Guid> GetByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonID(Guid PersonID, bool Inactive)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);
            this.BaseClassAddTableCondition(query, "Inactive", ConditionOperatorType.Equal, Inactive);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
