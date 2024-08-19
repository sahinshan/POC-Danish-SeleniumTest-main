using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventRolesRecordPage : CommonMethods
    {
        public ReportableEventRolesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_reportableEventRoles = By.Id("iframe_careproviderreportableeventrole");

        readonly By ReportableEventRolePageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By iframe_reportableEventRoleRecord = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableeventrole&')]");

        readonly By ReportableEventRoleName_TextField = By.Id("CWField_name");
        readonly By ReportableEventRoleCode_TextField = By.Id("CWField_code");
        readonly By ReportableEventRoleGovCode_TextField = By.Id("CWField_govcode");
        readonly By ReportableEventRoleInactive_YesOption = By.XPath("//input[@id='CWField_inactive_1']");
        readonly By ReportableEventRoleInactive_YesOptionChecked = By.XPath("//input[@id='CWField_inactive_1'][@checked='checked']");
        readonly By ReportableEventRoleInactive_NoOption = By.XPath("//input[@id='CWField_inactive_0']");
        readonly By ReportableEventRoleInactive_NoOptionChecked = By.XPath("//input[@id='CWField_inactive_0'][@checked='checked']");
        readonly By _StartDate_DateField = By.Id("CWField_startdate");
        readonly By ReportableEventRoleEndDate_TextField = By.Id("CWField_enddate");
        readonly By ReportableEventRoleValidForExport_YesOption = By.XPath("//input[@id='CWField_validforexport_1']");
        readonly By ReportableEventRoleValidForExport_YesOptionChecked = By.XPath("//input[@id='CWField_validforexport_1'][@checked='checked']");
        readonly By ReportableEventRoleValidForExport_NoOption = By.XPath("//input[@id='CWField_validforexport_0']");
        readonly By ReportableEventRoleValidForExport_NoOptionChecked = By.XPath("//input[@id='CWField_validforexport_0'][@checked='checked']");
        readonly By ReportableEventRoleResonsibleTeam_TextField = By.Id("CWField_ownerid_Link");
        readonly By ReportableEventRoleNotes_TextField = By.Id("CWField_notes");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By AlertText = By.XPath("//div[@class='alert alert-danger']");
        readonly By CloseButton = By.Id("CWCloseButton");

        
        public ReportableEventRolesRecordPage WaitForReportableEventRolesRecordPageToLoad(String MergedRecordName)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_reportableEventRoles);
            SwitchToIframe(iframe_reportableEventRoles);


            WaitForElement(iframe_reportableEventRoleRecord);
            SwitchToIframe(iframe_reportableEventRoleRecord);
            
            WaitForElement(ReportableEventRolePageHeader);

            if (driver.FindElement(ReportableEventRolePageHeader).Text != "Reportable Event Role:\r\n" + MergedRecordName)
                throw new Exception("Page title do not equals: Merged Record: " + MergedRecordName);
            return this;
        }

        public ReportableEventRolesRecordPage ValidateReportableEventRolesNameField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventRoleName_TextField);
            else
                WaitForElementNotVisible(ReportableEventRoleName_TextField, 3);


            return this;
        }

        public ReportableEventRolesRecordPage ValidateReportableEventRolesCodeField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventRoleCode_TextField);
            else
                WaitForElementNotVisible(ReportableEventRoleCode_TextField, 3);


            return this;
        }

        public ReportableEventRolesRecordPage ValidateReportableEventRolesGovCodeField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventRoleGovCode_TextField);
            else
                WaitForElementNotVisible(ReportableEventRoleGovCode_TextField, 3);


            return this;
        }


        public ReportableEventRolesRecordPage ValidateReportableEventRolesInactiveRadioBtnField(bool Inactive_Yes)
        {
            if (Inactive_Yes)
            {
                Wait.Until(c => c.FindElement(ReportableEventRoleInactive_YesOptionChecked));
                Wait.Until(c => c.FindElement(ReportableEventRoleInactive_NoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(ReportableEventRoleInactive_YesOption));
                Wait.Until(c => c.FindElement(ReportableEventRoleInactive_NoOptionChecked));
            }

            return this;
        }

        public ReportableEventRolesRecordPage ValidateStartDateFieldText(string ExpectDateText)
        {
            ScrollToElement(_StartDate_DateField);

            string fieldText = GetElementValue(_StartDate_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

              return this;
        }

        public ReportableEventRolesRecordPage ValidateReportableEventRoleEndDateField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventRoleEndDate_TextField);
            else
                WaitForElementNotVisible(ReportableEventRoleEndDate_TextField, 3);


            return this;
        }

        public ReportableEventRolesRecordPage ValidateReportableEventRoleValidForExportRadioBtnField(bool ValidForExport_Yes)
        {
            if (ValidForExport_Yes)
            {
                Wait.Until(c => c.FindElement(ReportableEventRoleValidForExport_YesOptionChecked));
                Wait.Until(c => c.FindElement(ReportableEventRoleValidForExport_NoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(ReportableEventRoleValidForExport_YesOption));
                Wait.Until(c => c.FindElement(ReportableEventRoleValidForExport_NoOptionChecked));
            }

            return this;
        }

        public ReportableEventRolesRecordPage ValidateReportableEventRoleResponsibleTeamField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventRoleResonsibleTeam_TextField);
            else
                WaitForElementNotVisible(ReportableEventRoleResonsibleTeam_TextField, 3);


            return this;
        }

        public ReportableEventRolesRecordPage ValidateReportableEventRoleNotesTextField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventRoleNotes_TextField);
            else
                WaitForElementNotVisible(ReportableEventRoleNotes_TextField, 3);


            return this;
        }

        public ReportableEventRolesRecordPage InsertReportableEventRoleNameTextField(String Name)
        {
            this.SendKeys(ReportableEventRoleName_TextField, Name);

            return this;
        }

        public ReportableEventRolesRecordPage InsertReportableEventRoleCodeTextField(String Code)
        {
            this.SendKeys(ReportableEventRoleCode_TextField, Code);

            return this;
        }

        public ReportableEventRolesRecordPage InsertReportableEventRoleGovCodeTextField(String GovCode)
        {
            this.SendKeys(ReportableEventRoleGovCode_TextField, GovCode);

            return this;
        }

        public ReportableEventRolesRecordPage InsertReportableEventRoleNotesTextField(String Notes)
        {
            this.SendKeys(ReportableEventRoleNotes_TextField, Notes);

            return this;
        }

        public ReportableEventRolesRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ReportableEventRolesRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ReportableEventRolesRecordPage WaitForAlertsSectionToLoad()
        {


            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_reportableEventRoles);
            SwitchToIframe(iframe_reportableEventRoles);


            WaitForElement(iframe_reportableEventRoleRecord);
            SwitchToIframe(iframe_reportableEventRoleRecord);

            return this;
        }

        public ReportableEventRolesRecordPage ValidateAlertText(String Expectedtext)
        {
            ValidateElementText(AlertText, Expectedtext);
            return this;
        }

        public ReportableEventRolesRecordPage ClickCloseBtn()
        {
            Click(CloseButton);
            return this;
        }

        public ReportableEventRolesRecordPage ValidateRepoRoleNameFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(ReportableEventRoleName_TextField);
            }
            else
            {
                ValidateElementEnabled(ReportableEventRoleName_TextField);
            }
            return this;
        }


    }
}
