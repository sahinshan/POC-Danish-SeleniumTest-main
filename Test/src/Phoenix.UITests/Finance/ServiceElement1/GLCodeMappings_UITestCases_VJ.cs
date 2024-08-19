using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Phoenix.UITests.Finance.ServiceMappings
{
    [TestClass]
    public class GLCodeMappings_UITestCases_VJ : FunctionalTest
    {
        private Guid _careDirectorQA_BusinessUnitId;
        private Guid _languageId;
        private Guid _careDirectorQA_TeamId;
        private Guid _authenticationproviderid;
        private string _systemUserName;
        private Guid _systemUserId;
        private Guid _ethnicityId;

        [TestInitialize()]
        public void TestsSetupMethod()
        {
            try
            {
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

                _careDirectorQA_BusinessUnitId = commonMethodsDB.CreateBusinessUnit("CareDirector QA");

                #endregion

                #region Team

                _careDirectorQA_TeamId = commonMethodsDB.CreateTeam("CareDirector QA", null, _careDirectorQA_BusinessUnitId, "907678", "CareDirectorQA@careworkstempmail.com", "CareDirector QA", "020 123456");

                #endregion

                #region System User AllActivitiesUser1

                _systemUserName = "GLCodeMappingsUser1";
                _systemUserId = commonMethodsDB.CreateSystemUserRecord(_systemUserName, "GLCodeMappings", "User1", "Passw0rd_!", _careDirectorQA_BusinessUnitId, _careDirectorQA_TeamId, _languageId, _authenticationproviderid);

                #endregion

                #region Ethnicity

                var ethnicitiesExist = dbHelper.ethnicity.GetEthnicityIdByName("Irish").Any();
                if (!ethnicitiesExist)
                    dbHelper.ethnicity.CreateEthnicity(_careDirectorQA_TeamId, "Irish", new DateTime(2020, 1, 1));
                _ethnicityId = dbHelper.ethnicity.GetEthnicityIdByName("Irish")[0];

                #endregion
            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                throw;
            }
        }

        #region https://advancedcsg.atlassian.net/browse/CDV6-22793

        [TestProperty("JiraIssueID", "CDV6-23209")]
        [Description("Steps 1 to 8 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_UITestMethod001()
        {
            #region Service Element 1

            var serviceElement1Name = "CDV6_23209_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, serviceElement1Name, startDate, code, whotopayid, paymentscommenceid, validRateUnits, false);

            #endregion

            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];

            #endregion

            #region Step 1

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            #endregion

            #region Step 2

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad();

            #endregion

            #region Step 3

            serviceElement1Page
                .ValidateSelectedSystemView("Active Records")
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(serviceElement1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad();

            #endregion

            #region Step 4 & 5

            serviceElement1RecordPage
                .NavigateToGLCodeMappingsTab();

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .ClickNewRecordButton();

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateDefaultFieldsVisible()
                .ValidateServiceElement1LinkFieldText(serviceElement1Name)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidatePositionNumberFieldValue("1");

            #endregion

            #region Step 6 & 7

            glCodeMappingsRecordPage
                .ClickLevel1LookupButton();

            lookupPopup
                .WaitForLookupPopupToLoad()
                .TypeSearchQuery("Service Element 1")
                .TapSearchButton()
                .SelectResultElement(_glCodeLocationId.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateLevel2Visibility(true)
                .ClickSaveButton()
                .WaitForRecordToBeSaved();

            glCodeMappingsRecordPage
                .ValidateLevel1LinkFieldText("Service Element 1")
                .ValidateServiceElement1LinkFieldText(serviceElement1Name)
                .ValidateResponsibleTeamLinkFieldText("CareDirector QA")
                .ValidateResponsibleTeamLookupIsDisabled()
                .ValidateResponsibleTeamRemoveButtonVisibility(false)
                .ValidateGLCodeMappingRecordTitle(serviceElement1Name + " \\ 1 \\ " + "Service Element 1")
                .ClickBackButton();

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .ValidateRecordCellText(1, 2, serviceElement1Name + " \\ 1 \\ " + "Service Element 1");

            #endregion

            #region Step 8

            glCodeMappingsPage
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(1500);

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateDefaultFieldsVisible()
                .ValidatePositionNumberFieldValue("2");

            #endregion

        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22806

        [TestProperty("JiraIssueID", "CDV6-23240")]
        [Description("Steps 20 to 23 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_UITestMethod005()
        {
            #region Service Element 1

            var serviceElement1Name = "CDV6_23240_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, serviceElement1Name, startDate, code, whotopayid, paymentscommenceid, validRateUnits, false);

            #endregion

            #region GL Code Location

            var _glCodeLocationId1 = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];
            var _glCodeLocationId2 = dbHelper.glCodeLocation.GetByName("Provider")[0];
            var _glCodeLocationId3 = dbHelper.glCodeLocation.GetByName("Person ID")[0];

            #endregion

            #region GL Code Mapping

            var glCodeMappingID1 = dbHelper.glCodeMapping.CreateGLCodeMapping(_careDirectorQA_TeamId, serviceElement1, _glCodeLocationId1);
            var glCodeMappingID2 = dbHelper.glCodeMapping.CreateGLCodeMapping(_careDirectorQA_TeamId, serviceElement1, _glCodeLocationId2);
            var glCodeMappingID3 = dbHelper.glCodeMapping.CreateGLCodeMapping(_careDirectorQA_TeamId, serviceElement1, _glCodeLocationId3);

            #endregion

            #region Step 20 & 21

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .ValidateSelectedSystemView("Active Records")
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(serviceElement1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToGLCodeMappingsTab();

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .ValidateRecordCellContent(glCodeMappingID1.ToString(), 2, serviceElement1Name + " \\ 1 \\ " + "Service Element 1")
                .ValidateRecordCellContent(glCodeMappingID1.ToString(), 4, "1")
                .ValidateRecordCellContent(glCodeMappingID2.ToString(), 2, serviceElement1Name + " \\ 2 \\ " + "Provider")
                .ValidateRecordCellContent(glCodeMappingID2.ToString(), 4, "2")
                .ValidateRecordCellContent(glCodeMappingID3.ToString(), 2, serviceElement1Name + " \\ 3 \\ " + "Person ID")
                .ValidateRecordCellContent(glCodeMappingID3.ToString(), 4, "3")
                .ClickNewRecordButton();

            System.Threading.Thread.Sleep(1500);

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateDefaultFieldsVisible()
                .ValidatePositionNumberFieldValue("4")
                .ClickBackButton();

            alertPopup.WaitForAlertPopupToLoad().ValidateAlertText("Your changes have not been saved. To stay on the page so that you can save your changes, click Cancel.").TapOKButton();

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .OpenRecord(glCodeMappingID2.ToString());

            glCodeMappingsRecordPage
                .WaitForGLCodeMappingsRecordPageToLoad()
                .ValidateServiceElement1LookupIsDisabled()
                .ValidatePositionNumberFieldIsDisabled()
                .ValidateResponsibleTeamLookupIsDisabled();

            #endregion

            #region Step 22

            glCodeMappingsRecordPage
                .ClickDeleteButton(true);

            alertPopup
                .WaitForAlertPopupToLoad()
                .ValidateAlertText("By deleting this record, all records with higher Positions will move up one Position. As an alternative, you can update this Position’s details. Please select Cancel to abandon the deletion or click OK to continue with the deletion")
                .TapOKButton();

            System.Threading.Thread.Sleep(3000);

            #endregion

            #region Step 23

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .ValidateRecordCellContent(glCodeMappingID1.ToString(), 2, serviceElement1Name + " \\ 1 \\ " + "Service Element 1")
                .ValidateRecordCellContent(glCodeMappingID1.ToString(), 4, "1")
                .ValidateRecordCellContent(glCodeMappingID3.ToString(), 2, serviceElement1Name + " \\ 2 \\ " + "Person ID")
                .ValidateRecordCellContent(glCodeMappingID3.ToString(), 4, "2")
                .ValidateRecordVisibility(glCodeMappingID2.ToString(), false);

            #endregion
        }

        #endregion

        #region https://advancedcsg.atlassian.net/browse/CDV6-22809

        [TestProperty("JiraIssueID", "CDV6-22808")]
        [Description("Steps 25 to 26 from the original jira test")]
        [TestCategory("IntegrationTestLevel3_SocialCare_AWS")]
        [TestMethod()]
        public void GLCodeMappings_UITestMethod007()
        {
            #region Service Element 1

            var serviceElement1Name = "CDV6_22809_" + commonMethodsHelper.GetCurrentDateTimeString();
            var startDate = commonMethodsHelper.GetCurrentDateWithoutCulture();
            var code = commonMethodsHelper.GetRandomValue(1, 2147483647);
            var whotopayid = 1; // Provider
            var paymentscommenceid = 1; //Actual 
            var defaulRateUnitID = dbHelper.rateUnit.GetByName("Per Day \\ Days").FirstOrDefault();

            var validRateUnits = new List<Guid>();
            validRateUnits.Add(defaulRateUnitID);

            var serviceElement1 = dbHelper.serviceElement1.CreateServiceElement1(_careDirectorQA_TeamId, serviceElement1Name, startDate, code, whotopayid, paymentscommenceid, validRateUnits, false);

            #endregion

            #region GL Code Location

            var _glCodeLocationId = dbHelper.glCodeLocation.GetByName("Service Element 1")[0];

            #endregion

            #region GL Code Mapping

            dbHelper.glCodeMapping.CreateGLCodeMapping(_careDirectorQA_TeamId, serviceElement1, _glCodeLocationId);

            #endregion

            #region Step 25

            loginPage
                .GoToLoginPage()
                .Login(_systemUserName, "Passw0rd_!");

            mainMenu
                .WaitForMainMenuToLoad()
                .ClickAdvancedSearchButton();

            advanceSearchPage
                .WaitForAdvanceSearchPageToLoad()
                .SelectRecordType("GL Code Mappings")
                .SelectSavedView("Active Records")
                .ClickSearchButton();

            advanceSearchPage
                .WaitForResultsPageToLoad()
                .ResultsPageValidateHeaderCellText(2, "Name")
                .ResultsPageValidateHeaderCellText(3, "Service Element 1")
                .ResultsPageValidateHeaderCellSortOrdedAscending(3)
                .ResultsPageValidateHeaderCellText(4, "Position Number")
                .ResultsPageValidateHeaderCellSortOrdedAscending(4)
                .ResultsPageValidateHeaderCellText(5, "Level 1")
                .ResultsPageValidateHeaderCellText(6, "Level 2")
                .ResultsPageValidateHeaderCellText(7, "Level 3")
                .ResultsPageValidateHeaderCellText(8, "Level 4")
                .ResultsPageValidateHeaderCellText(9, "Level 1 Constant")
                .ResultsPageValidateHeaderCellText(10, "Level 2 Constant")
                .ResultsPageValidateHeaderCellText(11, "Level 3 Constant")
                .ResultsPageValidateHeaderCellText(12, "Level 4 Constant")
                .ResultsPageValidateHeaderCellText(13, "If Position")
                .ResultsPageValidateHeaderCellText(14, "Is GL Code Location")
                .ResultsPageValidateHeaderCellText(15, "Method")
                .ResultsPageValidateHeaderCellText(16, "Then Use GL Code Location")
                .ResultsPageValidateHeaderCellText(17, "Then Use Position")
                .ResultsPageValidateHeaderCellText(18, "Updateable on Financial Assessment?")
                .ResultsPageValidateHeaderCellText(19, "Updateable on Service Provision?");

            #endregion

            #region Step 26

            mainMenu
               .WaitForMainMenuToLoad()
               .NavigateToReferenceDataSection();

            referenceDataPage
                .WaitForReferenceDataPageToLoad()
                .InsertSearchQuery("Service Element 1")
                .ClickReferenceDataMainHeader("Service Provision")
                .ClickReferenceDataElement("Service Element 1");

            serviceElement1Page
                .WaitForServiceElement1PageToLoad()
                .ValidateSelectedSystemView("Active Records")
                .InsertSearchQuery(serviceElement1Name)
                .TapSearchButton()
                .OpenRecord(serviceElement1.ToString());

            serviceElement1RecordPage
                .WaitForServiceElement1RecordPageToLoad()
                .NavigateToGLCodeMappingsTab();

            glCodeMappingsPage
                .WaitForGLCodeMappingsPageToLoad()
                .ValidateSelectedSystemView("Related Records")
                .ValidateHeaderCellText(2, "Name")
                .ValidateHeaderCellText(3, "Service Element 1")
                .ValidateHeaderCellSortOrdedAscending(3)
                .ValidateHeaderCellText(4, "Position Number")
                .ValidateHeaderCellSortOrdedAscending(4)
                .ValidateHeaderCellText(5, "Level 1")
                .ValidateHeaderCellText(6, "Level 2")
                .ValidateHeaderCellText(7, "Level 3")
                .ValidateHeaderCellText(8, "Level 4")
                .ValidateHeaderCellText(9, "Level 1 Constant")
                .ValidateHeaderCellText(10, "Level 2 Constant")
                .ValidateHeaderCellText(11, "Level 3 Constant")
                .ValidateHeaderCellText(12, "Level 4 Constant")
                .ValidateHeaderCellText(13, "If Position")
                .ValidateHeaderCellText(14, "Is GL Code Location")
                .ValidateHeaderCellText(15, "Method")
                .ValidateHeaderCellText(16, "Then Use GL Code Location")
                .ValidateHeaderCellText(17, "Then Use Position")
                .ValidateHeaderCellText(18, "Updateable on Financial Assessment?")
                .ValidateHeaderCellText(19, "Updateable on Service Provision?");

            #endregion
        }

        #endregion

    }
}
