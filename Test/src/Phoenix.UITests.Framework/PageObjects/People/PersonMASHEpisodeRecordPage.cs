using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonMASHEpisodeRecordPage : CommonMethods
    {

        public PersonMASHEpisodeRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=mashepisode')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By additionalItemsButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By backButton = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");

        readonly By leftMenuButton = By.XPath("//*[@id='CWNavGroup_Menu']/a");
        readonly By MASHEpisodeFormLeftMenuButton = By.XPath("//*[@id='CWNavItem_MASHEpisodeForm']");


        #region Fields

        readonly By DateTimeContactReceived_DateField = By.XPath("//*[@id='CWField_contactreceiveddatetime']");
        readonly By DateTimeContactReceived_TimeField = By.XPath("//*[@id='CWField_contactreceiveddatetime_Time']");
        readonly By DateTimeCaseRecorded_DateField = By.XPath("//*[@id='CWField_caserecordeddatetime']");
        readonly By DateTimeCaseRecorded_TimeField = By.XPath("//*[@id='CWField_caserecordeddatetime_Time']");

        #endregion

        public PersonMASHEpisodeRecordPage WaitForPersonMASHEpisodeRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }



        public PersonMASHEpisodeRecordPage InsertDateTimeContactReceived(string DateToInsert, string TimeToInsert)
        {
            SendKeys(DateTimeContactReceived_DateField, DateToInsert);
            SendKeys(DateTimeContactReceived_TimeField, TimeToInsert);

            return this;
        }

        public PersonMASHEpisodeRecordPage InsertDateTimeCaseRecorded(string DateToInsert, string TimeToInsert)
        {
            SendKeys(DateTimeCaseRecorded_DateField, DateToInsert);
            SendKeys(DateTimeCaseRecorded_TimeField, TimeToInsert);

            return this;
        }

        public PersonMASHEpisodeRecordPage ValidateDateTimeCaseRecorded(string ExpectedDate, string ExpectedTime)
        {
            ValidateElementValue(DateTimeCaseRecorded_DateField, ExpectedDate);
            ValidateElementValue(DateTimeCaseRecorded_TimeField, ExpectedTime);

            return this;
        }



        public PersonMASHEpisodeRecordPage ClickSaveButton()
        {
            WaitForElement(saveButton);
            Click(saveButton);

            return this;
        }
        public PersonMASHEpisodeRecordPage ClickSaveAndCloseButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(saveAndCloseButton);
            Click(saveAndCloseButton);

            return this;
        }
        public PersonMASHEpisodeRecordPage ClickDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElementToBeClickable(additionalItemsButton);
            Click(additionalItemsButton);

            WaitForElementToBeClickable(deleteButton);
            Click(deleteButton);

            return this;
        }
        public PersonMASHEpisodeRecordPage ClickBackButton()
        {
            WaitForElement(backButton);
            Click(backButton);

            return this;
        }

        public PersonMASHEpisodeRecordPage NavigateToMASHEpisodeFormsPage()
        {
            WaitForElement(leftMenuButton);
            Click(leftMenuButton);

            WaitForElementToBeClickable(MASHEpisodeFormLeftMenuButton);
            Click(MASHEpisodeFormLeftMenuButton);

            return this;
        }

    }
}
