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
    public class PersonBodyInjuryDescriptionPopup : CommonMethods
    {



        readonly Func<AppQuery, AppQuery> _popupTitle = e => e.Marked("HeaderLabel");
        readonly Func<AppQuery, AppQuery> _closeButton = e => e.Marked("CloseWindowLink");

        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("personbodymapinjurydescription_SaveButton");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personbodymapinjurydescription_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("personbodymapinjurydescription_TextToSpeechStopButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("personbodymapinjurydescription_DeleteRecordButton");

        Func<AppQuery, AppQuery> _recordName(string RecordName) => e => e.Marked(RecordName);


        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Name_FieldTitle = e => e.Marked("Name");
        readonly Func<AppQuery, AppQuery> _PersonBodyMap_FieldTitle = e => e.Marked("Person Body Map");
        readonly Func<AppQuery, AppQuery> _Description_FieldTitle = e => e.Marked("Description");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _BodyMapSetup_FieldTitle = e => e.Marked("Body Map Setup");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.Marked("ContentLayout").Descendant().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.Marked("ContentLayout").Descendant().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.Marked("ContentLayout").Descendant().Marked("MODIFIED BY");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.Marked("ContentLayout").Descendant().Marked("MODIFIED ON");

        #endregion

        #region Fields


        readonly Func<AppQuery, AppQuery> _Name_Field = e => e.Marked("Field_db84db2205aae81180dc0050560502cc");
        readonly Func<AppQuery, AppQuery> _PersonBodyMap_Field = e => e.Marked("Field_c79336baa25fea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _Description_Field = e => e.Marked("Field_eb9142f8a25fea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_e5db6407a35fea11a2ca0050569231cf");
        readonly Func<AppQuery, AppQuery> _BodyMapSetup_Field = e => e.Marked("Field_9336cd0da35fea11a2ca0050569231cf");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.Marked("ContentLayout").Descendant().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.Marked("ContentLayout").Descendant().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.Marked("ContentLayout").Descendant().Marked("FooterLabel_modifiedon");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.Marked("ContentLayout").Descendant().Marked("FooterLabel_modifiedby");

        #endregion



        public PersonBodyInjuryDescriptionPopup(IApp app)
        {
            _app = app;
        }


        public PersonBodyInjuryDescriptionPopup WaitForPersonBodyInjuryDescriptionPopupToLoad(string ReviewRecordName)
        {
            WaitForElement(_popupTitle);
            WaitForElement(_closeButton);

            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_deleteButton);
            WaitForElement(_recordName(ReviewRecordName));

            return this;
        }



        public PersonBodyInjuryDescriptionPopup ValidateNameFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Name_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Name_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Name_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Name_FieldTitle));
            }

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidatePersonBodymapFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_PersonBodyMap_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_PersonBodyMap_FieldTitle));
            }
            else
            {
                TryScrollToElement(_PersonBodyMap_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_PersonBodyMap_FieldTitle));
            }

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateDescriptionFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Description_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Description_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Description_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Description_FieldTitle));
            }

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateResponsibleTeamFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ResponsibleTeam_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ResponsibleTeam_FieldTitle));
            }
            else
            {
                TryScrollToElement(_ResponsibleTeam_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ResponsibleTeam_FieldTitle));
            }

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateBodyMapSetupFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_BodyMapSetup_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_BodyMapSetup_FieldTitle));
            }
            else
            {
                TryScrollToElement(_BodyMapSetup_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_BodyMapSetup_FieldTitle));
            }

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public PersonBodyInjuryDescriptionPopup ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public PersonBodyInjuryDescriptionPopup ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public PersonBodyInjuryDescriptionPopup ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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




        public PersonBodyInjuryDescriptionPopup ValidateNameFieldValue(string ExpectValue)
        {
            ScrollToElement(_Name_Field);
            Assert.AreEqual(ExpectValue, GetElementText(_Name_Field));

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidatePersonBodyMapFieldValue(string ExpectValue)
        {
            ScrollToElement(_PersonBodyMap_Field);
            Assert.AreEqual(ExpectValue, GetElementText(_PersonBodyMap_Field));

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateDescriptionFieldValue(string ExpectValue)
        {
            ScrollToElement(_Description_Field);
            Assert.AreEqual(ExpectValue, GetElementText(_Description_Field));

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateResponsibleTeamFieldValue(string ExpectValue)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            Assert.AreEqual(ExpectValue, GetElementText(_ResponsibleTeam_Field));

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateBodyMapSetupFieldValue(string ExpectValue)
        {
            ScrollToElement(_BodyMapSetup_Field);
            Assert.AreEqual(ExpectValue, GetElementText(_BodyMapSetup_Field));

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElement(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElement(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElement(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElement(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




        public PersonBodyInjuryDescriptionPopup InsertDescriptionReview(string ValueToInsert)
        {
            ScrollToElement(_Description_Field);
            EnterText(_Description_Field, ValueToInsert);

            return this;
        }

        public PersonBodyInjuryDescriptionPopup TapSaveButton()
        {
            ScrollToElement(_saveButton);
            Tap(_saveButton);

            return this;
        }

        public PersonBodyInjuryDescriptionPopup TapDeleteButton()
        {
            ScrollToElement(_deleteButton);
            Tap(_deleteButton);

            return this;
        }

        public PersonBodyInjuryDescriptionPopup ClosePopupIfOpen()
        {
            var elementVisible = CheckIfElementVisible(_closeButton);
            if (elementVisible)
                TryTap(_closeButton);

            return this;
        }
    }
}
