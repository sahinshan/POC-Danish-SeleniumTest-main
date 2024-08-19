using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Phoenix.UITests.QualityAndCompliance
{
    /// <summary>
    ///  This class contains a test method to
    ///  Verify Default Value is blank for Corporate Risk Register Field.   
    ///  Verify the field label titles for: Risk Consequence, Residual Risk Consequence and Action Plan Closure Date.
    ///  Verify Closure Date in Organisational Risk and Action Plans
    /// </summary>
    [TestClass]
    public class OrgRisk_ActionPlans_UITestCase : FunctionalTest
    {

        #region Properties

        private string EnvironmentName;
        private Guid _authenticationproviderid;//Internal
        private Guid _languageId;
        private Guid _careProviders_BusinessUnitId;
        private Guid _careProviders_TeamId;
        private Guid _riskTypeId;

        private string RiskID;
        private string ActionPlanNumber;

        private Guid _systemUserId;
        private Guid _environmentId;
        private Guid adminUserId;
        private string _loginUsername;

        #endregion

        [TestInitialize()]
        public void TestSetup()
        {
            try
            {
                #region Environment Name

                EnvironmentName = ConfigurationManager.AppSettings["CareProvidersEnvironmentName"];
                string tenantName = ConfigurationManager.AppSettings["CareProvidersTenantName"];
                dbHelper = new Phoenix.DBHelper.DatabaseHelper("CW_Admin_Test_User_2", "Passw0rd_!", tenantName);

                #endregion

                #region Authentication Provider
                _authenticationproviderid = dbHelper.authenticationProvider.GetAuthenticationProviderIdByName("Internal").FirstOrDefault();
                #endregion

                #region Language

                var language = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)").Any();
                if (!language)
                    dbHelper.productLanguage.CreateProductLanguage("English (UK)", "en-GB", "£", 1033);
                _languageId = dbHelper.productLanguage.GetProductLanguageIdByName("English (UK)")[0];

                #endregion Language

                #region Business Unit
                var businessUnitExists = dbHelper.businessUnit.GetByName("CareProviders").Any();
                if (!businessUnitExists)
                    dbHelper.businessUnit.CreateBusinessUnit("CareProviders");
                _careProviders_BusinessUnitId = dbHelper.businessUnit.GetByName("CareProviders")[0];

                #endregion

                #region Team

                var teamsExist = dbHelper.team.GetTeamIdByName("CareProviders").Any();
                if (!teamsExist)
                    dbHelper.team.CreateTeam("CareProviders", null, _careProviders_BusinessUnitId, "90400", "CareProviders@careworkstempmail.com", "CareProviders", "020 123456");
                _careProviders_TeamId = dbHelper.team.GetTeamIdByName("CareProviders")[0];

                #endregion

                #region Create Admin user

                var adminUserExists = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2").Any();
                if (!adminUserExists)
                    adminUserId = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_2", "CW", "Admin Test User 2", "CW Admin Test User 2", "Passw0rd_!", "CW_Admin_Test_User_2@somemail.com", "CW_Admin_Test_User_2@othermail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);

                adminUserId = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2").FirstOrDefault();
                _loginUsername = (string)dbHelper.systemUser.GetSystemUserBySystemUserID(adminUserId, "username")["username"];
                dbHelper.systemUser.UpdateLastPasswordChangedDate(adminUserId, DateTime.Now.Date);

                #endregion Create Admin User

                #region System User

                var newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_User_03").Any();
                if (!newSystemUser)
                    dbHelper.systemUser.CreateSystemUser("Testing_CDV6_User_03", "Testing", "CDV6_User_03", "Testing CDV6_User_03", "Summer2013@", "abcd@gmail.com", "dcfg@gmail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                _systemUserId = dbHelper.systemUser.GetSystemUserByUserName("Testing_CDV6_User_03")[0];


                newSystemUser = dbHelper.systemUser.GetSystemUserByUserName("CW_Admin_Test_User_2").Any();
                if (!newSystemUser)
                {
                    var adminUser = dbHelper.systemUser.CreateSystemUser("CW_Admin_Test_User_2", "Testing", "Admin_Test_User_1", "Testing Admin_Test_User_1", "Passw0rd_!", "CW_Admin_Test_User_2@somemail.com", "CW_Admin_Test_User_2@othermail.com", "GMT Standard Time", null, null, _languageId, _authenticationproviderid, _careProviders_BusinessUnitId, _careProviders_TeamId);
                }

                #endregion

                #region Risk Type

                var riskTypeExists = dbHelper.organisationalRiskType.GetRiskTypeIdByName("CD Automation Risk").Any();
                if (!riskTypeExists)
                    dbHelper.organisationalRiskType.CreateRiskType(_careProviders_TeamId, "CD Automation risk", DateTime.UtcNow);
                _riskTypeId = dbHelper.organisationalRiskType.GetRiskTypeIdByName("CD Automation risk")[0];



                #endregion Risk Type

                #region Organizational Risk Category

                foreach (var organizationalRiskCategoryId in dbHelper.organisationalRiskCategory.GetByAll())
                {
                    dbHelper.organisationalRiskCategory.Delete(organizationalRiskCategoryId);
                }

                dbHelper.organisationalRiskCategory.Create(_careProviders_TeamId, "Initial update", "", 1, 1);

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }

        }

        #region Corporate Risk Register as null by default + some typos https://advancedcsg.atlassian.net/browse/CDV6-15464


        [TestProperty("JiraIssueID", "ACC-3172")]
        [Description("Test to verify Corporate Risk Register as null by default + some typos" +
                     "Login to CD and access Quality And Compliance > Organisational Risk Management" +
                     "Click to Create New Record" + "Corporate Risk Register field value should display null by default." +
                     "Initial & Residual Risk Score sections: Field title should be displayed as Consequence" +
                     "Under Organisational Risk Record > Action Plans Record page, Field title should be displayed as Action Plan Closure Date")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRisk_ActionPlans_FieldValidation_UITestMethod01()
        {
            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ValidateCorporateRiskRegisterValue("")
                .ValidateConsequenceFieldLabelText()
                .ValidateResidualConsequenceFieldLabelText();

            organisationalRiskManagementRecordPage
                .ClickResponsibleUserLookUpButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_User_03")
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());


            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad();

            organisationalRiskManagementRecordPage
                .ClickRiskTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CD Automation Risk")
                .TapSearchButton()
                .SelectResultElement(_riskTypeId.ToString());

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription("Test Description")
                .SelectRiskStatus("New")
                .SelectCorporateRiskRegisterValue("No")
                .InsertRiskConsequenceValue("1")
                .InsertRiskLikelihoodValue("3")
                .InsertResidualConsequenceValue("2")
                .InsertResidualLikelihoodValue("1")
                .ClickSaveButton();

            organisationalRiskManagementRecordPage
                .NavigateToActionPlansTab();

            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .ClickNewRecordButton();

            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ValidateActionPlanClosureDateFieldLabelText();


        }

        #endregion

        #region Validate Closing date in Risk and Action Plans https://advancedcsg.atlassian.net/browse/CDV6-15462

        //Validate Next Review date, Closure Date in Organisational Risk Management Record and Action Plan Record
        [TestProperty("JiraIssueID", "ACC-3173")]
        [Description(
            "Verify that Validation Message is getting displayed for below:" +
            "Organisational Risk: Next Review Date is less than Risk Identification Date in Organisational Risk record." +
            "Action Plan: when Next Review Date is less than Action Plan record creation Date." +
            "Organisational Risk: Risk Closed Date is less than Risk Identification Date in Organisational Risk record." +
            "Action Plan: when Action plan closure date is less than Action Plan record creation Date." +
            "Verify that Action plan record and Organisational Risk Record is saved for below:" +
            "Organisational Risk: Record is saved when Next Review Date is equal to Risk Identification Date." +
            "Action Plan: Record is saved when Next Review Date is equal to Action Plan record creation Date.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS"), TestCategory("Daily_Runs")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRisk_ActionPlans_UITestMethod02()
        {
            string CurrentDate = commonMethodsHelper.GetCurrentDate();
            string OlderDate = commonMethodsHelper.GetOlderDate();
            string FutureDate = commonMethodsHelper.GetFutureDate();

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ClickRiskTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CD Automation Risk")
                .TapSearchButton()
                .SelectResultElement(_riskTypeId.ToString());

            #region Verify that validation message is displayed when Next Review Date is older than Risk Identification Date
            /*             
             * Verify that Validation Message "The Next Review Date must be later than the Risk Identification Date" should be displayed when 
             * Next Review Date is older than Risk Identification Date in Oraganisational Risk record.             
             */
            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription("Test Description")
                .SelectRiskStatus("New")
                .SelectCorporateRiskRegisterValue("No")
                .InsertRiskConsequenceValue("3")
                .InsertRiskLikelihoodValue("3")
                .InsertResidualConsequenceValue("2")
                .InsertResidualLikelihoodValue("4")
                .InsertNextReviewDate(OlderDate)
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("The Next Review Date must be later than the Risk Identification Date")
                 .TapCloseButton();

            #endregion

            #region Verify that record is saved when Next Review Date is equal to Risk Identification Date

            /*             
             * Verify that user successfully saved the record when
             * Next Review Date equal to Risk Identification Date in Oraganisational Risk record.             
             */
            organisationalRiskManagementRecordPage
                .InsertNextReviewDate(CurrentDate)
                .ClickSaveButton()
                .ValidateNextReviewDateField(CurrentDate);

            //RiskID = _organisationalRiskRecordPage.GetRiskNumber();

            #endregion

            #region Verify that record is saved when Next Review Date greater than Risk Identification Date

            /*             
             * Verify that user successfully saved the record when
             * Next Review Date is greater than Risk Identification Date in Oraganisational Risk record.             
             */
            organisationalRiskManagementRecordPage
                .InsertNextReviewDate(FutureDate)
                .ClickSaveButton()
                .ValidateNextReviewDateField(FutureDate);

            organisationalRiskManagementRecordPage
                .NavigateToActionPlansTab();

            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .ClickNewRecordButton();

            #endregion


            #region Verify that validation message is displayed when Next Review Date is older than Action Plan record Creation Date

            /*             
             * Verify that Validation Message "The Next Review Date must be later than the Creation Date" should be displayed
             * when Next Review Date is  less than Action Plan record creation Date  
             */
            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .InsertTitle("Action Plan")
                .SelectStatusForAction("In Progress")
                .InsertNextReviewDate(OlderDate)
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("The Next Review Date must be later than the Creation Date")
                 .TapCloseButton();

            #endregion

            #region Verify that action plan record is saved when Next Review Date is equal to record creation date

            /*
             * Verify that user successfully saved the record with Next Review Date equal to Action Plan creation Date.             
             */
            actionPlansRecordPage
                .InsertNextReviewDate(CurrentDate)
                .ClickSaveButton();

            System.Threading.Thread.Sleep(1000);
            actionPlansRecordPage
                .ValidateNextReviewDateField(CurrentDate);

            #endregion

            //ActionPlanNumber = _actionPlansRecordPage.GetActionPlansRecordNumber();

            #region Verify that action plan record is saved when Next Review Date is greater than record creation date

            /*
             * Verify that user successfully saved the record with Next Review Date greater than Action Plan creation Date.              
             */
            actionPlansRecordPage
                .InsertNextReviewDate(FutureDate)
                .ClickSaveButton();

            System.Threading.Thread.Sleep(1000);
            actionPlansRecordPage
                .ValidateNextReviewDateField(FutureDate);

            #endregion

            #region Verify that validation message is displayed when Action Plan Closure Date is older than Creation Date

            Thread.Sleep(5000);

            actionPlansRecordPage
                .WaitForActionPlansRecordPageElementsToLoad()
                .SelectStatusForAction("Completed");


            actionPlansRecordPage
              .WaitForActionPlansRecordPageElementsToLoad()
              .InsertActionPlanClosureDate(OlderDate)
              .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("The Closed Date must be later than the Creation Date")
                 .TapCloseButton();

            #endregion

            #region Verify that record is saved when Action Plan Closure Date is equal to Creation Date

            actionPlansRecordPage
                .WaitForActionPlansRecordPageElementsToLoad()
                .InsertActionPlanClosureDate(CurrentDate)
                .ClickSaveButton()
                .ValidateActionPlanClosureDateField(CurrentDate)
                .ClickBackButton();

            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .VerifyNoRecordsFoundsMessage()
                .ViewDetailsTab();

            #endregion

            #region Verify that validation message is displayed when Risk Closed Date is older than Risk Identified Date

            organisationalRiskManagementRecordPage
                .SelectRiskStatus("Completed")
                .InsertRiskClosedDate(OlderDate)
                .ClickSaveButton();

            dynamicDialogPopup
                 .WaitForDynamicDialogPopupToLoad()
                 .ValidateMessage("The Closed Date must be later than the Risk Identification Date")
                 .TapCloseButton();

            #endregion

            #region Verify that record is save when Risk Closed Date is equal to Risk Identified Date

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskClosedDate(CurrentDate)
                .ClickSaveButton()
                .ValidateRiskClosedDateField(CurrentDate);

            #endregion
        }


        //Validate greater Closure date in Organisation Risk Management Record and Action Plan record
        [TestProperty("JiraIssueID", "ACC-3174")]
        [Description("Verify that Action plan record and Organisational Risk Record is saved for below:" +
            "Organisational Risk: Record is saved when Risk Closed Date is later than Risk Identification Date." +
            "Action Plan: Record is saved when Closure Date is later than Action Plan record creation Date.")]
        [TestMethod, TestCategory("IntegrationTestLevel3_CareProviders_AWS")]
        [TestProperty("BusinessModule", "Quality and Compliance")]
        [TestProperty("Screen1", "Organisational Risks")]
        [TestProperty("Screen2", "Organisational Risk Action Plans")]
        public void OrganisationalRisk_ActionPlans_UITestMethod03()
        {
            string CurrentDate = commonMethodsHelper.GetCurrentDate();
            string OlderDate = commonMethodsHelper.GetOlderDate();
            string FutureDate = commonMethodsHelper.GetFutureDate();

            loginPage
                .GoToLoginPage()
                .Login(_loginUsername, "Passw0rd_!", EnvironmentName)
                .WaitForCareProvidermHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToOrganisationalRiskManagementPage();

            organisationalRiskManagementPage
                .WaitForOrganisationalRisksPageToLoad()
                .ClickNewRecordButton();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .ClickRiskTypeLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("CD Automation Risk")
                .TapSearchButton()
                .SelectResultElement(_riskTypeId.ToString());

            #region Verify that record is saved when Action Plan Closure Date is Later than Creation Date

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .InsertRiskDescription("Test Description")
                .SelectRiskStatus("New")
                .SelectCorporateRiskRegisterValue("No")
                .InsertRiskConsequenceValue("3")
                .InsertRiskLikelihoodValue("3")
                .InsertResidualConsequenceValue("2")
                .InsertResidualLikelihoodValue("4")
                .InsertNextReviewDate(CurrentDate)
                .ClickSaveButton();

            organisationalRiskManagementRecordPage
                .NavigateToActionPlansTab();

            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .ClickNewRecordButton();

            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .InsertTitle("Action Plan Record" + DateTime.UtcNow.ToString("dd.MM.yyyy.hh.mm.ss"))
                .SelectStatusForAction("In Progress")
                .InsertActionPlanDescription("Action Plan Record: " + DateTime.UtcNow.ToString("dd.MM.yyyy.hh.mm.ss"))
                .ClickResponsibleUserLookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .SelectLookIn("Lookup View")
                .TypeSearchQuery("Testing_CDV6_User_03")
                .TapSearchButton()
                .SelectResultElement(_systemUserId.ToString());

            actionPlansRecordPage
                .WaitForActionPlansRecordPageToLoad()
                .ClickSaveButton();

            Thread.Sleep(5000);

            actionPlansRecordPage
                .WaitForActionPlansRecordPageElementsToLoad()
                .SelectStatusForAction("Cancelled")
                .InsertActionPlanClosureDate(FutureDate)
                .ClickSaveButton();

            actionPlansRecordPage
                .WaitForActionPlansRecordPageElementsToLoad()
                .ValidateActionPlanClosureDateField(FutureDate)
                .ClickBackButton();
            #endregion


            #region Verify that record is saved when Organisational Risk Closed Date is Later than Risk Identification Date
            actionPlansPage
                .WaitForActionPlansPageToLoad()
                .VerifyNoRecordsFoundsMessage()
                .ViewDetailsTab();

            organisationalRiskManagementRecordPage
                .WaitForRiskRecordPageToLoad()
                .SelectRiskStatus("Cancelled")
                .InsertRiskClosedDate(FutureDate)
                .ClickSaveButton()
                .ValidateRiskClosedDateField(FutureDate);

            #endregion


        }


        #endregion

    }
}
