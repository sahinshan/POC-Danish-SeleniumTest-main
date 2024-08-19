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
    /// <summary>
    /// Use this class to interact with the edit assessment page for forms using the assessment type "Mobile - Person Form"
    /// </summary>
    public class MobileCaseFormEditAssessmentPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("caseform_SaveEnvironmentToolbarItem");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("caseform_SaveAndBackEnvironmentToolbarItem");
        readonly Func<AppQuery, AppQuery> _lockAssessmentButton = e => e.Marked("caseform_");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("caseform_TextToSpeechStartToolbarItem");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("caseform_TextToSpeechStopToolbarItem");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");


        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Section1_FieldTitle = e => e.Marked("Section 1");
        readonly Func<AppQuery, AppQuery> _Section1_1_FieldTitle = e => e.Marked("Section 1.1");
        readonly Func<AppQuery, AppQuery> _Section2_FieldTitle = e => e.Marked("Section 2");
        readonly Func<AppQuery, AppQuery> _DateOfBirth_FieldTitle = e => e.Marked("Date of Birth");
        readonly Func<AppQuery, AppQuery> _Decimal_FieldTitle = e => e.Marked("Decimal");
        readonly Func<AppQuery, AppQuery> _MultipleChoice_FieldTitle = e => e.Marked("Multiple Choice");

        readonly Func<AppQuery, AppQuery> _MultipleResponse_FieldTitle = e => e.Marked("MultiResponse");
        readonly Func<AppQuery, AppQuery> _MultipleResponse_Option1FieldTitle = e => e.Marked("Option 1");
        readonly Func<AppQuery, AppQuery> _MultipleResponse_Option2FieldTitle = e => e.Marked("Option 2");
        readonly Func<AppQuery, AppQuery> _MultipleResponse_Option3FieldTitle = e => e.Marked("Option 3");

        readonly Func<AppQuery, AppQuery> _Numeric_FieldTitle = e => e.Marked("Numeric");
        readonly Func<AppQuery, AppQuery> _ParagraphExplanation_FieldTitle = e => e.Marked("Paragraph/Explanation");
        readonly Func<AppQuery, AppQuery> _ParagraphExplanation_SubHeadingFieldTitle = e => e.Marked("Paragraph/Explanation - Sub heading");
        readonly Func<AppQuery, AppQuery> _Frequency_FieldTitle = e => e.Marked("Frequency");
        readonly Func<AppQuery, AppQuery> _ShortAnswer_FieldTitle = e => e.Marked("Short Answer");
        readonly Func<AppQuery, AppQuery> _ShortAnswer_SubHeadingFieldTitle = e => e.Marked("Short Answer - Sub heading");
        readonly Func<AppQuery, AppQuery> _Title_FieldTitle = e => e.Marked("https://www.bbc.co.uk");
        readonly Func<AppQuery, AppQuery> _Authorisation_FieldTitle = e => e.Marked("Authorisation");

        readonly Func<AppQuery, AppQuery> _TimeQuestion_FieldTitle = e => e.Marked("Time");

        readonly Func<AppQuery, AppQuery> _YesNo_FieldTitle = e => e.Marked("Yes/No");
        readonly Func<AppQuery, AppQuery> _YesNo_SubHeadingFieldTitle = e => e.Marked("Yes/No - Sub heading");

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _DateOfBirth_Field = e => e.Marked("QA-DQ-1035_Date");
        readonly Func<AppQuery, AppQuery> _Decimal_Field = e => e.Marked("QA-DQ-1036");
        readonly Func<AppQuery, AppQuery> _MultipleChoice_Field = e => e.Marked("QA-DQ-1037");

        readonly Func<AppQuery, AppQuery> _MultipleResponseOption1_Field = e => e.Marked("QA-DQ-1038_1");
        readonly Func<AppQuery, AppQuery> _MultipleResponseOption2_Field = e => e.Marked("QA-DQ-1038_2");
        readonly Func<AppQuery, AppQuery> _MultipleResponseOption3_Field = e => e.Marked("QA-DQ-1038_3");

        readonly Func<AppQuery, AppQuery> _Numeric_Field = e => e.Marked("QA-DQ-1039");
        readonly Func<AppQuery, AppQuery> _ParagraphExplanation_Field = e => e.Marked("QA-DQ-1040");
        readonly Func<AppQuery, AppQuery> _Frequency_Field = e => e.All().Marked("QA-DQ-1041");
        readonly Func<AppQuery, AppQuery> _ShortAnswer_Field = e => e.Marked("QA-DQ-1042");

        readonly Func<AppQuery, AppQuery> _Authorisation_OpenImageButton = e => e.All().Marked("_OpenControlImage");
        readonly Func<AppQuery, AppQuery> _Authorisation_DeleteButton = e => e.All().Marked("_DeleteSignatureImage");
        readonly Func<AppQuery, AppQuery> _Authorisation_SignatureImage = e => e.All().Marked("_SignatureControlImage");

        readonly Func<AppQuery, AppQuery> _TimeQuestion_Field = e => e.Marked("QA-DQ-1045_Time");
        readonly Func<AppQuery, AppQuery> _YesNo_Field = e => e.Marked("QA-DQ-1046");

        readonly Func<AppQuery, AppQuery> _createdOn_Field = e => e.All().Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdBy_Field = e => e.All().Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedOn_Field = e => e.All().Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedBy_Field = e => e.All().Marked("FooterLabel_modifiedon");

        #endregion

        readonly Func<AppQuery, AppQuery> _OpenHierarchy_Button = e => e.Marked("OpenHierarchySelector");
        readonly Func<AppQuery, AppQuery> _HierarchySelector_Label = e => e.Marked("HierarchySelectorLabel");
        readonly Func<AppQuery, AppQuery> _HierarchySelector_CloseButton = e => e.Marked("HierarchySelectorClose");
        readonly Func<AppQuery, AppQuery> _HierarchySelector_Section1 = e => e.Marked("HierarchyScroll").Descendant().Marked("Section 1");
        readonly Func<AppQuery, AppQuery> _HierarchySelector_Section_1_1 = e => e.Marked("HierarchyScroll").Descendant().Marked("Section 1.1");
        readonly Func<AppQuery, AppQuery> _HierarchySelector_Section2 = e => e.Marked("HierarchyScroll").Descendant().Marked("Section 2");

        //HierarchyScroll


        readonly Func<AppQuery, AppQuery> _ExpandAll_Button = e => e.Marked("ExpandAllImage");
        readonly Func<AppQuery, AppQuery> _CollapseAll_Button = e => e.Marked("CollapseAllImage");





        public MobileCaseFormEditAssessmentPage(IApp app)
        {
            _app = app;
        }


        public MobileCaseFormEditAssessmentPage WaitForMobileCaseFormEditAssessmentPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_syncIcon);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_startButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);
            
            return this;
        }



        public MobileCaseFormEditAssessmentPage ValidateDateOfBirthFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DateOfBirth_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_DateOfBirth_FieldTitle));
            }
            else
            {
                TryScrollToElement(_DateOfBirth_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_DateOfBirth_FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateDecimalFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Decimal_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Decimal_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Decimal_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Decimal_FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateMultipleChoiceFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_MultipleChoice_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_MultipleChoice_FieldTitle));
            }
            else
            {
                TryScrollToElement(_MultipleChoice_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_MultipleChoice_FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateMultipleResponseFieldTitlesVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_MultipleResponse_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_MultipleResponse_FieldTitle));

                ScrollToElement(_MultipleResponse_Option1FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_MultipleResponse_Option1FieldTitle));

                ScrollToElement(_MultipleResponse_Option2FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_MultipleResponse_Option2FieldTitle));

                ScrollToElement(_MultipleResponse_Option3FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_MultipleResponse_Option3FieldTitle));
            }
            else
            {
                TryScrollToElement(_MultipleResponse_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_MultipleResponse_FieldTitle));

                TryScrollToElement(_MultipleResponse_Option1FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_MultipleResponse_Option1FieldTitle));

                TryScrollToElement(_MultipleResponse_Option2FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_MultipleResponse_Option2FieldTitle));

                TryScrollToElement(_MultipleResponse_Option3FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_MultipleResponse_Option3FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateNumericFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Numeric_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Numeric_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Numeric_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Numeric_FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateParagraphExplanationFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ParagraphExplanation_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ParagraphExplanation_FieldTitle));

                ScrollToElement(_ParagraphExplanation_SubHeadingFieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ParagraphExplanation_SubHeadingFieldTitle));
            }
            else
            {
                TryScrollToElement(_ParagraphExplanation_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ParagraphExplanation_FieldTitle));

                TryScrollToElement(_ParagraphExplanation_SubHeadingFieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ParagraphExplanation_SubHeadingFieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateFrequencyFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Frequency_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Frequency_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Frequency_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Frequency_FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateShortAnswerFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ShortAnswer_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ShortAnswer_FieldTitle));

                ScrollToElement(_ShortAnswer_SubHeadingFieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_ShortAnswer_SubHeadingFieldTitle));
            }
            else
            {
                TryScrollToElement(_ShortAnswer_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ShortAnswer_FieldTitle));

                TryScrollToElement(_ShortAnswer_SubHeadingFieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_ShortAnswer_SubHeadingFieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateTitleFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Title_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Title_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Title_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Title_FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateAuthorisationFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Authorisation_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_Authorisation_FieldTitle));
            }
            else
            {
                TryScrollToElement(_Authorisation_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_Authorisation_FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateTimeQuestionFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TimeQuestion_FieldTitle);
                ScrollDown();
                ScrollToElement(_TimeQuestion_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_TimeQuestion_FieldTitle));
            }
            else
            {
                TryScrollToElement(_TimeQuestion_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_TimeQuestion_FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateTimeQuestionFieldTitleVisibleWithoutScrolling(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                Assert.IsTrue(CheckIfElementVisible(_TimeQuestion_FieldTitle));
            }
            else
            {
                Assert.IsFalse(CheckIfElementVisible(_TimeQuestion_FieldTitle));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateYesNoFieldTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_YesNo_FieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_YesNo_FieldTitle));

                ScrollToElement(_YesNo_SubHeadingFieldTitle);
                Assert.IsTrue(CheckIfElementVisible(_YesNo_SubHeadingFieldTitle));
            }
            else
            {
                TryScrollToElement(_YesNo_FieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_YesNo_FieldTitle));

                TryScrollToElement(_YesNo_SubHeadingFieldTitle);
                Assert.IsFalse(CheckIfElementVisible(_YesNo_SubHeadingFieldTitle));
            }

            return this;
        }




        public MobileCaseFormEditAssessmentPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public MobileCaseFormEditAssessmentPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public MobileCaseFormEditAssessmentPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public MobileCaseFormEditAssessmentPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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






        public MobileCaseFormEditAssessmentPage ValidateDateOfBirthFieldText(string ExpectText)
        {
            ScrollToElement(_DateOfBirth_Field);
            string fieldText = GetElementText(_DateOfBirth_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateDecimalFieldText(string ExpectText)
        {
            ScrollToElement(_Decimal_Field);
            string fieldText = GetElementText(_Decimal_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateMultipleChoiceFieldText(string ExpectText)
        {
            ScrollToElement(_MultipleChoice_Field);
            string fieldText = GetElementText(_MultipleChoice_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateMultipleResponseOption1FieldText(string ExpectText)
        {
            ScrollToElement(_MultipleResponseOption1_Field);
            string fieldText = GetElementText(_MultipleResponseOption1_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateMultipleResponseOption2FieldText(string ExpectText)
        {
            ScrollToElement(_MultipleResponseOption2_Field);
            string fieldText = GetElementText(_MultipleResponseOption2_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateMultipleResponseOption3FieldText(string ExpectText)
        {
            ScrollToElement(_MultipleResponseOption3_Field);
            string fieldText = GetElementText(_MultipleResponseOption3_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateNumericFieldText(string ExpectText)
        {
            ScrollToElement(_Numeric_Field);
            string fieldText = GetElementText(_Numeric_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateParagraphExplanationFieldText(string ExpectDateText)
        {
            ScrollToElement(_ParagraphExplanation_Field);

            string fieldText = GetElementText(_ParagraphExplanation_Field);
            Assert.AreEqual(ExpectDateText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateFrequencyFieldText(string ExpectText)
        {
            ScrollToElement(_Frequency_Field);
            string fieldText = GetElementText(_Frequency_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateShortAnswerFieldText(string ExpectText)
        {
            ScrollToElement(_ShortAnswer_Field);
            string fieldText = GetElementText(_ShortAnswer_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateSignatureFieldVisible(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElement(_Authorisation_SignatureImage);
                Assert.IsTrue(CheckIfElementVisible(_Authorisation_SignatureImage));
            }
            else
            {
                TryScrollToElement(_Authorisation_SignatureImage);
                Assert.IsFalse(CheckIfElementVisible(_Authorisation_SignatureImage));
            }

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateTimeQuestionFieldText(string ExpectText)
        {
            ScrollToElement(_TimeQuestion_Field);
            string fieldText = GetElementText(_TimeQuestion_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateTimeQuestionFieldTextWithoutScrolling(string ExpectText)
        {
            string fieldText = GetElementText(_TimeQuestion_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateYesNoFieldText(string ExpectText)
        {
            ScrollToElement(_YesNo_Field);
            ScrollDown();
            ScrollToElement(_YesNo_Field);
            string fieldText = GetElementText(_YesNo_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }
        



        public MobileCaseFormEditAssessmentPage ValidateCreatedByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdBy_Field);
            string fieldText = GetElementText(_createdBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateCreatedOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_createdOn_Field);
            string fieldText = GetElementText(_createdOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateModifieddByFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedBy_Field);
            string fieldText = GetElementText(_modifiedBy_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobileCaseFormEditAssessmentPage ValidateModifieddOnFieldText(string ExpectText)
        {
            ScrollToElementWithWidthAndHeight(_modifiedOn_Field);
            string fieldText = GetElementText(_modifiedOn_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }





        public MobileCaseFormEditAssessmentPage InsertDateOfBirthAnswer(string ValueToInsert)
        {
            ScrollToElement(_DateOfBirth_Field);
            this.EnterText(_DateOfBirth_Field, ValueToInsert);

            return this;
        }

        public MobileCaseFormEditAssessmentPage InsertDecimalAnswer(string ValueToInsert)
        {
            ScrollToElement(_Decimal_Field);
            this.EnterText(_Decimal_Field, ValueToInsert);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapMultipleChoiceField()
        {
            ScrollToElement(_MultipleChoice_Field);
            this.Tap(_MultipleChoice_Field);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapMultipleResponseOption1Field()
        {
            ScrollToElement(_MultipleResponseOption1_Field);
            this.Tap(_MultipleResponseOption1_Field);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapMultipleResponseOption2Field()
        {
            ScrollToElement(_MultipleResponseOption2_Field);
            this.Tap(_MultipleResponseOption2_Field);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapMultipleResponseOption3Field()
        {
            ScrollToElement(_MultipleResponseOption3_Field);
            this.Tap(_MultipleResponseOption3_Field);

            return this;
        }

        public MobileCaseFormEditAssessmentPage InsertNumericAnswer(string ValueToInsert)
        {
            ScrollToElement(_Numeric_Field);
            this.EnterText(_Numeric_Field, ValueToInsert);

            return this;
        }

        public MobileCaseFormEditAssessmentPage InsertParagraphExplanationAnswer(string ValueToInsert)
        {
            ScrollToElement(_ParagraphExplanation_Field);
            this.EnterText(_ParagraphExplanation_Field, ValueToInsert);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapFrequencyField()
        {
            ScrollToElement(_Frequency_Field);
            this.Tap(_Frequency_Field);

            return this;
        }

        public MobileCaseFormEditAssessmentPage InsertShortAnswer(string ValueToInsert)
        {
            ScrollToElement(_ShortAnswer_Field);
            this.EnterText(_ShortAnswer_Field, ValueToInsert);

            return this;
        }

        public MobileCaseFormEditAssessmentPage InsertTimeQuestionAnswer(string ValueToInsert)
        {
            ScrollToElement(_TimeQuestion_Field);
            this.EnterText(_TimeQuestion_Field, ValueToInsert);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapYesNoField()
        {
            ScrollToElement(_YesNo_Field);
            this.Tap(_YesNo_Field);

            return this;
        }



        public MobileCaseFormEditAssessmentPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapLockButton()
        {
            Tap(_lockAssessmentButton);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapOpenHierarchyButton()
        {
            Tap(_OpenHierarchy_Button);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapSection1HierarchyWindow()
        {
            Tap(_HierarchySelector_Section1);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapSection_1_1_HierarchyWindow()
        {
            Tap(_HierarchySelector_Section_1_1);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapSection2HierarchyWindow()
        {
            Tap(_HierarchySelector_Section2);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapExpandAllButton()
        {
            Tap(_ExpandAll_Button);

            return this;
        }

        public MobileCaseFormEditAssessmentPage TapCollapseAllButton()
        {
            Tap(_CollapseAll_Button);

            return this;
        }

        public MobileCaseFormEditAssessmentPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public MobileCaseFormEditAssessmentPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }



        public MobileCaseFormEditAssessmentPage ValidateHierarchyAreaVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                Assert.IsTrue(CheckIfElementVisible(_HierarchySelector_Label));
                Assert.IsTrue(CheckIfElementVisible(_HierarchySelector_CloseButton));
                Assert.IsTrue(CheckIfElementVisible(_HierarchySelector_Section1));
                Assert.IsTrue(CheckIfElementVisible(_HierarchySelector_Section_1_1));
                Assert.IsTrue(CheckIfElementVisible(_HierarchySelector_Section2));
            }
            else
            {
                Assert.IsFalse(CheckIfElementVisible(_HierarchySelector_Label));
                Assert.IsFalse(CheckIfElementVisible(_HierarchySelector_CloseButton));
                Assert.IsFalse(CheckIfElementVisible(_HierarchySelector_Section1));
                Assert.IsFalse(CheckIfElementVisible(_HierarchySelector_Section_1_1));
                Assert.IsFalse(CheckIfElementVisible(_HierarchySelector_Section2));
            }

            return this;
        }

    }
}
