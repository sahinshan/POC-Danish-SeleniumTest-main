using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ApplicantScheduleTransportSubPage : CommonMethods
    {
        public ApplicantScheduleTransportSubPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By ApplicantRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=applicant&')]");
        readonly By CWHTMLResourcePanel_IFrame = By.Id("CWUrlPanel_IFrame");

        readonly By ApplicantsPageHeader = By.XPath("//*[@id='SA-controls']/div[1]/h1[text()='Schedule Transport']");

        readonly By refreshButton = By.Id("TI_RefreshButton");
        readonly By SaveButton = By.Id("TI_SaveButton");
        readonly By SaveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By RoleApplicationTimeTable_Field = By.Id("dragContent");
        readonly By Week1CycleDate_Title = By.XPath("//*[@id='SA-date-anchor']/label");
        readonly By Week1CycleDate_Field = By.XPath("//*[@id='SA-schedule-start']");


        readonly By addWeekButton = By.XPath("//sa-sub-nav-link[@title='Create a new empty week']");
        By weekButton(int WeekNumber) => By.XPath("//*[@id='SA-main-header']/*/*/*/*/*/*/span[@title='View week " + WeekNumber + "']");
        By weekExpandButton(int WeekNumber) => By.XPath("//*[@id='SA-main-header']/*/*/*/*/*/*/span[@title='View week " + WeekNumber + "']/span");
        
        By removeWeekButton(int WeekNumber) => By.XPath("//span[@title='Remove week " + WeekNumber + "']");
        By duplicateWeekButton(int WeekNumber) => By.XPath("//span[contains(@title, 'Duplicate week " + WeekNumber + "')]");
        By moveWeekRightButton(int WeekNumber) => By.XPath("//span[contains(@title, 'Move week " + WeekNumber + " to after week')]");
        By moveWeekLeftButton(int WeekNumber) => By.XPath("//span[contains(@title, 'Move week " + WeekNumber + " to before week')]");



        By clickToCreateTransportButton_Monday(int Position) => By.XPath("//b[text()='Monday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");
        By clickToCreateTransportButton_Tuesday(int Position) => By.XPath("//b[text()='Tuesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");
        By clickToCreateTransportButton_Wednesday(int Position) => By.XPath("//b[text()='Wednesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");
        By clickToCreateTransportButton_Thursday(int Position) => By.XPath("//b[text()='Thursday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");
        By clickToCreateTransportButton_Friday(int Position) => By.XPath("//b[text()='Friday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");
        By clickToCreateTransportButton_Saturday(int Position) => By.XPath("//b[text()='Saturday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");
        By clickToCreateTransportButton_Sunday(int Position) => By.XPath("//b[text()='Sunday']/parent::div/parent::span/parent::div/span/span[" + Position + "]");

        By LeftDragButton_Monday(int Position) => By.XPath("//b[text()='Monday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Tuesday(int Position) => By.XPath("//b[text()='Tuesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Wednesday(int Position) => By.XPath("//b[text()='Wednesday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Thursday(int Position) => By.XPath("//b[text()='Thursday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Friday(int Position) => By.XPath("//b[text()='Friday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Saturday(int Position) => By.XPath("//b[text()='Saturday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        By LeftDragButton_Sunday(int Position) => By.XPath("//b[text()='Sunday']/parent::div/parent::span/parent::div/span/span[" + Position + "]/div[@class='touchBars left']");
        readonly By ViewDiaryOrManageAdhocTab = By.XPath("//*[@id='SA-card-diary']");
        By clickToCreateTransportAvailabilityButton(string DateOfMonth, int Position) => By.XPath("//div[@attr-date='" + DateOfMonth + "']/span/span[" + Position + "]");


        public ApplicantScheduleTransportSubPage WaitForApplicantScheduleTransportSubPagePageToLoad()
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

            WaitForElementVisible(ApplicantsPageHeader);

            WaitForElement(RoleApplicationTimeTable_Field);
            WaitForElement(Week1CycleDate_Title);
            WaitForElement(Week1CycleDate_Field);
            
            WaitForElementVisible(addWeekButton);
            
            WaitForElementVisible(weekButton(1));
            WaitForElementVisible(weekExpandButton(1));

            WaitForElementVisible(clickToCreateTransportButton_Monday(1));
            WaitForElementVisible(clickToCreateTransportButton_Tuesday(1));
            WaitForElementVisible(clickToCreateTransportButton_Wednesday(1));
            WaitForElementVisible(clickToCreateTransportButton_Thursday(1));
            WaitForElementVisible(clickToCreateTransportButton_Friday(1));
            WaitForElementVisible(clickToCreateTransportButton_Saturday(1));
            WaitForElementVisible(clickToCreateTransportButton_Sunday(1));

            return this;
        }

        public ApplicantScheduleTransportSubPage ClickOnSaveButton()
        {
            WaitForElementToBeClickable(SaveButton);
            Click(SaveButton);

            WaitForElementNotVisible("CWRefreshPanel", 14);

            return this;
        }
        public ApplicantScheduleTransportSubPage ClickOnSaveAndCloseButton()
        {
            WaitForElementToBeClickable(SaveAndCloseButton);
            Click(SaveAndCloseButton);
            return this;
        }
        public ApplicantScheduleTransportSubPage ClickRefreshButton()
        {
            WaitForElement(refreshButton);
            Click(refreshButton);

            return this;
        }


        public ApplicantScheduleTransportSubPage ClickAddWeekButton()
        {
            WaitForElementToBeClickable(addWeekButton);
            Click(addWeekButton);
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickWeekButton(int WeekNumber)
        {
            WaitForElementToBeClickable(weekButton(WeekNumber));
            Click(weekButton(WeekNumber));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickWeekExpandButton(int WeekNumber)
        {
            WaitForElementToBeClickable(weekExpandButton(WeekNumber));
            Click(weekExpandButton(WeekNumber));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickRemoveWeekButton(int WeekNumber)
        {
            WaitForElementToBeClickable(removeWeekButton(WeekNumber));
            Click(removeWeekButton(WeekNumber));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickDuplicateWeekButton(int WeekNumber)
        {
            WaitForElementToBeClickable(duplicateWeekButton(WeekNumber));
            Click(duplicateWeekButton(WeekNumber));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickMoveWeekRightButton(int WeekNumber)
        {
            WaitForElementToBeClickable(moveWeekRightButton(WeekNumber));
            Click(moveWeekRightButton(WeekNumber));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickMoveWeekLeftButton(int WeekNumber)
        {
            WaitForElementToBeClickable(moveWeekLeftButton(WeekNumber));
            Click(moveWeekLeftButton(WeekNumber));
            return this;
        }


        public ApplicantScheduleTransportSubPage ClickCreateTransportAvailabilityButton(string DayOfTheWeek, int Position)
        {
            switch (DayOfTheWeek)
            {
                case "Monday":
                    ClickCreateTransportAvailabilityButton_Monday(Position);
                    break;
                case "Tuesday":
                    ClickCreateTransportAvailabilityButton_Tuesday(Position);
                    break;
                case "Wednesday":
                    ClickCreateTransportAvailabilityButton_Wednesday(Position);
                    break;
                case "Thursday":
                    ClickCreateTransportAvailabilityButton_Thursday(Position);
                    break;
                case "Friday":
                    ClickCreateTransportAvailabilityButton_Friday(Position);
                    break;
                case "Saturday":
                    ClickCreateTransportAvailabilityButton_Saturday(Position);
                    break;
                case "Sunday":
                    ClickCreateTransportAvailabilityButton_Sunday(Position);
                    break;
                default:
                    break;
            }

            return this;
        }

        public ApplicantScheduleTransportSubPage ClickCreateTransportAvailabilityButton_Monday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateTransportButton_Monday(Position));
            Click(clickToCreateTransportButton_Monday(Position));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickCreateTransportAvailabilityButton_Tuesday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateTransportButton_Tuesday(Position));
            Click(clickToCreateTransportButton_Tuesday(Position));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickCreateTransportAvailabilityButton_Wednesday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateTransportButton_Wednesday(Position));
            Click(clickToCreateTransportButton_Wednesday(Position));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickCreateTransportAvailabilityButton_Thursday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateTransportButton_Thursday(Position));
            Click(clickToCreateTransportButton_Thursday(Position));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickCreateTransportAvailabilityButton_Friday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateTransportButton_Friday(Position));
            Click(clickToCreateTransportButton_Friday(Position));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickCreateTransportAvailabilityButton_Saturday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateTransportButton_Saturday(Position));
            Click(clickToCreateTransportButton_Saturday(Position));
            return this;
        }

        public ApplicantScheduleTransportSubPage ClickCreateTransportAvailabilityButton_Sunday(int Position)
        {
            WaitForElementToBeClickable(clickToCreateTransportButton_Sunday(Position));
            Click(clickToCreateTransportButton_Sunday(Position));
            return this;
        }


        public ApplicantScheduleTransportSubPage InsertWeek1CycleDate(string TextToInsert)
        {
            WaitForElementToBeClickable(Week1CycleDate_Field);
            SendKeys(Week1CycleDate_Field, TextToInsert);

            return this;
        }



        public ApplicantScheduleTransportSubPage ValidateRemoveWeekButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if(ExpectVisible)
            {
                WaitForElementVisible(removeWeekButton(WeekNumber));
            }
            else
            {
                WaitForElementNotVisible(removeWeekButton(WeekNumber), 2);
            }
            return this;
        }

        public ApplicantScheduleTransportSubPage ValidateDuplicateWeekButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(duplicateWeekButton(WeekNumber));
            }
            else
            {
                WaitForElementNotVisible(duplicateWeekButton(WeekNumber), 2);
            }
            return this;
        }
                                                 
        public ApplicantScheduleTransportSubPage ValidateMoveWeekRightButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(moveWeekRightButton(WeekNumber));
            }
            else
            {
                WaitForElementNotVisible(moveWeekRightButton(WeekNumber), 2);
            }
            return this;
        }
                                                 
        public ApplicantScheduleTransportSubPage ValidateMoveWeekLeftButtonVisibility(int WeekNumber, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(moveWeekLeftButton(WeekNumber));
            }
            else
            {
                WaitForElementNotVisible(moveWeekLeftButton(WeekNumber), 2);
            }
            return this;
        }

        public ApplicantScheduleTransportSubPage ValidateAddWeekButtonVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(addWeekButton);
            }
            else
            {
                WaitForElementNotVisible(addWeekButton, 2);
            }
            return this;
        }




        public ApplicantScheduleTransportSubPage ValidateWeek1CycleDate(string ExpectedText)
        {
            ValidateElementValueByJavascript("SA-schedule-start", ExpectedText);

            return this;
        }

        public ApplicantScheduleTransportSubPage ValidateWeek1CycleDateReadonly(bool ExpectReadonly)
        {
            if(ExpectReadonly)
            {
                ValidateElementReadonly(Week1CycleDate_Field);
            }
            else
            {
                ValidateElementNotReadonly(Week1CycleDate_Field);
            }

            return this;
        }




        public ApplicantScheduleTransportSubPage DragTransportAvailabilityToLeft(string DayOfTheWeek, int Position)
        {
            switch (DayOfTheWeek)
            {
                case "Monday":
                    DragTransportAvailabilityToLeft_Monday(Position);
                    break;
                case "Tuesday":
                    DragTransportAvailabilityToLeft_Tuesday(Position);
                    break;
                case "Wednesday":
                    DragTransportAvailabilityToLeft_Wednesday(Position);
                    break;
                case "Thursday":
                    DragTransportAvailabilityToLeft_Thursday(Position);
                    break;
                case "Friday":
                    DragTransportAvailabilityToLeft_Friday(Position);
                    break;
                case "Saturday":
                    DragTransportAvailabilityToLeft_Saturday(Position);
                    break;
                case "Sunday":
                    DragTransportAvailabilityToLeft_Sunday(Position);
                    break;
                default:
                    break;
            }

            return this;
        }

        public ApplicantScheduleTransportSubPage DragTransportAvailabilityToLeft_Monday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Monday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Monday(Position));
            
            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public ApplicantScheduleTransportSubPage DragTransportAvailabilityToLeft_Tuesday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Tuesday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Tuesday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public ApplicantScheduleTransportSubPage DragTransportAvailabilityToLeft_Wednesday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Wednesday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Wednesday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public ApplicantScheduleTransportSubPage DragTransportAvailabilityToLeft_Thursday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Thursday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Thursday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public ApplicantScheduleTransportSubPage DragTransportAvailabilityToLeft_Friday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Friday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Friday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public ApplicantScheduleTransportSubPage DragTransportAvailabilityToLeft_Saturday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Saturday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Saturday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }

        public ApplicantScheduleTransportSubPage DragTransportAvailabilityToLeft_Sunday(int Position)
        {
            WaitForElementToBeClickable(LeftDragButton_Sunday(Position));

            OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(driver);
            var webelement = GetWebElement(LeftDragButton_Sunday(Position));

            action.DragAndDropToOffset(webelement, -20, 0).Build().Perform();

            return this;
        }


        public ApplicantScheduleTransportSubPage ClickViewDiaryOrManageAdhocTab()
        {
            WaitForElement(ViewDiaryOrManageAdhocTab);
            MoveToElementInPage(ViewDiaryOrManageAdhocTab);
            Click(ViewDiaryOrManageAdhocTab);

            return this;
        }

        public ApplicantScheduleTransportSubPage ClickTransportAvailabilitySlot(string Date, int Position)
        {
            WaitForElementToBeClickable(clickToCreateTransportAvailabilityButton(Date, Position));
            ScrollToElement(clickToCreateTransportAvailabilityButton(Date, Position));
            Click(clickToCreateTransportAvailabilityButton(Date, Position));
            return this;
        }
    }
}
