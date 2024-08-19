using OpenQA.Selenium;
using System;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SelectStaffPopup : CommonMethods
    {
        public SelectStaffPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By dialogHeader = By.XPath("//div[@class='cd-layer-header--right']");
        
        readonly By filterStaffByResponsibleTeamDropdown = By.XPath("//*[@id='id--rostering--0--responsible-team-filter-cd-dropdown-search']");
        
        By responsibleTeamPicklistOptions(string TeamToSelect) => By.XPath("//*[@id='id--rostering--0--responsible-team-filter-cd-dropdown-search']//div/span[text() = '" + TeamToSelect + "']");

        By responsibleTeamDropdownSelectedText(Guid TeamId, string Team) => By.XPath("//*[@id='id--rostering--0--responsible-team-filter']//span[@data-id='"+ TeamId + "']/span[@class='mcc-chip__label'][text() = '" + Team + "']");
        
        readonly By filterStaffByNameSearchBox = By.XPath("//*[@id = 'id--rostering--0--name-filter--search']");

        readonly By confirmSelectionButton = By.XPath("//div[@id='id--id--rostering--0--layer--confirm']");
        readonly By backToBookingButton = By.XPath("//div[@id='id--id--rostering--0--layer--back']");
        readonly By onlyShowFullyMatchedChkBox = By.XPath("//input[@id='id--rostering--0--matched-staff-switch--checkbox']");
        readonly By onlyShowAvailableStaffChkBox = By.XPath("//input[@id='id--rostering--0--available-staff-switch--checkbox']");
        
        By StaffSelection(string RecordID) => By.XPath("//div[@data-id='" + RecordID + "']//input");

        By selectedStaffText(string RecordId, string StaffName) => By.XPath("//*[@id='id--rostering--0--selected']//span[@data-id='"+ RecordId + "']/span[text() = '"+ StaffName + "']");

        By selectedStaffValue(string RecordId) => By.XPath("//*[@id='id--rostering--0--pending-selected']//span[@data-id='" + RecordId + "']");


        public SelectStaffPopup WaitForSelectStaffPopupToLoad()
        {
            System.Threading.Thread.Sleep(1000);

            SwitchToDefaultFrame();

            WaitForElementNotVisible("CWRefreshPanel", 45);
            WaitForElementNotVisible("id--rostering--0--list-spinner", 45);

            WaitForElement(confirmSelectionButton);
            WaitForElement(backToBookingButton);

            WaitForElementVisible(filterStaffByNameSearchBox);
            
            return this;
        }

        //wait for element to be clickable, move to element, click filterStaffByResponsibleTeamDropdown
        public SelectStaffPopup ClickFilterStaffByResponsibleTeamDropdown()
        {
            WaitForElementToBeClickable(filterStaffByResponsibleTeamDropdown);
            MoveToElementInPage(filterStaffByResponsibleTeamDropdown);
            Click(filterStaffByResponsibleTeamDropdown);

            return this;
        }

        //method to select a team from the picklist
        public SelectStaffPopup SelectResponsibleTeam(string TeamToSelect)
        {
            WaitForElementToBeClickable(responsibleTeamPicklistOptions(TeamToSelect));
            MoveToElementInPage(responsibleTeamPicklistOptions(TeamToSelect));
            Click(responsibleTeamPicklistOptions(TeamToSelect));

            return this;
        }

        //method to verify the selected team is displayed in the filter
        public SelectStaffPopup VerifyResponsibleTeamIsDisplayed(Guid TeamId, string Team, bool IsDisplayed = true)
        {
            if (IsDisplayed)
                WaitForElement(responsibleTeamDropdownSelectedText(TeamId, Team));
            else
                WaitForElementNotVisible(responsibleTeamDropdownSelectedText(TeamId, Team), 3);

            return this;
        }

        //method to enter text into the filterStaffByNameSearchBox
        public SelectStaffPopup EnterTextIntoFilterStaffByNameSearchBox(string StaffName)
        {
            WaitForElementToBeClickable(filterStaffByNameSearchBox);
            ScrollToElement(filterStaffByNameSearchBox);
            Click(filterStaffByNameSearchBox);
            SendKeys(filterStaffByNameSearchBox, StaffName + Keys.Tab);

            return this;
        }

        public SelectStaffPopup ClickOnlyShowMatchedStaff()
        {
            WaitForElementToBeClickable(onlyShowFullyMatchedChkBox);
            Click(onlyShowFullyMatchedChkBox);
            WaitForElementToBeClickable(onlyShowAvailableStaffChkBox);
            Click(onlyShowAvailableStaffChkBox);

            return this;
        }

        public SelectStaffPopup ClickOnlyShowAvailableStaff()
        {
            WaitForElementToBeClickable(onlyShowAvailableStaffChkBox);
            MoveToElementInPage(onlyShowAvailableStaffChkBox);
            Click(onlyShowAvailableStaffChkBox);

            return this;
        }

        public SelectStaffPopup ClickStaffRecordCellText(string RecordID)
        {
            WaitForElementToBeClickable(StaffSelection(RecordID));
            MoveToElementInPage(StaffSelection(RecordID));
            Click(StaffSelection(RecordID));

            return this;
        }

        //validate staff record is displayed in the list
        public SelectStaffPopup VerifyStaffRecordIsDisplayed(Guid RecordID, bool IsDisplayed = true)
        {
            if (IsDisplayed)
                WaitForElement(StaffSelection(RecordID.ToString()));
            else
                WaitForElementNotVisible(StaffSelection(RecordID.ToString()), 3);

            return this;
        }

        public SelectStaffPopup ClickStaffRecordCellText(Guid RecordID)
        {
            return ClickStaffRecordCellText(RecordID.ToString());
        }

        public SelectStaffPopup ValidateStaffRecordNotVisible(string RecordID)
        {
            WaitForElementNotVisible(StaffSelection(RecordID), 3);

            return this;
        }

        public SelectStaffPopup ValidateStaffRecordNotVisible(Guid RecordID)
        {
            return ValidateStaffRecordNotVisible(RecordID.ToString());
        }

        public SelectStaffPopup ClickStaffConfirmSelection()
        {
            WaitForElementToBeClickable(confirmSelectionButton);
            MoveToElementInPage(confirmSelectionButton);
            Click(confirmSelectionButton);

            return this;
        }

        public SelectStaffPopup VerifySelectedStaffRecordInstaffForBookingIsDisplayed(Guid RecordID, string StaffName, bool IsDisplayed = true)
        {
            if (IsDisplayed)
                WaitForElement(selectedStaffText(RecordID.ToString(), StaffName));
            else
                WaitForElementNotVisible(selectedStaffText(RecordID.ToString(), StaffName), 3);

            return this;
        }

        public SelectStaffPopup VerifySelectedStaffRecordText(Guid RecordID, bool IsDisplayed = true)
        {
            if (IsDisplayed)
                WaitForElement(selectedStaffValue(RecordID.ToString()));
            else
                WaitForElementNotVisible(selectedStaffValue(RecordID.ToString()), 3);

            return this;
        }

        public SelectStaffPopup ClickBackToBookingButton()
        {
            WaitForElementToBeClickable(backToBookingButton);
            MoveToElementInPage(backToBookingButton);
            Click(backToBookingButton);

            return this;
        }
    }
}
