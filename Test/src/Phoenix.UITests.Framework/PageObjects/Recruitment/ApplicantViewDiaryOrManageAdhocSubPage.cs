using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ApplicantViewDiaryOrManageAdhoc : CommonMethods
    {
        public ApplicantViewDiaryOrManageAdhoc(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ApplicantRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicant&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By ApplicantsPageHeader = By.XPath("//*[@id='SA-controls']/div[1]/h1[text()='View Diary / Manage Ad Hoc']");
        readonly By refreshButton = By.Id("TI_RefreshButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By ScheduleAvailabiltyTab = By.XPath("//*[@id='SA-card-availability']");
        readonly By ScheduleTransportTab = By.XPath("//*[@id='SA-card-transport']");


        readonly By NoApplicationRecordErrorPopUp = By.XPath("//span[text='Current applicant does not have any applications.']");
        readonly By createFutureSchedule_Tab = By.XPath("//*[@id='SA-tab-future-schedule']/span");

        readonly By RoleApplicationTimeTable_Field = By.Id("dragContent");
        readonly By GoToDate_Title = By.XPath("//*[@id='SA-date-anchor']/label");
        readonly By GoToDate_Field = By.Id("SA-select-date");
        readonly By FilterAvailability_Title = By.XPath("//label[@for='SA-change-type']");
        readonly By FilterAvailability_Field = By.XPath("//label[@for='SA-change-type']/following-sibling::select[@id='SA-change-type']");
        readonly By FilterEmploymentContract_Title = By.XPath("//label[@for='SA-change-role']");
        readonly By FilterEmploymentContract_Field = By.XPath("//label[@for='SA-change-role']/following-sibling::select[@id='SA-change-role']");
        readonly By sheduleDate = By.XPath("//*[@id='SA-schedule-start']");
        readonly By RemoveTimeList = By.XPath("//div/button[text()='Remove time slot']");

        readonly By addWeek_Field = By.XPath("//*[@id='SA-main-header']/div/div/div[3]/nav/ul/li[2]/sa-sub-nav-link");

        readonly By exsistSheduleAvailability = By.XPath("//*[@class='ds-selected precedence-01']/span[1]");

        By weekDoggleButton_Field(int position) => By.XPath("//*[@id='SA-main-header']/div/div/div[3]/nav/ul/li[" + position + "]/span/span");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By ScheduleAvailabiltyDate(string Date) => By.XPath("//div[@attr-date='" + Date + "']/span/span/span[contains(text(),'HELPER')]");


        By ModifyScheduleAvailabiltyDate(string Date) => By.XPath("//div[@attr-date='" + Date + "']//*[@class='ds-selected precedence-01']/span[1]");

        By SheduleAvailabilityType(int position) => By.XPath("//*[@id='modal-dialog']/div/dialog/section/div[2]/div/ul/a[" + position + "]");
        By WeekOptionType(int weekPosition, int position) => By.XPath("//*[@id='SA-main-header']/div/div/div[3]/nav/ul/li[" + weekPosition + "]/div/span[" + position + "]");

        By doubleWeekOption = By.XPath("//*[@id='SA-main-header']/div/div/div[3]/nav/ul/li[1]/div/span");


        By clickToCreateTransportButton_Monday(int Position) => By.XPath("//b[text()='Monday']/parent::div/parent::span/parent::div/span[" + Position + "]");
        By clickToCreateTransportButton_Tuesday(int Position) => By.XPath("//b[text()='Tuesday']/parent::div/parent::span/parent::div/span[" + Position + "]");
        By clickToCreateTransportButton_Wednesday(int Position) => By.XPath("//b[text()='Wednesday']/parent::div/parent::span/parent::div/span[" + Position + "]");
        By clickToCreateTransportButton_Thursday(int Position) => By.XPath("//b[text()='Thursday']/parent::div/parent::span/parent::div/span[" + Position + "]");
        By clickToCreateTransportButton_Friday(int Position) => By.XPath("//b[text()='Friday']/parent::div/parent::span/parent::div/span[" + Position + "]");
        By clickToCreateTransportButton_Saturday(int Position) => By.XPath("//b[text()='Saturday']/parent::div/parent::span/parent::div/span[" + Position + "]");
        By clickToCreateTransportButton_Sunday(int Position) => By.XPath("//b[text()='Sunday']/parent::div/parent::span/parent::div/span[" + Position + "]");


        By clickToCreateOptionButton_Date(string DayOfWeek) => By.XPath("//div[@attr-date='" + DayOfWeek + "']/span/span[2]/child::span");
        By clickToDragOptionButton_Date(string DayOfWeek) => By.XPath("//div[@attr-date='" + DayOfWeek + "']/span/span[2]/child::div[3]/following-sibling::span[2]");

        By clickToCreateTransportOptionButton_Date(string DayOfWeek) => By.XPath("//div[@attr-date='" + DayOfWeek + "']/span");
        By clickToModifyTransportOptionButton_Date(string DayOfWeek) => By.XPath("//div[@attr-date='" + DayOfWeek + "']/span/span[2]");

        By clickToDragTransportOptionButton_Date(string DayOfWeek) => By.XPath("//div[@attr-date='" + DayOfWeek + "']/span[1]/span[2]/child::div[2]/following-sibling::span[2]");

        By transportRecordLeftSideDragButton(string Date, int RecordPosition) => By.XPath("//div[@attr-date='"+ Date + "']//span[@class='ds-selected transport'][" + RecordPosition + "]//div[@class='touchBars left']");
        By availabilityRecordLeftSideDragButton(string Date, int RecordPosition) => By.XPath("//div[@attr-date='"+ Date + "']//span[@class='ds-selected precedence-01'][" + RecordPosition + "]//span[@class='left drag precedence-01']");



        readonly By Availability_Tab = By.XPath("//*[@id='CWNavGroup_WorkScheduleAdvanced']/a");

        By ScheduleAvailabilitySlotText(string DateOfMonth, string name) => By.XPath("//div[@attr-date='" + DateOfMonth + "']//div[@class='roleName' and text()='" + name + "']");
        By clickToCreateTransportAvailabilityButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]");
        By clickToCreateScheduleAvailabilityButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span[2]/span[" + Position + "]");

        public ApplicantViewDiaryOrManageAdhoc WaitForApplicantSheduleViewOrAdhocSubPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ApplicantRecordIFrame);
            SwitchToIframe(ApplicantRecordIFrame);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElementVisible(ApplicantsPageHeader);

            WaitForElement(RoleApplicationTimeTable_Field);
            WaitForElement(FilterEmploymentContract_Title);
            WaitForElement(FilterEmploymentContract_Field);
            WaitForElement(FilterAvailability_Title);
            WaitForElement(FilterAvailability_Field);
            WaitForElement(GoToDate_Title);
            WaitForElement(GoToDate_Field);

            WaitForElementVisible(clickToCreateTransportButton_Monday(1));
            WaitForElementVisible(clickToCreateTransportButton_Tuesday(1));
            WaitForElementVisible(clickToCreateTransportButton_Wednesday(1));
            WaitForElementVisible(clickToCreateTransportButton_Thursday(1));
            WaitForElementVisible(clickToCreateTransportButton_Friday(1));
            WaitForElementVisible(clickToCreateTransportButton_Saturday(1));
            WaitForElementVisible(clickToCreateTransportButton_Sunday(1));


            return this;
        }
        public ApplicantViewDiaryOrManageAdhoc WaitForApplicantSheduleViewOrAdhocSubPageToLoadWhenNoRoleIsAdded()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(ApplicantRecordIFrame);
            SwitchToIframe(ApplicantRecordIFrame);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElementVisible(ApplicantsPageHeader);


            WaitForElementVisible(clickToCreateTransportButton_Monday(1));
            WaitForElementVisible(clickToCreateTransportButton_Tuesday(1));
            WaitForElementVisible(clickToCreateTransportButton_Wednesday(1));
            WaitForElementVisible(clickToCreateTransportButton_Thursday(1));
            WaitForElementVisible(clickToCreateTransportButton_Friday(1));
            WaitForElementVisible(clickToCreateTransportButton_Saturday(1));
            WaitForElementVisible(clickToCreateTransportButton_Sunday(1));


            return this;
        }


        public ApplicantViewDiaryOrManageAdhoc ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }


        #region Top Tabs

        public ApplicantViewDiaryOrManageAdhoc ClickScheduleAvailabiltyTab()
        {
            WaitForElement(ScheduleAvailabiltyTab);
            Click(ScheduleAvailabiltyTab);

            return this;
        }
        public ApplicantViewDiaryOrManageAdhoc ValidateScheduleAvailabiltyTabVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(ScheduleAvailabiltyTab);
            }
            else
            {
                WaitForElementNotVisible(ScheduleAvailabiltyTab, 5);
            }
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ClickScheduleTransportTab()
        {
            WaitForElement(ScheduleTransportTab);
            Click(ScheduleTransportTab);

            return this;
        }
        public ApplicantViewDiaryOrManageAdhoc ValidateScheduleTransportTabVisibility(bool ExpectedVisible)
        {
            if (ExpectedVisible)
            {
                WaitForElementVisible(ScheduleTransportTab);
            }
            else
            {
                WaitForElementNotVisible(ScheduleTransportTab, 5);
            }
            return this;
        }

        #endregion

        public ApplicantViewDiaryOrManageAdhoc InsertGoToDate(string ValueToInsert)
        {
            WaitForElementToBeClickable(GoToDate_Field);
            Click(GoToDate_Field);
            ClearInputElementViaJavascript("SA-select-date");
            SendKeysViaJavascript("SA-select-date", ValueToInsert);
            SendKeysWithoutClearing(GoToDate_Field, Keys.Tab);

            return this;

        }


        public ApplicantViewDiaryOrManageAdhoc ValidateNoApplicationRecordPopUpVisibile(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(NoApplicationRecordErrorPopUp);
            }
            else
            {
                WaitForElementNotVisible(NoApplicationRecordErrorPopUp, 5);
            }
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ValidateCreateRoleApplicationTimeTable()
        {
            WaitForElementVisible(RoleApplicationTimeTable_Field);
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc CreateScheduleAvailabiltyDate(string date)
        {
            WaitForElementToBeClickable(ScheduleAvailabiltyDate(date));
            Click(ScheduleAvailabiltyDate(date));
            return this;

        }

        public ApplicantViewDiaryOrManageAdhoc ClickModifyScheduleAvailabiltyDate(string date)
        {
            WaitForElementToBeClickable(ModifyScheduleAvailabiltyDate(date));
            Click(ModifyScheduleAvailabiltyDate(date));
            return this;

        }

        public ApplicantViewDiaryOrManageAdhoc ClickExsistScheduleAvailabilty()
        {
            WaitForElementToBeClickable(exsistSheduleAvailability);
            Click(exsistSheduleAvailability);
            return this;

        }

        public ApplicantViewDiaryOrManageAdhoc SelectSheduleAvailabilityType(int position)
        {
            WaitForElement(SheduleAvailabilityType(position));
            Click(SheduleAvailabilityType(position));
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ClickAddWeek()
        {
            WaitForElementToBeClickable(addWeek_Field);
            Click(addWeek_Field);
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ClickWeekDoggleButton(int position)
        {
            WaitForElementToBeClickable(weekDoggleButton_Field(position));
            Click(weekDoggleButton_Field(position));
            return this;
        }




        public ApplicantViewDiaryOrManageAdhoc ClickOnRemoveTimeList()
        {
            WaitForElement(RemoveTimeList);
            Click(RemoveTimeList);
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ClickCreateOptionButton()
        {
            WaitForElementToBeClickable(clickToCreateTransportButton_Monday(2));
            Click(clickToCreateTransportButton_Monday(2));
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ClickCreateOptionBasedOnDate(string Date)
        {
            WaitForElementToBeClickable(clickToCreateOptionButton_Date(Date));
            Click(clickToCreateOptionButton_Date(Date));
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ClickCreateAdhocTransportAvailabilityButton(string Date, int Position)
        {
            bool isVisible = GetElementVisibility(clickToCreateTransportAvailabilityButton(Date, Position));
            if (isVisible == false)
                InsertGoToDate(Date);

            WaitForElementToBeClickable(clickToCreateTransportAvailabilityButton(Date, Position));
            MoveToElementInPage(clickToCreateTransportAvailabilityButton(Date, Position));
            Click(clickToCreateTransportAvailabilityButton(Date, Position));
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ClickCreateAdhocScheduleAvailabilityButton(string Date, int Position)
        {
            bool isVisible = GetElementVisibility(clickToCreateScheduleAvailabilityButton(Date, Position));
            if (isVisible == false)
                InsertGoToDate(Date);

            WaitForElementToBeClickable(clickToCreateScheduleAvailabilityButton(Date, Position));
            MoveToElementInPage(clickToCreateScheduleAvailabilityButton(Date, Position));
            Click(clickToCreateScheduleAvailabilityButton(Date, Position));
            return this;
        }


        public ApplicantViewDiaryOrManageAdhoc ClickCreateTransportOptionBasedOnDate(string Date)
        {
            WaitForElementToBeClickable(clickToCreateTransportOptionButton_Date(Date));
            Click(clickToCreateTransportOptionButton_Date(Date));
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ClickModifyTransportOptionBasedOnDate(string Date)
        {
            WaitForElementToBeClickable(clickToModifyTransportOptionButton_Date(Date));
            Click(clickToModifyTransportOptionButton_Date(Date));
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc DragTransportAvailabilityToLeft_Monday(string Date)
        {
            WaitForElementToBeClickable(clickToDragOptionButton_Date(Date));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(clickToDragOptionButton_Date(Date));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc DragTransportAvailabilityToLeft_TransportType(string Date)
        {
            WaitForElementToBeClickable(clickToDragTransportOptionButton_Date(Date));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(clickToDragTransportOptionButton_Date(Date));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }



        public ApplicantViewDiaryOrManageAdhoc DragExpandOptionBasedOnDate_ToLeft(string Date)
        {
            DragTransportAvailabilityToLeft_Monday(Date);
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc DragTransportExpandOptionBasedOnDate_ToLeft(string Date)
        {
            DragTransportAvailabilityToLeft_TransportType(Date);
            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc DragTransportRecordLeftSide_ToLeft(string Date, int RecordPosition, int DragOffset = -20)
        {
            WaitForElementToBeClickable(transportRecordLeftSideDragButton(Date, RecordPosition));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(transportRecordLeftSideDragButton(Date, RecordPosition));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc DragAvailabilityRecordLeftSide_ToLeft(string Date, int RecordPosition, int DragOffset = -20)
        {
            WaitForElementToBeClickable(availabilityRecordLeftSideDragButton(Date, RecordPosition));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(availabilityRecordLeftSideDragButton(Date, RecordPosition));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ValidateOptionCreated(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(clickToCreateTransportButton_Monday(2));
            }
            else
            {
                WaitForElementNotVisible(clickToCreateTransportButton_Monday(2), 5);
            }
            return this;
        }


        public ApplicantViewDiaryOrManageAdhoc ValidateCreatedAdhocAvailabilitySlotIsVisible(string date, string expectedText, bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(ScheduleAvailabilitySlotText(date, expectedText));
            else
                WaitForElementNotVisible(ScheduleAvailabilitySlotText(date, expectedText), 5);

            return this;
        }

        public ApplicantViewDiaryOrManageAdhoc ClickCreateAdhocOptionButton(string DayOfTheWeek)
        {
            switch (DayOfTheWeek)
            {
                case "Monday":
                    WaitForElementToBeClickable(clickToCreateTransportButton_Tuesday(2));
                    Click(clickToCreateTransportButton_Monday(2));
                    break;
                case "Tuesday":
                    WaitForElementToBeClickable(clickToCreateTransportButton_Tuesday(2));
                    Click(clickToCreateTransportButton_Tuesday(2));
                    break;
                case "Wednesday":
                    WaitForElementToBeClickable(clickToCreateTransportButton_Tuesday(2));
                    Click(clickToCreateTransportButton_Wednesday(2));
                    break;
                case "Thursday":
                    WaitForElementToBeClickable(clickToCreateTransportButton_Tuesday(2));
                    Click(clickToCreateTransportButton_Thursday(2));
                    break;
                case "Friday":
                    WaitForElementToBeClickable(clickToCreateTransportButton_Tuesday(2));
                    Click(clickToCreateTransportButton_Friday(2));
                    break;
                case "Saturday":
                    WaitForElementToBeClickable(clickToCreateTransportButton_Tuesday(2));
                    Click(clickToCreateTransportButton_Saturday(2));
                    break;
                case "Sunday":
                    WaitForElementToBeClickable(clickToCreateTransportButton_Tuesday(2));
                    Click(clickToCreateTransportButton_Sunday(2));
                    break;
                default:
                    break;


            }
            return this;
        }
    }

}
