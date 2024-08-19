using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonPostAdoptionLinksPage : CommonMethods
    {
        public PersonPostAdoptionLinksPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By navItem_PersonAdoptionLinksFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");



        public PersonPostAdoptionLinksPage WaitForPersonPostAdoptionLinksPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(navItem_PersonAdoptionLinksFrame);
            SwitchToIframe(navItem_PersonAdoptionLinksFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Adoption Links");
            

            WaitForElement(newRecordButton);

            return this;
        }

        public PersonPostAdoptionLinksPage ValidateRecordCellText(string RecordID, int CellPosition, string ExpectedText)
        {
            WaitForElement(recordCell(RecordID, CellPosition));
            ValidateElementText(recordCell(RecordID, CellPosition), ExpectedText);

            return this;
        }

        public PersonPostAdoptionLinksPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }
        public PersonPostAdoptionLinksPage OpenPersonAdoptionLinksRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }
        public PersonPostAdoptionLinksPage SelectPersonAdoptionLinkRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }
        public PersonPostAdoptionLinksPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;

        }
        public PersonPostAdoptionLinksPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }
        public PersonPostAdoptionLinksPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }



    }
}
