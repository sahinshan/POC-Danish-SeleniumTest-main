
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// Person Record - Care Plans Tab - Care Plans Sub Tab
    /// </summary>
    public class PersonCarePlansSubPage_CarePlansTab : CommonMethods
    {
        public PersonCarePlansSubPage_CarePlansTab(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=person&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By CWSubTabsPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By CWFrame_ = By.Id("CWFrame");
        //readonly By listViewFrame = By.XPath("//iframe[contains(@src,'dataviewid=bebe2c39-91e6-e811-80dc-0050560502cc')]");
        readonly By CarePlanType_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[2]/a[@title='Care Plan Type']");
        readonly By Person_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[3]/a[@title='Person']");
        readonly By StartDate_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[4]/a[@title='Start Date']");
        readonly By Status_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[5]/a[@title='Status']");
        readonly By ReviewDate_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[6]/a[@title='Review Date']");
        readonly By ReviewFrequency_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[7]/a[@title='Review Frequency']");
        readonly By ResponsibleTeam_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[8]/a[@title='Responsible Team']");

        #region Top Menu

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By mailMergeButton = By.Id("TI_MailMergeButton");
        readonly By deleteRecordButton = By.Id("TI_DeleteRecordButton");

        #endregion

        #region Records

        By record_CheckBox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By recordRow(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");
        By recordRowSelected(string RecordID) => By.XPath("//*[@id='" + RecordID + "'][@class = 'selrow']");


        #endregion




        public PersonCarePlansSubPage_CarePlansTab WaitForPersonCarePlansSubPage_CarePlansTabToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWSubTabsPanel_IFrame);
            SwitchToIframe(CWSubTabsPanel_IFrame);

            WaitForElement(CWFrame_);
            SwitchToIframe(CWFrame_);

            //WaitForElement(listViewFrame);
            //SwitchToIframe(listViewFrame);

            WaitForElement(newRecordButton);
            WaitForElement(exportToExcelButton);
            WaitForElement(mailMergeButton);


            return this;
        }

        public PersonCarePlansSubPage_CarePlansTab SelectCarePlanRecord(string RecordID)
        {
            if(GetElementVisibility(recordRowSelected(RecordID)).Equals(false))
                Click(record_CheckBox(RecordID));

            return this;
        }

        //Method to open Care Plan Record
        public PersonCarePlansSubPage_CarePlansTab OpenRecord(string RecordID)
        {
            WaitForElement(recordRow(RecordID));
            ScrollToElement(recordRow(RecordID));
            Click(recordRow(RecordID));

            return this;
        }

        //Method to Delete record
        public PersonCarePlansSubPage_CarePlansTab ClickDeleteButton()
        {
            WaitForElement(deleteRecordButton);
            ScrollToElement(deleteRecordButton);
            Click(deleteRecordButton);

            return this;
        }

        public PersonCarePlansSubPage_CarePlansTab ClickOnCarePlanRecord(string RecordID)
        {
            Click(recordRow(RecordID));

            return this;
        }

        //verify record is present or not present
        public PersonCarePlansSubPage_CarePlansTab VerifyRecordIsPresent(string RecordID, bool ExpectedPresent = true)
        {
            if(ExpectedPresent)
                WaitForElementVisible(recordRow(RecordID));
            else
                WaitForElementNotVisible(recordRow(RecordID), 3);
            return this;
        }

        //Delete record
        public PersonCarePlansSubPage_CarePlansTab DeleteCarePlanRecord(string RecordID)
        {
            SelectCarePlanRecord(RecordID);
            ClickDeleteButton();
            return this;
        }

        public PersonCarePlansSubPage_CarePlansTab ClickMailMergeButton()
        {
            Click(mailMergeButton);

            return this;
        }
        public PersonCarePlansSubPage_CarePlansTab ClickCreateNewRecord()
        {
            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);
            return this;
        }
        
        public PersonCarePlansSubPage_CarePlansTab ValidateCarePlanTypeHeader(String ExpectedText)
        {
            WaitForElementVisible(CarePlanType_Header);
            ValidateElementText(CarePlanType_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_CarePlansTab ValidatePersonHeader(String ExpectedText)
        {
            ValidateElementText(Person_Header, ExpectedText);
            return this;
        }
        public PersonCarePlansSubPage_CarePlansTab ValidateStartDateHeader(String ExpectedText)
        {
            ValidateElementText(StartDate_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_CarePlansTab ValidateStatusHeader(String ExpectedText)
        {
            ValidateElementText(Status_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_CarePlansTab ValidateReviewDateHeader(String ExpectedText)
        {
            ValidateElementText(ReviewDate_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_CarePlansTab ValidateReviewFrequencyHeader(String ExpectedText)
        {
            ValidateElementText(ReviewFrequency_Header, ExpectedText);
            return this;
        }

        public PersonCarePlansSubPage_CarePlansTab ValidateResponsibleTeamHeader(String ExpectedText)
        {
            ValidateElementText(ResponsibleTeam_Header, ExpectedText);
            return this;
        }
    }
}
