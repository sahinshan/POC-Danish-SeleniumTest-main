using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.People
{
    public class PersonAlertandHazardsPage : CommonMethods
    {
        public PersonAlertandHazardsPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By navItem_PersonAlertAndHazardsFrame = By.Id("CWUrlPanel_IFrame");


        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By assignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button[@title='Back']");
        readonly By Back_Button = By.XPath("//*[@id='CWToolbar']/div/div/button[@onclick='; return false;']");

        readonly By NoRecordLabel = By.XPath("//div[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By NoRecordMessage = By.XPath("//div[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");
        readonly By summarySection = By.XPath("//li[@id='CWNavGroup_SummaryDashboard']/a[@title='Summary']");

        readonly By timelineSection = By.XPath("//li[@id='CWNavGroup_Timeline']/a[@title='Timeline']");


        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");
        By recordRowCheckBox(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[1]/input");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");

        public PersonAlertandHazardsPage PersonAlertandHazardsPageToLoad(string PageTitle)
        {
            this.driver.SwitchTo().DefaultContent();


            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog);
            this.SwitchToIframe(iframe_CWDialog);

            this.WaitForElement(navItem_PersonAlertAndHazardsFrame);
            this.SwitchToIframe(navItem_PersonAlertAndHazardsFrame);

            this.WaitForElement(pageHeader);


            this.WaitForElement(saveButton);
            this.WaitForElement(saveAndCloseButton);

            return this;
        }

        public PersonAlertandHazardsPage WaitForPersonAlertAndHazardsPageToLoad()
        {
            this.SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(navItem_PersonAlertAndHazardsFrame);
            SwitchToIframe(navItem_PersonAlertAndHazardsFrame);

            WaitForElement(pageHeader);

            ValidateElementText(pageHeader, "Person Alerts And Hazards");
            

            WaitForElement(newRecordButton);

            return this;
        }
      

        public PersonAlertandHazardsPage ClickNewRecordButton()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;

        }
        public PersonAlertandHazardsPage OpenPersonAlertAndHazardsRecord(string RecordId)
        {
            WaitForElement(recordRow(RecordId));
            driver.FindElement(recordRow(RecordId)).Click();

            return this;
        }
        public PersonAlertandHazardsPage SelectPersonAlertAndHazardRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }
        public PersonAlertandHazardsPage ClickDeleteRecordButton()
        {
            WaitForElementToBeClickable(deleteRecordButton);
            Click(deleteRecordButton);

            return this;

        }
        public PersonAlertandHazardsPage ClickExportToExcelButton()
        {
            WaitForElementToBeClickable(exportToExcelButton);
            Click(exportToExcelButton);

            return this;
        }

        public PersonAlertandHazardsPage ClickAssignButton()
        {
            WaitForElementToBeClickable(assignRecordButton);
            Click(assignRecordButton);

            return this;
        }
        public PersonAlertandHazardsPage ClickBackButton()
        {
            WaitForElementToBeClickable(Back_Button);
            Click(Back_Button);

            return this;
        }

        public PersonAlertandHazardsPage TapSummaryTab()
        {
            WaitForElement(summarySection);
            Click(summarySection);

            return this;
        }
        public PersonAlertandHazardsPage TapTimeLineTab()
        {
            Click(timelineSection);

            return this;
        }
        public PersonAlertandHazardsPage ValidateNoErrorMessageLabelVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordLabel);
            }
            else
            {
                WaitForElementNotVisible(NoRecordLabel, 5);
            }
            return this;
        }

        public PersonAlertandHazardsPage ValidateNoRecordMessageVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoRecordMessage);

            }
            else
            {
                WaitForElementNotVisible(NoRecordMessage, 5);
            }
            return this;
        }

    }
}
