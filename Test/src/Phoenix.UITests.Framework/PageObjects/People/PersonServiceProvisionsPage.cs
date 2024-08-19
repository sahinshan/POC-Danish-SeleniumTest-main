using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonServiceProvisionsPage : CommonMethods
    {

        public PersonServiceProvisionsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        
        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By BrokerageIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=brokerageepisode&')]");
        readonly By CWNavItem_ServiceProvisionFrame = By.Id("CWUrlPanel_IFrame");
        readonly By CWNavItem_ServiceProvisionsFrame = By.Id("CWUrlPanel_IFrame");

        readonly By newButton = By.Id("TI_NewRecordButton");
        readonly By pageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Service Provisions']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By refreshButton = By.Id("CWRefreshButton");

        By ToolbarMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        By CancelButton = By.XPath("//*[@id='TI_Cancel']");
        By readyToAuthoriseButton = By.XPath("//*[@id='TI_ReadyToAuthorise']");
        By authoriseButton = By.XPath("//*[@id='TI_Authorise']");
        By UpdateGLCodeButton = By.XPath("//*[@id='TI_UpdateGLCode']");
        By DeleteButton = By.XPath("//*[@id='TI_DeleteRecordButton']");

        readonly By ServiceElement1HeaderElementSortOrded = By.XPath("//*[@id='CWGridHeaderRow']/th[@field='serviceelement1id_cwname']/a/span[2]");
        readonly By ServiceElement2HeaderElementSortOrded = By.XPath("//*[@id='CWGridHeaderRow']/th[@field='serviceelement2id_cwname']/a/span[2]");
        readonly By CareTypeHeaderElementSortOrded = By.XPath("//*[@id='CWGridHeaderRow']/th[@field='caretypeid_cwname']/a/span[2]");

        readonly By ServiceElement1HeaderCell = By.XPath("//*[@id='CWGridHeaderRow']/th[@field='serviceelement1id_cwname']/a");
        readonly By ServiceElement2HeaderCell = By.XPath("//*[@id='CWGridHeaderRow']/th[@field='serviceelement2id_cwname']/a");
        readonly By CareTypeHeaderCell = By.XPath("//*[@id='CWGridHeaderRow']/th[@field='caretypeid_cwname']/a");


        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");
        By RecordIdentifierCheckbox(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[1]");

        By recordRowCell(int RowPosition, int Cellposition) => By.XPath("//table[@id='CWGrid']/tbody/tr[" + RowPosition + "]/td[" + Cellposition + "]");



        public PersonServiceProvisionsPage WaitForPersonServiceProvisionsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWNavItem_ServiceProvisionFrame);
            SwitchToIframe(CWNavItem_ServiceProvisionFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);

            WaitForElement(newButton);
            WaitForElement(pageHeader);

            ValidateElementTextContainsText(pageHeader, "Service Provisions");

            return this;
        }

        public PersonServiceProvisionsPage WaitForBrokerageServiceProvisionsPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(BrokerageIFrame);
            SwitchToIframe(BrokerageIFrame);
           
            WaitForElement(CWNavItem_ServiceProvisionsFrame);
            SwitchToIframe(CWNavItem_ServiceProvisionsFrame);

            WaitForElementNotVisible("CWRefreshPanel", 50);

            return this;
        }

        public PersonServiceProvisionsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public PersonServiceProvisionsPage ValidateRecordIsPresent(string RecordID, bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementToBeClickable(RecordIdentifier(RecordID));
                MoveToElementInPage(RecordIdentifier(RecordID));
            }
            else
            {
                WaitForElementNotVisible(RecordIdentifier(RecordID), 3);
            }
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(RecordIdentifier(RecordID)));
            return this;

        }
        public PersonServiceProvisionsPage OpenRecord(string RecordID)
        {
            MoveToElementInPage(RecordIdentifier(RecordID));
            WaitForElementToBeClickable(RecordIdentifier(RecordID));

            Click(RecordIdentifier(RecordID));

            return this;
        }

        public PersonServiceProvisionsPage SelectRecord(string RecordID)
        {
            WaitForElementToBeClickable(RecordIdentifierCheckbox(RecordID));
            MoveToElementInPage(RecordIdentifierCheckbox(RecordID));
            Click(RecordIdentifierCheckbox(RecordID));

            return this;
        }

        public PersonServiceProvisionsPage TapCancelButton()
        {
            WaitForElementToBeClickable(ToolbarMenuButton);
            MoveToElementInPage(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            WaitForElementToBeClickable(CancelButton);
            MoveToElementInPage(CancelButton);
            Click(CancelButton);

            return this;
        }

        public PersonServiceProvisionsPage TapNewButton()
        {
            MoveToElementInPage(newButton);
            WaitForElementToBeClickable(newButton);
            Click(newButton);

            return this;
        }

        public PersonServiceProvisionsPage TapRefreshButton()
        {
            MoveToElementInPage(refreshButton);
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public PersonServiceProvisionsPage TapReadyToAuthoriseButton()
        {
            WaitForElement(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            WaitForElement(readyToAuthoriseButton);
            Click(readyToAuthoriseButton);

            return this;
        }

        public PersonServiceProvisionsPage TapAuthoriseButton()
        {
            WaitForElement(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            WaitForElement(authoriseButton);
            Click(authoriseButton);

            return this;
        }

        public PersonServiceProvisionsPage ClickUpdateGLCodeButton()
        {
            WaitForElementToBeClickable(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            WaitForElementToBeClickable(UpdateGLCodeButton);
            Click(UpdateGLCodeButton);

            return this;
        }

        public PersonServiceProvisionsPage ClickDeleteButton()
        {
            WaitForElementToBeClickable(ToolbarMenuButton);
            Click(ToolbarMenuButton);

            WaitForElementToBeClickable(DeleteButton);
            Click(DeleteButton);

            return this;
        }

        public PersonServiceProvisionsPage ValidateNewRecordButtonVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(newButton);
            }
            else
            {
                WaitForElementNotVisible(newButton, 5);
            }
            return this;
        }

        public PersonServiceProvisionsPage ValidateRecordCellContent(int RowPosition, int Cellposition, string ExpectedText)
        {
            ScrollToElement(recordRowCell(RowPosition, Cellposition));
            WaitForElementToBeClickable(recordRowCell(RowPosition, Cellposition));
            MoveToElementInPage(recordRowCell(RowPosition, Cellposition));
            ValidateElementText(recordRowCell(RowPosition, Cellposition), ExpectedText);

            return this;
        }

        public PersonServiceProvisionsPage ValidateServiceElement1HeaderCellSortOrdedAscending()
        {
            ScrollToElement(ServiceElement1HeaderCell);
            WaitForElementVisible(ServiceElement1HeaderElementSortOrded);
            ValidateElementAttribute(ServiceElement1HeaderElementSortOrded, "class", "sortasc");

            return this;
        }

        public PersonServiceProvisionsPage ValidateServiceElement2HeaderCellSortOrdedAscending()
        {
            ScrollToElementViaJavascript(ServiceElement2HeaderCell);
            ValidateElementAttribute(ServiceElement2HeaderElementSortOrded, "class", "sortasc");

            return this;
        }

        public PersonServiceProvisionsPage ValidateCareTypeHeaderCellSortOrdedAscending()
        {
            ScrollToElementViaJavascript(CareTypeHeaderCell);
            ValidateElementAttribute(CareTypeHeaderElementSortOrded, "class", "sortasc");

            return this;
        }

        public PersonServiceProvisionsPage ClickServiceElement1HeaderCellToSortRecord()
        {
            WaitForElementToBeClickable(ServiceElement1HeaderCell);
            ScrollToElement(ServiceElement1HeaderCell);
            Click(ServiceElement1HeaderCell);

            return this;
        }

        public PersonServiceProvisionsPage ClickServiceElement2HeaderCellToSortRecord()
        {
            WaitForElementToBeClickable(ServiceElement2HeaderCell);
            ScrollToElement(ServiceElement2HeaderCell);
            Click(ServiceElement2HeaderCell);

            return this;
        }

        public PersonServiceProvisionsPage ClickCareTypeHeaderCellToSortRecord()
        {
            WaitForElementToBeClickable(CareTypeHeaderCell);
            ScrollToElement(CareTypeHeaderCell);
            Click(CareTypeHeaderCell);

            return this;
        }

    }
}
