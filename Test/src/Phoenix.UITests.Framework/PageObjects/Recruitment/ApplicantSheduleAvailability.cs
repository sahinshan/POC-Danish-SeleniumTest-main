using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Phoenix.UITests.Framework.WebAppAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ApplicantSheduleAvailability : CommonMethods
    {
        public ApplicantSheduleAvailability(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ApplicantRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicant&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By ApplicantsPageHeader = By.XPath("//*[@id='SA-controls']/div[1]/h1[text()='Schedule Availability']");
        readonly By refreshButton = By.Id("TI_RefreshButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By ScheduleAvailabiltyTab = By.XPath("//*[@id='SA-card-availability']");
        readonly By ScheduleTransportTab = By.XPath("//*[@id='SA-card-transport']");
        readonly By ViewDiaryOrManageAdhocTab = By.XPath("//*[@id='SA-card-diary']");

        readonly By ApplicantDashboard_Tab = By.Id("CWNavGroup_RoleApplicationsDashboardWidget");

        readonly By NoApplicationRecordErrorPopUp = By.XPath("//mcc-alert[text()='Current applicant does not have any applications.']");
        readonly By createFutureSchedule_Tab = By.XPath("//*[@id='SA-tab-future-schedule']/span");

        readonly By RoleApplicationTimeTable_Field = By.Id("dragContent");
        readonly By sheduleDate = By.XPath("//*[@id='SA-schedule-start']");
        readonly By RemoveTimeList = By.XPath("//div/button[text()='Remove time slot']");

        readonly By addWeek_Field = By.XPath("//*[@id='SA-main-header']/div/div/div[3]/nav/ul/li[2]/sa-sub-nav-link");

        readonly By exsistSheduleAvailability = By.XPath("//*[@class='ds-selected precedence-01']/span[1]");

        By weekDoggleButton_Field(int position) => By.XPath("//*[@id='SA-main-header']/div/div/div[3]/nav/ul/li[" + position + "]/span/span");

        By recordRow(string RecordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + RecordID + "']/td[2]");

        By ScheduleAvailabiltyDate(string Date) => By.XPath("//div[@attr-date='" + Date + "']/span/span/span[contains(text(),'HELPER')]");
        By ScheduleAvailabiltyDuplicate(string Date) => By.XPath("//div[@attr-date='" + Date + "']/span/span[3]");

        By SheduleAvailabilityType(int position) => By.XPath("//*[@id='modal-dialog']/div/dialog/section/div[2]/div/ul/a[" + position + "]");
        By SheduleAvailabilityType(string Text) => By.XPath("//*[@id='modal-dialog']/div/dialog/section/div[2]/div/ul/a[text()='"+ Text + "']");
        By WeekOptionType(int weekPosition, int position) => By.XPath("//*[@id='SA-main-header']/div/div/div[3]/nav/ul/li[" + weekPosition + "]/div/span[" + position + "]");

        By doubleWeekOption = By.XPath("//*[@id='SA-main-header']/div/div/div[3]/nav/ul/li[1]/div/span");

        By LeftDragButton_Monday(int Position) => By.XPath("//b[text()='Monday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Tuesday(int Position) => By.XPath("//b[text()='Tuesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Wednesday(int Position) => By.XPath("//b[text()='Wednesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Thursday(int Position) => By.XPath("//b[text()='Thursday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Friday(int Position) => By.XPath("//b[text()='Friday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Saturday(int Position) => By.XPath("//b[text()='Saturday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Sunday(int Position) => By.XPath("//b[text()='Sunday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");


        By LeftDragButtonAvailability(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]/span[contains(@class,'left drag precedence')]");
        By RightDragButtonAvailability(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]/span[contains(@class,'right drag precedence')]");

        public ApplicantSheduleAvailability WaitForApplicantSheduleAvailabilityPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(ApplicantRecordIFrame);
            SwitchToIframe(ApplicantRecordIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 45);

            return this;
        }

        public ApplicantSheduleAvailability ClickOnSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);            
            Click(SaveButton);

            return this;
        }
        public ApplicantSheduleAvailability ClickOnSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);
            return this;
        }
        public ApplicantSheduleAvailability ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }


        #region Top Tabs

        public ApplicantSheduleAvailability ClickScheduleAvailabiltyTab()
        {
            WaitForElement(ScheduleAvailabiltyTab);
            Click(ScheduleAvailabiltyTab);

            return this;
        }
        public ApplicantSheduleAvailability ValidateScheduleAvailabiltyTabVisibility(bool ExpectedVisible)
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

        public ApplicantSheduleAvailability ClickScheduleTransportTab(int timeout = 30)
        {
            WaitForElementNotVisible("CWRefreshPanel", timeout);
            WaitForElementToBeClickable(ScheduleTransportTab);
            Click(ScheduleTransportTab);

            return this;
        }
        public ApplicantSheduleAvailability ValidateScheduleTransportTabVisibility(bool ExpectedVisible)
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


        public ApplicantSheduleAvailability ValidateNoApplicationRecordPopUpVisibile(bool ExpectedText)
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

        public ApplicantSheduleAvailability ValidateCreateRoleApplicationTimeTable()
        {
            WaitForElementVisible(RoleApplicationTimeTable_Field);
            return this;
        }

        public ApplicantSheduleAvailability CreateScheduleAvailabiltyDate(string date)
        {
            WaitForElementToBeClickable(ScheduleAvailabiltyDate(date));
            Click(ScheduleAvailabiltyDate(date));
            return this;

        }

        public ApplicantSheduleAvailability CreateScheduleAvailabiltyDateDuplicate(string date)
        {
            WaitForElementToBeClickable(ScheduleAvailabiltyDuplicate(date));
            Click(ScheduleAvailabiltyDuplicate(date));
            return this;

        }

        public ApplicantSheduleAvailability ClickExsistScheduleAvailabilty()
        {
            WaitForElementToBeClickable(exsistSheduleAvailability);
            MoveToElementInPage(exsistSheduleAvailability);
            Click(exsistSheduleAvailability);
            return this;

        }

        public ApplicantSheduleAvailability SelectSheduleAvailabilityType(int position)
        {
            WaitForElement(SheduleAvailabilityType(position));
            Click(SheduleAvailabilityType(position));
            return this;
        }

      

        public ApplicantSheduleAvailability ClickAddWeek()
        {
            WaitForElementToBeClickable(addWeek_Field);
            Click(addWeek_Field);
            return this;
        }

        public ApplicantSheduleAvailability ClickWeekDoggleButton(int position)
        {
            WaitForElementToBeClickable(weekDoggleButton_Field(position));
            Click(weekDoggleButton_Field(position));
            return this;
        }

        public ApplicantSheduleAvailability ValidateDorpDownElementvisible(bool ExpectedText, int weekPosition, int position)
        {
            if (ExpectedText)
            {
                System.Threading.Thread.Sleep(3000);

                WaitForElementVisible(WeekOptionType(weekPosition, position));
            }
            else
            {
                WaitForElementNotVisible(WeekOptionType(weekPosition, position), 5);
            }
            return this;
        }
        
        public ApplicantSheduleAvailability ValidateWeek1CycleDateNonEditable()
        {
            WaitForElement(sheduleDate);
            ValidateElementDisabled(sheduleDate);

            return this;
        }



        public ApplicantSheduleAvailability ValidateWeek1CycleDateEditable()
        {
            WaitForElement(sheduleDate);
            ValidateElementEnabled(sheduleDate);

            return this;
        }

        public ApplicantSheduleAvailability ValidateCreateFeatureScheduleTap(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(createFutureSchedule_Tab);
            }
            else
            {
                WaitForElementNotVisible(createFutureSchedule_Tab, 5);
            }
            return this;
        }

        public ApplicantSheduleAvailability ValidateElmentDoubleWeekOption(bool ExpectedText)
        {
            if (ExpectedText)
            {
                WaitForElementVisible(doubleWeekOption);
            }
            else
            {
                WaitForElementNotVisible(doubleWeekOption, 5);
            }
            return this;
        }


        public ApplicantSheduleAvailability ClickViewDiaryOrManageAdhocTab()
        {
            WaitForElementToBeClickable(ViewDiaryOrManageAdhocTab);
            ScrollToElement(ViewDiaryOrManageAdhocTab);
            Click(ViewDiaryOrManageAdhocTab);

            return this;
        }

        public ApplicantSheduleAvailability ValidateViewDiaryOrManageAdhocTabVisibility(bool expectedvisible)
        {
            if (expectedvisible)
            {
                WaitForElementVisible(ViewDiaryOrManageAdhocTab);

            }

            else
            {
                WaitForElementNotVisible(ViewDiaryOrManageAdhocTab, 3);
            }


            return this;
        }

        public ApplicantSheduleAvailability DragAvailabilityToLeft(String Date,int FromPosition,int ToPosition)
        {
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var FromWebelement = GetWebElement(LeftDragButtonAvailability(Date, FromPosition));
            var ToWebelement = GetWebElement(RightDragButtonAvailability(Date, ToPosition));

            action.DragAndDrop(FromWebelement, ToWebelement).Build().Perform();

            return this;

          
        }


    }
}
