using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class OpenEndedAbsencesPopUp : CommonMethods
    {
        public OpenEndedAbsencesPopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By popupHeader = By.XPath("//*[@id='OpenEndedAbsenceFutureBookings_dialogHeader']/h1");


        readonly By ReallocateTextbox = By.XPath("//select[@id='OpenEndedAbsenceFutureBookings_sReallocate']");
        readonly By searchButton = By.Id("CWQuickSearchButton");
        readonly By refreshButton = By.Id("CWRefreshButton");
        readonly By okButton = By.Id("OpenEndedAbsenceFutureBookings_btnOk");
        readonly By cancelButton = By.Id("OpenEndedAbsenceFutureBookings_btnCancel");
        readonly By lookIn_Field = By.Id("CWViewSelector");
        readonly By addSelectedButton = By.Id("CWAddSelectedButton");


        readonly By gridResultsArea = By.XPath("//table[@class='grid-holder']");
        readonly By AlertText = By.XPath("//*[@id='OpenEndedAbsenceFutureBookings_dialogHeader']//div");
        By recordRow(string recordID) => By.XPath("//tr[@id='" + recordID + "']");

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=openendedabsence&')]");

        By checkboxElement(string ElementID) => By.XPath("//tr[@id='" + ElementID + "']/td[1]/input");

        By recordCheckbox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By nameElement(string ElementName) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[2][text()='" + ElementName + "']");

        By CheckboxElementToSelect(string SearchString) => By.XPath("//tr/td[@title ='" + SearchString + "'][contains(@id, '_Primary')]/preceding-sibling::td/input");

        By gridPageHeaderElement(int HeaderCellPosition, string text) => By.XPath("//*[@class='grid-header-wrapper']//table[@class='grid']/thead/tr/th[" + HeaderCellPosition +"]/span[text()='" + text + "']");
        By gridPageCellElement(int rowposition,string recordid,int CellPosition) => By.XPath("//tr["+ rowposition + "][@id='" + recordid + "']/td["+ CellPosition +"]");
        By gridHeaderChkBox(int CellPosition) => By.XPath("//*[@class='grid-header-wrapper']//table[@class='grid']/thead/tr/th[" + CellPosition +"]/input[@type='checkbox']");

        public OpenEndedAbsencesPopUp WaitForOpenEndedAbsencesPopUpToLoad()
        {
            

            WaitForElement(popupHeader);
            WaitForElement(ReallocateTextbox);
            WaitForElement(okButton);
            WaitForElement(cancelButton);

            return this;
        }
        public OpenEndedAbsencesPopUp SelectReallocationOptionsByText(string TextToSelect)
        {
            SelectPicklistElementByText(ReallocateTextbox, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }

        public OpenEndedAbsencesPopUp SelectReallocationOptionsByValue(string Value)
        {
            SwitchToDefaultFrame();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            SelectPicklistElementByValue(ReallocateTextbox, Value);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            return this;
        }

        public OpenEndedAbsencesPopUp SelectResultElement(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);
            WaitForElementToBeClickable(checkboxElement(ElementID));
            Click(checkboxElement(ElementID));
            Click(okButton);
            driver.SwitchTo().ParentFrame();
            return this;
        }

        public OpenEndedAbsencesPopUp ValidateHeaderText(string ExpectedText)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            ValidateElementText(AlertText, ExpectedText);
            return this;
        }

      
        public OpenEndedAbsencesPopUp ValidateResultElementPresent(Guid RecordID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementVisible(recordRow(RecordID.ToString()));
            return this;
        }

        public OpenEndedAbsencesPopUp ValidateResultElementNotPresent(string ElementID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementNotVisible(checkboxElement(ElementID), 7);

            return this;
        }

        public OpenEndedAbsencesPopUp ClickCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);
            return this;

        }

      
        public OpenEndedAbsencesPopUp ClickOkBtn()
        {
            WaitForElementToBeClickable(okButton);
            Click(okButton);
            driver.SwitchTo().ParentFrame();

            return this;
        }


        public OpenEndedAbsencesPopUp OEAbsencesRecordPageValidateHeaderCellText(int CellPosition,string ExpectedText)
        {
            MoveToElementInPage(gridPageHeaderElement(CellPosition, ExpectedText));
            WaitForElementVisible(gridPageHeaderElement(CellPosition, ExpectedText));
            ValidateElementText(gridPageHeaderElement(CellPosition, ExpectedText),ExpectedText);

            return this;
        }

        public OpenEndedAbsencesPopUp OEAbsencesRecordPageValidateCellText(int rowposition,string recordid,int CellPosition, string ExpectedText)
        {
            MoveToElementInPage(gridPageCellElement(rowposition,recordid, CellPosition));
            WaitForElementVisible(gridPageCellElement(rowposition,recordid, CellPosition));
            ValidateElementText(gridPageCellElement(rowposition,recordid, CellPosition), ExpectedText);

            return this;
        }

        public OpenEndedAbsencesPopUp ValidateOkButtonVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(okButton);
            }
            else
            {
                WaitForElementNotVisible(okButton, 5);
            }
            return this;
        }

        public OpenEndedAbsencesPopUp ValidateCancelButtonVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(cancelButton);
            }
            else
            {
                WaitForElementNotVisible(cancelButton, 5);
            }
            return this;
        }

        public OpenEndedAbsencesPopUp SelectBookingsToReallocate(int cellposition)
        {
            MoveToElementInPage(gridHeaderChkBox(cellposition));
            WaitForElementToBeClickable(gridHeaderChkBox(cellposition));
            Click(gridHeaderChkBox(cellposition));
            return this;
        }
    }

}

