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
    public class PersonBodyMapReviewPopup : CommonMethods
    {



        readonly Func<AppQuery, AppQuery> _popupTitle = e => e.Marked("HeaderLabel");
        readonly Func<AppQuery, AppQuery> _closeButton = e => e.Marked("CloseWindowLink");


        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("personbodymapreview_SaveButton");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personbodymapreview_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("personbodymapreview_TextToSpeechStopButton");

        Func<AppQuery, AppQuery> _recordName(string RecordName) => e => e.Marked(RecordName);


        #region Fields titles

        readonly Func<AppQuery, AppQuery> _dateTimeOfPlannedReview_FieldTitle = e => e.Marked("Date/Time of Planned Review");
        readonly Func<AppQuery, AppQuery> _dateTimeOfActualReview_FieldTitle = e => e.Marked("Date/Time of Actual review");
        readonly Func<AppQuery, AppQuery> _isReviewRequired_FieldTitle = e => e.Marked("Is Review Required?");
        readonly Func<AppQuery, AppQuery> _DateTimeOfNextReview_FieldTitle = e => e.Marked("Date/Time of Next Review");

        readonly Func<AppQuery, AppQuery> _professionalComments_FieldTitle = e => e.Marked("Professional Comments");
        readonly Func<AppQuery, AppQuery> _clientComments_FieldTitle = e => e.Marked("Client Comments");
        readonly Func<AppQuery, AppQuery> _agreedOutcome_FieldTitle = e => e.Marked("Agreed Outcome");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.Marked("ContentLayout").Descendant().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.Marked("ContentLayout").Descendant().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.Marked("ContentLayout").Descendant().Marked("MODIFIED BY");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.Marked("ContentLayout").Descendant().Marked("MODIFIED ON");

        #endregion

        #region Fields


        readonly Func<AppQuery, AppQuery> _dateTimeOfPlannedReview_Field = e => e.Marked("Field_5b6a9439ec5eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _dateTimeOfActualReview_Field = e => e.Marked("Field_e3964143ec5eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _dateTimeOfActualReview_DateField = e => e.Marked("Field_e3964143ec5eea11a2ca0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _dateTimeOfActualReview_TimeField = e => e.Marked("Field_e3964143ec5eea11a2ca0050569231cf_Time");
        readonly Func<AppQuery, AppQuery> _isReviewRequired_Field = e => e.Marked("Field_46b27353ec5eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _DateTimeOfNextReview_Field = e => e.Marked("Field_c52f1867ec5eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _DateTimeOfNextReview_DateField = e => e.Marked("Field_c52f1867ec5eea11a2ca0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _DateTimeOfNextReview_TimeField = e => e.Marked("Field_c52f1867ec5eea11a2ca0050569231cf_Time");


        readonly Func<AppQuery, AppQuery> _professionalComments_ScrollField = e => e.All().Marked("Field_c758ea8cec5eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _clientComments_ScrollField = e => e.All().Marked("Field_a7c63294ec5eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _agreedOutcome_ScrollField = e => e.All().Marked("Field_abc63294ec5eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _professionalComments_Field = e => e.Marked("Field_c758ea8cec5eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _clientComments_Field = e => e.Marked("Field_a7c63294ec5eea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _agreedOutcome_Field = e => e.Marked("Field_abc63294ec5eea11a2ca0050569231cf");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.Marked("ContentLayout").Descendant().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.Marked("ContentLayout").Descendant().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.Marked("ContentLayout").Descendant().Marked("FooterLabel_modifiedon");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.Marked("ContentLayout").Descendant().Marked("FooterLabel_modifiedby");

        #endregion



        public PersonBodyMapReviewPopup(IApp app)
        {
            _app = app;
        }


        public PersonBodyMapReviewPopup WaitForPersonBodyMapReviewPopupToLoad(string ReviewRecordName)
        {
            WaitForElement(_popupTitle);
            WaitForElement(_closeButton);

            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_recordName(ReviewRecordName));

            return this;
        }



        public PersonBodyMapReviewPopup ValidateDateTimeOfPlannedReviewFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_dateTimeOfPlannedReview_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_dateTimeOfPlannedReview_FieldTitle));
            }
            else
            {
                TryScrollToElement(_dateTimeOfPlannedReview_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_dateTimeOfPlannedReview_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapReviewPopup ValidateDateTimeOfActualReviewFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_dateTimeOfActualReview_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_dateTimeOfActualReview_FieldTitle));
            }
            else
            {
                TryScrollToElement(_dateTimeOfActualReview_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_dateTimeOfActualReview_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapReviewPopup ValidateIsReviewRequiredFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonBodyMapReviewPopup ValidateDateTimeOfNextReviewFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DateTimeOfNextReview_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_DateTimeOfNextReview_FieldTitle));
            }
            else
            {
                TryScrollToElement(_DateTimeOfNextReview_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_DateTimeOfNextReview_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapReviewPopup ValidateProfessionalCommentsFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_professionalComments_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_professionalComments_FieldTitle));
            }
            else
            {
                TryScrollToElement(_professionalComments_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_dateTimeOfPlannedReview_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapReviewPopup ValidateClientCommentsFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_clientComments_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_clientComments_FieldTitle));
            }
            else
            {
                TryScrollToElement(_clientComments_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_clientComments_FieldTitle));
            }

            return this;
        }

        public PersonBodyMapReviewPopup ValidateAgreedOutcomeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_agreedOutcome_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_agreedOutcome_FieldTitle));
            }
            else
            {
                TryScrollToElement(_agreedOutcome_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_agreedOutcome_FieldTitle));
            }

            return this;
        }


        public PersonBodyMapReviewPopup ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public PersonBodyMapReviewPopup ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public PersonBodyMapReviewPopup ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public PersonBodyMapReviewPopup ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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




        public PersonBodyMapReviewPopup ValidateDateTimeOfPlannedReviewFieldValue(string ExpectValue)
        {
            ScrollToElement(_dateTimeOfPlannedReview_Field);
            Assert.AreEqual(ExpectValue, GetElementText(_dateTimeOfPlannedReview_Field));

            return this;
        }

        public PersonBodyMapReviewPopup ValidateDateTimeOfActualReviewFieldValue(string ExpectValue)
        {
            ScrollToElement(_dateTimeOfActualReview_Field);
            Assert.AreEqual(ExpectValue, GetElementText(_dateTimeOfActualReview_Field));

            return this;
        }

        public PersonBodyMapReviewPopup ValidateIsReviewRequiredFieldValue(string ExpectValue)
        {
            ScrollToElement(_isReviewRequired_Field);
            Assert.AreEqual(ExpectValue, GetElementText(_isReviewRequired_Field));

            return this;
        }

        public PersonBodyMapReviewPopup ValidateDateTimeOfNextReviewFieldValue(string ExpectValue)
        {
            ScrollToElement(_DateTimeOfNextReview_Field);
            Assert.AreEqual(ExpectValue, GetElementText(_DateTimeOfNextReview_Field));

            return this;
        }

        public PersonBodyMapReviewPopup ValidateProfessionalCommentsFieldValue(string ExpectValue)
        {
            ScrollToElement(_professionalComments_ScrollField);
            Assert.AreEqual(ExpectValue, GetElementText(_professionalComments_Field));

            return this;
        }

        public PersonBodyMapReviewPopup ValidateClientCommentsFieldValue(string ExpectValue)
        {
            ScrollToElement(_clientComments_ScrollField);
            Assert.AreEqual(ExpectValue, GetElementText(_clientComments_Field));

            return this;
        }

        public PersonBodyMapReviewPopup ValidateAgreedOutcomeFieldValue(string ExpectValue)
        {
            ScrollToElement(_agreedOutcome_ScrollField);
            Assert.AreEqual(ExpectValue, GetElementText(_agreedOutcome_Field));

            return this;
        }


        public PersonBodyMapReviewPopup ValidateCreatedByFieldValue(string ExpectText)
        {
            ScrollToElement(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapReviewPopup ValidateCreatedOnFieldValue(string ExpectText)
        {
            ScrollToElement(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapReviewPopup ValidateModifieddByFieldValue(string ExpectText)
        {
            ScrollToElement(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyMapReviewPopup ValidateModifieddOnFieldValue(string ExpectText)
        {
            ScrollToElement(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }


        public PersonBodyMapReviewPopup InsertDateTimeOfActualReview(string DateValue, string TimeValue)
        {
            ScrollToElement(_dateTimeOfActualReview_DateField);
            EnterText(_dateTimeOfActualReview_DateField, DateValue);
            EnterText(_dateTimeOfActualReview_TimeField, TimeValue);

            return this;
        }

        public PersonBodyMapReviewPopup TapIsReviewRequiredField()
        {
            ScrollToElement(_isReviewRequired_Field);
            Tap(_isReviewRequired_Field);

            return this;
        }

        public PersonBodyMapReviewPopup InsertDateTimeOfNextReview(string DateValue, string TimeValue)
        {
            ScrollToElement(_DateTimeOfNextReview_DateField);
            EnterText(_DateTimeOfNextReview_DateField, DateValue);
            EnterText(_DateTimeOfNextReview_TimeField, TimeValue);

            return this;
        }

        public PersonBodyMapReviewPopup InsertProfessionalComments(string ValueToInsert)
        {
            ScrollToElementWithWidthAndHeight(_professionalComments_ScrollField);
            this.EnterText(_professionalComments_Field, ValueToInsert);

            return this;
        }

        public PersonBodyMapReviewPopup InsertClientComments(string ValueToInsert)
        {
            ScrollToElementWithWidthAndHeight(_clientComments_ScrollField);
            this.EnterText(_clientComments_Field, ValueToInsert);

            return this;
        }

        public PersonBodyMapReviewPopup InsertAgreedOutcome(string ValueToInsert)
        {
            ScrollToElementWithWidthAndHeight(_agreedOutcome_ScrollField);
            this.EnterText(_agreedOutcome_Field, ValueToInsert);

            return this;
        }

        public PersonBodyMapReviewPopup ClosePopupIfOpen()
        {
            bool elementVisible = CheckIfElementVisible(_closeButton);

            if(elementVisible)
                TryTap(_closeButton);

            return this;
        }

        public PersonBodyMapReviewPopup TapSaveButton()
        {
            bool elementVisible = CheckIfElementVisible(_saveButton);

            if (elementVisible)
                TryTap(_saveButton);

            return this;
        }

    }
}
