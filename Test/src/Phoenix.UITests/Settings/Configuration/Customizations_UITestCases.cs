//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Phoenix.UITests.Settings.Configuration
//{
//    [TestClass]
//    public class Customizations_UITestCases : FunctionalTest
//    {

//        #region https://advancedcsg.atlassian.net/browse/CDV6-16004
//        #region Properties
//        private string EnvironmentName;
//        private Guid _authenticationproviderid;        
//        private Guid _languageId;
//        private Guid _careProviders_BusinessUnitId;
//        private Guid _careProviders_TeamId;
//        private Guid adminUserId;
//        private string loginUsername;

//        #endregion

//        [TestInitialize()]
//        public void TestSetup()
//        {
//            try
//            {

//                #region Environment Name
//                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];

//                #endregion

//                #region Authentication Provider

//                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

//                #endregion

//                #region Language

//                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
//                if (!language)
//                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
//                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

//                #endregion Language

//                #region Business Unit
//                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
//                if (!businessUnitExists)
//                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
//                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];


//                #endregion

//                #region Team

//                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
//                if (!teamsExist)
//                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
//                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

//                #endregion

//                #region Create Admin user

//                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2").Any();
//                if (!adminUserExists)
//                {
//                    adminUserId = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_2", "CW", "Admin Test User 2", "CW Admin Test User 2", "Passw0rd_!", "CW_Admin_Test_User_2@somemail.com", "CW_Admin_Test_User_2@othermail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

//                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
//                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System User - Secure Fields (Edit)").First();
//                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId, systemAdministratorSecurityProfileId);
//                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(adminUserId, systemUserSecureFieldsSecurityProfileId);
//                }



//                adminUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2").FirstOrDefault();
//                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);
//                loginUsername = (string) dbHelper.systemUser.GetSystemUserBySystemUserID(adminUserId, "username")["username"];

//                #endregion

//            }
//            catch
//            {
//                if (driver != null)
//                    driver.Quit();

//                this.ShutDownAllProcesses();

//                throw;
//            }

//        }

//        // Inactivating both tests -- Not Require 

//        //[TestProperty("JiraIssueID", "CDV6-16168")]
//        //[Description("Login to CD. Navigate to Settings > Configuration > Customizations > Business Objects > PersonAboutMe Record"+
//        //            "Verify that the fields are set with the values as mentioned in the description."+
//        //            "Navigate to Menu > Related Records > Business Object Fields." +
//        //            "Verify that the Business object fields - Label name, Tooltip, Mandatory, Default value, Field Type, Searchable? are as per the document attached for the PersonAboutMe")]
//        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        //public void BusinessObjectDetails_FieldValidation_UITestMethod001()
//        //{

//        //    #region Business Object Fields Record Fields Data for Person About Me
//        //    List<string> titleFields = new List<string>() { "Title","", "1", "Single Line Textbox","yes", "no"};
//        //    List<string> personFields = new List<string>() { "Person","Unique identifier of the person record", "1", "Business Object Reference", "yes", "yes"};
//        //    //List<string> responsibleUserIdFields = new List<string>() { "Responsible User","", "0", "Business Object Reference", "yes", "yes"};
//        //    List<string> ownerIdFields = new List<string>() { "Responsible Team","Defines which team is responsible for this record", "1", "Record Owner", "yes", "no"};
//        //    List<string> dateFields = new List<string>() { "Date","The date this information was captured", "1", "Date", "yes", "no" };

