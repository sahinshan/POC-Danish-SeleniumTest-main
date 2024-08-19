using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonContractServiceChargesPerWeekRecordPage : CommonMethods
    {
        public PersonContractServiceChargesPerWeekRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By CareProviderPersonContractService_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontractservice&')]");
        readonly By UrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1[text()='Person Contract Service Charges Per Week']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        readonly By ViewSelector = By.Id("CWViewSelector");
        readonly By SearchField = By.Id("CWQuickSearch");
        readonly By SearchButton = By.Id("CWQuickSearchButton");
        readonly By RefreshButton = By.Id("CWRefreshButton");

        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By CalculateChargesPerWeekButton = By.Id("TI_CalculateButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");

        readonly By GridHeaderIdField = By.XPath("//*[@id='CWGridHeaderRow']//a[@title='Sort by Id']");


        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordPosition(int RecordPosition, string RecordID) => By.XPath("//table/tbody/tr[" + RecordPosition + "][@id='" + RecordID + "']");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        By pageHeaderElement(int HeaderCellPosition) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/*");

        By gridPageHeaderElementSortAscendingOrder(int HeaderCellPosition, string FieldName) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + HeaderCellPosition + "]/a[@title = 'Sort by " + FieldName + "']/span[2][@class = 'sortdesc']");
        readonly By SaveButton = By.XPath("//*[@id='TI_SaveButton']");
        readonly By SaveAndCloseButton = By.XPath("//*[@id='TI_SaveAndCloseButton']");
        readonly By DeleteButton = By.XPath("//button[@title = 'Delete']");
        readonly By activateButton = By.Id("TI_ActivateButton");
        readonly By deactivateButton = By.Id("TI_DeactivateButton");
        readonly By CareProviderPersonContractServiceChargePerWeek_IFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderpersoncontractservicechargeperwk&')]");
        readonly By ChargePerWeek_LabelField = By.XPath("//*[@id='CWLabelHolder_chargeperweek']/label[text()='Charge Per Week']");
        readonly By ChargePerWeek_MandatoryField = By.XPath("//*[@id='CWLabelHolder_chargeperweek']/label/span[@class='mandatory']");
        readonly By ChargePerWeek_Field = By.Id("CWField_chargeperweek");
        readonly By CareProviderPersonContractServiceId_Link = By.Id("CWField_careproviderpersoncontractserviceid_Link");
        readonly By ResponsibleTeam_LinkText = By.Id("CWField_ownerid_Link");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By PersonContractService = By.XPath("//*[@id='CWField_careproviderpersoncontractserviceid_Link']");
        readonly By PersonContractServiceLookup = By.XPath("//*[@id='CWLookupBtn_careproviderpersoncontractserviceid']");

        public PersonContractServiceChargesPerWeekRecordPage ValidateChargePerWeekFieldHavingTextAndDisabled(string ExpectedText)
        {
            MoveToElementInPage(ChargePerWeek_Field);
            ValidateElementValue(ChargePerWeek_Field, ExpectedText);
            ValidateElementDisabled(ChargePerWeek_Field);
            return this;
        }
        public PersonContractServiceChargesPerWeekRecordPage ValidatePersonContractServiceLinkHavingTextAndDisabled(string ExpectedText)
        {
            WaitForElementToBeClickable(ResponsibleTeam_LinkText);
            MoveToElementInPage(ResponsibleTeam_LinkText);
            ValidateElementText(ResponsibleTeam_LinkText, ExpectedText);
            ValidateElementDisabled(ResponsibleTeam_LookupButton);
            return this;
        }

        public PersonContractServiceChargesPerWeekRecordPage WaitForPersonContractServiceChargesPerWeekDetailsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(CareProviderPersonContractServiceChargePerWeek_IFrame);
            SwitchToIframe(CareProviderPersonContractServiceChargePerWeek_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 60);

            WaitForElementNotVisible(ExportToExcelButton,5);
            WaitForElementNotVisible(CalculateChargesPerWeekButton,5);
            WaitForElementVisible(AssignRecordButton);
            WaitForElementNotVisible(SaveButton,5);
            WaitForElementNotVisible(SaveAndCloseButton,5);
            WaitForElementNotVisible(DeleteButton, 5);
            WaitForElementNotVisible(activateButton, 5);
            WaitForElementNotVisible(deactivateButton, 5);

            return this;
        }

        public PersonContractServiceChargesPerWeekRecordPage ValidatePersonContractServiceHavingTextAndDisabled(string ExpectedText)
        {
            WaitForElementVisible(PersonContractService);
            ValidateElementText(PersonContractService, ExpectedText);
            ValidateElementDisabled(PersonContractServiceLookup);
                                    
            return this;
        }
     
    }
}