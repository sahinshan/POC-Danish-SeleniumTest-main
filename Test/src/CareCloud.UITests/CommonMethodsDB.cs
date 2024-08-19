
using Phoenix.DBHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest.Configuration;
using CareCloudTestFramework;
using System.Configuration;
using System.Xml.Linq;
//using Phoenix.UITests.Framework.FileSystem;



namespace CareCloud.UITests
{
    public class CommonMethodsDB
    {
        private Phoenix.DBHelper.DatabaseHelper dbHelper;
        internal TestContext testContext;
        //private Phoenix.FileSystem.FileIOHelper fileIOHelper;


        public CommonMethodsDB(Phoenix.DBHelper.DatabaseHelper DBHelper)
        {
            dbHelper = DBHelper;
        }

        public CommonMethodsDB(Phoenix.DBHelper.DatabaseHelper _dbHelper, TestContext _testContext)
        {
            _dbHelper = _dbHelper;
            //fileIOHelper = _fileIOHelper;
            testContext = _testContext;
        }

        public Guid CreatePersonRecord(string FirstName, string LastName, Guid EthnicityId, Guid TeamID)
        {
            #region Person
            Guid _personID = Guid.Empty;

            if (!dbHelper.person.GetByFirstAndLastName(FirstName, LastName).Any())
            {
                _personID = dbHelper.person.CreatePersonRecord("", FirstName, "", LastName, "", new DateTime(2000, 1, 2), EthnicityId, TeamID, 7, 2);
                System.Threading.Thread.Sleep(2000);
            }
            if (_personID == Guid.Empty)
            {
                _personID = dbHelper.person.GetByFirstAndLastName(FirstName, LastName).FirstOrDefault();
            }
            return _personID;

            #endregion
        }

        public Guid CreatePersonRecord(string Title, string FirstName, string MiddleName, string LastName, string PreferredName, DateTime DateOfBirth,
            Guid Ethnicity, Guid OwnerID, DateTime? addressstartdate, int AddressTypeId, int GenderId, string NHSNumber, string LegacyId, string NationalInsuranceNumber, string UniquePupilNumber,
            string PropertyName = "", string PropertyNo = "", string Street = "", string VlgDistrict = "", string TownCity = "", string County = "", string Postcode = "")
        {
            #region Person

            Guid _personID = Guid.Empty;

            if (!dbHelper.person.GetByFirstAndLastName(FirstName, LastName).Any())
            {
                _personID = dbHelper.person.CreatePersonRecord(Title, FirstName, MiddleName, LastName, PreferredName, DateOfBirth,
                    Ethnicity, OwnerID, addressstartdate, AddressTypeId, GenderId, NHSNumber, LegacyId, NationalInsuranceNumber, UniquePupilNumber,
                    PropertyName, PropertyNo, Street, VlgDistrict, TownCity, County, Postcode);
                System.Threading.Thread.Sleep(2000);
            }
            if (_personID == Guid.Empty)
            {
                _personID = dbHelper.person.GetByFirstAndLastName(FirstName, LastName).FirstOrDefault();
            }
            return _personID;

            #endregion
        }

