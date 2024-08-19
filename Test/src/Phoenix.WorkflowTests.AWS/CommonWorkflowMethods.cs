using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WorkflowTests.AWS
{
    public class CommonWorkflowMethods
    {
        internal DBHelper.DatabaseHelper dbHelper;
        internal WorkflowTestFramework.Helpers.FileIOHelper fileIOHelper;
        internal TestContext testContext;


        public CommonWorkflowMethods(DBHelper.DatabaseHelper _dbHelper, WorkflowTestFramework.Helpers.FileIOHelper _fileIOHelper, TestContext _testContext)
        {
            dbHelper = _dbHelper;
            fileIOHelper = _fileIOHelper;
            testContext = _testContext;
        }

        public Guid CreateDocumentIfNeeded(string DocumentName, string ZipFileName)
        {
            if (!dbHelper.document.GetDocumentByName(DocumentName).Any())
            {
                var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
                dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
            }

            var documentID = dbHelper.document.GetDocumentByName(DocumentName).First();

            dbHelper.document.UpdateStatus(documentID, 100000000); //Set the status to published 

            return documentID;

        }

        public Guid CreateWorkflowIfNeeded(string WorkflowName, string ZipFileName)
        {
            if (!dbHelper.workflow.GetWorkflowByName(WorkflowName).Any())
            {
                var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
                dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
            }

            return dbHelper.workflow.GetWorkflowByName(WorkflowName).First();

        }

        public Guid CreateWorkflowIfNeeded(string WorkflowName, string ZipFileName, Guid OwnerId)
        {
            var workflowID = CreateWorkflowIfNeeded(WorkflowName, ZipFileName);
            dbHelper.workflow.UpdateOwner(workflowID, OwnerId);

            return workflowID;
        }

        public Guid CreateBusinessUnit(string businessUnitName, Guid? parentBusinessUnit)
        {
            #region Business Unit 2

            Guid _recordId = Guid.Empty;

            if (!dbHelper.businessUnit.GetByName(businessUnitName).Any())
                dbHelper.businessUnit.CreateBusinessUnit(businessUnitName, parentBusinessUnit);
            _recordId = dbHelper.businessUnit.GetByName(businessUnitName)[0];

            if (_recordId == Guid.Empty)
                _recordId = dbHelper.businessUnit.GetByName(businessUnitName).First();

            return _recordId;

            #endregion
        }

        public Guid CreateTeam(string teamName, Guid businessUnitid)
        {
            #region Team

            Guid _recordId = Guid.Empty;

            if (!dbHelper.team.GetTeamIdByName(teamName).Any())
                dbHelper.team.CreateTeam(teamName, null, businessUnitid, "907678", teamName.Replace(" ", "") + "@careworkstempmail.com", teamName, "020 123456");
            _recordId = dbHelper.team.GetTeamIdByName(teamName)[0];

            if (_recordId == Guid.Empty)
                _recordId = dbHelper.team.GetTeamIdByName(teamName).First();

            return _recordId;

            #endregion
        }

        public Guid CreateTeam(Guid Teamid, string teamName, Guid businessUnitid)
        {
            #region Team

            Guid _recordId = Guid.Empty;

            if (!dbHelper.team.GetTeamIdByName(teamName).Any())
                dbHelper.team.CreateTeam(Teamid, teamName, null, businessUnitid, "907678", teamName.Replace(" ", "").Replace("-", "") + "@careworkstempmail.com", teamName, "020 123456");
            _recordId = dbHelper.team.GetTeamIdByName(teamName)[0];

            if (_recordId == Guid.Empty)
                _recordId = dbHelper.team.GetTeamIdByName(teamName).First();

            return _recordId;

            #endregion
        }
        
        public Guid CreateTeam(Guid Teamid, string teamName, Guid? teamManager, Guid businessUnitid)
        {
            #region Team

            Guid _recordId = Guid.Empty;

            if (!dbHelper.team.GetTeamIdByName(teamName).Any()) 
                dbHelper.team.CreateTeam(Teamid, teamName, teamManager, businessUnitid, "907678", teamName.Replace(" ", "") + "@careworkstempmail.com", teamName, "020 123456"); _recordId = dbHelper.team.GetTeamIdByName(teamName)[0];

            if (_recordId == Guid.Empty) _recordId = dbHelper.team.GetTeamIdByName(teamName).First();

            return _recordId;

            #endregion        
        }        

        public Guid CreatePersonRecord(Guid _ethnicityId, Guid _defaultTeamId)
        {
            #region Person

            var firstName = "Jhon";
            var middleName = "WF Test";
            var lastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            return CreatePersonRecord(firstName, middleName, lastName, _ethnicityId, _defaultTeamId);

            #endregion
        }

        public Guid CreatePersonRecord(string firstName, string middleName, string lastName, Guid _ethnicityId, Guid _defaultTeamId)
        {
            #region Person

            return dbHelper.person.CreatePersonRecord("", firstName, middleName, lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _defaultTeamId, 7, 2);

            #endregion
        }

        public Guid CreatePersonRecord(Guid PersonId, string firstName, string middleName, string lastName, Guid _ethnicityId, Guid _defaultTeamId)
        {
            #region Person

            var fields = dbHelper.person.GetPersonById(PersonId, "personid");

            if (!fields.ContainsKey("personid")) //if the personid key does not exist it means there is no person with this personid in the DB. we need to create a new one
                dbHelper.person.CreatePersonRecord(PersonId, "", firstName, middleName, lastName, "", new DateTime(2000, 1, 2), _ethnicityId, _defaultTeamId, 7, 2);

            return PersonId;
            #endregion
        }

        public Guid CreatePersonRecord(string firstName, string middleName, string lastName, Guid _ethnicityId, Guid _defaultTeamId, DateTime DOB)
        {
            #region Person

            return dbHelper.person.CreatePersonRecord("", firstName, middleName, lastName, "", DOB, _ethnicityId, _defaultTeamId, 7, 2);

            #endregion
        }

        public Guid CreateSystemUserRecord(string username, string firstName, string lastName, string password, Guid businessUnitId, Guid teamId, Guid _languageId, Guid _authenticationproviderid)
        {
            #region System User

            Guid _userId = Guid.Empty;

            if (!dbHelper.systemUser.GetSystemUserByUserName(username).Any())
            {
                _userId = dbHelper.systemUser.CreateSystemUser(username, firstName, lastName, firstName + " " + lastName, password, username + "@somemail.com", username + "@otheremail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, businessUnitId, teamId, DateTime.Now.Date);
                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_userId, systemAdministratorSecurityProfileId);
            }

            if (_userId == Guid.Empty)
                _userId = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_userId, DateTime.Now.Date);

            return _userId;

            #endregion
        }

        public Guid CreateSystemUserRecord(Guid SystemUserId, string username, string firstName, string lastName, string password, Guid businessUnitId, Guid teamId, Guid _languageId, Guid _authenticationproviderid)
        {
            #region System User

            Guid _userId = Guid.Empty;

            if (!dbHelper.systemUser.GetSystemUserByUserName(username).Any())
            {
                _userId = dbHelper.systemUser.CreateSystemUser(SystemUserId, username, firstName, lastName, firstName + " " + lastName, password, username + "@somemail.com", username + "@otheremail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, businessUnitId, teamId, DateTime.Now.Date);
                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                dbHelper.userSecurityProfile.CreateUserSecurityProfile(_userId, systemAdministratorSecurityProfileId);
            }

            if (_userId == Guid.Empty)
                _userId = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_userId, DateTime.Now.Date);

            return _userId;

            #endregion
        }

        public Guid CreateDataRestrictionRecord(string Name, int AccessType, Guid OwnerId)
        {
            #region Data Restriction
            Guid recordID = Guid.Empty;

            if (!dbHelper.dataRestriction.GetByName(Name).Any())
                dbHelper.dataRestriction.CreateDataRestriction(Name, AccessType, OwnerId);

            if (recordID != Guid.Empty)
                return recordID;

            return dbHelper.dataRestriction.GetByName(Name).FirstOrDefault();

            #endregion
        }

        public Guid CreateDataRestrictionRecord(Guid DataRestrictionId, string Name, int AccessType, Guid OwnerId)
        {
            #region Data Restriction
            Guid recordID = Guid.Empty;

            if (!dbHelper.dataRestriction.GetByName(Name).Any())
                dbHelper.dataRestriction.CreateDataRestriction(DataRestrictionId, Name, AccessType, OwnerId);

            if (recordID != Guid.Empty)
                return recordID;

            return dbHelper.dataRestriction.GetByName(Name).FirstOrDefault();

            #endregion
        }



        public Guid CreateSignificantEventCategory(string significantEventName, DateTime startdate, Guid ownerid, string govcode, int? code, DateTime? enddate)
        {
            Guid _significantEventCategoryId = Guid.Empty;

            if (!dbHelper.significantEventCategory.GetByName(significantEventName).Any())
                _significantEventCategoryId = dbHelper.significantEventCategory.CreateSignificantEventCategory(significantEventName, startdate, ownerid, code, govcode, enddate);

            return _significantEventCategoryId;

        }

        public Guid CreateUserRestrictedDataAccess(Guid dataRestrictionID, Guid userid, DateTime StartDate, Guid OwnerId)
        {
            #region Data Restriction

            Guid recordID = Guid.Empty;


            if (!dbHelper.userRestrictedDataAccess.GetUserRestrictedDataAccessByUserID(userid, dataRestrictionID).Any())
                dbHelper.userRestrictedDataAccess.CreateUserRestrictedDataAccess(dataRestrictionID, userid, StartDate, null, OwnerId);

            if (recordID != Guid.Empty)
                return recordID;

            return dbHelper.userRestrictedDataAccess.GetUserRestrictedDataAccessByUserID(dataRestrictionID, userid).FirstOrDefault();

            #endregion
        }

        public Guid CreateTeamRestrictedDataAccess(Guid dataRestrictionID, Guid teamid, DateTime StartDate, Guid OwnerId)
        {
            #region Team Data Restriction

            Guid recordID = Guid.Empty;


            if (!dbHelper.teamRestrictedDataAccess.GetTeamRestrictedDataAccessByTeamID(teamid, dataRestrictionID).Any())
                dbHelper.teamRestrictedDataAccess.CreateTeamRestrictedDataAccess(dataRestrictionID, teamid, StartDate, null, OwnerId);

            if (recordID != Guid.Empty)
                return recordID;

            return dbHelper.teamRestrictedDataAccess.GetTeamRestrictedDataAccessByTeamID(dataRestrictionID, teamid).FirstOrDefault();

            #endregion
        }

        public Guid CreateContactReasonIfNeeded(string RecordName, Guid OwnerId)
        {
            if (!dbHelper.contactReason.GetByName(RecordName).Any())
                dbHelper.contactReason.CreateContactReason(OwnerId, RecordName, new DateTime(2020, 1, 1), 110000000, false);

            return dbHelper.contactReason.GetByName(RecordName)[0];
        }

        public Guid CreateContactPresentingPriorityNeeded(string RecordName, Guid OwnerId)
        {
            if (!dbHelper.contactPresentingPriority.GetByName(RecordName).Any())
                dbHelper.contactPresentingPriority.CreateContactPresentingPriority(OwnerId, RecordName, null, null, new DateTime(2020, 1, 1));

            return dbHelper.contactPresentingPriority.GetByName(RecordName)[0];
        }

        public Guid CreateContactSourceIfNeeded(string RecordName, Guid OwnerId)
        {
            if (!dbHelper.contactSource.GetByName(RecordName).Any())
                dbHelper.contactSource.CreateContactSource(OwnerId, RecordName, new DateTime(2020, 1, 1));
            return dbHelper.contactSource.GetByName(RecordName)[0];
        }

        public Guid CreateContactSourceIfNeeded(Guid ContactSourceId, string RecordName, Guid OwnerId)
        {
            if (!dbHelper.contactSource.GetByName(RecordName).Any())
                dbHelper.contactSource.CreateContactSource(ContactSourceId, OwnerId, RecordName, new DateTime(2020, 1, 1));
            return dbHelper.contactSource.GetByName(RecordName)[0];
        }

        public Guid CreateCreateContactStatusIfNeeded(string RecordName, string Code, Guid OwnerId, int CategoryId, bool ValidForReopening)
        {
            if (!dbHelper.contactStatus.GetByName(RecordName).Any())
                dbHelper.contactStatus.CreateContactStatus(OwnerId, RecordName, Code, new DateTime(2020, 1, 1), CategoryId, ValidForReopening);
            return dbHelper.contactStatus.GetByName(RecordName)[0];
        }

        public Guid CreateCreateContactStatusIfNeeded(Guid ContactStatusid, string RecordName, string Code, Guid OwnerId, int CategoryId, bool ValidForReopening)
        {
            if (!dbHelper.contactStatus.GetByName(RecordName).Any())
                dbHelper.contactStatus.CreateContactStatus(ContactStatusid, OwnerId, RecordName, Code, new DateTime(2020, 1, 1), CategoryId, ValidForReopening);
            return dbHelper.contactStatus.GetByName(RecordName)[0];
        }

        public Guid CreateCreateContactTypeIfNeeded(string RecordName, Guid OwnerId)
        {
            if (!dbHelper.contactType.GetByName(RecordName).Any())
                dbHelper.contactType.CreateContactType(OwnerId, RecordName, new DateTime(2020, 1, 1), true);
            return dbHelper.contactType.GetByName(RecordName)[0];
        }

        public Guid CreateCreateContactTypeIfNeeded(Guid ContactTypeid, string RecordName, Guid OwnerId)
        {
            if (!dbHelper.contactType.GetByName(RecordName).Any())
                dbHelper.contactType.CreateContactType(ContactTypeid, OwnerId, RecordName, new DateTime(2020, 1, 1), true);
            return dbHelper.contactType.GetByName(RecordName)[0];
        }

        public Guid CreateCreateContactOutcomeIfNeeded(Guid ownerid, string Name, bool ValidForSequelToContact, bool ValidForAdministrativeCategory, int OutcomeCategoryId)
        {
            if (!dbHelper.contactOutcome.GetByName(Name).Any())
                dbHelper.contactOutcome.CreateContactOutcome(ownerid, Name, new DateTime(2020, 1, 1), ValidForSequelToContact, ValidForAdministrativeCategory, OutcomeCategoryId);
            return dbHelper.contactOutcome.GetByName(Name)[0];
        }

        public Guid CreateCreateContactOutcomeIfNeeded(Guid ContactOutcomeid, Guid ownerid, string Name, bool ValidForSequelToContact, bool ValidForAdministrativeCategory, int OutcomeCategoryId)
        {
            if (!dbHelper.contactOutcome.GetByName(Name).Any())
                dbHelper.contactOutcome.CreateContactOutcome(ContactOutcomeid, ownerid, Name, new DateTime(2020, 1, 1), ValidForSequelToContact, ValidForAdministrativeCategory, OutcomeCategoryId);
            return dbHelper.contactOutcome.GetByName(Name)[0];
        }

        public Guid CreateCreatereferralPriorityIfNeeded(Guid ownerid, string Name, int PriorityCategoryId)
        {
            if (!dbHelper.referralPriority.GetByName(Name).Any())
                dbHelper.referralPriority.CreateReferralPriority(ownerid, Name, new DateTime(2020, 1, 1), PriorityCategoryId);
            return dbHelper.referralPriority.GetByName(Name)[0];
        }

        public Guid CreateCreatereferralPriorityIfNeeded(Guid ReferralPriorityid, Guid ownerid, string Name, int PriorityCategoryId)
        {
            if (!dbHelper.referralPriority.GetByName(Name).Any())
                dbHelper.referralPriority.CreateReferralPriority(ReferralPriorityid, ownerid, Name, new DateTime(2020, 1, 1), PriorityCategoryId);
            return dbHelper.referralPriority.GetByName(Name)[0];
        }

        public Guid CreateProviderIfNeeded(string RecordName, Guid OwnerId, string email = "")
        {
            if (!dbHelper.provider.GetProviderByName(RecordName).Any())
                dbHelper.provider.CreateProvider(RecordName, OwnerId, 3, email);

            return dbHelper.provider.GetProviderByName(RecordName)[0];
        }

        public Guid CreateSignificantEventCategoryIfNeeded(string RecordName, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.significantEventCategory.GetByName(RecordName).Any())
                dbHelper.significantEventCategory.CreateSignificantEventCategory(RecordName, StartDate, OwnerId);

            return dbHelper.significantEventCategory.GetByName(RecordName)[0];
        }

        public Guid CreateCaseActionTypeIfNeeded(string RecordName, Guid OwnerId, int BusinessTypeId)
        {
            if (!dbHelper.caseActionType.GetByName(RecordName).Any())
                dbHelper.caseActionType.CreateCaseActionType(OwnerId, RecordName, new DateTime(2020, 1, 1), BusinessTypeId);
            return dbHelper.caseActionType.GetByName(RecordName)[0];
        }

        public Guid CreateCaseActionTypeIfNeeded(Guid caseActionTypeId, string RecordName, Guid OwnerId, int BusinessTypeId)
        {
            if (!dbHelper.caseActionType.GetByName(RecordName).Any())
                dbHelper.caseActionType.CreateCaseActionType(caseActionTypeId, OwnerId, RecordName, new DateTime(2020, 1, 1), BusinessTypeId);
            return dbHelper.caseActionType.GetByName(RecordName)[0];
        }

        public Guid CreateDataRestrictionIfNeeded(string RecordName, int AccessTypeID, Guid OwnerId)
        {
            if (!dbHelper.dataRestriction.GetByName(RecordName).Any())
                dbHelper.dataRestriction.CreateDataRestriction(RecordName, AccessTypeID, OwnerId);
            return dbHelper.dataRestriction.GetByName(RecordName)[0];
        }

        public Guid CreateDataRestrictionIfNeeded(Guid RecordId, string RecordName, int AccessTypeID, Guid OwnerId)
        {
            if (!dbHelper.dataRestriction.GetByName(RecordName).Any())
                dbHelper.dataRestriction.CreateDataRestriction(RecordId, RecordName, AccessTypeID, OwnerId);
            return dbHelper.dataRestriction.GetByName(RecordName)[0];

        }

        public Guid CreateActivityCategory(Guid ActivityCategoryId, string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityCategory.GetActivityCategoryByID(ActivityCategoryId, "activitycategoryid").ContainsKey("activitycategoryid");
            
            if (!recordExists)
                dbHelper.activityCategory.CreateActivityCategory(ActivityCategoryId, Name, StartDate, OwnerId);
            
            return ActivityCategoryId;

        }

        public Guid CreateActivitySubCategory(Guid ActivitySubCategoryId, string Name, DateTime StartDate, Guid ActivityCategoryId, Guid OwnerId)
        {

            var recordExists = dbHelper.activitySubCategory.GetActivitySubCategoryByID(ActivitySubCategoryId, "ActivitySubCategoryId".ToLower()).ContainsKey("ActivitySubCategoryId".ToLower());

            if (!recordExists)
                dbHelper.activitySubCategory.CreateActivitySubCategory(ActivitySubCategoryId, Name, StartDate, ActivityCategoryId, OwnerId);

            return ActivitySubCategoryId;

        }

        public Guid CreateActivityReason(Guid PrimaryKeyId, string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityReason.GetActivityReasonByID(PrimaryKeyId, "ActivityReasonId".ToLower()).ContainsKey("ActivityReasonId".ToLower());

            if (!recordExists)
                dbHelper.activityReason.CreateActivityReason(PrimaryKeyId, Name, StartDate, OwnerId);

            return PrimaryKeyId;

        }

        public Guid CreateActivityPriority(Guid ActivityPriorityId, string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityPriority.GetActivityPriorityByID(ActivityPriorityId, "ActivityPriorityId".ToLower()).ContainsKey("ActivityPriorityId".ToLower());

            if (!recordExists)
                dbHelper.activityPriority.CreateActivityPriority(ActivityPriorityId, Name, StartDate, OwnerId);

            return ActivityPriorityId;

        }

        public Guid CreateActivityOutcome(Guid ActivityOutcomeId, string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityOutcome.GetActivityOutcomeByID(ActivityOutcomeId, "ActivityOutcomeId".ToLower()).ContainsKey("ActivityOutcomeId".ToLower());

            if (!recordExists)
                dbHelper.activityReason.CreateActivityReason(ActivityOutcomeId, Name, StartDate, OwnerId);

            return ActivityOutcomeId;

        }

    }
}
