using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Recruitment
{
    public class RecruitmentDocumentsManagementPage : CommonMethods
    {
        public RecruitmentDocumentsManagementPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Options Toolbar

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=compliance&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=applicant'
        readonly By RoleApplicationsFrame = By.Id("CWUrlPanel_IFrame");
        readonly By CWIFrame_ComplianceManagement = By.Id("CWIFrame_ComplianceManagement");

        readonly By RecruitmentDocumentsManagementPageHeader = By.XPath("//h1[text() ='Recruitment Documents Management']");
        readonly By AddButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        #endregion

        #region Search Grid Header

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");
        readonly By refreshButton = By.Id("CWRefreshButton");

        #endregion

        #region Data Grid

        readonly By noRecordsFoundMessage = By.XPath("//h2[text() = 'NO RECORDS']");
        readonly By noRecordMessage = By.XPath("//*[@id='CWGridHolder']/div/h2");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int cellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td["+ cellPosition + "]");
        By recordRowAndCell(int RowPosition, int cellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr["+ RowPosition + "]/td["+ cellPosition + "]");

        #endregion



        public RecruitmentDocumentsManagementPage WaitForRecruitmentDocumentsManagementPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(RoleApplicationsFrame);
            SwitchToIframe(RoleApplicationsFrame);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(RecruitmentDocumentsManagementPageHeader);

            WaitForElement(viewsPicklist);

            WaitForElement(AddButton);
            WaitForElement(quickSearchTextBox);
            WaitForElement(quickSearchButton);
            WaitForElement(refreshButton);
            WaitForElement(exportToExcelButton);            

            return this;
        }

        public RecruitmentDocumentsManagementPage WaitForRecruitmentDocumentsManagementAreaToLoadInsideRecruitmentDocument()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog_);
            SwitchToIframe(iframe_CWDialog_);

            WaitForElement(CWIFrame_ComplianceManagement);
            SwitchToIframe(CWIFrame_ComplianceManagement);

            WaitForElementNotVisible("CWRefreshPanel", 15);

            WaitForElement(RecruitmentDocumentsManagementPageHeader);

            WaitForElementToBeClickable(AddButton);
            WaitForElementToBeClickable(exportToExcelButton);

            return this;
        }


        public RecruitmentDocumentsManagementPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(AddButton);
            MoveToElementInPage(AddButton);
            Click(AddButton);

            return this;
        }

        public RecruitmentDocumentsManagementPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            MoveToElementInPage(refreshButton);
            Click(refreshButton);

            return this;
        }

        public RecruitmentDocumentsManagementPage TypeSearchQuery(string Query)
        {
            WaitForElementVisible(quickSearchTextBox);
            MoveToElementInPage(quickSearchTextBox);
            SendKeys(quickSearchTextBox, Query);
            return this;
        }

        public RecruitmentDocumentsManagementPage ClickSearchButton()
        {
            WaitForElementToBeClickable(quickSearchButton);
            MoveToElementInPage(quickSearchButton);
            Click(quickSearchButton);
            return this;
        }

        public RecruitmentDocumentsManagementPage OpenRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            WaitForElementToBeClickable(recordRow(RecordId));
            MoveToElementInPage(recordRow(RecordId));
            Click(recordRow(RecordId));

            return this;
        }

        public RecruitmentDocumentsManagementPage ValidateNoRecordMessageVisibile(bool ExpectedText)
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

        public RecruitmentDocumentsManagementPage ValidateRecordIsPresent(string RecordId)
        {
            WaitForElementVisible(recordRow(RecordId));         
            MoveToElementInPage(recordRow(RecordId));

            return this;
        }

        public RecruitmentDocumentsManagementPage ValidateRecordIsNotPresent(string RecordId)
        {
            bool isRecordPresent = GetElementVisibility(recordRow(RecordId));
            Assert.IsFalse(isRecordPresent);

            return this;
        }

        public RecruitmentDocumentsManagementPage ValidateCellText(Guid RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordCell(RecordID.ToString(), CellPosition));
            MoveToElementInPage(recordCell(RecordID.ToString(), CellPosition));
            ValidateElementText(recordCell(RecordID.ToString(), CellPosition), ExpectedText);

            return this;
        }

        public RecruitmentDocumentsManagementPage ValidateCellText(int RowPosition, int CellPosition, string ExpectedText)
        {
            WaitForElementVisible(recordRowAndCell(RowPosition, CellPosition));
            MoveToElementInPage(recordRowAndCell(RowPosition, CellPosition));
            ValidateElementText(recordRowAndCell(RowPosition, CellPosition), ExpectedText);

            return this;
        }
    }

}

