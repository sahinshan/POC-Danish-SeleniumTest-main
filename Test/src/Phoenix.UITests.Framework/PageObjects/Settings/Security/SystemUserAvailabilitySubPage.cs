using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Security
{
    public class SystemUserAvailabilitySubPage : CommonMethods
    {
        public SystemUserAvailabilitySubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By SystemUserIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By ScheduleTransportCard = By.XPath("//div[@id = 'SA-card-transport']/div/h3");
        readonly By ScheduleAvailabilityCard = By.XPath("//div[@id = 'SA-card-availability']/div/h3");
        readonly By ViewDiary_ManageAdHocCard = By.XPath("//div[@id = 'SA-card-diary']/div/h3");

        readonly By createFutureSchedule_Tab = By.XPath("//*[@id='SA-tab-future-schedule']/span");


        readonly By infoHeaderArea = By.XPath("//*[@id='SA-main-content']/h3[text()='Info']");
        readonly By infoMessageArea = By.XPath("//*[@id='SA-main-content']/div");


        readonly By refreshButton = By.Id("TI_RefreshButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By BackButton = By.XPath("//button[@title = 'Back']");

        readonly By AvailabilityTimeTable_Field = By.Id("dragContent");
        readonly By Week1CycleDate_Title = By.XPath("//*[@id='SA-date-anchor']/label");
        readonly By Week1CycleDate_Field = By.XPath("//*[@id='SA-schedule-start']");
        readonly By Week1CycleDate_DatePicker = By.XPath("//*[@id='SA-schedule-start-date-picker']");
        By Week1CycleDate_CalendarDisabledPickerDate(string Date) => By.XPath("//span[@aria-label='" + Date + "'][contains(@class, 'disabled')]");

        readonly By CurrentSchedule = By.Id("SA-tab-current-schedule");

        By ScheduleTransport_SelectedTransportSlot(string DateOfMonth, string selectedTransport) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/*[@name='" + selectedTransport + "']/parent::span");
        By ScheduleTransport_SelectedTransportSlot(string DateOfMonth, string selectedTransport, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span["+ Position +"]/*[@name='" + selectedTransport + "']/parent::span");

        readonly By TooltipMessage = By.XPath("//*[@class='dragTooltip']/span[@class='tooltipText']");

        readonly By addWeekButton = By.XPath("//sa-sub-nav-link[@title='Create a new empty week']");
        By weekButton(int WeekNumber) => By.XPath("//*[@id='SA-main-header']/*/*/*/*/*/*/span[@title='View week " + WeekNumber + "']");
        By weekExpandButton(int WeekNumber) => By.XPath("//*[@id='SA-main-header']/*/*/*/*/*/*/span[@title='View week " + WeekNumber + "']/span");

        By removeWeekButton(int WeekNumber) => By.XPath("//span[@title='Remove week " + WeekNumber + "']");
        By duplicateWeekButton(int WeekNumber) => By.XPath("//span[contains(@title, 'Duplicate week " + WeekNumber + "')]");
        By moveWeekRightButton(int WeekNumber) => By.XPath("//span[contains(@title, 'Move week " + WeekNumber + " to after week')]");
        By moveWeekLeftButton(int WeekNumber) => By.XPath("//span[contains(@title, 'Move week " + WeekNumber + " to before week')]");

        By clickToCreateScheduleButton_Monday(int Position) => By.XPath("//b[text()='Monday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");

        By clickToCreateScheduleButton_Tuesday(int Position) => By.XPath("//b[text()='Tuesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");

        By clickToCreateScheduleButton_Wednesday(int Position) => By.XPath("//b[text()='Wednesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");

        By clickToCreateScheduleButton_Thursday(int Position) => By.XPath("//b[text()='Thursday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");

        By clickToCreateScheduleButton_Friday(int Position) => By.XPath("//b[text()='Friday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");
        By clickToCreateScheduleButton_Saturday(int Position) => By.XPath("//b[text()='Saturday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");
        By clickToCreateScheduleButton_Sunday(int Position) => By.XPath("//b[text()='Sunday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");

        By LeftDragButton_Monday(int Position) => By.XPath("//b[text()='Monday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Tuesday(int Position) => By.XPath("//b[text()='Tuesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Wednesday(int Position) => By.XPath("//b[text()='Wednesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Thursday(int Position) => By.XPath("//b[text()='Thursday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Friday(int Position) => By.XPath("//b[text()='Friday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Saturday(int Position) => By.XPath("//b[text()='Saturday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Sunday(int Position) => By.XPath("//b[text()='Sunday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");

        By RightDragButton_Monday(int Position) => By.XPath("//b[text()='Monday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars right']");
        By RightDragButton_Tuesday(int Position) => By.XPath("//b[text()='Tuesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars right']");
        By RightDragButton_Wednesday(int Position) => By.XPath("//b[text()='Wednesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars right']");
        By RightDragButton_Thursday(int Position) => By.XPath("//b[text()='Thursday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars right']");
        By RightDragButton_Friday(int Position) => By.XPath("//b[text()='Friday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars right']");
        By RightDragButton_Saturday(int Position) => By.XPath("//b[text()='Saturday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars right']");
        By RightDragButton_Sunday(int Position) => By.XPath("//b[text()='Sunday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars right']");

        By GenericLeftDragButton(string date, string availabilityName, int availabilityPosition) => By.XPath("//div[@attr-date='" + date + "']//*[text()='" + availabilityName + "']/parent::span/parent::span/span[" + availabilityPosition + "]/*[@class='touchBars left']");
        By GenericRightDragButton(string date, string availabilityName, int availabilityPosition) => By.XPath("//div[@attr-date='" + date + "']//*[text()='" + availabilityName + "']/parent::span/parent::span/span[" + availabilityPosition + "]/*[@class='touchBars right']");


        readonly By snackbarWarning_Message = By.XPath("//*[@class = 'cd-snackbar-container']");
        readonly By deleteSign = By.XPath("//*[@class = 'cd-snackbar-container']//*[@name='delete_sign']");
        readonly By infoSign = By.XPath("//*[@class = 'cd-snackbar-container']//*[@name='info_popup']");

        By clickToCreateContractButton_Monday(string contractTitle) => By.XPath("//b[text() = 'Monday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Tuesday(string contractTitle) => By.XPath("//b[text() = 'Tuesday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Wednesday(string contractTitle) => By.XPath("//b[text() = 'Wednesday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Thursday(string contractTitle) => By.XPath("//b[text() = 'Thursday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Friday(string contractTitle) => By.XPath("//b[text() = 'Friday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Saturday(string contractTitle) => By.XPath("//b[text() = 'Saturday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Sunday(string contractTitle) => By.XPath("//b[text() = 'Sunday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");

        By clickToCreateScheduleTransportAvailabilityButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]");

        By clickToCreateScheduleAvailabilityButton(string DateOfMonth, int Position, string name) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]//*[text() = \"Click to create '" + name + "' contract availability\"]");

        By clickToCreateScheduleAvailabilityButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]");

        By CreatedAvailabilitySlot(string dateValue, string contractTitle) => By.XPath("//div[@attr-date = '" + dateValue + "']/span/span[contains(@class, 'ds-selected precedence')][1]/div[text() = \"" + contractTitle + "\"]");

        By CreatedAvailabilitySlotClickHolder(string dateValue, string name) => By.XPath("//div[@attr-date = '" + dateValue + "']/span/span[contains(@class, 'ds-selected precedence')][1]/div[text() = \"" + name + "\"]/following-sibling::span[@class = 'clickHolder']");

        By ScheduleTransportAvailableTransportSlot(string DateOfMonth, string selectedTransport) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/*[@name='" + selectedTransport + "']");
        By ScheduleTransportAvailableTransportSlot(string DateOfMonth, string selectedTransport, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span["+ Position + "]/*[@name='" + selectedTransport + "']");

        By RightDragButtonFromSlot(string DateOfMonth, string selectedTransport) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/*[@name='" + selectedTransport + "']/parent::span/span[@class='right drag transport']");

        By LeftDragButtonFromSlot(string DateOfMonth, string selectedTransport) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/*[@name='" + selectedTransport + "']/parent::span/span[@class='left drag transport']");

        By LeftDragButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]/span[@class='left drag transport']");
        By RightDragButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]/span[@class='right drag transport']");


        By ScheduleTransportSelectedSlot(string DateOfMonth, string selectedTransport) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/*[@name='" + selectedTransport + "']/parent::span");

        By clickToCreatedScheduledAvailabilityButton(string DateOfMonth, string contractName) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/div[@class='roleName'][text()='" + contractName + "']/parent::span");

        By ScheduleTransportDayOfTheWeekArea(string DateOfMonth) => By.XPath("//div[@attr-date='" + DateOfMonth + "']");

        By ScheduleAvailabilitySlotText(string DateOfMonth, string name) => By.XPath("//div[@attr-date='" + DateOfMonth + "']//div[@class='roleName' and text()='" + name + "']");
        By ScheduleAvailabilitySlotColor(string DateOfMonth,string name,string number) => By.XPath("//div[@attr-date='" + DateOfMonth + "']//span[contains(@class,'ds-selected precedence-" + number + "')]//div[@class='roleName' and text()='" + name + "']");

        By ScheduleAvailabilityDateLabel(string DateOfMonth) => By.XPath("//div[@attr-date='" + DateOfMonth + "']//div[contains(text(),'" + DateOfMonth + "')]");
        By ScheduleAvailabilityDayLabel(string DayOfWeek) => By.XPath("//span[@class='attributes']//div/b[contains(text(),'"+ DayOfWeek + "')]");

        By CurrentSelectedWeek(int weekNmmber) => By.XPath("//li[@class='SA-sub-nav-tab current']/span[@class='SA-sub-nav-link' and @title='View week "+ weekNmmber + "']");

        readonly By toolTip = By.XPath("//span[@class='dragTooltip']/span[@class='tooltipText']");

        By ScheduleAvailabilityLeftDragButton(string DayOfWeek, int Position) => By.XPath("//b[text()='" + DayOfWeek + "']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By ScheduleAvailabilityRightDragButton(string DayOfWeek, int Position) => By.XPath("//b[text()='" + DayOfWeek + "']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars right']");

        public SystemUserAvailabilitySubPage WaitForSystemUserAvailabilitySubPageToLoad(bool AddWeekButton = true)
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(SystemUserIFrame);
            SwitchToIframe(SystemUserIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(AvailabilityTimeTable_Field);
            WaitForElement(Week1CycleDate_Title);
            WaitForElement(Week1CycleDate_Field);

            if (AddWeekButton)
                WaitForElementVisible(addWeekButton);

            WaitForElementVisible(weekButton(1));
            WaitForElementVisible(weekExpandButton(1));

            WaitForElementVisible(clickToCreateScheduleButton_Monday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Tuesday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Wednesday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Thursday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Friday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Saturday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Sunday(1));

            return this;
        }

        /// <summary>
        /// used this method when we need to wait for the Availability Tab to load with the message "ou have already selected that you always have access to the transportation type: XXXXXXXX" visible
        /// </summary>
        /// <returns></returns>
        public SystemUserAvailabilitySubPage WaitForSystemUserAvailabilitySubPageToLoadWithInfoSectionVisible()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SystemUserIFrame);
            SwitchToIframe(SystemUserIFrame);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElementVisible(infoHeaderArea);
            WaitForElementVisible(infoMessageArea);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickOnSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickOnSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickOnBackButton()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(SystemUserIFrame);
            SwitchToIframe(SystemUserIFrame);

            WaitForElement(BackButton);
            ScrollToElement(BackButton);
            Click(BackButton);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickRefreshButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 20);
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);
            WaitForElementNotVisible("CWRefreshPanel", 20);
            System.Threading.Thread.Sleep(1000);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickScheduleAvailabilityCard()
        {
            WaitForElementToBeClickable(ScheduleAvailabilityCard);
            ScrollToElement(ScheduleAvailabilityCard);
            Click(ScheduleAvailabilityCard);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickAddWeekButton()
        {
            WaitForElementToBeClickable(addWeekButton);
            Click(addWeekButton);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickWeekButton(int WeekNumber)
        {
            WaitForElementToBeClickable(weekButton(WeekNumber));
            Click(weekButton(WeekNumber));

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateWeekButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(weekButton(WeekNumber));
            else
                WaitForElementNotVisible(weekButton(WeekNumber), 3);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickWeekExpandButton(int WeekNumber)
        {
            ScrollToElement(weekExpandButton(WeekNumber));
            WaitForElementToBeClickable(weekExpandButton(WeekNumber));
            Click(weekExpandButton(WeekNumber));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickRemoveWeekButton(int WeekNumber)
        {
            ScrollToElement(removeWeekButton(WeekNumber));
            WaitForElementToBeClickable(removeWeekButton(WeekNumber));
            Click(removeWeekButton(WeekNumber));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickDuplicateWeekButton(int WeekNumber)
        {
            WaitForElementToBeClickable(duplicateWeekButton(WeekNumber));
            Click(duplicateWeekButton(WeekNumber));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickMoveWeekRightButton(int WeekNumber)
        {
            WaitForElementToBeClickable(moveWeekRightButton(WeekNumber));
            Click(moveWeekRightButton(WeekNumber));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickMoveWeekLeftButton(int WeekNumber)
        {
            WaitForElementToBeClickable(moveWeekLeftButton(WeekNumber));
            Click(moveWeekLeftButton(WeekNumber));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButton(string DayOfTheWeek, int Position)
        {
            switch (DayOfTheWeek)
            {
                case "Monday":
                    ClickCreateScheduleAvailabilityButton_Monday(Position);
                    break;
                case "Tuesday":
                    ClickCreateScheduleAvailabilityButton_Tuesday(Position);
                    break;
                case "Wednesday":
                    ClickCreateScheduleAvailabilityButton_Wednesday(Position);
                    break;
                case "Thursday":
                    ClickCreateScheduleAvailabilityButton_Thursday(Position);
                    break;
                case "Friday":
                    ClickCreateScheduleAvailabilityButton_Friday(Position);
                    break;
                case "Saturday":
                    ClickCreateScheduleAvailabilityButton_Saturday(Position);
                    break;
                case "Sunday":
                    ClickCreateScheduleAvailabilityButton_Sunday(Position);
                    break;
                default:
                    break;
            }

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButton_Monday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Monday(Position));
            Click(clickToCreateScheduleButton_Monday(Position));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButton_Tuesday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Tuesday(Position));
            //ScrollToElement(clickToCreateScheduleButton_Tuesday(Position));
            Click(clickToCreateScheduleButton_Tuesday(Position));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButton_Wednesday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Wednesday(Position));
            //ScrollToElement(clickToCreateScheduleButton_Wednesday(Position));
            Click(clickToCreateScheduleButton_Wednesday(Position));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButton_Thursday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Thursday(Position));
            //ScrollToElement(clickToCreateScheduleButton_Thursday(Position));
            Click(clickToCreateScheduleButton_Thursday(Position));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButton_Friday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Friday(Position));
            //ScrollToElement(clickToCreateScheduleButton_Friday(Position));
            Click(clickToCreateScheduleButton_Friday(Position));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButton_Saturday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Saturday(Position));
            //ScrollToElement(clickToCreateScheduleButton_Saturday(Position));
            Click(clickToCreateScheduleButton_Saturday(Position));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButton_Sunday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Sunday(Position));
            //ScrollToElement(clickToCreateScheduleButton_Sunday(Position));
            Click(clickToCreateScheduleButton_Sunday(Position));

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreateScheduleAvailabilityButton_Monday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Monday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Monday(name));
            Assert.IsTrue(isVisible);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreateScheduleAvailabilityButton_Tuesday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Tuesday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Tuesday(name));
            Assert.IsTrue(isVisible);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreateScheduleAvailabilityButton_Wednesday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Wednesday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Wednesday(name));
            Assert.IsTrue(isVisible);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreateScheduleAvailabilityButton_Thursday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Thursday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Thursday(name));
            Assert.IsTrue(isVisible);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreateScheduleAvailabilityButton_Friday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Friday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Friday(name));
            Assert.IsTrue(isVisible);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreateScheduleAvailabilityButton_Saturday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Saturday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Saturday(name));
            Assert.IsTrue(isVisible);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreateScheduleAvailabilityButton_Sunday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Sunday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Sunday(name));
            Assert.IsTrue(isVisible);

            return this;
        }

        public SystemUserAvailabilitySubPage InsertWeek1CycleDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Week1CycleDate_Field);
            Click(Week1CycleDate_Field);
            ClearInputElementViaJavascript("SA-schedule-start");
            SendKeys(Week1CycleDate_Field, TextToInsert + Keys.Tab);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickWeek1CycleDateDatePicker()
        {
            WaitForElementToBeClickable(Week1CycleDate_DatePicker);
            Click(Week1CycleDate_DatePicker);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateWeek1CycleDateCalendarDateDisabled(string Date)
        {
            WaitForElement(Week1CycleDate_CalendarDisabledPickerDate(Date));

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateRemoveWeekButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(removeWeekButton(WeekNumber));
            else
                WaitForElementNotVisible(removeWeekButton(WeekNumber), 2);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateDuplicateWeekButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(duplicateWeekButton(WeekNumber));
            else
                WaitForElementNotVisible(duplicateWeekButton(WeekNumber), 2);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateMoveWeekRightButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(moveWeekRightButton(WeekNumber));
            else
                WaitForElementNotVisible(moveWeekRightButton(WeekNumber), 2);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateMoveWeekLeftButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(moveWeekLeftButton(WeekNumber));
            else
                WaitForElementNotVisible(moveWeekLeftButton(WeekNumber), 2);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateAddWeekButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(addWeekButton);
            else
                WaitForElementNotVisible(addWeekButton, 2);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateWeek1CycleDateIsDisplayed(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(Week1CycleDate_Title);
                WaitForElementVisible(Week1CycleDate_Field);
            }
            else
            {
                WaitForElementNotVisible(Week1CycleDate_Title, 3);
                WaitForElementNotVisible(Week1CycleDate_Field, 3);
            }

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateWeek1CycleDate(string ExpectedText)
        {
            ValidateElementValueByJavascript("SA-schedule-start", ExpectedText);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateWeek1CycleDateReadonly(bool ExpectReadonly)
        {
            WaitForElement(Week1CycleDate_Field);
            ScrollToElement(Week1CycleDate_Field);

            if (ExpectReadonly)
                ValidateElementReadonly(Week1CycleDate_Field);
            else
                ValidateElementNotReadonly(Week1CycleDate_Field);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateWeek1CycleDateFieldTitle(string ExpectedText)
        {
            WaitForElement(Week1CycleDate_Title);
            ScrollToElement(Week1CycleDate_Title);
            string ActualText = GetElementTextByJavascript(Week1CycleDate_Title);
            Assert.AreEqual(ExpectedText, ActualText);
            
            return this;
        }            

        public SystemUserAvailabilitySubPage DragAvailabilityRecordLeftArea(string date, string availabilityName, int availabilityPosition, int DragOffset)
        {
            WaitForElementToBeClickable(GenericLeftDragButton(date, availabilityName, availabilityPosition));
            ScrollToElement(GenericLeftDragButton(date, availabilityName, availabilityPosition));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(GenericLeftDragButton(date, availabilityName, availabilityPosition));
            action.DragAndDropToOffset(webelement, DragOffset, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragAvailabilityRecordRightArea(string date, string availabilityName, int availabilityPosition, int DragOffset)
        {
            WaitForElementToBeClickable(GenericRightDragButton(date, availabilityName, availabilityPosition));
            ScrollToElement(GenericRightDragButton(date, availabilityName, availabilityPosition));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(GenericRightDragButton(date, availabilityName, availabilityPosition));
            action.DragAndDropToOffset(webelement, DragOffset, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateWeek1CycleDatePickerDisabled(bool ExpectedDisabled)
        {
            WaitForElement(Week1CycleDate_DatePicker);
            ScrollToElement(Week1CycleDate_DatePicker);

            if (ExpectedDisabled)
            {
                ValidateElementDisabled(Week1CycleDate_DatePicker);
            }
            else
            {
                ValidateElementEnabled(Week1CycleDate_DatePicker);
            }
            return this;
        }

        public SystemUserAvailabilitySubPage DragTransportAvailabilityToLeft(string DayOfTheWeek, int Position)
        {
            switch (DayOfTheWeek)
            {
                case "Monday":
                    DragScheduleAvailabilityToLeft_Monday(Position);
                    break;
                case "Tuesday":
                    DragScheduleAvailabilityToLeft_Tuesday(Position);
                    break;
                case "Wednesday":
                    DragScheduleAvailabilityToLeft_Wednesday(Position);
                    break;
                case "Thursday":
                    DragScheduleAvailabilityToLeft_Thursday(Position);
                    break;
                case "Friday":
                    DragScheduleAvailabilityToLeft_Friday(Position);
                    break;
                case "Saturday":
                    DragScheduleAvailabilityToLeft_Saturday(Position);
                    break;
                case "Sunday":
                    DragScheduleAvailabilityToLeft_Sunday(Position);
                    break;
                default:
                    break;
            }

            return this;
        }

        public SystemUserAvailabilitySubPage DragAvailabilityToLeft(string DayOfTheWeek, int Position)
        {
            switch (DayOfTheWeek)
            {
                case "Monday":
                    DragScheduleAvailabilityToLeft_Monday(Position);
                    break;
                case "Tuesday":
                    DragScheduleAvailabilityToLeft_Tuesday(Position);
                    break;
                case "Wednesday":
                    DragScheduleAvailabilityToLeft_Wednesday(Position);
                    break;
                case "Thursday":
                    DragScheduleAvailabilityToLeft_Thursday(Position);
                    break;
                case "Friday":
                    DragScheduleAvailabilityToLeft_Friday(Position);
                    break;
                case "Saturday":
                    DragScheduleAvailabilityToLeft_Saturday(Position);
                    break;
                case "Sunday":
                    DragScheduleAvailabilityToLeft_Sunday(Position);
                    break;
                default:
                    break;
            }

            return this;
        }

        public SystemUserAvailabilitySubPage DragAvailabilityToRight(string DayOfTheWeek, int Position)
        {
            switch (DayOfTheWeek)
            {
                case "Monday":
                    DragScheduleAvailabilityToRight_Monday(Position);
                    break;
                case "Tuesday":
                    DragScheduleAvailabilityToRight_Tuesday(Position);
                    break;
                case "Wednesday":
                    DragScheduleAvailabilityToRight_Wednesday(Position);
                    break;
                case "Thursday":
                    DragScheduleAvailabilityToRight_Thursday(Position);
                    break;
                case "Friday":
                    DragScheduleAvailabilityToRight_Friday(Position);
                    break;
                case "Saturday":
                    DragScheduleAvailabilityToRight_Saturday(Position);
                    break;
                case "Sunday":
                    DragScheduleAvailabilityToRight_Sunday(Position);
                    break;
                default:
                    break;
            }

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToLeft_Monday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Monday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Monday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToLeft_Tuesday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Tuesday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Tuesday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToLeft_Wednesday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Wednesday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Wednesday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToLeft_Thursday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Thursday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Thursday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToLeft_Friday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Friday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Friday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToLeft_Saturday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Saturday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Saturday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToLeft_Sunday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Sunday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Sunday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToRight_Monday(int Position)
        {
            WaitForElementToBeClickable(RightDragButton_Monday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButton_Monday(Position));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToRight_Tuesday(int Position)
        {
            WaitForElementToBeClickable(RightDragButton_Tuesday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButton_Tuesday(Position));

            action.DragAndDropToOffset(webelement, 40, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToRight_Wednesday(int Position)
        {
            WaitForElementToBeClickable(RightDragButton_Wednesday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButton_Wednesday(Position));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToRight_Thursday(int Position)
        {
            WaitForElementToBeClickable(RightDragButton_Thursday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButton_Thursday(Position));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToRight_Friday(int Position)
        {
            WaitForElementToBeClickable(RightDragButton_Friday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButton_Friday(Position));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToRight_Saturday(int Position)
        {
            WaitForElementToBeClickable(RightDragButton_Saturday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButton_Saturday(Position));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilityToRight_Sunday(int Position)
        {
            WaitForElementToBeClickable(RightDragButton_Sunday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButton_Sunday(Position));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateSystemUserWarningMessage(string expectedText)
        {
            WaitForElement(snackbarWarning_Message);
            WaitForElementVisible(snackbarWarning_Message);
            string actualMessageText = GetElementText(snackbarWarning_Message);
            Assert.IsTrue(actualMessageText.Contains(expectedText));

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateInfoAreaMessage(string expectedText)
        {
            ValidateElementText(infoMessageArea, expectedText);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateScheduleAvailabilityAreaDislayed()
        {
            WaitForElement(ScheduleAvailabilityCard);
            ScrollToElement(ScheduleAvailabilityCard);
            bool availabilityCardDisplayed = GetElementVisibility(ScheduleAvailabilityCard);
            Assert.IsTrue(availabilityCardDisplayed);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateScheduleAvailabilityAreaNotDisplayed()
        {
            bool availabilityCardDisplayed = GetElementVisibility(ScheduleAvailabilityCard);
            Assert.IsFalse(availabilityCardDisplayed);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreateFutureScheduleTab(bool expectedStatus)
        {
            if (expectedStatus)
                WaitForElementVisible(createFutureSchedule_Tab);
            else
                WaitForElementNotVisible(createFutureSchedule_Tab, 5);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateSaveButtonsAreDisabled()
        {
            ValidateElementDisabled(SaveButton);
            ValidateElementDisabled(SaveAndCloseButton);

            return this;
        }

        #region Schedule Transport

        public SystemUserAvailabilitySubPage ValidateScheduleTransportAreaDislayed()
        {
            WaitForElement(ScheduleTransportCard);
            ScrollToElement(ScheduleTransportCard);
            bool availabilityCardDisplayed = GetElementVisibility(ScheduleTransportCard);
            Assert.IsTrue(availabilityCardDisplayed);
            System.Threading.Thread.Sleep(1000);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickScheduleTransportCard()
        {
            WaitForElementToBeClickable(ScheduleTransportCard);
            ScrollToElement(ScheduleTransportCard);
            WaitForElementNotVisible("CWRefreshPanel", 7);
            Click(ScheduleTransportCard);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleTransportButton(string DayOfTheWeek, int Position)
        {
            switch (DayOfTheWeek)
            {
                case "Monday":
                    ClickCreateScheduleAvailabilityButton_Monday(Position);
                    break;
                case "Tuesday":
                    ClickCreateScheduleAvailabilityButton_Tuesday(Position);
                    break;
                case "Wednesday":
                    ClickCreateScheduleAvailabilityButton_Wednesday(Position);
                    break;
                case "Thursday":
                    ClickCreateScheduleAvailabilityButton_Thursday(Position);
                    break;
                case "Friday":
                    ClickCreateScheduleAvailabilityButton_Friday(Position);
                    break;
                case "Saturday":
                    ClickCreateScheduleAvailabilityButton_Saturday(Position);
                    break;
                case "Sunday":
                    ClickCreateScheduleAvailabilityButton_Sunday(Position);
                    break;
                default:
                    break;
            }

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleTransportButtonByDate(string Date, int Position)
        {
            System.Threading.Thread.Sleep(1000);

            WaitForElementToBeClickable(clickToCreateScheduleTransportAvailabilityButton(Date, Position));
            Click(clickToCreateScheduleTransportAvailabilityButton(Date, Position));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButtonByDate(string Date, int Position, string name)
        {
            WaitForElementToBeClickable(clickToCreateScheduleAvailabilityButton(Date, Position, name));
            Click(clickToCreateScheduleAvailabilityButton(Date, Position, name));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreateScheduleAvailabilityButtonByDate(string Date, int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleAvailabilityButton(Date, Position));
            ScrollToElement(clickToCreateScheduleAvailabilityButton(Date, Position));
            Click(clickToCreateScheduleAvailabilityButton(Date, Position));

            return this;
        }

        public SystemUserAvailabilitySubPage ClickSpecificSlotToCreateOrEditScheduleTransport(string Date, string selectedTransport)
        {
            WaitForElementToBeClickable(ScheduleTransportSelectedSlot(Date, selectedTransport));
            ScrollToElement(ScheduleTransportSelectedSlot(Date, selectedTransport));
            Click(ScheduleTransportSelectedSlot(Date, selectedTransport));

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateAvailableRecordUnderScheduleTransport(string DateOfMonth, string selectedTransport, bool Visibility = true)
        {
            if (Visibility)
            {
                WaitForElement(ScheduleTransportAvailableTransportSlot(DateOfMonth, selectedTransport));
                WaitForElementVisible(ScheduleTransportAvailableTransportSlot(DateOfMonth, selectedTransport));
                bool availabilityCardDisplayed = GetElementVisibility(ScheduleTransportAvailableTransportSlot(DateOfMonth, selectedTransport));
                Assert.IsTrue(availabilityCardDisplayed);
            }
            else
            {
                bool availabilityCardDisplayed = GetElementVisibility(ScheduleTransportAvailableTransportSlot(DateOfMonth, selectedTransport));
                Assert.IsFalse(availabilityCardDisplayed);
            }
            return this;
        }

        public SystemUserAvailabilitySubPage ValidateAvailableRecordUnderScheduleTransport(string DateOfMonth, string selectedTransport, int Position, bool Visibility = true)
        {
            if (Visibility)
            {
                WaitForElement(ScheduleTransportAvailableTransportSlot(DateOfMonth, selectedTransport, Position));
                ScrollToElement(ScheduleTransportAvailableTransportSlot(DateOfMonth, selectedTransport, Position));
                WaitForElementVisible(ScheduleTransportAvailableTransportSlot(DateOfMonth, selectedTransport, Position));
                bool availabilityCardDisplayed = GetElementVisibility(ScheduleTransportAvailableTransportSlot(DateOfMonth, selectedTransport, Position));
                Assert.IsTrue(availabilityCardDisplayed);
            }
            else
            {
                bool availabilityCardDisplayed = GetElementVisibility(ScheduleTransportAvailableTransportSlot(DateOfMonth, selectedTransport, Position));
                Assert.IsFalse(availabilityCardDisplayed);
            }
            return this;
        }

        public SystemUserAvailabilitySubPage DragSpecificSlotToRight(string Date, string selectedTransport)
        {
            WaitForElementToBeClickable(RightDragButtonFromSlot(Date, selectedTransport));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButtonFromSlot(Date, selectedTransport));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleTransportSlotRightSlider(string Date, string selectedTransport, int offsetX)
        {
            WaitForElementToBeClickable(RightDragButtonFromSlot(Date, selectedTransport));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButtonFromSlot(Date, selectedTransport));

            action.DragAndDropToOffset(webelement, offsetX, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleTransportSlotLeftSlider(string Date, string selectedTransport, int offsetX)
        {
            WaitForElementToBeClickable(LeftDragButtonFromSlot(Date, selectedTransport));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButtonFromSlot(Date, selectedTransport));

            action.DragAndDropToOffset(webelement, offsetX, 0).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage DragAndDropTransportAvailabilitySlotFromLeftToRight(string Date, int FromPosition, int ToPosition)
        {
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var FromWebelement = GetWebElement(LeftDragButton(Date, FromPosition));
            var ToWebelement = GetWebElement(RightDragButton(Date, ToPosition));

            action.DragAndDrop(FromWebelement, ToWebelement).Build().Perform();

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateScheduleTransportDayOfWeekAreaVisible(string Date, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(ScheduleTransportDayOfTheWeekArea(Date));
            else
                WaitForElementNotVisible(ScheduleTransportDayOfTheWeekArea(Date), 7);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateTooltipOnMouseHover(string Date, string selectedTransport, string expectedText)
        {
            WaitForElementToBeClickable(ScheduleTransport_SelectedTransportSlot(Date, selectedTransport));
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(ScheduleTransport_SelectedTransportSlot(Date, selectedTransport));
            action.MoveToElement(webelement).Perform();

            ValidateElementTextContainsText(TooltipMessage, expectedText);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateTooltipOnMouseHover(string Date, string selectedTransport, int Position, string expectedText)
        {
            WaitForElementToBeClickable(ScheduleTransport_SelectedTransportSlot(Date, selectedTransport, Position));
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(ScheduleTransport_SelectedTransportSlot(Date, selectedTransport, Position));
            action.MoveToElement(webelement).Perform();

            ValidateElementTextContainsText(TooltipMessage, expectedText);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateSlotToCreateScheduleTransportVisible(string Date, int Position, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementToBeClickable(clickToCreateScheduleTransportAvailabilityButton(Date, Position));
            }
            else
            {
                WaitForElementNotVisible(clickToCreateScheduleTransportAvailabilityButton(Date, Position), 7);
            }
            return this;
        }

        #endregion

        #region View Diary / Manage Ad Hoc

        public SystemUserAvailabilitySubPage ValidateViewDiary_ManageAdHocAreaDislayed()
        {
            WaitForElement(ViewDiary_ManageAdHocCard);
            ScrollToElement(ViewDiary_ManageAdHocCard);
            bool availabilityCardDisplayed = GetElementVisibility(ViewDiary_ManageAdHocCard);
            Assert.IsTrue(availabilityCardDisplayed);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickViewDiary_ManageAdHocCard()
        {
            WaitForElementToBeClickable(ViewDiary_ManageAdHocCard);
            ScrollToElement(ViewDiary_ManageAdHocCard);
            Click(ViewDiary_ManageAdHocCard);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreatedScheduleAvailabilitySlot(string date, string name)
        {
            WaitForElementVisible(CreatedAvailabilitySlot(date, name));
            ScrollToElement(CreatedAvailabilitySlot(date, name));
            ValidateElementTextContainsText(CreatedAvailabilitySlot(date, name), name);

            return this;
        }

        public SystemUserAvailabilitySubPage MouseoverCreatedScheduleAvailabilitySlot(string date, string name)
        {
            WaitForElementToBeClickable(CreatedAvailabilitySlot(date, name));
            ScrollToElement(CreatedAvailabilitySlot(date, name));
            MouseHover(CreatedAvailabilitySlot(date, name));

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCreateScheduleAvailabilitySlotByDateIsVisible(string Date, int Position, string name)
        {
            WaitForElement(clickToCreateScheduleAvailabilityButton(Date, Position, name));
            ScrollToElement(clickToCreateScheduleAvailabilityButton(Date, Position, name));

            bool isVisible = GetElementVisibility(clickToCreateScheduleAvailabilityButton(Date, Position, name));
            Assert.IsTrue(isVisible);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickCreatedScheduledAvailabilityButton(string Date, string ContractName)
        {
            WaitForElementToBeClickable(clickToCreatedScheduledAvailabilityButton(Date, ContractName));
            ScrollToElement(clickToCreatedScheduledAvailabilityButton(Date, ContractName));
            Click(clickToCreatedScheduledAvailabilityButton(Date, ContractName));

            return this;
        }

        #endregion

        public SystemUserAvailabilitySubPage WaitForRecordToBeSaved(bool AddWeekButton = true)
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(AvailabilityTimeTable_Field);
            WaitForElement(Week1CycleDate_Title);
            WaitForElement(Week1CycleDate_Field);

            if (AddWeekButton)
                WaitForElementVisible(addWeekButton);

            WaitForElementVisible(weekButton(1));
            WaitForElementVisible(weekExpandButton(1));

            return this;
        }

        public SystemUserAvailabilitySubPage WaitForSavedOperationToComplete()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(SystemUserIFrame);
            SwitchToIframe(SystemUserIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CWHTMLResourcePanel_IFrame);
            SwitchToIframe(CWHTMLResourcePanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElementToBeClickable(ScheduleAvailabilityCard);
            WaitForElementToBeClickable(ScheduleTransportCard);
            WaitForElementToBeClickable(ViewDiary_ManageAdHocCard);
            WaitForElementToBeClickable(refreshButton);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCurrentScheduleIsVisible(bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(CurrentSchedule);
            else
                WaitForElementNotVisible(CurrentSchedule, 5);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateWarningMessageNotDisplay()
        {
            WaitForElementNotVisible(snackbarWarning_Message, 5);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateSaveButtonsAreEnabled()
        {
            ValidateElementEnabled(SaveButton);
            ValidateElementEnabled(SaveAndCloseButton);

            return this;
        }

        public SystemUserAvailabilitySubPage ClickDeleteSignIcon()
        {
            WaitForElementVisible(deleteSign);
            ScrollToElement(deleteSign);
            Click(deleteSign);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateSelectedScheduleAvailabilitySlotIsVisible(string date, string expectedText, bool IsVisible)
        {

            if (IsVisible)
                WaitForElementVisible(ScheduleAvailabilitySlotText(date, expectedText));
            else
                WaitForElementNotVisible(ScheduleAvailabilitySlotText(date, expectedText), 5);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateWaringMessageSignIcons()
        {
            WaitForElementVisible(deleteSign);
            WaitForElementVisible(infoSign);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateToolTip(string expectedText)
        {
            ValidateElementText(toolTip, expectedText);

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateSelectedScheduleAvailabilityColorIsVisible(string date, string expectedText, string number, bool IsVisible)
        {
            if (IsVisible)
                WaitForElementVisible(ScheduleAvailabilitySlotColor(date, expectedText, number));
            else
                WaitForElementNotVisible(ScheduleAvailabilitySlotColor(date, expectedText, number), 5);
            return this;
        }

        public SystemUserAvailabilitySubPage ScrollAndClickOnScheduleAvailabilityDateLabel(string date)
        {
            WaitForElementToBeClickable(ScheduleAvailabilityDateLabel(date));
            ScrollToElement(ScheduleAvailabilityDateLabel(date));
            Click(ScheduleAvailabilityDateLabel(date));

            return this;
        }

        public SystemUserAvailabilitySubPage ValidateCurrentSelectedWeek(int WeekNumber)
        {
            WaitForElementVisible(CurrentSelectedWeek(WeekNumber));
            ScrollToElement(CurrentSelectedWeek(WeekNumber));
            bool weekDisplay = GetElementVisibility(CurrentSelectedWeek(WeekNumber));
            Assert.IsTrue(weekDisplay);

            return this;
        }

        public SystemUserAvailabilitySubPage ScrollAndClickOnScheduleAvailabilityDayLabel(string dayOfWeek)
        {
            WaitForElementToBeClickable(ScheduleAvailabilityDayLabel(dayOfWeek));
            ScrollToElement(ScheduleAvailabilityDayLabel(dayOfWeek));
            Click(ScheduleAvailabilityDayLabel(dayOfWeek));

            return this;
        }

        public SystemUserAvailabilitySubPage DragScheduleAvailabilitySlotFromLeftToRight(string DayOfTheWeek, int FromPosition, int ToPosition)
        {
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var FromWebelement = GetWebElement(ScheduleAvailabilityLeftDragButton(DayOfTheWeek, FromPosition));
            var ToWebelement = GetWebElement(ScheduleAvailabilityRightDragButton(DayOfTheWeek, ToPosition));

            action.DragAndDrop(FromWebelement, ToWebelement).Build().Perform();
            return this;
        }

    }
}
