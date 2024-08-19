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
    public class PersonDNARActiveNInactiveRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("persondnar_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("persondnar_TextToSpeechStopButton");

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


        #region fields

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
         Func<AppQuery, AppQuery> _person_field( string CellText) => e => e.Marked("Field_31b2d2cc4a11ec11a33f0050569231cf").Marked(CellText);
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        readonly Func<AppQuery, AppQuery> _CancelledDecision_field_ScrollElement = e => e.All().Marked("Field_b8730f2d4e11ec11a33f0050569231cf");
        readonly Func<AppQuery, AppQuery> _CancelledBy_field_ScrollElement = (e => e.Marked("Field_5e933e8a2b15ec11a3430050569231cf"));
        readonly Func<AppQuery, AppQuery> _CancelledOn_field_ScrollElement = (e => e.Marked("Field_33137d354e11ec11a33f0050569231cf"));

        Func<AppQuery, AppQuery> _CancelledDecision_field(string CellText) => e => e.Marked("Field_b8730f2d4e11ec11a33f0050569231cf").Marked(CellText);
        Func<AppQuery, AppQuery> _CancelledBy_Field(string CellText) => e => e.Marked("Field_5e933e8a2b15ec11a3430050569231cf").Marked(CellText);
        Func<AppQuery, AppQuery> _CancelledOn_Field(string CellText) => e => e.Marked("Field_33137d354e11ec11a33f0050569231cf").Marked(CellText);

        #endregion

        #region Menu

        readonly Func<AppQuery, AppQuery> _RelatedItemsButton = e => e.Marked("RelatedItemsButton");
        readonly Func<AppQuery, AppQuery> _AllergicReactionsButton = e => e.Marked("RelatedItems_Item_AllergicReactions");

        #endregion



        public PersonDNARActiveNInactiveRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonDNARActiveNInactiveRecordPage WaitForPersoDNARActiveINactiveRecordPageToLoad()
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            //WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);
            
            return this;
        }



        public PersonDNARActiveNInactiveRecordPage ValidateAllergyTypeFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonDNARActiveNInactiveRecordPage ValidateLevelFieldTitleVisible(bool ExpectFieldVisible)
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

        public PersonDNARActiveNInactiveRecordPage ValidatePersonFieldText(string ExpectedText)
        {
            ScrollToElementWithWidthAndHeight(_person_field(ExpectedText));
            string fieldText = GetElementText(_person_field(ExpectedText));
            Assert.AreEqual(ExpectedText, fieldText);

            return this;
           
        }

        public PersonDNARActiveNInactiveRecordPage ValidateCancelledDecisionFieldText(string ExpectedText)
        {
            ScrollToElement(_CancelledDecision_field_ScrollElement);
            Assert.AreEqual(ExpectedText, GetElementText(_CancelledDecision_field(ExpectedText)));
           

            return this;

        }

        public PersonDNARActiveNInactiveRecordPage ValidateCancelledByFieldText(string ExpectedText)
        {
            ScrollToElement(_CancelledBy_field_ScrollElement);
            Assert.AreEqual(ExpectedText, GetElementText(_CancelledBy_field_ScrollElement));


            return this;

        }

        public PersonDNARActiveNInactiveRecordPage ValidateCancelledOnFieldText(String ExpectedText)
        {
            ScrollToElement(_CancelledOn_field_ScrollElement);
            string Actualdate = GetElementText(_CancelledOn_field_ScrollElement);
            Assert.AreEqual(ExpectedText, Actualdate);


            return this;

        }









    }
}