//        //    List<string> supportedToWriteThisFields = new List<string>() 
//        //    { "Supported To Write This By", "The person/worker that assisted in capturing this information", "1", "Multi Lookup", "yes", "no"};
//        //    List<string> capacityEstablishedFields = new List<string>() 
//        //    { "Capacity Established", "Indicates capacity has been established to grant consent for audio/video recording", "1", "Boolean", "yes", "yes"};
//        //    List<string> consentGrantedFields = new List<string>()
//        //    { "Consent Granted", "Indicates the person has given consent for audio/video recordings to be used for About Me", "1", "Boolean", "yes", "yes"};
//        //    List<string> aboutMeFields = new List<string>() 
//        //    { "About Me", "This is a record of the things that the person feels it is important to communicate to others providing support and care", "0", "Multiline Large Data Textbox", "no", "no"};
//        //    List<string> aboutMeMediaFields = new List<string>() 
//        //    { "About Me Media", "This is a media file containing a recording of the person using their own words to relate the things they feel are important to share to those providing support and care", "0", "Video File", "no", "no" };
//        //    List<string> whatIsMostImportantToMeFields = new List<string>()
//        //    { "What Is Most Important To Me", "This is a record of the things that the person feels it is important to them", "0", "Multiline Large Data Textbox", "no", "no"};
//        //    List<string> whatIsMostImportantToMeMediaFields = new List<string>() 
//        //    { "What Is Most Important To Me Media", "This is a media file containing a recording of the person using their own words to relate the things they feel are important", "0", "Video File", "no", "no"};
//        //    List<string> peopleWhoAreImportantToMeFields = new List<string>() 
//        //    { "People Who Are Important To Me", "This is a record of the people the person feels are important to them", "0", "Multiline Large Data Textbox", "no", "no"};
//        //    List<string> PeopleWhoAreimportantToMeMediaFields = new List<string>()
//        //    { "People Who Are Important To Me Media", "This is a media file containing a recording of the person using their own words to convey the people they feel are important to them", "0", "Video File", "no", "no"};
//        //    List<string> howICommunicateAndHowToCommunicateWithMeFields = new List<string>()
//        //    { "How I Communicate And How To Communicate With Me", "This is a record of the persons preferred communication methods and needs", "0", "Multiline Large Data Textbox", "no", "no"};
//        //    List<string> howICommunicateAndHowToCommunicateWithMeMediaFields = new List<string>()
//        //    { "How I Communicate And How To Communicate With Me Media", "This is a media file containing a recording of the person using their own words to convey their preferred communications methods and needs", "0", "Video File", "no", "no"};
//        //    List<string> pleaseDoAndPleaseDoNotFields = new List<string>()
//        //    { "Please Do And Please Do Not", "This is a record of things the person would like those supporting them to do and not do", "0", "Multiline Large Data Textbox", "no", "no"};
//        //    List<string> pleaseDoAndPleaseDoNotMediaFields = new List<string>()
//        //    { "Please Do And Please Do Not Media", "This is a media file containing a recording of the person using their own words to convey things the person would like those supporting them to do and not do", "0", "Video File", "yes", "no"};
//        //    List<string> myWellnessFields = new List<string>()
//        //    { "My Wellness", "This is a record of how the person feels from a good day through to a bad day", "0", "Multiline Large Data Textbox", "no", "no"};
//        //    List<string> myWellnessMediaFields = new List<string>()
//        //    { "My Wellness Media", "This is a media file containing a recording of the person using their own words to convey their wellness", "0", "Video File", "yes", "no"};
//        //    List<string> howAndWhenToSupportMeFields = new List<string>()
//        //    { "How And When To Support Me", "This is a record of how and when to support the person", "0", "Multiline Large Data Textbox", "no", "no"};
//        //    List<string> howAndWhenToSupportMeMediaFields = new List<string>()
//        //    { "How And When To Support Me Media", "This is a media file containing a recording of the person using their own words to convey how and when they want to be supported", "0", "Video File", "yes", "no"};
//        //    List<string> alsoWorthKnowingAboutMeFields = new List<string>()
//        //    { "Also Worth Knowing About Me", "This is a record of other information the person feels is worth knowing about them", "0", "Multiline Large Data Textbox", "no", "no"};
//        //    List<string> alsoWorthKnowingAboutMeMediaFields = new List<string>()
//        //    { "Also Worth Knowing About Me Media", "This is a media file containing a recording of the person using their own words to convey other information they feel is worth knowing about them", "0", "Video File", "yes", "no"};
//        //    List<string> physicalCharacteristicsFields = new List<string>()
//        //    { "Physical Characteristics", "This is a record of physical characteristics the person believes would help those supporting them to recognise them", "0", "Multiline Large Data Textbox", "no", "no"};
//        //    List<string> physicalCharacteristicsMediaFields = new List<string>()
//        //    { "Physical Characteristics Media", "This is a media file containing a recording of the person using their own words to convey any physical characteristics that would help those supporting them to identify them", "0", "Video File", "yes", "no"};
//        //    List<string> aboutMeSetupIdFields = new List<string>()
//        //    { "About Me Setup Record", "Reference to the About Me Setup record used for creation of this About me record", "0", "Business Object Reference", "yes", "yes"};

