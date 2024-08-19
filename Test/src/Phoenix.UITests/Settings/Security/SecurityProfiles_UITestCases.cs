using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.Settings.Security
{
    /// <summary>
    /// https://advancedcsg.atlassian.net/browse/CDV6-788
    ///
    /// </summary>
    [TestClass]
    public class SecurityProfiles_UITestCases : FunctionalTest
    {


        #region https://advancedcsg.atlassian.net/browse/CDV6-8546

        [Description("Open a security profile record with privileges assigned to different business objects - " +
            "validate that the business objects with privileges assigned are displayed on top")]
        [TestCategory("UITest")]
        [TestMethod]
        [TestProperty("JiraIssueID", "CDV6-24580")]
        public void SecurityProfile_Usability_UITestMethod001()
        {
            Guid securityProfileID = new Guid("e6a7c1d5-9fdf-e511-80cb-0050560502cc");//CW Alert/Hazard Module (BU Edit)

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSecurityProfileSection();

            securityProfilesPage
                .WaitForSecurityProfilesPageToLoad()
                .InsertQuickSearchText("CW Alert/Hazard Module (BU Edit)")
                .ClickQuickSearchButton()
                .OpenRecord(securityProfileID.ToString());

            securityProfileRecordPage
                .WaitForSecurityProfileRecordPageToLoad()
                .ClickRecordPrivilegesTab()
                .WaitForRecordPrivilegesTabToLoad()

                .ValidateBusinessObjectName("1", "Person")
                .ValidateBusinessObjectViewPrivilege("1", "Own Business Unit Records")
                .ValidateBusinessObjectCreatePrivilege("1", "None")
                .ValidateBusinessObjectEditPrivilege("1", "None")
                .ValidateBusinessObjectDeletePrivilege("1", "None")
                .ValidateBusinessObjectSharePrivilege("1", "None")

                .ValidateBusinessObjectName("2", "Person Alert And Hazard")
                .ValidateBusinessObjectViewPrivilege("2", "Own Business Unit Records")
                .ValidateBusinessObjectCreatePrivilege("2", "Own Business Unit Records")
                .ValidateBusinessObjectEditPrivilege("2", "Own Business Unit Records")
                .ValidateBusinessObjectDeletePrivilege("2", "None")
                .ValidateBusinessObjectSharePrivilege("2", " ")

                .ValidateBusinessObjectName("3", "Person Alert And Hazard Review")
                .ValidateBusinessObjectViewPrivilege("3", "Own Business Unit Records")
                .ValidateBusinessObjectCreatePrivilege("3", "Own Business Unit Records")
                .ValidateBusinessObjectEditPrivilege("3", "Own Business Unit Records")
                .ValidateBusinessObjectDeletePrivilege("3", "None")
                .ValidateBusinessObjectSharePrivilege("3", " ")

                ;

        }

        #endregion
    }
}
