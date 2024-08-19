
using OpenQA.Selenium;
using Phoenix.UITests.Framework.PageObjects.Cases.Health;
using Phoenix.UITests.Framework.PageObjects.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    /// <summary>
    /// Person Record - Care Plans Tab - Regular Care Tasks Sub Tab
    /// </summary>
    public class PersonCarePlansSubPage_RegularCareTasksTab_Record : CommonMethods
    {
        public PersonCarePlansSubPage_RegularCareTasksTab_Record(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWDialog = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=regularcaretask&')]"); //find the iframe that have the text 'iframe_CWDialog_' and whose src property contains the text 'type=case'
        readonly By iframe_DetailsDialog = By.XPath("//iframe[contains(@id,'mcc-iframe')][contains(@src,'type=regularcaretask&')]"); 

        readonly By CWSubTabsPanel_IFrame = By.Id("CWUrlPanel_IFrame");
        readonly By CarePlanType_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[2]/a[@title='Care Plan Type']");
        readonly By Person_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[3]/a[@title='Person']");
        readonly By StartDate_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[4]/a[@title='Start Date']");
        readonly By Status_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[5]/a[@title='Status']");
        readonly By ReviewDate_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[6]/a[@title='Review Date']");
        readonly By ReviewFrequency_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[7]/a[@title='Review Frequency']");
        readonly By ResponsibleTeam_Header = By.XPath("//table[@id='CWGridHeader']//tr/th[8]/a[@title='Responsible Team']");
        readonly By Person_FieldLabel = By.XPath("//li[@id='CWLabelHolder_personid']");
        readonly By CareTask_FieldLabel = By.XPath("//li[@id='CWLabelHolder_caretaskid']");
        readonly By LinkedAction_FieldLabel = By.XPath("//li[@id='CWLabelHolder_personcareplaninterventionid']");
        readonly By ResponsibleTeam_FieldLabel = By.XPath("//li[@id='CWLabelHolder_ownerid']");
        readonly By CarePlan_FieldLabel = By.XPath("//li[@id='CWLabelHolder_careplanid']");
        readonly By Inactive_No = By.XPath("//*[@id='CWField_inactive_0']");
        readonly By Inactive_Yes = By.XPath("//*[@id='CWField_inactive_1']");

        readonly By ActivateButton = By.XPath("//*[@id='TI_ActivateButton']");
        readonly By CareTaskLookup_Btn = By.XPath("//*[@id='CWLookupBtn_caretaskid']");
        readonly By SaveNCloseBtn = By.XPath("//button[@id='TI_SaveAndCloseButton']");
        readonly By notificationMessage = By.XPath("//div[@class='mcc-dialog__body']");
        readonly By CareSchedules_Header = By.XPath("//div/h1[text()='Care Schedules']");
        readonly By CareSchedules_Btn = By.XPath("//li[@id='CWNavGroup_Schedule']");
        readonly By Details_Btn = By.XPath("//li[@id='CWNavGroup_EditForm']");



        #region Top Menu

        readonly By newRecordButton = By.Id("TI_NewRecordButton");
        readonly By exportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By mailMergeButton = By.Id("TI_MailMergeButton");
        readonly By deleteButton = By.Id("TI_DeleteRecordButton");

        #endregion

        #region Records

        By record_CheckBox(string recordID) => By.XPath("//*[@id='CHK_" + recordID + "']");
        By recordRow(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/td[2]");
        By recordCell(string RecordID, int CellPosition) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[" + CellPosition + "]");


        #endregion




        public PersonCarePlansSubPage_RegularCareTasksTab_Record WaitForPersonCarePlansSubPage_RegularCareTasks_RecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record WaitForPersonCarePlansSubPage_Details_PageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(iframe_DetailsDialog);
            SwitchToIframe(iframe_DetailsDialog);

            return this;
        }

       

        public PersonCarePlansSubPage_RegularCareTasksTab_Record WaitForPersonCarePlansSubPage_Schedules_PageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWDialog);
            SwitchToIframe(iframe_CWDialog);

            WaitForElement(CWSubTabsPanel_IFrame);
            SwitchToIframe(CWSubTabsPanel_IFrame);

            return this;
        }



        public PersonCarePlansSubPage_RegularCareTasksTab_Record ValidatePersonLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Person_FieldLabel);
            }
            else
            {
                WaitForElementNotVisible(Person_FieldLabel, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ValidateCareTaskLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CareTask_FieldLabel);
            }
            else
            {
                WaitForElementNotVisible(CareTask_FieldLabel, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record VerifyCareScheduleTitle(bool ExpectVisible)
        {
            
            if (ExpectVisible)
            {
                ScrollToElement(CareSchedules_Header);
                WaitForElementVisible(CareSchedules_Header);
            }
            else
            {
                WaitForElementNotVisible(CareSchedules_Header, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ClickNewCareScheduleButton()
        {

            WaitForElementToBeClickable(newRecordButton);
            Click(newRecordButton);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ClickCareScheduleTab()
        {

            WaitForElementToBeClickable(CareSchedules_Btn);
            Click(CareSchedules_Btn);

            return this;
        }

       

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ClickDetailsTab()
        {

            WaitForElementToBeClickable(Details_Btn);
            Click(Details_Btn);

            return this;
        }


        public PersonCarePlansSubPage_RegularCareTasksTab_Record ValidateNewCareScheduleButton(bool ExpectVisible)
        {

            if (ExpectVisible)
            {
                WaitForElementVisible(newRecordButton);
            }
            else
            {
                WaitForElementNotVisible(newRecordButton, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ValidateLinkedActionLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(LinkedAction_FieldLabel);
            }
            else
            {
                WaitForElementNotVisible(LinkedAction_FieldLabel, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ValidateResponsibleTeamLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(ResponsibleTeam_FieldLabel);
            }
            else
            {
                WaitForElementNotVisible(ResponsibleTeam_FieldLabel, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ValidateCarePlanLabelVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(CarePlan_FieldLabel);
            }
            else
            {
                WaitForElementNotVisible(CarePlan_FieldLabel, 3);
            }

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ValidateInactive_NoRadioButtonChecked()
        {
            WaitForElement(Inactive_No);
            ValidateElementChecked(Inactive_No);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ClickInactive_YesRadioButton()
        {
            WaitForElement(Inactive_Yes);
            Click(Inactive_Yes);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ValidateActivateButtonVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(ActivateButton);
            }
            else
            {
                WaitForElementNotVisible(ActivateButton, 5);
            }
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ClickCareTaskLookUp()
        {
            WaitForElementVisible(CareTaskLookup_Btn);
            Click(CareTaskLookup_Btn);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ClickSaveNClose()
        {
            WaitForElementVisible(SaveNCloseBtn);
            Click(SaveNCloseBtn);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ClickActivateBtn()
        {
            WaitForElementVisible(ActivateButton);
            Click(ActivateButton);
            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ValidateMessageAreaText(String ExpectedText)
        {
            WaitForElementVisible(notificationMessage);
            ValidateElementText(notificationMessage, ExpectedText);

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ClickRecordCellText(string RecordID, int CellPosition)
        {
            WaitForElementToBeClickable(recordCell(RecordID, CellPosition));
            ScrollToElement(recordCell(RecordID, CellPosition));
            System.Threading.Thread.Sleep(2000);
            Click(recordCell(RecordID, CellPosition));

            return this;
        }

        public PersonCarePlansSubPage_RegularCareTasksTab_Record ClickDeleteRecord()
        {
            WaitForElementToBeClickable(deleteButton);
            ScrollToElement(deleteButton);
            Click(deleteButton);
            return this;
        }


    }
}