        public Guid CreatePersonRecord(string FirstName, string LastName, Guid EthnicityId, Guid TeamID, DateTime DOB)
        {
            #region Person
            Guid _personID = Guid.Empty;

            if (!dbHelper.person.GetByFirstAndLastName(FirstName, LastName).Any())
            {
                _personID = dbHelper.person.CreatePersonRecord("", FirstName, "", LastName, "", DOB, EthnicityId, TeamID, 7, 2);
                System.Threading.Thread.Sleep(2000);
            }
            if (_personID == Guid.Empty)
            {
                _personID = dbHelper.person.GetByFirstAndLastName(FirstName, LastName).FirstOrDefault();
            }
            return _personID;

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

        public Guid CreateContactReasonIfNeeded(string RecordName, Guid OwnerId)
        {
            if (!dbHelper.contactReason.GetByName(RecordName).Any())
                dbHelper.contactReason.CreateContactReason(OwnerId, RecordName, new DateTime(2020, 1, 1), 110000000, false);

            return dbHelper.contactReason.GetByName(RecordName)[0];
        }

        public Guid CreateContactReasonIfNeeded(string RecordName, Guid OwnerId, int BusinessTypeId, bool SupportMultipleCases)
        {
            if (!dbHelper.contactReason.GetByName(RecordName).Any())
                dbHelper.contactReason.CreateContactReason(OwnerId, RecordName, new DateTime(2020, 1, 1), BusinessTypeId, SupportMultipleCases);

            return dbHelper.contactReason.GetByName(RecordName)[0];
        }

        public Guid CreateContactReason(Guid OwnerId, string Name, DateTime StartDate, int BusinessTypeId, int rttadmissiontypeid, bool SupportMultipleCases)
        {
            if (!dbHelper.contactReason.GetByName(Name).Any())
                dbHelper.contactReason.CreateContactReason(OwnerId, Name, StartDate, BusinessTypeId, rttadmissiontypeid, SupportMultipleCases);

            return dbHelper.contactReason.GetByName(Name)[0];
        }

        public Guid CreateContactReason(Guid contactreasonid, Guid OwnerId, string Name, DateTime StartDate, int BusinessTypeId, int rttadmissiontypeid, bool SupportMultipleCases)
        {
            if (!dbHelper.contactReason.GetByName(Name).Any())
                dbHelper.contactReason.CreateContactReason(contactreasonid, OwnerId, Name, StartDate, BusinessTypeId, rttadmissiontypeid, SupportMultipleCases);

            return dbHelper.contactReason.GetByName(Name)[0];
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

        public Guid CreateSystemUserRecord(string username, string firstName, string lastName, string password, Guid businessUnitId, Guid teamId, Guid _languageId, Guid _authenticationproviderid)
        {
            #region System User

            Guid _userId = Guid.Empty;

            if (!dbHelper.systemUser.GetSystemUserByUserName(username).Any())
            {
                _userId = dbHelper.systemUser.CreateSystemUser(username, firstName, lastName, firstName + " " + lastName, password, username + "@somemail.com", username + "@otheremail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, businessUnitId, teamId, DateTime.Now.Date);
                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                var secureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
                CreateUserSecurityProfile(_userId, systemAdministratorSecurityProfileId);
                CreateUserSecurityProfile(_userId, secureFieldsSecurityProfileId);
            }

            if (_userId == Guid.Empty)
                _userId = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_userId, DateTime.Now.Date);
            dbHelper.systemUser.UpdateEmployeeTypeId(_userId, 4);
            TimeZone localZone = TimeZone.CurrentTimeZone;
            dbHelper.systemUser.UpdateSystemUserTimezone(_userId, localZone.StandardName);

            return _userId;

            #endregion
        }

        public Guid CreateSystemUserRecord(string username, string firstName, string lastName, string password, Guid businessUnitId, Guid teamId, Guid _languageId, Guid _authenticationproviderid, List<Guid> SecurityProfiles)
        {
            #region System User

            Guid _userId = Guid.Empty;

            if (!dbHelper.systemUser.GetSystemUserByUserName(username).Any())
                _userId = dbHelper.systemUser.CreateSystemUser(username, firstName, lastName, firstName + " " + lastName, password, username + "@somemail.com", username + "@otheremail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, businessUnitId, teamId, DateTime.Now.Date);

            if (_userId == Guid.Empty)
                _userId = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();

            foreach (var SecurityProfileId in SecurityProfiles)
                CreateUserSecurityProfile(_userId, SecurityProfileId);

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_userId, DateTime.Now.Date);
            dbHelper.systemUser.UpdateEmployeeTypeId(_userId, 1);
            dbHelper.systemUser.UpdateSystemUserTimezone(_userId, TimeZone.CurrentTimeZone.StandardName);

            return _userId;

            #endregion
        }

        public string UpdateSystemUserLastPasswordChange(string username, string dataEncoded)
        {
            if (dataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(username);
                username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            var userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
            dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);
            return username;
        }

        public Guid CreateSystemUserRecord(Guid SystemUserId, string username, string firstName, string lastName, string password, Guid businessUnitId, Guid teamId, Guid _languageId, Guid _authenticationproviderid)
        {
            #region System User

            Guid _userId = Guid.Empty;

            if (!dbHelper.systemUser.GetSystemUserByUserName(username).Any())
            {
                _userId = dbHelper.systemUser.CreateSystemUser(SystemUserId, username, firstName, lastName, firstName + " " + lastName, password, username + "@somemail.com", username + "@otheremail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, businessUnitId, teamId, DateTime.Now.Date);
                var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                CreateUserSecurityProfile(_userId, systemAdministratorSecurityProfileId);
            }

            if (_userId == Guid.Empty)
                _userId = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();

            dbHelper.systemUser.UpdateLastPasswordChangedDate(_userId, DateTime.Now.Date);

            return _userId;

            #endregion
        }

        public Guid CreateUserSecurityProfile(Guid userId, Guid securityProfileId)
        {
            #region System User Security Profile

            if (!dbHelper.userSecurityProfile.GetByUserIDAndProfileId(userId, securityProfileId).Any())
                return dbHelper.userSecurityProfile.CreateUserSecurityProfile(userId, securityProfileId);

            return dbHelper.userSecurityProfile.GetByUserIDAndProfileId(userId, securityProfileId).First();

            #endregion
        }

        public Guid CreateChildProtectionCategoryOfAbuse(string RecordName, Guid OwnerId, string Code, string GovCode, DateTime StartDate)
        {
            if (!dbHelper.childProtectionCategoryOfAbuse.GetByName(RecordName).Any())
                dbHelper.childProtectionCategoryOfAbuse.CreateChildProtectionCategoryOfAbuse(OwnerId, RecordName, StartDate, Code, GovCode);

            return dbHelper.childProtectionCategoryOfAbuse.GetByName(RecordName)[0];
        }

        public Guid CreateChildProtectionStatusType(string RecordName, Guid OwnerId, DateTime StartDate, bool EndDateMandatory = true, bool EndReasonMandatory = true, bool Section47FormRequired = false, bool IsDefault = false)
        {
            if (!dbHelper.childProtectionStatusType.GetByName(RecordName).Any())
                dbHelper.childProtectionStatusType.CreateChildProtectionStatusType(OwnerId, RecordName, StartDate, EndDateMandatory, EndReasonMandatory, Section47FormRequired, IsDefault);

            return dbHelper.childProtectionStatusType.GetByName(RecordName)[0];
        }

        public Guid CreateProductLanguage(string LanguageName, string culturename, string currencysymbol, int lcid)
        {
            if (!dbHelper.productLanguage.GetProductLanguageIdByName(LanguageName).Any())
                dbHelper.productLanguage.CreateProductLanguage(LanguageName, culturename, currencysymbol, lcid);

            return dbHelper.productLanguage.GetProductLanguageIdByName(LanguageName)[0];
        }

        public Guid CreateTeam(string Name, Guid? TeamManagerId, Guid OwningBusinessUnitId, string Code, string EmailAddress, string Description, string PhoneNumber)
        {
            if (!dbHelper.team.GetTeamIdByName(Name).Any())
                dbHelper.team.CreateTeam(Name, TeamManagerId, OwningBusinessUnitId, Code, EmailAddress, Description, PhoneNumber);

            return dbHelper.team.GetTeamIdByName(Name)[0];

        }

        public Guid CreateTeam(Guid TeamId, string Name, Guid? TeamManagerId, Guid OwningBusinessUnitId, string Code, string EmailAddress, string Description, string PhoneNumber)
        {
            if (!dbHelper.team.GetTeamIdByName(Name).Any())
                dbHelper.team.CreateTeam(TeamId, Name, TeamManagerId, OwningBusinessUnitId, Code, EmailAddress, Description, PhoneNumber);

            return dbHelper.team.GetTeamIdByName(Name)[0];

        }

        public Guid CreateBusinessUnit(string Name)
        {
            if (!dbHelper.businessUnit.GetByName(Name).Any())
                dbHelper.businessUnit.CreateBusinessUnit(Name);
            return dbHelper.businessUnit.GetByName(Name)[0];
        }

        public Guid CreateLACLegalStatusReason(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate, bool ApplicableToStartedLookedAfter, bool ValidForContinuingEpisodeOfCare)
        {
            if (!dbHelper.lacLegalStatusReason.GetByName(Name).Any())
                dbHelper.lacLegalStatusReason.CreateLACLegalStatusReason(ownerid, Name, Code, GovCode, StartDate, ApplicableToStartedLookedAfter, ValidForContinuingEpisodeOfCare);

            return dbHelper.lacLegalStatusReason.GetByName(Name)[0];
        }

        public Guid CreateLACLegalStatus(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate, int StatusTypeId)
        {
            if (!dbHelper.lacLegalStatus.GetByName(Name).Any())
                dbHelper.lacLegalStatus.CreateLACLegalStatus(ownerid, Name, Code, GovCode, StartDate, StatusTypeId);

            return dbHelper.lacLegalStatus.GetByName(Name)[0];
        }

        public Guid CreateLACPlacement(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate)
        {
            if (!dbHelper.lacPlacement.GetByName(Name).Any())
                dbHelper.lacPlacement.CreateLACPlacement(ownerid, Name, Code, GovCode, StartDate);

            return dbHelper.lacPlacement.GetByName(Name)[0];
        }

        public Guid CreateLACLocationCode(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate)
        {
            if (!dbHelper.lacLocationCode.GetByName(Name).Any())
                dbHelper.lacLocationCode.CreateLACLocationCode(ownerid, Name, Code, GovCode, StartDate);

            return dbHelper.lacLocationCode.GetByName(Name)[0];
        }

        public Guid CreateLACPlacementProvider(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate)
        {
            if (!dbHelper.lacPlacementProvider.GetByName(Name).Any())
                dbHelper.lacPlacementProvider.CreateLACPlacementProvider(ownerid, Name, Code, GovCode, StartDate);

            return dbHelper.lacPlacementProvider.GetByName(Name)[0];
        }

        public Guid CreateLACLegalStatusEndReason(Guid ownerid, string Name, string Code, string GovCode, DateTime StartDate, bool ApplicableToCeasedLookedAfter, bool POCContinuesSubsequentLAC)
        {
            if (!dbHelper.lacLegalStatusEndReason.GetByName(Name).Any())
                dbHelper.lacLegalStatusEndReason.CreateLACLegalStatusEndReason(ownerid, Name, Code, GovCode, StartDate, ApplicableToCeasedLookedAfter, POCContinuesSubsequentLAC);

            return dbHelper.lacLegalStatusEndReason.GetByName(Name)[0];
        }

        public Guid CreateProvider(string name, Guid ownerid, int providertypeid = 3, string email = "")
        {

            if (!dbHelper.provider.GetProviderByName(name).Any())
                dbHelper.provider.CreateProvider(name, ownerid, providertypeid);
            return dbHelper.provider.GetProviderByName(name)[0];
        }

        public Guid CreateProvider(string name, Guid ownerid, int providertypeid, bool enablescheduling)
        {

            if (!dbHelper.provider.GetProviderByName(name).Any())
                dbHelper.provider.CreateProvider(name, ownerid, providertypeid, enablescheduling);
            return dbHelper.provider.GetProviderByName(name)[0];
        }

        public Guid CreateProviderAllowableBookingTypes(Guid OwnerId, Guid ProviderId, Guid BookingTypeId, bool DefaultBookingType)
        {

            if (!dbHelper.providerAllowableBookingTypes.GetByBookingTypeAndProviderId(BookingTypeId, ProviderId).Any())
                dbHelper.providerAllowableBookingTypes.CreateProviderAllowableBookingTypes(OwnerId, ProviderId, BookingTypeId, DefaultBookingType);
            return dbHelper.providerAllowableBookingTypes.GetByBookingTypeAndProviderId(BookingTypeId, ProviderId).FirstOrDefault();
        }

        public Guid CreateServiceElement1(string name, Guid ownerid, DateTime startdate, int code, int whotopayid, int paymentscommenceid, List<Guid> ValidRateUnits, bool usedinfinance = true)
        {
            if (!dbHelper.serviceElement1.GetByCode(code).Any())
                dbHelper.serviceElement1.CreateServiceElement1(ownerid, name, startdate, code, whotopayid, paymentscommenceid, ValidRateUnits, usedinfinance);
            return dbHelper.serviceElement1.GetByCode(code)[0];
        }

        public Guid CreateServiceElement1(string name, Guid ownerid, DateTime startdate, int code, int whotopayid, int paymentscommenceid, List<Guid> ValidRateUnits, Guid DefaultRateUnitId, Guid? glcodeid = null, bool usedinfinance = true)
        {
            if (!dbHelper.serviceElement1.GetByCode(code).Any())
                dbHelper.serviceElement1.CreateServiceElement1(ownerid, name, startdate, code, whotopayid, paymentscommenceid, ValidRateUnits, DefaultRateUnitId, glcodeid, usedinfinance);
            return dbHelper.serviceElement1.GetByCode(code)[0];
        }

        public Guid CreateServiceElement1(Guid ownerid, string name, DateTime startdate, int code, int whotopayid, int paymentscommenceid, List<Guid> ValidRateUnits, Guid DefaultRateUnitId, Guid PaymentTypeCodeId, Guid ProviderBatchGroupingId, int AdjustedDays, Guid VatCodeId, bool usedinfinance = false, Guid? glcodeid = null)
        {
            if (!dbHelper.serviceElement1.GetByCode(code).Any())
                dbHelper.serviceElement1.CreateServiceElement1(ownerid, name, startdate, code, whotopayid, paymentscommenceid, ValidRateUnits, DefaultRateUnitId, PaymentTypeCodeId, ProviderBatchGroupingId, AdjustedDays, VatCodeId, usedinfinance, glcodeid);
            return dbHelper.serviceElement1.GetByCode(code)[0];
        }

        public Guid CreateServiceElement2(Guid ownerid, string name, DateTime startdate, int code)
        {
            if (!dbHelper.serviceElement2.GetByCode(code).Any())
                dbHelper.serviceElement2.CreateServiceElement2(ownerid, name, startdate, code);
            return dbHelper.serviceElement2.GetByCode(code)[0];
        }

        public Guid CreateServiceMapping(Guid ownerid, Guid ServiceElement1Id, Guid ServiceElement2Id)
        {
            if (!dbHelper.serviceMapping.GetByServiceElement1And2(ServiceElement1Id, ServiceElement2Id).Any())
                dbHelper.serviceMapping.CreateServiceMapping(ownerid, ServiceElement1Id, ServiceElement2Id);
            return dbHelper.serviceMapping.GetByServiceElement1And2(ServiceElement1Id, ServiceElement2Id)[0];
        }

        public Guid CreateServiceMapping(Guid ownerid, Guid ServiceElement1Id, Guid CareTypeId, bool inactive = false)
        {
            if (!dbHelper.serviceMapping.GetByServiceElement1AndCareType(ServiceElement1Id, CareTypeId).Any())
                dbHelper.serviceMapping.CreateServiceMapping(ownerid, ServiceElement1Id, CareTypeId, inactive);
            return dbHelper.serviceMapping.GetByServiceElement1AndCareType(ServiceElement1Id, CareTypeId)[0];
        }

        public Guid CreateEthnicity(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.ethnicity.GetEthnicityIdByName(Name).Any())
                dbHelper.ethnicity.CreateEthnicity(OwnerId, Name, StartDate);
            return dbHelper.ethnicity.GetEthnicityIdByName(Name)[0];
        }

        //public Guid CreateDocumentIfNeeded(string DocumentName, string ZipFileName)
        //{
        //    if (!dbHelper.document.GetDocumentByName(DocumentName).Any())
        //    {
        //        var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
        //        dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
        //    }

        //    var documentID = dbHelper.document.GetDocumentByName(DocumentName).First();

        //    dbHelper.document.UpdateStatus(documentID, 100000000); //Set the status to published 

        //    return documentID;

        //}

        //public void ImportDocumentRules(string ZipFileName)
        //{
        //    var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
        //    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
        //}

        //public void ImportFormula(string ZipFileName)
        //{
        //    var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
        //    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
        //}

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

        public Guid CreateFormCancellationReason(Guid OwnerId, Guid OwningBusinessUnitId, string Name, DateTime startdate, DateTime? enddate = null, bool inactive = false)
        {
            #region Form Cancellation Reason

            if (!dbHelper.formCancellationReason.GetByName(Name).Any())
                dbHelper.formCancellationReason.CreateFormCancellationReason(OwnerId, OwningBusinessUnitId, Name, startdate, enddate, inactive);
            return dbHelper.formCancellationReason.GetByName(Name).FirstOrDefault();

            #endregion
        }

        public Guid CreateStaffTrainingItem(Guid ownerid, string name, DateTime startdate, int trainingtypeid = 1)
        {
            if (!dbHelper.staffTrainingItem.GetStaffTrainingItemByName(name).Any())
                dbHelper.staffTrainingItem.CreateStaffTrainingItem(ownerid, name, startdate, trainingtypeid);
            return dbHelper.staffTrainingItem.GetStaffTrainingItemByName(name)[0];
        }

        public Guid CreateTrainingRequirement(string title, Guid ownerid, Guid courseTitleId, DateTime ValidFromDate, DateTime? ValidToDate, int? trainingRecurrenceId, int categoryId)
        {
            if (!dbHelper.trainingRequirement.GetTrainingRequirementByTrainingItem(courseTitleId, categoryId).Any())
                dbHelper.trainingRequirement.CreateTrainingRequirement(title, ownerid, courseTitleId, ValidFromDate, ValidToDate, trainingRecurrenceId, categoryId);
            return dbHelper.trainingRequirement.GetTrainingRequirementByTrainingItem(courseTitleId, categoryId)[0];
        }

        public Guid CreateAppointmentTypeIfNeeded(string RecordName, Guid OwnerId)
        {
            if (!dbHelper.appointmentType.GetByName(RecordName).Any())
                dbHelper.appointmentType.CreateAppointmentType(RecordName, DateTime.Now.Date.AddYears(-1), OwnerId);

            return dbHelper.appointmentType.GetByName(RecordName)[0];
        }

        public Guid CreateActivityPriority(Guid ActivityPriorityId, string Name, DateTime StartDate, Guid OwnerId)
        {
            var recordExists = dbHelper.activityPriority.GetActivityPriorityByID(ActivityPriorityId, "activitypriorityid".ToLower()).ContainsKey("activitypriorityid".ToLower());

            if (!recordExists)
                dbHelper.activityPriority.CreateActivityPriority(ActivityPriorityId, Name, StartDate, OwnerId);

            return ActivityPriorityId;
        }

        public Guid CreateActivityPriority(string Name, DateTime StartDate, Guid OwnerId)
        {
            var recordExists = dbHelper.activityPriority.GetByName(Name).Any();

            if (!recordExists)
                dbHelper.activityPriority.CreateActivityPriority(Name, StartDate, OwnerId);

            return dbHelper.activityPriority.GetByName(Name)[0];
        }

        public Guid CreateActivityCategory(Guid ActivityCategoryId, string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityCategory.GetActivityCategoryByID(ActivityCategoryId, "activitycategoryid").ContainsKey("activitycategoryid");

            if (!recordExists)
                dbHelper.activityCategory.CreateActivityCategory(ActivityCategoryId, Name, StartDate, OwnerId);

            return ActivityCategoryId;

        }

        public Guid CreateActivityCategory(string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityCategory.GetByName(Name).Any();
            if (!recordExists)
                dbHelper.activityCategory.CreateActivityCategory(Name, StartDate, OwnerId);

            return dbHelper.activityCategory.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateActivitySubCategory(Guid ActivitySubCategoryId, string Name, DateTime StartDate, Guid ActivityCategoryId, Guid OwnerId)
        {

            var recordExists = dbHelper.activitySubCategory.GetActivitySubCategoryByID(ActivitySubCategoryId, "ActivitySubCategoryId".ToLower()).ContainsKey("ActivitySubCategoryId".ToLower());

            if (!recordExists)
                dbHelper.activitySubCategory.CreateActivitySubCategory(ActivitySubCategoryId, Name, StartDate, ActivityCategoryId, OwnerId);

            return ActivitySubCategoryId;

        }

        public Guid CreateActivitySubCategory(string Name, DateTime StartDate, Guid ActivityCategoryId, Guid OwnerId)
        {

            var recordExists = dbHelper.activitySubCategory.GetByName(Name).Any();

            if (!recordExists)
                dbHelper.activitySubCategory.CreateActivitySubCategory(Name, StartDate, ActivityCategoryId, OwnerId);

            return dbHelper.activitySubCategory.GetByName(Name).FirstOrDefault();

        }

        public Guid CreateActivityOutcome(Guid ActivityOutcomeId, string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityOutcome.GetActivityOutcomeByID(ActivityOutcomeId, "ActivityOutcomeId".ToLower()).ContainsKey("ActivityOutcomeId".ToLower());

            if (!recordExists)
                dbHelper.activityOutcome.CreateActivityOutcome(ActivityOutcomeId, Name, StartDate, OwnerId);

            return ActivityOutcomeId;

        }

        public Guid CreateActivityOutcome(string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityOutcome.GetByName(Name).Any();

            if (!recordExists)
                dbHelper.activityOutcome.CreateActivityOutcome(Name, StartDate, OwnerId);

            return dbHelper.activityOutcome.GetByName(Name).FirstOrDefault();

        }

        public Guid CreateActivityReason(Guid PrimaryKeyId, string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityReason.GetActivityReasonByID(PrimaryKeyId, "ActivityReasonId".ToLower()).ContainsKey("ActivityReasonId".ToLower());

            if (!recordExists)
                dbHelper.activityReason.CreateActivityReason(PrimaryKeyId, Name, StartDate, OwnerId);

            return PrimaryKeyId;

        }

        public Guid CreateActivityReason(string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.activityReason.GetByName(Name).Any();

            if (!recordExists)
                dbHelper.activityReason.CreateActivityReason(Name, StartDate, OwnerId);

            return dbHelper.activityReason.GetByName(Name).FirstOrDefault();

        }

        public Guid CreateInpatientWardSpecialty(string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.inpatientWardSpecialty.GetByName(Name).Any();
            if (!recordExists)
                dbHelper.inpatientWardSpecialty.CreateInpatientWardSpecialty(Name, StartDate, OwnerId);

            return dbHelper.inpatientWardSpecialty.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateCarePhysicalLocation(string Name, DateTime StartDate, Guid OwnerId, List<int> value)
        {
            var recordExists = dbHelper.carePhysicalLocation.GetByName(Name).Any();

            if (!recordExists)
                dbHelper.carePhysicalLocation.CreateCarePhysicalLocation(Name, StartDate, OwnerId, value);

            return dbHelper.carePhysicalLocation.GetByName(Name)[0];
        }


        public Guid CreateInpatientBedType(string Name, DateTime StartDate, Guid OwnerId)
        {

            var recordExists = dbHelper.inpatientBedType.GetByName(Name).Any();
            if (!recordExists)
                dbHelper.inpatientBedType.CreateInpatientBedType(Name, StartDate, OwnerId);

            return dbHelper.inpatientBedType.GetByName(Name).FirstOrDefault();
        }

        //public Guid CreateWorkflowIfNeeded(string WorkflowName, string ZipFileName)
        //{
        //    if (!dbHelper.workflow.GetWorkflowByName(WorkflowName).Any())
        //    {
        //        var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
        //        dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
        //    }

        //    return dbHelper.workflow.GetWorkflowByName(WorkflowName).First();
        //}

        public Guid CreateFinanceClientCategory(Guid OwnerId, string Name, DateTime StartDate, string Code)
        {
            #region Data Restriction

            if (!dbHelper.financeClientCategory.GetByName(Name).Any())
                return dbHelper.financeClientCategory.CreateFinanceClientCategory(OwnerId, Name, StartDate, Code);

            return dbHelper.financeClientCategory.GetByName(Name)[0];

            #endregion
        }

        public Guid CreateServiceProvisionStartReason(Guid OwnerId, Guid OwningBusinessUnitId, string Name, DateTime StartDate, bool DefaultBrokerageStartReason = false)
        {
            #region Service Provision Start Reason

            if (!dbHelper.serviceProvisionStartReason.GetByName(Name).Any())
                return dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(OwnerId, OwningBusinessUnitId, Name, StartDate, DefaultBrokerageStartReason);
            return dbHelper.serviceProvisionStartReason.GetByName(Name)[0];

            #endregion
        }

        public Guid CreateServiceProvisionEndReason(Guid OwnerId, string Name, DateTime StartDate, bool DefaultBrokerageEndReason = false)
        {
            #region Service Provision End Reason

            if (!dbHelper.serviceProvisionEndReason.GetByName(Name).Any())
                return dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(OwnerId, Name, StartDate, DefaultBrokerageEndReason);
            return dbHelper.serviceProvisionEndReason.GetByName(Name)[0];

            #endregion
        }

        public Guid CreateGLCode(Guid OwnerId, Guid GLCodeLocationId, string Description, string ExpenditureCode, string IncomeCode, bool ExemptFromCharging = false)
        {
            #region GL Code

            if (!dbHelper.glCode.GetByDescription(Description).Any())
                dbHelper.glCode.CreateGLCode(OwnerId, GLCodeLocationId, Description, ExpenditureCode, IncomeCode, ExemptFromCharging);
            return dbHelper.glCode.GetByDescription(Description)[0];

            #endregion
        }

        public Guid CreateCareType(Guid ownerid, Guid OwningBusinessUnitId, string name, Int32 code, DateTime startdate)
        {
            #region GL Code

            if (!dbHelper.careType.GetByName(name).Any())
                return dbHelper.careType.CreateCareType(ownerid, OwningBusinessUnitId, name, code, startdate);
            return dbHelper.careType.GetByName(name)[0];

            #endregion
        }

        public Guid CreateAllowanceType(string name, int code, int govcode, DateTime startdate, Guid ownerid)
        {
            #region AllowanceType

            if (!dbHelper.cpAllowanceType.GetByName(name).Any())
                return dbHelper.cpAllowanceType.CreateCPAllowanceType(name, code, govcode, startdate, ownerid);
            return dbHelper.cpAllowanceType.GetByName(name)[0];

            #endregion
        }

        public Guid CreateAllowanceSetup(
            Guid cpallowancetypeid, int payeetypeid, bool allowglcodeupdating, DateTime startdate, bool enddatemandatory, bool taxableallowance,
            int ratetypeid, decimal rate, Guid carerrateunitid, bool fixedrate,
            int ruletypeid, int minimumdays, int cessationage, int adjusteddays, int advanceweeks,
            bool reclaimable, bool reclaimfunction, Guid ownerid, Guid carerbatchgroupingid)
        {
            #region AllowanceType

            if (!dbHelper.cpAllowanceSetup.GetByAllowanceType(cpallowancetypeid).Any())
                return dbHelper.cpAllowanceSetup.CreateCPAllowanceSetup(
                    cpallowancetypeid, payeetypeid, allowglcodeupdating, startdate, enddatemandatory, taxableallowance,
                    ratetypeid, rate, carerrateunitid, fixedrate,
                    ruletypeid, minimumdays, cessationage, adjusteddays, advanceweeks,
                    reclaimable, reclaimfunction, ownerid, carerbatchgroupingid);

            return dbHelper.cpAllowanceSetup.GetByAllowanceType(cpallowancetypeid)[0];

            #endregion
        }

        public Guid CreateAuthorisationLevel(Guid ownerid, Guid systemuserid, DateTime startdate,
            int financerecordid, decimal? amount, bool authoriseownrecords, bool forallservices, DateTime? enddate = null)
        {
            #region Authorisation Level

            if (!dbHelper.authorisationLevel.GetBySystemUserId(systemuserid, financerecordid).Any())
                return dbHelper.authorisationLevel.CreateAuthorisationLevel(ownerid, systemuserid, startdate, financerecordid, amount, authoriseownrecords, forallservices, enddate);
            return dbHelper.authorisationLevel.GetBySystemUserId(systemuserid, financerecordid)[0];

            #endregion
        }

        public Guid CreateCPFeeType(string name, DateTime startdate, int code, Guid ownerid)
        {
            #region CP Fee Type

            if (!dbHelper.cpFeeType.GetByName(name).Any())
                return dbHelper.cpFeeType.CreateCPFeeType(name, startdate, code, ownerid);
            return dbHelper.cpFeeType.GetByName(name)[0];

            #endregion
        }

        public Guid CreateCPFeeSetup(Guid cpfeetypeid, Guid ownerid, DateTime startdate, bool enddatemandatory, bool allowglupdating, bool taxablefee,
            int ratetypeid, Guid cpcarerrateunitid, decimal rate, bool fixedrate,
            int ruletypeid, int adjusteddays, int advanceweeks,
            bool reclaimable, bool reclaimfunction, Guid carerbatchgroupingid, bool usedincalculations)
        {
            #region CP Fee Setup

            if (!dbHelper.cpFeeSetup.GetByFeeType(cpfeetypeid).Any())
                return dbHelper.cpFeeSetup.CreateCPFeeSetup(cpfeetypeid, ownerid, startdate, enddatemandatory, allowglupdating, taxablefee,
                     ratetypeid, cpcarerrateunitid, rate, fixedrate,
                     ruletypeid, adjusteddays, advanceweeks,
                     reclaimable, reclaimfunction, carerbatchgroupingid, usedincalculations);
            return dbHelper.cpFeeSetup.GetByFeeType(cpfeetypeid)[0];

            #endregion
        }

        public Guid CreateBrokerageEpisodePriority(Guid PrimaryKeyId, string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.brokerageEpisodePriority.GetByName(Name).Any())
                dbHelper.brokerageEpisodePriority.CreateBrokerageEpisodePriority(PrimaryKeyId, Name, StartDate, OwnerId);
            return dbHelper.brokerageEpisodePriority.GetByName(Name)[0];
        }

        public Guid CreateBrokerageEpisodePriority(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.brokerageEpisodePriority.GetByName(Name).Any())
                dbHelper.brokerageEpisodePriority.CreateBrokerageEpisodePriority(Name, StartDate, OwnerId);
            return dbHelper.brokerageEpisodePriority.GetByName(Name)[0];
        }


        public Guid CreateBrokerageEpisodeRejectionReason(string Name, DateTime StartDate, Guid OwnerId, bool isOther = false, bool inactive = false)
        {
            if (!dbHelper.brokerageEpisodeRejectionReason.GetByName(Name).Any())
                dbHelper.brokerageEpisodeRejectionReason.CreateBrokerageEpisodeRejectionReason(Name, StartDate, OwnerId, isOther, inactive);
            return dbHelper.brokerageEpisodeRejectionReason.GetByName(Name)[0];

        }

        public Guid CreateBrokerageEpisodeRejectionReason(Guid PrimaryKeyId, string Name, DateTime StartDate, Guid OwnerId, bool isOther = false, bool inactive = false)
        {
            if (!dbHelper.brokerageEpisodeRejectionReason.GetByName(Name).Any())
                dbHelper.brokerageEpisodeRejectionReason.CreateBrokerageEpisodeRejectionReason(PrimaryKeyId, Name, StartDate, OwnerId, isOther, inactive);
            return dbHelper.brokerageEpisodeRejectionReason.GetByName(Name)[0];

        }

        public Guid CreateBrokerageOfferRejectionReason(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.brokerageOfferRejectionReason.GetByName(Name).Any())
                dbHelper.brokerageOfferRejectionReason.CreateBrokerageOfferRejectionReason(Name, StartDate, OwnerId);
            return dbHelper.brokerageOfferRejectionReason.GetByName(Name)[0];
        }

        public Guid CreateBrokerageOfferCancellationReason(string Name, DateTime StartDate, Guid OwnerId, bool Inactive = false, bool DefaultForEpisodeCancellation = false)
        {
            if (!dbHelper.brokerageOfferCancellationReason.GetByName(Name).Any())
                dbHelper.brokerageOfferCancellationReason.CreateBrokerageOfferCancellationReason(Name, StartDate, OwnerId, Inactive, DefaultForEpisodeCancellation);
            return dbHelper.brokerageOfferCancellationReason.GetByName(Name)[0];
        }

        public Guid CreateChargingRuleType(string name, Guid ownerid, DateTime StartDate)
        {
            if (!dbHelper.chargingRuleType.GetChargingRuleTypeByName(name).Any())
                return dbHelper.chargingRuleType.CreateChargingRuleType(name, ownerid, StartDate);
            return dbHelper.chargingRuleType.GetChargingRuleTypeByName(name)[0];
        }

        public Guid CreateChargingRuleSetup(Guid ownerid, Guid chargingruletypeid, int authorityid, DateTime startdate,
            int agefrom, int ageto,
            decimal singleminimumcapitallimit, decimal singlemaximumcapitallimit, decimal jointminimumcapitallimit, decimal jointmaximumcapitallimit,
            int chargerate, int foreachvalue)
        {
            if (!dbHelper.chargingRuleSetup.GetChargingRuleSetup(chargingruletypeid, authorityid).Any())
                return dbHelper.chargingRuleSetup.CreateChargingRuleSetup(ownerid, chargingruletypeid, authorityid, startdate,
                    agefrom, ageto,
                    singleminimumcapitallimit, singlemaximumcapitallimit, jointminimumcapitallimit, jointmaximumcapitallimit,
                    chargerate, foreachvalue);
            return dbHelper.chargingRuleSetup.GetChargingRuleSetup(chargingruletypeid, authorityid)[0];
        }

        public Guid CreateIncomeSupportTypeChargingRuleType(Guid IncomeSupportTypeId, Guid ChargingRuleTypeId)
        {
            if (!dbHelper.incomeSupportTypeChargingRuleTypes.GetByIncomeSupportTypeAndChargingRule(IncomeSupportTypeId, ChargingRuleTypeId).Any())
                return dbHelper.incomeSupportTypeChargingRuleTypes.CreateIncomeSupportTypeChargingRuleTypes(IncomeSupportTypeId, ChargingRuleTypeId);
            return dbHelper.incomeSupportTypeChargingRuleTypes.GetByIncomeSupportTypeAndChargingRule(IncomeSupportTypeId, ChargingRuleTypeId)[0];
        }

        public Guid CreateIncomeSupportSetup(Guid ownerid, Guid incomesupporttypeid,
            DateTime startdate, DateTime? enddate, int agefrom, int ageto,
            decimal amount, decimal jointamount, decimal minimumguaranteeamount, decimal jointminimumguaranteeamount)
        {
            if (!dbHelper.incomeSupportSetup.GetIncomeSupportSetup(incomesupporttypeid).Any())
                return dbHelper.incomeSupportSetup.CreateIncomeSupportSetup(ownerid, incomesupporttypeid,
                    startdate, enddate, agefrom, ageto,
                    amount, jointamount, minimumguaranteeamount, jointminimumguaranteeamount);
            return dbHelper.incomeSupportSetup.GetIncomeSupportSetup(incomesupporttypeid)[0];
        }

        public Guid CreateScheduleSetup(Guid ownerid, Guid chargingruletypeid, Guid scheduletypeid,
            DateTime startdate, DateTime? enddate, int assessmentcategoryid, bool rounddowncharge, bool calculatesavingscredit, int adjusteddays, int percentageallocation, bool showcostonschedule,
            bool contributionpersoncharge, bool contributionnonpersoncharge,
            bool suspendchargeincrease)
        {
            if (!dbHelper.scheduleSetup.GetScheduleSetup(chargingruletypeid, scheduletypeid).Any())
                return dbHelper.scheduleSetup.CreateScheduleSetup(ownerid, chargingruletypeid, scheduletypeid,
                    startdate, enddate, assessmentcategoryid, rounddowncharge, calculatesavingscredit, adjusteddays, percentageallocation, showcostonschedule,
                    contributionpersoncharge, contributionnonpersoncharge,
                    suspendchargeincrease);
            return dbHelper.scheduleSetup.GetScheduleSetup(chargingruletypeid, scheduletypeid)[0];
        }

        public Guid CreateChargeforServicesSetup(Guid ownerid, Guid chargingruletypeid, Guid serviceelement1id, Guid serviceelement2id, Guid? financeclientcategoryid, Guid rateunitid, DateTime startdate)
        {
            if (!dbHelper.chargeforServicesSetup.GetChargeforServicesSetup(chargingruletypeid, serviceelement1id, serviceelement2id).Any())
                return dbHelper.chargeforServicesSetup.CreateChargeforServicesSetup(ownerid, chargingruletypeid, serviceelement1id, serviceelement2id, financeclientcategoryid, rateunitid, startdate);
            return dbHelper.chargeforServicesSetup.GetChargeforServicesSetup(chargingruletypeid, serviceelement1id, serviceelement2id)[0];
        }

        public Guid CreateHealthAppointmentReason(Guid HealthAppointmentReasonid, Guid OwnerId, string Name, DateTime StartDate, string Code, string GovCode)
        {
            #region Health Appointment Reason

            var fields = dbHelper.healthAppointmentReason.GetByID(HealthAppointmentReasonid, dbHelper.healthAppointmentReason.PrimaryKeyName); ;

            if (!fields.ContainsKey(dbHelper.healthAppointmentReason.PrimaryKeyName.ToLower()))
                return dbHelper.healthAppointmentReason.CreateHealthAppointmentReason(HealthAppointmentReasonid, OwnerId, Name, StartDate, Code, GovCode);

            return HealthAppointmentReasonid;

            #endregion
        }

        public Guid CreateCaseFormOutcomeType(Guid OwnerId, Guid OwningBusinessUnitId, string Name, DateTime startdate, DateTime? enddate, bool inactive = false, bool ApplicableToAllDocuments = true)
        {
            #region Case Form Outcome Type

            if (!dbHelper.caseFormOutcomeType.GetByName(Name).Any())
                return dbHelper.caseFormOutcomeType.CreateCaseFormOutcomeType(OwnerId, OwningBusinessUnitId, Name, startdate, enddate, inactive, ApplicableToAllDocuments);

            return dbHelper.caseFormOutcomeType.GetByName(Name).FirstOrDefault();

            #endregion
        }

        public Guid CreateAssessmentFactorType(Guid OwnerId, Guid OwningBusinessUnitId, string Name, DateTime startdate, DateTime? enddate, bool inactive = false)
        {
            #region Data Restriction
            Guid recordID = Guid.Empty;

            if (!dbHelper.assessmentFactorType.GetByName(Name).Any())
                dbHelper.assessmentFactorType.CreateAssessmentFactorType(OwnerId, OwningBusinessUnitId, Name, startdate, enddate, inactive);

            if (recordID != Guid.Empty)
                return recordID;

            return dbHelper.assessmentFactorType.GetByName(Name).FirstOrDefault();

            #endregion
        }

        public Guid CreateHealthAppointmentReason(Guid OwnerId, string Name, DateTime StartDate, string Code, string GovCode)
        {
            #region Health Appointment Reason

            if (!dbHelper.healthAppointmentReason.GetByName(Name).Any())
                return dbHelper.healthAppointmentReason.CreateHealthAppointmentReason(OwnerId, Name, StartDate, Code, GovCode);

            return dbHelper.healthAppointmentReason.GetByName(Name)[0];

            #endregion
        }

        public Guid CreateContactAdministrativeCategory(Guid OwnerId, string Name, DateTime StartDate)
        {
            #region Contact Administrative Category

            if (!dbHelper.contactAdministrativeCategory.GetByName(Name).Any())
                return dbHelper.contactAdministrativeCategory.CreateContactAdministrativeCategory(OwnerId, Name, StartDate);

            return dbHelper.contactAdministrativeCategory.GetByName(Name)[0];

            #endregion
        }

        public Guid CreateCaseServiceTypeRequested(Guid OwnerId, string Name, DateTime StartDate)
        {
            #region Case Service Type Requested

            if (!dbHelper.caseServiceTypeRequested.GetByName(Name).Any())
                return dbHelper.caseServiceTypeRequested.CreateCaseServiceTypeRequested(OwnerId, Name, StartDate);

            return dbHelper.caseServiceTypeRequested.GetByName(Name)[0];

            #endregion
        }

        public Guid CreateHealthAppointmentContactType(Guid OwnerId, string Name, DateTime StartDate, string contacttypeid)
        {
            #region Health Appointment Contact Type

            if (!dbHelper.healthAppointmentContactType.GetByName(Name).Any())
                dbHelper.healthAppointmentContactType.CreateHealthAppointmentContactType(OwnerId, Name, StartDate, contacttypeid);

            return dbHelper.healthAppointmentContactType.GetByName(Name)[0];

            #endregion
        }

        public Guid CreateHealthAppointmentLocationType(Guid OwnerId, string Name, DateTime StartDate)
        {
            #region Health Appointment Location Type

            if (!dbHelper.healthAppointmentLocationType.GetByName(Name).Any())
                dbHelper.healthAppointmentLocationType.CreateHealthAppointmentLocationType(OwnerId, Name, StartDate);

            return dbHelper.healthAppointmentLocationType.GetByName(Name)[0];

            #endregion
        }

        public Guid CreateCommunityAndClinicTeam(Guid Ownerid, Guid Providerid, Guid TeamId, string Title, string Comments)
        {
            #region Community And Clinic Team

            if (!dbHelper.communityAndClinicTeam.GetByTitle(Title).Any())
                dbHelper.communityAndClinicTeam.CreateCommunityAndClinicTeam(Ownerid, Providerid, TeamId, Title, Comments);

            return dbHelper.communityAndClinicTeam.GetByTitle(Title)[0];

            #endregion
        }

        public Guid CreateSignificantEventCategory(string significantEventName, DateTime startdate, Guid ownerid, string govcode, int? code, DateTime? enddate, bool useInChronology = false)
        {
            Guid _significantEventCategoryId = Guid.Empty;

            if (!dbHelper.significantEventCategory.GetByName(significantEventName).Any())
                dbHelper.significantEventCategory.CreateSignificantEventCategory(significantEventName, startdate, ownerid, code, govcode, enddate, useInChronology);
            _significantEventCategoryId = dbHelper.significantEventCategory.GetByName(significantEventName).FirstOrDefault();

            dbHelper.significantEventCategory.UpdateuseInChronology(_significantEventCategoryId, useInChronology);

            return _significantEventCategoryId;
        }

        public Guid CreateSignificantEventSubCategory(Guid ownerId, string significantEventSubCategoryName,
            Guid SignificantEventCategoryId, DateTime startdate, string code, string govcode, bool? enddate = null, bool validforexport = false, bool inactive = false)
        {
            if (!dbHelper.significantEventSubCategory.GetByName(significantEventSubCategoryName).Any())
                dbHelper.significantEventSubCategory.CreateSignificantEventSubCategory(ownerId, significantEventSubCategoryName, SignificantEventCategoryId, startdate, code, govcode);
            Guid _significantEventSubCategoryId = dbHelper.significantEventSubCategory.GetByName(significantEventSubCategoryName).FirstOrDefault();

            return _significantEventSubCategoryId;
        }

        public Guid CreateMaritalStatus(string Name, DateTime startdate, Guid Ownerid)
        {
            if (!dbHelper.maritalStatus.GetMaritalStatusIdByName(Name).Any())
                dbHelper.maritalStatus.CreateMaritalStatus(Name, startdate, Ownerid);

            return dbHelper.maritalStatus.GetMaritalStatusIdByName(Name)[0];

        }

        public Guid CreateCarePlanTypeId(string Name, DateTime startdate, Guid Ownerid)
        {
            if (!dbHelper.carePlanType.GetByName(Name).Any())
                dbHelper.carePlanType.CreateCarePlanTypeId(Name, startdate, Ownerid);

            return dbHelper.carePlanType.GetByName(Name)[0];

        }

        

        public Guid CreateContactPresentingPriority(Guid Ownerid, string Name)
        {
            if (!dbHelper.contactPresentingPriority.GetByName(Name).Any())
                dbHelper.contactPresentingPriority.CreateContactPresentingPriority(Ownerid, Name, null, null, new DateTime(2021, 10, 10));

            return dbHelper.contactPresentingPriority.GetByName(Name)[0];

        }

        public Guid CreateCasePriority(string Name, DateTime StartDate, int CasePriorityCategoryId, Guid OwnerId)
        {
            if (!dbHelper.casePriority.GetByName(Name).Any())
                dbHelper.casePriority.CreateCasePriority(Name, StartDate, CasePriorityCategoryId, OwnerId);

            return dbHelper.casePriority.GetByName(Name)[0];

        }

        public Guid CreateFosteringExperience(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.fosteringExperience.GetByName(Name).Any())
                dbHelper.fosteringExperience.CreateFosteringExperience(Name, StartDate, OwnerId);

            return dbHelper.fosteringExperience.GetByName(Name)[0];

        }

