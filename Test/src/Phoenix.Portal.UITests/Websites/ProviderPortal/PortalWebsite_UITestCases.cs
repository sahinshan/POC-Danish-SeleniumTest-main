using System;
using System.Linq;
using System.Text;
using NUnit.Framework;


namespace Phoenix.Portal.UITests.Websites.ProviderPortal
{

    public class PortalWebsite_UITestCases : FunctionalTest
    {

        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private Guid _ethnicityId;
        private Guid _personID;
        private int _personNumber;
        private string _personFullName;
        private Guid _systemUserId;
        private Guid _languageId;
        private Guid _serviceElement1Id;
        private Guid _serviceElement2Id;
        private Guid _serviceElement3Id;
        private string _serviceElement1IdName;
        private string _serviceElement2IdName;
        private string _serviceElement3IdName;
        private Guid _serviceMappingId;
        private Guid _clientCategory1Id;
        private string _clientCategory1IdName;
        private Guid _clientCategory2Id;
        private string _clientCategory2IdName;
        private Guid _providerId;
        private Guid _staffmembershipid;
        private int _providerNumber;
        private string _systemUsername;
        private Guid serviceProvidedId;
        private Guid rateunitid;
        private Guid serviceProvisionID;
        private Guid serviceprovisionstatusid;
        private Guid serviceprovisionstartreasonid;
        private Guid serviceprovisionendreasonid;
        private Guid placementRoomTypeId;
        private string _provider1Name = "CDV6-9020_Provider1" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss FFFFF");
        private string _provider2Name = "CDV6-9020_Provider2" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss FFFFF");

        //[SetUp()]
        private void TestsSetupMethod()
        {
            try
            {
                #region Business Unit

                var businessUnitExists = dbHelper.businessUnit.GetByName("CareDirector QA").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareDirector QA");
                _careDirectorQA_BusinessUnitId = dbHelper.businessUnit.GetByName("CareDirector QA")[0];

                #endregion

                #region Authentication Provider

                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareDirector QA").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");
                _careDirectorQA_TeamId = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];

                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Create Default System User 

                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_1").Any();
                if (!adminUserExists)
                {
                    _systemUserId = dbHelper.systemUser.CreateSystemUser("CW_Forms_Test_User_1", "CW", "Forms Test User 1", "CW Forms Test User 1", "Passw0rd_!", "CW_Forms_Test_User_1@somemail.com", "CW_Forms_Test_User_1@othermail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId);

                    //var systemAdministratorSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("System Administrator").First();
                    //var systemUserSecureFieldsSecurityProfileId = dbHelper.securityProfile.GetSecurityProfileByName("CW System User - Secure Fields (Edit)").First();

                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, systemAdministratorSecurityProfileId);
                    //dbHelper.userSecurityProfile.CreateUserSecurityProfile(_systemUserId, systemUserSecureFieldsSecurityProfileId);
                }

                if (Guid.Empty == _systemUserId)
                    _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Forms_Test_User_1").FirstOrDefault();

                dbHelper.systemUser.UpdateLastPasswordChangedDate(_systemUserId, DateTime.Now.Date);
                _systemUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(_systemUserId, "username")["username"];

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("CDV6-17924_Ethnicity").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "CDV6-17924_Ethnicity", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("CDV6-17924_Ethnicity")[0];

                #endregion

                #region  ServiceElement1

                var serviceElement1Exist = dbHelper.serviceElement1.GetByName("01Jun21-SE1").Any();
                if (!serviceElement1Exist)
                    dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, "01Jun21-SE1", new DateTime(2020, 1, 1), 17924, 1, 1);
                _serviceElement1Id = dbHelper.serviceElement1.GetByName("01Jun21-SE1")[0];
                _serviceElement1IdName = (string)dbHelper.serviceElement1.GetByID(_serviceElement1Id, "name")["name"];


                #endregion

                #region  ServiceElement2

