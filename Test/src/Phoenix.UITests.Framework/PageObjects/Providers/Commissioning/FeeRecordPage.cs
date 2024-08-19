using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FeeRecordPage : CommonMethods
    {

        public FeeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpfee')]");



        readonly By messageArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalIttemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By UpdateGLCodeButton = By.Id("TI_UpdateGLCode");



        #region Field Title

        readonly By DateRange_SectionTitle = By.XPath("//*[@id='CWSection_General']/fieldset/div/span[text()='Date Range']");

        readonly By StartDate_FieldTitle = By.XPath("//*[@id='CWLabelHolder_startdate']/label");
        readonly By FeeID_FieldTitle = By.XPath("//*[@id='CWLabelHolder_feenumber']/label");
        readonly By GLCode_FieldTitle = By.XPath("//*[@id='CWLabelHolder_glcode']/label");
        
        #endregion


        #region Fields

        readonly By StartDate_Field = By.XPath("//*[@id='CWField_startdate']");

        readonly By FeeID_Field = By.XPath("//*[@id='CWField_feenumber']");
                    
        readonly By GLCode_Field = By.XPath("//*[@id='CWField_glcode']");
        

        #endregion

        #region Menu

        readonly By LeftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By RelatedItemsSubMenuButton = By.XPath("//*[@id='CWNavSubGroup_RelatedItems']/a");
        readonly By Audit_MenuLink = By.XPath("//*[@id='CWNavItem_AuditHistory']");

        #endregion


        public FeeRecordPage WaitForFeeRecordPageToLoad(string Title = null)
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

            this.WaitForElement(DateRange_SectionTitle);

            this.WaitForElement(StartDate_FieldTitle);
            this.WaitForElement(FeeID_FieldTitle);
            this.WaitForElement(GLCode_FieldTitle);

            this.WaitForElementVisible(StartDate_Field);
            this.WaitForElementVisible(FeeID_Field);
            this.WaitForElementVisible(GLCode_Field);

            if(!string.IsNullOrEmpty(Title))
                ValidateElementText(pageHeader, "Contribution:\r\n" + Title);

            return this;
        }



        public FeeRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public FeeRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);
            return this;
        }
        public FeeRecordPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public FeeRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }
        public FeeRecordPage ClickUpdateGLCodeButton()
        {
            WaitForElementToBeClickable(additionalIttemsButton);
            Click(additionalIttemsButton);

            WaitForElementToBeClickable(UpdateGLCodeButton);
            Click(UpdateGLCodeButton);

            return this;
        }



        public FeeRecordPage ValidateMessageAreaText(string ExpectedText)
        {
            ValidateElementText(messageArea, ExpectedText);

            return this;
        }


        public FeeRecordPage ValidateStartDateFieldValue(string ExpectedValue)
        {
            ValidateElementText(StartDate_Field, ExpectedValue);

            return this;
        }
        public FeeRecordPage ValidateFeeIDFieldValue(string ExpectedValue)
        {
            ValidateElementValue(FeeID_Field, ExpectedValue);

            return this;
        }
        public FeeRecordPage ValidateGLCodeFieldValue(string ExpectedValue)
        {
            ValidateElementValue(GLCode_Field, ExpectedValue);

            return this;
        }
        
    }
}
