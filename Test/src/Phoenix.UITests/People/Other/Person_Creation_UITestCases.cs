using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.People
{
    [TestClass]
    public class Person_Creation_UITestCases : FunctionalTest
    {
        #region Properties

        private string _environmentName;
        private Guid _authenticationproviderid;
        private Guid _languageId;
        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _ethnicityId;
        private Guid _systemUserId;
        private string _systemUserName;
        private string _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {

            try
            {
                #region Environment 

                _environmentName = ConfigurationManager.AppSettings["EnvironmentName"];

                #endregion

                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                username = commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);
                var defaultSystemUserId = dbHelper.systemUser.GetSystemUserByUserName(username)[0];
                TimeZone localZone = TimeZone.CurrentTimeZone;
                dbHelper.systemUser.UpdateSystemUserTimezone(defaultSystemUserId, localZone.StandardName);
                var _adminUserFullname = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(defaultSystemUserId, "fullname")["fullname"];

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _businessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region Ethnicity

                _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

                #endregion

                #region Default System User

                _systemUserName = "PersonCreationUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "Person Creation", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }

        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-12834

        [TestProperty("JiraIssueID", "CDV6-12842")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Click on Save- Validate  the Notification Message.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Creation_UITestCases01()
        {

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Chase Sam")
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .TapSaveButton()
                .ValidateNotificationMessageVisibility(true)
                .ValidateNotificationMessageText("Some data is not correct. Please review the data in the Form.")
                .ValidateLastNameErrorLabelVisibility(true)
                .ValidateLastNameErrorLabelText("Please fill out this field.")
                .ValidateStatedGenderNotificationMessageVisibility(true)
                .ValidateStatedGenderNotificationMessageText("Please fill out this field.")
                .ValidateEthnicityNotificationMessageVisibility(true)
                .ValidateEthnicityNotificationMessageText("Please fill out this field.")
                .ValidateDOBNotificationMessageVisibility(true)
                .ValidateDOBNotificationMessageText("Please fill out this field.")
                .ValidateAddressTypeNotificationMessageVisibility(true)
                .ValidateAddressTypeNotificationMessageText("Please fill out this field.");

        }

        [DeploymentItem("Files\\FileToUpload.jpg")]
        [DeploymentItem("chromedriver.exe")]
        [TestProperty("JiraIssueID", "CDV6-12843")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Enter All the Madatory and Optional Field- Click on Save Button")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Creation_UITestCases02()
        {
            var personEmail = "CareDirectorAutomationTestingForPerson" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@mail.com";
            DateTime dob = new DateTime(2008, 1, 1);
            var startDateOfAddress = DateTime.Now.Date.AddYears(-1);

            Guid EthnicityId = commonMethodsDB.CreateEthnicity(_teamId, "White and Asian", new DateTime(2020, 1, 1));
            Guid MaritalStatusId = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid PropertyTypeId = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Care Home")[0];
            Guid AddressBoroughId = commonMethodsDB.CreateAddressBorough("Test_Borough", new DateTime(2020, 1, 2), _teamId);
            Guid AddressWardId = commonMethodsDB.CreateAddressWard("Test Ward2", new DateTime(2020, 1, 2), _teamId);
            Guid AccommodationTypeId = commonMethodsDB.CreateAccommodationType("Owner Occupier", new DateTime(2020, 1, 2), _teamId);
            Guid ProviderId = commonMethodsDB.CreateProvider("Healthify Provider", _teamId, 11);
            Guid LowerSuperOutputAreaId = commonMethodsDB.CreateLowerSuperOutputArea("Test_Lower1", new DateTime(2020, 1, 2), _teamId);
            Guid LanguageId = commonMethodsDB.CreateLanguage("English", _teamId, "", "", new DateTime(2000, 1, 1), null);
            Guid PrefferedContactMethodId = dbHelper.contactMethod.GetByName("Email").First();
            Guid ModeOfCommunicationId = commonMethodsDB.CreateModeOfCommunication("Phone", new DateTime(2020, 1, 2), _teamId);
            Guid PersonDocumentFormatId = commonMethodsDB.CreatePersonDocumentFormat(_teamId, "Easy Read", new DateTime(2020, 1, 2));
            Guid TargetGroupId = commonMethodsDB.CreatePersonTargetGroup(_teamId, _businessUnitId, "Target Group 1", 1234, DateTime.Now.Date, null);
            Guid ImmigrationStatusId = commonMethodsDB.CreateImmigrationStatus("Child has been an UASC", new DateTime(2020, 1, 2), _teamId);
            Guid ReligionId = commonMethodsDB.CreateReligion("Christian", new DateTime(2020, 1, 2), _teamId);
            Guid CountryId = commonMethodsDB.CreateCountry("British Indian Ocean Territory (the)", new DateTime(2020, 1, 2), _teamId);
            Guid LeavingCareEligibilityId = commonMethodsDB.CreateLeavingCareEligibility("Eligible", new DateTime(2020, 1, 2), _teamId);
            Guid UPNUnknownReasonId = commonMethodsDB.CreateUPNUnknownReason("Child is educated outside of England", new DateTime(2020, 1, 2), _teamId);
            Guid SexualOrientationId = commonMethodsDB.CreateSexualOrientation("Not Known", new DateTime(2020, 1, 2), _teamId);
            Guid NationalityId = dbHelper.nationality.GetNationalityByName("British Ind. OT.").First();

            #region Business Object

            var businessObjectId = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            #endregion

            #region Business Object Field

            var businessObjectFieldId_DateOfBirth = dbHelper.businessObjectField.GetBusinessObjectFieldByName("DateOfBirth", businessObjectId)[0];
            var businessObjectFieldId_FirstName = dbHelper.businessObjectField.GetBusinessObjectFieldByName("FirstName", businessObjectId)[0];

            #endregion

            #region Duplicate Detection Rule

            var duplicateDetectionRuleId = commonMethodsDB.CreateDuplicateDetectionRule("Person Duplication", "...", businessObjectId, true, true);

            #endregion

            #region Duplicate Detection Conditions

            int criterionid_SameDate = 1;
            var duplicateDetectionCondition1Id = commonMethodsDB.CreateDuplicateDetectionCondition("DateOfBirth Same Date", duplicateDetectionRuleId, criterionid_SameDate, null, true, businessObjectFieldId_DateOfBirth);
            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, true);

            int criterionid_ExactMatch = 3;
            var duplicateDetectionCondition2Id = commonMethodsDB.CreateDuplicateDetectionCondition("FirstName Exact Match", duplicateDetectionRuleId, criterionid_ExactMatch, null, true, businessObjectFieldId_FirstName);

            #endregion

            dbHelper.person.CreatePersonRecord("", "AutomationQAPersonCreation", "AutomationQAPersonCreation", "AutomationQAPersonCreation", "", dob, _ethnicityId, _teamId, 7, 2);


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Chase Sam")
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertTitle("Mr")
                .InsertFirstName("AutomationQAPersonCreation")
                .InsertMiddleName("AutomationQAPersonCreation")
                .InsertLastName("AutomationQAPersonCreation")
                .SelectStatedGender("Male")
                .InsertNHSNo("9876543210")
                .ClickImageUploadDocument(this.TestContext.DeploymentDirectory + "\\FileToUpload.jpg")
                .InsertDOB(dob.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectDOBAndAge("DOB")
                .ClickEthnicityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("White and Asian")
                .TapSearchButton()
                .SelectResultElement(EthnicityId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickMaritalStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Married")
                .TapSearchButton()
                .SelectResultElement(MaritalStatusId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertStartDateOfAddress(startDateOfAddress.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectAddressType("Home")
                .InsertPropertyName("Residential")
                .InsertPropertyNo("12")
                .InsertStreet("East Linda")
                .InsertVillageDistrict("Showlow")
                .InsertTownCity("CA")
                .InsertCounty("Maricopa")
                .InsertPostCode("85258")
                .InsertUPRN("12345")
                .ClickPropertyTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Care Home")
                .TapSearchButton()
                .SelectResultElement(PropertyTypeId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickBoroughLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Test_Borough")
                .TapSearchButton()
                .SelectResultElement(AddressBoroughId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickWardLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Test Ward2")
                .TapSearchButton()
                .SelectResultElement(AddressWardId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertCountry("United Kingdom")
                .SelectAccomodationStatus("Unsettled")
                .ClickAccomodationTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Owner Occupier")
                .TapSearchButton()
                .SelectResultElement(AccommodationTypeId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectLivesAlone("No")
                .ClickCCGBoundaryLookupButton();


            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Healthify Provider")
                .TapSearchButton()
                .SelectResultElement(ProviderId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickLowerSuperOutputAreaLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("TEst_Lower1")
                .TapSearchButton()
                .SelectResultElement(LowerSuperOutputAreaId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertBusinessPhone("9876543210")
                .InsertHomePhone("9876543210")
                .InsertMobilePhone("9876543210")
                .InsertPrimaryEmail(personEmail)
                .InsertSecondaryEmail(personEmail)
                .InsertBillingEmail(personEmail)
                .InsertTelephone1("9876543210")
                .InsertTelephone2("9876543210")
                .InsertTelephone3("9876543210")
                .ClickPreferredLanguageLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("English")
                .TapSearchButton()
                .SelectResultElement(LanguageId.ToString());


            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertPreferredName("Chase")
                .ClickPreferredContactMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Email")
                .TapSearchButton()
                .SelectResultElement(PrefferedContactMethodId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickPreferredModeOfCommunicationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Phone")
                .TapSearchButton()
                .SelectResultElement(ModeOfCommunicationId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectPreferredDay("Any Day")
                .SelectPreferredTime("Morning")
                .ClickDocumentFormatLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Easy Read")
                .TapSearchButton()
                .SelectResultElement(PersonDocumentFormatId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertNHSCardLocation("Online")
                .ClickTargetGroupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Target Group 1")
                .TapSearchButton()
                .SelectResultElement(TargetGroupId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickImmigrationStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Child has been an UASC")
                .TapSearchButton()
                .SelectResultElement(ImmigrationStatusId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertMaidenName("Val")
                .InsertPlaceOfBirth("Scottsdale")
                .ClickReligionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Christian")
                .TapSearchButton()
                .SelectResultElement(ReligionId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectAgeGroup("25-35")
                .ClickNationalityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("British Ind. OT.")
                .TapSearchButton()
                .SelectResultElement(NationalityId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickSexualOrientationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Not Known")
                .TapSearchButton()
                .SelectResultElement(SexualOrientationId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickCountryOfOriginLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("British Indian Ocean Territory (the)")
                .TapSearchButton()
                .SelectResultElement(CountryId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickLeavingCareEligibilityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Eligible")
                .TapSearchButton()
                .SelectResultElement(LeavingCareEligibilityId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectExBritishForces("Not Stated")
                .InsertCreditorNo("12345")
                .InsertDebtor1("45678")
                .InsertDebtor2("90876")
                .InsertReferenceCode("3");

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertSSDNumber("3457")
                .InsertNationalInsuranceNumber("AB123007C")
                .InsertNHSNoPre("4567")
                .InsertUniquePupilNo("10")
                .InsertFormerUniquePupilNo("1")
                .InsertCourtCaseNo("100")
                .InsertBirthCertificateNo("564")
                .InsertHomeOfficeRegistrationNumber("345")
                .ClickUPNUnknownReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Child is educated outside of England")
                .TapSearchButton()
                .SelectResultElement(UPNUnknownReasonId.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .TapSaveButton();

            System.Threading.Thread.Sleep(3000);

            potentialDuplicatesPopup
                .WaitForPotentialDuplicatesPopupToLoad()
                .ClickCreateButton();

            System.Threading.Thread.Sleep(5000);

            var personRecords = dbHelper.person.GetByPrimaryEmail(personEmail);
            Assert.AreEqual(1, personRecords.Count);

            var fields = dbHelper.person.GetPersonById(personRecords[0], "title", "firstname", "middlename", "lastname", "genderid", "dobandagetypeid", "dateofbirth", "personphotoid", "nhsnumber", "nonhsnumberreasonid",
                                     "ethnicityid", "maritalstatusid", "addressstartdate", "addresspropertytypeid", "addresstypeid", "uprn", "propertyname", "addressboroughid", "addresstypeid", "addressline1", "addressline2", "addressline3", "addressline4", "addressline5",
                                    "addresswardid", "country", "accommodationstatusid", "accommodationtypeid", "livesalonetypeid", "postcode", "ccgboundaryid", "lowersuperoutputareaid", "businessphone", "homephone", "mobilephone", "telephone1", "telephone2", "telephone3", "primaryemail", "secondaryemail",
                                    "billingemail", "languageid", "preferredname", "documentformatid", "contactmethodid", "modeofcommunicationid", "preferredcontactdayid", "preferredcontacttimeid", "ownerid", "nhscardlocation", "persontargetgroupid", "immigrationstatusid", "maidenname", "placeofbirth",
                                    "religionid", "agegroupid", "nationalityid", "sexualorientationid", "countryoforiginid", "exbritishforcesid", "leavingcareeligibilityid", "creditornumber", "referencecode", "debtornumber1", "debtornumber2", "payerid", "ssdnumber", "courtcasenumber", "nationalinsurancenumber",
                                    "birthcertificatenumber", "nhsnumberpre1995", "uniquepupilnumber", "formeruniquepupilnumber", "homeofficeregistrationnumber", "upnunknownreasonid");


            Assert.AreEqual("Mr", fields["title"]);
            Assert.AreEqual("AutomationQAPersonCreation", fields["firstname"]);
            Assert.AreEqual("AutomationQAPersonCreation", fields["middlename"]);
            Assert.AreEqual("AutomationQAPersonCreation", fields["lastname"]);
            Assert.AreEqual(1, fields["genderid"]);
            Assert.AreEqual(5, fields["dobandagetypeid"]);
            Assert.AreEqual(dob.Date, fields["dateofbirth"]);
            Assert.AreEqual("987 654 3210", fields["nhsnumber"]);
            Assert.AreEqual(EthnicityId.ToString(), fields["ethnicityid"].ToString());
            Assert.AreEqual(MaritalStatusId.ToString(), fields["maritalstatusid"].ToString());
            Assert.AreEqual(startDateOfAddress, fields["addressstartdate"]);
            Assert.AreEqual(PropertyTypeId.ToString(), fields["addresspropertytypeid"].ToString());
            Assert.AreEqual("6", fields["addresstypeid"].ToString());
            Assert.AreEqual("12345", fields["uprn"]);
            Assert.AreEqual("Residential", fields["propertyname"]);
            Assert.AreEqual(AddressBoroughId.ToString(), fields["addressboroughid"].ToString());
            Assert.AreEqual(6, fields["addresstypeid"]);
            Assert.AreEqual("12", fields["addressline1"]);
            Assert.AreEqual("East Linda", fields["addressline2"]);
            Assert.AreEqual("Showlow", fields["addressline3"]);
            Assert.AreEqual("CA", fields["addressline4"]);
            Assert.AreEqual("Maricopa", fields["addressline5"]);
            Assert.AreEqual("85258", fields["postcode"]);
            Assert.AreEqual(AddressWardId.ToString(), fields["addresswardid"].ToString());
            Assert.AreEqual("United Kingdom", fields["country"]);
            Assert.AreEqual(2, fields["accommodationstatusid"]);
            Assert.AreEqual(AccommodationTypeId.ToString(), fields["accommodationtypeid"].ToString());
            Assert.AreEqual(2, fields["livesalonetypeid"]);
            Assert.AreEqual(ProviderId.ToString(), fields["ccgboundaryid"].ToString());
            Assert.AreEqual(LowerSuperOutputAreaId.ToString(), fields["lowersuperoutputareaid"].ToString());
            Assert.AreEqual("9876543210", fields["businessphone"]);
            Assert.AreEqual("9876543210", fields["homephone"]);
            Assert.AreEqual("9876543210", fields["mobilephone"]);
            Assert.AreEqual("9876543210", fields["telephone1"]);
            Assert.AreEqual("9876543210", fields["telephone2"]);
            Assert.AreEqual("9876543210", fields["telephone3"]);
            Assert.AreEqual(personEmail, fields["primaryemail"]);
            Assert.AreEqual(personEmail, fields["secondaryemail"]);
            Assert.AreEqual(personEmail, fields["billingemail"]);
            Assert.AreEqual(LanguageId.ToString(), fields["languageid"].ToString());
            Assert.AreEqual("Chase", fields["preferredname"]);
            Assert.AreEqual(PersonDocumentFormatId.ToString(), fields["documentformatid"].ToString());
            Assert.AreEqual(PrefferedContactMethodId.ToString(), fields["contactmethodid"].ToString());
            Assert.AreEqual(ModeOfCommunicationId.ToString(), fields["modeofcommunicationid"].ToString());
            Assert.AreEqual(8, fields["preferredcontactdayid"]);
            Assert.AreEqual(1, fields["preferredcontacttimeid"]);
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual("Online", fields["nhscardlocation"]);
            Assert.AreEqual(TargetGroupId.ToString(), fields["persontargetgroupid"].ToString());
            Assert.AreEqual(ImmigrationStatusId.ToString(), fields["immigrationstatusid"].ToString());
            Assert.AreEqual("Scottsdale", fields["placeofbirth"]);
            Assert.AreEqual("Val", fields["maidenname"]);
            Assert.AreEqual(ReligionId.ToString(), fields["religionid"].ToString());
            Assert.AreEqual(3, fields["agegroupid"]);
            Assert.AreEqual(NationalityId.ToString(), fields["nationalityid"].ToString());
            Assert.AreEqual(SexualOrientationId.ToString(), fields["sexualorientationid"].ToString());
            Assert.AreEqual(CountryId.ToString(), fields["countryoforiginid"].ToString());
            Assert.AreEqual(4, fields["exbritishforcesid"]);
            Assert.AreEqual(LeavingCareEligibilityId.ToString(), fields["leavingcareeligibilityid"].ToString());
            Assert.AreEqual("12345", fields["creditornumber"]);
            Assert.AreEqual("3", fields["referencecode"]);
            Assert.AreEqual("45678", fields["debtornumber1"]);
            Assert.AreEqual("90876", fields["debtornumber2"]);
            //Assert.AreEqual(payer, fields["payerid"]);
            Assert.AreEqual("3457", fields["ssdnumber"]);
            Assert.AreEqual("100", fields["courtcasenumber"]);
            Assert.AreEqual("AB123007C", fields["nationalinsurancenumber"]);
            Assert.AreEqual("564", fields["birthcertificatenumber"]);
            Assert.AreEqual("4567", fields["nhsnumberpre1995"]);
            Assert.AreEqual("10", fields["uniquepupilnumber"]);
            Assert.AreEqual("1", fields["formeruniquepupilnumber"]);
            Assert.AreEqual("345", fields["homeofficeregistrationnumber"]);
            Assert.AreEqual(UPNUnknownReasonId.ToString(), fields["upnunknownreasonid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12887")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Select DOB for DOB and Age- Validate  the Age field is inactive.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Creation_UITestCases03()
        {

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage.
                WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Chase Sam")
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectDOBAndAge("DOB")
                .ValidateDateOfBirthFieldActiveStatus(true)
                .ValidateAgeFieldInactiveStatus(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12888")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Select Estimated Age for DOB and Age- Validate  the DOB field is inactive.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Creation_UITestCases04()
        {

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Chase Sam")
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectDOBAndAge("Estimated Age")
                .ValidateDateOfBirthFieldInActiveStatus(true)
                .ValidateAgeFieldActiveStatus(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12890")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Select Stated Age for DOB and Age- Validate  the DOB field is inactive.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Creation_UITestCases05()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Chase Sam")
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectDOBAndAge("Stated Age")
                .ValidateDateOfBirthFieldInActiveStatus(true)
                .ValidateAgeFieldActiveStatus(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12891")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Select Unborn for DOB and Age- Validate  the DOB and Age field is inactive and Expected Date field shoulb be visible.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Creation_UITestCases06()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Chase Sam")
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectDOBAndAge("Unborn")
                .ValidateDateOfBirthFieldInActiveStatus(true)
                .ValidateAgeFieldInactiveStatus(true)
                .ValidateExpectedDOBActiveStatus(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12918")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Select Unknown for DOB and Age- Validate  the DOB and Age field is inactive.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Creation_UITestCases07()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Chase Sam")
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectDOBAndAge("Unborn")
                .ValidateDateOfBirthFieldInActiveStatus(true)
                .ValidateAgeFieldInactiveStatus(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12919")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Select Deceased as Yes and Validate the text fields.")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Creation_UITestCases08()
        {
            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Chase Sam")
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickDeceasedYesRadioButton()
                .ValidateCauseOfDeathFieldActiveStatus(true)
                .ValidateDateOfDeathFieldActiveStatus(true)
                .ValidatePlaceOfDeathFieldActiveStatus(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12920")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Create a Person Record - Update the Deceased to Yes- Verify the Deceased Banner Icon in the person Banner")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_Creation_UITestCases09()
        {
            string FirstName = "AutomationQAPersonDeceased" + DateTime.Now.ToString("yyyyMMddHHmmss");
            var dob = DateTime.Now.Date.AddYears(-15);
            Guid Ethnicity = commonMethodsDB.CreateEthnicity(_teamId, "English", new DateTime(2020, 1, 1));
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = commonMethodsDB.CreateLanguage("English", _teamId, "", "", new DateTime(2000, 1, 1), null);
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId_Primary = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            var websiteid = new Guid("5d9019f3-591b-ec11-a32d-f90a4322a942"); //AutomationPersonCreation
            dbHelper.website.UpdateAdministrationInformation(websiteid, false, false);

            //remove any matching website user
            foreach (var websiteuserid in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteid, "CareDirectorAutomationTestingForPerson@mail.com"))
                dbHelper.websiteUser.DeleteWebsiteUser(websiteuserid);

            //remove any matching person record
            foreach (var personid in dbHelper.person.GetByFirstName("AutomationQAPersonDeceased"))
            {
                dbHelper.person.UpdateLinkedAddress(personid, null);

                foreach (var addressid in dbHelper.personAddress.GetByPersonId(personid))
                    dbHelper.personAddress.DeletePersonAddress(addressid);


                dbHelper.person.DeletePerson(personid);
            }

            //Create Master person record
            Guid personRecord = dbHelper.person.CreatePersonRecord("", FirstName, "", "AutomationQAPersonDeceased", "", dob, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Get PersonNumber
            var personNumber = (int)dbHelper.person.GetPersonById(personRecord, "personnumber")["personnumber"];

            //Update Deceased
            dbHelper.person.UpdateDeceased(personRecord, true);

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString())
                .OpenCreatedPersonRecord(personNumber.ToString());

            personRecordPage
                  .WaitForPersonRecordPageToLoad()
                  .ValidateDeceasedPerson_Icon(true);


        }

        [DeploymentItem("Files\\FileToUpload.jpg")]
        [DeploymentItem("chromedriver.exe")]
        [TestProperty("JiraIssueID", "CDV6-12924")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and click on search - Click on New Record Button- Enter All the Madatory and Optional Field- Click on Save and close Button")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        public void Person_Creation_UITestCases10()
        {
            var personEmail = "CareDirectorAutomationTestingForPerson" + DateTime.Now.ToString("yyyyMMddHHmmss") + "@mail.com";
            var dob = new DateTime(2008, 1, 1);
            var ethinicity = commonMethodsDB.CreateEthnicity(_teamId, "White and Asian", new DateTime(2020, 1, 1));
            var maritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            var startDateOfAddress = DateTime.Now.Date.AddYears(-1);
            var propertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Care Home")[0];
            var borough = commonMethodsDB.CreateAddressBorough("Test_Borough", new DateTime(2020, 1, 2), _teamId);
            var ward = commonMethodsDB.CreateAddressWard("Test Ward2", new DateTime(2020, 1, 2), _teamId);
            var accomodationTypes = commonMethodsDB.CreateAccommodationType("Owner Occupier", new DateTime(2020, 1, 2), _teamId);
            var providers = commonMethodsDB.CreateProvider("Healthify Provider", _teamId, 11);
            var lowerSuperOutputArea = commonMethodsDB.CreateLowerSuperOutputArea("Test_Lower1", new DateTime(2020, 1, 2), _teamId);
            var language = commonMethodsDB.CreateLanguage("English", _teamId, "", "", new DateTime(2000, 1, 1), null);
            var contactMethod = dbHelper.contactMethod.GetByName("Email").First();
            var modeOfCommunication = commonMethodsDB.CreateModeOfCommunication("Phone", new DateTime(2020, 1, 2), _teamId);
            var documentFormat = commonMethodsDB.CreatePersonDocumentFormat(_teamId, "Easy Read", new DateTime(2020, 1, 2));
            var targetGroup = commonMethodsDB.CreatePersonTargetGroup(_teamId, _businessUnitId, "Target Group 1", 1234, DateTime.Now.Date, null);
            var immigrationStatus = commonMethodsDB.CreateImmigrationStatus("Child has been an UASC", new DateTime(2020, 1, 2), _teamId);
            var religion = commonMethodsDB.CreateReligion("Christian", new DateTime(2020, 1, 2), _teamId);
            var nationality = dbHelper.nationality.GetNationalityByName("British Ind. OT.").First();
            var countryOfOrigin = commonMethodsDB.CreateCountry("British Indian Ocean Territory (the)", new DateTime(2020, 1, 2), _teamId);
            var leavingCareEligibility = commonMethodsDB.CreateLeavingCareEligibility("Eligible", new DateTime(2020, 1, 2), _teamId);
            var upnUnknownReason = commonMethodsDB.CreateUPNUnknownReason("Child is educated outside of England", new DateTime(2020, 1, 2), _teamId);
            //Guid ownerID = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5");//CareDirector QA
            var sexualOrientation = commonMethodsDB.CreateSexualOrientation("Not Known", new DateTime(2020, 1, 2), _teamId);

            #region Business Object

            var businessObjectId = dbHelper.businessObject.GetBusinessObjectByName("person")[0];

            #endregion

            #region Business Object Field

            var businessObjectFieldId_DateOfBirth = dbHelper.businessObjectField.GetBusinessObjectFieldByName("DateOfBirth", businessObjectId)[0];
            var businessObjectFieldId_FirstName = dbHelper.businessObjectField.GetBusinessObjectFieldByName("FirstName", businessObjectId)[0];

            #endregion

            #region Duplicate Detection Rule

            var duplicateDetectionRuleId = commonMethodsDB.CreateDuplicateDetectionRule("Person Duplication", "...", businessObjectId, true, true);

            #endregion

            #region Duplicate Detection Conditions

            int criterionid_SameDate = 1;
            var duplicateDetectionCondition1Id = commonMethodsDB.CreateDuplicateDetectionCondition("DateOfBirth Same Date", duplicateDetectionRuleId, criterionid_SameDate, null, true, businessObjectFieldId_DateOfBirth);
            dbHelper.duplicateDetectionCondition.UpdateAdministrationInformation(duplicateDetectionCondition1Id, true);

            int criterionid_ExactMatch = 3;
            var duplicateDetectionCondition2Id = commonMethodsDB.CreateDuplicateDetectionCondition("FirstName Exact Match", duplicateDetectionRuleId, criterionid_ExactMatch, null, true, businessObjectFieldId_FirstName);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .ClickNewRecordButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad()
                .InsertFirstName("Chase Sam")
                .ClickSearchButton()
                .ClickNewRecordButton();

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertTitle("Mr")
                .InsertFirstName("AutomationQAPersonCreation")
                .InsertMiddleName("AutomationQAPersonCreation")
                .InsertLastName("AutomationQAPersonCreation")
                .SelectStatedGender("Male")
                .InsertNHSNo("987 654 3210")
                .ClickImageUploadDocument(this.TestContext.DeploymentDirectory + "\\FileToUpload.jpg")
                .InsertDOB(dob.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectDOBAndAge("DOB")
                .ClickEthnicityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("White and Asian")
                .TapSearchButton()
                .SelectResultElement(ethinicity.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickMaritalStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Married")
                .TapSearchButton()
                .SelectResultElement(maritalStatus.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertStartDateOfAddress(startDateOfAddress.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectAddressType("Home")
                .InsertPropertyName("Residential")
                .InsertPropertyNo("12")
                .InsertStreet("East Linda")
                .InsertVillageDistrict("Showlow")
                .InsertTownCity("CA")
                .InsertCounty("Maricopa")
                .InsertPostCode("85258")
                .InsertUPRN("12345")
                .ClickPropertyTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Care Home")
                .TapSearchButton()
                .SelectResultElement(propertyType.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickBoroughLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Test_Borough")
                .TapSearchButton()
                .SelectResultElement(borough.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickWardLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Test Ward2")
                .TapSearchButton()
                .SelectResultElement(ward.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertCountry("United Kingdom")
                .SelectAccomodationStatus("Unsettled")
                .ClickAccomodationTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Owner Occupier")
                .TapSearchButton()
                .SelectResultElement(accomodationTypes.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectLivesAlone("No")
                .ClickCCGBoundaryLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Healthify Provider")
                .TapSearchButton()
                .SelectResultElement(providers.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickLowerSuperOutputAreaLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("TEst_Lower1")
                .TapSearchButton()
                .SelectResultElement(lowerSuperOutputArea.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertBusinessPhone("9876543210")
                .InsertHomePhone("9876543210")
                .InsertMobilePhone("9876543210")
                .InsertPrimaryEmail(personEmail)
                .InsertSecondaryEmail(personEmail)
                .InsertBillingEmail(personEmail)
                .InsertTelephone1("9876543210")
                .InsertTelephone2("9876543210")
                .InsertTelephone3("9876543210")
                .ClickPreferredLanguageLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("English")
                .TapSearchButton()
                .SelectResultElement(language.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertPreferredName("Chase")
                .ClickPreferredContactMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Email")
                .TapSearchButton()
                .SelectResultElement(contactMethod.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickPreferredModeOfCommunicationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Phone")
                .TapSearchButton()
                .SelectResultElement(modeOfCommunication.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectPreferredDay("Any Day")
                .SelectPreferredTime("Morning")
                .ClickDocumentFormatLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Easy Read")
                .TapSearchButton()
                .SelectResultElement(documentFormat.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertNHSCardLocation("Online")
                .ClickTargetGroupLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Target Group 1")
                .TapSearchButton()
                .SelectResultElement(targetGroup.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickImmigrationStatusLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Child has been an UASC")
                .TapSearchButton()
                .SelectResultElement(immigrationStatus.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertMaidenName("Val")
                .InsertPlaceOfBirth("Scottsdale")
                .ClickReligionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Christian")
                .TapSearchButton()
                .SelectResultElement(religion.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectAgeGroup("25-35")
                .ClickNationalityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("British Ind. OT.")
                .TapSearchButton()
                .SelectResultElement(nationality.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickSexualOrientationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Not Known")
                .TapSearchButton()
                .SelectResultElement(sexualOrientation.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickCountryOfOriginLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("British Indian Ocean Territory (the)")
                .TapSearchButton()
                .SelectResultElement(countryOfOrigin.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .ClickLeavingCareEligibilityLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Eligible")
                .TapSearchButton()
                .SelectResultElement(leavingCareEligibility.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .SelectExBritishForces("Not Stated")
                .InsertCreditorNo("12345")
                .InsertDebtor1("45678")
                .InsertDebtor2("90876")
                .InsertReferenceCode("3");

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .InsertSSDNumber("3457")
                .InsertNationalInsuranceNumber("AB123007C")
                .InsertNHSNoPre("4567")
                .InsertUniquePupilNo("10")
                .InsertFormerUniquePupilNo("1")
                .InsertCourtCaseNo("100")
                .InsertBirthCertificateNo("564")
                .InsertHomeOfficeRegistrationNumber("345")
                .ClickUPNUnknownReasonLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup Records")
                .TypeSearchQuery("Child is educated outside of England")
                .TapSearchButton()
                .SelectResultElement(upnUnknownReason.ToString());

            personRecordNewPage
                .WaitForNewPersonRecordPageToLoad()
                .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(4000);

            potentialDuplicatesPopup
                .WaitForPotentialDuplicatesPopupToLoad()
                .ClickCreateButton();

            personSearchPage
                .WaitForPersonSearchPageToLoad();

            System.Threading.Thread.Sleep(5000);

            var personRecords = dbHelper.person.GetByPrimaryEmail(personEmail);
            Assert.AreEqual(1, personRecords.Count);

            var fields = dbHelper.person.GetPersonById(personRecords[0], "title", "firstname", "middlename", "lastname", "genderid", "dobandagetypeid", "dateofbirth", "personphotoid", "nhsnumber", "nonhsnumberreasonid",
                                     "ethnicityid", "maritalstatusid", "addressstartdate", "addresspropertytypeid", "addresstypeid", "uprn", "propertyname", "addressboroughid", "addresstypeid", "addressline1", "addressline2", "addressline3", "addressline4", "addressline5",
                                    "addresswardid", "country", "accommodationstatusid", "accommodationtypeid", "livesalonetypeid", "postcode", "ccgboundaryid", "lowersuperoutputareaid", "businessphone", "homephone", "mobilephone", "telephone1", "telephone2", "telephone3", "primaryemail", "secondaryemail",
                                    "billingemail", "languageid", "preferredname", "documentformatid", "contactmethodid", "modeofcommunicationid", "preferredcontactdayid", "preferredcontacttimeid", "ownerid", "nhscardlocation", "persontargetgroupid", "immigrationstatusid", "maidenname", "placeofbirth",
                                    "religionid", "agegroupid", "nationalityid", "sexualorientationid", "countryoforiginid", "exbritishforcesid", "leavingcareeligibilityid", "creditornumber", "referencecode", "debtornumber1", "debtornumber2", "payerid", "ssdnumber", "courtcasenumber", "nationalinsurancenumber",
                                    "birthcertificatenumber", "nhsnumberpre1995", "uniquepupilnumber", "formeruniquepupilnumber", "homeofficeregistrationnumber", "upnunknownreasonid");


            Assert.AreEqual("Mr", fields["title"]);
            Assert.AreEqual("AutomationQAPersonCreation", fields["firstname"]);
            Assert.AreEqual("AutomationQAPersonCreation", fields["middlename"]);
            Assert.AreEqual("AutomationQAPersonCreation", fields["lastname"]);
            Assert.AreEqual(1, fields["genderid"]);
            Assert.AreEqual(5, fields["dobandagetypeid"]);
            Assert.AreEqual(dob.Date, fields["dateofbirth"]);
            Assert.AreEqual("987 654 3210", fields["nhsnumber"]);
            Assert.AreEqual(ethinicity.ToString(), fields["ethnicityid"].ToString());
            Assert.AreEqual(maritalStatus.ToString(), fields["maritalstatusid"].ToString());
            Assert.AreEqual(startDateOfAddress.ToString(), fields["addressstartdate"].ToString());
            Assert.AreEqual(propertyType.ToString(), fields["addresspropertytypeid"].ToString());
            Assert.AreEqual("6", fields["addresstypeid"].ToString());
            Assert.AreEqual("12345", fields["uprn"]);
            Assert.AreEqual("Residential", fields["propertyname"]);
            Assert.AreEqual(borough.ToString(), fields["addressboroughid"].ToString());
            Assert.AreEqual(6, fields["addresstypeid"]);
            Assert.AreEqual("12", fields["addressline1"]);
            Assert.AreEqual("East Linda", fields["addressline2"]);
            Assert.AreEqual("Showlow", fields["addressline3"]);
            Assert.AreEqual("CA", fields["addressline4"]);
            Assert.AreEqual("Maricopa", fields["addressline5"]);
            Assert.AreEqual("85258", fields["postcode"]);
            Assert.AreEqual(ward.ToString(), fields["addresswardid"].ToString());
            Assert.AreEqual("United Kingdom", fields["country"]);
            Assert.AreEqual(2, fields["accommodationstatusid"]);
            Assert.AreEqual(accomodationTypes.ToString(), fields["accommodationtypeid"].ToString());
            Assert.AreEqual(2, fields["livesalonetypeid"]);
            Assert.AreEqual(providers.ToString(), fields["ccgboundaryid"].ToString());
            Assert.AreEqual(lowerSuperOutputArea.ToString(), fields["lowersuperoutputareaid"].ToString());
            Assert.AreEqual("9876543210", fields["businessphone"]);
            Assert.AreEqual("9876543210", fields["homephone"]);
            Assert.AreEqual("9876543210", fields["mobilephone"]);
            Assert.AreEqual("9876543210", fields["telephone1"]);
            Assert.AreEqual("9876543210", fields["telephone2"]);
            Assert.AreEqual("9876543210", fields["telephone3"]);
            Assert.AreEqual(personEmail.ToString(), fields["primaryemail"].ToString());
            Assert.AreEqual(personEmail.ToString(), fields["secondaryemail"].ToString());
            Assert.AreEqual(personEmail.ToString(), fields["billingemail"].ToString());
            Assert.AreEqual(language.ToString(), fields["languageid"].ToString());
            Assert.AreEqual("Chase", fields["preferredname"]);
            Assert.AreEqual(documentFormat.ToString(), fields["documentformatid"].ToString());
            Assert.AreEqual(contactMethod.ToString(), fields["contactmethodid"].ToString());
            Assert.AreEqual(modeOfCommunication.ToString(), fields["modeofcommunicationid"].ToString());
            Assert.AreEqual(8, fields["preferredcontactdayid"]);
            Assert.AreEqual(1, fields["preferredcontacttimeid"]);
            Assert.AreEqual(_teamId.ToString(), fields["ownerid"].ToString());
            Assert.AreEqual("Online", fields["nhscardlocation"]);
            Assert.AreEqual(targetGroup.ToString(), fields["persontargetgroupid"].ToString());
            Assert.AreEqual(immigrationStatus.ToString(), fields["immigrationstatusid"].ToString());
            Assert.AreEqual("Scottsdale", fields["placeofbirth"]);
            Assert.AreEqual("Val", fields["maidenname"]);
            Assert.AreEqual(religion.ToString(), fields["religionid"].ToString());
            Assert.AreEqual(3, fields["agegroupid"]);
            Assert.AreEqual(nationality.ToString(), fields["nationalityid"].ToString());
            Assert.AreEqual(sexualOrientation.ToString(), fields["sexualorientationid"].ToString());
            Assert.AreEqual(countryOfOrigin.ToString(), fields["countryoforiginid"].ToString());
            Assert.AreEqual(4, fields["exbritishforcesid"]);
            Assert.AreEqual(leavingCareEligibility.ToString(), fields["leavingcareeligibilityid"].ToString());
            Assert.AreEqual("12345", fields["creditornumber"]);
            Assert.AreEqual("3", fields["referencecode"]);
            Assert.AreEqual("45678", fields["debtornumber1"]);
            Assert.AreEqual("90876", fields["debtornumber2"]);
            Assert.AreEqual("3457", fields["ssdnumber"]);
            Assert.AreEqual("100", fields["courtcasenumber"]);
            Assert.AreEqual("AB123007C", fields["nationalinsurancenumber"]);
            Assert.AreEqual("564", fields["birthcertificatenumber"]);
            Assert.AreEqual("4567", fields["nhsnumberpre1995"]);
            Assert.AreEqual("10", fields["uniquepupilnumber"]);
            Assert.AreEqual("1", fields["formeruniquepupilnumber"]);
            Assert.AreEqual("345", fields["homeofficeregistrationnumber"]);
            Assert.AreEqual(upnUnknownReason.ToString(), fields["upnunknownreasonid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12925")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Create a Person Record - Go to Edit- Generate the word document for the person Record")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Person_Creation_UITestCases11()
        {
            #region Person

            var personID = commonMethodsDB.CreatePersonRecord("John", _currentDateString, _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personFullName = "John " + _currentDateString;
            var personNumber = (int)(dbHelper.person.GetPersonById(personID, "personnumber")["personnumber"]);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), personID.ToString())
                .OpenPersonRecord(personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personID.ToString(), personFullName)
                .ClickPrintButton();

            System.Threading.Thread.Sleep(5000);

            bool fileExists = fileIOHelper.ValidateIfFileExists(DownloadsDirectory, "Record.docx");
            Assert.IsTrue(fileExists);

        }

        [TestProperty("JiraIssueID", "CDV6-12932")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Create a Person Record and Deactivate it")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_Creation_UITestCases12()
        {
            string FirstName = "AutomationQAPersonDeactivate";
            var dob = DateTime.Now.Date.AddYears(-15);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId_Primary = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            var websiteid = new Guid("5d9019f3-591b-ec11-a32d-f90a4322a942"); //AutomationPersonCreation
            dbHelper.website.UpdateAdministrationInformation(websiteid, false, false);

            //remove any matching website user
            foreach (var websiteuserid in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteid, "CareDirectorAutomationTestingForPerson@mail.com"))
                dbHelper.websiteUser.DeleteWebsiteUser(websiteuserid);

            //remove any matching person record
            foreach (var personid in dbHelper.person.GetByFirstName("AutomationQAPersonDeactivate"))
            {
                dbHelper.person.UpdateLinkedAddress(personid, null);

                foreach (var addressid in dbHelper.personAddress.GetByPersonId(personid))
                    dbHelper.personAddress.DeletePersonAddress(addressid);

                //remove Languages for the person
                foreach (var personlanguage in dbHelper.personLanguage.GetPersonLanguageIdByPersonID(personid))
                    dbHelper.personLanguage.DeletePersonLanguage(personlanguage);


                dbHelper.person.DeletePerson(personid);
            }

            //Create Master person record
            Guid personRecord = dbHelper.person.CreatePersonRecord("", FirstName, "", "AutomationQAPersonDeactivate", "", dob, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Get PersonNumber
            var personNumber = (int)dbHelper.person.GetPersonById(personRecord, "personnumber")["personnumber"];


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString())
                .OpenCreatedPersonRecord(personNumber.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personRecord.ToString(), "AutomationQAPersonDeactivate AutomationQAPersonDeactivate")
                .ClickDeactivateButton();

            confirmDynamicDialogPopup.WaitForConfirmDynamicDialogPopupToLoad().ValidateMessage("Are you sure you want to deactivate this record? To continue, click ok.").TapOKButton();

            System.Threading.Thread.Sleep(3000);

            var personRecords = dbHelper.person.GetByFirstName("AutomationQAPersonDeactivate");
            Assert.AreEqual(1, personRecords.Count);

            var Allfields = dbHelper.person.GetPersonById(personRecord, "inactive");
            Assert.AreEqual(true, Allfields["inactive"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12933")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Create a Person Record and open the record ,Edit and  Update")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_Creation_UITestCases13()
        {
            string FirstName = "AutomationQAPersonUpdate";
            string LastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var dob = DateTime.Now.Date.AddYears(-15);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId_Primary = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            var websiteid = new Guid("5d9019f3-591b-ec11-a32d-f90a4322a942"); //AutomationPersonCreation
            dbHelper.website.UpdateAdministrationInformation(websiteid, false, false);

            //remove any matching website user
            foreach (var websiteuserid in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteid, "CareDirectorAutomationTestingForPerson@mail.com"))
                dbHelper.websiteUser.DeleteWebsiteUser(websiteuserid);

            //Create Master person record
            Guid personRecord = dbHelper.person.CreatePersonRecord("", FirstName, "", LastName, "", dob, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Update
            dbHelper.person.UpdateDateOfBirth(personRecord, dob);
            dbHelper.person.UpdateDOBAndAgeTypeId(personRecord, 1);

            //Get PersonNumber
            var personNumber = (int)dbHelper.person.GetPersonById(personRecord, "personnumber")["personnumber"];

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString())
                .OpenCreatedPersonRecord(personNumber.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personRecord.ToString(), FirstName + " " + LastName)
                .InsertMiddleName("Update")
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            System.Threading.Thread.Sleep(3000);

            var personRecords = dbHelper.person.GetByFirstAndLastName(FirstName, LastName);
            Assert.AreEqual(1, personRecords.Count);

            var fields = dbHelper.person.GetPersonById(personRecords[0], "middlename");
            Assert.AreEqual("Update", fields["middlename"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12936")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Create a Person Record and open the record - edit and  Update the Address")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_Creation_UITestCases14()
        {
            string FirstName = "AutomationQAPersonUpdate";
            string LastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var dob = DateTime.Now.Date.AddYears(-15);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId_Primary = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            var websiteid = new Guid("5d9019f3-591b-ec11-a32d-f90a4322a942"); //AutomationPersonCreation
            dbHelper.website.UpdateAdministrationInformation(websiteid, false, false);

            //remove any matching website user
            foreach (var websiteuserid in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteid, "CareDirectorAutomationTestingForPerson@mail.com"))
                dbHelper.websiteUser.DeleteWebsiteUser(websiteuserid);


            //Create Master person record
            Guid personRecord = dbHelper.person.CreatePersonRecord("", FirstName, "", LastName, "", dob, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Update
            dbHelper.person.UpdateDateOfBirth(personRecord, dob);
            dbHelper.person.UpdateDOBAndAgeTypeId(personRecord, 1);

            //Get PersonNumber
            var personNumber = (int)dbHelper.person.GetPersonById(personRecord, "personnumber")["personnumber"];


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString())
                .OpenCreatedPersonRecord(personNumber.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personRecord.ToString(), FirstName + " " + LastName)
                .SelectAddressType("Business")
                .TapSaveAndCloseButton();

            addressActionPopUp.WaitForAddressActionPopUpToLoad().SelectViewByText("Update existing address record").TapOkButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personRecord.ToString(), FirstName + " " + LastName)
                .TapSaveAndCloseButton();

            personRecordPage
                .WaitForPersonRecordPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var personRecords = dbHelper.person.GetByFirstAndLastName(FirstName, LastName);
            Assert.AreEqual(1, personRecords.Count);

            var fields = dbHelper.person.GetPersonById(personRecords[0], "addresstypeid");
            Assert.AreEqual(3, fields["addresstypeid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12938")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Create a Person Record - Update the Date of death- Verify the Deceased Banner Icon in the person Banner")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_Creation_UITestCases15()
        {
            string FirstName = "AutomationQAPersonUpdate";
            string LastName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var dob = DateTime.Now.Date.AddYears(-15);
            var dod = DateTime.Now.Date;
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId_Primary = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            var websiteid = new Guid("5d9019f3-591b-ec11-a32d-f90a4322a942"); //AutomationPersonCreation
            dbHelper.website.UpdateAdministrationInformation(websiteid, false, false);

            //remove any matching website user
            foreach (var websiteuserid in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteid, "CareDirectorAutomationTestingForPerson@mail.com"))
                dbHelper.websiteUser.DeleteWebsiteUser(websiteuserid);

            //Create Master person record
            Guid personRecord = dbHelper.person.CreatePersonRecord("", FirstName, "", LastName, "", dob, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Update
            dbHelper.person.UpdateDateOfBirth(personRecord, dob);
            dbHelper.person.UpdateDOBAndAgeTypeId(personRecord, 1);

            //Get PersonNumber
            var personNumber = (int)dbHelper.person.GetPersonById(personRecord, "personnumber")["personnumber"];


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString())
                .OpenCreatedPersonRecord(personNumber.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personRecord.ToString(), FirstName + " " + LastName)
                .ClickDeceasedYesRadioButton()
                .InsertDateOfDeath(dod.ToString("dd'/'MM'/'yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .TapSaveAndCloseButton();

            System.Threading.Thread.Sleep(3000);

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .ValidateDeceasedPerson_Icon(true);

        }

        [TestProperty("JiraIssueID", "CDV6-12939")]
        [Description("Navigate to People- Click on + Sign to add new Person- Enter any Name in FirstName and clickon search" +
                     "Click on New Record Button- Create a Person Record and Validate the Record is Active")]
        [TestMethod]
        [TestCategory("UITest")]
        public void Person_Creation_UITestCases16()
        {
            string FirstName = "AutomationQAPersonActivate";
            var dob = DateTime.Now.Date.AddYears(-15);
            Guid Ethnicity = dbHelper.ethnicity.GetEthnicityIdByName("English")[0];
            Guid MaritalStatus = dbHelper.maritalStatus.GetMaritalStatusIdByName("Married")[0];
            Guid LanguageId = dbHelper.language.GetLanguageIdByName("English")[0];
            Guid AddressPropertyType = dbHelper.addressPropertyType.GetAddressPropertyTypeIdByName("Other")[0];
            Guid OwnerID = dbHelper.team.GetTeamIdByName("Mobile Team 1")[0];
            int AddressTypeId_Primary = 7;
            int AccommodationStatusId = 1;
            int LivesAloneTypeId = 1;
            int GenderId = 1;

            var websiteid = new Guid("5d9019f3-591b-ec11-a32d-f90a4322a942"); //AutomationPersonCreation
            dbHelper.website.UpdateAdministrationInformation(websiteid, false, false);

            //remove any matching website user
            foreach (var websiteuserid in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteid, "CareDirectorAutomationTestingForPerson@mail.com"))
                dbHelper.websiteUser.DeleteWebsiteUser(websiteuserid);

            //remove any matching person record
            foreach (var personid in dbHelper.person.GetByFirstName("AutomationQAPersonActivate"))
            {
                dbHelper.person.UpdateLinkedAddress(personid, null);

                foreach (var addressid in dbHelper.personAddress.GetByPersonId(personid))
                    dbHelper.personAddress.DeletePersonAddress(addressid);

                //remove Languages for the person
                foreach (var personlanguage in dbHelper.personLanguage.GetPersonLanguageIdByPersonID(personid))
                    dbHelper.personLanguage.DeletePersonLanguage(personlanguage);


                dbHelper.person.DeletePerson(personid);
            }

            //Create Master person record
            Guid personRecord = dbHelper.person.CreatePersonRecord("", FirstName, "", "AutomationQAPersonActivate", "", dob, Ethnicity, MaritalStatus, LanguageId, AddressPropertyType, OwnerID, "PROPERTY NAME", "PROPERTY NO", "COUNTRY", "STREET", "VLG", "UPRN", "TOWN", "COUNTY", "POSTCODE", AddressTypeId_Primary, AccommodationStatusId, LivesAloneTypeId, GenderId);

            //Get PersonNumber
            var personNumber = (int)dbHelper.person.GetPersonById(personRecord, "personnumber")["personnumber"];


            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString())
                .OpenCreatedPersonRecord(personNumber.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapEditButton();

            personRecordEditPage
                .WaitForPersonRecordEditPageToLoad(personRecord.ToString(), "AutomationQAPersonActivate AutomationQAPersonActivate");

            var personRecords = dbHelper.person.GetByFirstName("AutomationQAPersonDeactivate");
            Assert.AreEqual(1, personRecords.Count);

            var Allfields = dbHelper.person.GetPersonById(personRecord, "inactive");
            Assert.AreEqual(false, Allfields["inactive"]);

        }

        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        [TestCategory("UITest")]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}
