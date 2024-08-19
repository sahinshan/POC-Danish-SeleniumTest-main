using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.QualityAndCompliance
{
    public class ActionPlansPage : CommonMethods
    {
        public ActionPlansPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Action Plans Page Options Toolbar
        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By OrgRiskRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=organisationalrisk&')]");
        readonly By iframe_CWDataFormDialog = By.Id("iframe_CWDataFormDialog");


        readonly By CWRelatedRecordsIFrame = By.Id("CWUrlPanel_IFrame");
        readonly By ActionPlansPageHeader = By.XPath("//h1[text() ='Organisational Risk Action Plans']");
        readonly By AddButton = By.Id("TI_NewRecordButton");
        readonly By refreshButton = By.Id("CWRefreshButton");

        readonly By DetailsTab = By.XPath("//li[@id = 'CWNavGroup_EditForm']");
        readonly By noRecordsFoundMessage = By.XPath("//h2[text() = 'NO RECORDS']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");

        #endregion

        #region Action Plans Search Grid Header
        readonly By viewsPicklist = By.Id("CWViewSelector");

        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");

        By ActionPlanRow(string RiskID) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[@title = '" + RiskID + "']");
        By ActionPlanRowCheckBox(string RiskID) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[@title = '" + RiskID + "']/preceding-sibling::td/input[contains(@id, 'CHK_')]");
        By RiskRowCheckBox(string RiskID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RiskID + "']/td[1]/input");
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        #endregion


        #region Action Plans Page

        public ActionPlansPage WaitForActionPlansPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(OrgRiskRecordIFrame);
            SwitchToIframe(OrgRiskRecordIFrame);

            WaitForElement(CWRelatedRecordsIFrame);
            SwitchToIframe(CWRelatedRecordsIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 100);
            WaitForElement(ActionPlansPageHeader);

            WaitForElement(AddButton);

            return this;
        }

        public ActionPlansPage WaitForOrganisationalRiskActionPlanPageToLoadFromAdvancedSearch()
        {
            SwitchToDefaultFrame();

            this.WaitForElement(CWContentIFrame);
            this.SwitchToIframe(CWContentIFrame);

            this.WaitForElement(iframe_CWDataFormDialog);
            this.SwitchToIframe(iframe_CWDataFormDialog);


            return this;
        }


        public ActionPlansPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public ActionPlansPage VerifyNoRecordsFoundsMessage()
        {
            WaitForElementVisible(noRecordsFoundMessage);
            ScrollToElement(noRecordsFoundMessage);
            Assert.IsTrue(GetElementVisibility(noRecordsFoundMessage));

            return this;
        }

        public ActionPlansPage TypeSearchQuery(string Query)
        {
            SendKeys(quickSearchTextBox, Query);
            Click(quickSearchButton);
            return this;
        }
        public ActionPlansPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public ActionPlansPage ValidateRecordVisible(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));

            return this;
        }
        public ActionPlansPage ViewDetailsTab()
        {
            SwitchToDefaultFrame();
            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);
            WaitForElement(OrgRiskRecordIFrame);
            SwitchToIframe(OrgRiskRecordIFrame);
            WaitForElement(DetailsTab);
            WaitForElementToBeClickable(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public ActionPlansPage SearchActionPlanRecord(string SearchQuery, string ActionPlanID)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(ActionPlanRow(ActionPlanID));

            return this;
        }


        public ActionPlansPage SearchActionPlanRecord(string SearchQuery)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, SearchQuery);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        public ActionPlansPage OpenActionPlanRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public ActionPlansPage SelectOrganisationalRiskActionPlanRecord(string RiskId)
        {
            WaitForElement(RiskRowCheckBox(RiskId));
            Click(RiskRowCheckBox(RiskId));

            return this;
        }

        public ActionPlansPage ClickExportToExcelButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }


        public ActionPlansPage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(AddButton);
            Click(AddButton);

            return this;
        }

        public ActionPlansPage SearchFields(string TextToInsert)
        {
            WaitForElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, TextToInsert);
            Click(quickSearchButton);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }
        public ActionPlansPage SelectViewSelector(string TextToInsert)
        {
            WaitForElement(viewsPicklist);
            SelectPicklistElementByText(viewsPicklist, TextToInsert);
            
            WaitForElementNotVisible("CWRefreshPanel", 80);

            return this;
        }

        #endregion
    }
}