//        //    Dictionary<string, List<string>> Map = new Dictionary<string, List<string>>();

//        //    Map.Add("Title", titleFields);
//        //    Map.Add("PersonId", personFields);
//        //    //Map.Add("ResponsibleUserId", responsibleUserIdFields);
//        //    Map.Add("OwnerId", ownerIdFields);
//        //    Map.Add("Date", dateFields);

//        //    Map.Add("SupportedToWriteThisById", supportedToWriteThisFields);
//        //    Map.Add("CapacityEstablished", capacityEstablishedFields);
//        //    Map.Add("ConsentGranted", consentGrantedFields);
//        //    Map.Add("AboutMe", aboutMeFields);
//        //    Map.Add("AboutMeMedia", aboutMeMediaFields);

//        //    Map.Add("WhatIsMostImportantToMe", whatIsMostImportantToMeFields);
//        //    Map.Add("WhatIsMostImportantToMeMedia", whatIsMostImportantToMeMediaFields);
//        //    Map.Add("PeopleWhoAreImportantToMe", peopleWhoAreImportantToMeFields);
//        //    Map.Add("PeopleWhoAreimportantToMeMedia", PeopleWhoAreimportantToMeMediaFields);
//        //    Map.Add("HowICommunicateAndHowToCommunicateWithMe", howICommunicateAndHowToCommunicateWithMeFields);
//        //    Map.Add("HowICommunicateAndHowToCommunicateWithMeMedia", howICommunicateAndHowToCommunicateWithMeMediaFields);

//        //    Map.Add("PleaseDoAndPleaseDoNot", pleaseDoAndPleaseDoNotFields);
//        //    Map.Add("PleaseDoAndPleaseDoNotMedia", pleaseDoAndPleaseDoNotMediaFields);
//        //    Map.Add("MyWellness", myWellnessFields);
//        //    Map.Add("MyWellnessMedia", myWellnessMediaFields);
//        //    Map.Add("HowAndWhenToSupportMe", howAndWhenToSupportMeFields);
//        //    Map.Add("HowAndWhenToSupportMeMedia", howAndWhenToSupportMeMediaFields);
//        //    Map.Add("AlsoWorthKnowingAboutMe", alsoWorthKnowingAboutMeFields);
//        //    Map.Add("AlsoWorthKnowingAboutMeMedia", alsoWorthKnowingAboutMeMediaFields);
//        //    Map.Add("PhysicalCharacteristics", physicalCharacteristicsFields);
//        //    Map.Add("PhysicalCharacteristicsMedia", physicalCharacteristicsMediaFields);
//        //    Map.Add("AboutMeSetupId", aboutMeSetupIdFields);            

//        //    #endregion

//        //    var personAboutMeBusinessObjectID = dbHelper.businessObject.GetBusinessObjectByName("PersonAboutMe")[0];

//        //    loginPage
//        //        .GoToLoginPage()
//        //        .Login(loginUsername, "Passw0rd_!", EnvironmentName)
//        //    //    .WaitFormHomePageToLoad();
//        //    .WaitForCareProvidermHomePageToLoad();

//        //    mainMenu
//        //        .WaitForMainMenuToLoad()
//        //        .NavigateToCustomizationsSection();

//        //    customizationsPage
//        //        .WaitForCustomizationsPageToLoad()
//        //        .ClickBusinessObjectsButton();