        public Guid CreateChildInNeedCode(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.childInNeedCode.GetByName(Name).Any())
                dbHelper.childInNeedCode.CreateChildInNeedCode(Name, StartDate, OwnerId);

            return dbHelper.childInNeedCode.GetByName(Name)[0];

        }

        public Guid CreateContactType(Guid OwnerId, string Name, DateTime StartDate, bool ValidForInitialContact)
        {
            if (!dbHelper.contactType.GetByName(Name).Any())
                dbHelper.contactType.CreateContactType(OwnerId, Name, StartDate, ValidForInitialContact);

            return dbHelper.contactType.GetByName(Name)[0];

        }

        public Guid CreateContactStatus(Guid OwnerId, string Name, string Code, DateTime StartDate, int CategoryId, bool ValidForReopening)
        {
            if (!dbHelper.contactStatus.GetByName(Name).Any())
                dbHelper.contactStatus.CreateContactStatus(OwnerId, Name, Code, StartDate, CategoryId, ValidForReopening);

            return dbHelper.contactStatus.GetByName(Name)[0];

        }

        public Guid CreateErrorManagementReason(string Name, DateTime StartDate, int ApplicableToId, Guid OwnerId, bool Inactive = false)
        {
            if (!dbHelper.errorManagementReason.GetByName(Name).Any())
                dbHelper.errorManagementReason.CreateErrorManagementReason(Name, StartDate, ApplicableToId, OwnerId, Inactive);

            return dbHelper.errorManagementReason.GetByName(Name)[0];

        }

