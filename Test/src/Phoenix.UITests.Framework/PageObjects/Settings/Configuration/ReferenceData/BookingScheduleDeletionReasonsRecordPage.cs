using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BookingScheduleDeletionReasonsRecordPage : CommonMethods
    {
        public BookingScheduleDeletionReasonsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_cpbookingscheduledeletionreason = By.Id("iframe_cpbookingscheduledeletionreason");
        readonly By cwDialog_cpbookingscheduledeletionreasonFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbookingscheduledeletionreason')]");

        readonly By mccIframe = By.Id("mcc-iframe");
        readonly By popupHeader = By.XPath("//*[@class='mcc-drawer__header']//h2");
        readonly By closeIcon = By.XPath("//button[@class='mcc-button mcc-button--sm mcc-button--ghost mcc-button--icon-only btn-close-drawer']");

        #region option toolbar

        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");
        readonly By backButton = By.XPath("//div[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By closeButton = By.Id("CWCloseDrawerButton");

        #endregion

        readonly By BookingScheduleDeletionReasonsRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By Name_Field = By.Id("CWField_name");

        public BookingScheduleDeletionReasonsRecordPage WaitForBookingScheduleDeletionReasonsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_cpbookingscheduledeletionreason);
            SwitchToIframe(iframe_cpbookingscheduledeletionreason);

            WaitForElement(cwDialog_cpbookingscheduledeletionreasonFrame);
            SwitchToIframe(cwDialog_cpbookingscheduledeletionreasonFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElementVisible(BookingScheduleDeletionReasonsRecordPageHeader);
            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);

            return this;
        }

        public BookingScheduleDeletionReasonsRecordPage WaitForBookingScheduleDeletionReasonsRecordPageToLoadInDrawerMode()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(mccIframe);
            SwitchToIframe(mccIframe);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElementVisible(popupHeader);
            WaitForElementVisible(closeIcon);
            WaitForElementVisible(closeButton);
            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);

            return this;
        }

        public BookingScheduleDeletionReasonsRecordPage ValidateNameFieldValue(string ExpectedValue)
        {
            WaitForElementToBeClickable(Name_Field);
            MoveToElementInPage(Name_Field);
            ValidateElementValue(Name_Field, ExpectedValue);

            return this;
        }

    }
}