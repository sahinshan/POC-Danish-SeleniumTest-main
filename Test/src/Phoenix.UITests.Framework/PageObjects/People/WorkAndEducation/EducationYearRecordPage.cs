using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class EducationYearRecordPage : CommonMethods
    {
        public EducationYearRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Iframe

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personeducationyear&')]");

        #endregion


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");


        #region Field Labels

        readonly By personFieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']/label[text()='Person']");

        #endregion

        #region Fields

        readonly By personLinkField = By.XPath("//li[@id='CWControlHolder_personid']/div/div/a[@id='CWField_personid_Link']");
        readonly By personLookupButton = By.XPath("//li[@id='CWControlHolder_personid']/div/div/button[@id='CWLookupBtn_personid']");

        readonly By AttendedEducationEstablishment_LinkField = By.XPath("//a[@id='CWField_personattendededucationestablishmentid_Link']");
        readonly By AttendedEducationEstablishment_LookupButton = By.XPath("//*[@id='CWLookupBtn_personattendededucationestablishmentid']");

        #endregion


        public EducationYearRecordPage WaitForEducationYearRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(pageHeader);

            WaitForElement(personFieldLabel);
            WaitForElement(personLookupButton);
            WaitForElement(personLinkField);

            WaitForElement(AttendedEducationEstablishment_LinkField);
            WaitForElement(AttendedEducationEstablishment_LookupButton);

            return this;
        }

        public EducationYearRecordPage ClickBackButton()
        {
            WaitForElementToBeClickable(backButton);
            Click(backButton);

            return this;
        }

        public EducationYearRecordPage ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public EducationYearRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementToBeClickable(saveAndCloseButton);
            Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public EducationYearRecordPage ClickAttendedEducationEstablishmentLookupButton()
        {
            WaitForElementToBeClickable(AttendedEducationEstablishment_LookupButton);
            Click(AttendedEducationEstablishment_LookupButton);

            return this;
        }

    }
}
