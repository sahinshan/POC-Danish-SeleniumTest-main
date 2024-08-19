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
    public class CasesPage: CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _CasesPageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("CASES");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _genericViewPicker = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");

        readonly Func<AppQuery, AppQuery> _LoadingSymbol = e => e.Marked("ProgressControl");


        Func<AppQuery, AppQuery> _record_DefaultRender(string RecordID, string CellName) => e => e.Marked("case_Row_" + RecordID + "_Cell_" + CellName);
        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("case_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        Func<AppQuery, AppQuery> _screenText (string MatchText) => e => e.Marked(MatchText);


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

        Func<AppQuery, AppQuery> _recordIdentifier_MobileView(string RecordID) => c => c.Marked("case_PrimaryRow_" + RecordID);

        Func<AppQuery, AppQuery> _toogleButton(string parentCellIdentifier) => c => c.Marked(parentCellIdentifier).Descendant().Marked("toggleIcon");

        Func<AppQuery, AppQuery> _personidLabel(string PersonID) => c => c.Marked("case_PrimaryRow_" + PersonID).Descendant().Marked("personid_cwname_0_Label").Text("Person: ");
        Func<AppQuery, AppQuery> _personidText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_PrimaryRow_" + PersonID).Descendant().Marked("personid_cwname0_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _contactreasonidLabel(string PersonID) => c => c.Marked("case_PrimaryRow_" + PersonID).Descendant().Marked("contactreasonid_cwname1_Label").Text("Contact Reason: ");
        Func<AppQuery, AppQuery> _contactreasonidText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_PrimaryRow_" + PersonID).Descendant().Marked("contactreasonid_cwname1_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _nhsnumberLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("casenumber_2_Label").Text("NHS NO: ");
        Func<AppQuery, AppQuery> _nhsnumberText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("casenumber_2_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _lastNameLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("lastname_3_Label").Text("Last Name: ");
        Func<AppQuery, AppQuery> _lastNameText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("lastname_3_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _firstNameLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("firstname_4_Label").Text("First Name: ");
        Func<AppQuery, AppQuery> _firstNameText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("firstname_4_Value").Text(ExpectedPersonName);
        
        Func<AppQuery, AppQuery> _dateofbirthLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("dateofbirth_5_Label").Text("DOB: ");
        Func<AppQuery, AppQuery> _dateofbirthText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("dateofbirth_5_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _requestreceiveddatetimeLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("requestreceiveddatetime_6_Label").Text("DATE/TIME REQUEST RECEIVED: ");
        Func<AppQuery, AppQuery> _requestreceiveddatetimeText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("requestreceiveddatetime_6_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _presentingpriorityidLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("presentingpriorityid_cwname_7_Label").Text("PRESENTING PRIORITY: ");
        Func<AppQuery, AppQuery> _presentingpriorityidText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("presentingpriorityid_cwname_7_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _casestatusidLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("casestatusid_cwname_8_Label").Text("CASE STATUS: ");
        Func<AppQuery, AppQuery> _casestatusidText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("casestatusid_cwname_8_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _secondarycasereasonidLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("secondarycasereasonid_cwname_9_Label").Text("SECONDARY CASE REASON: ");
        Func<AppQuery, AppQuery> _secondarycasereasonidText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("secondarycasereasonid_cwnamme_9_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _casenumberLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("casenumber_10_Label").Text("CASE NO: ");
        Func<AppQuery, AppQuery> _casenumberText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("casenumber_10_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _owneridLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("ownerid_cwname_11_Label").Text("RESPONSIBLE TEAM: ");
        Func<AppQuery, AppQuery> _owneridText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("ownerid_cwnamme_11_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _createdByLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("createdby_cwname_12_Label").Text("Created By: ");
        Func<AppQuery, AppQuery> _createdByText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("createdby_cwname_12_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _createdOnLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("createdon_13_Label").Text("Created On: ");
        Func<AppQuery, AppQuery> _createdOnText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("createdon_13_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _modifiedByLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("modifiedby_cwname_14_Label").Text("Modified By: ");
        Func<AppQuery, AppQuery> _modifiedByText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("modifiedby_cwname_14_Value").Text(ExpectedPersonName);

        Func<AppQuery, AppQuery> _modifiedOnLabel(string PersonID) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("modifiedon_15_Label").Text("Modified On: ");
        Func<AppQuery, AppQuery> _modifiedOnText(string PersonID, string ExpectedPersonName) => c => c.Marked("case_SecondaryRow_" + PersonID).Descendant().Marked("modifiedon_15_Value").Text(ExpectedPersonName);

        #endregion



        public CasesPage(IApp app)
        {
            _app = app;
        }




        public CasesPage VerifyFirstPageButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                bool firstPageDisabledButtonVisible = this._app.Query(_firstDisabledButton).Any();
                if (firstPageDisabledButtonVisible)
                    Assert.Fail("_firstDisabledButton is visible");

                bool firstPageButtonVisible = this._app.Query(_firstButton).Any();
                if (!firstPageButtonVisible)
                    Assert.Fail("_firstButton is not visible");

            }
            else
            {
                bool firstPageDisabledButtonVisible = this._app.Query(_firstDisabledButton).Any();
                if (!firstPageDisabledButtonVisible)
                    Assert.Fail("_firstDisabledButton is not visible");

                bool firstPageButtonVisible = this._app.Query(_firstButton).Any();
                if (firstPageButtonVisible)
                    Assert.Fail("_firstButton is visible");
            }
            return this;
        }
        
        public CasesPage VerifyPreviousPageButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                bool firstPageDisabledButtonVisible = this._app.Query(_previousDisabledButton).Any();
                Assert.IsFalse(firstPageDisabledButtonVisible, "_previousDisabledButton is visible");

                bool firstPageButtonVisible = this._app.Query(_previousButton).Any();
                Assert.IsTrue(firstPageButtonVisible, "_previousButton is not visible");
            }
            else
            {
                bool firstPageDisabledButtonVisible = this._app.Query(_previousDisabledButton).Any();
                Assert.IsTrue(firstPageDisabledButtonVisible, "_previousDisabledButton is not visible");

                bool firstPageButtonVisible = this._app.Query(_previousButton).Any();
                Assert.IsFalse(firstPageButtonVisible, "_previousButton is visible");
            }
            return this;
        }
        
        public CasesPage VerifyNextPageButtonEnabled(bool ExpectEnabled)
        {
            if (ExpectEnabled)
            {
                bool nextPageDisabledButtonVisible = this._app.Query(_nextDisabledButton).Any();
                Assert.IsFalse(nextPageDisabledButtonVisible, "_nextDisabledButton is visible");

                bool nextPageButtonVisible = this._app.Query(_nextButton).Any();
                Assert.IsTrue(nextPageButtonVisible, "_nextButton is not visible");
            }
            else
            {
                bool nextPageDisabledButtonVisible = this._app.Query(_nextDisabledButton).Any();
                Assert.IsTrue(nextPageDisabledButtonVisible, "_nextDisabledButton is not visible");

                bool nextPageButtonVisible = this._app.Query(_nextButton).Any();
                Assert.IsFalse(nextPageButtonVisible, "_nextButton is visible");
            }

            return this;
        }
        
        public CasesPage VerifyPaginationButtonInfo(string expectedPaginationText)
        {
            string paginationText = this._app.Query(_pageInfoButton).FirstOrDefault().Text;
            Assert.AreEqual(expectedPaginationText, paginationText);
            return this;
        }
        
        public CasesPage TapFistPageButton()
        {
            this._app.Tap(_firstButton);
            return this;
        }
        
        public CasesPage TapPreviousPageButton()
        {
            this._app.Tap(_previousButton);
            return this;
        }
        
        public CasesPage TapNextPageButton()
        {
            this._app.Tap(_nextButton);
            return this;
        }




        public CasesPage WaitForCasesPageToLoad(string ViewText = "Linked As An Involvement")
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_CasesPageIconButton);
            _app.WaitForElement(_pageTitle);

            _app.WaitForElement(_viewPicker(ViewText));

            _app.WaitForElement(_searchTextBox);
            _app.WaitForElement(_searchButton);
            _app.WaitForElement(_refreshButton);

            return this;
        }

        public CasesPage WaitForLoadSymbolToBeRemoved()
        {
            WaitForElementNotVisible(_LoadingSymbol);

            return this;
        }

        public CasesPage SelectRecentlyViewedRecordsView()
        {
            _app.Tap(_genericViewPicker);

            GenericPicklistPopup _pickList = new GenericPicklistPopup(this._app);

            _pickList
                .WaitForPicklistToLoad()
                .ScrollUpPickList(1)
                .TapOkButton();

            return this;
        }

        public CasesPage SelecMyActiveRecordsView()
        {
            _app.Tap(_genericViewPicker);

            GenericPicklistPopup _pickList = new GenericPicklistPopup(this._app);

            _pickList
                .WaitForPicklistToLoad()
                .ScrollDownPickList(1)
                .TapOkButton();

            return this;
        }

        public CasesPage TapViewPicker()
        {
            _app.Tap(_genericViewPicker);
            return this;
        }


        public CasesPage TypeInSearchTextBox(string Text)
        {
            _app.ClearText(_searchTextBox);
            _app.DismissKeyboard();

            _app.EnterText(_searchTextBox, Text);
            _app.DismissKeyboard();

            return this;
        }


        public CasesPage TapRefreshButton()
        {
            _app.Tap(_refreshButton);

            return this;
        }

        public CasesPage TapSearchButton()
        {
            _app.Tap(_searchButton);

            return this;
        }

        public CasesPage TapOnCaseRecordButton(string TextToFind, string CaseID)
        {
            string parentCellIdentifier = "case_Row_" + CaseID + "_Cell_personid";

            this._app.Tap(c => c.Marked(parentCellIdentifier).Descendant().Marked(TextToFind));

            return this;
        }



        public CasesPage VerifyThatTextVisible(string TextToFind)
        {
            bool _anyMatch = _app.Query(TextToFind).Any();

            if (!_anyMatch)
                Assert.Fail("The test framework didn´t found any element that maches the query. Query: " + TextToFind);

            

            return this;
        }

        public CasesPage VerifyThatTextVisible(string TextToFind, string CaseID)
        {
            string parentCellIdentifier = "case_Row_" + CaseID + "_Cell_";

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

        public CasesPage VerifyThatTextVisible(string TextToFind, string CaseID, string CellSuffix)
        {
            string parentCellIdentifier = "case_Row_" + CaseID + "_Cell_" + CellSuffix;

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

        public CasesPage VerifyThatTextNotVisible(string TextToFind)
        {
            TryScrollToElement(_screenText(TextToFind));

            bool _anyMatch = _app.Query(TextToFind).Any();

            if (_anyMatch)
                Assert.Fail("The test framework did found one or more elements that maches the query. Query: " + TextToFind);

            return this;
        }




        public CasesPage ValidatePersonCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "personid"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "personid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateContactReaonCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "contactreasonid"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "contactreasonid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateNHSNOCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "nhsnumber"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "nhsnumber", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateLastNameCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "lastname"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "lastname", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateFirstNameCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "firstname"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "firstname", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateDOBCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "dateofbirth"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "dateofbirth", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateDateTimeRequestRecievedCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "requestreceiveddatetime"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "requestreceiveddatetime", ExpectedText));

            if(string.IsNullOrEmpty(ExpectedText))
            { 
                Assert.IsFalse(elementVisible);
                return this;
            }

            Assert.IsTrue(elementVisible);

            return this;
        }
        
        public CasesPage ValidatePresentingPriorityCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "presentingpriorityid"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "presentingpriorityid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
            { 
                Assert.IsFalse(elementVisible);
                return this;
            }

            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, ""));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateCaseStatusCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "casestatusid"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "casestatusid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateSecundaryCaseReasonCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "secondarycasereasonid"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "secondarycasereasonid", ExpectedText));
            if(string.IsNullOrEmpty(ExpectedText))
            { 
                Assert.IsFalse(elementVisible);
                return this;
            }

            Assert.IsTrue(elementVisible);


            return this;
        }

        public CasesPage ValidateCaseNumberCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "casenumber"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "casenumber", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateResponsibleTeamCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "ownerid"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "ownerid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateCreatedByCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "createdby"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateCreatedOnCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "createdon"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "createdon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateModifiedByCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "modifiedby"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedby", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CasesPage ValidateModifiedOnCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_DefaultRender(RecordID, "modifiedon"));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "modifiedon", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }



        #region Mobile View Display

        public CasesPage TapOnCaseRecordButton_MobileView(string CaseID)
        {
            ScrollToElement(_recordIdentifier_MobileView(CaseID));
            Tap(_recordIdentifier_MobileView(CaseID));

            return this;
        }

        public CasesPage TapExpandCaseRecordButton_MobileView(string CaseID)
        {
            string parentCellIdentifier = "Case_Row_" + CaseID;

            _app.Tap(_toogleButton(parentCellIdentifier));

            return this;
        }

        public CasesPage ValidateCaseLabels_MobileView(string CaseID)
        {
            ScrollToElement(_personidLabel(CaseID));
            bool elementFound = _app.Query(_personidLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _personidLabel");

            ScrollToElement(_contactreasonidLabel(CaseID));
            elementFound = _app.Query(_contactreasonidLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _contactreasonidLabel");

            ScrollToElement(_nhsnumberLabel(CaseID));
            elementFound = _app.Query(_nhsnumberLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _nhsnumberLabel");

            ScrollToElement(_firstNameLabel(CaseID));
            elementFound = _app.Query(_firstNameLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _firstNameLabel");

            ScrollToElement(_lastNameLabel(CaseID));
            elementFound = _app.Query(_lastNameLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _lastNameLabel");

            ScrollToElement(_dateofbirthLabel(CaseID));
            elementFound = _app.Query(_dateofbirthLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _dateofbirthLabel");

            ScrollToElement(_requestreceiveddatetimeLabel(CaseID));
            elementFound = _app.Query(_requestreceiveddatetimeLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _requestreceiveddatetimeLabel");

            ScrollToElement(_presentingpriorityidLabel(CaseID));
            elementFound = _app.Query(_presentingpriorityidLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _dobLabel");

            ScrollToElement(_casestatusidLabel(CaseID));
            elementFound = _app.Query(_casestatusidLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _nhsNoLabel");

            ScrollToElement(_secondarycasereasonidLabel(CaseID));
            elementFound = _app.Query(_secondarycasereasonidLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _secondarycasereasonidLabel");




            ScrollToElement(_casenumberLabel(CaseID));
            elementFound = _app.Query(_casenumberLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _casenumberLabel");

            ScrollToElement(_owneridLabel(CaseID));
            elementFound = _app.Query(_owneridLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _owneridLabel");




            ScrollToElement(_createdByLabel(CaseID));
            elementFound = _app.Query(_createdByLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _createdByLabel");

            ScrollToElement(_createdOnLabel(CaseID));
            elementFound = _app.Query(_createdOnLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _createdOnLabel");

            ScrollToElement(_modifiedByLabel(CaseID));
            elementFound = _app.Query(_modifiedByLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _modifiedByLabel");

            ScrollToElement(_modifiedOnLabel(CaseID));
            elementFound = _app.Query(_modifiedOnLabel(CaseID)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _modifiedOnLabel");

            return this;
        }

        public CasesPage ValidatePersonText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_personidLabel(CaseID));
            bool elementFound = this._app.Query(_personidText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _personidLabel");

            return this;
        }

        public CasesPage ValidateContactReasonText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_contactreasonidLabel(CaseID));
            bool elementFound = this._app.Query(_contactreasonidText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _contactreasonidText");

            return this;
        }

        public CasesPage ValidateIdText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_nhsnumberLabel(CaseID));
            bool elementFound = this._app.Query(_nhsnumberText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _nhsnumberText");

            return this;
        }

        public CasesPage ValidateFirstNameText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_firstNameLabel(CaseID));
            bool elementFound = this._app.Query(_firstNameText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _firstNameText");

            return this;
        }

        public CasesPage ValidateLastNameText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_lastNameLabel(CaseID));
            bool elementFound = this._app.Query(_lastNameText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _lastNameText");

            return this;
        }

        public CasesPage ValidateRepresentsHazzardText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_dateofbirthLabel(CaseID));
            bool elementFound = this._app.Query(_dateofbirthText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _dateofbirthText");

            return this;
        }

        public CasesPage ValidateRequestReceivedDateTimeText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_requestreceiveddatetimeLabel(CaseID));
            bool elementFound = this._app.Query(_requestreceiveddatetimeText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _requestreceiveddatetimeLabel");

            return this;
        }

        public CasesPage ValidateDOBText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_presentingpriorityidLabel(CaseID));
            bool elementFound = this._app.Query(_presentingpriorityidText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _dobLabel");

            return this;
        }

        public CasesPage ValidateNHSNoText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_casestatusidLabel(CaseID));
            bool elementFound = this._app.Query(_casestatusidText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _nhsNoText");

            return this;
        }

        public CasesPage ValidateSecondaryCaseReasonIdText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_secondarycasereasonidLabel(CaseID));
            bool elementFound = this._app.Query(_secondarycasereasonidText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _secondarycasereasonidText");

            return this;
        }

        public CasesPage ValidateCaseNumberText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_casenumberLabel(CaseID));
            bool elementFound = this._app.Query(_casenumberText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _casenumberText");

            return this;
        }


        public CasesPage ValidateResponsibleTeamText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_owneridLabel(CaseID));
            bool elementFound = this._app.Query(_owneridText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _owneridText");

            return this;
        }



        public CasesPage ValidateCreatedByText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_createdByLabel(CaseID));
            bool elementFound = this._app.Query(_createdByText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _createdByText");

            return this;
        }

        public CasesPage ValidateCreatedOnText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_createdOnLabel(CaseID));
            bool elementFound = this._app.Query(_createdOnText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _createdOnText");

            return this;
        }

        public CasesPage ValidateModifiedByText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_modifiedByLabel(CaseID));
            bool elementFound = this._app.Query(_modifiedByText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _modifiedByText");

            return this;
        }

        public CasesPage ValidateMOdifiedOnText_MobileView(string CaseID, string ExpectedText)
        {
            ScrollToElement(_modifiedOnLabel(CaseID));
            bool elementFound = this._app.Query(_modifiedOnText(CaseID, ExpectedText)).Any();
            if (!elementFound)
                throw new Exception("Element not found: _modifiedOnText");

            return this;
        }



        public CasesPage ValidateContactReasonTextViewNotVisible_MobileView(string CaseID)
        {
            bool elementVisible = this._app.Query(c => c.Marked("Case_PrimaryRow_" + CaseID).Descendant().Marked("contactreasonid_1_Value")).Any();
            Assert.IsFalse(elementVisible);

            return this;
        }

        public CasesPage ValidateFirstNameTextViewNotVisible_MobileView(string CaseID)
        {
            bool elementVisible = this._app.Query(c => c.Marked("Case_SecondaryRow_" + CaseID).Descendant().Marked("firstname_3_Value")).Any();
            Assert.IsFalse(elementVisible);

            return this;
        }

        public CasesPage ValidateNHSNoTextViewNotVisible_MobileView(string CaseID)
        {
            bool elementVisible = this._app.Query(c => c.Marked("Case_SecondaryRow_" + CaseID).Descendant().Marked("nhsnumber_8_Value")).Any();
            Assert.IsFalse(elementVisible);

            return this;
        }

        public CasesPage ValidatePostCodeTextViewNotVisible_MobileView(string CaseID)
        {
            bool elementVisible = this._app.Query(c => c.Marked("Case_SecondaryRow_" + CaseID).Descendant().Marked("postcode_9_Value")).Any();
            Assert.IsFalse(elementVisible);

            return this;
        }


        #endregion


    }
}
