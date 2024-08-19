    
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FAQApplicationComponentRecordPage : CommonMethods
    {
        public FAQApplicationComponentRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By SummaryDashboardRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicationcomponent&')]"); 


        
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        
        readonly By notificationMessageArea = By.Id("CWNotificationMessage_DataForm");

        


        #region Navigation Area

        readonly By MenuButton = By.XPath("//li[@id='CWNavGroup_Menu']/button");

        readonly By DetailsSection = By.XPath("//li[@id='CWNavGroup_EditForm']/a[@title='Details']");

        #endregion

        #region FieldTitles

        readonly By application_FieldTitle = By.XPath("//*[@id='CWLabelHolder_applicationid']/label");
        readonly By component_FieldTitle = By.XPath("//*[@id='CWLabelHolder_componentid']/label");
        readonly By validforexport_FieldTitle = By.XPath("//*[@id='CWLabelHolder_validforexport']/label");
        

        #endregion

        #region Fields

        readonly By application_Picklist = By.Id("CWField_applicationid");
        
        readonly By component_LinkField = By.Id("CWField_componentid_Link");
        readonly By component_RemoveButton = By.Id("CWClearLookup_componentid");
        readonly By component_LookupButton = By.Id("CWLookupBtn_componentid");

        readonly By ValidForExport_YesRadioButton = By.Id("CWField_validforexport_1");
        readonly By ValidForExport_NoRadioButton = By.Id("CWField_validforexport_0");
        

        #endregion



        public FAQApplicationComponentRecordPage WaitForFAQApplicationComponentRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SummaryDashboardRecordIFrame);
            SwitchToIframe(SummaryDashboardRecordIFrame);

            WaitForElement(saveButton);
            WaitForElement(saveAndCloseButton);
            
            WaitForElement(DetailsSection);

            WaitForElement(application_FieldTitle);
            WaitForElement(component_FieldTitle);
            WaitForElement(validforexport_FieldTitle);

            return this;
        }




        public FAQApplicationComponentRecordPage SelectApplication(string TextToSelect)
        {
            SelectPicklistElementByText(application_Picklist, TextToSelect);

            return this;
        }

        public FAQApplicationComponentRecordPage TapComponentLookupButton()
        {
            Click(component_LookupButton);

            return this;
        }

        public FAQApplicationComponentRecordPage TapComponentRemoveButton()
        {
            Click(component_RemoveButton);

            return this;
        }

        public FAQApplicationComponentRecordPage TapValidForExportYesRadioButton()
        {
            Click(ValidForExport_YesRadioButton);

            return this;
        }

        public FAQApplicationComponentRecordPage TapValidForExportNoRadioButton()
        {
            Click(ValidForExport_NoRadioButton);

            return this;
        }




        public FAQApplicationComponentRecordPage ValidateApplicationValue(string ExpectedValue)
        {
            ValidatePicklistSelectedText(application_Picklist, ExpectedValue);

            return this;
        }

        public FAQApplicationComponentRecordPage ValidateComponentValue(string ExpectedValue)
        {
            ValidateElementText(component_LinkField, ExpectedValue);

            return this;
        }

        public FAQApplicationComponentRecordPage ValidateValidForExportYesRadioBuuttonChecked()
        {
            ValidateElementChecked(ValidForExport_YesRadioButton);

            return this;
        }
        
        public FAQApplicationComponentRecordPage ValidateValidForExportNoRadioBuuttonChecked()
        {
            ValidateElementChecked(ValidForExport_NoRadioButton);

            return this;
        }





        


        public FAQApplicationComponentRecordPage TapSaveButton()
        {
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FAQApplicationComponentRecordPage TapSaveAndCloseButton()
        {
            Click(saveAndCloseButton);

            //WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FAQApplicationComponentRecordPage TapDeleteButton()
        {
            Click(deleteButton);

            return this;
        }
    }
}