                var serviceElement2Exist = dbHelper.serviceElement2.GetByName("01Jun21-SE2").Any();
                if (!serviceElement2Exist)
                    dbHelper.serviceElement2.CreateServiceElement2(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "01Jun21-SE2", new DateTime(2020, 1, 1), 17925);
                _serviceElement2Id = dbHelper.serviceElement2.GetByName("01Jun21-SE2")[0];
                _serviceElement2IdName = (string)dbHelper.serviceElement2.GetByID(_serviceElement2Id, "name")["name"];

                #endregion

                #region Service Mapping 

                var serviceMappingExist = dbHelper.serviceMapping.GetByName("01Jun21-SE1 \\ 01Jun21-SE2").Any();
                if (!serviceMappingExist)
                    dbHelper.serviceMapping.CreateServiceMapping(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, _serviceElement1Id, _serviceElement2Id);
                _serviceMappingId = dbHelper.serviceMapping.GetByName("01Jun21-SE1 \\ 01Jun21-SE2").FirstOrDefault();

                #endregion

                #region Rate Units

                rateunitid = dbHelper.rateUnit.GetByName("Per Day \\ Days")[0];

                #endregion

                #region Provider


                if (!dbHelper.provider.GetProviderByName("9020_Provider").Any())
                {
                    _providerId = dbHelper.provider.CreateProvider("9020_Provider", _careDirectorQA_TeamId, 2); //creating a "Supplier" provider
                    serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);
                    var serviceProvidedRatePeriodId = dbHelper.serviceProvidedRatePeriod.CreateServiceProvidedRatePeriod(_careDirectorQA_TeamId, serviceProvidedId, rateunitid, new DateTime(2022, 1, 1), 2);
                    var serviceProvidedRateScheduleId = dbHelper.serviceProvidedRateSchedule.CreateServiceProvidedRateSchedule(_careDirectorQA_TeamId, serviceProvidedRatePeriodId, serviceProvidedId, 10m, 15m);

