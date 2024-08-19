using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventImpactRecordPages : CommonMethods
    {
        public ReportableEventImpactRecordPages(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By CWContent_Iframe = By.XPath("//iframe[@id='CWContentIFrame']");
        readonly By careproviderReportableEvent_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableeventimpact&')]");
        readonly By ReportableEventImpactPersonBodyMapsFrame_Iframe = By.Id("CWIFrame_LinkedRecords");


        readonly By EventIdColumn = By.XPath("//*[@id='CWGridHeaderRow']/th[2]/a/span");
        readonly By pageHeader = By.XPath("//h1[@title='Reportable Event: New']");
        readonly By BackButton = By.XPath("//*[@id='CWToolbar']/div/div/button");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By InternalPersonOrganisation_LookUpButton = By.Id("CWLookupBtn_internalpersonorganisationid");
        readonly By impactTypeList_Field = By.Id("CWField_impacttypeid");
        readonly By InjuriesText_Field = By.XPath("//div[@id='CWSection_Injuries']//div/span");
        readonly By SeverityOfInjuriesMandatory_TextField = By.Id("CWField_careproviderreportableeventseverityid_cwname");
        readonly By RelatedRisk_TextField = By.Id("CWField_relatedriskid_cwname");
        readonly By PersonBodyMaps_Title = By.XPath("//div[@id='CWWrapper']//div/h1[text()='Person Body Maps']");
        readonly By CreatePersonBodyMapRecordButton = By.Id("TI_NewRecordButton");
        readonly By ReportableEvent_MandatoryField = By.Id("CWField_reportableeventid_Link");
        readonly By SeverityOfInjuriesMandatory_LookUpBtn = By.Id("CWLookupBtn_careproviderreportableeventseverityid");
        readonly By RoleInEvevntMandatory_LookUpBtn = By.Id("CWLookupBtn_roleineventid");
        readonly By RelatedRisk_LookUpBtn = By.Id("CWLookupBtn_relatedriskid");
        readonly By IsExternalPersonOrganisationYesOption= By.XPath("//input[@id='CWField_isexternalpersonorganisation_1']");
        readonly By IsExternalPersonOrganisationYesOptionChecked = By.XPath("//input[@id='CWField_isexternalpersonorganisation_1'][@checked='checked']");
        readonly By IsExternalPersonOrganisationNoOption= By.XPath("//input[@id='CWField_isexternalpersonorganisation_0']");
        readonly By IsExternalPersonOrganisationNoOptionChecked = By.XPath("//input[@id='CWField_isexternalpersonorganisation_0'][@checked='checked']");
        readonly By _ExternaPersonOrganisationFieldText = By.Id("CWField_externalpersonorganisationname");
        readonly By _ExternaPersonOrganisationContactFieldText = By.Id("CWField_externalpersonorganisationcontact");
        readonly By _InternalPersonOrganisationFieldText = By.Id("CWField_internalpersonorganisationid_Link");
        readonly By _RoleInEventFieldText = By.Id("CWField_internalpersonorganisationid_Link");
        readonly By _NotesFieldText = By.Id("cke_1_contents");

        
        By RecordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By RecordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By tableHeaderRow(int position) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + position + "]/a/span[1]");


        public ReportableEventImpactRecordPages WaitForReportableEventImpactsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(careproviderReportableEvent_Iframe);
            SwitchToIframe(careproviderReportableEvent_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            System.Threading.Thread.Sleep(3000);

            return this;
        }

        public ReportableEventImpactRecordPages WaitForReportableEventImpactsPersonBodyMapsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(careproviderReportableEvent_Iframe);
            SwitchToIframe(careproviderReportableEvent_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(ReportableEventImpactPersonBodyMapsFrame_Iframe);
            SwitchToIframe(ReportableEventImpactPersonBodyMapsFrame_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            System.Threading.Thread.Sleep(3000);

            return this;
        }

        public ReportableEventImpactRecordPages ClickSaveButton()
        {
            WaitForElement(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

       

        public ReportableEventImpactRecordPages TapInternalPersonOrganisationLookupButton()
        {
            WaitForElementToBeClickable(InternalPersonOrganisation_LookUpButton);
            Click(InternalPersonOrganisation_LookUpButton);

            return this;
        }

        public ReportableEventImpactRecordPages TapSeverityOfInjuriesLookupButton()
        {
            WaitForElementToBeClickable(SeverityOfInjuriesMandatory_LookUpBtn);
            Click(SeverityOfInjuriesMandatory_LookUpBtn);

            return this;
        }

        public ReportableEventImpactRecordPages TapRoleInEvevntLookupButton()
        {
            WaitForElementToBeClickable(RoleInEvevntMandatory_LookUpBtn);
            Click(RoleInEvevntMandatory_LookUpBtn);

            return this;
        }

        public ReportableEventImpactRecordPages TapRelatedRiskLookupButton()
        {
            WaitForElementToBeClickable(RelatedRisk_LookUpBtn);
            Click(RelatedRisk_LookUpBtn);

            return this;
        }

        public ReportableEventImpactRecordPages SelectImpactTypeByText(string ImpactTypeText)
        {
            SelectPicklistElementByText(impactTypeList_Field, ImpactTypeText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }


        public ReportableEventImpactRecordPages ValidateInjuriesTextHeader(string ExpectedText)
        {
            ValidateElementText(InjuriesText_Field, ExpectedText);

            return this;
        }

        public ReportableEventImpactRecordPages ValidateSeverityOfInjuriesMandatoryField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(SeverityOfInjuriesMandatory_TextField);
                ScrollToElement(SeverityOfInjuriesMandatory_TextField);
            }
            else
                WaitForElementNotVisible(SeverityOfInjuriesMandatory_TextField, 3);

            return this;
        }

        public ReportableEventImpactRecordPages ValidateImpactTypeFieldText(string ExpectText)
        {
            ScrollToElement(impactTypeList_Field);
            string fieldText = GetElementValue(impactTypeList_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventImpactRecordPages ValidateRelatedRiskField(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(RelatedRisk_TextField);
                ScrollToElement(RelatedRisk_TextField);
            }
            else
                WaitForElementNotVisible(RelatedRisk_TextField, 3);

            return this;
        }

        public ReportableEventImpactRecordPages ValidatePersonBodyMapsTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(PersonBodyMaps_Title);
                WaitForElementVisible(PersonBodyMaps_Title);
                CheckIfElementExists(PersonBodyMaps_Title);
            }
            else
                WaitForElementNotVisible(PersonBodyMaps_Title, 3);

            return this;
        }

        public ReportableEventImpactRecordPages ClickCreatePersonBodyMapRecordButton()
        {
            WaitForElementToBeClickable(CreatePersonBodyMapRecordButton);
            Click(CreatePersonBodyMapRecordButton);

            return this;
        }

        public ReportableEventImpactRecordPages ValidateImpactRecordReportableEventField(string ExpectText)
        {
            ScrollToElement(ReportableEvent_MandatoryField);
            string fieldText = GetElementText(ReportableEvent_MandatoryField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;


        }

        public ReportableEventImpactRecordPages ValidateIsExternalPersonOrOrganisationField(bool IsExternalPersonOrganisation_Field)
        {
            if (IsExternalPersonOrganisation_Field)
            {
                Wait.Until(c => c.FindElement(IsExternalPersonOrganisationYesOptionChecked));
                Wait.Until(c => c.FindElement(IsExternalPersonOrganisationNoOption));
            }
            else
            {
                Wait.Until(c => c.FindElement(IsExternalPersonOrganisationYesOption));
                Wait.Until(c => c.FindElement(IsExternalPersonOrganisationNoOptionChecked));
            }

            return this;
        }

        public ReportableEventImpactRecordPages SelectIsExternalPersonOrOrganisation(bool IsExternalPersonOrOrganisation)
        {
            if (IsExternalPersonOrOrganisation)
                driver.FindElement(IsExternalPersonOrganisationYesOption).Click();
            else
                driver.FindElement(IsExternalPersonOrganisationNoOption).Click();

            return this;
        }

        public ReportableEventImpactRecordPages ValidatExternaPersonOrganisationFieldText(string ExpectText)
        {
            ScrollToElement(_ExternaPersonOrganisationFieldText);
            string fieldText = GetElementText(_ExternaPersonOrganisationFieldText);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventImpactRecordPages ValidatExternaPersonOrganisationContactFieldText(string ExpectText)
        {
            ScrollToElement(_ExternaPersonOrganisationContactFieldText);
            string fieldText = GetElementText(_ExternaPersonOrganisationContactFieldText);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventImpactRecordPages ValidatInternalPersonOrganisationFieldText(string ExpectText)
        {
            ScrollToElement(_InternalPersonOrganisationFieldText);
            string fieldText = GetElementText(_InternalPersonOrganisationFieldText);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventImpactRecordPages ValidatRoleInEventFieldText(string ExpectText)
        {
            ScrollToElement(_RoleInEventFieldText);
            string fieldText = GetElementText(_RoleInEventFieldText);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventImpactRecordPages ValidatNotesField()
        {
            ScrollToElement(_NotesFieldText);
            CheckIfElementExists(_NotesFieldText);

            return this;
        }

        public ReportableEventImpactRecordPages ValidatExternalPersonOrganisationField(bool isDisplayed)
        {

            if (isDisplayed)
            {
                WaitForElement(_ExternaPersonOrganisationFieldText);
                Assert.IsTrue(GetElementVisibility(_ExternaPersonOrganisationFieldText));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(_ExternaPersonOrganisationFieldText));
            }
            return this;

        }

        public ReportableEventImpactRecordPages ValidatExternalPersonOrganisationContactField(bool isDisplayed)
        {

            if (isDisplayed)
            {
                WaitForElement(_ExternaPersonOrganisationContactFieldText);
                Assert.IsTrue(GetElementVisibility(_ExternaPersonOrganisationContactFieldText));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(_ExternaPersonOrganisationContactFieldText));
            }
            return this;

        }

        public ReportableEventImpactRecordPages ValidatInternalPersonOrganisationField(bool isDisplayed)
        {
            if (isDisplayed)
            {
                WaitForElement(_InternalPersonOrganisationFieldText);
                Assert.IsTrue(GetElementVisibility(_InternalPersonOrganisationFieldText));
            }
            else
            {
                Assert.IsFalse(GetElementVisibility(_InternalPersonOrganisationFieldText));
            }
            return this;
        }

    }
    }