//        //    businessObjectsPage
//        //        .WaitForBusinessObjectsPageToLoad()
//        //        .InsertQuickSearchText("PersonAboutMe")
//        //        .ClickQuickSearchButton()
//        //        .OpenRecord(personAboutMeBusinessObjectID.ToString());

//        //    businessObjectRecordPage
//        //        .WaitForBusinessObjectRecordPageToLoad()
//        //        .ValidateTitleFieldRequiredFieldVisible("1")
//        //        .ValidateOwnershipFieldVisible("Team Based")
//        //        //.ValidateEnableResponsibleUserFieldVisible("1")
//        //        .ValidateCanBeCustomizedFieldVisible("1")
//        //        .ValidateSharingFieldVisible("0")
//        //        .ValidateAuditFieldVisible("1")
//        //        .ValidateViewAuditFieldVisible("1")
//        //        .ValidateValidForUpdateFieldVisible("1")
//        //        .ValidateRecentRecordsFieldVisible("0")
//        //        .ValidateEnablePinToSelfFieldVisible("0")
//        //        .ValidateEnablePinToAnotherFieldVisible("0")
//        //        .ValidateSupportsMultipleDataFormsFieldVisible("0")
//        //        .ValidateAvailableInAdvancedSearchFieldVisible("0")
//        //        //.ValidateAvailableInGlobalSearchFieldVisible("0")
//        //        .ValidateDataRestrictionEnabledFieldVisible("1")
//        //        .ValidateRestrictforLegitimateRelationshipsFieldVisible("1")
//        //        .ValidateIsMergeEnabledFieldVisible("0")
//        //        .ValidateAvailableforFieldLevelSecurityVisible("0")
//        //        .ValidateAvailableinMobileFieldVisible("1")
//        //        .ValidateEncryptMobileOfflineDataFieldVisible("0")
//        //        .ValidateEnableMailMergeFieldVisible("0")
//        //        .ValidateExcludefromDataDictionaryFieldVisible("0")
//        //        .ValidateExcludefromDataMapsFieldVisible("0")
//        //        .ValidateExcludefromWorkflowFieldVisible("0")
//        //        .ValidateExcludeFromSARFieldVisible("0")
//        //        .ValidateTrackChangesFieldVisible("0")
//        //        .ValidateEnableErrorManagementFieldVisible("0")
//        //        .ValidateValidForExportFieldVisible("0")
//        //        .ValidateDisplayOnTimelineFieldVisible("1")
//        //        .ValidateIsBulkEditEnabledFieldVisible("0");

//        //    businessObjectRecordPage
//        //        .NavigateToBusinessObjectFieldsSubArea();

//        //    foreach (KeyValuePair<string, List<string>> item in Map)
//        //    {                

//        //        businessObjectFieldsSubPage
//        //            .WaitForBusinessObjectFieldsSubPageToLoad()
//        //            .SearchAndOpenRecord(item.Key);

//        //        businessObjectFieldRecordPage
//        //            .WaitForBusinessObjectFieldRecordPageToLoad()
//        //            .ValidateLabelNameMandatoryFieldSignVisible(true)
//        //            .ValidateTypeMandatoryFieldSignVisible(true)                    
//        //            .ValidateLabelNameFieldValue(item.Value[0])                    
//        //            .ValidateToolTipFieldValue(item.Value[1])
//        //            .ValidateRequiredFieldValue(item.Value[2])
//        //            .ValidateFieldTypeFieldValue(item.Value[3])                    
//        //            .ValidateIsSearchableAndDefaultValueFields(item.Value[4], item.Value[5])
//        //            .ClickBackButton();
//        //    }

//        //}

//        //[TestProperty("JiraIssueID", "CDV6-16171")]
//        //[Description("Login to CD. Navigate to Settings > Configuration > Customizations > Business Objects > AboutMeSetup Record" +
//        //            "Verify that the fields are set with the values as mentioned in the description." +
//        //            "Navigate to Menu > Related Records > Business Object Fields." +
//        //            "Verify that the Business object fields - Label name, Tooltip, Mandatory, Default value, Field Type, Searchable? are as per the document attached for the AboutMeSetup")]
//        //[TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
//        //public void BusinessObjectDetails_FieldValidation_UITestMethod002()
//        //{

