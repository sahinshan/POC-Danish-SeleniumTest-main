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
    public class PersonDNARRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personbodymap_SpeechStart");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("personbodymap_SpeechStop");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("personbodymap_DeleteRecordButton");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("personbodymap_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("personbodymap_SaveAndCloseButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");

        readonly Func<AppQuery, AppQuery> _generalSectionArea = e => e.Marked("46b925f7-f9a9-e811-80dc-0050560502cc_Section");
        readonly Func<AppQuery, AppQuery> _bodyMapSectionArea = e => e.Marked("7d11ce63-d05e-ea11-a2ca-0050569231cf_Section");

        



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _dateOfEvent_FieldTitle = e => e.Marked("Date Of Event");
        readonly Func<AppQuery, AppQuery> _viewType_FieldTitle = e => e.Marked("View Type");
        readonly Func<AppQuery, AppQuery> _bodyMapInjuryDescriptions_FieldTitle = e => e.Marked("Body Map Injury Descriptions");
        readonly Func<AppQuery, AppQuery> _isReviewRequired_FieldTitle = e => e.Marked("Is Review Required?");
        readonly Func<AppQuery, AppQuery> _reviews_FieldTitle = e => e.Marked("Reviews");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.Marked("CREATED ON").Index(3);
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.Marked("CREATED BY").Index(3);
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.Marked("MODIFIED ON").Index(3);
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.Marked("MODIFIED BY").Index(3);

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _dateOfEvent_DateField = e => e.Marked("Field_306bf747d05eea11a2ca0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _dateOfEvent_TimeField = e => e.Marked("Field_306bf747d05eea11a2ca0050569231cf_Time");

        readonly Func<AppQuery, AppQuery> _dateOfEvent_Field = e => e.Marked("Field_306bf747d05eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _viewType_Field = e => e.Marked("Field_5b1d426dd05eea11a2ca0050569231cf");

        readonly Func<AppQuery, AppQuery> _emptyDescriptionOfIssueField = e => e.Marked("_LookupEntry");
        readonly Func<AppQuery, AppQuery> _bodyAreaLookupButton = e => e.Marked("_OpenLookup");
        readonly Func<AppQuery, AppQuery> _bodyAreaDefaultImage = e => e.Marked("DefaultImage");
        readonly Func<AppQuery, AppQuery> _descriptionOfIssueField = e => e.Marked("Field_bodyareadescription");

        Func<AppQuery, AppQuery> _DNARActiveRecordTitle_Field(string DNARActiveRecordTitle) => e => e.Marked(DNARActiveRecordTitle);
        Func<AppQuery, AppQuery> _bodyMapInjuryDescriptionRecordDescription_Field(string bodyMapInjuryDescriptionRecordDescription) => e => e.Marked(bodyMapInjuryDescriptionRecordDescription);

        readonly Func<AppQuery, AppQuery> _isReviewRequired_Field = e => e.All().Marked("Field_99ad9187d05eea11a2ca0050569231cf");
        
        readonly Func<AppQuery, AppQuery> _dateTimeOfreview_DateField = e => e.Marked("Field_83dfb292d05eea11a2ca0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _dateTimeOfreview_TimeField = e => e.Marked("Field_83dfb292d05eea11a2ca0050569231cf_Time");



        Func<AppQuery, AppQuery> _reviewRecordTitle_Field(string Title) => e => e.Marked(Title);
        Func<AppQuery, AppQuery> _reviewRecordSubTitle_Field(string SubTitle) => e => e.Marked(SubTitle);
        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked("ListLayoutContentView").Descendant().Text("Person: "+RecordText);

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.Marked("FooterLabel_createdby").Index(3);
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.Marked("FooterLabel_createdon").Index(3);
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.Marked("FooterLabel_modifiedby").Index(3);
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.Marked("FooterLabel_modifiedon").Index(3);


        readonly Func<AppQuery, AppQuery> _ViewOptionsLookupButton = e => e.Marked("Active Records");
        readonly Func<AppQuery, AppQuery> _InactiveViewOptionsLookupButton = e => e.Marked("Inactive Records");

        Func<AppQuery, AppQuery> _record_Cell(string RecordID,string CellText) => e => e.Marked("Field_" + RecordID).Marked(CellText);
        #endregion



        public PersonDNARRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonDNARRecordPage WaitForPersonDNARRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);
            
            return this;
        }

        public PersonDNARRecordPage WaitForPersonDNARRecordPageToLoadAfterFirstSave(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);


            return this;
        }


      

        public PersonDNARRecordPage TapViewOptionsLookupButton()
        {
            ScrollToElement(_ViewOptionsLookupButton);
            Tap(_ViewOptionsLookupButton);
            
            return this;
        }

        public PersonDNARRecordPage TapInactiveViewOptionsLookupButton()
        {
            ScrollToElement(_InactiveViewOptionsLookupButton);
            Tap(_InactiveViewOptionsLookupButton);

            return this;
        }


        

        public PersonDNARRecordPage TapOnActiveDNARRecord(string TextToFind, string PersonID)
        {
            
            string parentCellIdentifier = "persondnar_Row_" + PersonID + "_Cell_personid"; 
           
            this._app.Tap(c => c.Marked(parentCellIdentifier));

            return new PersonDNARRecordPage(this._app);

            
        }

        public PersonDNARRecordPage ValidateRecordPresent(string recordText)
        {
            bool _anyMatch = _app.Query(_recordText(recordText)).Any();

            if (!_anyMatch)
                Assert.Fail("The test framework didn´t found any element that maches the query. Query: " + _recordText(recordText));

            //WaitForElement(_recordText(recordText));

            return this;
            

            return this;
        }


        public PersonDNARRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public PersonDNARRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public PersonDNARRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }

        public PersonDNARRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public PersonDNARRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

        public PersonDNARRecordPage ValidatePersonCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }
    }
}