                    var paymentTypeCodeId = dbHelper.paymentTypeCode.GetByName("Invoice")[0];
                    var vatCodeId = dbHelper.vatCode.GetByName("Standard Rated")[0];
                    var providerBatchGroupingId = dbHelper.providerBatchGrouping.GetByName("Batching Not Applicable")[0];
                    dbHelper.serviceFinanceSettings.CreateServiceFinanceSettings(_careDirectorQA_TeamId, serviceProvidedId, paymentTypeCodeId, vatCodeId, providerBatchGroupingId, new DateTime(2022, 1, 1), null, 0);
                }
                _providerId = dbHelper.provider.GetProviderByName("9020_Provider")[0];
                serviceProvidedId = dbHelper.serviceProvided.GetByProviderId(_providerId)[0];
                dbHelper.provider.UpdateavailableOnProviderPortal(_providerId);


                #endregion

                #region Staff membership

                Guid professiontypeid = new Guid("961F03E7-6F3A-E911-A2C5-005056926FE4"); //doctor

                //Create CreateProfessional
                Guid _professionalid = dbHelper.professional.CreateProfessional(_careDirectorQA_TeamId, professiontypeid, "Mr", "FirstName", "ToDelete");
                _staffmembershipid = dbHelper.staffMembership.CreateStaffMembership(_careDirectorQA_TeamId, "Test", _providerId, _professionalid, professiontypeid, DateTime.Today);
                _staffmembershipid = dbHelper.staffMembership.GetByproviderid(_providerId).FirstOrDefault();

                #endregion

                #region websiteuser

                var websiteID = dbHelper.website.GetWebSiteByName("Provider Portal")[0]; //Provider Portal
                var personid = new Guid("9fff48cd-3772-4a95-989d-b7e6ecf583e3"); //Rosalyn Petersen
                var websiteSecurityProfile = dbHelper.websiteSecurityProfile.GetByWebSiteIdAndName(websiteID, "CW Full Access")[0];
                foreach (var websiteUserID in dbHelper.websiteUser.GetByWebSiteIDAndUserName(websiteID, "WebSiteAutomationUser10@mail.com"))
                {
                    foreach (var passwordReset in dbHelper.websiteUserPasswordReset.GetByWebSiteUserID(websiteUserID))
                        dbHelper.websiteUserPasswordReset.DeleteWebsiteUserPasswordReset(passwordReset);

                    dbHelper.websiteUser.DeleteWebsiteUser(websiteUserID);
                }

                var userID = dbHelper.websiteUser.CreateWebsiteUser(websiteID, "Rosalyn Petersen", "WebSiteAutomationUser10@mail.com", "Passw0rd_!", true, 2, _staffmembershipid, "staffmembership", "bala Manoj Staff Member of ProviderPortal01", websiteSecurityProfile);

               // serviceProvidedId = dbHelper.serviceProvided.CreateServiceProvided(_careDirectorQA_TeamId, _systemUserId, _providerId, _serviceElement1Id, _serviceElement2Id, null, null, null, 2);
                #endregion

                             
                #region Service Provision Status

                serviceprovisionstatusid = dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Draft")[0];

                #endregion
              
                #region Service Provision Start Reason

                if (!dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_17923").Any())
                    dbHelper.serviceProvisionStartReason.CreateServiceProvisionStartReason(_careDirectorQA_TeamId, _careDirectorQA_BusinessUnitId, "StartReason_CDV6_17923", new DateTime(2022, 1, 1));
                serviceprovisionstartreasonid = dbHelper.serviceProvisionStartReason.GetByName("StartReason_CDV6_17923")[0];

                #endregion



                #region Service Provision End Reason

                if (!dbHelper.serviceProvisionEndReason.GetByName("EndReason_CDV6_17923").Any())
                    dbHelper.serviceProvisionEndReason.CreateServiceProvisionEndReason(_careDirectorQA_TeamId, "EndReason_CDV6_17923", new DateTime(2022, 1, 1));
                serviceprovisionendreasonid = dbHelper.serviceProvisionEndReason.GetByName("EndReason_CDV6_17923")[0];

                #endregion

                #region Placement Room Type

                placementRoomTypeId = dbHelper.placementRoomType.GetPlacementRoomTypeByName("Not Applicable")[0];

                #endregion

                #region Person

                var personRecordExists = dbHelper.person.GetByFirstName("CDV6-17924_TestUser").Any();
                if (!personRecordExists)
                {
                    _personID = dbHelper.person.CreatePersonRecord("", "CDV6-17924_TestUser", "", "1", "", new DateTime(2000, 1, 2), _ethnicityId, _careDirectorQA_TeamId, 7, 2);
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                if (_personID == Guid.Empty)
                {
                    _personID = dbHelper.person.GetByFirstName("CDV6-17924_TestUser").FirstOrDefault();
                    _personNumber = (int)dbHelper.person.GetPersonById(_personID, "personnumber")["personnumber"];
                }
                _personFullName = "CDV6-17924_TestUser 1";

                //remove any matching case form
                foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(_personID))
                    dbHelper.personForm.DeletePersonForm(personformid);


                var documentid = new Guid("e8c08c9e-c2c2-eb11-a323-005056926fe4"); //Automation - Portal - Person Form 1
                var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
                 var startDate = new DateTime(2021, 3, 29);
                //create a new case form record
                var assessmentid = dbHelper.personForm.CreatePersonForm(OwnerID, _personID, documentid, startDate);

                #endregion


                #region Service Provision


                serviceProvisionID = dbHelper.serviceProvision.CreateServiceProvision(_careDirectorQA_TeamId, _systemUserId, _personID,
                    serviceprovisionstatusid, _serviceElement1Id, _serviceElement2Id,
                    rateunitid,
                    serviceprovisionstartreasonid, serviceprovisionendreasonid,
                    _careDirectorQA_TeamId, serviceProvidedId,
                    _providerId, _systemUserId, placementRoomTypeId, new DateTime(2022, 6, 20), new DateTime(2022, 6, 26), null);

                #endregion

                //reset the status of the service provision to Authorised
                Guid AuthoriseStatus = this.dbHelper.serviceProvisionStatus.GetServiceProvisionStatusByName("Authorised")[0];
                this.dbHelper.serviceProvision.UpdateServiceProvisionStatus(serviceProvisionID, AuthoriseStatus);
            }
            catch
            {
                if (driver != null)
                    driver.Close();

                throw;
            }
        }


        #region https://advancedcsg.atlassian.net/browse/CDV6-9267


        [Test]
        [Property("JiraIssueID", "CDV6-11390")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions (user has no messages linked to him) - Validate that the member home page is displayed - " +
            "Validate that the messages area displays no messages")]
        public void MessageFeatureForProviderUsers_TestMethod01()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .ValidateMessageVisibility(1, false);
            
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11391")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions (user has no messages linked to him) - Insert a text in the message textbox and click on the submit button - " +
            "Validate that a new website message record is created")]
        public void MessageFeatureForProviderUsers_TestMethod02()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .InsertMessageText("New message sent from provider portal 01 ...")
                .ClickSubmitButton()

                .WaitForMemberHomePageToLoad();

            System.Threading.Thread.Sleep(1000);

            var websitemessages = dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID);
            Assert.AreEqual(1, websitemessages.Count);

            var fields = dbHelper.websiteMessage.GetByID(websitemessages[0], "ownerid", "fromid", "fromidtablename", "fromidname", 
                "toid", "toidtablename", "toidname", 
                "regardingid", "regardingidtablename", "regardingidname", 
                "message", "read");

            Assert.AreEqual(ownerid, fields["ownerid"]);

            Assert.AreEqual(websiteUserId, fields["fromid"]);
            Assert.AreEqual("websiteuser", fields["fromidtablename"]);
            Assert.AreEqual("Mr Joana MCMinnion Staff Member of ProviderPortal01", fields["fromidname"]);

            Assert.AreEqual(ownerid, fields["toid"]);
            Assert.AreEqual("team", fields["toidtablename"]);
            Assert.AreEqual("CareDirector QA", fields["toidname"]);

            Assert.AreEqual(providerID, fields["regardingid"]);
            Assert.AreEqual("provider", fields["regardingidtablename"]);
            Assert.AreEqual("ProviderPortal01", fields["regardingidname"]);

            Assert.AreEqual("New message sent from provider portal 01 ...", fields["message"]);
            Assert.AreEqual(false, fields["read"]); 
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11392")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions (user has no messages linked to him) - Insert a text in the message textbox and click on the submit button - " +
            "Validate that the new message is displayed in the messages area as a Sent message - " +
            "Validate that the message is displayed with text in bold")]
        public void MessageFeatureForProviderUsers_TestMethod03()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .InsertMessageText("New message sent from provider portal 01 ...")
                .ClickSubmitButton()

                .WaitForMemberHomePageToLoad();

            System.Threading.Thread.Sleep(1000);

            var websitemessages = dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID);
            Assert.AreEqual(1, websitemessages.Count);

            var fields = dbHelper.websiteMessage.GetByID(websitemessages[0], "createdon");

            var createdOn = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");

            memberHomePage
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "light")
                .ValidateMessageReadFlag(1, false)
                .ValidateMessageFromText(1, "Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateMessageSentDate(1, createdOn)
                .ValidateMessageText(1, "New message sent from provider portal 01 ...")
                ;
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11393")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions (user has no messages linked to him) - Insert a text in the message textbox and click on the submit button - " +
            "Validate that the new message is displayed in the messages area as a Sent message - " +
            "Reload the member home page - Validate that the message is displayed with text in bold")]
        public void MessageFeatureForProviderUsers_TestMethod04()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .InsertMessageText("New message sent from provider portal 01 ...")
                .ClickSubmitButton()

                .WaitForMemberHomePageToLoad();

            System.Threading.Thread.Sleep(1000);

            var websitemessages = dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID);
            Assert.AreEqual(1, websitemessages.Count);

            memberHomePage
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "light")
                .ValidateMessageReadFlag(1, false)
                .ValidateMessageFromText(1, "Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateMessageText(1, "New message sent from provider portal 01 ...");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickMemberHomeLink();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "light")
                .ValidateMessageReadFlag(1, false)
                .ValidateMessageFromText(1, "Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateMessageText(1, "New message sent from provider portal 01 ...");
        }


        [Test]
        [Property("JiraIssueID", "CDV6-11394")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions - User has 1 sent message linked to him - Message is not marked as read - " +
            "Validate that the message is displayed with bold text")]
        public void MessageFeatureForProviderUsers_TestMethod05()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                ownerid, "team", "CareDirector QA",
                providerID, "provider", "ProviderPortal01", "Message Text ...", false);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "light")
                .ValidateMessageReadFlag(1, false)
                .ValidateMessageFromText(1, "Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateMessageText(1, "Message Text ...");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11395")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions - User has 1 sent message linked to him - Message is marked as read - " +
            "Validate that the message is NOT displayed with bold text")]
        public void MessageFeatureForProviderUsers_TestMethod06()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                ownerid, "team", "CareDirector QA",
                providerID, "provider", "ProviderPortal01", "Message Text ...", true);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "light")
                .ValidateMessageReadFlag(1, true)
                .ValidateMessageFromText(1, "Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateMessageText(1, "Message Text ...");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11396")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions - User has 2 sent messages linked to him - " +
            "Validate that both messages are displayed")]
        public void MessageFeatureForProviderUsers_TestMethod07()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId1 = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                ownerid, "team", "CareDirector QA",
                providerID, "provider", "ProviderPortal01", "Message 1 Text ...", true);

            System.Threading.Thread.Sleep(300);

            var websiteMessageId2 = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                ownerid, "team", "CareDirector QA",
                providerID, "provider", "ProviderPortal01", "Message 2 Text ...", true);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()

                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "light")
                .ValidateMessageReadFlag(1, true)
                .ValidateMessageFromText(1, "Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateMessageText(1, "Message 2 Text ...")

                .ValidateMessageVisibility(2, true)
                .ValidateMessageBackgroundColor(2, "light")
                .ValidateMessageReadFlag(2, true)
                .ValidateMessageFromText(2, "Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateMessageText(2, "Message 1 Text ...")
                ;
        }




        [Test]
        [Property("JiraIssueID", "CDV6-11397")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions - User has 1 received message linked to him - Message is not marked as read - " +
            "Validate that the message is displayed with bold text")]
        public void MessageFeatureForProviderUsers_TestMethod08()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                systemUserID, "systemuser", "Security Test User Admin",
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                providerID, "provider", "ProviderPortal01", "Message Text ...", false);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "info")
                .ValidateMessageReadFlag(1, false)
                .ValidateMessageFromText(1, "Security Test User Admin")
                .ValidateMessageText(1, "Message Text ...");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11398")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions - User has 1 received message linked to him - Message is marked as read - " +
            "Validate that the message is NOT displayed with bold text")]
        public void MessageFeatureForProviderUsers_TestMethod09()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                systemUserID, "systemuser", "Security Test User Admin",
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                providerID, "provider", "ProviderPortal01", "Message Text ...", true);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "info")
                .ValidateMessageReadFlag(1, true)
                .ValidateMessageFromText(1, "Security Test User Admin")
                .ValidateMessageText(1, "Message Text ...");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11399")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions - User has 2 received messages linked to him - " +
            "Validate that both messages are displayed")]
        public void MessageFeatureForProviderUsers_TestMethod10()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId1 = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                systemUserID, "systemuser", "Security Test User Admin",
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                providerID, "provider", "ProviderPortal01", "Message 1 Text ...", true);

            System.Threading.Thread.Sleep(300);

            var websiteMessageId2 = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                systemUserID, "systemuser", "Security Test User Admin",
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                providerID, "provider", "ProviderPortal01", "Message 2 Text ...", true);


            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()

                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "info")
                .ValidateMessageReadFlag(1, true)
                .ValidateMessageFromText(1, "Security Test User Admin")
                .ValidateMessageText(1, "Message 2 Text ...")

                .ValidateMessageVisibility(2, true)
                .ValidateMessageBackgroundColor(2, "info")
                .ValidateMessageReadFlag(2, true)
                .ValidateMessageFromText(2, "Security Test User Admin")
                .ValidateMessageText(2, "Message 1 Text ...");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11400")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
            "Login with a user with full permissions - User has 2 received messages linked to him - Insert a text in the message text box - click on the submit button - " +
            "Validate that the new message is displayed at the top of the messages area")]
        public void MessageFeatureForProviderUsers_TestMethod11()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId1 = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                systemUserID, "systemuser", "Security Test User Admin",
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                providerID, "provider", "ProviderPortal01", "Message 1 Text ...", true);

            System.Threading.Thread.Sleep(300);

            var websiteMessageId2 = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
                systemUserID, "systemuser", "Security Test User Admin",
                websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
                providerID, "provider", "ProviderPortal01", "Message 2 Text ...", true);


            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .InsertMessageText("New message sent from provider portal 01 ...")
                .ClickSubmitButton()

                .WaitForMemberHomePageToLoad();

            System.Threading.Thread.Sleep(1000);

            var websitemessages = dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID);
            Assert.AreEqual(1, websitemessages.Count);

            var fields = dbHelper.websiteMessage.GetByID(websitemessages[0], "createdon");

            var createdOn = ((DateTime)fields["createdon"]).ToLocalTime().ToString("dd/MM/yyyy HH:mm:ss");

            memberHomePage
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "light")
                .ValidateMessageReadFlag(1, false)
                .ValidateMessageFromText(1, "Mr Joana MCMinnion Staff Member of ProviderPortal01")
                .ValidateMessageSentDate(1, createdOn)
                .ValidateMessageText(1, "New message sent from provider portal 01 ...")


                .ValidateMessageVisibility(2, true)
                .ValidateMessageBackgroundColor(2, "info")
                .ValidateMessageReadFlag(2, true)
                .ValidateMessageFromText(2, "Security Test User Admin")
                .ValidateMessageText(2, "Message 2 Text ...")

                .ValidateMessageVisibility(3, true)
                .ValidateMessageBackgroundColor(3, "info")
                .ValidateMessageReadFlag(3, true)
                .ValidateMessageFromText(3, "Security Test User Admin")
                .ValidateMessageText(3, "Message 1 Text ...");
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11401")]
        [Description("Navigate to the portal URL - Wait for the Home Page to load - " +
           "Login with a user with full permissions - User has 1 received message linked to him - Message is not marked as read - " +
           "Validate that the message is displayed with bold text - Reload the member home page - " +
            "Validate that the message is NOT displayed with bold text")]
        public void MessageFeatureForProviderUsers_TestMethod12()
        {
            var providerID = new Guid("3add09af-6fa6-eb11-a323-005056926fe4"); //ProviderPortal01
            var websiteUserId = new Guid("dced1c54-95a6-eb11-a323-005056926fe4"); //ProviderPortalUser3@somemail.com
            var systemUserID = new Guid("fdeaba2c-e8a6-e911-a2c6-005056926fe4"); //Security Test User Admin
            var ownerid = new Guid("b6060dfa-7333-43b2-a662-3d9cadab12e5"); //CareDirector QA


            foreach (Guid recordid in dbHelper.websiteMessage.GetByToIdAndRegardingId(websiteUserId, providerID)) //delete all messages sent to the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            foreach (Guid recordid in dbHelper.websiteMessage.GetByFromIdAndRegardingId(websiteUserId, providerID))//delete all messages sent by the website user
                dbHelper.websiteMessage.DeleteWebsiteMessage(recordid);

            var websiteMessageId = dbHelper.websiteMessage.CreateWebsiteMessage(ownerid,
               systemUserID, "systemuser", "Security Test User Admin",
               websiteUserId, "websiteuser", "Mr Joana MCMinnion Staff Member of ProviderPortal01",
               providerID, "provider", "ProviderPortal01", "Message Text ...", false);

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("ProviderPortalUser3@somemail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "info")
                .ValidateMessageReadFlag(1, false)
                .ValidateMessageFromText(1, "Security Test User Admin")
                .ValidateMessageText(1, "Message Text ...");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickMemberHomeLink();

            memberHomePage
                .WaitForMemberHomePageToLoad()
                .ValidateMessageVisibility(1, true)
                .ValidateMessageBackgroundColor(1, "info")
                .ValidateMessageReadFlag(1, true)
                .ValidateMessageFromText(1, "Security Test User Admin")
                .ValidateMessageText(1, "Message Text ...");
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-9020

        [Test]
        [Property("JiraIssueID", "CDV6-11787")]
        [Description("Verify the new grid on Provider Portal for Person Forms with below fields. Option for different views, Search box, Refresh button, Export to excel option")]
        public void VerifyNewGridonPortalPersonForm_TestMethod01()
        {
            var websiteid = new Guid("61adff97-0315-eb11-a2ce-0050569231cf"); //StaffordshireCitizenPortalUser1@mail.com
            var documentid = new Guid("e8c08c9e-c2c2-eb11-a323-005056926fe4"); //Automation - Portal - Person Form 1
            var OwnerID = dbHelper.team.GetTeamIdByName("CareDirector QA")[0];
            var personID = new Guid("70d5ed5f-4126-4e49-920d-e5ba5ef99377"); //Spencer Bishop (90451)
            var startDate = new DateTime(2021, 3, 29);

            //Set website Email Verification Required to No
            dbHelper.website.UpdateAdministrationInformation(websiteid, false, false);

            //Reset two factor authentication for the website
            dbHelper.website.UpdateTwoFactorAuthenticationInfo(websiteid, false, null, null, null, null);

            //remove any matching case form
            foreach (var personformid in dbHelper.personForm.GetPersonFormByPersonID(personID))
                dbHelper.personForm.DeletePersonForm(personformid);

            //create a new case form record
            var assessmentid = dbHelper.personForm.CreatePersonForm(OwnerID, personID, documentid, startDate);




            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("WebSiteAutomationUser10@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickPersonFormsViewButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .validateViewDropDown("All Forms")
                .validateSearchText()
                .ValidateRefreshButtonVisible(true)
                .ValidateExportToExcelButtonVisible(true);

           
        }

        [Test]
        [Property("JiraIssueID", "CDV6-11789")]
        [Description("Verify the 7 different views of Person Forms.PreRequisites:Login credentials for Provider Portal.There should be at least one Person Form created in Provider Portal “Yes“ and its Person Form Involvement tagged to the Provider Portal which satisficing the condition for each view")]
        public void VerifyPortalPersonForm_TestMethod02()
        {
            TestsSetupMethod();

            homePage
                .GoToHomePage()
                .WaitForHomePageToLoad()
                .InsertUserName("WebSiteAutomationUser10@mail.com")
                .InsertPassword("Passw0rd_!")
                .ClickLoginButton();

            mainMenu
               .WaitForMainMenuToLoad()
               .ClickPersonFormsViewButton();

            personFormsPage
                .WaitForPersonFormsPageToLoad()
                .validateViewDropDown("All Forms")
                .validateSearchText();
              //  .ValidateRefreshButtonVisible(true)
            //    .ValidateExportToExcelButtonVisible(true);


        }


        #endregion


        [Description("Method will return the name of all tests and the Description of each one")]
        [Test]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }

    }
}
