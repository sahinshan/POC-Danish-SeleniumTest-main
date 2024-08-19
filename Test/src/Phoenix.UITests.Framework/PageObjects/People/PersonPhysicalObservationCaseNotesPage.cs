using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonPhysicalObservationCaseNotesPage : CommonMethods
    {
        public PersonPhysicalObservationCaseNotesPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personphysicalobservation&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWNavItem_PersonCaseNoteFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[contains(text(),'Person Physical Observation Case Notes')]");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        readonly By AssignButton = By.Id("TI_AssignRecordButton");


        readonly By description_TextAreaField = By.XPath("//*[@id='CWField_notes']");
        readonly string description_TextAreaFieldName = "CWField_notes";
        By description_Field(string LineNumber) => By.XPath("/html/body/p[" + LineNumber + "]");

        By recordsAreaHeaderCell(int CellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + CellPosition + "]/a");

        By recordFullRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By recordRowCheckbox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        readonly By reason_RemoveButton = By.Id("CWClearLookup_activityreasonid");



        public PersonPhysicalObservationCaseNotesPage WaitForPersonPhysicalObservationCaseNotesPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_PersonCaseNoteFrame);
            SwitchToIframe(CWNavItem_PersonCaseNoteFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            

            return this;
        }

        public PersonPhysicalObservationCaseNotesPage SearchPersonPhysicalObservationCaseNoteRecord(string SearchQuery, string PersonID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);


            return this;
        }

        public PersonPhysicalObservationCaseNotesPage SearchPersonPhysicalObservationCaseNoteRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public PersonPhysicalObservationCaseNotesPage OpenPersonPhysicalObservationCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }

        public PersonPhysicalObservationCaseNotesPage SelectPersonPhysicalObservationCaseNoteRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }


        public PersonPhysicalObservationCaseNotesPage WaitForBulkEditButtonVisible()
        {
            WaitForElementVisible(BulkEditButton);

            return this;
        }

        public PersonPhysicalObservationCaseNotesPage ClickBulkEditButton()
        {
            WaitForElementToBeClickable(BulkEditButton);
            Click(BulkEditButton);

            return this;
        }
        public PersonPhysicalObservationCaseNotesPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;

        }

        public PersonPhysicalObservationCaseNotesPage ValidateNoRecordLabelVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordLabel);
            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
            }
            return this;
        }

        public PersonPhysicalObservationCaseNotesPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }

        public PersonPhysicalObservationCaseNotesPage ValidateRecordVisible(string RecordID)
        {
            WaitForElementVisible(recordRow(RecordID));

            return this;
        }
        public PersonPhysicalObservationCaseNotesPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElement(recordCell(RecordID, CellPosition));
            ScrollToElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
        public PersonPhysicalObservationCaseNotesPage ClickDeletedButton()
        {
            WaitForElementToBeClickable(DeleteRecordButton);
            Click(DeleteRecordButton);

            return this;
        }

        public PersonPhysicalObservationCaseNotesPage ValidateRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(recordRow(RecordID), 3);

            return this;
        }

        public PersonPhysicalObservationCaseNotesPage InsertDescription(string TextToInsert)
        {
            System.Threading.Thread.Sleep(1000);
            SetElementDisplayStyleToInline(description_TextAreaFieldName);
            SetElementVisibilityStyleToVisible(description_TextAreaFieldName);

            WaitForElementVisible(description_TextAreaField);
            SendKeys(description_TextAreaField, TextToInsert);

            return this;
        }

        public PersonPhysicalObservationCaseNotesPage ClickReasonRemoveButton()
        {
            WaitForElementToBeClickable(reason_RemoveButton);
            Click(reason_RemoveButton);

            return this;
        }

        public PersonPhysicalObservationCaseNotesPage ClickAssignButton()
        {
            WaitForElementToBeClickable(AssignButton);
            Click(AssignButton);

            return this;
        }

        public PersonPhysicalObservationCaseNotesPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(ExportToExcelButton);
            Click(ExportToExcelButton);

            return this;
        }

    }
}