//        //    #region Business Object Fields Record Fields Data for About Me Setup


//        //    //List<string> responsibleUserIdFields = new List<string>() { "Responsible User", "", "0", "Business Object Reference", "yes", "yes" };
//        //    List<string> ownerIdFields = new List<string>() { "Responsible Team", "The Team responsible for this MASH record.", "1", "Record Owner", "yes", "no" };

//        //    List<string> enableMediaContentFields = new List<string>() { "Enable Media Content", "Determines if media content will be available on About Me records", "1", "Boolean", "yes", "yes" };

//        //    List<string> statusFields = new List<string>()
//        //    { "Status", "Determines if this record will be the one used by  the About Me module on person records", "1", "Picklist (Numeric)", "yes", "yes"};

//        //    List<string> hideAboutMeSectionFields = new List<string>()
//        //    { "Hide “About Me” Section", "Controls the display of “About Me” relevant fields", "1", "Boolean", "yes", "yes"};

//        //    List<string> guidelinesToConsiderWhenCapturingAboutFields = new List<string>()
//        //    { "Guidelines To Consider When Capturing “About Me” Information", "Defines the text to be displayed as guidelines for “About Me” section", "1", "Multiline Textbox", "no", "no"};

//        //    List<string> hideWhatIsMostImportantToMeSectionFields = new List<string>()
//        //    { "Hide “What Is Most Important To Me” Section", "Controls the display of “What is most important  to me” relevant fields", "1", "Boolean", "yes", "yes"};


//        //    List<string> guidelinesToConsiderWhenCapturingWhatIsMostImportantToMeInformationFields = new List<string>()
//        //    { "Guidelines To Consider When Capturing “What Is Most Important To Me” Information", "Defines the text to be displayed as guidelines for “What is most important to me” section", "1", "Multiline Textbox", "no", "no" };

//        //    List<string> hidePeopleWhoAreImportantToMeSectFields = new List<string>()
//        //    { "Hide “People Who Are Important To Me” Section", "Controls the display of “People who are  important to me” relevant fields", "1", "Boolean", "yes", "yes"};

//        //    List<string> guidelinesToConsiderWhenCapturingPeopleFields = new List<string>()
//        //    { "Guidelines To Consider When Capturing “People Who Are Important To Me” Information", "Defines the text to be displayed as guidelines for “People who are important to me” section", "1", "Multiline Textbox", "no", "no"};

//        //    List<string> hideHowICommunicateAndHowToFields = new List<string>()
//        //    { "Hide “How I Communicate And How To Communicate With Me” Section", "Controls the display of “How I communicate and how to communicate with me” relevant fields", "1", "Boolean", "yes", "yes"};

//        //    List<string> guidelinesToConsiderWhenCapturingHowFields = new List<string>()
//        //    { "Guidelines To Consider When Capturing “How I Communicate And How To Communicate With Me” Information", "Defines the text to be displayed as guidelines for “How I communicate and how to communicate with me” section", "1", "Multiline Textbox", "no", "no"};

//        //    List<string> hidePleaseDoAndPleaseDoNotSectionFields = new List<string>()
//        //    { "Hide “Please Do And Please Do Not” Section", "Controls the display of “Please Do and Please Do Not” relevant fields", "1", "Boolean", "yes", "yes"};

//        //    List<string> guidelinesToConsiderWhenCapturingPleaseDoAndFields = new List<string>()
//        //    { "Guidelines To Consider When Capturing “Please Do And Please Do Not” Information", "Defines the text to be displayed as guidelines for “Please Do and Please Do Not” section", "1", "Multiline Textbox", "no", "no"};

//        //    List<string> hideMyWellnessSectionFields = new List<string>()
//        //    { "Hide “My Wellness” Section", "Controls the display of “My Wellness” relevant fields", "1", "Boolean", "yes", "yes"};