        public Guid CreateCareProviderStaffRoleType(Guid ownerid, string name, string code, string govcode, DateTime StartDate, string description)
        {
            if (!dbHelper.careProviderStaffRoleType.GetByName(name).Any())
                return dbHelper.careProviderStaffRoleType.CreateCareProviderStaffRoleType(ownerid, name, code, govcode, StartDate, description);
            return dbHelper.careProviderStaffRoleType.GetByName(name)[0];

        }

        public Guid CreateTransportType(Guid OwnerId, string Name, DateTime StartDate, int traveltimecalculationid, string speed, int transporttypeiconid)
        {
            if (!dbHelper.transportType.GetTransportTypeByName(Name).Any())
                return dbHelper.transportType.CreateTransportType(OwnerId, Name, StartDate, traveltimecalculationid, speed, transporttypeiconid);
            return dbHelper.transportType.GetTransportTypeByName(Name)[0];
        }

        public Guid CreatePaymentTypeCode(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.paymentTypeCode.GetByName(Name).Any())
                dbHelper.paymentTypeCode.CreatePaymentTypeCode(Name, StartDate, OwnerId);

            return dbHelper.paymentTypeCode.GetByName(Name)[0];

        }

        public Guid CreateVATCode(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.vatCode.GetByName(Name).Any())
                dbHelper.vatCode.CreateVATCode(Name, StartDate, OwnerId);

            return dbHelper.vatCode.GetByName(Name)[0];

        }

        public Guid CreateProviderBatchGrouping(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.providerBatchGrouping.GetByName(Name).Any())
                dbHelper.providerBatchGrouping.CreateProviderBatchGrouping(Name, StartDate, OwnerId);

            return dbHelper.providerBatchGrouping.GetByName(Name)[0];

        }

        public Guid CreateFormDelayReason(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.formDelayReason.GetByName(Name).Any())
                dbHelper.formDelayReason.CreateFormDelayReason(Name, StartDate, OwnerId);

            return dbHelper.formDelayReason.GetByName(Name)[0];
        }

        public Guid CreateInpatientAdmissionSource(Guid OwnerId, string Name, DateTime StartDate)
        {
            var inpatientAdmissionSourceExists = dbHelper.inpatientAdmissionSource.GetByName(Name).Any();

            if (!inpatientAdmissionSourceExists)
                return dbHelper.inpatientAdmissionSource.CreateInpatientAdmissionSource(OwnerId, Name, StartDate);

            return dbHelper.inpatientAdmissionSource.GetByName(Name)[0];
        }

        public Guid CreateInpatientAdmissionMethod(string name, Guid ownerid, Guid OwningBusinessUnitId, DateTime startDate)
        {
            var inpatientAdmissionMethodExists = dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName(name).Any();
            if (!inpatientAdmissionMethodExists)
                dbHelper.inpatientAdmissionMethod.CreateAdmissionMethod(name, ownerid, OwningBusinessUnitId, startDate);
            return dbHelper.inpatientAdmissionMethod.GetAdmissionMethodByName(name)[0];
        }

        public Guid CreateCaseClosureReason(Guid ownerid, string Name, string LegacyId, string Code, DateTime StartDate, int BusinessTypeId, bool ValidForRejection, Guid? RTTTreatmentStatusId = null)
        {
            if (!dbHelper.caseClosureReason.GetByName(Name).Any())
                return dbHelper.caseClosureReason.CreateCaseClosureReason(ownerid, Name, LegacyId, Code, StartDate, BusinessTypeId, ValidForRejection, RTTTreatmentStatusId);
            return dbHelper.caseClosureReason.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateInpatientDischargeDestination(Guid ownerid, string Name, DateTime StartDate)
        {
            if (!dbHelper.inpatientDischargeDestination.GetByName(Name).Any())
                dbHelper.inpatientDischargeDestination.CreateInpatientDischargeDestination(ownerid, Name, StartDate);
            return dbHelper.inpatientDischargeDestination.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateCaseReopenReason(Guid ownerid, string Name, string Code, DateTime StartDate, int BusinessTypeId)
        {
            if (!dbHelper.caseReopenReason.GetByName(Name).Any())
                dbHelper.caseReopenReason.CreateCaseReopenReason(ownerid, Name, Code, StartDate, BusinessTypeId);
            return dbHelper.caseReopenReason.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateSystemSetting(string Name, string SettingValue, string Description, bool IsEncrypted, string EncryptedValue)
        {
            var systemSettingID = Guid.Empty;
            if (!dbHelper.systemSetting.GetSystemSettingIdByName(Name).Any())
            {
                systemSettingID = dbHelper.systemSetting.CreateSystemSetting(Name, SettingValue, Description, IsEncrypted, EncryptedValue);
            }
            else
            {
                systemSettingID = dbHelper.systemSetting.GetSystemSettingIdByName(Name).FirstOrDefault();
                dbHelper.systemSetting.UpdateSystemSettingValue(systemSettingID, SettingValue);
            }

            return systemSettingID;
        }

        public Guid CreateAttachDocumentType(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName(Name).Any())
                dbHelper.attachDocumentType.CreateAttachDocumentType(OwnerId, Name, StartDate);

            return dbHelper.attachDocumentType.GetAttachDocumentTypeIdByName(Name).FirstOrDefault();
        }

        public Guid CreateAttachDocumentSubType(Guid OwnerId, string Name, DateTime StartDate, Guid AttachDocumentTypeId)
        {
            if (!dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName(Name).Any())
                dbHelper.attachDocumentSubType.CreateAttachDocumentSubType(OwnerId, Name, StartDate, AttachDocumentTypeId);

            return dbHelper.attachDocumentSubType.GetAttachDocumentSubTypeIdByName(Name).FirstOrDefault();
        }

        public Guid CreateProviderComplaintFeedBackType(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.providerComplaintFeedBackType.GetProviderComplaintFeedBackTypeByName(Name).Any())
                return dbHelper.providerComplaintFeedBackType.CreateProviderComplaintFeedBackType(OwnerId, Name, StartDate);

            return dbHelper.providerComplaintFeedBackType.GetProviderComplaintFeedBackTypeByName(Name).FirstOrDefault();
        }

        public Guid CreateProviderComplaintStage(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.providerComplaintStage.GetProviderComplaintStageByName(Name).Any())
                return dbHelper.providerComplaintStage.CreateProviderComplaintStage(OwnerId, Name, StartDate);

            return dbHelper.providerComplaintStage.GetProviderComplaintStageByName(Name).FirstOrDefault();
        }

        public Guid CreateProviderComplaintOutcome(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.providerComplaintOutcome.GetProviderComplaintOutcomeByName(Name).Any())
                return dbHelper.providerComplaintOutcome.CreateProviderComplaintOutcome(OwnerId, Name, StartDate);

            return dbHelper.providerComplaintOutcome.GetProviderComplaintOutcomeByName(Name).FirstOrDefault();
        }

        public Guid CreateProviderComplaintNature(Guid OwnerId, string Name, DateTime StartDate, Guid ProviderComplaintFeedBackTypeId)
        {
            if (!dbHelper.providerComplaintNature.GetProviderComplaintNatureByName(Name).Any())
                return dbHelper.providerComplaintNature.CreateProviderComplaintNature(OwnerId, Name, StartDate, ProviderComplaintFeedBackTypeId);

            return dbHelper.providerComplaintNature.GetProviderComplaintNatureByName(Name).FirstOrDefault();
        }

        public Guid CreateDocumentOutcomeType(Guid outcomeid, Guid documentid)
        {
            #region Document Outcome Type            

            if (!dbHelper.documentOutcomeType.GetByOutcomeIdAndDocumentId(outcomeid, documentid).Any())
                return dbHelper.documentOutcomeType.CreateDocumentOutcomeType(outcomeid, documentid);

            return dbHelper.documentOutcomeType.GetByOutcomeIdAndDocumentId(outcomeid, documentid).FirstOrDefault();

            #endregion
        }

        public Guid CreateUserWorkSchedule(string title, Guid systemuserid, Guid ownerid, Guid recurrencepatternid, DateTime startdate, DateTime? enddate, TimeSpan starttime, TimeSpan endtime)
        {
            if (!dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(systemuserid).Any())
                return dbHelper.userWorkSchedule.CreateUserWorkSchedule(title, systemuserid, ownerid, recurrencepatternid, startdate, enddate, starttime, endtime);

            return dbHelper.userWorkSchedule.GetUserWorkScheduleByUserID(systemuserid).FirstOrDefault();
        }

        public Guid CreateTeamMember(Guid TeamId, Guid SystemUserId, DateTime StartDate, DateTime? EndDate)
        {
            #region Team Member            

            if (!dbHelper.teamMember.GetTeamMemberByUserAndTeamID(SystemUserId, TeamId).Any())
                return dbHelper.teamMember.CreateTeamMember(TeamId, SystemUserId, StartDate, EndDate);

            return dbHelper.teamMember.GetTeamMemberByUserAndTeamID(SystemUserId, TeamId).FirstOrDefault();

            #endregion
        }

        //public Guid CreateDocumentBusinessObjectMapping(string DocumentBusinessObjectMappingName, string ZipFileName)
        //{
        //    if (!dbHelper.documentBusinessObjectMapping.GetDocumentBusinessObjectMappingByTitle(DocumentBusinessObjectMappingName).Any())
        //    {
        //        var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
        //        dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
        //    }

        //    return dbHelper.documentBusinessObjectMapping.GetDocumentBusinessObjectMappingByTitle(DocumentBusinessObjectMappingName)[0];
        //}

        public Guid CreateRTTPathwaySetup(Guid OwnerId, string Name, DateTime StartDate, int firstappointmentnolaterthan, int warnafter, int breachoccursafter)
        {
            if (!dbHelper.rttPathwaySetup.GetByName(Name).Any())
                dbHelper.rttPathwaySetup.CreateRTTPathwaySetup(OwnerId, Name, StartDate, firstappointmentnolaterthan, warnafter, breachoccursafter);

            return dbHelper.rttPathwaySetup.GetByName(Name)[0];
        }

        public Guid CreateJobRoleType(Guid OwnerId, string Name, DateTime StartDate, int isconsultantid)
        {
            if (!dbHelper.jobRoleType.GetByName(Name).Any())
                dbHelper.jobRoleType.CreateJobRoleType(OwnerId, Name, StartDate, isconsultantid);

            return dbHelper.jobRoleType.GetByName(Name)[0];
        }

        public Guid CreateRTTTreatmentStatus(Guid OwnerId, Guid OwningBusinessUnitId, string Name, string GovCode, int RTTWaitingStatusId, DateTime StartDate, string Description)
        {
            if (!dbHelper.rttTreatmentStatus.GetByName(Name).Any())
                dbHelper.rttTreatmentStatus.CreateRTTTreatmentStatus(OwnerId, OwningBusinessUnitId, Name, GovCode, RTTWaitingStatusId, StartDate, Description);

            return dbHelper.rttTreatmentStatus.GetByName(Name)[0];
        }

        public Guid CreateProfessional(Guid OwnerId, Guid ProfessionTypeId, string Title, string FirstName, string LastName, string Email = "")
        {
            if (!dbHelper.professional.GetProfessionalIdByFirstName(FirstName).Any())
                dbHelper.professional.CreateProfessional(OwnerId, ProfessionTypeId, Title, FirstName, LastName, Email);

            return dbHelper.professional.GetProfessionalIdByFirstName(FirstName)[0];
        }

        public Guid CreateClinicalRiskFactorType(Guid OwnerId, string Name, DateTime StartDate, DateTime? EndDate)
        {
            if (!dbHelper.clinicalRiskFactorType.GetByName(Name).Any())
                dbHelper.clinicalRiskFactorType.CreateClinicalRiskFactorType(OwnerId, Name, StartDate, EndDate);

            return dbHelper.clinicalRiskFactorType.GetByName(Name)[0];
        }

        public Guid CreateClinicalRiskFactorSubType(Guid OwnerId, string Name, DateTime StartDate, Guid ClinicalRiskFactorTypeId)
        {
            if (!dbHelper.clinicalRiskFactorSubType.GetByName(Name).Any())
                dbHelper.clinicalRiskFactorSubType.CreateClinicalRiskFactorSubType(OwnerId, Name, StartDate, ClinicalRiskFactorTypeId);

            return dbHelper.clinicalRiskFactorSubType.GetByName(Name).FirstOrDefault();

        }

        public Guid CreateClinicalRiskfactorEndReason(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.clinicalRiskfactorEndReason.GetByName(Name).Any())
                dbHelper.clinicalRiskfactorEndReason.CreateClinicalRiskfactorEndReason(OwnerId, Name, StartDate);

            return dbHelper.clinicalRiskfactorEndReason.GetByName(Name)[0];
        }

        public Guid CreateClinicalRiskLevel(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.clinicalRiskLevel.GetByName(Name).Any())
                dbHelper.clinicalRiskLevel.CreateClinicalRiskLevel(OwnerId, Name, StartDate);

            return dbHelper.clinicalRiskLevel.GetByName(Name)[0];
        }

        public Guid CreateMHASectionSetup(string sectionName, string code, Guid ownerId, string description,
            bool informal = false, bool validforexport = false, int courtorderid = 1, int ministryofjusticerestrictionid = 1,
            bool section117entitlement = true, int medicalApproverCountId = 2, int? mhasectionlengthtypeid = null,
            bool allowplaceofextensions = false, bool allowrenewalbeforesectionend = false)
        {
            if (!dbHelper.mhaSectionSetup.GetMHASectionSetupByCode(code).Any())
                dbHelper.mhaSectionSetup.CreateMHASectionSetup(sectionName, code, ownerId, description, informal,
                    validforexport, courtorderid, ministryofjusticerestrictionid, section117entitlement,
                    medicalApproverCountId, mhasectionlengthtypeid, allowplaceofextensions, allowrenewalbeforesectionend);
            return dbHelper.mhaSectionSetup.GetMHASectionSetupByCode(code)[0];
        }

        public Guid CreateGestationPeriodEndReason(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.gestationPeriodEndReason.GetByName(Name).Any())
                dbHelper.gestationPeriodEndReason.CreateGestationPeriodEndReason(OwnerId, Name, StartDate, Inactive);

            return dbHelper.gestationPeriodEndReason.GetByName(Name)[0];

        }

        public Guid CreateClinicalRiskStatus(Guid OwnerId, string Name, DateTime StartDate, DateTime? EndDate = null)
        {
            if (!dbHelper.clinicalRiskStatus.GetByName(Name).Any())
                dbHelper.clinicalRiskStatus.CreateClinicalRiskStatus(OwnerId, Name, StartDate, EndDate);

            return dbHelper.clinicalRiskStatus.GetByName(Name)[0];
        }

        public Guid CreateHealthIssueTypeRecord(Guid OwnerId, String Name, int Inactive, int ValidForExport, DateTime StartDate, int? HealthIssueCategoryId = null)
        {
            if (!dbHelper.healthIssueType.GetByHealthIssueTypeName(Name).Any())
                dbHelper.healthIssueType.CreateHealthIssueTypeRecord(OwnerId, Name, Inactive, ValidForExport, StartDate, HealthIssueCategoryId);

            return dbHelper.healthIssueType.GetByHealthIssueTypeName(Name)[0];
        }

        public Guid CreateDisabilityType(Guid OwnerId, string Name, DateTime StartDate, bool ApplicableForMHSDS = false, bool IsNoneKnown = false, bool ValidForExport = false, bool Inactive = false)
        {
            if (!dbHelper.disabilityType.GetByDisabilityTypeName(Name).Any())
                dbHelper.disabilityType.CreateDisabilityType(OwnerId, Name, StartDate, ApplicableForMHSDS, IsNoneKnown, ValidForExport, Inactive);

            return dbHelper.disabilityType.GetByDisabilityTypeName(Name)[0];
        }

        public Guid CreateImpairmentType(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.impairmentType.GetByName(Name).Any())
                dbHelper.impairmentType.CreateImpairmentType(OwnerId, Name, StartDate);

            return dbHelper.impairmentType.GetByName(Name)[0];
        }

        public Guid CreateImmunisationType(Guid OwnerId, string Name, DateTime StartDate, bool ValidForExport = false, bool Inactive = false)
        {
            if (!dbHelper.immunisationType.GetByName(Name).Any())
                dbHelper.immunisationType.CreateImmunisationType(OwnerId, Name, StartDate, ValidForExport, Inactive);

            return dbHelper.immunisationType.GetByName(Name)[0];
        }

        public Guid CreateProfessionType(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.professionType.GetByName(Name).Any())
                dbHelper.professionType.CreateProfessionType(OwnerId, Name, StartDate);

            return dbHelper.professionType.GetByName(Name)[0];
        }

        public Guid CreateExtractName(Guid OwnerId, String Name, DateTime StartDate, bool IsUsedInSupplierPayments, bool IsUsedInCarerPayments, bool IsUsedInDebtors)
        {
            if (!dbHelper.extractName.GetByName(Name).Any())
                dbHelper.extractName.CreateExtractName(OwnerId, Name, StartDate, IsUsedInSupplierPayments, IsUsedInCarerPayments, IsUsedInDebtors);

            return dbHelper.extractName.GetByName(Name)[0];
        }

        public Guid CreateInvoiceBy(Guid OwnerId, string Name, DateTime StartDate, bool IsUsedInSupplierPayments, bool IsUsedInCarerPayments, bool IsUsedInDebtors, bool ValidForExport = false, bool Inactive = false)
        {
            if (!dbHelper.invoiceBy.GetByName(Name).Any())
                dbHelper.invoiceBy.CreateInvoiceBy(OwnerId, Name, StartDate, IsUsedInSupplierPayments, IsUsedInCarerPayments, IsUsedInDebtors, ValidForExport, Inactive);

            return dbHelper.invoiceBy.GetByName(Name)[0];
        }

        public Guid CreateStaffRecruitmentItem(Guid ownerid, string Name, DateTime StartDate, int? compliancerecurrenceid = null)
        {
            if (!dbHelper.staffRecruitmentItem.GetByName(Name).Any())
                dbHelper.staffRecruitmentItem.CreateStaffRecruitmentItem(ownerid, Name, StartDate, compliancerecurrenceid);

            return dbHelper.staffRecruitmentItem.GetByName(Name)[0];
        }

        public Guid CreateServicePackageType(Guid OwnerId, string Name, DateTime StartDate, DateTime? EndDate = null, bool inactive = false, bool validforexport = false)
        {
            if (!dbHelper.servicePackageType.GetByName(Name).Any())
                dbHelper.servicePackageType.CreateServicePackageType(OwnerId, Name, StartDate, EndDate, inactive, validforexport);
            return dbHelper.servicePackageType.GetByName(Name)[0];
        }

        public Guid CreateRecurrencePattern(string Title, int RecurrencePatternTypeId, int RecurrenceDay)
        {
            if (!dbHelper.recurrencePattern.GetByTitle(Title).Any())
                return dbHelper.recurrencePattern.CreateRecurrencePattern(RecurrencePatternTypeId, RecurrenceDay);
            return dbHelper.recurrencePattern.GetByTitle(Title).FirstOrDefault();
        }

        public Guid CreateAlertAndHazardType(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.alertAndHazardType.GetByName(Name).Any())
                return dbHelper.alertAndHazardType.CreateAlertAndHazardType(OwnerId, Name, StartDate, Inactive);

            return dbHelper.alertAndHazardType.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateAlertAndHazardEndReason(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.alertAndHazardEndReason.GetByName(Name).Any())
                return dbHelper.alertAndHazardEndReason.CreateAlertAndHazardEndReason(OwnerId, Name, StartDate, Inactive);

            return dbHelper.alertAndHazardEndReason.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateAlertAndHazardReviewOutcome(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.alertAndHazardReviewOutcome.GetByName(Name).Any())
                return dbHelper.alertAndHazardReviewOutcome.CreateAlertAndHazardReviewOutcome(OwnerId, Name, StartDate, Inactive);

            return dbHelper.alertAndHazardReviewOutcome.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateRejectedReason(string Name, Guid OwnerId, Guid OwningBusinessUnitId, DateTime StartDate)
        {
            if (!dbHelper.rejectedReason.GetByName(Name).Any())
                dbHelper.rejectedReason.CreateRejectedReason(Name, OwnerId, OwningBusinessUnitId, StartDate);
            return dbHelper.rejectedReason.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateCaseRejectedReason(string Name, string Code, Guid OwnerId, DateTime StartDate, Guid caseclosurereasonid, int businesstypeid)
        {
            if (!dbHelper.caseRejectedReason.GetByName(Name).Any())
                dbHelper.caseRejectedReason.CreateCaseRejectedReason(Name, Code, OwnerId, StartDate, caseclosurereasonid, businesstypeid);
            return dbHelper.caseRejectedReason.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateAdultSafeguardingCategoryOfAbuse(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.adultSafeguardingCategoryOfAbuse.GetByName(Name).Any())
                return dbHelper.adultSafeguardingCategoryOfAbuse.CreateAdultSafeguardingCategoryOfAbuse(OwnerId, Name, StartDate, Inactive);

            return dbHelper.adultSafeguardingCategoryOfAbuse.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateAdultSafeguardingStatus(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.adultSafeguardingStatus.GetByName(Name).Any())
                return dbHelper.adultSafeguardingStatus.CreateAdultSafeguardingStatus(OwnerId, Name, StartDate, Inactive);

            return dbHelper.adultSafeguardingStatus.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateAllegationCategory(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.allegationCategory.GetByName(Name).Any())
                return dbHelper.allegationCategory.CreateAllegationCategory(OwnerId, Name, StartDate, Inactive);

            return dbHelper.allegationCategory.GetByName(Name).FirstOrDefault();
        }

        public Guid CreatePrimarySupportReasonType(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.primarySupportReasonType.GetByName(Name).Any())
                return dbHelper.primarySupportReasonType.CreatePrimarySupportReasonType(OwnerId, Name, StartDate, Inactive);

            return dbHelper.primarySupportReasonType.GetByName(Name).FirstOrDefault();
        }

        public Guid CreatePersonFormOutcomeType(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.personFormOutcomeType.GetByName(Name).Any())
                return dbHelper.personFormOutcomeType.CreatePersonFormOutcomeType(OwnerId, Name, StartDate, Inactive);

            return dbHelper.personFormOutcomeType.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateFinanceGeneralSettingsIfNeeded(Guid OwnerId, int FinanceModuleId, bool IsFullFinancialYear, DateTime FinanceTransactionsUpTo)
        {
            if (!dbHelper.financeGeneralSettings.GetByOwnerIdAndFinanceModuleId(OwnerId, FinanceModuleId).Any())
                dbHelper.financeGeneralSettings.CreateFinanceGeneralSettings(OwnerId, FinanceModuleId, IsFullFinancialYear, FinanceTransactionsUpTo);

            return dbHelper.financeGeneralSettings.GetByOwnerIdAndFinanceModuleId(OwnerId, FinanceModuleId).FirstOrDefault();
        }

        public Guid CreateFinanceGeneralSettingsIfNeeded(Guid OwnerId, Guid BusinessUnitId, int FinanceModuleId, bool IsFullFinancialYear, DateTime FinanceTransactionsUpTo)
        {
            if (!dbHelper.financeGeneralSettings.GetByBusinessUnitIdAndFinanceModuleId(OwnerId, BusinessUnitId, FinanceModuleId).Any())
                dbHelper.financeGeneralSettings.CreateFinanceGeneralSettings(OwnerId, BusinessUnitId, FinanceModuleId, IsFullFinancialYear, FinanceTransactionsUpTo);

            return dbHelper.financeGeneralSettings.GetByBusinessUnitIdAndFinanceModuleId(OwnerId, BusinessUnitId, FinanceModuleId).FirstOrDefault();
        }

        public Guid CreateEmploymentContractType(Guid ownerid, string name, string code, string govcode, DateTime StartDate, int employmentContractTypePayCategoryId)
        {
            if (!dbHelper.employmentContractType.GetByName(name).Any())
                return dbHelper.employmentContractType.CreateEmploymentContractType(ownerid, name, code, govcode, StartDate, employmentContractTypePayCategoryId);
            return dbHelper.employmentContractType.GetByName(name)[0];
        }

        public Guid CreateAllergyType(Guid OwnerId, string Name, DateTime StartDate, bool Inactive = false)
        {
            if (!dbHelper.allergyType.GetByName(Name).Any())
                return dbHelper.allergyType.CreateAllergyType(OwnerId, Name, StartDate, Inactive);

            return dbHelper.allergyType.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateDiagnosis(Guid OwnerId, string Name, DateTime StartDate, bool validforkeyrisk)
        {
            if (!dbHelper.diagnosis.GetByName(Name).Any())
                return dbHelper.diagnosis.CreateDiagnosis(OwnerId, Name, StartDate, validforkeyrisk);

            return dbHelper.diagnosis.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateSystemUserSuspensionReason(Guid ownerId, string name, DateTime startDate)
        {
            if (!dbHelper.systemUserSuspensionReason.GetSystemUserSuspensionReasonByName(name).Any())
                return dbHelper.systemUserSuspensionReason.CreateSystemUserSuspensionReason(ownerId, name, startDate);
            return dbHelper.systemUserSuspensionReason.GetSystemUserSuspensionReasonByName(name)[0];
        }

        public Guid CreateLACReviewType(Guid ownerid, string name, DateTime startdate)
        {
            if (!dbHelper.lacReviewType.GetByName(name).Any())
                dbHelper.lacReviewType.CreateLACReviewType(ownerid, name, startdate);
            return dbHelper.lacReviewType.GetByName(name)[0];
        }

        public Guid CreateUserRestrictedDataAccess(Guid DataRestrictionID, Guid UserID, DateTime StartDate, DateTime? EndDate, Guid OwnerId)
        {
            #region Data Restriction
            Guid recordID = Guid.Empty;

            if (!dbHelper.userRestrictedDataAccess.GetByUserIDAndDataRestrictionID(DataRestrictionID, UserID).Any())
                dbHelper.userRestrictedDataAccess.CreateUserRestrictedDataAccess(DataRestrictionID, UserID, StartDate, EndDate, OwnerId);

            if (recordID != Guid.Empty)
                return recordID;

            return dbHelper.userRestrictedDataAccess.GetByUserIDAndDataRestrictionID(DataRestrictionID, UserID).FirstOrDefault();

            #endregion
        }

        //public void ImportMailMergeTemplate(string ZipFileName)
        //{
        //    var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
        //    dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
        //}

        public Guid CreateLanguage(string Name, Guid ownerid, string Code, string govcode, DateTime startdate, DateTime? enddate)
        {
            if (!dbHelper.language.GetLanguageIdByName(Name).Any())
                dbHelper.language.CreateLanguage(Name, ownerid, Code, govcode, startdate, enddate);
            return dbHelper.language.GetLanguageIdByName(Name)[0];
        }

        public Guid CreateLanguage(Guid PrimaryKeyId, string Name, Guid ownerid, string Code, string govcode, DateTime startdate, DateTime? enddate)
        {

            var fields = dbHelper.language.GetByID(PrimaryKeyId, dbHelper.language.PrimaryKeyName);
            if (!fields.ContainsKey(dbHelper.language.PrimaryKeyName.ToLower()))
                dbHelper.language.CreateLanguage(PrimaryKeyId, Name, ownerid, Code, govcode, startdate, enddate);

            return PrimaryKeyId;
        }

        public Guid CreateLanguageFluency(Guid PrimaryKeyId, string Name, Guid ownerid, DateTime startdate)
        {

            var fields = dbHelper.languageFluency.GetByID(PrimaryKeyId, dbHelper.languageFluency.PrimaryKeyName);
            if (!fields.ContainsKey(dbHelper.languageFluency.PrimaryKeyName.ToLower()))
                dbHelper.languageFluency.CreateLanguageFluency(PrimaryKeyId, Name, ownerid, startdate);

            return PrimaryKeyId;
        }

        public Guid CreateDuplicateDetectionRule(string Name, string Description, Guid RecordTypeId, bool Published, bool ExcludeInactiveMatchingRecords)
        {
            if (!dbHelper.duplicateDetectionRule.GetByName(Name).Any())
                dbHelper.duplicateDetectionRule.CreateDuplicateDetectionRule(Name, Description, RecordTypeId, Published, ExcludeInactiveMatchingRecords);
            return dbHelper.duplicateDetectionRule.GetByName(Name)[0];
        }

        public Guid CreateDuplicateDetectionCondition(string name, Guid duplicatedetectionruleid, int CriterionId, int? nofcharacters, bool IgnoreBlankValues, Guid BusinessObjectFieldId)
        {
            if (!dbHelper.duplicateDetectionCondition.GetByName(name, duplicatedetectionruleid).Any())
                dbHelper.duplicateDetectionCondition.CreateDuplicateDetectionCondition(name, duplicatedetectionruleid, CriterionId, nofcharacters, IgnoreBlankValues, BusinessObjectFieldId);
            return dbHelper.duplicateDetectionCondition.GetByName(name, duplicatedetectionruleid)[0];
        }

        public Guid CreateFinanceExtractSetupIfNeeded(Guid OwnerId, Guid BusinessUnitId, int FinanceModuleId,
            DateTime StartDate, TimeSpan StartTime, Guid ExtractNameId, int ExtractFrequencyId,
            bool Monday, bool Tuesday, bool Wednesday, bool Thursday, bool Friday, bool Saturday, bool Sunday,
            int FileFormatId = 1, bool ExcludeZeroTransactions = true, bool SeparateCreditExtract = false, bool ExtractCreditInvoices = true,
            string VatGlCode = "", string ExtractReference = "")
        {
            if (!dbHelper.financeExtractSetup.GetByBusinessUnitIdAndFinanceModuleId(OwnerId, BusinessUnitId, FinanceModuleId).Any())
                dbHelper.financeExtractSetup.CreateFinanceExtractSetup(OwnerId, BusinessUnitId, FinanceModuleId, StartDate, StartTime, ExtractNameId, ExtractFrequencyId,
                    Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, FileFormatId, ExcludeZeroTransactions, SeparateCreditExtract, ExtractCreditInvoices, VatGlCode, ExtractReference);

            return dbHelper.financeExtractSetup.GetByBusinessUnitIdAndFinanceModuleId(OwnerId, BusinessUnitId, FinanceModuleId).FirstOrDefault();
        }

        public Guid CreateEmploymentContractType(Guid ownerid, String name, string code, string govcode, DateTime StartDate)
        {
            if (!dbHelper.employmentContractType.GetByName(name).Any())
                dbHelper.employmentContractType.CreateEmploymentContractType(ownerid, name, code, govcode, StartDate);
            return dbHelper.employmentContractType.GetByName(name)[0];
        }

        public Guid CreateCareProviderContractScheme(Guid ownerid, Guid responsibleuserid, Guid owningbusinessunitid, string name, DateTime startdate, int code, Guid EstablishmentId, Guid FunderId)
        {
            if (!dbHelper.careProviderContractScheme.GetCareProviderContractSchemeByName(name).Any())
                dbHelper.careProviderContractScheme.CreateCareProviderContractScheme(ownerid, responsibleuserid, owningbusinessunitid, name, startdate, code, EstablishmentId, FunderId);
            return dbHelper.careProviderContractScheme.GetCareProviderContractSchemeByName(name)[0];
        }

        public Guid CreateAvailabilityType(string name, int precedence, bool validForExport, Guid ownerId,
            int diaryBookingsValidityId, int regularBookingsValidityId, bool countsTowardsPeriodWorked)
        {
            if (!dbHelper.availabilityTypes.GetAvailabilityTypeByName(name).Any())
                dbHelper.availabilityTypes.CreateAvailabilityType(name, precedence, validForExport, ownerId, diaryBookingsValidityId, regularBookingsValidityId, countsTowardsPeriodWorked);
            return dbHelper.availabilityTypes.GetAvailabilityTypeByName(name)[0];
        }

        public Guid CreateCareTask(Guid ownerid, Guid OwningBusinessUnitId, string name, Int32 code, DateTime startdate)
        {
            if (!dbHelper.careTask.GetByName(name).Any())
                dbHelper.careTask.CreateCareTask(ownerid, OwningBusinessUnitId, name, code, startdate);
            return dbHelper.careTask.GetByName(name)[0];

        }


        public Guid CreateCarePlanNeedDomain(Guid ownerid, string name, DateTime startdate)
        {
            if (!dbHelper.personCarePlanNeedDomain.GetByName(name).Any())
                dbHelper.personCarePlanNeedDomain.CreateCarePlanNeedDomain(ownerid, name, startdate);
            return dbHelper.personCarePlanNeedDomain.GetByName(name)[0];

        }

        public Guid CreateCarePlanAgreedBy(Guid ownerid, string name, DateTime startdate)
        {
            if (!dbHelper.carePlanAgreedBy.GetByName(name).Any())
                dbHelper.carePlanAgreedBy.CreateCarePlanAgreedBy(ownerid, name, startdate);
            return dbHelper.carePlanAgreedBy.GetByName(name)[0];

        }

        public Guid CreateMashStatus(string name, int mashstatuscategoryid, DateTime startdate, Guid ownerid)
        {
            if (!dbHelper.mashStatus.GetByName(name).Any())
                dbHelper.mashStatus.CreateMASHStatus(name, mashstatuscategoryid, startdate, ownerid);
            return dbHelper.mashStatus.GetByName(name)[0];
        }

        public Guid CreateProtectiveMarkingScheme(Guid ProtectiveMarkingSchemeId, string Name, Guid OwnerId, Guid OwningBusinessUnitId,
            bool? displaytext, bool? bold, bool? italic, bool? underline,
            int? textalignmentid, int? textlocationid, int? fontsizeid, int? fontstyleid, int? fontcolourid, bool? includesecurityheader, string pmsText = " ", bool inactive = false)
        {
            var recordExists = dbHelper.protectiveMarkingScheme.GetProtectiveMarkingSchemeById(ProtectiveMarkingSchemeId, "protectivemarkingschemeid").ContainsKey("protectivemarkingschemeid");
            if (!recordExists)
                dbHelper
                .protectiveMarkingScheme
                .CreateProtectiveMarkingScheme(ProtectiveMarkingSchemeId, Name, OwnerId, OwningBusinessUnitId,
                displaytext, bold, italic, underline, textalignmentid, textlocationid, fontsizeid, fontstyleid, fontcolourid, includesecurityheader, pmsText, inactive);

            return ProtectiveMarkingSchemeId;

        }

        public Guid CreateCareProviderService(Guid ownerid, string Name, DateTime startdate, int code)
        {
            if (!dbHelper.careProviderService.GetByName(Name).Any())
                dbHelper.careProviderService.CreateCareProviderService(ownerid, Name, startdate, code);
            return dbHelper.careProviderService.GetByName(Name)[0];
        }

        public Guid CreateCareProviderRateUnit(Guid ownerid, string Name, DateTime startdate, int code)
        {
            if (!dbHelper.careProviderRateUnit.GetByName(Name).Any())
                dbHelper.careProviderRateUnit.CreateCareProviderRateUnit(ownerid, Name, startdate, code);
            return dbHelper.careProviderRateUnit.GetByName(Name)[0];
        }

        public Guid CreateCareProviderBatchGrouping(Guid ownerid, string Name, DateTime startdate, int code)
        {
            if (!dbHelper.careProviderBatchGrouping.GetByName(Name).Any())
                dbHelper.careProviderBatchGrouping.CreateCareProviderBatchGrouping(ownerid, Name, startdate, code);
            return dbHelper.careProviderBatchGrouping.GetByName(Name)[0];
        }

        public Guid CreateCareProviderVatCode(Guid ownerid, string Name, DateTime startdate, int code)
        {
            if (!dbHelper.careProviderVatCode.GetByName(Name).Any())
                dbHelper.careProviderVatCode.CreateCareProviderVatCode(ownerid, Name, startdate, code);
            return dbHelper.careProviderVatCode.GetByName(Name)[0];
        }

        public Guid CreateCPBookingType(string name, int BookingTypeClassId, int Duration, TimeSpan DefaultStartTime, TimeSpan DefaultEndTime,

            int WorkingContractedTime, bool IsAbsence, int? capduration = null, DateTime? validfromdate = null, DateTime? validtodate = null, int? cpbookingchargetypeid = null)
        {
            if (!dbHelper.cpBookingType.GetByName(name).Any())
                return dbHelper.cpBookingType.CreateBookingType(name, BookingTypeClassId, Duration, DefaultStartTime, DefaultEndTime, WorkingContractedTime, IsAbsence, capduration, validfromdate, validtodate, cpbookingchargetypeid);

            return dbHelper.cpBookingType.GetByName(name).First();
        }

        public Guid CreateCPBookingType(string name, int BookingTypeClassId, int Duration, TimeSpan DefaultStartTime, TimeSpan DefaultEndTime,
            int WorkingContractedTime, bool IsAbsence, bool istraining, bool AssumeStaffAvailable, bool OpenEndedAllowed, bool AnnualLeave, bool isserviceusertraining)
        {
            if (!dbHelper.cpBookingType.GetByName(name).Any())
                return dbHelper.cpBookingType.CreateBookingType(name, BookingTypeClassId, Duration, DefaultStartTime, DefaultEndTime,
                    WorkingContractedTime, IsAbsence, istraining, AssumeStaffAvailable, OpenEndedAllowed, AnnualLeave, isserviceusertraining);

            return dbHelper.cpBookingType.GetByName(name).First();
        }

        public Guid CreateCareProviderPersonContractEndReason(string Name, DateTime startdate, int code, Guid ownerid, bool inactive)
        {
            if (!dbHelper.careProviderPersonContractEndReason.GetByName(Name).Any())
                return dbHelper.careProviderPersonContractEndReason.CreateCareProviderPersonContractEndReason(Name, startdate, code, ownerid, inactive);

            return dbHelper.careProviderPersonContractEndReason.GetByName(Name).First();
        }

        public Guid CreateCPPersonAbsenceReason(Guid ownerid, string Name, DateTime startdate, int code)
        {
            if (!dbHelper.cpPersonAbsenceReason.GetByName(Name).Any())
                dbHelper.cpPersonAbsenceReason.CreateCPPersonAbsenceReason(ownerid, Name, startdate, code);
            return dbHelper.cpPersonAbsenceReason.GetByName(Name)[0];
        }

        public Guid CreateHealthAppointmentOutcomeType(Guid ownerid, string Name, DateTime StartDate, string LegacyId = "", string Code = "", Guid? RTTTreatmentStatusId = null)
        {
            if (!dbHelper.healthAppointmentOutcomeType.GetByName(Name).Any())
                return dbHelper.healthAppointmentOutcomeType.CreateHealthAppointmentOutcomeType(ownerid, Name, StartDate, LegacyId, Code, RTTTreatmentStatusId);

            return dbHelper.healthAppointmentOutcomeType.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateCareProviderPersonContractServiceEndReason(Guid ownerid, string Name, DateTime startdate, int code)
        {
            if (!dbHelper.careProviderPersonContractServiceEndReason.GetByName(Name).Any())
                dbHelper.careProviderPersonContractServiceEndReason.CreateCareProviderPersonContractServiceEndReason(ownerid, Name, startdate, code);
            return dbHelper.careProviderPersonContractServiceEndReason.GetByName(Name)[0];
        }

        public Guid CreateAddressBorough(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.addressBorough.GetAddressBoroughByName(Name).Any())
                dbHelper.addressBorough.CreateAddressBorough(Name, StartDate, OwnerId);

            return dbHelper.addressBorough.GetAddressBoroughByName(Name).FirstOrDefault();
        }

        public Guid CreateAddressWard(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.addressWard.GetAddressWardByName(Name).Any())
                dbHelper.addressWard.CreateAddressWard(Name, StartDate, OwnerId);

            return dbHelper.addressWard.GetAddressWardByName(Name).FirstOrDefault();
        }

        public Guid CreatePersonTargetGroup(Guid ownerid, Guid OwningBusinessUnitId, string Name, int Code, DateTime StartDate, DateTime? EndDate, bool Inactive = false, bool ValidForExport = false)
        {
            if (!dbHelper.personTargetGroup.GetByName(Name).Any())
                dbHelper.personTargetGroup.CreatePersonTargetGroup(ownerid, OwningBusinessUnitId, Name, Code, StartDate, EndDate, Inactive, ValidForExport);

            return dbHelper.personTargetGroup.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateAccommodationType(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.accommodationType.GetByName(Name).Any())
                dbHelper.accommodationType.CreateAccommodationType(Name, StartDate, OwnerId);

            return dbHelper.accommodationType.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateLowerSuperOutputArea(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.lowerSuperOutputArea.GetByName(Name).Any())
                dbHelper.lowerSuperOutputArea.CreateLowerSuperOutputArea(Name, StartDate, OwnerId);

            return dbHelper.lowerSuperOutputArea.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateModeOfCommunication(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.modeOfCommunication.GetByName(Name).Any())
                dbHelper.modeOfCommunication.CreateModeOfCommunication(Name, StartDate, OwnerId);

            return dbHelper.modeOfCommunication.GetByName(Name).FirstOrDefault();
        }

        public Guid CreatePersonDocumentFormat(Guid OwnerId, string Name, DateTime StartDate, int DocumentFormatTypeId = 2)
        {
            if (!dbHelper.personDocumentFormat.GetByName(Name).Any())
                dbHelper.personDocumentFormat.CreatePersonDocumentFormat(OwnerId, Name, StartDate, DocumentFormatTypeId);

            return dbHelper.personDocumentFormat.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateImmigrationStatus(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.immigrationStatus.GetByName(Name).Any())
                dbHelper.immigrationStatus.CreateImmigrationStatus(Name, StartDate, OwnerId);

            return dbHelper.immigrationStatus.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateReligion(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.religion.GetByName(Name).Any())
                dbHelper.religion.CreateReligion(Name, StartDate, OwnerId);

            return dbHelper.religion.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateCountry(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.country.GetByName(Name).Any())
                dbHelper.country.CreateCountry(Name, StartDate, OwnerId);

            return dbHelper.country.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateLeavingCareEligibility(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.leavingCareEligibility.GetByName(Name).Any())
                dbHelper.leavingCareEligibility.CreateLeavingCareEligibility(Name, StartDate, OwnerId);

            return dbHelper.leavingCareEligibility.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateUPNUnknownReason(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.upnUnknownReason.GetByName(Name).Any())
                dbHelper.upnUnknownReason.CreateUPNUnknownReason(Name, StartDate, OwnerId);

            return dbHelper.upnUnknownReason.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateSexualOrientation(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.sexualOrientation.GetByName(Name).Any())
                dbHelper.sexualOrientation.CreateSexualOrientation(Name, StartDate, OwnerId);

            return dbHelper.sexualOrientation.GetByName(Name).FirstOrDefault();
        }

        public Guid CreateSocialWorkerChangeReason(string Name, DateTime StartDate, Guid OwnerId)
        {
            if (!dbHelper.socialWorkerChangeReason.GetByName(Name).Any())
                dbHelper.socialWorkerChangeReason.CreateSocialWorkerChangeReason(Name, StartDate, OwnerId);

            return dbHelper.socialWorkerChangeReason.GetByName(Name)[0];
        }


        public Guid CreateBookingType(string name, int BookingTypeClassId, int Duration, TimeSpan DefaultStartTime, TimeSpan DefaultEndTime,

            int WorkingContractedTime, bool IsAbsence, int? capduration = null, DateTime? validfromdate = null, DateTime? validtodate = null, int? cpbookingchargetypeid = null)
        {
            if (!dbHelper.cpBookingType.GetByName(name).Any())
                dbHelper.cpBookingType.CreateBookingType(name, BookingTypeClassId, Duration, DefaultStartTime, DefaultEndTime, WorkingContractedTime, IsAbsence, capduration, validfromdate, validtodate, cpbookingchargetypeid);

            return dbHelper.cpBookingType.GetByName(name)[0];
        }


        public Guid CreateCPPersonAbsenceReason(string name, Guid ownerid, Guid owningbusinessunitid, int code, DateTime startdate)
        {
            if (!dbHelper.cPPersonAbsenceReason.GetByName(name).Any())
                dbHelper.cPPersonAbsenceReason.CreateCPPersonAbsenceReason(name, ownerid, owningbusinessunitid, code, startdate);

            return dbHelper.cPPersonAbsenceReason.GetByName(name)[0];
        }

        public Guid CreateCarePlanReviewOutcome(Guid CarePlanReviewOutcomeId, string Name, DateTime StartDate, DateTime? EndDate, Guid OwnerId)
        {
            if (!dbHelper.carePlanReviewOutcome.GetByName(Name).Any())
                dbHelper.carePlanReviewOutcome.CreateCarePlanReviewOutcome(CarePlanReviewOutcomeId, Name, StartDate, EndDate, OwnerId);

            return dbHelper.carePlanReviewOutcome.GetByName(Name)[0];
        }

        public Guid CreateCarePlanReviewOutcome(string Name, DateTime StartDate, DateTime? EndDate, Guid OwnerId)
        {
            if (!dbHelper.carePlanReviewOutcome.GetByName(Name).Any())
                dbHelper.carePlanReviewOutcome.CreateCarePlanReviewOutcome(Name, StartDate, EndDate, OwnerId);

            return dbHelper.carePlanReviewOutcome.GetByName(Name)[0];
        }

        //public Guid ImportSystemDashboardIfNeeded(string SystemDashboardName, string ZipFileName)
        //{
        //    if (!dbHelper.systemDashboard.GetSystemDashboardByName(SystemDashboardName).Any())
        //    {
        //        var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
        //        dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
        //    }

        //    return dbHelper.systemDashboard.GetSystemDashboardByName(SystemDashboardName).First();
        //}

        public Guid CreateInpatientConsultantEpisodeEndReason(Guid ownerid, string Name, DateTime startdate, int code, bool inactive = false, bool DefaultForConsultantChange = false, bool DefaultForDischarge = false)
        {
            if (!dbHelper.inpatientConsultantEpisodeEndReason.GetByName(Name).Any())
                dbHelper.inpatientConsultantEpisodeEndReason.CreateInpatientConsultantEpisodeEndReason(ownerid, Name, startdate, code, inactive, DefaultForConsultantChange, DefaultForDischarge);

            return dbHelper.inpatientConsultantEpisodeEndReason.GetByName(Name)[0];
        }

        public Guid CreateRTTTreatmentFunctionCode(Guid OwnerId, string Name, DateTime StartDate)
        {
            if (!dbHelper.rttTreatmentFunctionCode.GetByName(Name).Any())
                dbHelper.rttTreatmentFunctionCode.CreateRTTTreatmentFunctionCode(OwnerId, Name, StartDate);

            return dbHelper.rttTreatmentFunctionCode.GetByName(Name)[0];
        }

        //public Guid ImportUserDashboardIfNeeded(string UserDashboardName, string ZipFileName)
        //{
        //    if (!dbHelper.userDashboard.GetUserDashboardByName(UserDashboardName).Any())
        //    {
        //        var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
        //        dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
        //    }

        //    return dbHelper.userDashboard.GetUserDashboardByName(UserDashboardName).First();
        //}

        //public Guid ImportSummaryDashboardIfNeeded(string SummaryDashboardName, string ZipFileName)
        //{
        //    if (!dbHelper.businessObjectDashboard.GetBusinessObjectDashboardByName(SummaryDashboardName).Any())
        //    {
        //        var documentByteArray = fileIOHelper.ReadFileIntoByteArray(testContext.DeploymentDirectory + "\\" + ZipFileName);
        //        dbHelper.document.ImportDocumentUsingPlatformAPI(documentByteArray, ZipFileName);
        //    }

        //    return dbHelper.businessObjectDashboard.GetBusinessObjectDashboardByName(SummaryDashboardName).First();
        //}

        public Guid CreateCareProviderStaffRoleTypeGroup(Guid OwnerId, string Name, string Code, string GovCode, DateTime StartDate, string Description, bool Inactive = false)
        {
            if (!dbHelper.careProviderStaffRoleTypeGroup.GetByName(Name).Any())
                dbHelper.careProviderStaffRoleTypeGroup.CreateCareProviderStaffRoleTypeGroup(OwnerId, Name, Code, GovCode, StartDate, Description, Inactive);

            return dbHelper.careProviderStaffRoleTypeGroup.GetByName(Name)[0];
        }

        public Guid CreateCPBankHolidayChargingCalendar(Guid OwnerId, string Name, string Code)
        {
            if (!dbHelper.cpBankHolidayChargingCalendar.GetByName(Name).Any())
                dbHelper.cpBankHolidayChargingCalendar.CreateCPBankHolidayChargingCalendar(OwnerId, Name, Code);

            return dbHelper.cpBankHolidayChargingCalendar.GetByName(Name)[0];
        }

        public Guid CreateBankHoliday(string Name, DateTime HolidayDate, string Description)
        {
            if (!dbHelper.bankHoliday.GetByHolidayDate(HolidayDate).Any())
                dbHelper.bankHoliday.CreateBankHoliday(Name, HolidayDate, Description);

            return dbHelper.bankHoliday.GetByHolidayDate(HolidayDate)[0];
        }

        public Guid CreateStaffReviewSetup(string Name, Guid StaffReviewFormId, DateTime ValidFrom, string Description, bool AllBusinessUnits, bool AllRoles, bool UpdateStaffReviewItem, Guid BookingTypeId)
        {
            if (!dbHelper.staffReviewSetup.GetByName(Name).Any())
                dbHelper.staffReviewSetup.CreateStaffReviewSetup(Name, StaffReviewFormId, ValidFrom, Description, AllBusinessUnits, AllRoles, UpdateStaffReviewItem, BookingTypeId);

            return dbHelper.staffReviewSetup.GetByName(Name)[0];
        }

        public Guid CreateSystemUserEmploymentContract(Guid SystemUserId, DateTime StartDate, Guid CareProviderStaffRoleTypeId, Guid OwnerId, Guid EmploymentContractTypeId, decimal? ContractHoursPerWeek = null)
        {
            if (!dbHelper.systemUserEmploymentContract.GetBySystemUserId(SystemUserId).Any())
                dbHelper.systemUserEmploymentContract.CreateSystemUserEmploymentContract(SystemUserId, StartDate, CareProviderStaffRoleTypeId, OwnerId, EmploymentContractTypeId, ContractHoursPerWeek);

            return dbHelper.systemUserEmploymentContract.GetBySystemUserId(SystemUserId)[0];
        }

        public Guid CreatePronouns(Guid ownerId, Guid businessUnitId, string name, int? code, DateTime startDate, bool inactive = false, bool validforexport = true)
        {
            if (!dbHelper.pronouns.GetByName(name).Any())
                dbHelper.pronouns.CreatePronouns(ownerId, businessUnitId, name, code, startDate, inactive, validforexport);

            return dbHelper.pronouns.GetByName(name)[0];
        }

        public Guid CreateTrainingRequirementSetup(string RecordName, Guid TrainingItemId, DateTime ValidFromDate, DateTime? ValidToDate, bool AllRoles, int StatusId, List<Guid> CareProviderStaffRoleTypeIds)
        {
            if (!dbHelper.trainingRequirementSetup.GetByName(RecordName).Any())
                dbHelper.trainingRequirementSetup.CreateTrainingRequirementSetup(RecordName, TrainingItemId, ValidFromDate, ValidToDate, AllRoles, StatusId, CareProviderStaffRoleTypeIds);
            return dbHelper.trainingRequirementSetup.GetByName(RecordName)[0];
        }

        public Guid CreateRecruitmentRequirement(Guid ownerid, string RequirementName, Guid RequiredItemId, string requireditemidtablename, string requireditemidname, bool AllRoles, int InductionNumber, int? InductionStatusId, int AcceptanceNumber, int? AcceptanceStatusId, DateTime StartDate, DateTime? EndDate, List<Guid> CareProviderStaffRoleTypeIds)
        {
            var recruitmentRequirementId = Guid.Empty;

            if (!dbHelper.recruitmentRequirement.GetByName(RequirementName).Any())
                recruitmentRequirementId = dbHelper.recruitmentRequirement.CreateRecruitmentRequirement(ownerid, RequirementName, RequiredItemId, requireditemidtablename, requireditemidname, AllRoles, InductionNumber, InductionStatusId, AcceptanceNumber, AcceptanceStatusId, StartDate, EndDate, CareProviderStaffRoleTypeIds);

            if (recruitmentRequirementId == Guid.Empty)
            {
                recruitmentRequirementId = dbHelper.recruitmentRequirement.GetByName(RequirementName)[0];
                dbHelper.recruitmentRequirement.UpdateAllRoles(recruitmentRequirementId, AllRoles, CareProviderStaffRoleTypeIds);
            }

            return recruitmentRequirementId;
        }

        public Guid CreateRequirementLastChasedOutcome(string Name, Guid ownerid, DateTime StartDate, DateTime? EndDate = null)
        {
            if (!dbHelper.requirementLastChasedOutcome.GetRequirementLastChasedOutcomeByName(Name).Any())
                dbHelper.requirementLastChasedOutcome.CreateRequirementLastChasedOutcome(Name, ownerid, StartDate, EndDate);

            return dbHelper.requirementLastChasedOutcome.GetRequirementLastChasedOutcomeByName(Name)[0];
        }

        public Guid CreateDemographicsTitle(string Name, DateTime startdate, Guid ownerid)
        {
            if (!dbHelper.demographicsTitle.GetByName(Name).Any())
                dbHelper.demographicsTitle.CreateDemographicsTitle(Name, startdate, ownerid);

            return dbHelper.demographicsTitle.GetByName(Name)[0];
        }

        public Guid CreateSystemUserAliasType(Guid ownerId, Guid OwningBusinessUnitId, string Name, DateTime startdate)
        {
            if (!dbHelper.systemUserAliasType.GetSystemUserAliasesTypeByName(Name).Any())
                dbHelper.systemUserAliasType.CreateSystemUserAliasType(ownerId, OwningBusinessUnitId, Name, startdate);

            return dbHelper.systemUserAliasType.GetSystemUserAliasesTypeByName(Name)[0];
        }

        public Guid CreateNumericQuestion(string Question, string SubHeading)
        {
            if (!dbHelper.questionCatalogue.GetByQuestionName(Question).Any())
                dbHelper.questionCatalogue.CreateNumericQuestion(Question, SubHeading);

            return dbHelper.questionCatalogue.GetByQuestionName(Question)[0];
        }

        //method to create PaymentMethod
        public Guid CreatePaymentMethod(string Name, DateTime StartDate, Guid OwnerId, bool ValidForExport = true, bool Inactive = false)
        {
            if (!dbHelper.paymentMethod.GetByName(Name).Any())
                dbHelper.paymentMethod.CreatePaymentMethod(Name, StartDate, OwnerId, ValidForExport, Inactive);

            return dbHelper.paymentMethod.GetByName(Name)[0];
        }

    }
}