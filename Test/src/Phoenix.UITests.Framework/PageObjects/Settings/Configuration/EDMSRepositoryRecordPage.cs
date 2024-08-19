    
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class EDMSRepositoryRecordPage : CommonMethods
    {
        public EDMSRepositoryRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By SummaryDashboardRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=edmsrepository&')]"); 


        
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");

        


        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");


        #endregion

        #region FieldNames

        readonly By name_FieldName = By.XPath("//*[@id='CWLabelHolder_name']/label");
        readonly By RepositoryType_FieldName = By.XPath("//*[@id='CWLabelHolder_repositorytypeid']/label");
        readonly By AzureConnection_FieldName = By.XPath("//*[@id='CWLabelHolder_azureconnectionid']/label");
        readonly By AzureConnection_ErrorLabel = By.XPath("//*[@id='CWControlHolder_azureconnectionid']/label/span");
        readonly By AzureContainerName_FieldName = By.XPath("//*[@id='CWLabelHolder_containername']/label");
        readonly By AzureContainerName_ErrorLabel = By.XPath("//*[@id='CWControlHolder_containername']/label/span");
        readonly By BusinessObjectName_FieldName = By.XPath("//*[@id='CWLabelHolder_businessobjecttablename']/label");
        readonly By FieldName_FieldName = By.XPath("//*[@id='CWLabelHolder_businessobjectfieldname']/label");

        #endregion

        #region Fields

        readonly By name_Field = By.Id("CWField_name");
        readonly By RepositoryType_Picklist = By.Id("CWField_repositorytypeid");
        readonly By AzureConnection_LinkField = By.Id("CWField_azureconnectionid_Link");
        readonly By AzureConnection_LookupButton = By.Id("CWLookupBtn_azureconnectionid");
        readonly By AzureContainerName_Field = By.Id("CWField_containername");
        readonly By BusinessObjectName_Field = By.Id("CWField_businessobjecttablename");
        readonly By FieldName_Field = By.Id("CWField_businessobjectfieldname");

        #endregion



        public EDMSRepositoryRecordPage WaitForEDMSRepositoryRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SummaryDashboardRecordIFrame);
            SwitchToIframe(SummaryDashboardRecordIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);

            WaitForElement(name_FieldName);
            WaitForElement(RepositoryType_FieldName);
            WaitForElement(BusinessObjectName_FieldName);
            WaitForElement(FieldName_FieldName);

            return this;
        }




        public EDMSRepositoryRecordPage InsertName(string ValueToInsert)
        {
            SendKeys(name_Field, ValueToInsert);

            return this;
        }
        
        public EDMSRepositoryRecordPage InsertAzureContainerName(string ValueToInsert)
        {
            SendKeys(AzureContainerName_Field, ValueToInsert);
            SendKeysWithoutClearing(AzureContainerName_Field, Keys.Tab);

            return this;
        }

        public EDMSRepositoryRecordPage InsertBusinessObjectName(string ValueToInsert)
        {
            SendKeys(BusinessObjectName_Field, ValueToInsert);

            return this;
        }

        public EDMSRepositoryRecordPage InsertFieldName(string ValueToInsert)
        {
            SendKeys(FieldName_Field, ValueToInsert);

            return this;
        }



        public EDMSRepositoryRecordPage SelectRepositoryType(string TextToSelect)
        {
            SelectPicklistElementByText(RepositoryType_Picklist, TextToSelect);

            return this;
        }


        public EDMSRepositoryRecordPage ClickAzureConnectionLookupButton()
        {
            Click(AzureConnection_LookupButton);

            return this;
        }



        public EDMSRepositoryRecordPage ValidateNotificationMessageAreaVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(notificationMessageArea);
            }
            else
            {
                WaitForElementNotVisible(notificationMessageArea, 7);
            }

            return this;
        }

        public EDMSRepositoryRecordPage ValidateAzureConnectionVisibility(bool ExpectedVisible)
        {
            if(ExpectedVisible)
            {
                WaitForElementVisible(AzureConnection_FieldName);
                WaitForElementVisible(AzureConnection_LookupButton);
            }
            else
            {
                WaitForElementNotVisible(AzureConnection_FieldName, 7);
                WaitForElementNotVisible(AzureConnection_LinkField, 7);
                WaitForElementNotVisible(AzureConnection_LookupButton, 7);
            }

            return this;
        }

        public EDMSRepositoryRecordPage ValidateAzureConnectionErrorLabelVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(AzureConnection_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(AzureConnection_ErrorLabel, 7);
            }

            return this;
        }

        public EDMSRepositoryRecordPage ValidateAzureContainerNameVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(AzureContainerName_FieldName);
            }
            else
            {
                WaitForElementNotVisible(AzureContainerName_FieldName, 7);
            }

            return this;
        }

        public EDMSRepositoryRecordPage ValidateAzureContainerNameErrorLabelVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(AzureContainerName_ErrorLabel);
            }
            else
            {
                WaitForElementNotVisible(AzureContainerName_ErrorLabel, 7);
            }

            return this;
        }



        public EDMSRepositoryRecordPage ValidateNotificationMessageAreaText(string ExpectedText)
        {
            ValidateElementText(notificationMessageArea, ExpectedText);

            return this;
        }
        public EDMSRepositoryRecordPage ValidateAzureConnectionErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AzureConnection_ErrorLabel, ExpectedText);

            return this;
        }
        public EDMSRepositoryRecordPage ValidateAzureContainerNameErrorLabelText(string ExpectedText)
        {
            ValidateElementText(AzureContainerName_ErrorLabel, ExpectedText);

            return this;
        }






        public EDMSRepositoryRecordPage TapSaveButton()
        {
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public EDMSRepositoryRecordPage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public EDMSRepositoryRecordPage TapDeleteButton()
        {
            Click(deleteButton);

            return this;
        }
    }
}
