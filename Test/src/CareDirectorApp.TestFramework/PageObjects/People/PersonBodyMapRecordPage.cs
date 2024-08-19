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
    public class PersonBodyMapRecordPage : CommonMethods
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

        Func<AppQuery, AppQuery> _bodyMapInjuryDescriptionRecordTitle_Field(string bodyMapInjuryDescriptionRecordTitle) => e => e.Marked(bodyMapInjuryDescriptionRecordTitle);
        Func<AppQuery, AppQuery> _bodyMapInjuryDescriptionRecordDescription_Field(string bodyMapInjuryDescriptionRecordDescription) => e => e.Marked(bodyMapInjuryDescriptionRecordDescription);

        readonly Func<AppQuery, AppQuery> _isReviewRequired_Field = e => e.All().Marked("Field_99ad9187d05eea11a2ca0050569231cf");
        
        readonly Func<AppQuery, AppQuery> _dateTimeOfreview_DateField = e => e.Marked("Field_83dfb292d05eea11a2ca0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _dateTimeOfreview_TimeField = e => e.Marked("Field_83dfb292d05eea11a2ca0050569231cf_Time");



        Func<AppQuery, AppQuery> _reviewRecordTitle_Field(string Title) => e => e.Marked(Title);
        Func<AppQuery, AppQuery> _reviewRecordSubTitle_Field(string SubTitle) => e => e.Marked(SubTitle);


        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.Marked("FooterLabel_createdby").Index(3);
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.Marked("FooterLabel_createdon").Index(3);
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.Marked("FooterLabel_modifiedby").Index(3);
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.Marked("FooterLabel_modifiedon").Index(3);

        #endregion



        public PersonBodyMapRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonBodyMapRecordPage WaitForPersonBodyMapRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);
            
            return this;
        }

        public PersonBodyMapRecordPage WaitForPersonBodyMapRecordPageToLoadAfterFirstSave(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);


            return this;
        }



        public PersonBodyMapRecordPage ValidateDateOfEventFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_dateOfEvent_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_dateOfEvent_FieldTitle));
            }
            else
            {
                TryScrollToElement(_dateOfEvent_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_dateOfEvent_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateViewTypeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_viewType_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_viewType_FieldTitle));
            }
            else
            {
                TryScrollToElement(_viewType_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_viewType_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateBodyAreaLookupButtonVisible(bool ExpectButtonVisible)
        {
            if (ExpectButtonVisible)
            {
                ScrollToElement(_bodyAreaDefaultImage);
                Assert.IsTrue(CheckIfElementVisible(_bodyAreaDefaultImage));
            }
            else
            {
                TryScrollToElement(_bodyAreaDefaultImage);
                Assert.IsFalse(CheckIfElementVisible(_bodyAreaDefaultImage));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateBodyAreaDefaultImageVisible(bool ExpectDefaultImageVisible)
        {
            if (ExpectDefaultImageVisible)
            {
                ScrollToElement(_bodyAreaLookupButton);
                Assert.IsTrue(CheckIfElementVisible(_bodyAreaLookupButton));
            }
            else
            {
                TryScrollToElement(_bodyAreaLookupButton);
                Assert.IsFalse(CheckIfElementVisible(_bodyAreaLookupButton));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateInjuryDescriptionFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_bodyMapInjuryDescriptions_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_bodyMapInjuryDescriptions_FieldTitle));
            }
            else
            {
                TryScrollToElement(_bodyMapInjuryDescriptions_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_bodyMapInjuryDescriptions_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateIsReviewRequiredFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_isReviewRequired_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_isReviewRequired_FieldTitle));
            }
            else
            {
                TryScrollToElement(_isReviewRequired_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_isReviewRequired_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateReviewsTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_reviews_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_reviews_FieldTitle));
            }
            else
            {
                TryScrollToElement(_reviews_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_reviews_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_createdBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdBy_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_createdOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdOn_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_modifiedBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_modifiedOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }

            return this;
        }



        public PersonBodyMapRecordPage ValidateDateOfEventFieldText(string ExpectText)
        {
            ScrollToElement(_dateOfEvent_Field);
            string fieldText = GetElementText(_dateOfEvent_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateViewTypeFieldText(string ExpectText)
        {
            ScrollToElement(_viewType_Field);
            string fieldText = GetElementText(_viewType_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        /// <summary>
        /// this field is only displayed when we open a body map (not finished/disabled). This is the text field id displayed by the APP before the user selects a body area. 
        /// When the user selects a body area (using the lookup button) the field id is changed to Field_bodyareadescription.
        /// To validate the field with id Field_bodyareadescription use the method "ValidateDescriptionOfIssueField" instead of this one.
        /// 
        /// </summary>
        /// <param name="ExpectText"></param>
        /// <returns></returns>
        public PersonBodyMapRecordPage ValidateEmptyDescriptionOfIssueField(string ExpectText)
        {
            ScrollToElement(_emptyDescriptionOfIssueField);
            string fieldText = GetElementText(_emptyDescriptionOfIssueField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateDescriptionOfIssueField(string ExpectText)
        {
            ScrollToElement(_descriptionOfIssueField);
            string fieldText = GetElementText(_descriptionOfIssueField);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateInjuryDescriptionRecordTitle(string RecordTitle)
        {
            ScrollToElement(_bodyMapInjuryDescriptionRecordTitle_Field(RecordTitle));
            string fieldText = GetElementText(_bodyMapInjuryDescriptionRecordTitle_Field(RecordTitle));
            Assert.AreEqual(RecordTitle, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateInjuryDescriptionRecordDescription(string RecordDescription)
        {
            ScrollToElement(_bodyMapInjuryDescriptionRecordDescription_Field(RecordDescription));
            string fieldText = GetElementText(_bodyMapInjuryDescriptionRecordDescription_Field(RecordDescription));
            Assert.AreEqual(RecordDescription, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateIsReviewRequiriedFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_isReviewRequired_Field);
            string fieldText = GetElementText(_isReviewRequired_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateReviewRecordTitle(string RecordTitle)
        {
            ScrollToElement(_reviewRecordTitle_Field(RecordTitle));
            string fieldText = GetElementText(_reviewRecordTitle_Field(RecordTitle));
            Assert.AreEqual(RecordTitle, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateReviewRecordSubTitle(string RecordSubTitle)
        {
            ScrollToElement(_reviewRecordSubTitle_Field(RecordSubTitle));
            string fieldText = GetElementText(_reviewRecordSubTitle_Field(RecordSubTitle));
            Assert.AreEqual(RecordSubTitle, fieldText);

            return this;
        }


        public PersonBodyMapRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElement(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElement(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElement(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElement(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }


        public PersonBodyMapRecordPage InsertDateOfEvent(string DateValue, string TimeValue)
        {
            ScrollToElement(_dateOfEvent_DateField);
            this.EnterText(_dateOfEvent_DateField, DateValue);
            this.EnterText(_dateOfEvent_TimeField, TimeValue);

            return this;
        }

        public PersonBodyMapRecordPage TapBodyAreaLookupButton()
        {
            ScrollToElement(_bodyAreaLookupButton);
            Tap(_bodyAreaLookupButton);
            
            return this;
        }

        public PersonBodyMapRecordPage InsertDescriptionOfIssueInjurySymptom(string ValueToInsert)
        {
            ScrollToElement(_descriptionOfIssueField);
            this.EnterText(_descriptionOfIssueField, ValueToInsert);

            return this;
        }

        public PersonBodyMapRecordPage TapIsReviewrequiredField()
        {
            ScrollToElementWithWidthAndHeight(_isReviewRequired_Field);
            TapOnElementWithWidthAndHeight(_isReviewRequired_Field);

            return this;
        }

        public PersonBodyMapRecordPage InsertDateTimeOfReview(string DateValue, string TimeValue)
        {
            ScrollToElement(_dateTimeOfreview_DateField);
            this.EnterText(_dateTimeOfreview_DateField, DateValue);
            this.EnterText(_dateTimeOfreview_TimeField, TimeValue);

            return this;
        }

        public PersonBodyMapRecordPage TapOnInjuryDescriptionRecord(string RecordTitle)
        {
            ScrollToElement(_bodyMapInjuryDescriptionRecordTitle_Field(RecordTitle));
            Tap(_bodyMapInjuryDescriptionRecordTitle_Field(RecordTitle));

            return this;
        }

        public PersonBodyMapRecordPage TapOnReviewRecord(string RecordTitle)
        {
            ScrollToElement(_reviewRecordTitle_Field(RecordTitle));
            Tap(_reviewRecordTitle_Field(RecordTitle));

            return this;
        }

        public PersonBodyMapRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public PersonBodyMapRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public PersonBodyMapRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }

        public PersonBodyMapRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public PersonBodyMapRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }


    }
}
