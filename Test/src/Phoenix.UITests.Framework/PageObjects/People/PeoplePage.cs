using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PeoplePage : CommonMethods
    {
        public PeoplePage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }



        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By peoplePageHeader = By.XPath("//div[@id='CWToolbar']/div/h1[text()='People']");


        #region Advanced Search Filter Panel

        readonly By DoNotUseViewFilter_CheckBox = By.Id("CWIgnoreViewFilter");

        readonly By FirstName_Field = By.Id("CWField_person_firstname");
        readonly By LastName_Field = By.Id("CWField_person_lastname");
        readonly By Id_Field = By.Id("CWField_person_personnumber");
        readonly By DOB_Field = By.Id("CWField_person_dateofbirth");

        readonly By UseRange_CheckBox = By.Id("CWField_person_dateofbirth_range");
        readonly By DOBFrom_Field = By.Id("CWField_person_dateofbirth_from");
        readonly By DOBTo_Field = By.Id("CWField_person_dateofbirth_to");

        readonly By StatedGender_Picklist = By.Id("CWField_person_genderid");

        readonly By Deceased_YesRadioButton = By.Id("CWField_person_deceased_1");
        readonly By Deceased_NoRadioButton = By.Id("CWField_person_deceased_0");
        readonly By Inactive_YesRadioButton = By.Id("CWField_person_inactive_1");
        readonly By Inactive_NoRadioButton = By.Id("CWField_person_inactive_0");

        readonly By ClearFilters_Button = By.Id("CWClearFiltersButton");
        readonly By Search_Button = By.Id("CWSearchButton");

        #endregion

        readonly By viewsPicklist = By.Id("CWViewSelector");

        readonly By PersonHeader_Id_Link = By.XPath("//*[@id='CWGridHeaderRow']/th[@field='personnumber']//a");

        By PersonRow(string PersonID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + PersonID + "']/td[2]");

        readonly By searchResult_DropDown = By.XPath("//select[@id='CWViewSelector']");

        By PersonD(string PersonID) => By.XPath("//table[@id='CWGrid']/tbody/tr/td[4]");

        By PersonRowCheckBox(string PersonID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + PersonID + "']/td[1]/input");

        readonly By myPinnedPeople_Option = By.XPath("//select[@id='CWViewSelector']/optgroup/option[text()='My Pinned People']");

        #region Toolbar

        readonly By NewRecordButton = By.Id("TI_NewRecordButton");
        readonly By ExportToExcelButton = By.Id("TI_ExportToExcelButton");
        readonly By MailMergeButton = By.Id("TI_MailMergeButton");
        readonly By AssignRecordButton = By.Id("TI_AssignRecordButton");
        readonly By topBannerMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");
        readonly By PinToMeButton = By.Id("TI_PinToMeButton");
        readonly By UnpinFromMeButton = By.Id("TI_UnpinFromMeButton");
        readonly By BulkEditButton = By.Id("TI_BulkEditButton");
        readonly By DeleteRecordButton = By.Id("TI_DeleteRecordButton");
        readonly By additionalItemsMenuButton = By.XPath("//*[@id='CWToolbarMenu']/button");

        #endregion

        readonly By noRecorrdsMainMessage = By.XPath("//*[@id='CWGridHolder']/div/h2[text()='NO RECORDS']");
        readonly By noRecordsSubMessage = By.XPath("//*[@id='CWGridHolder']/div/span[text()='No results were found for this screen.']");

        public PeoplePage WaitForPeoplePageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(peoplePageHeader);

            ValidateElementText(peoplePageHeader, "People");

            return this;
        }

        public PeoplePage SearchPersonRecord(string SearchQuery, string PersonID)
        {
            WaitForElementNotVisible("CWRefreshPanel", 30);

            ClickDoNotUseViewFilterCheckbox();

            WaitForElementToBeClickable(Id_Field);
            SendKeys(Id_Field, SearchQuery);

            WaitForElementToBeClickable(Search_Button);
            Click(Search_Button);

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(PersonRow(PersonID));

            return this;
        }





        public PeoplePage SearchPersonRecordByID(string SearchQuery, string PersonID)
        {
            ClickDoNotUseViewFilterCheckbox();
            InsertID(SearchQuery);
            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(PersonRow(PersonID));

            return this;
        }

        public PeoplePage SearchPersonRecordByFirstName(string SearchQuery, string PersonID)
        {
            ClickDoNotUseViewFilterCheckbox();
            InsertFirstName(SearchQuery);
            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(PersonRow(PersonID));

            return this;
        }

        public PeoplePage SearchPersonRecordByLastName(string SearchQuery, string PersonID)
        {
            ClickDoNotUseViewFilterCheckbox();
            InsertLastName(SearchQuery);
            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(PersonRow(PersonID));

            return this;
        }

        public PeoplePage SearchPersonRecordByFullName(string FirstName, string LastName, string PersonID)
        {
            ClickDoNotUseViewFilterCheckbox();
            InsertFirstName(FirstName);
            InsertLastName(LastName);
            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            WaitForElement(PersonRow(PersonID));

            return this;
        }







        public PeoplePage SearchPersonRecordByID(string SearchQuery)
        {
            ClickDoNotUseViewFilterCheckbox();
            InsertID(SearchQuery);
            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public PeoplePage SearchPersonRecordByFirstName(string SearchQuery)
        {
            ClickDoNotUseViewFilterCheckbox();
            InsertFirstName(SearchQuery);
            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public PeoplePage SearchPersonRecordByLastName(string SearchQuery)
        {
            ClickDoNotUseViewFilterCheckbox();
            InsertLastName(SearchQuery);
            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }

        public PeoplePage SearchPersonRecordByFullName(string FirstName, string LastName)
        {
            ClickDoNotUseViewFilterCheckbox();
            InsertFirstName(FirstName);
            InsertLastName(LastName);
            ClickSearchButton();

            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }



        public PeoplePage OpenPersonRecord(string PersonId)
        {
            WaitForElementToBeClickable(PersonRow(PersonId));
            Click(PersonRow(PersonId));            

            return this;
        }

        public PeoplePage OpenPersonRecord(Guid PersonId)
        {
            return OpenPersonRecord(PersonId.ToString());
        }

        public PeoplePage SelectPersonRecord(string PersonId)
        {
            WaitForElement(PersonRowCheckBox(PersonId));
            Click(PersonRowCheckBox(PersonId));

            return this;
        }

        public PeoplePage SelectPersonRecord(Guid PersonId)
        {
            return SelectPersonRecord(PersonId.ToString());
        }

        public PeoplePage ClickAdditionalItemsMenuButton()
        {
            this.Click(additionalItemsMenuButton);

            return this;
        }

        public PeoplePage ClickPinToMe()
        {
            this.Click(PinToMeButton);
            this.WaitForElementNotVisible("CWRefreshPanel", 10);

            return this;
        }

        public PeoplePage TapTopBannerMenuButton()
        {
            WaitForElementToBeClickable(topBannerMenuButton);
            Click(topBannerMenuButton);

            return this;
        }

        public PeoplePage ClickNewRecordButton()
        {
            WaitForElementNotVisible("CWRefreshPanel", 7);
            WaitForElementToBeClickable(NewRecordButton);
            Click(NewRecordButton);

            return this;
        }

        public PeoplePage TapMailMergeButton()
        {
            Click(MailMergeButton);

            return this;
        }

        public PeoplePage TapAssignRecordButton()
        {
            WaitForElementToBeClickable(AssignRecordButton);
            ScrollToElement(AssignRecordButton);            
            Click(AssignRecordButton);

            return this;
        }


        public PeoplePage SelectAvailableViewByText(string PicklistText)
        {
            SelectPicklistElementByText(viewsPicklist, PicklistText);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        public PeoplePage WaitForBulkEditButtonVisible()
        {
            WaitForElementVisible(BulkEditButton);

            return this;
        }

        public PeoplePage ClickBulkEditButton()
        {
            Click(BulkEditButton);

            return this;
        }

        public ProviderRecordPage OpenCreatedPersonRecord(string PersonId)
        {
            WaitForElement(PersonD(PersonId));
            driver.FindElement(PersonD(PersonId)).Click();

            return new ProviderRecordPage(this.driver, this.Wait, this.appURL);
        }

        public PeoplePage ValidateMyPinnedPeopleOption(string ExpectedText)
        {
            WaitForElement(myPinnedPeople_Option);
            ValidateElementText(myPinnedPeople_Option, ExpectedText);
            return this;
        }

        public PeoplePage ValidatePersonRecord(string PersonId, string ExpectedValue)
        {
            WaitForElement(PersonRowCheckBox(PersonId));
            ValidateElementValue(PersonRowCheckBox(PersonId), ExpectedValue);

            return this;
        }

        public PeoplePage ValidatePersonRecordPresent(string PersonId)
        {
            WaitForElement(PersonRowCheckBox(PersonId));

            return this;
        }
        public PeoplePage ValidatePersonRecordPresent(Guid PersonId)
        {
            return ValidatePersonRecordPresent(PersonId.ToString());
        }

        public PeoplePage ValidatePersonRecordNotPresent(string PersonId)
        {
            WaitForElementNotVisible(PersonRowCheckBox(PersonId), 7);

            return this;
        }

        public PeoplePage ValidatePersonRecordNotPresent(Guid PersonId)
        {
            return ValidatePersonRecordNotPresent(PersonId.ToString());
        }

        public PeoplePage ValidateNoRecordsMessageVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(noRecorrdsMainMessage);
                WaitForElementVisible(noRecordsSubMessage);
            }
            else
            {
                WaitForElementNotVisible(noRecorrdsMainMessage, 3);
                WaitForElementNotVisible(noRecordsSubMessage, 3);
            }

            return this;
        }

        #region Advanced Search Filter Panel

        public PeoplePage ClickDoNotUseViewFilterCheckbox()
        {
            WaitForElementToBeClickable(DoNotUseViewFilter_CheckBox);
            string elementChecked = GetElementByAttributeValue(DoNotUseViewFilter_CheckBox, "checked");
            if (elementChecked != "true")
                Click(DoNotUseViewFilter_CheckBox);

            return this;
        }

        public PeoplePage InsertFirstName(string TextToInsert)
        {
            WaitForElementToBeClickable(FirstName_Field);
            SendKeys(FirstName_Field, TextToInsert);

            return this;
        }

        public PeoplePage InsertLastName(string TextToInsert)
        {
            WaitForElementToBeClickable(LastName_Field);
            SendKeys(LastName_Field, TextToInsert);

            return this;
        }

        public PeoplePage InsertID(string TextToInsert)
        {
            WaitForElementToBeClickable(Id_Field);
            SendKeys(Id_Field, TextToInsert);

            return this;
        }

        public PeoplePage InsertID(int Id)
        {
            return InsertID(Id.ToString());
        }

        public PeoplePage InsertDOB(string TextToInsert)
        {
            WaitForElementToBeClickable(DOB_Field);
            SendKeys(DOB_Field, TextToInsert);

            return this;
        }

        public PeoplePage ClickUseRangeCheckbox()
        {
            WaitForElementToBeClickable(UseRange_CheckBox);
            Click(UseRange_CheckBox);

            return this;
        }

        public PeoplePage InsertDOBFrom(string TextToInsert)
        {
            WaitForElementToBeClickable(DOBFrom_Field);
            SendKeys(DOBFrom_Field, TextToInsert);

            return this;
        }

        public PeoplePage InsertDOBTo(string TextToInsert)
        {
            WaitForElementToBeClickable(DOBTo_Field);
            SendKeys(DOBTo_Field, TextToInsert);

            return this;
        }

        public PeoplePage ValidateDOBDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(DOB_Field);
            }
            else
            {
                ValidateElementNotDisabled(DOB_Field);
            }

            return this;
        }

        public PeoplePage ValidateDOBFromDisabled(bool ExpectDisabled)
        {
            if(ExpectDisabled)
            {
                ValidateElementDisabled(DOBFrom_Field);
            }
            else
            {
                ValidateElementNotDisabled(DOBFrom_Field);
            }

            return this;
        }

        public PeoplePage ValidateDOBToDisabled(bool ExpectDisabled)
        {
            if (ExpectDisabled)
            {
                ValidateElementDisabled(DOBTo_Field);
            }
            else
            {
                ValidateElementNotDisabled(DOBTo_Field);
            }

            return this;
        }

        public PeoplePage SelectStatedGender(string TextToSelect)
        {
            WaitForElementToBeClickable(StatedGender_Picklist);
            SelectPicklistElementByText(StatedGender_Picklist, TextToSelect);

            return this;
        }

        public PeoplePage ClickDeceasedYesRadioButton()
        {
            WaitForElementToBeClickable(Deceased_YesRadioButton);
            Click(Deceased_YesRadioButton);

            return this;
        }

        public PeoplePage ClickDeceasedNoRadioButton()
        {
            WaitForElementToBeClickable(Deceased_NoRadioButton);
            Click(Deceased_NoRadioButton);

            return this;
        }

        public PeoplePage ClickInactiveYesRadioButton()
        {
            WaitForElementToBeClickable(Inactive_YesRadioButton);
            Click(Inactive_YesRadioButton);

            return this;
        }

        public PeoplePage ClickInactiveNoRadioButton()
        {
            WaitForElementToBeClickable(Inactive_NoRadioButton);
            Click(Inactive_NoRadioButton);

            return this;
        }

        public PeoplePage ClickClearFiltersButton()
        {
            WaitForElementToBeClickable(ClearFilters_Button);
            Click(ClearFilters_Button);

            return this;
        }

        public PeoplePage ClickSearchButton()
        {
            WaitForElementToBeClickable(Search_Button);
            Click(Search_Button);

            return this;
        }

        #endregion

        public PeoplePage ClickOnTableHeaderIdLink()
        {
            WaitForElementToBeClickable(PersonHeader_Id_Link);
            Click(PersonHeader_Id_Link);
            WaitForElementNotVisible("CWRefreshPanel", 30);

            return this;
        }
    }
}
