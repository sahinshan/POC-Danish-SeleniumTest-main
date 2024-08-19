using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FinancialAssessmentContributionRecordPage : CommonMethods
    {

        public FinancialAssessmentContributionRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=facontribution')]");



        readonly By messageArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalIttemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");



        #region Field Title

        readonly By general_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div[1]/span[text()='General']");

        readonly By FinancialAssessment_FieldTitle = By.XPath("//*[@id='CWLabelHolder_financialassessmentid']/label");
        readonly By ID_FieldTitle = By.XPath("//*[@id='CWLabelHolder_facontributionnumber']/label");
        readonly By GLCode_FieldTitle = By.XPath("//*[@id='CWLabelHolder_glcode']/label");
        
        #endregion


        #region Fields

        readonly By FinancialAssessment_LinkField = By.XPath("//*[@id='CWField_financialassessmentid_Link']");
        readonly By FinancialAssessment_LookupButton = By.XPath("//*[@id='CWLookupBtn_financialassessmentid']");

        readonly By ServiceProvision_LinkField = By.XPath("//*[@id='CWField_serviceprovisionid_Link']");
        readonly By ServiceProvision_LookupButton = By.XPath("//*[@id='CWLookupBtn_serviceprovisionid']");
        readonly By ServiceProvision_RemoveButton = By.XPath("//*[@id='CWClearLookup_serviceprovisionid']");

        readonly By ID_Field = By.XPath("//*[@id='CWField_facontributionnumber']");
                    
        readonly By GLCode_Field = By.XPath("//*[@id='CWField_glcode']");
        

        #endregion

        #region Menu

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By RelatedItemsSubMenuButton = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By Audit_MenuLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        #endregion


        public FinancialAssessmentContributionRecordPage WaitForFinancialAssessmentContributionRecordPageToLoad(string Title = null)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            this.WaitForElement(backButton);
            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            this.WaitForElement(pageHeader);

            this.WaitForElement(general_SectionTitle);

            this.WaitForElement(FinancialAssessment_FieldTitle);
            this.WaitForElement(ID_FieldTitle);
            this.WaitForElement(GLCode_FieldTitle);

            this.WaitForElementVisible(FinancialAssessment_LookupButton);
            this.WaitForElementVisible(ID_Field);
            this.WaitForElementVisible(GLCode_Field);

            if(!string.IsNullOrEmpty(Title))
                ValidateElementText(pageHeader, "Contribution:\r\n" + Title);

            return this;
        }



        public FinancialAssessmentContributionRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public FinancialAssessmentContributionRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }
        public FinancialAssessmentContributionRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public FinancialAssessmentContributionRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }


        public FinancialAssessmentContributionRecordPage ClickServiceProvisionLookupButton()
        {
            WaitForElementToBeClickable(ServiceProvision_LookupButton);
            Click(ServiceProvision_LookupButton);

            return this;
        }



        public FinancialAssessmentContributionRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(messageArea, ExpectedText);

            return this;
        }

        public FinancialAssessmentContributionRecordPage ValidateFinancialAssessmentLinkFieldText(string ExpectedText)
        {
            ValidateElementText(FinancialAssessment_LinkField, ExpectedText);

            return this;
        }
        public FinancialAssessmentContributionRecordPage ValidateIDFieldValue(string ExpectedValue)
        {
            ValidateElementValue(ID_Field, ExpectedValue);

            return this;
        }
        public FinancialAssessmentContributionRecordPage ValidateGLCodeFieldValue(string ExpectedValue)
        {
            ValidateElementValue(GLCode_Field, ExpectedValue);

            return this;
        }
        
    }
}
