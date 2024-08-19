using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventInjuritySeveritiesRecordPage : CommonMethods
    {
        public ReportableEventInjuritySeveritiesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_reportableEventInjurySeverity = By.Id("iframe_careproviderreportableeventinjuryseverity");

        readonly By ReportableEventSeverityRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By iframe_reportableEventInjurySeverityRecord = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableeventinjuryseverity&')]");

        readonly By ReportableEventInjurieSeverityName_TextField = By.Id("CWField_name");
        readonly By ReportableEventInjurieSeverityCode_TextField = By.Id("CWField_code");
        readonly By ReportableEventInjurieSeverityGovCode_TextField = By.Id("CWField_govcode");
        readonly By ReportableEventInjurieSeverityInactive_YesOption = By.XPath("//input[@id='CWField_inactive_1']");
        readonly By ReportableEventInjurieSeverityInactive_YesOptionChecked = By.XPath("//input[@id='CWField_inactive_1'][@checked='checked']");
        readonly By ReportableEventInjurieSeverityInactive_NoOption = By.XPath("//input[@id='CWField_inactive_0']");
        readonly By ReportableEventInjurieSeverityInactive_NoOptionChecked = By.XPath("//input[@id='CWField_inactive_0'][@checked='checked']");
        readonly By _StartDate_DateField = By.Id("CWField_startdate");
        readonly By ReportableEventInjurieSeverityEndDate_TextField = By.Id("CWField_enddate");
        readonly By ReportableEventInjurieSeverityValidForExport_YesOption = By.XPath("//input[@id='CWField_validforexport_1']");
        readonly By ReportableEventInjurieSeverityValidForExport_YesOptionChecked = By.XPath("//input[@id='CWField_validforexport_1'][@checked='checked']");
        readonly By ReportableEventInjurieSeverityValidForExport_NoOption = By.XPath("//input[@id='CWField_validforexport_0']");
        readonly By ReportableEventInjurieSeverityValidForExport_NoOptionChecked = By.XPath("//input[@id='CWField_validforexport_0'][@checked='checked']");
        readonly By ReportableEventInjurieSeverityResonsibleTeam_TextField = By.Id("CWField_ownerid_Link");
        readonly By ReportableEventInjurieSeverityNotes_TextField = By.Id("CWField_notes");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By AlertText = By.XPath("//div[@class='alert alert-danger']");
        readonly By CloseButton = By.Id("CWCloseButton");

        
        public ReportableEventInjuritySeveritiesRecordPage WaitForReportableEventInjurySeverityRecordPageToLoad(String MergedRecordName)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_reportableEventInjurySeverity);
            SwitchToIframe(iframe_reportableEventInjurySeverity);


            WaitForElement(iframe_reportableEventInjurySeverityRecord);
            SwitchToIframe(iframe_reportableEventInjurySeverityRecord);
            
            WaitForElement(ReportableEventSeverityRecordPageHeader);

            if (driver.FindElement(ReportableEventSeverityRecordPageHeader).Text != "Reportable Event Injury Severity:\r\n" + MergedRecordName)
                throw new Exception("Page title do not equals: Merged Record: " + MergedRecordName);
            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateReportableEventInjurieSeverityNameField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventInjurieSeverityName_TextField);
            else
                WaitForElementNotVisible(ReportableEventInjurieSeverityName_TextField, 3);


            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateReportableEventInjurieSeverityCodeField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventInjurieSeverityCode_TextField);
            else
                WaitForElementNotVisible(ReportableEventInjurieSeverityCode_TextField, 3);


            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateReportableEventInjurieSeverityGovCodeField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventInjurieSeverityGovCode_TextField);
            else
                WaitForElementNotVisible(ReportableEventInjurieSeverityGovCode_TextField, 3);


            return this;
        }


        public ReportableEventInjuritySeveritiesRecordPage ValidateReportableEventInjurieSeverityInactiveRadioBtnField(bool Inactive_Yes)
        {
            if (Inactive_Yes)
            {
                Wait.Until(c => c.FindElement(ReportableEventInjurieSeverityInactive_YesOptionChecked));
                Wait.Until(c => c.FindElement(ReportableEventInjurieSeverityInactive_NoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(ReportableEventInjurieSeverityInactive_YesOption));
                Wait.Until(c => c.FindElement(ReportableEventInjurieSeverityInactive_NoOptionChecked));
            }

            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateStartDateFieldText(string ExpectDateText)
        {
            ScrollToElement(_StartDate_DateField);

            string fieldText = GetElementValue(_StartDate_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

              return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateReportableEventInjurieSeverityEndDateField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventInjurieSeverityEndDate_TextField);
            else
                WaitForElementNotVisible(ReportableEventInjurieSeverityEndDate_TextField, 3);


            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateReportableEventInjurieSeverityValidForExportRadioBtnField(bool ValidForExport_Yes)
        {
            if (ValidForExport_Yes)
            {
                Wait.Until(c => c.FindElement(ReportableEventInjurieSeverityValidForExport_YesOptionChecked));
                Wait.Until(c => c.FindElement(ReportableEventInjurieSeverityValidForExport_NoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(ReportableEventInjurieSeverityValidForExport_YesOption));
                Wait.Until(c => c.FindElement(ReportableEventInjurieSeverityValidForExport_NoOptionChecked));
            }

            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateReportableEventInjurieSeverityResponsibleTeamField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventInjurieSeverityResonsibleTeam_TextField);
            else
                WaitForElementNotVisible(ReportableEventInjurieSeverityResonsibleTeam_TextField, 3);


            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateReportableEventInjurieSeverityNotesTextField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventInjurieSeverityNotes_TextField);
            else
                WaitForElementNotVisible(ReportableEventInjurieSeverityNotes_TextField, 3);


            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage InsertReportableEventInjurieSeverityNameTextField(String Name)
        {
            this.SendKeys(ReportableEventInjurieSeverityName_TextField, Name);

            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage InsertReportableEventInjurieSeverityCodeTextField(String Code)
        {
            this.SendKeys(ReportableEventInjurieSeverityCode_TextField, Code);

            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage InsertReportableEventInjurieSeverityGovCodeTextField(String GovCode)
        {
            this.SendKeys(ReportableEventInjurieSeverityGovCode_TextField, GovCode);

            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage InsertReportableEventInjurieSeverityNotesTextField(String Notes)
        {
            this.SendKeys(ReportableEventInjurieSeverityNotes_TextField, Notes);

            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage WaitForAlertsSectionToLoad()
        {


            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_reportableEventInjurySeverity);
            SwitchToIframe(iframe_reportableEventInjurySeverity);


            WaitForElement(iframe_reportableEventInjurySeverityRecord);
            SwitchToIframe(iframe_reportableEventInjurySeverityRecord);

            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateAlertText(String Expectedtext)
        {
            ValidateElementText(AlertText, Expectedtext);
            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ClickCloseBtn()
        {
            Click(CloseButton);
            return this;
        }

        public ReportableEventInjuritySeveritiesRecordPage ValidateRepoInjurySeverityNameFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(ReportableEventInjurieSeverityName_TextField);
            }
            else
            {
                ValidateElementEnabled(ReportableEventInjurieSeverityName_TextField);
            }
            return this;
        }


    }
}
