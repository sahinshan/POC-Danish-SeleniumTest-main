using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class FileDestructionGDPRRecordPage : CommonMethods
    {

        public FileDestructionGDPRRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog_ = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=filedestruction&')]");

        readonly By pageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By NotificationArea = By.XPath("//*[@id='CWNotificationMessage_DataForm']");

        

        readonly By backbutton = By.XPath("//button[@title='Back']");
        readonly By saveButton = By.Id("TI_SaveButton");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By FirstApprovedByButton = By.Id("TI_FirstApprovedByButton");
        readonly By SecondApprovedByButton = By.Id("TI_SecondApprovedByButton");



        readonly By RecordsToBeDeleted_FieldTitle = By.XPath("//*[@id='CWLabelHolder_recordid']/label[text()='Record to be deleted']");
        readonly By SurrogateRecord_FieldTitle = By.XPath("//*[@id='CWLabelHolder_defaultrecordid']/label[text()='Surrogate Record']");
        readonly By ScheduleDateFordestruction_FieldTitle = By.XPath("//*[@id='CWLabelHolder_scheduledatetime']/label[text()='Schedule date for destruction']");
        readonly By FileDestructionStatus_FieldTitle = By.XPath("//*[@id='CWLabelHolder_filedestructionstatusid']/label[text()='Status']");
        readonly By ResponsibleTeam_FieldTitle = By.XPath("//*[@id='CWLabelHolder_ownerid']/label[text()='Responsible Team']");
        readonly By FirstApprovedBy_FieldTitle = By.XPath("//*[@id='CWLabelHolder_firstapprovedbyid']/label[text()='First Approved By']");
        readonly By SecondApprovedBy_FieldTitle = By.XPath("//*[@id='CWLabelHolder_secondapprovedbyid']/label[text()='Second Approved By']");
        readonly By Error_FieldTitle = By.XPath("//*[@id='CWLabelHolder_error']/label[text()='Error']");


        
        readonly By RecordToBeDeleted_MessageArea = By.XPath("//*[@id='CWControlHolder_recordid']/label/span");
        readonly By SurrogateRecord_MessageArea = By.XPath("//*[@id='CWControlHolder_defaultrecordid']/label/span");
        readonly By ScheduleDateForDestructionDate_MessageArea = By.XPath("//*[@id='CWControlHolder_scheduledatetime']/div/div[1]/label/span");
        readonly By ScheduleDateForDestructionTime_MessageArea = By.XPath("//*[@id='CWControlHolder_scheduledatetime']/div/div[2]/label/span");


        readonly By RecordsToBeDeleted_LookupButton = By.Id("CWLookupBtn_recordid");
        readonly By SurrogateRecord_LookupButton = By.Id("CWLookupBtn_defaultrecordid");
        readonly By ScheduleDateFordestruction_DateField = By.Id("CWField_scheduledatetime");
        readonly By ScheduleDateFordestruction_TimeField = By.Id("CWField_scheduledatetime_Time");
        readonly By FileDestructionStatus_Field = By.Id("CWField_filedestructionstatusid");
        readonly By ResponsibleTeam_LookupButton = By.Id("CWLookupBtn_ownerid");
        readonly By FirstApprovedBy_FieldLink = By.XPath("//*[@id='CWField_firstapprovedbyid_Link']");
        readonly By SecondApprovedBy_FieldLink = By.XPath("//*[@id='CWField_secondapprovedbyid_Link']");
        readonly By Error_Field = By.Id("CWField_error");


        public FileDestructionGDPRRecordPage WaitForFileDestructionGDPRRecordPageToLoad(string RecordName)
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog_);
            this.SwitchToIframe(iframe_CWDialog_);

            this.WaitForElement(pageHeader);

            this.WaitForElement(RecordsToBeDeleted_FieldTitle);
            this.WaitForElement(SurrogateRecord_FieldTitle);
            this.WaitForElement(ScheduleDateFordestruction_FieldTitle);
            this.WaitForElement(FileDestructionStatus_FieldTitle);
            this.WaitForElement(ResponsibleTeam_FieldTitle);
            this.WaitForElement(FirstApprovedBy_FieldTitle);
            this.WaitForElement(SecondApprovedBy_FieldTitle);
            this.WaitForElement(Error_FieldTitle);

            ValidateElementText(pageHeader, "File Destruction (GDPR):\r\n" + RecordName);

            return this;
        }

        public FileDestructionGDPRRecordPage WaitForFileDestructionGDPRRecordPageToLoad()
        {
            this.driver.SwitchTo().DefaultContent();

            this.WaitForElement(contentIFrame);
            this.SwitchToIframe(contentIFrame);

            this.WaitForElement(iframe_CWDialog_);
            this.SwitchToIframe(iframe_CWDialog_);

            this.WaitForElement(pageHeader);

            this.WaitForElement(RecordsToBeDeleted_FieldTitle);
            this.WaitForElement(SurrogateRecord_FieldTitle);
            this.WaitForElement(ScheduleDateFordestruction_FieldTitle);
            this.WaitForElement(FileDestructionStatus_FieldTitle);
            this.WaitForElement(ResponsibleTeam_FieldTitle);
            this.WaitForElement(FirstApprovedBy_FieldTitle);
            this.WaitForElement(SecondApprovedBy_FieldTitle);
            this.WaitForElement(Error_FieldTitle);

            return this;
        }





        public FileDestructionGDPRRecordPage ClickRecordsToBeDeletedLookupButton()
        {
            this.Click(RecordsToBeDeleted_LookupButton);

            return this;
        }

        public FileDestructionGDPRRecordPage ClickSurrogateRecordLookupButton()
        {
            this.Click(SurrogateRecord_LookupButton);

            return this;
        }

        public FileDestructionGDPRRecordPage InsertScheduleDateForDestruction_Date(string ValueToInsert)
        {
            SendKeys(ScheduleDateFordestruction_DateField, ValueToInsert);
            SendKeysWithoutClearing(ScheduleDateFordestruction_DateField, Keys.Tab);

            return this;
        }

        public FileDestructionGDPRRecordPage InsertScheduleDateForDestruction_Time(string ValueToInsert)
        {
            this.SendKeys(ScheduleDateFordestruction_TimeField, ValueToInsert);

            return this;
        }

        public FileDestructionGDPRRecordPage ValidateFileDestructionStatusText(string ExpectedText)
        {
            ValidateElementText(FileDestructionStatus_Field, ExpectedText);

            return this;
        }

        public FileDestructionGDPRRecordPage ClickResponsibleTeamLookupButton()
        {
            this.Click(ResponsibleTeam_LookupButton);

            return this;
        }

        public FileDestructionGDPRRecordPage ValidateFirstApprovedByText(string ExpectedText)
        {
            ValidateElementText(FirstApprovedBy_FieldLink, ExpectedText);

            return this;
        }

        public FileDestructionGDPRRecordPage ValidateSecondApprovedByText(string ExpectedText)
        {
            ValidateElementText(SecondApprovedBy_FieldLink, ExpectedText);

            return this;
        }

        public FileDestructionGDPRRecordPage ValidateErrorText(string ExpectedText)
        {
            ValidateElementText(Error_Field, ExpectedText);

            return this;
        }



        public FileDestructionGDPRRecordPage ValidateNotificationAreaMessageVisible(string ExpectedMessage)
        {
            WaitForElementVisible(NotificationArea);
            ValidateElementText(NotificationArea, ExpectedMessage);

            return this;
        }

        public FileDestructionGDPRRecordPage RecordToBeDeletedAreaMessageVisible(string ExpectedMessage)
        {
            WaitForElementVisible(RecordToBeDeleted_MessageArea);
            ValidateElementText(RecordToBeDeleted_MessageArea, ExpectedMessage);

            return this;
        }

        public FileDestructionGDPRRecordPage SurrogateRecordAreaMessageVisible(string ExpectedMessage)
        {
            WaitForElementVisible(SurrogateRecord_MessageArea);
            ValidateElementText(SurrogateRecord_MessageArea, ExpectedMessage);

            return this;
        }

        public FileDestructionGDPRRecordPage ScheduleDateForDestructionDateAreaMessageVisible(string ExpectedMessage)
        {
            WaitForElementVisible(ScheduleDateForDestructionDate_MessageArea);
            ValidateElementText(ScheduleDateForDestructionDate_MessageArea, ExpectedMessage);

            return this;
        }

        public FileDestructionGDPRRecordPage ScheduleDateForDestructionTimeAreaMessageVisible(string ExpectedMessage)
        {
            WaitForElementVisible(ScheduleDateForDestructionTime_MessageArea);
            ValidateElementText(ScheduleDateForDestructionTime_MessageArea, ExpectedMessage);

            return this;
        }



        public FileDestructionGDPRRecordPage ClickSaveButton()
        {
            this.Click(saveButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FileDestructionGDPRRecordPage ClickSaveAndCloseButton()
        {
            this.Click(saveAndCloseButton);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public FileDestructionGDPRRecordPage ClickBackButton()
        {
            this.WaitForElement(backbutton);
            this.Click(backbutton);

            return this;
        }

        public FileDestructionGDPRRecordPage ClickFirstApprovedByButton()
        {
            this.WaitForElement(FirstApprovedByButton);
            this.Click(FirstApprovedByButton);

            return this;
        }

        public FileDestructionGDPRRecordPage ClickSecondApprovedByButton()
        {
            this.WaitForElement(SecondApprovedByButton);
            this.Click(SecondApprovedByButton);

            return this;
        }

    }
}
