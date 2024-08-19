using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Phoenix.UITests.Settings.Configuration
{
    /// <summary>
    /// This class contains Automated UI test scripts for 
    /// </summary>
    [TestClass]
    public class SystemManagement_UITestCases : FunctionalTest
    {

        #region https://advancedcsg.atlassian.net/browse/CDV6-8704

        [TestProperty("JiraIssueID", "CDV6-24966")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Validate that the New EDMS Repository record page is displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod001()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad();
        }

        [TestProperty("JiraIssueID", "CDV6-24967")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - " +
            "Validate that by default the Azure Connection and Azure Container Name fields are not displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod002()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .ValidateAzureConnectionVisibility(false)
                .ValidateAzureConnectionErrorLabelVisibility(false)

                .ValidateAzureContainerNameVisibility(false)
                .ValidateAzureContainerNameErrorLabelVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24968")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to External - " +
            "Validate that by the Azure Connection and Azure Container Name fields are not displayed")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod003()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("External")

                .ValidateAzureConnectionVisibility(false)
                .ValidateAzureConnectionErrorLabelVisibility(false)

                .ValidateAzureContainerNameVisibility(false)
                .ValidateAzureContainerNameErrorLabelVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24969")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to Azure Blob Storage - " +
            "Insert the text 'ab' in the Azure Container Name value field - Validate that the REGEX error is activated and displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod005()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("Azure Blob Storage")

                .InsertAzureContainerName("ab")

                .ValidateAzureContainerNameErrorLabelVisibility(true)
                .ValidateAzureContainerNameErrorLabelText("This name may only contain lowercase letters, numbers, and hyphens, and must begin with a letter or a number. Each hyphen must be preceded and followed by a non-hyphen character. The name must also be between 3 and 63 characters long.");
        }

        [TestProperty("JiraIssueID", "CDV6-24970")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to Azure Blob Storage - " +
            "Insert the text 'abC' in the Azure Container Name value field - Validate that the REGEX error is activated and displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod006()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("Azure Blob Storage")

                .InsertAzureContainerName("abC")

                .ValidateAzureContainerNameErrorLabelVisibility(true)
                .ValidateAzureContainerNameErrorLabelText("This name may only contain lowercase letters, numbers, and hyphens, and must begin with a letter or a number. Each hyphen must be preceded and followed by a non-hyphen character. The name must also be between 3 and 63 characters long.");
        }

        [TestProperty("JiraIssueID", "CDV6-24971")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to Azure Blob Storage - " +
            "Insert the text 'abc' in the Azure Container Name value field - Validate that the NO ERROR is displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod007()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("Azure Blob Storage")

                .InsertAzureContainerName("abc")

                .ValidateAzureContainerNameErrorLabelVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24972")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to Azure Blob Storage - " +
            "Insert the text 'abc--def' in the Azure Container Name value field - Validate that the REGEX error is activated and displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod008()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("Azure Blob Storage")

                .InsertAzureContainerName("abc--def")

                .ValidateAzureContainerNameErrorLabelVisibility(true)
                .ValidateAzureContainerNameErrorLabelText("This name may only contain lowercase letters, numbers, and hyphens, and must begin with a letter or a number. Each hyphen must be preceded and followed by a non-hyphen character. The name must also be between 3 and 63 characters long.");
        }

        [TestProperty("JiraIssueID", "CDV6-24973")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to Azure Blob Storage - " +
            "Insert the text 'abc-def' in the Azure Container Name value field - Validate that the NO ERROR is displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod009()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("Azure Blob Storage")

                .InsertAzureContainerName("abc-def")

                .ValidateAzureContainerNameErrorLabelVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24974")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to Azure Blob Storage - " +
            "Insert the text '123-456' in the Azure Container Name value field - Validate that the NO ERROR is displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod010()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("Azure Blob Storage")

                .InsertAzureContainerName("123-456")

                .ValidateAzureContainerNameErrorLabelVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24975")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to Azure Blob Storage - " +
            "Insert the text '1a3-4b6' in the Azure Container Name value field - Validate that the NO ERROR is displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod011()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("Azure Blob Storage")

                .InsertAzureContainerName("1a3-4b6")

                .ValidateAzureContainerNameErrorLabelVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24976")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to Azure Blob Storage - " +
            "Insert the a random GUID in the Azure Container Name value field - Validate that the NO ERROR is displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod012()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("Azure Blob Storage")

                .InsertAzureContainerName(Guid.NewGuid().ToString())

                .ValidateAzureContainerNameErrorLabelVisibility(false);
        }

        [TestProperty("JiraIssueID", "CDV6-24977")]
        [Description("Navigate to the system management page - Click on the EDMS Repositories link - wait for the EDMS Repositories page to load - " +
            "Click on the add new record button - Wait for the EDMS Repository new record page to load - Set Repository type to Azure Blob Storage - " +
            "Insert the text '1a3-4b?' in the Azure Container Name value field - Validate that the a REGEX error is activated and displayed to the user")]
        [TestCategory("UITest")]
        [TestMethod]
        public void EDMSProvider_NewAzureBlobStorageType_UITestMethod013()
        {

            loginPage
                .GoToLoginPage()
                .Login("CW_Forms_Test_User_1", "Passw0rd_!")
                .WaitFormHomePageToLoad();

            mainMenu
                .WaitForMainMenuToLoad()
                .NavigateToSystemManagementSection();

            systemManagementPage
                .WaitForSystemManagementPageToLoad()
                .ClickEDMSRepositoriesLink();

            edmsRepositoriesPage
                .WaitForEDMSRepositoriesPageToLoad()
                .TapNewRecordButton();

            edmsRepositoryRecordPage
                .WaitForEDMSRepositoryRecordPageToLoad()

                .SelectRepositoryType("Azure Blob Storage")

                .InsertAzureContainerName("1a3-4b?")

                .ValidateAzureContainerNameErrorLabelVisibility(true)
                .ValidateAzureContainerNameErrorLabelText("This name may only contain lowercase letters, numbers, and hyphens, and must begin with a letter or a number. Each hyphen must be preceded and followed by a non-hyphen character. The name must also be between 3 and 63 characters long.");
        }

        #endregion


        [TestMethod]
        public void GetTestNames()
        {
            this.GetAllTestNamesAndDescriptions();
        }


    }
}
