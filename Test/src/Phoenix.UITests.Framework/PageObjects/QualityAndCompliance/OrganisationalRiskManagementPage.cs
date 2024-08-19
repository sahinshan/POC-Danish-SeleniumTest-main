using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class OrganisationalRiskManagementPage : CommonMethods
    {
        public OrganisationalRiskManagementPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By ContentIFrame = By.Id("CWContentIFrame");
        readonly By OrganisationalRisksPageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='Organisational Risks']");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");

        readonly By viewsPicklist = By.Id("CWViewSelector");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");
        readonly By refreshButton = By.Id("CWRefreshButton");

        //readonly By assignDialogIFrame = By.Id("iframe_CWAssignDialog");
        //readonly By popupHeader = By.Id("CWHeaderText");

        By tableHeaderRow(int position) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + position + "]");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        By RiskRow(string RiskID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RiskID + "']/td[2]");
        By RiskRowCheckBox(string RiskID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RiskID + "']/td[1]/input");


        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");




        public OrganisationalRiskManagementPage WaitForOrganisationalRisksPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(ContentIFrame);
            SwitchToIframe(ContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(OrganisationalRisksPageHeader);

            WaitForElement(NewRecordButton);

            return this;
        }


        public OrganisationalRiskManagementPage WaitForOrganisationalRiskManagementPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(ContentIFrame);
            this.SwitchToIframe(ContentIFrame);

            this.WaitForElement(iframe_CWDataFormDialog);
            this.SwitchToIframe(iframe_CWDataFormDialog);


            return this;
        }
        public OrganisationalRiskManagementPage SearchRiskRecord(string SearchQuery, string RiskID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(RiskRow(RiskID));

            return this;
        }


        public OrganisationalRiskManagementPage ValidateRecordCellText(int Position, string ExpectedText)
        {
            ScrollToElement(tableHeaderRow(Position));
            ValidateElementText(tableHeaderRow(Position), ExpectedText);

            return this;
        }

        public OrganisationalRiskManagementPage ClickTableHeaderCell(int Position)
        {
            WaitForElementToBeClickable(tableHeaderRow(Position));;
            Click(tableHeaderRow(Position));;

            return this;
        }



        public OrganisationalRiskManagementPage TypeSearchQuery(string Query)
        {
            WaitForElement(quickSearchButton);
            SendKeys(quickSearchTextBox, Query);
            Click(quickSearchButton);
            return this;
        }
        public OrganisationalRiskManagementPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }



        public OrganisationalRiskManagementPage SearchRiskRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public OrganisationalRiskManagementPage OpenRiskRecordUsingID(string RiskId)
        {
            WaitForElement(RiskRow(RiskId));
            driver.FindElement(RiskRow(RiskId)).Click();



            return this;
        }
        public OrganisationalRiskManagementPage OpenRiskRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

       

        public OrganisationalRiskManagementPage SelectOrganisationalRiskRecord(string RiskId)
        {
            WaitForElement(RiskRowCheckBox(RiskId));
            Click(RiskRowCheckBox(RiskId));

            return this;
        }

        public OrganisationalRiskManagementPage OpenOrganisationalRiskRecord(string RecordId)
        {
            WaitForElement(RiskRow(RecordId));
            WaitForElementToBeClickable(RiskRow(RecordId));
            Click(RiskRow(RecordId));

            return this;
        }

        public OrganisationalRiskManagementPage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public OrganisationalRiskManagementPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }


        public OrganisationalRiskManagementPage ClickExportToExcelButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public OrganisationalRiskManagementPage ClickDeleteButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }


        public OrganisationalRiskManagementPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public OrganisationalRiskManagementPage ValidateRecordPresent(string RecordId)
        {
            WaitForElementVisible(recordRowCheckBox(RecordId));

            return this;
        }

        public OrganisationalRiskManagementPage ValidateRecordNotPresent(string RecordId)
        {
            WaitForElementNotVisible(recordRowCheckBox(RecordId), 7);

            return this;
        }

    }
}
