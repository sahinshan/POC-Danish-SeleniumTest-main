using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FAQApplicationComponentsPage : CommonMethods
    {
        public FAQApplicationComponentsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By SummaryDashboardRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=faq&')]");
        readonly By CWRelatedRecordPanel_IFrame = By.Id("CWUrlPanel_IFrame");


        readonly By FAQApplicationComponentsPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By viewsPicklist = By.Id("CWViewSelector");
        readonly By quickSearchTextBox = By.XPath("//input[@id='CWQuickSearch']");
        readonly By quickSearchButton = By.XPath("//button[@id='CWQuickSearchButton']");




        readonly By selectAll_Checkbox = By.Id("cwgridheaderselector");
        readonly By application_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[2]");
        readonly By component_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[3]");
        readonly By createdby_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[4]");
        readonly By createdon_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[5]");
        readonly By modifiedby_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[6]");
        readonly By modifiedon_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[7]");
        readonly By validforExport_Header = By.XPath("//*[@id='CWGridHeaderRow']/th[8]");




        By FAQRowCheckBox(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[1]/input");

        By FAQRow_ApplicationCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[2]");
        By FAQRow_ComponentCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[3]");
        By FAQRow_CreatedByCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[4]");
        By FAQRow_CreatedOnCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[5]");
        By FAQRow_ModifiedByCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[6]");
        By FAQRow_ModifiedOnCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[7]");
        By FAQRow_ValidForExportCell(string FAQID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + FAQID + "']/td[8]");



        readonly By NewRecordButton = By.Id("TI_NewRecordButton");






        public FAQApplicationComponentsPage WaitForFAQApplicationComponentsPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SummaryDashboardRecordIFrame);
            SwitchToIframe(SummaryDashboardRecordIFrame);

            WaitForElement(CWRelatedRecordPanel_IFrame);
            SwitchToIframe(CWRelatedRecordPanel_IFrame);

            WaitForElement(FAQApplicationComponentsPageHeader);

            WaitForElement(application_Header);
            WaitForElement(component_Header);
            WaitForElement(createdby_Header);
            WaitForElement(createdon_Header);
            WaitForElement(modifiedby_Header);
            WaitForElement(modifiedon_Header);
            WaitForElement(validforExport_Header);

            return this;
        }

        

        public FAQApplicationComponentsPage OpenApplicationComponentRecord(string recordID)
        {
            WaitForElement(FAQRow_ApplicationCell(recordID));
            Click(FAQRow_ApplicationCell(recordID));

            return this;
        }

        public FAQApplicationComponentsPage SelectApplicationComponentRecord(string recordID)
        {
            WaitForElement(FAQRowCheckBox(recordID));
            Click(FAQRowCheckBox(recordID));

            return this;
        }

        public FAQApplicationComponentsPage TapNewRecordButton()
        {
            WaitForElement(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }


        public FAQApplicationComponentsPage ValidateApplicationCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_ApplicationCell(recordID), ExpectedText);
            return this;
        }
        public FAQApplicationComponentsPage ValidateComponentCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_ComponentCell(recordID), ExpectedText);
            return this;
        }
        public FAQApplicationComponentsPage ValidateCreatedByCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_CreatedByCell(recordID), ExpectedText);
            return this;
        }
        public FAQApplicationComponentsPage ValidateCreatedOnCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_CreatedOnCell(recordID), ExpectedText);
            return this;
        }
        public FAQApplicationComponentsPage ValidateModifiedByCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_ModifiedByCell(recordID), ExpectedText);
            return this;
        }
        public FAQApplicationComponentsPage ValidateModifiedOnCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_ModifiedOnCell(recordID), ExpectedText);
            return this;
        }
        public FAQApplicationComponentsPage ValidateValidForExportCell(string recordID, string ExpectedText)
        {
            ValidateElementText(FAQRow_ValidForExportCell(recordID), ExpectedText);
            return this;
        }


    }
}
