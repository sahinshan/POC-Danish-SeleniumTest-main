using CareWorks.Foundation.Enums;
using CareWorks.Foundation.SystemEntities;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class AboutMeSetup : BaseClass
    {
        public string TableName = "AboutMeSetup";
        public string PrimaryKeyName = "AboutMeSetupId";


        public AboutMeSetup(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonAboutMeSetup(Guid ownerId,
            bool enableMediaContent, int statusId,
            bool hideAboutMe, string hideAboutMeGuidelines,
            bool whatIsMostImportant, string whatIsMostImportantGuidelines,
            bool peopleWhoAreImportant, string peopleWhoAreImportantGuidelines,
            bool howICommunicate, string howICommunicateGuidelines,
            bool pleaseDoAndPleaseDoNot, string pleaseDoAndPleaseDoNotGuidelines,
            bool myWellness, string myWellnessGuidelines,
            bool howAndWhenToSupport, string howAndWhenToSupportGuidelines,
            bool alsoWorthKnowingAboutMe, string alsoWorthKnowingAboutMeGuidelines,
            bool physicalCharacteristics, string physicalCharacteristicsGuidelines)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            //AddFieldToBusinessDataObject(businessDataObject, "OwningBusinessUnitId", owningBusinessUnitId);
            //AddFieldToBusinessDataObject(businessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(businessDataObject, "EnableMediaContent", enableMediaContent);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", statusId);

            AddFieldToBusinessDataObject(businessDataObject, "HideAboutMeSection", hideAboutMe);
            AddFieldToBusinessDataObject(businessDataObject, "GuidelinesToConsiderWhenCapturingAbout", hideAboutMeGuidelines);

            AddFieldToBusinessDataObject(businessDataObject, "HideWhatIsMostImportantToMeSection", whatIsMostImportant);
            AddFieldToBusinessDataObject(businessDataObject, "GuidelinesToConsiderWhenCapturingWhat", whatIsMostImportantGuidelines);

            AddFieldToBusinessDataObject(businessDataObject, "HidePeopleWhoAreImportantToMeSect", peopleWhoAreImportant);
            AddFieldToBusinessDataObject(businessDataObject, "GuidelinesToConsiderWhenCapturingPeople", peopleWhoAreImportantGuidelines);

            AddFieldToBusinessDataObject(businessDataObject, "HideHowICommunicateAndHowToCom", howICommunicate);
            AddFieldToBusinessDataObject(businessDataObject, "GuidelinesToConsiderWhenCapturingHow", howICommunicateGuidelines);

            AddFieldToBusinessDataObject(businessDataObject, "HidePleaseDoAndPleaseDoNotSection", pleaseDoAndPleaseDoNot);
            AddFieldToBusinessDataObject(businessDataObject, "guidelinestoconsiderwhencapturingpleasedoand", pleaseDoAndPleaseDoNotGuidelines);

            AddFieldToBusinessDataObject(businessDataObject, "HideMyWellnessSection", myWellness);
            AddFieldToBusinessDataObject(businessDataObject, "GuidelinesToConsiderWhenCapturingMyWellness", myWellnessGuidelines);

            AddFieldToBusinessDataObject(businessDataObject, "HideHowAndWhenToSupportMeSection", howAndWhenToSupport);
            AddFieldToBusinessDataObject(businessDataObject, "GuidelinesToConsiderWhenCapturingHowAndWhenTo", howAndWhenToSupportGuidelines);

            AddFieldToBusinessDataObject(businessDataObject, "HideAlsoWorthKnowingAboutMeSection", alsoWorthKnowingAboutMe);
            AddFieldToBusinessDataObject(businessDataObject, "GuidelinesToConsiderWhenCapturingAlsoWorthKno", alsoWorthKnowingAboutMeGuidelines);

            AddFieldToBusinessDataObject(businessDataObject, "HidePhysicalCharacteristicsSection", physicalCharacteristics);
            AddFieldToBusinessDataObject(businessDataObject, "GuidelinesToConsiderWhenCapturingGuidanceToCo", physicalCharacteristicsGuidelines);


            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public Guid CreatePersonAboutMeSetup(Guid ownerId,
            bool enableMediaContent, int statusId,
            bool hideAboutMe, bool whatIsMostImportant,
            bool peopleWhoAreImportant, bool howICommunicate,
            bool pleaseDoAndPleaseDoNot, bool myWellness,
            bool howAndWhenToSupport, bool alsoWorthKnowingAboutMe, bool physicalCharacteristics)
        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);


            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            AddFieldToBusinessDataObject(businessDataObject, "EnableMediaContent", enableMediaContent);
            AddFieldToBusinessDataObject(businessDataObject, "StatusId", statusId);

            AddFieldToBusinessDataObject(businessDataObject, "HideAboutMeSection", hideAboutMe);
            AddFieldToBusinessDataObject(businessDataObject, "HideWhatIsMostImportantToMeSection", whatIsMostImportant);
            AddFieldToBusinessDataObject(businessDataObject, "HidePeopleWhoAreImportantToMeSect", peopleWhoAreImportant);
            AddFieldToBusinessDataObject(businessDataObject, "HideHowICommunicateAndHowToCom", howICommunicate);
            AddFieldToBusinessDataObject(businessDataObject, "HidePleaseDoAndPleaseDoNotSection", pleaseDoAndPleaseDoNot);
            AddFieldToBusinessDataObject(businessDataObject, "HideMyWellnessSection", myWellness);
            AddFieldToBusinessDataObject(businessDataObject, "HideHowAndWhenToSupportMeSection", howAndWhenToSupport);
            AddFieldToBusinessDataObject(businessDataObject, "HideAlsoWorthKnowingAboutMeSection", alsoWorthKnowingAboutMe);
            AddFieldToBusinessDataObject(businessDataObject, "HidePhysicalCharacteristicsSection", physicalCharacteristics);

            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);

            return this.CreateRecord(businessDataObject);
        }

        public List<Guid> GetAboutMeSetupIdByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", CareWorks.Foundation.Enums.ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetByAboutMeSetupID(Guid AboutMeSetupId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, AboutMeSetupId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        //public void UpdateName(Guid OrganisationalRiskCategoryId, string Name)
        //{
        //    var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

        //    this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, OrganisationalRiskCategoryId);
        //    this.AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);

        //    this.UpdateRecord(buisinessDataObject);
        //}

        public void UpdateInactive(Guid AboutMeSetupId, bool Inactive)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, AboutMeSetupId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", Inactive);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetAll()
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAll(bool Inactive)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Inactive", ConditionOperatorType.Equal, Inactive);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetAboutMeSetupIdByResponsibleUserID(Guid SystemUserAliasId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SystemUserAliasId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Dictionary<string, object> GetByID(Guid AboutMeSetupId, params string[] FieldsToReturn)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, CareWorks.Foundation.Enums.ConditionOperatorType.Equal, AboutMeSetupId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void Delete(Guid AboutMeSetupId)
        {
            this.DeleteRecord(TableName, AboutMeSetupId);
        }

    }
}
