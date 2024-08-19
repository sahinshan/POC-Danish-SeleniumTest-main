using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReviewExistingBookingsPopUp : CommonMethods
    {
        public ReviewExistingBookingsPopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=cppersonabsence&')]");

        readonly By mcciframe = By.XPath("//*[@id='mcc-iframe']");

        readonly By popupHeader = By.XPath("//*[@id='CPPersonAbsenceRosteringExistingBookings_dialogHeader']/h1");
        readonly By okButton = By.Id("CPPersonAbsenceRosteringExistingBookings_btnOk");
        readonly By okButtonDisabled = By.XPath("//button[@id='CPPersonAbsenceRosteringExistingBookings_btnOk' and @class='btn btn-primary wx-100 mr-2 disabled']");
        readonly By cancelButton = By.Id("CPPersonAbsenceRosteringExistingBookings_btnCancel");
        readonly By gridResultsArea = By.XPath("//table[@class='grid-holder']");
        readonly By alertText = By.XPath("//*[@id='CPPersonAbsenceRosteringExistingBookings_dialogHeader']//div");
        readonly By alertWarningText = By.XPath("//*[@class='alert alert-warning atLeastOne']");
        readonly By CancellationOptionTextbox = By.XPath("//select[@id='CPPersonAbsenceRosteringExistingBookings_sReallocate']");

        By recordRow(string recordID) => By.XPath("//tr[@id='" + recordID + "']");
        By gridPageHeaderElement(int HeaderCellPosition, string text) => By.XPath("//*[@class='grid-header-wrapper']//table[@class='grid']/thead/tr/th[" + HeaderCellPosition + "]/span[text()='" + text + "']");

        By gridPageCellElement(int rowposition, string recordid, int CellPosition) => By.XPath("//tr[" + rowposition + "][@id='" + recordid + "']/td[" + CellPosition + "]");
        By CheckBoxElementDisabled(int rowposition, string recordid, int CellPosition) => By.XPath("//tr[" + rowposition + "][@id='" + recordid + "' and @class='']/td[" + CellPosition + "]");



        public ReviewExistingBookingsPopUp WaitForReviewExistingBookingsPopUpToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(popupHeader);
            WaitForElement(okButton);
            WaitForElement(cancelButton);

            return this;
        }

        public ReviewExistingBookingsPopUp WaitForPopUpToLoadInDrawerMode()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 7);

            WaitForElement(mcciframe);
            SwitchToIframe(mcciframe);

            WaitForElement(popupHeader);
            WaitForElement(okButton);
            WaitForElement(cancelButton);

            return this;
        }

        public ReviewExistingBookingsPopUp ValidateAlertText(string ExpectedText)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            ValidateElementText(alertText, ExpectedText);
            return this;
        }

        public ReviewExistingBookingsPopUp ValidateAlertWarningText(string ExpectedText)
        {
            WaitForElementVisible(alertWarningText);
            ValidateElementText(alertWarningText, ExpectedText);
            return this;
        }

        public ReviewExistingBookingsPopUp ReviewExistingBookingValidateHeaderCellText(int CellPosition, string ExpectedText)
        {
            MoveToElementInPage(gridPageHeaderElement(CellPosition, ExpectedText));
            WaitForElementVisible(gridPageHeaderElement(CellPosition, ExpectedText));
            ValidateElementText(gridPageHeaderElement(CellPosition, ExpectedText), ExpectedText);

            return this;
        }

        public ReviewExistingBookingsPopUp SelectCancellationOptionsByValue(string Value)
        {

            SelectPicklistElementByValue(CancellationOptionTextbox, Value);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }

        public ReviewExistingBookingsPopUp ValidateCancellationOptionsByValue(string OptionName)
        {
            WaitForElementVisible(CancellationOptionTextbox);
            ValidatePicklistContainsElementByValue(CancellationOptionTextbox, OptionName);


            return this;
        }
        public ReviewExistingBookingsPopUp ReviewExistingBookingsValidateCellText(int rowposition, string recordid, int CellPosition, string ExpectedText)
        {
            MoveToElementInPage(gridPageCellElement(rowposition, recordid, CellPosition));
            WaitForElementVisible(gridPageCellElement(rowposition, recordid, CellPosition));
            ValidateElementText(gridPageCellElement(rowposition, recordid, CellPosition), ExpectedText);

            return this;
        }

        public ReviewExistingBookingsPopUp ReviewExistingBookingsValidateCheckboxChecked(int rowposition, string recordid, int CellPosition)
        {
            MoveToElementInPage(gridPageCellElement(rowposition, recordid, CellPosition));
            WaitForElementVisible(gridPageCellElement(rowposition, recordid, CellPosition));
            ValidateElementChecked(gridPageCellElement(rowposition, recordid, CellPosition));

            return this;
        }


        public ReviewExistingBookingsPopUp ReviewExistingBookingsSelectCheckbox(int rowposition, string recordid, int CellPosition)
        {
            MoveToElementInPage(gridPageCellElement(rowposition, recordid, CellPosition));
            WaitForElementVisible(gridPageCellElement(rowposition, recordid, CellPosition));
            Click(gridPageCellElement(rowposition, recordid, CellPosition));

            return this;
        }

        public ReviewExistingBookingsPopUp ValidateReviewExistingBookingsCheckboxDisabled(bool ExpectDisabled, int rowposition, string recordid, int CellPosition)
        {
            if (ExpectDisabled)
                WaitForElementVisible(CheckBoxElementDisabled(rowposition, recordid, CellPosition));
            else
                WaitForElementNotVisible(CheckBoxElementDisabled(rowposition, recordid, CellPosition), 10);

            return this;
        }

        public ReviewExistingBookingsPopUp ValidateOkButtonDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
                WaitForElementVisible(okButtonDisabled);
            else
                WaitForElementNotVisible(okButton, 10);

            return this;
        }

        public ReviewExistingBookingsPopUp ClickOkButton()
        {

            WaitForElementToBeClickable(okButton);
            Click(okButton);

            return this;
        }

        public ReviewExistingBookingsPopUp ClickCancelButton()
        {

            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }

        public ReviewExistingBookingsPopUp ValidateResultElementPresent(Guid RecordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementVisible(recordRow(RecordID.ToString()));
            return this;
        }
    }
}

