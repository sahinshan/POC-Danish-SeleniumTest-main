using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Cases
{
    [TestClass]
    public class Case_InpatientCase_UITestCases : FunctionalTest
    {

        private Guid _businessUnitId;
        private Guid _teamId;
        private Guid _languageId;
        private Guid _authenticationproviderid;
        private string _tenantName;
        private Guid _systemUserId;

        [TestInitialize()]
        public void InpatientTestCases_SetupTest()
        {
            try
            {
                #region Tenant

                _tenantName = ConfigurationManager.AppSettings["TenantName"];
                dbHelper = new DBHelper.DatabaseHelper(_tenantName);
                commonMethodsDB = new CommonMethodsDB(dbHelper, fileIOHelper, TestContext);

                #endregion

                #region Internal

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal")[0];

                #endregion

                #region Default User

                string username = ConfigurationManager.AppSettings["Username"];
                string dataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                commonMethodsDB.UpdateSystemUserLastPasswordChange(username, dataEncoded);

                #endregion

                #region Language

                _languageId = commonMethodsDB.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);

                #endregion Language

                #region Business Unit

                _businessUnitId = commonMethodsDB.CreateBusinessUnit("InpatientCase BU");

                #endregion

                #region Team

                _teamId = commonMethodsDB.CreateTeam("InpatientCase T1", null, _businessUnitId, "907678", "InpatientCaseT1@careworkstempmail.com", "InpatientCase T1", "020 123456");

                #endregion

                #region System User

                _systemUserId = commonMethodsDB.CreateSystemUserRecord("CaseInpatientCaseUser1", "CaseInpatientCase", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

                #endregion
            }

            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-12833

        [TestProperty("JiraIssueID", "CDV6-12869")]
        [Description("Creating Hospital Ward Record with all mandatory fields" +
                     "Validate Hospital Ward Record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod01()
        {
            #region Current Date Time

            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #endregion

            #region Provider_Hospital

            var _provider_Name = "Hospital_" + _currentDateString;
            var hospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;


            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthSetUp();

            healthSetupPage
                .WaitForHealthSetupPageToLoad()
                .ClickHospitalWardsButton();

            hospitalWardsPage
                .WaitForHospitalWardsPageToLoad()
                .ClickNewRecordButton();

            hospitalWardsRecordPage
                .WaitForHospitalWardsRecordPageToLoad()
                .InsertName("Ward " + _currentDateString)
                .ClickHospitalLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_provider_Name).TapSearchButton().SelectResultElement(hospitalId.ToString());

            hospitalWardsRecordPage
                .WaitForHospitalWardsRecordPageToLoad()
                .ClickWardsMangerLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().SelectViewByText("Lookup View").TypeSearchQuery("WardManagerUser1").TapSearchButton().SelectResultElement(wardManagerId.ToString());

            hospitalWardsRecordPage
                .WaitForHospitalWardsRecordPageToLoad()
                .ClickWardSpecialtyLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Community Care").TapSearchButton().SelectResultElement(wardSpecialtiesId.ToString());

            hospitalWardsRecordPage
                .WaitForHospitalWardsRecordPageToLoad()
                .InsertStartDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickSaveAndCloseButton();

            hospitalWardsPage
                .WaitForHospitalWardsPageToLoad()
                .ClickRefreshButton();

            dbHelper = new DBHelper.DatabaseHelper(_tenantName);

            var InpatientWardRecords = dbHelper.inpatientWard.GetInpatientWardByTitle("Ward " + _currentDateString);
            Assert.AreEqual(1, InpatientWardRecords.Count);

            var InpatientWardRecordFields = dbHelper.inpatientWard.GetInpatientWardByID(InpatientWardRecords[0], "Title", "providerid", "wardmanagerid", "wardspecialtyid", "startdate", "enddate");
            Assert.AreEqual("Ward " + _currentDateString, InpatientWardRecordFields["title"]);
            Assert.AreEqual(hospitalId.ToString(), InpatientWardRecordFields["providerid"].ToString());
            Assert.AreEqual(wardManagerId.ToString(), InpatientWardRecordFields["wardmanagerid"].ToString());
            Assert.AreEqual(wardSpecialtiesId.ToString(), InpatientWardRecordFields["wardspecialtyid"].ToString());
            Assert.AreEqual(currentDate.Date, InpatientWardRecordFields["startdate"]);
            Assert.AreEqual(currentDate.Date, InpatientWardRecordFields["enddate"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12870")]
        [Description("Creating Hospital  Record with all mandatory fields and provider type=Hospital" +
                     "Validate Hospital Record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod02()
        {
            #region Current Date Time

            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #endregion

            #region Parent Provider

            var _providerParent_Name = "ParentH_" + _currentDateString;
            var providerParentId = commonMethodsDB.CreateProvider(_providerParent_Name, _teamId);

            #endregion

            #region Profession Type

            var _professionTypeId = dbHelper.professionType.GetByName("General Practitioner").FirstOrDefault();

            #endregion

            #region Professional

            var primaryContactId = commonMethodsDB.CreateProfessional(_teamId, _professionTypeId, "Dr", "Jordernson", "MCPeter", "Dr.Jordernson.MCPeter@somemail.com");

            #endregion

            #region Property Type

            var propertyTypeAddressId = dbHelper.addressPropertyType.GetAddressPropertyTypeByName("Care Home").First();

            #endregion

            #region Property Type

            var prefferedContactMethodId = dbHelper.contactMethod.GetByName("Email").First();

            #endregion

            #region Commissioner Organisation

            var _commissionerOrganisation_Name = "CommissionerO_" + _currentDateString;
            var _commissionerOrganisationId = commonMethodsDB.CreateProvider(_commissionerOrganisation_Name, _teamId);

            #endregion

            #region rtt Treatment Function Code

            var rttTreatmentFunctionCodeID = dbHelper.rttTreatmentFunctionCode.GetByName("General Surgery Service")[0];

            #endregion

            var _providerName = "MainP_" + _currentDateString;
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var pastDate = currentDate.AddYears(-10);
            var odscode = commonMethodsHelper.GetRandomValue(1, 2147483647);

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToProvidersSection();

            providersPage
                .WaitForProvidersPageToLoad()
                .ClickNewRecordButton();

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertName(_providerName)
                .SelectProviderType("Hospital")
                .InsertAccountNumber("123456789")
                .ClickParentProviderLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_providerParent_Name).TapSearchButton().SelectResultElement(providerParentId);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickPrimaryContactLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("MCPeter").TapSearchButton().SelectResultElement(primaryContactId);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertCQCLocation("12345")
                .InsertODSCode(odscode.ToString())
                .InsertDescription("automation")
                .InsertMainPhoneNo("123456789")
                .InsertOtherPhoneNo("45661")
                .InsertWebsite("caredirector@QA")
                .InsertEmailId("caredirectorqa@gmail.com")
                .InsertStartDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertEndDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertStartDateOfAddress(pastDate.ToString("dd'/'MM'/'yyyy"))
                .SelectAddressTypeId("Alternative")
                .InsertPropertyName("caredirector")
                .InsertPropertyNo("3456")
                .InsertStreetName("st jons")
                .InsertVlgDistrictName("QA")
                .InsertTownCityName("ghjy")
                .InsertCounty("yfjhyu")
                .InsertPostCodeNo("457896")
                .ClickAddressPropertyTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad().TypeSearchQuery("Care Home").TapSearchButton().SelectResultElement(propertyTypeAddressId);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertCountryName("india")
                .InsertAddressPhoneNo("456782135")
                .InsertContactHours("10")
                .ClickContactMethodeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Email").TapSearchButton().SelectResultElement(prefferedContactMethodId);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickCommissionerOrganisationLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_commissionerOrganisation_Name).TapSearchButton().SelectResultElement(_commissionerOrganisationId);

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .ClickRTTTreatmentFunctionLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("General Surgery Service").TapSearchButton().SelectResultElement(rttTreatmentFunctionCodeID.ToString());

            providersRecordPage
                .WaitForProvidersRecordPageToLoad()
                .InsertNotesText("automation test")
                .ClickSaveAndCloseButton();

            providersPage
                .WaitForProvidersPageToLoad()
                .ClickRefreshButton();

            var providersRecords = dbHelper.provider.GetProviderByName(_providerName);
            Assert.AreEqual(1, providersRecords.Count);

            var providerRecordFields = dbHelper.provider.GetProviderByID(providersRecords[0], "Name", "ProviderTypeId", "AccountNumber", "ParentProviderId", "PrimaryContactId",
                "AddressPropertyTypeId", "CQCLocationId", "ODSCode", "Description", "MainPhone", "OtherPhone", "Website",
                "Email", "StartDate", "EndDate", "AddressStartDate", "AddressTypeId", "PropertyName", "AddressLine1",
                "AddressLine2", "AddressLine3", "AddressLine4", "AddressLine5", "PostCode", "country", "AddressPhone",
                "ContactHours", "contactmethodid", "notes");

            Assert.AreEqual(3, providerRecordFields["providertypeid"]);
            Assert.AreEqual(_providerName, providerRecordFields["name"]);
            Assert.AreEqual("123456789", providerRecordFields["accountnumber"]);
            Assert.AreEqual(providerParentId.ToString(), providerRecordFields["parentproviderid"].ToString());
            Assert.AreEqual(primaryContactId.ToString(), providerRecordFields["primarycontactid"].ToString());
            Assert.AreEqual(propertyTypeAddressId.ToString(), providerRecordFields["addresspropertytypeid"].ToString());
            Assert.AreEqual("12345", providerRecordFields["cqclocationid"]);
            Assert.AreEqual(odscode.ToString(), providerRecordFields["odscode"]);
            Assert.AreEqual("automation", providerRecordFields["description"]);
            Assert.AreEqual("123456789", providerRecordFields["mainphone"]);
            Assert.AreEqual("45661", providerRecordFields["otherphone"]);
            Assert.AreEqual("caredirector@QA", providerRecordFields["website"]);
            Assert.AreEqual("caredirectorqa@gmail.com", providerRecordFields["email"]);
            Assert.AreEqual(currentDate.Date, providerRecordFields["startdate"]);
            Assert.AreEqual(currentDate.Date, providerRecordFields["enddate"]);
            Assert.AreEqual(pastDate.Date, providerRecordFields["addressstartdate"]);
            Assert.AreEqual(7, providerRecordFields["addresstypeid"]);
            Assert.AreEqual("caredirector", providerRecordFields["propertyname"]);
            Assert.AreEqual("3456", providerRecordFields["addressline1"]);
            Assert.AreEqual("st jons", providerRecordFields["addressline2"]);
            Assert.AreEqual("QA", providerRecordFields["addressline3"]);
            Assert.AreEqual("ghjy", providerRecordFields["addressline4"]);
            Assert.AreEqual("yfjhyu", providerRecordFields["addressline5"]);
            Assert.AreEqual("457896", providerRecordFields["postcode"]);
            Assert.AreEqual("india", providerRecordFields["country"]);
            Assert.AreEqual("456782135", providerRecordFields["addressphone"]);
            Assert.AreEqual("10", providerRecordFields["contacthours"]);
            Assert.AreEqual(prefferedContactMethodId.ToString(), providerRecordFields["contactmethodid"].ToString());
            Assert.AreEqual("automation test", providerRecordFields["notes"]);
        }

        [TestProperty("JiraIssueID", "CDV6-12912")]
        [Description("Creating Hospital Ward Record with DB Helper" +
                     "Creating Hospital Bay Record with all mandatory Fields in UI" +
                     "Validate Hospital Bay Record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod03()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider_Hospital

            var _provider_Name = "Hospital_" + _currentDateString;
            var hospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            string wardName = "Ward_" + _currentDateString;
            Guid WardRecord = dbHelper.inpatientWard.CreateInpatientWard(_teamId, hospitalId, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion


            string bayName = "Bay_" + _currentDateString;

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthSetUp();

            healthSetupPage
                .WaitForHealthSetupPageToLoad()
                .ClickHospitalWardsButton();

            hospitalWardsPage
                .WaitForHospitalWardsPageToLoad()
                .SearchWardRecord(wardName)
                .OpenWardRecord(WardRecord.ToString());

            hospitalWardsRecordPage
                .WaitForHospitalWardsRecordPageToLoad(wardName)
                .NavigateToBayRoomPage();

            bayOrRoomspage
                .WaitForBayOrRoomspageToLoad()
                .ClickNewRecordButton();

            bayOrRoomsRecordpage
                .WaitForBayOrRoomsRecordPageToLoad()
                .InsertName(bayName)
                .ClickWardLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(wardName).TapSearchButton().SelectResultElement(WardRecord);

            bayOrRoomsRecordpage
                .WaitForBayOrRoomsRecordPageToLoad()
                .SelectRoomTypeByText("Bay")
                .SelectEntryPoint("Bottom")
                .Insertdescription("Automation")
                .InsertRowNo("5")
                .InsertOrderNo("6")
                .Insertmaxbedsinleftrow("3")
                .Insertmaxbedsinrighttrow("4")
                .SelectGenderType("Male")
                .ClickSaveAndCloseButton();

            bayOrRoomspage
                .WaitForBayOrRoomspageToLoad()
                .ClickRefreshButton();

            var InpatientbayRecords = dbHelper.inpatientBay.GetInpatientBayByName(bayName);
            Assert.AreEqual(1, InpatientbayRecords.Count);

            var InpatientBayRecordFields = dbHelper.inpatientBay.GetInpatientBayByID(InpatientbayRecords[0], "name", "inpatientwardid", "roomtypeid", "entrypointid",
                "description", "row", "order", "maxbedsinleftrow", "maxbedsinrightrow",
                "applicablegendertypeid");

            Assert.AreEqual(bayName, InpatientBayRecordFields["name"]);
            Assert.AreEqual(WardRecord.ToString(), InpatientBayRecordFields["inpatientwardid"].ToString());
            Assert.AreEqual(1, InpatientBayRecordFields["roomtypeid"]);
            Assert.AreEqual(2, InpatientBayRecordFields["entrypointid"]);
            Assert.AreEqual("Automation", InpatientBayRecordFields["description"]);
            Assert.AreEqual(5, InpatientBayRecordFields["row"]);
            Assert.AreEqual(6, InpatientBayRecordFields["order"]);
            Assert.AreEqual(3, InpatientBayRecordFields["maxbedsinleftrow"]);
            Assert.AreEqual(4, InpatientBayRecordFields["maxbedsinrightrow"]);
            Assert.AreEqual(1, InpatientBayRecordFields["applicablegendertypeid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12914")]
        [Description("Creating Hospital Ward and Hospital Bay Record with DB Helper" +
                     "Creating Hospital Bed Record With all mandatory Fields in UI " +
                      "Validate Hospital Bed Record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod04()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider_Hospital

            var _provider_Name = "Hospital_" + _currentDateString;
            var hospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            string wardName = "Ward_" + _currentDateString;
            Guid wardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, hospitalId, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayId = dbHelper.inpatientBay.CreateInpatientBay(_teamId, wardId, bayName, "1", "1", "6", "6");

            #endregion

            #region Bed Type

            var bedTypeId = dbHelper.inpatientBedType.GetByName("Day bed").FirstOrDefault();

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthSetUp();

            healthSetupPage
                .WaitForHealthSetupPageToLoad()
                .ClickHospitalWardsButton();

            hospitalWardsPage
                .WaitForHospitalWardsPageToLoad()
                .SearchWardRecord(wardName)
                .OpenWardRecord(wardId.ToString());

            hospitalWardsRecordPage
                .WaitForHospitalWardsRecordPageToLoad(wardName)
                .NavigateToBayRoomPage();

            bayOrRoomspage
                .WaitForBayOrRoomspageToLoad()
                .OpenBayRecord(bayId.ToString());

            bayOrRoomsRecordpage
                .WaitForBayOrRoomsRecordPageToLoad()
                .NavigateToBedPage();

            bedPage
                .WaitForBedPageToLoad()
                .ClickNewRecordButton();

            bedRecordPage
                .WaitForBedRecordPageToLoad()
                .InsertBedNo("1")
                .InsertSerialNo("456")
                .ClickBedTypeLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Day bed").TapSearchButton().SelectResultElement(bedTypeId.ToString());

            bedRecordPage
                .WaitForBedRecordPageToLoad()
                .Insertdescription("Fever")
                .SelectRowPositionByText("Left Row")
                .InsertPositionNo("4")
                .ClickSaveAndCloseButton();

            bedPage
                .WaitForBedPageToLoad()
                .ClickRefreshButton();

            var InpatientbedRecords = dbHelper.inpatientBed.GetInpatientBedByInpatientBayId(bayId);
            Assert.AreEqual(1, InpatientbedRecords.Count);

            var InpatientBedRecordFields = dbHelper.inpatientBed.GetInpatientBedByID(InpatientbedRecords[0], "bednumber", "inpatientbayid", "serialnumber", "ownerid",
                "statusid", "inpatientbedtypeid", "description", "rowpositionid", "position");

            Assert.AreEqual("1", InpatientBedRecordFields["bednumber"]);
            Assert.AreEqual(bayId.ToString(), InpatientBedRecordFields["inpatientbayid"].ToString());
            Assert.AreEqual("456", InpatientBedRecordFields["serialnumber"]);
            Assert.AreEqual(_teamId.ToString(), InpatientBedRecordFields["ownerid"].ToString());
            Assert.AreEqual(1, InpatientBedRecordFields["statusid"]);
            Assert.AreEqual(bedTypeId.ToString(), InpatientBedRecordFields["inpatientbedtypeid"].ToString());
            Assert.AreEqual("Fever", InpatientBedRecordFields["description"]);
            Assert.AreEqual(1, InpatientBedRecordFields["rowpositionid"]);
            Assert.AreEqual(4, InpatientBedRecordFields["position"]);

        }

        [TestProperty("JiraIssueID", "CDV6-12968")]
        [Description("Creating Hospital Ward and Hospital Bay Record with condition (Type =Other (No Beds)) with DB Helper" +
                     "Validate No Bed Icon is visible in the Bay/Room Related items")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod05()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Provider_Hospital

            var _provider_Name = "Hospital_" + _currentDateString;
            var hospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            var wardName = "Ward_" + _currentDateString;
            var wardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, hospitalId, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayId = dbHelper.inpatientBay.CreateInpatientBayNoBed(_teamId, wardId, bayName, "1", "1");

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToHealthSetUp();

            healthSetupPage
                .WaitForHealthSetupPageToLoad()
                .ClickHospitalWardsButton();

            hospitalWardsPage
                .WaitForHospitalWardsPageToLoad()
                .SearchWardRecord(wardName)
                .OpenWardRecord(wardId.ToString());

            hospitalWardsRecordPage
                .WaitForHospitalWardsRecordPageToLoad(wardName)
                .NavigateToBayRoomPage();

            bayOrRoomspage
                .WaitForBayOrRoomspageToLoad()
                .OpenBayRecord(bayId.ToString());

            bayOrRoomsRecordpage
                .WaitForBayOrRoomsRecordPageToLoad()
                .NavigateToRelatedItemsPage()
                .ValidateBed_IconVisible(false);
        }

        [TestProperty("JiraIssueID", "CDV6-12969")]
        [Description("Creating Case Record with all mandatory fields and (Inpatient Status = Admission)" +
                     "Validate case Record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod06()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var pastdate = currentDate.AddDays(-01);
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Provider_Hospital

            var _hospitalName = "Hospital_" + _currentDateString;
            var hospitalid = commonMethodsDB.CreateProvider(_hospitalName, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            var wardName = "Ward_" + _currentDateString;
            var wardid = dbHelper.inpatientWard.CreateInpatientWard(_teamId, hospitalid, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayid = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, wardid, bayName, 1, "1", "4", "4", 3);

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Inpatient Bed

            var bedid = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "1", "1", "1", bayid, 1, _inpatientBedTypeId, "1");

            #endregion

            #region Contact Reason

            var reasonForAdimmison = commonMethodsDB.CreateContactReason(_teamId, "Default Reason", new DateTime(2020, 1, 1), 140000001, 2, true);

            #endregion

            #region Contact Source

            var contactSourceid = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Contact Inpatient Admission Source

            var admissionSourceid = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var admissionMethodid = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            var contactReceivedByid = _systemUserId;
            var currentConsultantid = _systemUserId;
            var responsileWardid = wardid;



            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .SelectDateForm("Inpatient Case")
                .ClickReasonForAdmissionLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Reason").TapSearchButton().SelectResultElement(reasonForAdimmison);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .InsertContactReceivedDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .SelectInpatientStatusId("Admission")
                .InserContactReceivedTime("00:00")
                .ClickContactReceivedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CaseInpatientCaseUser1").TapSearchButton().SelectResultElement(contactReceivedByid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickContactSourceLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Source").TapSearchButton().SelectResultElement(contactSourceid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickAdmissionSourceLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Inpatient Admission Source").TapSearchButton().SelectResultElement(admissionSourceid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickAdmissionMethodLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Default Inpatient Admission Method").TapSearchButton().SelectResultElement(admissionMethodid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .InsertOutlineNeedForAdmission("Need For Admission ...")
                .InsertAdmissionDate(pastdate.ToString("dd'/'MM'/'yyyy"))
                .InsertAdmissionTime("00:00")
                .InsertWaitingTime(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickCurrentConsultantLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("CaseInpatientCaseUser1").TapSearchButton().SelectResultElement(currentConsultantid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickHospitalLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(_hospitalName).TapSearchButton().SelectResultElement(hospitalid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickWardLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(wardName).TapSearchButton().SelectResultElement(wardid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickBayLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery(bayName).TapSearchButton().SelectResultElement(bayid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickBedLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapSearchButton().SelectResultElement(bedid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickResponsibleWardLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TapSearchButton().SelectResultElement(responsileWardid);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .SelectRTTReferralFieldValue("No")
                .ClickSaveAndCloseButton();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickRefreshButton();

            var CaseRecords = dbHelper.Case.GetActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, CaseRecords.Count);

            var CaseRecordFields = dbHelper.Case.GetCaseByID(CaseRecords[0], "personid", "contactreceiveddatetime", "contactreceivedbyid", "ownerid", "contactreasonid",
                "inpatientcasestatusid", "contactsourceid", "inpatientadmissionsourceid", "presentingneeddetails", "inpatientadmissionmethodid",
                "currentconsultantid", "admissiondatetime", "waitingtimestartdate", "providerid", "inpatientwardid", "inpatientbayid", "inpatientbedid", "inpatientresponsiblewardid");

            Assert.AreEqual(_personID.ToString(), CaseRecordFields["personid"].ToString());
            Assert.AreEqual(currentDate.Date, ((DateTime)CaseRecordFields["contactreceiveddatetime"]).ToLocalTime().Date);
            Assert.AreEqual(contactReceivedByid.ToString(), CaseRecordFields["contactreceivedbyid"].ToString());
            Assert.AreEqual(_teamId.ToString(), CaseRecordFields["ownerid"].ToString());
            Assert.AreEqual(reasonForAdimmison.ToString(), CaseRecordFields["contactreasonid"].ToString());
            Assert.AreEqual(1, CaseRecordFields["inpatientcasestatusid"]);
            Assert.AreEqual(contactSourceid.ToString(), CaseRecordFields["contactsourceid"].ToString());
            Assert.AreEqual(admissionSourceid.ToString(), CaseRecordFields["inpatientadmissionsourceid"].ToString());
            Assert.AreEqual("Need For Admission ...", CaseRecordFields["presentingneeddetails"]);
            Assert.AreEqual(admissionMethodid.ToString(), CaseRecordFields["inpatientadmissionmethodid"].ToString());
            Assert.AreEqual(currentConsultantid.ToString(), CaseRecordFields["currentconsultantid"].ToString());
            Assert.AreEqual(pastdate.Date, ((DateTime)CaseRecordFields["admissiondatetime"]).ToLocalTime().Date);
            Assert.AreEqual(currentDate.Date, CaseRecordFields["waitingtimestartdate"]);
            Assert.AreEqual(hospitalid.ToString(), CaseRecordFields["providerid"].ToString());
            Assert.AreEqual(wardid.ToString(), CaseRecordFields["inpatientwardid"].ToString());
            Assert.AreEqual(bayid.ToString(), CaseRecordFields["inpatientbayid"].ToString());
            Assert.AreEqual(bedid.ToString(), CaseRecordFields["inpatientbedid"].ToString());
            Assert.AreEqual(responsileWardid.ToString(), CaseRecordFields["inpatientresponsiblewardid"].ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-12973")]
        [Description("Creating case Record with all mandatory fields and condition (Inpatient Status = “Awaiting Admission”)" +
                     "Validating Awaiting Admission Section is Enabled" +
                     "Validating Ward,Room/Bay,Bed Fields Are not Visible" +
                     "Validate Hospital case Record is created")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod07()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var pastdate = currentDate.AddDays(-01);
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Provider_Hospital

            var _hospitalName = "Hospital_" + _currentDateString;
            var hospitalid = commonMethodsDB.CreateProvider(_hospitalName, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            var wardName = "Ward_" + _currentDateString;
            var wardid = dbHelper.inpatientWard.CreateInpatientWard(_teamId, hospitalid, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayid = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, wardid, bayName, 1, "1", "4", "4", 3);

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Inpatient Bed

            var bedid = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "1", "1", "1", bayid, 1, _inpatientBedTypeId, "1");

            #endregion

            #region Contact Reason

            var reasonForAdimmison = commonMethodsDB.CreateContactReason(_teamId, "Default Reason", new DateTime(2020, 1, 1), 140000001, 2, true);

            #endregion

            #region Contact Source

            var contactSourceid = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Contact Inpatient Admission Source

            var admissionSourceid = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var admissionMethodid = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            var contactReceivedByid = _systemUserId;
            var currentConsultantid = _systemUserId;
            var responsileWardid = wardid;

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .SelectDateForm("Inpatient Case")
                .ClickReasonForAdmissionLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Reason")
                .TapSearchButton()
                .SelectResultElement(reasonForAdimmison.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .InsertContactReceivedDate(currentDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .SelectInpatientStatusId("Awaiting Admission")
                .InserContactReceivedTime("00:00")
                .ClickContactReceivedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CaseInpatientCaseUser1")
                .TapSearchButton()
                .SelectResultElement(contactReceivedByid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickContactSourceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Source")
                .TapSearchButton()
                .SelectResultElement(contactSourceid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .InsertOutlineNeedForAdmission("Need For Admission ...")
                .InsertWaitingTime(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ValidateAwaitingAdmission_SectionVisible(true)
                .InsertDecisionAdmitDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InserDecisionAdmitTime("00:00")
                .InsertIntendedAdmissionDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .ClickCurrentConsultantLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CaseInpatientCaseUser1")
                .TapSearchButton()
                .SelectResultElement(currentConsultantid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickHospitalLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(_hospitalName)
                .TapSearchButton()
                .SelectResultElement(hospitalid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ValidateWard_FieldVisible(false)
                .ValidateBay_FieldVisible(false)
                .ValidateBed_FieldVisible(false)
                .ClickResponsibleWardLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery(wardName)
                .TapSearchButton()
                .SelectResultElement(responsileWardid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .SelectRTTReferralFieldValue("No")
                .ClickSaveAndCloseButton();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickRefreshButton();

            var CaseRecords = dbHelper.Case.GetActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, CaseRecords.Count);

            var CaseRecordFields = dbHelper.Case.GetCaseByID(CaseRecords[0], "personid", "contactreceiveddatetime", "contactreceivedbyid", "ownerid", "contactreasonid",
                "inpatientcasestatusid", "presentingneeddetails",
                "currentconsultantid", "waitingtimestartdate", "decisiontoadmitagreeddatetime", "intendedadmissiondate", "providerid", "inpatientresponsiblewardid");

            Assert.AreEqual(_personID.ToString(), CaseRecordFields["personid"].ToString());
            Assert.AreEqual(currentDate.Date, ((DateTime)CaseRecordFields["contactreceiveddatetime"]).ToLocalTime().Date);
            Assert.AreEqual(contactReceivedByid.ToString(), CaseRecordFields["contactreceivedbyid"].ToString());
            Assert.AreEqual(_teamId.ToString(), CaseRecordFields["ownerid"].ToString());
            Assert.AreEqual(reasonForAdimmison.ToString(), CaseRecordFields["contactreasonid"].ToString());
            Assert.AreEqual(3, CaseRecordFields["inpatientcasestatusid"]);
            Assert.AreEqual("Need For Admission ...", CaseRecordFields["presentingneeddetails"]);
            Assert.AreEqual(currentConsultantid.ToString(), CaseRecordFields["currentconsultantid"].ToString());
            Assert.AreEqual(currentDate.Date, CaseRecordFields["waitingtimestartdate"]);
            Assert.AreEqual(currentDate.Date, ((DateTime)CaseRecordFields["decisiontoadmitagreeddatetime"]).ToLocalTime().Date);
            Assert.AreEqual(currentDate.Date, CaseRecordFields["intendedadmissiondate"]);
            Assert.AreEqual(hospitalid.ToString(), CaseRecordFields["providerid"].ToString());
            Assert.AreEqual(responsileWardid.ToString(), CaseRecordFields["inpatientresponsiblewardid"].ToString());
        }

        [TestProperty("JiraIssueID", "CDV6-12974")]
        [Description("Creating case Record without Filling all mandatory fields" +
                    "Validate Error Message id displayed(Some data is not correct. Please review the data in the Form.)")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod08()
        {
            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion


            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickNewRecordButton();

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .SelectDateForm("Inpatient Case")
                .ClickSaveAndCloseButton()
                .ValidateNotificationErrorMessage("Some data is not correct. Please review the data in the Form.");

        }

        [TestProperty("JiraIssueID", "CDV6-12978")]
        [Description("Creating InpatientCase Record with all mandatory fields and condition (Inpatient Status = “Awaiting Admission”)" +
            "Updating Status (Inpatient Status = “Admission”) " +
            "Validating Discharge Option is (Disabled)" +
            "Validate Inpatient Status is changed from Awaiting Admission to Admission")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod09()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var pastdate = currentDate.AddDays(-01);
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Contact Reason

            var contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Reason", new DateTime(2020, 1, 1), 140000001, 2, true);

            #endregion

            #region Contact Source

            var contactSourceid = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Contact Inpatient Admission Source

            var admissionSourceid = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var admissionMethodid = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region Provider_Hospital

            var _hospitalName = "Hospital_" + _currentDateString;
            var providerId = commonMethodsDB.CreateProvider(_hospitalName, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            var wardName = "Ward_" + _currentDateString;
            var wardid = dbHelper.inpatientWard.CreateInpatientWard(_teamId, providerId, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayid = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, wardid, bayName, 1, "1", "4", "4", 3);

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Inpatient Bed

            var bedid = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "1", "1", "1", bayid, 1, _inpatientBedTypeId, "1");

            #endregion

            #region Data Form

            var dataFormListid = dbHelper.dataForm.GetByName("InpatientCase").First();

            #endregion

            #region Case Status

            var casestatusid = dbHelper.caseStatus.GetByName("Awaiting Admission").First();

            #endregion

            var contactReceivedByid = _systemUserId;
            var currentConsultantid = _systemUserId;
            var inpatientresponsileWardid = wardid;

            //creating Inpatient Case Record With Admission Status=" Awaiting For Admission"
            Guid InpatientCaseRecord = dbHelper.Case.CreateInpatientCaseRecord(dataFormListid, _teamId, _personID, currentDate,
                contactReceivedByid,
                _systemUserId, contactReasonId, contactSourceid, "hdsa", 3, casestatusid,
                currentConsultantid, currentDate, currentDate, currentDate, currentDate, providerId,
                inpatientresponsileWardid, false, false, false, false, false, false, false, false, false, false, 2);

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(InpatientCaseRecord.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDetailsPage()
                .WaitForPersonCasesRecordPageToLoad()
                .SelectInpatientStatusId("Admission")
                .ClickAdmissionSourceLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Source")
                .TapSearchButton()
                .SelectResultElement(admissionSourceid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickAdmissionMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Default Inpatient Admission Method")
                .TapSearchButton()
                .SelectResultElement(admissionMethodid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .InsertAdmissionDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertAdmissionTime("00:00");

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickWardLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Ward_" + _currentDateString)
                .TapSearchButton()
                .SelectResultElement(wardid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickBayLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(bayid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickBedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectResultElement(bedid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickRefreshButton();

            var CaseRecords = dbHelper.Case.GetActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, CaseRecords.Count);

            var CaseRecordFields = dbHelper.Case.GetCaseByID(CaseRecords[0], "personid", "inpatientcasestatusid", "inpatientadmissionsourceid", "inpatientadmissionmethodid", "admissiondatetime", "inpatientwardid", "inpatientbayid", "inpatientbedid");

            Assert.AreEqual(_personID.ToString(), CaseRecordFields["personid"].ToString());
            Assert.AreEqual(1, CaseRecordFields["inpatientcasestatusid"]);
            Assert.AreEqual(admissionSourceid.ToString(), CaseRecordFields["inpatientadmissionsourceid"].ToString());
            Assert.AreEqual(admissionMethodid.ToString(), CaseRecordFields["inpatientadmissionmethodid"].ToString());
            Assert.AreEqual(currentDate.Date, ((DateTime)CaseRecordFields["admissiondatetime"]).ToLocalTime().Date);
            Assert.AreEqual(wardid.ToString(), CaseRecordFields["inpatientwardid"].ToString());
            Assert.AreEqual(bayid.ToString(), CaseRecordFields["inpatientbayid"].ToString());
            Assert.AreEqual(bedid.ToString(), CaseRecordFields["inpatientbedid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12979")]
        [Description("Creating InpatientCase Record with all mandatory fields and condition (Inpatient Status = “Awaiting Admission”)" +
                     "Updating Status (Inpatient Status = “Discharge ANd Closer”) " +
                     "Validate Inpatient Status is changed from Awaiting Admission to Discharge And Closer")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod10()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var pastdate = currentDate.AddDays(-01);
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Contact Reason

            var contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Reason", new DateTime(2020, 1, 1), 140000001, 2, true);

            #endregion

            #region Contact Source

            var contactSourceid = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Contact Inpatient Admission Source

            var admissionSourceid = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var admissionMethodid = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region Provider_Hospital

            var _hospitalName = "Hospital_" + _currentDateString;
            var providerId = commonMethodsDB.CreateProvider(_hospitalName, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            var wardName = "Ward_" + _currentDateString;
            var wardid = dbHelper.inpatientWard.CreateInpatientWard(_teamId, providerId, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayid = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, wardid, bayName, 1, "1", "4", "4", 3);

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Inpatient Bed

            var bedid = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "1", "1", "1", bayid, 1, _inpatientBedTypeId, "1");

            #endregion

            #region Data Form

            var dataFormListid = dbHelper.dataForm.GetByName("InpatientCase").First();

            #endregion

            #region Case Status

            var casestatusid = dbHelper.caseStatus.GetByName("Awaiting Admission").First();

            #endregion

            #region Case Closure Reason

            var caseCloseingReasonid = commonMethodsDB.CreateCaseClosureReason(_teamId, "Deceased", "", "99", new DateTime(2020, 1, 1), 140000001, true);

            #endregion

            #region Inpatient Discharge Destination

            var actualDischargeDestination = commonMethodsDB.CreateInpatientDischargeDestination(_teamId, "Usual residence", new DateTime(2020, 1, 1));

            #endregion

            #region Case Rejected Reason

            var ReasonNotAccepted = commonMethodsDB.CreateCaseRejectedReason("Inpatient Case", "99", _teamId, new DateTime(2020, 1, 1), caseCloseingReasonid, 140000001);

            #endregion

            var contactReceivedByid = _systemUserId;
            var currentConsultantid = _systemUserId;
            var inpatientresponsileWardid = wardid;


            //creating Inpatient Case Record With Admission Status=" Awaiting For Admission"
            Guid InpatientCaseRecord = dbHelper.Case.CreateInpatientCaseRecord(dataFormListid, _teamId, _personID, currentDate,
                contactReceivedByid,
                _systemUserId, contactReasonId, contactSourceid, "hdsa", 3, casestatusid,
                currentConsultantid, currentDate, currentDate, currentDate, currentDate, providerId,
                inpatientresponsileWardid, false, false, false, false, false, false, false, false, false, false, 2);

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(InpatientCaseRecord.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDetailsPage()
                .WaitForPersonCasesRecordPageToLoad()
                .SelectInpatientStatusId("Discharge Awaiting Closure")
                .InsertDischargeDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertDischargeTime("00:00")
                .ClickActualDischargeMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Deceased")
                .TapSearchButton()
                .SelectResultElement(caseCloseingReasonid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickActualDischargeDestinationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Usual residence")
                .TapSearchButton()
                .SelectResultElement(actualDischargeDestination.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickReasonNotAcceptedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Inpatient Case")
                .TapSearchButton()
                .SelectResultElement(ReasonNotAccepted.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(2000);
            var CaseRecords = dbHelper.Case.GetActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, CaseRecords.Count);

            var CaseRecordFields = dbHelper.Case.GetCaseByID(CaseRecords[0], "personid", "actualdischargedatetime", "caseclosurereasonid", "actualdischargedestinationid", "caserejectedreasonid",
                "inpatientcasestatusid");

            Assert.AreEqual(_personID.ToString(), CaseRecordFields["personid"].ToString());
            Assert.AreEqual(currentDate.Date, ((DateTime)CaseRecordFields["actualdischargedatetime"]).ToLocalTime().Date);
            Assert.AreEqual(caseCloseingReasonid.ToString(), CaseRecordFields["caseclosurereasonid"].ToString());
            Assert.AreEqual(actualDischargeDestination.ToString(), CaseRecordFields["actualdischargedestinationid"].ToString());
            Assert.AreEqual(ReasonNotAccepted.ToString(), CaseRecordFields["caserejectedreasonid"].ToString());
            Assert.AreEqual(4, CaseRecordFields["inpatientcasestatusid"]);
        }

        [TestProperty("JiraIssueID", "CDV6-12998")]
        [Description("Creating InpatientCase Record with all mandatory fields and condition (Inpatient Status = “Admission”)" +
                     "Updating Status (Inpatient Status = “Discharge ANd Closer”) " +
                     "Validate Awaiting Admission Option Is 'Disabled'" +
                     "Validate Inpatient Status is changed from Admission to Discharge And Closer")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod11()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var pastdate = currentDate.AddDays(-01);
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Contact Reason

            var contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Reason", new DateTime(2020, 1, 1), 140000001, 2, true);

            #endregion

            #region Contact Source

            var contactSourceid = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Contact Inpatient Admission Source

            var admissionSourceid = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var admissionMethodid = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region Provider_Hospital

            var _hospitalName = "Hospital_" + _currentDateString;
            var providerId = commonMethodsDB.CreateProvider(_hospitalName, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            var wardName = "Ward_" + _currentDateString;
            var wardid = dbHelper.inpatientWard.CreateInpatientWard(_teamId, providerId, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayid = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, wardid, bayName, 1, "1", "4", "4", 3);

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Inpatient Bed

            var bedid = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "1", "1", "1", bayid, 1, _inpatientBedTypeId, "1");

            #endregion

            #region Data Form

            var dataFormListid = dbHelper.dataForm.GetByName("InpatientCase").First();

            #endregion

            #region Case Status

            var casestatusid = dbHelper.caseStatus.GetByName("Awaiting Admission").First();

            #endregion

            #region Case Closure Reason

            var caseCloseingReasonid = commonMethodsDB.CreateCaseClosureReason(_teamId, "Deceased", "", "99", new DateTime(2020, 1, 1), 140000001, true);

            #endregion

            #region Inpatient Discharge Destination

            var actualDischargeDestination = commonMethodsDB.CreateInpatientDischargeDestination(_teamId, "Usual residence", new DateTime(2020, 1, 1));

            #endregion

            #region Case Rejected Reason

            var ReasonNotAccepted = commonMethodsDB.CreateCaseRejectedReason("Inpatient Case", "99", _teamId, new DateTime(2020, 1, 1), caseCloseingReasonid, 140000001);

            #endregion

            var contactReceivedByid = _systemUserId;
            var currentConsultantid = _systemUserId;
            var inpatientresponsileWardid = wardid;




            //creating Inpatient Case Record With Admission Status="Admission"
            Guid InpatientCaseRecord = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_teamId, _personID, currentDate, contactReceivedByid, "hdsa", _systemUserId, casestatusid, contactReasonId, currentDate, dataFormListid, contactSourceid, wardid, bayid, bedid, admissionSourceid, admissionMethodid, currentConsultantid, currentDate, providerId, inpatientresponsileWardid, 1, currentDate,
                false, false, false, false, false, false, false, false, false, false, 2);

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(InpatientCaseRecord.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDetailsPage()
                .WaitForPersonCasesRecordPageToLoad()
                .SelectInpatientStatusId("Discharge Awaiting Closure")
                .InsertDischargeDate(currentDate.ToString("dd''/MM'/'yyyy"))
                .InsertDischargeTime("00:00")
                .ClickActualDischargeMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Deceased")
                .TapSearchButton()
                .SelectResultElement(caseCloseingReasonid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickActualDischargeDestinationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Usual residence")
                .TapSearchButton()
                .SelectResultElement(actualDischargeDestination.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickReasonNotAcceptedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Inpatient Case")
                .TapSearchButton()
                .SelectResultElement(ReasonNotAccepted.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(2000);

            var CaseRecords = dbHelper.Case.GetActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, CaseRecords.Count);

            var CaseRecordFields = dbHelper.Case.GetCaseByID(CaseRecords[0], "personid", "actualdischargedatetime", "caseclosurereasonid", "actualdischargedestinationid", "caserejectedreasonid",
                "inpatientcasestatusid");

            Assert.AreEqual(_personID.ToString(), CaseRecordFields["personid"].ToString());
            Assert.AreEqual(4, CaseRecordFields["inpatientcasestatusid"]);
            Assert.AreEqual(currentDate.Date, ((DateTime)CaseRecordFields["actualdischargedatetime"]).ToLocalTime().Date);
            Assert.AreEqual(caseCloseingReasonid.ToString(), CaseRecordFields["caseclosurereasonid"].ToString());
            Assert.AreEqual(actualDischargeDestination.ToString(), CaseRecordFields["actualdischargedestinationid"].ToString());
            Assert.AreEqual(ReasonNotAccepted.ToString(), CaseRecordFields["caserejectedreasonid"].ToString());

        }

        [TestProperty("JiraIssueID", "CDV6-12999")]
        [Description("Creating InpatientCase Record with all mandatory fields and condition (Inpatient Status = “Admission”)" +
                    "Updating Status (Inpatient Status = “Discharge”) " +
                    "Validating When Inpatient Status=Discharge Edit Field is blocked in UI)" +
                    "Validate Inpatient Status is changed from Admission to Discharge")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod12()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var pastdate = currentDate.AddDays(-01);
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Contact Reason

            var contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Reason", new DateTime(2020, 1, 1), 140000001, 2, true);

            #endregion

            #region Contact Source

            var contactSourceid = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Contact Inpatient Admission Source

            var admissionSourceid = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var admissionMethodid = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region Provider_Hospital

            var _hospitalName = "Hospital_" + _currentDateString;
            var providerId = commonMethodsDB.CreateProvider(_hospitalName, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            var wardName = "Ward_" + _currentDateString;
            var wardid = dbHelper.inpatientWard.CreateInpatientWard(_teamId, providerId, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayid = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, wardid, bayName, 1, "1", "4", "4", 3);

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Inpatient Bed

            var bedid = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "1", "1", "1", bayid, 1, _inpatientBedTypeId, "1");

            #endregion

            #region Data Form

            var dataFormListid = dbHelper.dataForm.GetByName("InpatientCase").First();

            #endregion

            #region Case Status

            var casestatusid = dbHelper.caseStatus.GetByName("Admission").First();

            #endregion

            #region Case Closure Reason

            var caseCloseingReasonid = commonMethodsDB.CreateCaseClosureReason(_teamId, "Deceased", "", "99", new DateTime(2020, 1, 1), 140000001, true);

            #endregion

            #region Inpatient Discharge Destination

            var actualDischargeDestination = commonMethodsDB.CreateInpatientDischargeDestination(_teamId, "Usual residence", new DateTime(2020, 1, 1));

            #endregion

            #region Case Rejected Reason

            var ReasonNotAccepted = commonMethodsDB.CreateCaseRejectedReason("Inpatient Case", "99", _teamId, new DateTime(2020, 1, 1), caseCloseingReasonid, 140000001);

            #endregion

            var contactReceivedByid = _systemUserId;
            var currentConsultantid = _systemUserId;
            var inpatientresponsileWardid = wardid;


            //creating Inpatient Case Record With Admission Status="Admission"

            Guid InpatientCaseRecord = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_teamId, _personID, currentDate, contactReceivedByid, "hdsa", _systemUserId, casestatusid, contactReasonId, currentDate, dataFormListid, contactSourceid, wardid, bayid, bedid, admissionSourceid, admissionMethodid, currentConsultantid, currentDate, providerId, inpatientresponsileWardid, 1, currentDate,
                 false, false, false, false, false, false, false, false, false, false, 2);

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(InpatientCaseRecord.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDetailsPage()
                .WaitForPersonCasesRecordPageToLoad()
                .SelectInpatientStatusId("Discharge")
                .InsertDischargeDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertDischargeTime("00:00")
                .ClickActualDischargeMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Deceased")
                .TapSearchButton()
                .SelectResultElement(caseCloseingReasonid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickActualDischargeDestinationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Usual residence")
                .TapSearchButton()
                .SelectResultElement(actualDischargeDestination.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickReasonNotAcceptedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Inpatient Case")
                .TapSearchButton()
                .SelectResultElement(ReasonNotAccepted.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickSaveAndCloseButton();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickRefreshButton();

            System.Threading.Thread.Sleep(2000);

            var CaseRecords = dbHelper.Case.GetInActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, CaseRecords.Count);


            var CaseRecordFields = dbHelper.Case.GetCaseByID(CaseRecords[0], "personid", "actualdischargedatetime", "caseclosurereasonid", "actualdischargedestinationid", "caserejectedreasonid",
                "inpatientcasestatusid", "inactive");

            Assert.AreEqual(_personID.ToString(), CaseRecordFields["personid"].ToString());
            Assert.AreEqual(2, CaseRecordFields["inpatientcasestatusid"]);
            Assert.AreEqual(currentDate.Date, ((DateTime)CaseRecordFields["actualdischargedatetime"]).ToLocalTime().Date.Date);
            Assert.AreEqual(caseCloseingReasonid.ToString(), CaseRecordFields["caseclosurereasonid"].ToString());
            Assert.AreEqual(actualDischargeDestination.ToString(), CaseRecordFields["actualdischargedestinationid"].ToString());
            Assert.AreEqual(ReasonNotAccepted.ToString(), CaseRecordFields["caserejectedreasonid"].ToString());
            Assert.AreEqual(true, CaseRecordFields["inactive"]);

        }

        [TestProperty("JiraIssueID", "CDV6-13024")]
        [Description("Creating InpatientCase Record with all mandatory fields and condition (Inpatient Status = “Admission”)" +
                    "Updating Status (Inpatient Status = “Discharge ANd Closer”) " +
                    "Updating Status (Inpatient Status = “Discharge”) " +
                    "Validate Inpatient Status is changed from Discharge And Closer to Discharge")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod13()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var pastdate = currentDate.AddDays(-01);
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Contact Reason

            var contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Reason", new DateTime(2020, 1, 1), 140000001, 2, true);

            #endregion

            #region Contact Source

            var contactSourceid = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Contact Inpatient Admission Source

            var admissionSourceid = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var admissionMethodid = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region Provider_Hospital

            var _hospitalName = "Hospital_" + _currentDateString;
            var providerId = commonMethodsDB.CreateProvider(_hospitalName, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            var wardName = "Ward_" + _currentDateString;
            var wardid = dbHelper.inpatientWard.CreateInpatientWard(_teamId, providerId, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayid = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, wardid, bayName, 1, "1", "4", "4", 3);

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Inpatient Bed

            var bedid = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "1", "1", "1", bayid, 1, _inpatientBedTypeId, "1");

            #endregion

            #region Data Form

            var dataFormListid = dbHelper.dataForm.GetByName("InpatientCase").First();

            #endregion

            #region Case Status

            var casestatusid = dbHelper.caseStatus.GetByName("Awaiting Admission").First();

            #endregion

            #region Case Closure Reason

            var caseCloseingReasonid = commonMethodsDB.CreateCaseClosureReason(_teamId, "Deceased", "", "99", new DateTime(2020, 1, 1), 140000001, true);

            #endregion

            #region Inpatient Discharge Destination

            var actualDischargeDestination = commonMethodsDB.CreateInpatientDischargeDestination(_teamId, "Usual residence", new DateTime(2020, 1, 1));

            #endregion

            #region Case Rejected Reason

            var ReasonNotAccepted = commonMethodsDB.CreateCaseRejectedReason("Inpatient Case", "99", _teamId, new DateTime(2020, 1, 1), caseCloseingReasonid, 140000001);

            #endregion

            var contactReceivedByid = _systemUserId;
            var currentConsultantid = _systemUserId;
            var inpatientresponsileWardid = wardid;



            //creating Inpatient Case Record With Admission Status="Admission"
            Guid InpatientCaseRecord = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_teamId, _personID, currentDate, contactReceivedByid, "hdsa", _systemUserId, casestatusid, contactReasonId, currentDate, dataFormListid, contactSourceid, wardid, bayid, bedid, admissionSourceid, admissionMethodid, currentConsultantid, currentDate, providerId, inpatientresponsileWardid, 1, currentDate,
                 false, false, false, false, false, false, false, false, false, false, 2);

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(InpatientCaseRecord.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDetailsPage()
                .WaitForPersonCasesRecordPageToLoad()
                .SelectInpatientStatusId("Discharge Awaiting Closure")
                .InsertDischargeDate(currentDate.ToString("dd'/'MM'/'yyyy"))
                .InsertDischargeTime("00:00")
                .ClickActualDischargeMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Deceased")
                .TapSearchButton()
                .SelectResultElement(caseCloseingReasonid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickActualDischargeDestinationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Usual residence")
                .TapSearchButton()
                .SelectResultElement(actualDischargeDestination.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickReasonNotAcceptedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Inpatient Case")
                .TapSearchButton()
                .SelectResultElement(ReasonNotAccepted.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickSaveAndCloseButton();
            System.Threading.Thread.Sleep(2000);

            var CaseRecords = dbHelper.Case.GetActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, CaseRecords.Count);

            var CaseRecordFields = dbHelper.Case.GetCaseByID(CaseRecords[0], "personid", "inpatientcasestatusid");

            Assert.AreEqual(_personID.ToString(), CaseRecordFields["personid"].ToString());
            Assert.AreEqual(4, CaseRecordFields["inpatientcasestatusid"]);

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .ClickRefreshButton()
                .OpenCaseRecord(InpatientCaseRecord.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDetailsPage()
                .WaitForPersonCasesRecordPageToLoad()
                .SelectInpatientStatusId("Discharge")
                .ClickSaveButton()
                .WaitForPersonCasesRecordPageToLoad();

            System.Threading.Thread.Sleep(2000);

            var UpdatedCaseRecords = dbHelper.Case.GetInActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, UpdatedCaseRecords.Count);

            var UpdatedCaseRecordFields = dbHelper.Case.GetCaseByID(UpdatedCaseRecords[0], "personid", "inpatientcasestatusid");

            Assert.AreEqual(_personID.ToString(), UpdatedCaseRecordFields["personid"].ToString());
            Assert.AreEqual(2, UpdatedCaseRecordFields["inpatientcasestatusid"]);

        }

        [TestProperty("JiraIssueID", "CDV6-13032")]
        [Description("Creating InpatientCase Record with all mandatory fields and condition (Inpatient Status = “Admission”)" +
                     "Updating Status (Inpatient Status = “Discharge”) " +
                     "Validating Activate Option Is working" +
                     "Validate Inpatient Status is changed from Discharge to Admission")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void CaseInpatientCase_UITestMethod14()
        {
            var currentDate = commonMethodsHelper.GetCurrentDateWithoutCulture().Date;
            var pastdate = currentDate.AddDays(-01);
            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));
            var personNumber = (int)(dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"]);

            #endregion

            #region Contact Reason

            var contactReasonId = commonMethodsDB.CreateContactReason(_teamId, "Default Reason", new DateTime(2020, 1, 1), 140000001, 2, true);

            #endregion

            #region Contact Source

            var contactSourceid = commonMethodsDB.CreateContactSourceIfNeeded("Default Source", _teamId);

            #endregion

            #region Contact Inpatient Admission Source

            var admissionSourceid = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Default Inpatient Admission Source", new DateTime(2020, 1, 1));

            #endregion

            #region Inpatient Admission Method

            var admissionMethodid = commonMethodsDB.CreateInpatientAdmissionMethod("Default Inpatient Admission Method", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region Provider_Hospital

            var _hospitalName = "Hospital_" + _currentDateString;
            var providerId = commonMethodsDB.CreateProvider(_hospitalName, _teamId);

            #endregion

            #region System User (Ward Manager)

            var wardManagerId = commonMethodsDB.CreateSystemUserRecord("WardManagerUser1", "WardManager", "User1", "Passw0rd_!", _businessUnitId, _teamId, _languageId, _authenticationproviderid);

            #endregion

            #region Hospital Ward Specialties

            var wardSpecialtiesId = dbHelper.inpatientWardSpecialty.GetByName("Community Care").First();

            #endregion

            #region Inpatient Ward

            var wardName = "Ward_" + _currentDateString;
            var wardid = dbHelper.inpatientWard.CreateInpatientWard(_teamId, providerId, wardManagerId, wardSpecialtiesId, wardName, currentDate);

            #endregion

            #region Inpatient Bay

            string bayName = "Bay_" + _currentDateString;
            var bayid = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, wardid, bayName, 1, "1", "4", "4", 3);

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Inpatient Bed

            var bedid = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "1", "1", "1", bayid, 1, _inpatientBedTypeId, "1");

            #endregion

            #region Data Form

            var dataFormListid = dbHelper.dataForm.GetByName("InpatientCase").First();

            #endregion

            #region Case Status

            var casestatusid = dbHelper.caseStatus.GetByName("Awaiting Admission").First();

            #endregion

            #region Case Closure Reason

            var caseCloseingReasonid = commonMethodsDB.CreateCaseClosureReason(_teamId, "Deceased", "", "99", new DateTime(2020, 1, 1), 140000001, true);

            #endregion

            #region Inpatient Discharge Destination

            var actualDischargeDestination = commonMethodsDB.CreateInpatientDischargeDestination(_teamId, "Usual residence", new DateTime(2020, 1, 1));

            #endregion

            #region Case Rejected Reason

            var ReasonNotAccepted = commonMethodsDB.CreateCaseRejectedReason("Inpatient Case", "99", _teamId, new DateTime(2020, 1, 1), caseCloseingReasonid, 140000001);

            #endregion

            #region Case Reopen Reason

            var reopeningCaseReason = commonMethodsDB.CreateCaseReopenReason(_teamId, "Closed in Error - Test", "109", new DateTime(2020, 1, 1), 140000001);

            #endregion

            var contactReceivedByid = _systemUserId;
            var currentConsultantid = _systemUserId;
            var inpatientresponsileWardid = wardid;


            //creating Inpatient Case Record With Admission Status="Admission"
            Guid InpatientCaseRecord = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_teamId, _personID, currentDate, contactReceivedByid, "hdsa", _systemUserId, casestatusid, contactReasonId, currentDate, dataFormListid, contactSourceid, wardid, bayid, bedid, admissionSourceid, admissionMethodid, currentConsultantid, currentDate, providerId, inpatientresponsileWardid, 1, currentDate,
                 false, false, false, false, false, false, false, false, false, false, 2);

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToPeopleSection();

            peoplePage
                .WaitForPeoplePageToLoad()
                .SearchPersonRecordByID(personNumber.ToString(), _personID.ToString())
                .OpenPersonRecord(_personID.ToString());

            personRecordPage
                .WaitForPersonRecordPageToLoad()
                .TapCasesTab();

            personCasesPage
                .WaitForPersonCasesPageToLoad()
                .OpenCaseRecord(InpatientCaseRecord.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .NavigateToDetailsPage()
                .WaitForPersonCasesRecordPageToLoad()
                .SelectInpatientStatusId("Discharge")
                .InsertDischargeDate(currentDate.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture))
                .InsertDischargeTime("00:00")
                .ClickActualDischargeMethodLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Deceased")
                .TapSearchButton()
                .SelectResultElement(caseCloseingReasonid.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickActualDischargeDestinationLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Usual residence")
                .TapSearchButton()
                .SelectResultElement(actualDischargeDestination.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickReasonNotAcceptedLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Inpatient Case")
                .TapSearchButton()
                .SelectResultElement(ReasonNotAccepted.ToString());

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickSaveButton();

            System.Threading.Thread.Sleep(2000);

            var UpdatedCaseRecords = dbHelper.Case.GetInActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, UpdatedCaseRecords.Count);

            var UpdatedCaseRecordFields = dbHelper.Case.GetCaseByID(UpdatedCaseRecords[0], "personid", "inpatientcasestatusid");

            Assert.AreEqual(_personID.ToString(), UpdatedCaseRecordFields["personid"].ToString());
            Assert.AreEqual(2, UpdatedCaseRecordFields["inpatientcasestatusid"]);

            personCasesRecordPage
                .WaitForPersonCasesRecordPageToLoad()
                .ClickActivateButton();

            alertPopup
                .WaitForAlertPopupToLoad()
                .TapOKButton();

            personCasesRecordPage
                .WaitForReactiveRecordPageToLoad()
                .SelectCaseStatus("Admission")
                .ClickReopenReasonLookUp();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Closed in Error - Test")
                .TapSearchButton()
                .SelectResultElement(reopeningCaseReason.ToString());

            personCasesRecordPage
                .WaitForReactiveRecordPageToLoad()
                .ClickOKButton();

            System.Threading.Thread.Sleep(2000);

            var ReopnedCaseRecords = dbHelper.Case.GetActiveCasesByPersonID(_personID);
            Assert.AreEqual(1, ReopnedCaseRecords.Count);

            var ReopnedCaseRecordFields = dbHelper.Case.GetCaseByID(ReopnedCaseRecords[0], "personid", "inpatientcasestatusid");

            Assert.AreEqual(_personID.ToString(), ReopnedCaseRecordFields["personid"].ToString());
            Assert.AreEqual(1, ReopnedCaseRecordFields["inpatientcasestatusid"]);

        }

        [Description("Method will return the name of all tests and the Description of each one")]
        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/ACC-422

        [TestProperty("JiraIssueID", "CDV6-21350")]
        [Description("Reactivation of a discharged inpatient case")]
        [TestMethod]
        [TestCategory("IntegrationTestLevel3_MentalHealth_AWS")]
        public void Case_ReActivate_UITestMethod01()
        {
            #region Current Date Time

            var _currentDateString = DateTime.Now.ToString("yyyyMMddHHmmss");

            #endregion

            #region Ethnicity

            var _ethnicityId = commonMethodsDB.CreateEthnicity(_teamId, "Irish", new DateTime(2020, 1, 1));

            #endregion

            #region Hospital Ward Specialty

            var _wardSpecialityId = dbHelper.inpatientWardSpecialty.GetByName("Adult Acute")[0];

            #endregion

            #region Inpatient Bed Type

            var _inpatientBedTypeId = dbHelper.inpatientBedType.GetByName("Clinitron")[0];

            #endregion

            #region Case Status

            var _admission_CaseStatusId = dbHelper.caseStatus.GetByName("Admission").FirstOrDefault();
            var _awaitingAdmission_caseStatusId = dbHelper.caseStatus.GetByName("Awaiting Admission").FirstOrDefault();

            #endregion

            #region Contact Reason

            var _contactReasonId = commonMethodsDB.CreateContactReasonIfNeeded("Default", _teamId);

            #endregion

            #region Contact Source

            var _contactSourceId = commonMethodsDB.CreateContactSourceIfNeeded("Default", _teamId);

            #endregion

            #region Data Form

            var _dataFormId = dbHelper.dataForm.GetByName("InpatientCase")[0];

            #endregion

            #region Provider_Hospital

            var _provider_Name = "Hospital_" + _currentDateString;
            var _provider_HospitalId = commonMethodsDB.CreateProvider(_provider_Name, _teamId);

            #endregion

            #region Ward

            var _inpatientWardName = "Ward_" + _currentDateString;
            var _inpatientWardId = dbHelper.inpatientWard.CreateInpatientWard(_teamId, _provider_HospitalId, _systemUserId, _wardSpecialityId, _inpatientWardName, new DateTime(2020, 1, 1));

            #endregion

            #region Bay/Room

            var _inpatientBayName = "Bay_" + _currentDateString;
            var _inpatientBayId = dbHelper.inpatientBay.CreateInpatientCaseBay(_teamId, _inpatientWardId, _inpatientBayName, 1, "4", "4", "4", 2);

            #endregion

            #region Bed

            var _inpatientBedId = dbHelper.inpatientBed.CreateInpatientBed(_teamId, "12665", "4", "4", _inpatientBayId, 1, _inpatientBedTypeId, "4");

            #endregion

            #region Contact Inpatient Admission Source

            var _inpatientAdmissionSourceId = commonMethodsDB.CreateInpatientAdmissionSource(_teamId, "Other Hospital (General)", new DateTime(2020, 1, 1));

            #endregion

            #region InpatientAdmissionMethod

            var _inpationAdmissionMethodId = commonMethodsDB.CreateInpatientAdmissionMethod("General Admission", _teamId, _businessUnitId, new DateTime(2020, 1, 1));

            #endregion

            #region Case Closure Reason

            var _caseClosureReasonId = commonMethodsDB.CreateCaseClosureReason(_teamId, "Refused Admission", "", "", new DateTime(2020, 1, 1), 140000001, true);

            #endregion

            #region Inpatient Discharge Destination

            var _actualDischargeDestinationId = commonMethodsDB.CreateInpatientDischargeDestination(_teamId, "Usual residence", new DateTime(2020, 1, 1));

            #endregion

            #region Case Reopen Reason

            var _caseReopenReasonID = commonMethodsDB.CreateCaseReopenReason(_teamId, "Closed in error", "", commonMethodsHelper.GetCurrentDateWithoutCulture().Date, 140000001);

            #endregion

            #region Person

            var personFirstName = DateTime.Now.ToString("yyyyMMddHHmmss");
            var _personFullName = personFirstName + " AutomationLeaveAWOLLastName";
            var _personID = commonMethodsDB.CreatePersonRecord(personFirstName, "AutomationLeaveAWOLLastName", _ethnicityId, _teamId, new DateTime(2000, 1, 2));

            #endregion

            #region Inpatient Case record

            var _caseId = dbHelper.Case.CreateInpatientCaseRecordWithStatusAsAdmission(_teamId, _personID, DateTime.Now.Date, _systemUserId,
                "needs ...", _systemUserId, _admission_CaseStatusId, _contactReasonId, DateTime.Now.Date, _dataFormId, _contactSourceId,
                _inpatientWardId, _inpatientBayId, _inpatientBedId, _inpatientAdmissionSourceId, _inpationAdmissionMethodId, _systemUserId,
                DateTime.Now.Date, _provider_HospitalId, _inpatientWardId, 1, DateTime.Now.Date, false, false, false, false, false, false,
                false, false, false, false);
            var _caseNumber = (string)dbHelper.Case.GetCaseByID(_caseId, "casenumber")["casenumber"];

            var inpatientCaseStatusId = 2;//Discharge
            DateTime actualdischargedatetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dbHelper.Case.UpdateCaseRecordToDischarge(_caseId, inpatientCaseStatusId, actualdischargedatetime, _caseClosureReasonId, _actualDischargeDestinationId);

            #endregion

            loginPage
                .GoToLoginPage()
                .Login("CaseInpatientCaseUser1", "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToCasesSection();

            casesPage
                .WaitForCasesPageToLoad()
                .SearchByCaseNumber(_caseNumber, _caseId.ToString())
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .TapDetailsLink()
                .ClickActivateButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Are you sure you want to activate this record? To continue, click ok.").TapOKButton();

            reactivateCasePopup
                .WaitForReactivateCasePopupToLoad()
                .SelectInpatientStatus("Discharge Awaiting Closure")
                .ClickReasonForReopeningLookupButton();

            lookupPopup.WaitForLookupPopupToLoad().TypeSearchQuery("Closed in error").TapSearchButton().SelectResultElement(_caseReopenReasonID);

            reactivateCasePopup
                .WaitForReactivateCasePopupToLoad()
                .TapOkButton();

            caseRecordPage
                .SwitchToDefaultFrameAndWaitForCaseRecordPageToLoad()
                .TapDetailsLink();

            var inactive = (bool)(dbHelper.Case.GetCaseByID(_caseId, "inactive")["inactive"]);
            Assert.IsFalse(inactive);

            caseRecordPage
                .SelectInpatientStatus("Discharge")
                .ClickSaveAndCloseButton();

            casesPage
                .WaitForCasesPageToLoad()
                .OpenCaseRecord(_caseId.ToString(), _caseNumber);

            caseRecordPage
                .WaitForCaseRecordPageToLoad()
                .TapDetailsLink()
                .ValidateInpatientCaseDisabled();
        }

        #endregion


    }
}
