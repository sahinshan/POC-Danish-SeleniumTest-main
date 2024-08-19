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
    public class PersonAllergyRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personallergy_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("personallergy_TextToSpeechStopButton");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");



        #region Fields titles

        readonly Func<AppQuery, AppQuery> _AllergyType_FieldTitle = e => e.Marked("Allergy Type");
        readonly Func<AppQuery, AppQuery> _Level_FieldTitle = e => e.Marked("Level");
        readonly Func<AppQuery, AppQuery> _AllergenWhatSubstanceCausedTheReaction_FieldTitle = e => e.Marked("Allergen – What Substance Caused the Reaction");
        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _EndDate_FieldTitle = e => e.Marked("End Date");
        readonly Func<AppQuery, AppQuery> _Description_FieldTitle = e => e.Marked("Description");


        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _AllergyType_Field = e => e.Marked("Field_9539608f2281e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _Level_Field = e => e.Marked("Field_45e0bccd2281e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _AllergenWhatSubstanceCausedTheReaction_Field = e => e.Marked("Field_6c92fbe72281e911a2c50050569231cf");
        readonly Func<AppQuery, AppQuery> _StartDate_DateField = e => e.Marked("Field_a6f588bf2281e911a2c50050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _EndDate_Field = e => e.All().Marked("Field_14241beecc86e911a2c50050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _Description_Field = e => e.Marked("Field_fb8b50f12281e911a2c50050569231cf");



        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion

        #region Menu

        readonly Func<AppQuery, AppQuery> _RelatedItemsButton = e => e.Marked("RelatedItemsButton");
        readonly Func<AppQuery, AppQuery> _AllergicReactionsButton = e => e.Marked("RelatedItems_Item_AllergicReactions");

        #endregion



        public PersonAllergyRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonAllergyRecordPage WaitForPersonAllergyRecordPageToLoad(string PageTitleText)
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



        public PersonAllergyRecordPage ValidateAllergyTypeFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AllergyType_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AllergyType_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AllergyType_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AllergyType_FieldTitle));
            }

            return this;
        }

        public PersonAllergyRecordPage ValidateLevelFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Level_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Level_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Level_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Level_FieldTitle));
            }

            return this;
        }

        public PersonAllergyRecordPage ValidateAllergenWhatSubstanceCausedTheReactionFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_AllergenWhatSubstanceCausedTheReaction_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_AllergenWhatSubstanceCausedTheReaction_FieldTitle));
            }
            else
            {
                TryScrollToElement(_AllergenWhatSubstanceCausedTheReaction_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_AllergenWhatSubstanceCausedTheReaction_FieldTitle));
            }

            return this;
        }

        public PersonAllergyRecordPage ValidateStartDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_StartDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_StartDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_StartDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_StartDate_FieldTitle));
            }

            return this;
        }

        public PersonAllergyRecordPage ValidateEndDateFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_EndDate_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_EndDate_FieldTitle));
            }
            else
            {
                TryScrollToElement(_EndDate_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_EndDate_FieldTitle));
            }

            return this;
        }

        public PersonAllergyRecordPage ValidateDescriptionFieldTitleVisible(bool ExpectFieldVisible)
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




        


        public PersonAllergyRecordPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_createdBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdBy_FieldTitle));
            }

            return this;
        }

        public PersonAllergyRecordPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_createdOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_createdOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_createdOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_createdOn_FieldTitle));
            }

            return this;
        }

        public PersonAllergyRecordPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_modifiedBy_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedBy_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedBy_FieldTitle));
            }

            return this;
        }

        public PersonAllergyRecordPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElementWithWidthAndHeight(_modifiedOn_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }
            else
            {
                TryScrollToElement(_modifiedOn_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_modifiedOn_FieldTitle));
            }

            return this;
        }






        public PersonAllergyRecordPage ValidateAllergyTypeFieldText(string ExpectText)
        {
            ScrollToElement(_AllergyType_Field);
            string fieldText = GetElementText(_AllergyType_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAllergyRecordPage ValidateLevelFieldText(string ExpectText)
        {
            ScrollToElement(_Level_Field);
            string fieldText = GetElementText(_Level_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAllergyRecordPage ValidateAllergenWhatSubstanceCausedTheReactionFieldText(string ExpectText)
        {
            ScrollToElement(_AllergenWhatSubstanceCausedTheReaction_Field);
            string fieldText = GetElementText(_AllergenWhatSubstanceCausedTheReaction_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAllergyRecordPage ValidateStartDateFieldText(string ExpectDateText)
        {
            ScrollToElement(_StartDate_DateField);

            string fieldText = GetElementText(_StartDate_DateField);
            Assert.AreEqual(ExpectDateText, fieldText);

            return this;
        }

        public PersonAllergyRecordPage ValidateEndDateFieldText(string ExpectText)
        {
            ScrollToElement(_EndDate_Field);
            string fieldText = GetElementText(_EndDate_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        
        public PersonAllergyRecordPage ValidateDescriptionFieldText(string ExpectText)
        {
            ScrollToElement(_Description_Field);
            string fieldText = GetElementText(_Description_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }





        public PersonAllergyRecordPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAllergyRecordPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAllergyRecordPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonAllergyRecordPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }






        public PersonAllergyRecordPage TapRelatedItemsButton()
        {
            WaitForElement(_RelatedItemsButton);
            Tap(_RelatedItemsButton);

            return this;
        }

        public PersonAllergyRecordPage TapAllergicReactionsButton()
        {
            WaitForElement(_AllergicReactionsButton);
            Tap(_AllergicReactionsButton);

            return this;
        }

    }
}
