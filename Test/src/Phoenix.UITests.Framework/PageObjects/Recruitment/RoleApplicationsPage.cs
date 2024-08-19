using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects.Recruitment
{
    public class RoleApplicationsPage : CommonMethods
    {
        public RoleApplicationsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Role Applications Page Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By RoleApplicationsFrame = By.Id("CWUrlPanel_IFrame");
        readonly By iframe_CWDialog_Applicant = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicant&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=applicant'
        readonly By iframe_CWDialog_systemUser = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=systemuser'
        readonly By RoleApplicationsPageHeader = By.XPath("//h1[text() ='Role Applications']");
        readonly By AddButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        readonly By DetailsTab = By.XPath("//li[@id = 'CWNavGroup_EditForm']");


        #endregion

        #region Role Applications Page Search Grid Header

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");

        #endregion

        #region Role Applications Page Data Grid
        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        readonly By noRecordsFoundMessage = By.XPath("//h2[text() = 'NO RECORDS']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");

        By recruitmentStatusCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[6]");
        By roleRecordCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[3]");
        By targetTeamRecordCell(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[4]");

        #endregion

        #region Role Applications Page

        public RoleApplicationsPage WaitForRoleApplicationsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_Applicant);
            SwitchToIframe(iframe_CWDialog_Applicant);

            WaitForElement(RoleApplicationsFrame);
            SwitchToIframe(RoleApplicationsFrame);

            WaitForElementNotVisible("CWRefreshPanel", 80);

            WaitForElement(RoleApplicationsPageHeader);

            WaitForElement(viewsPicklist);

            WaitForElement(AddButton);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);
            WaitForElement(exportToExcelButton);            

            return this;
        }

        public RoleApplicationsPage WaitForSystemUserRoleApplicationsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_systemUser);
            SwitchToIframe(iframe_CWDialog_systemUser);

            WaitForElement(RoleApplicationsFrame);
            SwitchToIframe(RoleApplicationsFrame);

            WaitForElementNotVisible("CWRefreshPanel", 20);

            WaitForElement(RoleApplicationsPageHeader);

            WaitForElement(viewsPicklist);

            WaitForElement(AddButton);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);
            WaitForElement(exportToExcelButton);            

            return this;
        }

        public RoleApplicationsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(AddButton);
            ScrollToElement(AddButton);
            Click(AddButton);

            return this;
        }

        public RoleApplicationsPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            ScrollToElement(refreshButton);
            Click(refreshButton);

            return this;
        }

        public RoleApplicationsPage TypeSearchQuery(string Query)
        {
            WaitForElementVisible(quickSearchTextBox);
            ScrollToElement(quickSearchTextBox);
            SendKeys(quickSearchTextBox, Query);
            return this;
        }

        public RoleApplicationsPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            ScrollToElement(quickSearchButton);
            Click(quickSearchButton);
            return this;
        }

        public RoleApplicationsPage OpenRoleApplicationRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            ScrollToElement(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public RoleApplicationsPage OpenRoleApplicationRecord(Guid RecordId)
        {
            return OpenRoleApplicationRecord(RecordId.ToString());
        }

        public RoleApplicationsPage ViewDetailsTab()
        {
            SwitchToDefaultFrame();
            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);
            WaitForElement(DetailsTab);
            WaitForElementToBeClickable(DetailsTab);
            Click(DetailsTab);

            return this;
        }

        public RoleApplicationsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(noRecordsFoundMessage);

            }
            else
            {
                WaitForElementNotVisible(noRecordsFoundMessage, 5);
            }
            return this;
        }

        public RoleApplicationsPage ValidateRoleApplicationRecordIsPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));         
            ScrollToElement(recordRow(RecordId));

            bool isRecordPresent = GetElementVisibility(recordRow(RecordId));
            Assert.IsTrue(isRecordPresent);

            return this;
        }

        public RoleApplicationsPage ValidateRoleApplicationRecordIsNotPresent(string RecordId)
        {

            bool isRecordPresent = GetElementVisibility(recordRow(RecordId));
            Assert.IsFalse(isRecordPresent);

            return this;
        }

        public RoleApplicationsPage ValidateRecruitmentStatusCellText(string RecordID, string ExpectedText)
        {
            WaitForElementVisible(recruitmentStatusCell(RecordID));
            ScrollToElement(recruitmentStatusCell(RecordID));
            ValidateElementText(recruitmentStatusCell(RecordID), ExpectedText);
            return this;
        }

        public RoleApplicationsPage ValidateRoleCellText(string RecordID, string ExpectedText)
        {
            WaitForElementVisible(roleRecordCell(RecordID));
            ScrollToElement(roleRecordCell(RecordID));
            ValidateElementText(roleRecordCell(RecordID), ExpectedText);
            return this;
        }

        public RoleApplicationsPage ValidateTargetTeamCellText(string RecordID, string ExpectedText)
        {
            WaitForElementVisible(targetTeamRecordCell(RecordID));
            ScrollToElement(targetTeamRecordCell(RecordID));
            ValidateElementText(targetTeamRecordCell(RecordID), ExpectedText);
            return this;
        }

        #endregion
    }

}

