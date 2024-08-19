
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PersonRecordPage_SummaryArea : CommonMethods
    {
        public PersonRecordPage_SummaryArea(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]");
        readonly By CWSummaryDashboardPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By widgetIframe = By.XPath("//iframe[@onload='CW.DashboardControl.ResizeIFrame(this)']");



        readonly By refreshButton = By.Id("btnRefresh");

        readonly By automationTestingSummaryDashboard_NewRecordButton = By.Id("TI_NewRecordButton");
        By automationTestingSummaryDashboard_record(string RecordID) => By.XPath("//*[@id='" + RecordID + "_Primary']");


        readonly By pprcMessage = By.XPath("//*[@id='CWListViewContainer']/ul/li[1]/span/span");



        #region CDV6-11611 - Test Automation Person Dashboard

        readonly By representsAlertHazard_Field = By.XPath("//*[@id='CWRecordViewContainer']/div/div/div/div/ul/li[1]/p");
        readonly By id_Field = By.XPath("//*[@id='CWRecordViewContainer']/div/div/div/div/ul/li[2]/p");
        readonly By firstName_Field = By.XPath("//*[@id='CWRecordViewContainer']/div/div/div/div/ul/li[3]/p");
        readonly By lastName_Field = By.XPath("//*[@id='CWRecordViewContainer']/div/div/div/div/ul/li[4]/p");
        readonly By statedGender_Field = By.XPath("//*[@id='CWRecordViewContainer']/div/div/div/div/ul/li[5]/p");
        readonly By dob_Field = By.XPath("//*[@id='CWRecordViewContainer']/div/div/div/div/ul/li[6]/p");
        readonly By postCode_Field = By.XPath("//*[@id='CWRecordViewContainer']/div/div/div/div/ul/li[9]/p");
        readonly By createdBy_Field = By.XPath("//*[@id='CWRecordViewContainer']/div/div/div/div/ul/li[10]/p");
        readonly By createdOn_Field = By.XPath("//*[@id='CWRecordViewContainer']/div/div/div/div/ul/li[11]/p");

        #endregion




        public PersonRecordPage_SummaryArea WaitForPersonRecordPage_SummaryAreaToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

            WaitForElement(CWSummaryDashboardPanel_IFrame);
            SwitchToIframe(CWSummaryDashboardPanel_IFrame);

            WaitForElement(widgetIframe);
            SwitchToIframe(widgetIframe);

            return this;
        }

        public PersonRecordPage_SummaryArea ValidateAutomationTestingSummaryDashboardRecordPresent(string RecordID)
        {
            WaitForElement(automationTestingSummaryDashboard_record(RecordID));

            return this;
        }

        public PersonRecordPage_SummaryArea ValidateAutomationTestingSummaryDashboardRecordNotPresent(string RecordID)
        {
            WaitForElementNotVisible(automationTestingSummaryDashboard_record(RecordID), 7);

            return this;
        }


        public PersonRecordPage_SummaryArea ValidateRepresentsAlertHazardText(string ExpectedText)
        {
            ValidateElementText(representsAlertHazard_Field, ExpectedText);

            return this;
        }
        public PersonRecordPage_SummaryArea ValidateIdText(string ExpectedText)
        {
            ValidateElementText(id_Field, ExpectedText);

            return this;
        }
        public PersonRecordPage_SummaryArea ValidateFirstNameText(string ExpectedText)
        {
            ValidateElementText(firstName_Field, ExpectedText);

            return this;
        }
        public PersonRecordPage_SummaryArea ValidateLastNameText(string ExpectedText)
        {
            ValidateElementText(lastName_Field, ExpectedText);

            return this;
        }
        public PersonRecordPage_SummaryArea ValidateStatedGenderText(string ExpectedText)
        {
            ValidateElementText(statedGender_Field, ExpectedText);

            return this;
        }
        public PersonRecordPage_SummaryArea ValidateDOBText(string ExpectedText)
        {
            ValidateElementText(dob_Field, ExpectedText);

            return this;
        }
        public PersonRecordPage_SummaryArea ValidatePostCodeText(string ExpectedText)
        {
            ValidateElementText(postCode_Field, ExpectedText);

            return this;
        }
        public PersonRecordPage_SummaryArea ValidateCreatedByText(string ExpectedText)
        {
            ValidateElementText(createdBy_Field, ExpectedText);

            return this;
        }
        public PersonRecordPage_SummaryArea ValidateCreatedOnText(string ExpectedText)
        {
            ValidateElementText(createdOn_Field, ExpectedText);

            return this;
        }

        public PersonRecordPage_SummaryArea ValidatePPRCMessage(String ExpectedText)
        {
            WaitForElement(pprcMessage);
            ValidateElementText(pprcMessage, ExpectedText);
            return this;
        }

    }
}