//        //    List<string> guidelinesToConsiderWhenCapturingMyWellnessFields = new List<string>()
//        //    { "Guidelines To Consider When Capturing “My Wellness” Information", "Defines the text to be displayed as guidelines for “My Wellness” section", "1", "Multiline Textbox", "no", "no"};

//        //    List<string> hideHowAndWhenToSupportMeSectionFields = new List<string>()
//        //    { "Hide “How And When To Support Me” Section", "Controls the display of “How and when to support me” relevant fields", "1", "Boolean", "yes", "yes"};

//        //    List<string> guidelinesToConsiderWhenCapturingHowAndWhenToFields = new List<string>()
//        //    { "Guidelines To Consider When Capturing “How And When To Support Me” Information", "Defines the text to be displayed as guidelines for “How and when to support me” section", "1", "Multiline Textbox", "no", "no"};

//        //    List<string> hideAlsoWorthKnowingAboutMeSectionFields = new List<string>()
//        //    { "Hide “Also Worth Knowing About Me” Section", "Controls the display of “Also worth knowing about me” relevant fields", "1", "Boolean", "yes", "yes"};

//        //    List<string> guidelinesToConsiderWhenCapturingAlsoWorthFields = new List<string>()
//        //    { "Guidelines To Consider When Capturing “Also Worth Knowing About Me” Information", "Defines the text to be displayed as guidelines for “Also worth knowing about me” section", "1", "Multiline Textbox", "no", "no"};

//        //    List<string> hidePhysicalCharacteristicsSectionFields = new List<string>()
//        //    { "Hide “Physical Characteristics” Section", "Controls the display of “Physical Characteristics” relevant fields", "1", "Boolean", "yes", "yes"};

//        //    //List<string> guidelinesToConsiderWhenCapturingPhysicalCharacteristicsInformation = new List<string>()
//        //    //{ "Guidelines To Consider When Capturing “Also Worth Knowing About Me” Information", "Defines the text to be displayed as guidelines for “Also worth knowing about me” section", "1", "Multiline Textbox", "no", "no"};


//        //    Dictionary<string, List<string>> Map = new Dictionary<string, List<string>>();

//        //    Map.Add("OwnerId", ownerIdFields);
//        //    Map.Add("EnableMediaContent", enableMediaContentFields);
//        //    Map.Add("StatusId", statusFields);
//        //    Map.Add("HideAboutMeSection", hideAboutMeSectionFields);
//        //    Map.Add("GuidelinesToConsiderWhenCapturingAbout", guidelinesToConsiderWhenCapturingAboutFields);
//        //    Map.Add("HideWhatIsMostImportantToMeSection", hideWhatIsMostImportantToMeSectionFields);
//        //    Map.Add("GuidelinesToConsiderWhenCapturingWhat", guidelinesToConsiderWhenCapturingWhatIsMostImportantToMeInformationFields);
//        //    Map.Add("HidePeopleWhoAreImportantToMeSect", hidePeopleWhoAreImportantToMeSectFields);
//        //    Map.Add("GuidelinesToConsiderWhenCapturingPeople", guidelinesToConsiderWhenCapturingPeopleFields);
//        //    Map.Add("HideHowICommunicateAndHowToCom", hideHowICommunicateAndHowToFields);
//        //    Map.Add("GuidelinesToConsiderWhenCapturingHow", guidelinesToConsiderWhenCapturingHowFields);
//        //    Map.Add("HidePleaseDoAndPleaseDoNotSection", hidePleaseDoAndPleaseDoNotSectionFields);
//        //    Map.Add("guidelinestoconsiderwhencapturingpleasedoand", guidelinesToConsiderWhenCapturingPleaseDoAndFields);
//        //    Map.Add("HideMyWellnessSection", hideMyWellnessSectionFields);
//        //    Map.Add("GuidelinesToConsiderWhenCapturingMyWellness", guidelinesToConsiderWhenCapturingMyWellnessFields);
//        //    Map.Add("HideHowAndWhenToSupportMeSection", hideHowAndWhenToSupportMeSectionFields);
//        //    Map.Add("GuidelinesToConsiderWhenCapturingHowAndWhenTo", guidelinesToConsiderWhenCapturingHowAndWhenToFields);
//        //    Map.Add("HideAlsoWorthKnowingAboutMeSection", hideAlsoWorthKnowingAboutMeSectionFields);
//        //    Map.Add("GuidelinesToConsiderWhenCapturingAlsoWorthKno", guidelinesToConsiderWhenCapturingAlsoWorthFields);
//        //    Map.Add("HidePhysicalCharacteristicsSection", hidePhysicalCharacteristicsSectionFields);
//        //    //Map.Add("HowAndWhenToSupportMe", howAndWhenToSupportMeFields);


