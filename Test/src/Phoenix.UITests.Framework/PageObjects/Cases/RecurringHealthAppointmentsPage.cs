using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Phoenix.UITests.Framework.PageObjects.Cases
{
    public class RecurringHealthAppointmentsPage : CommonMethods
    {
        public RecurringHealthAppointmentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By caseIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]");
        readonly By CWNavItem_RecurringHealthAppointmentFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Recurring Health Appointments']");


        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By viewSelector = By.Id("CWViewSelector");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By RecurringHealthAppointmentRecordCell(string RecurringHealthAppointmentId, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecurringHealthAppointmentId + "']/td[" + CellPosition + "]");

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By DeleteButton = By.Id("TI_DeleteRecordButton");


        public RecurringHealthAppointmentsPage WaitForRecurringHealthAppointmentsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(caseIFrame);
            SwitchToIframe(caseIFrame);

            WaitForElement(CWNavItem_RecurringHealthAppointmentFrame);
            SwitchToIframe(CWNavItem_RecurringHealthAppointmentFrame);

            WaitForElement(pageHeader);

            return this;
        }

        public RecurringHealthAppointmentsPage SearchRecurringHealthAppointmentRecord(string SearchQuery, string RecurringHealthAppointmentID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(recordRow(RecurringHealthAppointmentID));

            return this;
        }

        public RecurringHealthAppointmentsPage SearchRecurringHealthAppointmentRecord(string SearchQuery)
        {
            WaitForElementToBeClickable(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public RecurringHealthAppointmentsPage OpenRecurringHealthAppointmentRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public RecurringHealthAppointmentsPage SelectRecurringHealthAppointmentRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

        public RecurringHealthAppointmentsPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(DeleteButton);
            MoveToElementInPage(DeleteButton);
            Click(DeleteButton);
            return this;
        }

        public RecurringHealthAppointmentsPage SelectViewSelector(string TextToInsert)
        {
            WaitForElement(viewSelector);
            SelectPicklistElementByText(viewSelector, TextToInsert);

            return this;
        }
        public RecurringHealthAppointmentsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(noRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(noRecordMessage, 5);
            }
            return this;
        }

        public RecurringHealthAppointmentsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(NewRecordButton);
            MoveToElementInPage(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public RecurringHealthAppointmentsPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            ScrollToElement(RecurringHealthAppointmentRecordCell(RecordID, CellPosition));
            ValidateElementText(RecurringHealthAppointmentRecordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }
    }
}
