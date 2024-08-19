using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class PeoplePage: CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("PEOPLE");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _genericViewPicker = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        #region Pagination

        readonly Func<AppQuery, AppQuery> _firstDisabledButton = e => e.Marked("FirstDisabled");
        readonly Func<AppQuery, AppQuery> _firstButton = e => e.Marked("First");
        readonly Func<AppQuery, AppQuery> _previousDisabledButton = e => e.Marked("PreviousDisabled");
        readonly Func<AppQuery, AppQuery> _previousButton = e => e.Marked("Previous");

        readonly Func<AppQuery, AppQuery> _pageInfoButton = e => e.Marked("PageInfo");

        readonly Func<AppQuery, AppQuery> _nextDisabledButton = e => e.Marked("NextDisabled");
        readonly Func<AppQuery, AppQuery> _nextButton = e => e.Marked("Next");


        #endregion


        #region Mobile View

        Func<AppQuery, AppQuery> _recordIdentifier_MobileView(string RecordID) => c => c.Marked("person_Row_" + RecordID+"_Cell_fullname");

        Func<AppQuery, AppQuery> _toogleButton(string parentCellIdentifier) => c => c.Marked(parentCellIdentifier).Descendant().Marked("toggleIcon");

        Func<AppQuery, AppQuery> _fullNameLabel(string PersonID) => c => c.Marked("person_PrimaryRow_" + PersonID).Descendant().Marked("fullname_0_Label").Text("Full Name: ");
        Func<AppQuery, AppQuery> _fullNameText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_PrimaryRow_" + PersonID).Descendant().Marked("fullname_0_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _fullAddressLabel(string PersonID) => c => c.Marked("person_PrimaryRow_" + PersonID).Descendant().Marked("fulladdress_1_Label").Text("Full Address: ");
        Func<AppQuery, AppQuery> _fullAddressText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_PrimaryRow_" + PersonID).Descendant().Marked("fulladdress_1_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _idLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("personnumber_2_Label").Text("Id: ");
        Func<AppQuery, AppQuery> _idText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("personnumber_2_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _firstNameLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("firstname_3_Label").Text("First Name: ");
        Func<AppQuery, AppQuery> _firstNameText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("firstname_3_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _lastNameLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("lastname_4_Label").Text("Last Name: ");
        Func<AppQuery, AppQuery> _lastNameText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("lastname_4_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _representsHazzardLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("representalertorhazard_5_Label").Text("Represents Alert/Hazard?: ");
        Func<AppQuery, AppQuery> _representsHazzardText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("representalertorhazard_5_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _genderLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("genderid_cwname_6_Label").Text("Stated Gender: ");
        Func<AppQuery, AppQuery> _genderText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("genderid_cwname_6_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _dobLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("dateofbirth_7_Label").Text("DOB: ");
        Func<AppQuery, AppQuery> _dobText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("dateofbirth_7_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _nhsNoLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("nhsnumber_8_Label").Text("NHS No: ");
        Func<AppQuery, AppQuery> _nhsNoText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("nhsnumber_8_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _postCodeLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("postcode_9_Label").Text("Postcode: ");
        Func<AppQuery, AppQuery> _postCodeText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("postcode_9_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _createdByLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("createdby_cwname_10_Label").Text("Created By: ");
        Func<AppQuery, AppQuery> _createdByText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("createdby_cwname_10_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _createdOnLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("createdon_11_Label").Text("Created On: ");
        Func<AppQuery, AppQuery> _createdOnText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("createdon_11_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _modifiedByLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("modifiedby_cwname_12_Label").Text("Modified By: ");
        Func<AppQuery, AppQuery> _modifiedByText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("modifiedby_cwname_12_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _modifiedOnLabel(string PersonID) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("modifiedon_13_Label").Text("Modified On: ");
        Func<AppQuery, AppQuery> _modifiedOnText(string PersonID, string ExpectedPersonName) => c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("modifiedon_13_Value").Text(ExpectedPersonName);

        #endregion



        public PeoplePage(IApp app)
        {
            _app = app;
        }




        public PeoplePage VerifyFirstPageButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                WaitForElementNotVisible(_firstDisabledButton);
                //bool firstPageDisabledButtonVisible = this._app.Query(_firstDisabledButton).Any();
                //if (firstPageDisabledButtonVisible)
                //    Assert.Fail("_firstDisabledButton is visible");

                WaitForElement(_firstButton);
                //bool firstPageButtonVisible = this._app.Query(_firstButton).Any();
                //if (!firstPageButtonVisible)
                //    Assert.Fail("_firstButton is not visible");

            }
            else
            {
                WaitForElement(_firstDisabledButton);
                //bool firstPageDisabledButtonVisible = this._app.Query(_firstDisabledButton).Any();
                //if (!firstPageDisabledButtonVisible)
                //    Assert.Fail("_firstDisabledButton is not visible");

                WaitForElementNotVisible(_firstButton);
                //bool firstPageButtonVisible = this._app.Query(_firstButton).Any();
                //if (firstPageButtonVisible)
                //    Assert.Fail("_firstButton is visible");
            }
            return this;
        }
        public PeoplePage VerifyPreviousPageButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                WaitForElementNotVisible(_previousDisabledButton);
                //bool firstPageDisabledButtonVisible = this._app.Query(_previousDisabledButton).Any();
                //Assert.IsFalse(firstPageDisabledButtonVisible, "_previousDisabledButton is visible");

                WaitForElement(_previousButton);
                //bool firstPageButtonVisible = this._app.Query(_previousButton).Any();
                //Assert.IsTrue(firstPageButtonVisible, "_previousButton is not visible");
            }
            else
            {
                WaitForElement(_previousDisabledButton);
                //bool firstPageDisabledButtonVisible = this._app.Query(_previousDisabledButton).Any();
                //Assert.IsTrue(firstPageDisabledButtonVisible, "_previousDisabledButton is not visible");

                WaitForElementNotVisible(_previousButton);
                //bool firstPageButtonVisible = this._app.Query(_previousButton).Any();
                //Assert.IsFalse(firstPageButtonVisible, "_previousButton is visible");
            }
            return this;
        }
        public PeoplePage VerifyNextPageButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                WaitForElementNotVisible(_nextDisabledButton);
                //bool nextPageDisabledButtonVisible = this._app.Query(_nextDisabledButton).Any();
                //Assert.IsFalse(nextPageDisabledButtonVisible, "_nextDisabledButton is visible");

                WaitForElement(_nextButton);
                //bool nextPageButtonVisible = this._app.Query(_nextButton).Any();
                //Assert.IsTrue(nextPageButtonVisible, "_nextButton is not visible");
            }
            else
            {
                WaitForElement(_nextDisabledButton);
                //bool nextPageDisabledButtonVisible = this._app.Query(_nextDisabledButton).Any();
                //Assert.IsTrue(nextPageDisabledButtonVisible, "_nextDisabledButton is not visible");

                WaitForElementNotVisible(_nextButton);
                //bool nextPageButtonVisible = this._app.Query(_nextButton).Any();
                //Assert.IsFalse(nextPageButtonVisible, "_nextButton is visible");
            }

            return this;
        }
        public PeoplePage VerifyPaginationButtonInfo(string expectedPaginationText)
        {
            string paginationText = this._app.Query(_pageInfoButton).FirstOrDefault().Text;
            Assert.AreEqual(expectedPaginationText, paginationText);
            return this;
        }
        public PeoplePage TapFistPageButton()
        {
            this._app.Tap(_firstButton);
            return this;
        }
        public PeoplePage TapPreviousPageButton()
        {
            this._app.Tap(_previousButton);
            return this;
        }
        public PeoplePage TapNextPageButton()
        {
            this._app.Tap(_nextButton);
            return this;
        }




        public PeoplePage WaitForPeoplePageToLoad(string ViewText = "People whom with I have an active Involvement")
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_peoplePageIconButton);
            _app.WaitForElement(_pageTitle);
            
            _app.WaitForElement(_viewPicker(ViewText));

            _app.WaitForElement(_searchTextBox);
            _app.WaitForElement(_searchButton);
            _app.WaitForElement(_refreshButton);

            return this;
        }


        public PeoplePage SelectRecentlyViewedRecordsView()
        {
            _app.Tap(_genericViewPicker);

            GenericPicklistPopup _pickList = new GenericPicklistPopup(this._app);

            _pickList
                .WaitForPicklistToLoad()
                .ScrollUpPickList(1)
                .TapOkButton();

            return this;
        }

        public PeoplePage SelecMyActiveRecordsView()
        {
            _app.Tap(_genericViewPicker);

            GenericPicklistPopup _pickList = new GenericPicklistPopup(this._app);

            _pickList
                .WaitForPicklistToLoad()
                .ScrollDownPickList(1)
                .TapOkButton();

            return this;
        }

        public PeoplePage TapViewPicker()
        {
            _app.Tap(_genericViewPicker);
            return this;
        }


        public PeoplePage TypeInSearchTextBox(string Text)
        {
            _app.ClearText(_searchTextBox);
            _app.DismissKeyboard();

            _app.EnterText(_searchTextBox, Text);
            _app.DismissKeyboard();

            return this;
        }


        public PeoplePage TapRefreshButton()
        {
            _app.Tap(_refreshButton);

            return this;
        }

        public PeoplePage TapSearchButton()
        {
            _app.Tap(_searchButton);

            return this;
        }

        public PersonPage TapOnPersonRecordButton(string TextToFind, string PersonID)
        {
            string parentCellIdentifier = "person_Row_" + PersonID + "_Cell_fullname";

            this._app.Tap(c => c.Marked(parentCellIdentifier).Descendant().Marked(TextToFind));

            return new PersonPage(this._app);
        }

        public PersonPage TapOnPersonRecordButton(string PersonID)
        {
            string parentCellIdentifier = "person_Row_" + PersonID ;

            this._app.Tap(c => c.Marked(parentCellIdentifier));

            return new PersonPage(this._app);
        }


        public PeoplePage VerifyThatTextVisible(string TextToFind)
        {
            bool _anyMatch = _app.Query(TextToFind).Any();

            if (!_anyMatch)
                Assert.Fail("The test framework didn´t found any element that maches the query. Query: " + TextToFind);

            return this;
        }

        public PeoplePage VerifyThatTextVisible(string TextToFind, string PersonID)
        {
            string parentCellIdentifier = "person_Row_" + PersonID + "_Cell_";

            bool _anyMatch = _app.Query(c => c.Property("contentDescription").Contains(parentCellIdentifier).Descendant().Marked(TextToFind)).Any();

            if (_anyMatch)
                return this;

            // if no match is found try to scroll to find the element
            _app.ScrollTo(TextToFind);
            _anyMatch = _app.Query(c => c.Property("contentDescription").Contains(parentCellIdentifier).Descendant().Marked(TextToFind)).Any();

            if (_anyMatch)
                return this;

            throw new Exception("The test framework didn´t found any element that maches the query. Query: " + TextToFind);
        }

        public PeoplePage VerifyThatTextVisible(string TextToFind, string PersonID, string CellSuffix)
        {
            string parentCellIdentifier = "person_Row_" + PersonID + "_Cell_" + CellSuffix;

            TryScrollToElement(parentCellIdentifier);

            bool _anyMatch = _app.Query(c => c.Property("contentDescription").Contains(parentCellIdentifier).Descendant().Marked(TextToFind)).Any();

            if (_anyMatch)
                return this;

            // if no match is found try to scroll to find the element
            _app.ScrollTo(parentCellIdentifier);
            _anyMatch = _app.Query(c => c.Property("contentDescription").Contains(parentCellIdentifier).Descendant().Marked(TextToFind)).Any();

            if (_anyMatch)
                return this;

            throw new Exception("The test framework didn´t found any element that maches the query. Query: " + TextToFind);
        }

        public PeoplePage VerifyThatTextNotVisible(string TextToFind)
        {
            bool _anyMatch = _app.Query(TextToFind).Any();

            if (_anyMatch)
                Assert.Fail("The test framework did found one or more elements that maches the query. Query: " + TextToFind);

            return this;
        }



        #region Mobile View Display

        public ProfessionalPage TapOnPersonRecordButton_MobileView(string PersonID)
        {
            ScrollToElement(_recordIdentifier_MobileView(PersonID));
            Tap(_recordIdentifier_MobileView(PersonID));

            return new ProfessionalPage(this._app);
        }

        public PeoplePage TapExpandPersonRecordButton_MobileView(string PersonID)
        {
            string parentCellIdentifier = "person_Row_" + PersonID;

            _app.Tap(_toogleButton(parentCellIdentifier));

            return this;
        }

        public PeoplePage ValidatePersonLabels_MobileView(string PersonID)
        {
            ScrollToElement(_fullNameLabel(PersonID));
            bool elementFound = _app.Query(_fullNameLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _fullNameLabel");

            ScrollToElement(_fullAddressLabel(PersonID));
            elementFound = _app.Query(_fullAddressLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _fullAddressLabel");

            ScrollToElement(_idLabel(PersonID));
            elementFound = _app.Query(_idLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _idLabel");

            ScrollToElement(_firstNameLabel(PersonID));
            elementFound = _app.Query(_firstNameLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _firstNameLabel");

            ScrollToElement(_lastNameLabel(PersonID));
            elementFound = _app.Query(_lastNameLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _lastNameLabel");

            ScrollToElement(_representsHazzardLabel(PersonID));
            elementFound = _app.Query(_representsHazzardLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _representsHazzardLabel");

            ScrollToElement(_genderLabel(PersonID));
            elementFound = _app.Query(_genderLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _genderLabel");

            ScrollToElement(_dobLabel(PersonID));
            elementFound = _app.Query(_dobLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _dobLabel");

            ScrollToElement(_nhsNoLabel(PersonID));
            elementFound = _app.Query(_nhsNoLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _nhsNoLabel");

            ScrollToElement(_postCodeLabel(PersonID));
            elementFound = _app.Query(_postCodeLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _postCodeLabel");

            ScrollToElement(_createdByLabel(PersonID));
            elementFound = _app.Query(_createdByLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _createdByLabel");

            ScrollToElement(_createdOnLabel(PersonID));
            elementFound = _app.Query(_createdOnLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _createdOnLabel");

            ScrollToElement(_modifiedByLabel(PersonID));
            elementFound = _app.Query(_modifiedByLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _modifiedByLabel");

            ScrollToElement(_modifiedOnLabel(PersonID));
            elementFound = _app.Query(_modifiedOnLabel(PersonID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _modifiedOnLabel");

            return this;
        }

        public PeoplePage ValidateFullNameText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_fullNameLabel(PersonID));
            bool elementFound = this._app.Query(_fullNameText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _fullNameLabel");

            return this;
        }

        public PeoplePage ValidateFullAddressText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_fullAddressLabel(PersonID));
            bool elementFound = this._app.Query(_fullAddressText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _fullAddressText");

            return this;
        }

        public PeoplePage ValidateIdText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_idLabel(PersonID));
            bool elementFound = this._app.Query(_idText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _idText");

            return this;
        }

        public PeoplePage ValidateFirstNameText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_firstNameLabel(PersonID));
            bool elementFound = this._app.Query(_firstNameText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _firstNameText");

            return this;
        }

        public PeoplePage ValidateLastNameText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_lastNameLabel(PersonID));
            bool elementFound = this._app.Query(_lastNameText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _lastNameText");

            return this;
        }

        public PeoplePage ValidateRepresentsHazzardText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_representsHazzardLabel(PersonID));
            bool elementFound = this._app.Query(_representsHazzardText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _representsHazzardText");

            return this;
        }

        public PeoplePage ValidateGenderText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_genderLabel(PersonID));
            bool elementFound = this._app.Query(_genderText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _genderLabel");

            return this;
        }

        public PeoplePage ValidateDOBText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_dobLabel(PersonID));
            bool elementFound = this._app.Query(_dobText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _dobLabel");

            return this;
        }

        public PeoplePage ValidateNHSNoText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_nhsNoLabel(PersonID));
            bool elementFound = this._app.Query(_nhsNoText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _nhsNoText");

            return this;
        }

        public PeoplePage ValidatePostCodeText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_postCodeLabel(PersonID));
            bool elementFound = this._app.Query(_postCodeText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _postCodeText");

            return this;
        }

        public PeoplePage ValidateCreatedByText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_createdByLabel(PersonID));
            bool elementFound = this._app.Query(_createdByText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _createdByText");

            return this;
        }

        public PeoplePage ValidateCreatedOnText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_createdOnLabel(PersonID));
            bool elementFound = this._app.Query(_createdOnText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _createdOnText");

            return this;
        }

        public PeoplePage ValidateModifiedByText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_modifiedByLabel(PersonID));
            bool elementFound = this._app.Query(_modifiedByText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _modifiedByText");

            return this;
        }

        public PeoplePage ValidateMOdifiedOnText_MobileView(string PersonID, string ExpectedText)
        {
            ScrollToElement(_modifiedOnLabel(PersonID));
            bool elementFound = this._app.Query(_modifiedOnText(PersonID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _modifiedOnText");

            return this;
        }



        public PeoplePage ValidateFullAddressTextViewNotVisible_MobileView(string PersonID)
        {
            bool elementVisible = this._app.Query(c => c.Marked("person_PrimaryRow_" + PersonID).Descendant().Marked("fulladdress_1_Value")).Any();
            Assert.IsFalse(elementVisible);

            return this;
        }

        public PeoplePage ValidateFirstNameTextViewNotVisible_MobileView(string PersonID)
        {
            bool elementVisible = this._app.Query(c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("firstname_3_Value")).Any();
            Assert.IsFalse(elementVisible);

            return this;
        }

        public PeoplePage ValidateNHSNoTextViewNotVisible_MobileView(string PersonID)
        {
            bool elementVisible = this._app.Query(c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("nhsnumber_8_Value")).Any();
            Assert.IsFalse(elementVisible);

            return this;
        }

        public PeoplePage ValidatePostCodeTextViewNotVisible_MobileView(string PersonID)
        {
            bool elementVisible = this._app.Query(c => c.Marked("person_SecondaryRow_" + PersonID).Descendant().Marked("postcode_9_Value")).Any();
            Assert.IsFalse(elementVisible);

            return this;
        }


        #endregion


    }
}
