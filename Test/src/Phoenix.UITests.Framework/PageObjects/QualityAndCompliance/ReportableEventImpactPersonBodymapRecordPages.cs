using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Phoenix.UITests.Framework.PageObjects
{
    public class ReportableEventImpactPersonBodymapRecordPages : CommonMethods
    {
        public ReportableEventImpactPersonBodymapRecordPages(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }
        readonly By CWContent_Iframe = By.XPath("//iframe[@id='CWContentIFrame']");
        //readonly By careproviderReportableEvent_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careproviderreportableeventimpact&')]");
        // readonly By ReportableEventImpactPersonBodyMapsFrame_Iframe = By.Id("CWIFrame_EventImpactBodyMaps");
        readonly By careproviderReportableEvent_PersonBodymapsRecord_Iframe = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=personbodymap&')]");

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
        readonly By PersonBodyMaps_Title = By.XPath("//div[@id='CWWrapper']//div/h1[text()='Person Body Maps']");
        readonly By CreatePersonBodyMapRecordButton = By.Id("TI_NewRecordButton");
        readonly By ReportableEventImpactPerson_Field = By.XPath("//li[@id='CWControlHolder_personid']//div/a");

        By ReportableEventImpact_Field(string RecordId) => By.XPath("//li[@id='MS_linkedrecordid_" + RecordId + "']");

        readonly By _DateOfEvent_Field = By.XPath("//li[@id='CWControlHolder_dateofevent']//div/input[@id='CWField_dateofevent']");
        By RecordRow(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");
        By RecordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]");

        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");
        By tableHeaderRow(int position) => By.XPath("//*[@id='CWGridHeaderRow']/th[" + position + "]/a/span[1]");


        public ReportableEventImpactPersonBodymapRecordPages WaitForReportableEventImpactsPersonBodyMapsRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(CWContent_Iframe);
            SwitchToIframe(CWContent_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(careproviderReportableEvent_PersonBodymapsRecord_Iframe);
            SwitchToIframe(careproviderReportableEvent_PersonBodymapsRecord_Iframe);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            WaitForElement(SaveButton);
            WaitForElement(SaveAndCloseButton);

            System.Threading.Thread.Sleep(3000);

            return this;
        }

        public ReportableEventImpactPersonBodymapRecordPages ValidateReportableEventImpactPersonFieldText(string ExpectText)
        {
            ScrollToElement(ReportableEventImpactPerson_Field);
            string fieldText = GetElementText(ReportableEventImpactPerson_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventImpactPersonBodymapRecordPages ValidateReportableEventImpactFieldText(string ReportableEventImpactId, string ExpectText)
        {
            ScrollToElement(ReportableEventImpact_Field(ReportableEventImpactId));
            string fieldText = GetElementText(ReportableEventImpact_Field(ReportableEventImpactId));
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public ReportableEventImpactPersonBodymapRecordPages ValidateReportableEventImpactFieldText(Guid ReportableEventImpactId, string ExpectText)
        {
            return ValidateReportableEventImpactFieldText(ReportableEventImpactId.ToString(), ExpectText);
        }

        public ReportableEventImpactPersonBodymapRecordPages ValidateDateOfEventFieldText(string ExpectText)
        {
            ScrollToElement(_DateOfEvent_Field);
            string fieldText = GetElementValue(_DateOfEvent_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




    }
}

