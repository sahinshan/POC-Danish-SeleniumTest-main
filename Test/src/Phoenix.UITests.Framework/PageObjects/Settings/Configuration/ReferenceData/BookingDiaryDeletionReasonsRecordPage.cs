using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class BookingDiaryDeletionReasonsRecordPage : CommonMethods
    {
        public BookingDiaryDeletionReasonsRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_cpbookingdiarydeletionreason = By.Id("iframe_cpbookingdiarydeletionreason");
        readonly By cwDialog_cpbookingdiarydeletionreasonFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cpbookingdiarydeletionreason')]");

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

        readonly By BookingDiaryDeletionReasonsRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By Name_Field = By.Id("CWField_name");

        public BookingDiaryDeletionReasonsRecordPage WaitForBookingDiaryDeletionReasonsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_cpbookingdiarydeletionreason);
            SwitchToIframe(iframe_cpbookingdiarydeletionreason);

            WaitForElement(cwDialog_cpbookingdiarydeletionreasonFrame);
            SwitchToIframe(cwDialog_cpbookingdiarydeletionreasonFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElementVisible(BookingDiaryDeletionReasonsRecordPageHeader);
            WaitForElementVisible(saveButton);
            WaitForElementVisible(saveAndCloseButton);

            return this;
        }

        public BookingDiaryDeletionReasonsRecordPage WaitForBookingDiaryDeletionReasonsRecordPageToLoadInDrawerMode()
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

        public BookingDiaryDeletionReasonsRecordPage ValidateNameFieldValue(string ExpectedValue)
        {
            WaitForElementToBeClickable(Name_Field);
            ScrollToElement(Name_Field);
            ValidateElementValue(Name_Field, ExpectedValue);

            return this;
        }

        public BookingDiaryDeletionReasonsRecordPage ClickCloseButton()
        {
            WaitForElementToBeClickable(closeButton);
            Click(closeButton);

            return this;
        }

    }
}