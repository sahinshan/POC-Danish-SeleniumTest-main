using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventSubCategoriesRecordPage : CommonMethods
    {
        public ReportableEventSubCategoriesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_reportableEventSubCategories = By.Id("iframe_careproviderreportableeventsubcategory");

        readonly By ReportableEventSubCategoriesPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");



        readonly By iframe_reportableEventSubCategoriesRecord = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableeventsubcategory&')]");

        readonly By ReportableEventSubCategoriesName_TextField = By.Id("CWField_name");
        readonly By ReportableEventSubCategoriesCode_TextField = By.Id("CWField_code");
        readonly By ReportableEventSubCategoriesGovCode_TextField = By.Id("CWField_govcode");
        readonly By ReportableEventSubCategoriesInactive_YesOption = By.XPath("//input[@id='CWField_inactive_1']");
        readonly By ReportableEventSubCategoriesInactive_YesOptionChecked = By.XPath("//input[@id='CWField_inactive_1'][@checked='checked']");
        readonly By ReportableEventSubCategoriesInactive_NoOption = By.XPath("//input[@id='CWField_inactive_0']");
        readonly By ReportableEventSubCategoriesInactive_NoOptionChecked = By.XPath("//input[@id='CWField_inactive_0'][@checked='checked']");
        readonly By _StartDate_DateField = By.Id("CWField_startdate");
        readonly By ReportableEventSubCategoriesEndDate_TextField = By.Id("CWField_enddate");
        readonly By ReportableEventSubCategoriesValidForExport_YesOption = By.XPath("//input[@id='CWField_validforexport_1']");
        readonly By ReportableEventSubCategoriesValidForExport_YesOptionChecked = By.XPath("//input[@id='CWField_validforexport_1'][@checked='checked']");
        readonly By ReportableEventSubCategoriesValidForExport_NoOption = By.XPath("//input[@id='CWField_validforexport_0']");
        readonly By ReportableEventSubCategoriesValidForExport_NoOptionChecked = By.XPath("//input[@id='CWField_validforexport_0'][@checked='checked']");
        readonly By ReportableEventSubCategoriesCategory_TextField = By.Id("CWField_careproviderreportableeventcategoryid_cwname");
        readonly By ReportableEventSubCategoriesResonsibleTeam_TextField = By.Id("CWField_ownerid_Link");
        readonly By ReportableEventSubCategoriesNotes_TextField = By.Id("CWField_notes");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By AlertText = By.XPath("//div[@class='alert alert-danger']");
        readonly By CloseButton = By.Id("CWCloseButton");
        readonly By CategoryLookUpButton = By.Id("CWLookupBtn_careproviderreportableeventcategoryid");



        public ReportableEventSubCategoriesRecordPage WaitForReportableEventSubCategoriesRecordPageToLoad(String MergedRecordName)
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_reportableEventSubCategories);
            SwitchToIframe(iframe_reportableEventSubCategories);


            WaitForElement(iframe_reportableEventSubCategoriesRecord);
            SwitchToIframe(iframe_reportableEventSubCategoriesRecord);
            
            WaitForElement(ReportableEventSubCategoriesPageHeader);

            if (driver.FindElement(ReportableEventSubCategoriesPageHeader).Text != "Reportable Event Subcategory:\r\n" + MergedRecordName)
                throw new Exception("Page title do not equals: Merged Record: " + MergedRecordName);
            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateReportableEventSubCategoriesNameField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventSubCategoriesName_TextField);
            else
                WaitForElementNotVisible(ReportableEventSubCategoriesName_TextField, 3);


            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateReportableEventSubCategoriesCodeField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventSubCategoriesCode_TextField);
            else
                WaitForElementNotVisible(ReportableEventSubCategoriesCode_TextField, 3);


            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateReportableEventSubCategoriesGovCodeField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventSubCategoriesGovCode_TextField);
            else
                WaitForElementNotVisible(ReportableEventSubCategoriesGovCode_TextField, 3);


            return this;
        }


        public ReportableEventSubCategoriesRecordPage ValidateReportableEventSubCategoriesInactiveRadioBtnField(bool Inactive_Yes)
        {
            if (Inactive_Yes)
            {
                Wait.Until(c => c.FindElement(ReportableEventSubCategoriesInactive_YesOptionChecked));
                Wait.Until(c => c.FindElement(ReportableEventSubCategoriesInactive_NoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(ReportableEventSubCategoriesInactive_YesOption));
                Wait.Until(c => c.FindElement(ReportableEventSubCategoriesInactive_NoOptionChecked));
            }

            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateStartDateFieldText(string ExpectDateText)
        {
            ScrollToElement(_StartDate_DateField);

            string fieldText = GetElementValue(_StartDate_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

              return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateReportableEventSubCategoriesEndDateField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventSubCategoriesEndDate_TextField);
            else
                WaitForElementNotVisible(ReportableEventSubCategoriesEndDate_TextField, 3);


            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateReportableEventSubCategoriesValidForExportRadioBtnField(bool ValidForExport_Yes)
        {
            if (ValidForExport_Yes)
            {
                Wait.Until(c => c.FindElement(ReportableEventSubCategoriesValidForExport_YesOptionChecked));
                Wait.Until(c => c.FindElement(ReportableEventSubCategoriesValidForExport_NoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(ReportableEventSubCategoriesValidForExport_YesOption));
                Wait.Until(c => c.FindElement(ReportableEventSubCategoriesValidForExport_NoOptionChecked));
            }

            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateReportableEventSubCategoriesResponsibleTeamField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventSubCategoriesResonsibleTeam_TextField);
            else
                WaitForElementNotVisible(ReportableEventSubCategoriesResonsibleTeam_TextField, 3);


            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateReportableEventSubCategoriesCategoryField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventSubCategoriesResonsibleTeam_TextField);
            else
                WaitForElementNotVisible(ReportableEventSubCategoriesResonsibleTeam_TextField, 3);


            return this;
        }

        public ReportableEventSubCategoriesRecordPage TapReportableEventSubCategoriesCategoryLookupButton()
        {
            Click(CategoryLookUpButton);

            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateReportableEventSubCategoriesNotesTextField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
                WaitForElementVisible(ReportableEventSubCategoriesNotes_TextField);
            else
                WaitForElementNotVisible(ReportableEventSubCategoriesNotes_TextField, 3);


            return this;
        }

        public ReportableEventSubCategoriesRecordPage InsertReportableEventSubCategoriesNameTextField(String Name)
        {
            this.SendKeys(ReportableEventSubCategoriesName_TextField, Name);

            return this;
        }

        public ReportableEventSubCategoriesRecordPage InsertReportableEventSubCategoriesCodeTextField(String Code)
        {
            this.SendKeys(ReportableEventSubCategoriesCode_TextField, Code);

            return this;
        }

        public ReportableEventSubCategoriesRecordPage InsertReportableEventSubCategoriesGovCodeTextField(String GovCode)
        {
            this.SendKeys(ReportableEventSubCategoriesGovCode_TextField, GovCode);

            return this;
        }

        public ReportableEventSubCategoriesRecordPage InsertReportableEventSubCategoriesNotesTextField(String Notes)
        {
            this.SendKeys(ReportableEventSubCategoriesNotes_TextField, Notes);

            return this;
        }

        public ReportableEventSubCategoriesRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ReportableEventSubCategoriesRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public ReportableEventSubCategoriesRecordPage WaitForAlertsSectionToLoad()
        {


            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_reportableEventSubCategories);
            SwitchToIframe(iframe_reportableEventSubCategories);


            WaitForElement(iframe_reportableEventSubCategoriesRecord);
            SwitchToIframe(iframe_reportableEventSubCategoriesRecord);

            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateAlertText(String Expectedtext)
        {
            ValidateElementText(AlertText, Expectedtext);
            return this;
        }

        public ReportableEventSubCategoriesRecordPage ClickCloseBtn()
        {
            Click(CloseButton);
            return this;
        }

        public ReportableEventSubCategoriesRecordPage ValidateRepoEvevntSubCategoryNameFieldDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(ReportableEventSubCategoriesName_TextField);
            }
            else
            {
                ValidateElementEnabled(ReportableEventSubCategoriesName_TextField);
            }
            return this;
        }


    }
}