//        //    #endregion

//        //    var aboutMeSetupBusinessObjectID = dbHelper.businessObject.GetBusinessObjectByName("AboutMeSetup")[0];

//        //    loginPage
//        //        .GoToLoginPage()
//        //        .Login(loginUsername, "Passw0rd_!", EnvironmentName)
//        //        .WaitForCareProvidermHomePageToLoad();

//        //    mainMenu
//        //        .WaitForMainMenuToLoad()
//        //        .NavigateToCustomizationsSection();

//        //    customizationsPage
//        //        .WaitForCustomizationsPageToLoad()
//        //        .ClickBusinessObjectsButton();

//        //    businessObjectsPage
//        //        .WaitForBusinessObjectsPageToLoad()
//        //        .InsertQuickSearchText("AboutMeSetup")
//        //        .ClickQuickSearchButton()
//        //        .OpenRecord(aboutMeSetupBusinessObjectID.ToString());

//        //    businessObjectRecordPage
//        //        .WaitForBusinessObjectRecordPageToLoad()
//        //        .ValidateTitleFieldRequiredFieldVisible("1")
//        //        .ValidateOwnershipFieldVisible("Team Based")
//        //        .ValidateEnableResponsibleUserFieldVisible("0")
//        //        .ValidateCanBeCustomizedFieldVisible("0")
//        //        //.ValidateSharingFieldVisible("0")
//        //        .ValidateAuditFieldVisible("1")
//        //        .ValidateValidForUpdateFieldVisible("1")
//        //        .ValidateAvailableInAdvancedSearchFieldVisible("0")
//        //        .ValidateIsMergeEnabledFieldVisible("0")
//        //        .ValidateAvailableforFieldLevelSecurityVisible("0")
//        //        .ValidateExcludefromDataDictionaryFieldVisible("0")
//        //        .ValidateExcludefromDataMapsFieldVisible("1")
//        //        .ValidateExcludefromWorkflowFieldVisible("1")
//        //        .ValidateExcludeFromSARFieldVisible("0")
//        //        .ValidateTrackChangesFieldVisible("0")
//        //        .ValidateValidForExportFieldVisible("0")
//        //        .ValidateIsBulkEditEnabledFieldVisible("0");

//        //    businessObjectRecordPage
//        //        .NavigateToBusinessObjectFieldsSubArea();

//        //    foreach (KeyValuePair<string, List<string>> item in Map)
//        //    {

//        //        businessObjectFieldsSubPage
//        //            .WaitForBusinessObjectFieldsSubPageToLoad()
//        //            .SearchAndOpenRecord(item.Key);

//        //        businessObjectFieldRecordPage
//        //            .WaitForBusinessObjectFieldRecordPageToLoad()
//        //            .ValidateLabelNameMandatoryFieldSignVisible(true)
//        //            .ValidateTypeMandatoryFieldSignVisible(true)
//        //            .ValidateLabelNameFieldValue(item.Value[0])
//        //            .ValidateToolTipFieldValue(item.Value[1])
//        //            .ValidateRequiredFieldValue(item.Value[2])
//        //            .ValidateFieldTypeFieldValue(item.Value[3])
//        //            .ValidateIsSearchableAndDefaultValueFields(item.Value[4], item.Value[5])
//        //            .ClickBackButton();


//        //    }

//        //}

//        #endregion
//    }
//}
