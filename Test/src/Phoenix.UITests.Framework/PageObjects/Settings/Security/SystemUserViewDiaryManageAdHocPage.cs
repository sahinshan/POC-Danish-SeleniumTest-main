using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects.Settings.Security
{
    public class SystemUserViewDiaryManageAdHocPage : CommonMethods
    {
        public SystemUserViewDiaryManageAdHocPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By SystemUserIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=systemuser&')]");
        readonly By CWUrlPanel_IFrame = By.Id("CWUrlPanel_IFrame");

        //readonly By ScheduleTransportPageHeader = By.XPath("//*[@id='SA-controls']/div[1]/h1[text()='Schedule Transport']");
        //readonly By ScheduleTransportCard = By.XPath("//div[@id = 'SA-card-transport']/div/h3");

        readonly By ScheduleAvailabilityPageHeader = By.XPath("//*[@id='SA-controls']/div[1]/h1[text()='Schedule Availability']");
        readonly By ScheduleAvailabilityCard = By.XPath("//div[@id = 'SA-card-availability']/div/h3");
        readonly By viewDiaryManageAdHocPageHeader = By.XPath("//*[@id='SA-controls']/div[1]/h1[text()='View Diary / Manage Ad Hoc']");
        readonly By viewDiaryManageAdHocCard = By.XPath("//div[@id = 'SA-card-diary']/div/h3");

        readonly By refreshButton = By.Id("TI_RefreshButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");
        readonly By BackButton = By.XPath("//button[@title = 'Back']");

        readonly By viewDiaryAdhocTimeTable = By.Id("dragContent");
        readonly By GoToDate_Title = By.XPath("//*[@id='SA-date-anchor']/label");
        readonly By GoToDate_Field = By.XPath("//*[@id='SA-select-date']");
        readonly By employmentContractPicklist = By.Id("SA-change-role");
        readonly By availabilityTypePicklist = By.Id("SA-change-type");

        By clickToCreateScheduleButton_Monday(int Position) => By.XPath("//b[text() = 'Monday']/parent::div/parent::span/parent::div/span[" + Position + "]");

        By clickToCreateScheduleButton_Tuesday(int Position) => By.XPath("//b[text() = 'Tuesday']/parent::div/parent::span/parent::div/span[" + Position + "]");

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

        readonly By snackbarWarning_Message = By.XPath("//*[@class = 'cd-snackbar-container']");
        
        readonly By deleteSign = By.XPath("//*[@class = 'cd-snackbar-container']//*[@name='delete_sign']");
        
        readonly By infoSign = By.XPath("//*[@class = 'cd-snackbar-container']//*[@name='info_popup']");

        By ViewDiary_ManageAdHocSelectedRecord(string DayOfTheWeek, int Position, string selectedTransport) => By.XPath("//b[text()='" + DayOfTheWeek + "']/parent::div/parent::span/parent::div/span/span[" + Position + "]/*[@name='" + selectedTransport + "']");
        By ViewDiary_ManageAdHocSelectedRecordByDate(string DateOfMonth, int Position, string selectedTransport) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]/*[@name='" + selectedTransport + "']");

        By clickToCreateContractButton_Monday(string contractTitle) => By.XPath("//b[text() = 'Monday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Tuesday(string contractTitle) => By.XPath("//b[text() = 'Tuesday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Wednesday(string contractTitle) => By.XPath("//b[text() = 'Wednesday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Thursday(string contractTitle) => By.XPath("//b[text() = 'Thursday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Friday(string contractTitle) => By.XPath("//b[text() = 'Friday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Saturday(string contractTitle) => By.XPath("//b[text() = 'Saturday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");
        By clickToCreateContractButton_Sunday(string contractTitle) => By.XPath("//b[text() = 'Sunday']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");

        By clickToCreateContractButton(string date, string dayOfTheWeek, string contractTitle) => By.XPath("//div[@attr-date='" + date + "']//b[text() = '" + dayOfTheWeek + "']/parent::div/parent::span/parent::div/span/span/span[text() = \"Click to create '" + contractTitle + "' contract availability\"]");

        By CreateAdHocSlot(string dateValue, string contractTitle) => By.XPath("//div[@attr-date = '" + dateValue + "']/span/span/span[@class = 'rowType'][contains(text(),\"" + contractTitle + "\")]");
        By CreateAdHocSlot(string dateValue, int RowNumber, int CellNumber) => By.XPath("//div[@attr-date = '"+ dateValue + "']/span[" + RowNumber + "]/span[" + CellNumber + "]");

        By CreatedAdHocSlot(string dateValue, string contractTitle) => By.XPath("//div[@attr-date = '" + dateValue + "']/span/span[contains(@class, 'ds-selected precedence')][1]/div[text() = \"" + contractTitle + "\"]");

        By CreatedAdHocSlotClickHolder(string dateValue, string name) => By.XPath("//div[@attr-date = '" + dateValue + "']/span/span[contains(@class, 'ds-selected precedence')][1]/div[text() = \"" + name + "\"]/following-sibling::span[@class = 'clickHolder']");

        By clickToCreateTransportAvailabilityButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]");

        By clickToCreateScheduleAvailabilityButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span[2]/span[" + Position + "]");

        By clickCreatedScheduleAvailabilityButton(string DateOfMonth, string contractName) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/div[@class='roleName'][text()='" + contractName + "']/parent::span");

        By LeftDragButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]/span[@class='left drag transport']");
        By RightDragButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]/span[@class='right drag transport']");

        By ViewDiary_ManageAdHocAvailableTransportSlot(string DateOfMonth, string selectedTransport) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/*[@name='" + selectedTransport + "']");
        By ViewDiary_ManageAdHocSelectedTransportSlot(string DateOfMonth, string selectedTransport) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/*[@name='" + selectedTransport + "']/parent::span");

        By RightDragButtonFromSlot(string DateOfMonth, string selectedTransport) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span/*[@name='" + selectedTransport + "']/parent::span/span[@class='right drag transport']");

        readonly By TooltipMessage = By.XPath("//*[@class='dragTooltip']/span[@class='tooltipText']");

        By LeftDragButtonAdHoc(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]/span[contains(@class,'left drag precedence')]");
        By RightDragButtonAdHoc(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]/span[contains(@class,'right drag precedence')]");

        public SystemUserViewDiaryManageAdHocPage WaitForSystemUserViewDiaryManageAdHocPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(SystemUserIFrame);
            SwitchToIframe(SystemUserIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(CWUrlPanel_IFrame);
            SwitchToIframe(CWUrlPanel_IFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(viewDiaryAdhocTimeTable);
            WaitForElement(GoToDate_Title);
            WaitForElement(GoToDate_Field);

            WaitForElementVisible(clickToCreateScheduleButton_Monday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Tuesday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Wednesday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Thursday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Friday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Saturday(1));
            WaitForElementVisible(clickToCreateScheduleButton_Sunday(1));

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickRefreshButton()
        {
            WaitForElementToBeClickable(refreshButton);
            Click(refreshButton);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickScheduleAvailabilityCard()
        {
            WaitForElementToBeClickable(ScheduleAvailabilityCard);
            ScrollToElement(ScheduleAvailabilityCard);
            Click(ScheduleAvailabilityCard);
            return this;
        }


        public SystemUserViewDiaryManageAdHocPage InsertGoToDate(string date)
        {
            WaitForElementToBeClickable(GoToDate_Field);
            ClearInputElementViaJavascript("SA-select-date");
            SendKeys(GoToDate_Field, date + Keys.Tab);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage SelectEmploymentContractFilter(string OptionToSelect)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);
            WaitForElement(employmentContractPicklist);
            ScrollToElement(employmentContractPicklist);
            SelectPicklistElementByText(employmentContractPicklist, OptionToSelect);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage SelectAvailabilityTypeFilter(string OptionToSelect)
        {
            WaitForElementNotVisible("CWRefreshPanel", 10);
            WaitForElement(availabilityTypePicklist);
            ScrollToElement(availabilityTypePicklist);
            SelectPicklistElementByText(availabilityTypePicklist, OptionToSelect);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailabilityButton(string DayOfTheWeek, int Position)
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

        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailabilityButton_Monday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Monday(Position));
            ScrollToElement(clickToCreateScheduleButton_Monday(Position));
            Click(clickToCreateScheduleButton_Monday(Position)); ;
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailability(string dateValue, string title)
        {
            WaitForElementToBeClickable(CreateAdHocSlot(dateValue, title));
            ScrollToElement(CreateAdHocSlot(dateValue, title));
            Click(CreateAdHocSlot(dateValue, title));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailability(string dateValue, int RowNumber, int CellNumber)
        {
            WaitForElementToBeClickable(CreateAdHocSlot(dateValue, RowNumber, CellNumber));
            ScrollToElement(CreateAdHocSlot(dateValue, RowNumber, CellNumber));
            Click(CreateAdHocSlot(dateValue, RowNumber, CellNumber));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreatedScheduleAvailability(string date, string name)
        {
            WaitForElementToBeClickable(CreatedAdHocSlotClickHolder(date, name));
            ScrollToElement(CreatedAdHocSlotClickHolder(date, name));
            Click(CreatedAdHocSlotClickHolder(date, name));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailabilityButton_Tuesday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Tuesday(Position));
            ScrollToElement(clickToCreateScheduleButton_Tuesday(Position));
            Click(clickToCreateScheduleButton_Tuesday(Position));
            return this;
        }


        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailabilityButton_Wednesday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Wednesday(Position));
            ScrollToElement(clickToCreateScheduleButton_Wednesday(Position));
            Click(clickToCreateScheduleButton_Wednesday(Position));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailabilityButton_Thursday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Thursday(Position));
            ScrollToElement(clickToCreateScheduleButton_Thursday(Position));
            Click(clickToCreateScheduleButton_Thursday(Position));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailabilityButton_Friday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Friday(Position));
            ScrollToElement(clickToCreateScheduleButton_Friday(Position));
            Click(clickToCreateScheduleButton_Friday(Position));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailabilityButton_Saturday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Saturday(Position));
            ScrollToElement(clickToCreateScheduleButton_Saturday(Position));
            Click(clickToCreateScheduleButton_Saturday(Position));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateScheduleAvailabilityButton_Sunday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateScheduleButton_Sunday(Position));
            ScrollToElement(clickToCreateScheduleButton_Sunday(Position));
            Click(clickToCreateScheduleButton_Sunday(Position));
            return this;
        }


        public SystemUserViewDiaryManageAdHocPage ValidateCreateScheduleAvailabilityButton_Monday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Monday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Monday(name));
            Assert.IsTrue(isVisible);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateCreateScheduleAvailabilityButton_Tuesday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Tuesday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Tuesday(name));
            Assert.IsTrue(isVisible);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateCreateScheduleAvailabilityButton_Wednesday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Wednesday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Wednesday(name));
            Assert.IsTrue(isVisible);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateCreateScheduleAvailabilityButton_Thursday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Thursday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Thursday(name));
            Assert.IsTrue(isVisible);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateCreateScheduleAvailabilityButton_Friday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Friday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Friday(name));
            Assert.IsTrue(isVisible);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateCreateScheduleAvailabilityButton_Saturday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Saturday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Saturday(name));
            Assert.IsTrue(isVisible);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateCreateScheduleAvailabilityButton_Sunday(string name)
        {
            WaitForElementToBeClickable(clickToCreateContractButton_Sunday(name));
            bool isVisible = GetElementVisibility(clickToCreateContractButton_Sunday(name));
            Assert.IsTrue(isVisible);
            return this;
        }



        public SystemUserViewDiaryManageAdHocPage ValidateRemoveWeekButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                //WaitForElementVisible(removeWeekButton(WeekNumber));
            }
            else
            {
                // WaitForElementNotVisible(removeWeekButton(WeekNumber), 2);
            }
            return this;
        }


        public SystemUserViewDiaryManageAdHocPage ValidateWeek1CycleDate(string ExpectedText)
        {
            ValidateElementValueByJavascript("SA-schedule-start", ExpectedText);

            return this;
        }


        public SystemUserViewDiaryManageAdHocPage DragTransportAvailabilityToLeft(string DayOfTheWeek, int Position)
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

        public SystemUserViewDiaryManageAdHocPage DragScheduleAvailabilityToLeft_Monday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Monday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Monday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragScheduleAvailabilityToLeft_Tuesday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Tuesday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Tuesday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragScheduleAvailabilityToLeft_Wednesday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Wednesday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Wednesday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragScheduleAvailabilityToLeft_Thursday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Thursday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Thursday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragScheduleAvailabilityToLeft_Friday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Friday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Friday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragScheduleAvailabilityToLeft_Saturday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Saturday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Saturday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragScheduleAvailabilityToLeft_Sunday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Sunday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Sunday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateSystemUserWarningMessage(string expectedText)
        {
            WaitForElement(snackbarWarning_Message);
            WaitForElementVisible(snackbarWarning_Message);
            string actualMessageText = GetElementText(snackbarWarning_Message);
            Assert.IsTrue(actualMessageText.Contains(expectedText));

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateWaringMessageSignIcons()
        {
            WaitForElementVisible(deleteSign);
            WaitForElementVisible(infoSign);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateScheduleAvailabilityAreaDislayed()
        {
            WaitForElement(ScheduleAvailabilityCard);
            ScrollToElement(ScheduleAvailabilityCard);
            bool availabilityCardDisplayed = GetElementVisibility(ScheduleAvailabilityCard);
            Assert.IsTrue(availabilityCardDisplayed);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateViewDiaryManageAdHocAreaDislayed()
        {
            WaitForElementToBeClickable(viewDiaryManageAdHocCard);
            ScrollToElement(viewDiaryManageAdHocCard);
            bool viewDiaryManageAdHocCardDisplayed = GetElementVisibility(viewDiaryManageAdHocCard);
            Assert.IsTrue(viewDiaryManageAdHocCardDisplayed);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateAdHocRecordNotDisplayed(string date, string DayOfTheWeek, string contractName)
        {
            WaitForElementNotVisible(clickToCreateContractButton(date, DayOfTheWeek, contractName), 30);
            bool isVisible = GetElementVisibility(clickToCreateContractButton(date, DayOfTheWeek, contractName));
            Assert.IsFalse(isVisible);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateDefaultAdHocSlotIsDisplayed(string date, string DayOfTheWeek, string contractName)
        {
            bool isVisible = GetElementVisibility(clickToCreateContractButton(date, DayOfTheWeek, contractName));
            if (isVisible == false)
                InsertGoToDate(date);

            WaitForElementVisible(clickToCreateContractButton(date, DayOfTheWeek, contractName));
            ScrollToElement(clickToCreateContractButton(date, DayOfTheWeek, contractName));
            isVisible = GetElementVisibility(clickToCreateContractButton(date, DayOfTheWeek, contractName));
            Assert.IsTrue(isVisible);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateCreatedAdHocSlot(string date, string name)
        {
            bool isVisible = GetElementVisibility(CreatedAdHocSlot(date, name));
            if (isVisible == false)
                InsertGoToDate(date);

            WaitForElementVisible(CreatedAdHocSlot(date, name));
            ScrollToElement(CreatedAdHocSlot(date, name));
            ValidateElementTextContainsText(CreatedAdHocSlot(date, name), name);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateRecordUnderViewDiary_ManageAdHocArea(string DayOfTheWeek, int Position, string selectedTransport)
        {
            WaitForElement(ViewDiary_ManageAdHocSelectedRecord(DayOfTheWeek, Position, selectedTransport));
            ScrollToElement(ViewDiary_ManageAdHocSelectedRecord(DayOfTheWeek, Position, selectedTransport));
            bool availabilityCardDisplayed = GetElementVisibility(ViewDiary_ManageAdHocSelectedRecord(DayOfTheWeek, Position, selectedTransport));
            Assert.IsTrue(availabilityCardDisplayed);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateRecordUnderViewDiary_ManageAdHocAreaByDate(string Date, int Position, string selectedTransport)
        {
            bool isVisible = GetElementVisibility(clickToCreateTransportAvailabilityButton(Date, Position));
            if (isVisible == false)
                InsertGoToDate(Date);

            WaitForElement(ViewDiary_ManageAdHocSelectedRecordByDate(Date, Position, selectedTransport));
            ScrollToElement(ViewDiary_ManageAdHocSelectedRecordByDate(Date, Position, selectedTransport));
            bool availabilityCardDisplayed = GetElementVisibility(ViewDiary_ManageAdHocSelectedRecordByDate(Date, Position, selectedTransport));
            Assert.IsTrue(availabilityCardDisplayed);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateTransportAvailabilityButton(string Date, int Position)
        {
            bool isVisible = GetElementVisibility(clickToCreateTransportAvailabilityButton(Date, Position));
            if (isVisible == false)
                InsertGoToDate(Date);

            WaitForElementToBeClickable(clickToCreateTransportAvailabilityButton(Date, Position));
            ScrollToElement(clickToCreateTransportAvailabilityButton(Date, Position));
            Click(clickToCreateTransportAvailabilityButton(Date, Position));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreateAdhocScheduleAvailabilityButton(string Date, int Position)
        {
            bool isVisible = GetElementVisibility(clickToCreateScheduleAvailabilityButton(Date, Position));
            if (isVisible == false)
                InsertGoToDate(Date);

            WaitForElementToBeClickable(clickToCreateScheduleAvailabilityButton(Date, Position));
            ScrollToElement(clickToCreateScheduleAvailabilityButton(Date, Position));
            Click(clickToCreateScheduleAvailabilityButton(Date, Position));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragAndDropTransportAvailabilitySlotFromLeftToRight(string Date, int FromPosition, int ToPosition)
        {
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var FromWebelement = GetWebElement(LeftDragButton(Date, FromPosition));
            var ToWebelement = GetWebElement(RightDragButton(Date, ToPosition));

            action.DragAndDrop(FromWebelement, ToWebelement).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragScheduleAvailabilitySlotToRight(string Date, int Position)
        {
            WaitForElementToBeClickable(RightDragButton(Date, Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButton(Date, Position));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateAvailableRecordUnderViewDiary_ManageAdHoc(string DateOfMonth, string selectedTransport, bool Visibility = true)
        {
            if (Visibility)
            {
                bool isVisible = GetElementVisibility(ViewDiary_ManageAdHocSelectedTransportSlot(DateOfMonth, selectedTransport));

                if (isVisible == false)
                    InsertGoToDate(DateOfMonth);

                WaitForElement(ViewDiary_ManageAdHocAvailableTransportSlot(DateOfMonth, selectedTransport));
                ScrollToElement(ViewDiary_ManageAdHocAvailableTransportSlot(DateOfMonth, selectedTransport));
                bool availabilityCardDisplayed = GetElementVisibility(ViewDiary_ManageAdHocAvailableTransportSlot(DateOfMonth, selectedTransport));
                Assert.IsTrue(availabilityCardDisplayed);
            }
            else
            {
                bool availabilityCardDisplayed = GetElementVisibility(ViewDiary_ManageAdHocAvailableTransportSlot(DateOfMonth, selectedTransport));
                Assert.IsFalse(availabilityCardDisplayed);
            }
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickSpecificSlotToCreateOrEditTransportAvailability(string Date, string selectedTransport)
        {
            WaitForElementToBeClickable(ViewDiary_ManageAdHocSelectedTransportSlot(Date, selectedTransport));
            ScrollToElement(ViewDiary_ManageAdHocSelectedTransportSlot(Date, selectedTransport));
            Click(ViewDiary_ManageAdHocSelectedTransportSlot(Date, selectedTransport));
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateAvailableSlotsOnMangeAdHoc(string DateOfMonth, string selectedTransport, int count)
        {
            WaitForElement(ViewDiary_ManageAdHocAvailableTransportSlot(DateOfMonth, selectedTransport));
            int totalRecords = CountElements(ViewDiary_ManageAdHocAvailableTransportSlot(DateOfMonth, selectedTransport));
            Assert.AreEqual(totalRecords, count);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragSpecificSlotToRight(string Date, string selectedTransport)
        {
            WaitForElementToBeClickable(RightDragButtonFromSlot(Date, selectedTransport));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButtonFromSlot(Date, selectedTransport));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateTooltipOnMouseHover(string Date, string selectedTransport, string expectedText)
        {
            bool isVisible = GetElementVisibility(ViewDiary_ManageAdHocSelectedTransportSlot(Date, selectedTransport));

            if (isVisible == false)
                InsertGoToDate(Date);

            WaitForElementToBeClickable(ViewDiary_ManageAdHocSelectedTransportSlot(Date, selectedTransport));
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(ViewDiary_ManageAdHocSelectedTransportSlot(Date, selectedTransport));
            action.MoveToElement(webelement).Perform();

            ValidateElementTextContainsText(TooltipMessage, expectedText);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragAndDropAdHocSlotFromLeftToRight(string Date, int FromPosition, int ToPosition)
        {
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var FromWebelement = GetWebElement(LeftDragButtonAdHoc(Date, FromPosition));
            var ToWebelement = GetWebElement(RightDragButtonAdHoc(Date, ToPosition));

            action.DragAndDrop(FromWebelement, ToWebelement).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage DragSpecificAdHocSlotToRight(string Date, int position)
        {
            WaitForElementToBeClickable(RightDragButtonAdHoc(Date, position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(RightDragButtonAdHoc(Date, position));

            action.DragAndDropToOffset(webelement, 20, 0).Build().Perform();

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateTooltipOnMouseHoverForAdHocSlot(string Date, int position, string expectedText)
        {
            bool isVisible = GetElementVisibility(clickToCreateScheduleAvailabilityButton(Date, position));

            if (isVisible == false)
                InsertGoToDate(Date);

            WaitForElementToBeClickable(clickToCreateScheduleAvailabilityButton(Date, position));
            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(clickToCreateScheduleAvailabilityButton(Date, position));
            action.MoveToElement(webelement).Perform();
            System.Threading.Thread.Sleep(2000);

            ValidateElementTextContainsText(TooltipMessage, expectedText);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ClickCreatedScheduleAvailabilityButton(string Date, string ContractName)
        {
            WaitForElementToBeClickable(clickCreatedScheduleAvailabilityButton(Date, ContractName));
            ScrollToElement(clickCreatedScheduleAvailabilityButton(Date, ContractName));
            Click(clickCreatedScheduleAvailabilityButton(Date, ContractName));

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateGoToDateFieldText(string ExpectedValue)
        {
            ScrollToElement(GoToDate_Field);
            ValidateElementValue(GoToDate_Field, ExpectedValue);
            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateGoToDateFieldIsDisplayed(bool ExpectedVisible)
        {
            WaitForElement(GoToDate_Field);
            ScrollToElement(GoToDate_Field);
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(GoToDate_Field));

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateFilterEmploymentContractPicklistIsDisplayed(bool ExpectedVisible)
        {
            WaitForElement(employmentContractPicklist);
            ScrollToElement(employmentContractPicklist);
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(employmentContractPicklist));

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateFilterAvailabilityTypePicklistIsDisplayed(bool ExpectedVisible)
        {
            WaitForElement(availabilityTypePicklist);
            ScrollToElement(availabilityTypePicklist);
            Assert.AreEqual(ExpectedVisible, GetElementVisibility(availabilityTypePicklist));

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateFilterEmploymentContractSelectedText(string ExpectedText)
        {
            WaitForElement(employmentContractPicklist);
            ScrollToElement(employmentContractPicklist);
            ValidatePicklistSelectedText(employmentContractPicklist, ExpectedText);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateFilterAvailabilityTypeSelectedText(string ExpectedText)
        {
            WaitForElement(availabilityTypePicklist);
            ScrollToElement(availabilityTypePicklist);
            ValidatePicklistSelectedText(availabilityTypePicklist, ExpectedText);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateFilterEmploymentContractPicklistContainsText(string ExpectedText)
        {
            WaitForElement(employmentContractPicklist);
            ScrollToElement(employmentContractPicklist);
            ValidatePicklistContainsElementByText(employmentContractPicklist, ExpectedText);

            return this;
        }

        public SystemUserViewDiaryManageAdHocPage ValidateFilterAvailabilityTypePicklistContainsText(string ExpectedText)
        {
            WaitForElement(availabilityTypePicklist);
            ScrollToElement(availabilityTypePicklist);
            ValidatePicklistContainsElementByText(availabilityTypePicklist, ExpectedText);

            return this;
        }

    }
}
