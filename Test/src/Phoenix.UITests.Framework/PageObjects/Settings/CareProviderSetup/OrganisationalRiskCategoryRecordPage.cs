using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.CareProviderSetup
{
    public class OrganisationalRiskCategoryRecordPage : CommonMethods
    {
        public OrganisationalRiskCategoryRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By OrgRiskCategoryRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=organisationalriskcategory&')]");

        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By ActivateButton = By.Id("TI_ActivateButton");
        readonly By BackButton = By.XPath("//button[@title='Back']");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");
        readonly By LoadingImage = By.XPath("//*[@class = 'loader']");
        
        #endregion

        #region General Fields

        readonly By RiskCategoryName_Field = By.Id("CWField_name");
        readonly By RiskCategoryValueFrom_Field = By.Id("CWField_valuefrom");
        readonly By RiskCategoryValueTo_Field = By.Id("CWField_valueto");
        readonly By RiskCategoryActive_RadioButton = By.XPath("//input[@id = 'CWField_inactive_0']");
        readonly By RiskCategoryInactive_RadioButton = By.XPath("//input[@id = 'CWField_inactive_1']");

        readonly By RiskCategoryName_Label = By.Id("CWField_name");
        readonly By RiskCategoryValueFrom_Label = By.Id("CWField_valuefrom");
        readonly By RiskCategoryValueTo_Label = By.Id("CWField_valueto");
        readonly By RiskCategoryActive_Label = By.Id("CWField_inactive_0");

       
        readonly By ValueToField_ErrorLabel = By.XPath("//*[@id = 'CWControlHolder_valueto']/label/span");
        readonly By ValueFromField_ErrorLabel = By.XPath("//*[@id = 'CWControlHolder_valuefrom']/label/span");


        #endregion

        #region Field titles
        readonly By ValueFromField_Title = By.XPath("//input[@id = 'CWField_valuefrom'][@title = 'Value From: Min: 1, Max 25']");        
        readonly By ValueToField_Title = By.XPath("//input[@id = 'CWField_valueto'][@title = 'Value To: Min: 1, Max 25']");
        #endregion


        #region Alert Message
        readonly By notificationMessage = By.Id("CWNotificationMessage_DataForm");
        readonly By CloseButton = By.Id("CWCloseButton");

        #endregion

        readonly By InactiveRecordFooterLabel = By.XPath("//label[text()='Active']/following-sibling::span[text() = 'No']");
        readonly By ActiveRecordFooterLabel = By.XPath("//label[text()='Active']/following-sibling::span[text() = 'Yes']");

        public OrganisationalRiskCategoryRecordPage WaitForRiskCategoryRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(OrgRiskCategoryRecordIFrame);
            SwitchToIframe(OrgRiskCategoryRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage WaitForInactiveRiskCategoryRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(OrgRiskCategoryRecordIFrame);
            SwitchToIframe(OrgRiskCategoryRecordIFrame);

            WaitForElement(ActivateButton);

            WaitForElementNotVisible(SaveButton, 7);
            WaitForElementNotVisible(SaveAndCloseButton, 7);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ClickSaveButton()
        {
            WaitForElementNotVisible(LoadingImage, 15);
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            while (GetElementVisibility(LoadingImage))
            {
                WaitForElementNotVisible(LoadingImage, 5);
            }

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible(LoadingImage, 15);
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            while (GetElementVisibility(LoadingImage))
            {
                WaitForElementNotVisible(LoadingImage, 5);
            }

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(BackButton);
            MoveToElementInPage(BackButton);
            Click(BackButton);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage AcceptBrowserAlert()
        {
            AcceptAlert();

            return this;
        }


        public OrganisationalRiskCategoryRecordPage ValidateMessageAreaText()
        {
            WaitForElement(notificationMessage);
            Assert.AreEqual("Some data is not correct. Please review the data in the Form.", GetElementText(notificationMessage));

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ValidateValueFromFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementByTitle(ValueFromField_ErrorLabel, ExpectedText);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ValidateValueToFieldErrorLabelText(string ExpectedText)
        {
            ValidateElementText(ValueToField_ErrorLabel, ExpectedText);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ClickCloseButton()
        {
            WaitForElement(CloseButton);
            Click(CloseButton);

            return this;
        }

        #region Risk Category Details Field population methods


        public OrganisationalRiskCategoryRecordPage InsertRiskCategoryName(string RiskNameText)
        {
            WaitForElement(RiskCategoryName_Field, 5);
            SendKeys(RiskCategoryName_Field, RiskNameText);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage InsertValueFromField(string ValueFrom)
        {
            WaitForElement(RiskCategoryValueFrom_Field);
            SendKeys(RiskCategoryValueFrom_Field, ValueFrom);
            SendKeysWithoutClearing(RiskCategoryValueFrom_Field, Keys.Tab);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage InsertValueToField(string ValueTo)
        {
            WaitForElement(RiskCategoryValueTo_Field);
            SendKeys(RiskCategoryValueTo_Field, ValueTo);
            SendKeysWithoutClearing(RiskCategoryValueTo_Field, Keys.Tab);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage InsertValueFromAndValueToField(string ValueFrom, string ValueTo)
        {
            WaitForElement(RiskCategoryValueFrom_Field);
            WaitForElement(RiskCategoryValueTo_Field);
            SendKeys(RiskCategoryValueFrom_Field, ValueFrom);
            SendKeys(RiskCategoryValueTo_Field, ValueTo);            

            return this;
        }

        public OrganisationalRiskCategoryRecordPage SelectInactiveRadioButton()
        {
            WaitForElement(RiskCategoryInactive_RadioButton);
            Click(RiskCategoryInactive_RadioButton);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage SelectActiveRadioButton()
        {
            WaitForElement(RiskCategoryActive_RadioButton);
            Click(RiskCategoryActive_RadioButton);

            return this;
        }


        #endregion


        public string GetRiskCategoryName()
        {
            string riskCategory = GetElementValue(RiskCategoryName_Field);

            return riskCategory;
        }

        public OrganisationalRiskCategoryRecordPage ValidateRiskCategoryName(string expectedText)
        {
            WaitForElement(RiskCategoryName_Field);
            string actualValue = GetElementValue(RiskCategoryName_Field);
            Assert.AreEqual(expectedText, actualValue);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ValidateValueFromField(string expectedText)
        {
            WaitForElement(RiskCategoryValueFrom_Field);
            string actualValue = GetElementValue(RiskCategoryValueFrom_Field);
            Assert.AreEqual(expectedText, actualValue);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ValidateValueToField(string expectedText)
        {
            WaitForElement(RiskCategoryValueTo_Field);
            string actualValue = GetElementValue(RiskCategoryValueTo_Field);
            Assert.AreEqual(expectedText, actualValue);

            return this;
        }



        public OrganisationalRiskCategoryRecordPage ValidateIntialRiskScoreSectionFields()
        {
            WaitForElement(RiskCategoryName_Label);
            WaitForElement(RiskCategoryValueFrom_Label);
            WaitForElement(RiskCategoryValueTo_Label);

            WaitForElement(RiskCategoryName_Field);
            ValidateElementEnabled(RiskCategoryName_Field);

            WaitForElement(RiskCategoryValueFrom_Field);
            ValidateElementEnabled(RiskCategoryValueFrom_Field);

            WaitForElement(RiskCategoryValueTo_Field);
            ValidateElementEnabled(RiskCategoryValueTo_Field);

            return this;
        }


        public OrganisationalRiskCategoryRecordPage ValidateValueFromMinMaxScore(string ExpectedText)
        {
            WaitForElement(ValueFromField_Title);
            ValidateElementByTitle(ValueFromField_Title, ExpectedText);

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ValidateValueToMinMaxScore(string ExpectedText)
        {
            WaitForElement(ValueToField_Title);
            ValidateElementByTitle(ValueToField_Title, ExpectedText);

            return this;
        }
        

        public OrganisationalRiskCategoryRecordPage ValidateInactiveRecordFooterLabel()
        {
            Assert.IsTrue(GetElementVisibility(InactiveRecordFooterLabel));

            return this;
        }

        public OrganisationalRiskCategoryRecordPage ValidateActiveRecordFooterLabel()
        {
            Assert.IsTrue(GetElementVisibility(ActiveRecordFooterLabel));

            return this;
        }


    }
}
