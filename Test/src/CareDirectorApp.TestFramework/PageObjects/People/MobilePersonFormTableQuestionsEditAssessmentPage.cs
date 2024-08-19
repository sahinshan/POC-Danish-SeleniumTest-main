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
    public class MobilePersonFormTableQuestionsEditAssessmentPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("personform_SaveEnvironmentToolbarItem");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("personform_SaveAndBackEnvironmentToolbarItem");
        readonly Func<AppQuery, AppQuery> _lockAssessmentButton = e => e.Marked("personform_");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personform_TextToSpeechStartToolbarItem");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("personform_TextToSpeechStopToolbarItem");

        Func<AppQuery, AppQuery> _pageTitle(string TitleText) => e => e.Marked("MainStackLayout").Descendant().Text(TitleText);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");


        readonly Func<AppQuery, AppQuery> scrollViewer = e => e.Marked("AssesmentContentScroll");

        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Section1_FieldTitle = e => e.Marked("Section 1");
        
        readonly Func<AppQuery, AppQuery> _TestHQ_TableTitle = e => e.Marked("Test HQ");
        readonly Func<AppQuery, AppQuery> _TestHQ_Location_HeaderTitle = e => e.Marked("Location");
        readonly Func<AppQuery, AppQuery> _TestHQ_TestDec_HeaderTitle = e => e.Marked("Test Dec");


        readonly Func<AppQuery, AppQuery> _TablePQ_TableTitle = e => e.Marked("Table PQ");
        readonly Func<AppQuery, AppQuery> _Question1SubHeading_HeaderTitle = e => e.Marked("Question 1 - Sub Heading");
        readonly Func<AppQuery, AppQuery> _ContributionNotes_HeaderTitle = e => e.Marked("Contribution Notes");
        readonly Func<AppQuery, AppQuery> _Role_HeaderTitle = e => e.Marked("Role");


        readonly Func<AppQuery, AppQuery> _TestQPC_TableTitle = e => e.Marked("Test QPC");
        readonly Func<AppQuery, AppQuery> _Outcome_HeaderTitle = e => e.Marked("Outcome");
        readonly Func<AppQuery, AppQuery> _TypeOfInvolvement_HeaderTitle = e => e.Marked("Type of Involvement");
        readonly Func<AppQuery, AppQuery> _WFTime_HeaderTitle = e => e.Marked("WF Time");
        readonly Func<AppQuery, AppQuery> _Who_HeaderTitle = e => e.Marked("Who");


        readonly Func<AppQuery, AppQuery> _WFTableWithUnlimitedRows_TableTitle = e => e.Marked("WF Table With Unlimited Rows");
        readonly Func<AppQuery, AppQuery> _WFUnlimitedRowsTableSubHeading_HeaderTitle = e => e.Marked("WF Unlimited Rows Table Sub Heading");
        readonly Func<AppQuery, AppQuery> _DateBecameInvolved_HeaderTitle = e => e.Marked("Date became involved");
        readonly Func<AppQuery, AppQuery> _ReasonForAssessment_HeaderTitle = e => e.Marked("Reason for Assessment");

        

        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _TestHQ_Location_Row1_Field = e => e.Marked("QA-DQ-994");
        readonly Func<AppQuery, AppQuery> _TestHQ_TestDec_Row1_Field = e => e.Marked("QA-DQ-995");
        readonly Func<AppQuery, AppQuery> _TestHQ_Location_Row2_Field = e => e.Marked("QA-DQ-996");
        readonly Func<AppQuery, AppQuery> _TestHQ_TestDec_Row2_Field = e => e.Marked("QA-DQ-997");

        readonly Func<AppQuery, AppQuery> _TablePQ_ContributionNotes_Row1_Field = e => e.Marked("QA-DQ-999");
        readonly Func<AppQuery, AppQuery> _TablePQ_Role_Row2_Field = e => e.Marked("QA-DQ-1000");
        readonly Func<AppQuery, AppQuery> _TablePQ_ContributionNotes_Row3_Field = e => e.Marked("QA-DQ-1001");
        readonly Func<AppQuery, AppQuery> _TablePQ_Role_Row4_Field = e => e.Marked("QA-DQ-1002");

        readonly Func<AppQuery, AppQuery> _TestQPC_Outcome_Row1_Field = e => e.Marked("QA-DQ-1004");
        readonly Func<AppQuery, AppQuery> _TestQPC_TypeOfinvolvement_Row1_Field = e => e.Marked("QA-DQ-1005");
        readonly Func<AppQuery, AppQuery> _TestQPC_WFTime_Row2_Field = e => e.Marked("QA-DQ-1006_Time");
        readonly Func<AppQuery, AppQuery> _TestQPC_Who_Row2_Field = e => e.Marked("QA-DQ-1007");

        readonly Func<AppQuery, AppQuery> _WFTableWithUnlimitedRows_AddNewRowButton = e => e.Marked("NewRowButton");
        Func<AppQuery, AppQuery> _WFTableWithUnlimitedRows_DateBecomeInvolved_Field(string RowID) => e => e.Marked(RowID + "-QA-DQ-1009_Date");
        Func<AppQuery, AppQuery> _WFTableWithUnlimitedRows_ReasonForAssessment_Field(string RowID) => e => e.Marked(RowID + "-QA-DQ-1010");
        Func<AppQuery, AppQuery> _WFTableWithUnlimitedRows_DeleteRowButton(int RowID) => e => e.All().Property("contentDescription").StartsWith("DeleteImage_").Index(RowID);



        #endregion

        readonly Func<AppQuery, AppQuery> _OpenHierarchy_Button = e => e.Marked("OpenHierarchySelector");
        readonly Func<AppQuery, AppQuery> _HierarchySelector_Label = e => e.Marked("HierarchySelectorLabel");
        readonly Func<AppQuery, AppQuery> _HierarchySelector_CloseButton = e => e.Marked("HierarchySelectorClose");
        readonly Func<AppQuery, AppQuery> _HierarchySelector_Section1 = e => e.Marked("HierarchyScroll").Descendant().Marked("Section 1");
        readonly Func<AppQuery, AppQuery> _HierarchySelector_Section2 = e => e.Marked("HierarchyScroll").Descendant().Marked("Section 2");
        readonly Func<AppQuery, AppQuery> _ExpandAll_Button = e => e.Marked("ExpandAllImage");
        readonly Func<AppQuery, AppQuery> _CollapseAll_Button = e => e.Marked("CollapseAllImage");





        public MobilePersonFormTableQuestionsEditAssessmentPage(IApp app)
        {
            _app = app;
        }


        public MobilePersonFormTableQuestionsEditAssessmentPage WaitForMobilePersonFormTableQuestionsEditAssessmentPageToLoad(string PageTitleText)
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



        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestHQTableTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TestHQ_TableTitle);
                Assert.IsTrue(CheckIfElementVisible(_TestHQ_TableTitle));
            }
            else
            {
                TryScrollToElement(_TestHQ_TableTitle);
                Assert.IsFalse(CheckIfElementVisible(_TestHQ_TableTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestHQ_Location_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TestHQ_Location_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_TestHQ_Location_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_TestHQ_Location_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_TestHQ_Location_HeaderTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestHQ_TestDec_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TestHQ_TestDec_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_TestHQ_TestDec_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_TestHQ_Location_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_TestHQ_TestDec_HeaderTitle));
            }

            return this;
        }
        
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTablePQ_TableTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TablePQ_TableTitle);
                Assert.IsTrue(CheckIfElementVisible(_TablePQ_TableTitle));
            }
            else
            {
                TryScrollToElement(_TablePQ_TableTitle);
                Assert.IsFalse(CheckIfElementVisible(_TablePQ_TableTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTablePQ_Question1SubHeading_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Question1SubHeading_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_Question1SubHeading_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_Question1SubHeading_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_Question1SubHeading_HeaderTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTablePQ_ContributionNotes_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ContributionNotes_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_ContributionNotes_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_ContributionNotes_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_ContributionNotes_HeaderTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTablePQ_Role_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Role_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_Role_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_Role_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_Role_HeaderTitle));
            }

            return this;
        }


        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestQPC_TableTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TestQPC_TableTitle);
                Assert.IsTrue(CheckIfElementVisible(_TestQPC_TableTitle));
            }
            else
            {
                TryScrollToElement(_TestQPC_TableTitle);
                Assert.IsFalse(CheckIfElementVisible(_TestQPC_TableTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestQPC_Outcome_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Outcome_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_Outcome_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_Outcome_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_Outcome_HeaderTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestQPC_TypeOfInvolvement_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_TypeOfInvolvement_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_TypeOfInvolvement_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_TypeOfInvolvement_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_TypeOfInvolvement_HeaderTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestQPC_WFTime_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_WFTime_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_WFTime_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_WFTime_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_WFTime_HeaderTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestQPC_Who_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_Who_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_Who_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_Who_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_Who_HeaderTitle));
            }

            return this;
        }


        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateWFTableWithUnlimitedRows_TableTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_WFTableWithUnlimitedRows_TableTitle);
                Assert.IsTrue(CheckIfElementVisible(_WFTableWithUnlimitedRows_TableTitle));
            }
            else
            {
                TryScrollToElement(_WFTableWithUnlimitedRows_TableTitle);
                Assert.IsFalse(CheckIfElementVisible(_WFTableWithUnlimitedRows_TableTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateWFUnlimitedRowsTableSubHeading_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_WFUnlimitedRowsTableSubHeading_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_WFUnlimitedRowsTableSubHeading_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_WFUnlimitedRowsTableSubHeading_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_WFUnlimitedRowsTableSubHeading_HeaderTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateWFUnlimitedRowsTable_DateBecameInvolved_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_DateBecameInvolved_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_DateBecameInvolved_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_DateBecameInvolved_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_DateBecameInvolved_HeaderTitle));
            }

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateWFUnlimitedRowsTable_ReasonForAssessment_HeaderTitleVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ReasonForAssessment_HeaderTitle);
                Assert.IsTrue(CheckIfElementVisible(_ReasonForAssessment_HeaderTitle));
            }
            else
            {
                TryScrollToElement(_ReasonForAssessment_HeaderTitle);
                Assert.IsFalse(CheckIfElementVisible(_ReasonForAssessment_HeaderTitle));
            }

            return this;
        }





        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateCreatedByTitleVisible(bool ExpectFieldVisible)
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

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateCreatedOnTitleVisible(bool ExpectFieldVisible)
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

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateModifieddByTitleVisible(bool ExpectFieldVisible)
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

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateModifieddOnTitleVisible(bool ExpectFieldVisible)
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






        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestHQ_Location_Row1_FieldText(string ExpectText)
        {
            ScrollToElement(_TestHQ_Location_Row1_Field);
            string fieldText = GetElementText(_TestHQ_Location_Row1_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestHQ_TestDec_Row1_FieldText(string ExpectText)
        {
            ScrollToElement(_TestHQ_TestDec_Row1_Field);
            string fieldText = GetElementText(_TestHQ_TestDec_Row1_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestHQ_Location_Row2_FieldText(string ExpectText)
        {
            ScrollToElement(_TestHQ_Location_Row2_Field);
            string fieldText = GetElementText(_TestHQ_Location_Row2_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestHQ_TestDec_Row2_FieldText(string ExpectText)
        {
            ScrollToElement(_TestHQ_TestDec_Row2_Field);
            string fieldText = GetElementText(_TestHQ_TestDec_Row2_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTablePQ_ContributionNotes_Row1_FieldText(string ExpectText)
        {
            ScrollToElement(_TablePQ_ContributionNotes_Row1_Field);
            string fieldText = GetElementText(_TablePQ_ContributionNotes_Row1_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTablePQ_Role_Row2_FieldText(string ExpectText)
        {
            ScrollToElement(_TablePQ_Role_Row2_Field);
            string fieldText = GetElementText(_TablePQ_Role_Row2_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTablePQ_ContributionNotes_Row3_FieldText(string ExpectText)
        {
            ScrollToElement(_TablePQ_ContributionNotes_Row3_Field);
            string fieldText = GetElementText(_TablePQ_ContributionNotes_Row3_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTablePQ_Role_Row4_FieldText(string ExpectText)
        {
            ScrollToElement(_TablePQ_Role_Row4_Field);
            string fieldText = GetElementText(_TablePQ_Role_Row4_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestQPC_Outcome_Row1_FieldText(string ExpectText)
        {
            ScrollToElement(_TestQPC_Outcome_Row1_Field);
            string fieldText = GetElementText(_TestQPC_Outcome_Row1_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestQPC_TypeOfinvolvement_Row1_FieldText(string ExpectText)
        {
            ScrollToElement(_TestQPC_TypeOfinvolvement_Row1_Field);
            string fieldText = GetElementText(_TestQPC_TypeOfinvolvement_Row1_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestQPC_WFTime_Row2_FieldText(string ExpectText)
        {
            ScrollToElement(_TestQPC_WFTime_Row2_Field);
            string fieldText = GetElementText(_TestQPC_WFTime_Row2_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateTestQPC_Who_Row2_FieldText(string ExpectText)
        {
            ScrollToElement(_TestQPC_Who_Row2_Field);
            string fieldText = GetElementText(_TestQPC_Who_Row2_Field);
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }



        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateWFTableWithUnlimitedRows_DateBecomeInvolved_FieldText(string ExpectText, string IndexID)
        {
            ScrollToElement(_WFTableWithUnlimitedRows_DateBecomeInvolved_Field(IndexID));
            string fieldText = GetElementText(_WFTableWithUnlimitedRows_DateBecomeInvolved_Field(IndexID));
            Assert.AreEqual(ExpectText, fieldText);

            _app.DragCoordinates(800, 543, 800, 220);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateWFTableWithUnlimitedRows_ReasonForAssessment_FieldText(string ExpectText, string IndexID)
        {
            ScrollToElement(_WFTableWithUnlimitedRows_ReasonForAssessment_Field(IndexID));
            string fieldText = GetElementText(_WFTableWithUnlimitedRows_ReasonForAssessment_Field(IndexID));
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }






        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTestHQ_Location_Row1_Answer(string ValueToInsert)
        {
            ScrollToElement(_TestHQ_Location_Row1_Field);
            this.EnterText(_TestHQ_Location_Row1_Field, ValueToInsert);


            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTestHQ_TestDec_Row1_Answer(string ValueToInsert)
        {
            ScrollToElement(_TestHQ_TestDec_Row1_Field);
            this.EnterText(_TestHQ_TestDec_Row1_Field, ValueToInsert);


            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTestHQ_Location_Row2_Answer(string ValueToInsert)
        {
            ScrollToElement(_TestHQ_Location_Row2_Field);
            this.EnterText(_TestHQ_Location_Row2_Field, ValueToInsert);


            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTestHQ_TestDec_Row2_Answer(string ValueToInsert)
        {
            ScrollToElement(_TestHQ_TestDec_Row2_Field);
            this.EnterText(_TestHQ_TestDec_Row2_Field, ValueToInsert);


            return this;
        }




        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTablePQ_ContributionNotes_Row1_Answer(string ValueToInsert)
        {
            ScrollToElement(_TablePQ_ContributionNotes_Row1_Field);
            this.EnterText(_TablePQ_ContributionNotes_Row1_Field, ValueToInsert);


            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTablePQ_Role_Row2_Answer(string ValueToInsert)
        {
            ScrollToElement(_TablePQ_Role_Row2_Field);
            this.EnterText(_TablePQ_Role_Row2_Field, ValueToInsert);


            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTablePQ_ContributionNotes_Row3_Answer(string ValueToInsert)
        {
            ScrollToElement(_TablePQ_ContributionNotes_Row3_Field);
            this.EnterText(_TablePQ_ContributionNotes_Row3_Field, ValueToInsert);


            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTablePQ_Role_Row4_Answer(string ValueToInsert)
        {
            ScrollToElement(_TablePQ_Role_Row4_Field);
            
            ScrollToElement(_TablePQ_Role_Row4_Field);
            EnterText(_TablePQ_Role_Row4_Field, ValueToInsert);

            return this;
        }



        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTestQPC_Outcome_Row1_Answer(string ValueToInsert)
        {
            ScrollToElement(_TestQPC_Outcome_Row1_Field);
            this.EnterText(_TestQPC_Outcome_Row1_Field, ValueToInsert);


            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTestQPC_TypeOfinvolvement_Row1_Answer(string ValueToInsert)
        {
            ScrollToElement(_TestQPC_TypeOfinvolvement_Row1_Field);
            this.EnterText(_TestQPC_TypeOfinvolvement_Row1_Field, ValueToInsert);


            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTestQPC_WFTime_Row2_Answer(string ValueToInsert)
        {
            ScrollToElement(_TestQPC_WFTime_Row2_Field);
            ScrollToElement(_TestQPC_WFTime_Row2_Field);
            this.EnterText(_TestQPC_WFTime_Row2_Field, ValueToInsert);


            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage InsertTestQPC_Who_Row2_Answer(string ValueToInsert)
        {
            ScrollToElement(_TestQPC_Who_Row2_Field);
            this.EnterText(_TestQPC_Who_Row2_Field, ValueToInsert);


            return this;
        }



        public MobilePersonFormTableQuestionsEditAssessmentPage InsertWFTableWithUnlimitedRows_DateBecomeInvolved_Answer(string ValueToInsert, string IndexID)
        {
            ScrollToElement(_WFTableWithUnlimitedRows_DateBecomeInvolved_Field(IndexID));
            ScrollUpForm();

            EnterText(_WFTableWithUnlimitedRows_DateBecomeInvolved_Field(IndexID), ValueToInsert);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage TapWFTableWithUnlimitedRows_ReasonForAssessment_Picklist(string IndexID)
        {
            ScrollToElement(_WFTableWithUnlimitedRows_ReasonForAssessment_Field(IndexID));
            this.Tap(_WFTableWithUnlimitedRows_ReasonForAssessment_Field(IndexID));

            return this;
        }





        public MobilePersonFormTableQuestionsEditAssessmentPage TapTestHQ_TableTitle()
        {
            ScrollToElement(_TestHQ_TableTitle);
            Tap(_TestHQ_TableTitle);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage TapTablePQ_TableTitle()
        {
            ScrollToElement(_TablePQ_TableTitle);
            Tap(_TablePQ_TableTitle);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage TapTestQPC_TableTitle()
        {
            ScrollToElement(_TestQPC_TableTitle);
            ScrollToElement(_TestQPC_TableTitle);
            Tap(_TestQPC_TableTitle);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage TapWFTableWithUnlimitedRows_TableTitle()
        {
            ScrollToElement(_WFTableWithUnlimitedRows_TableTitle);
            
            ScrollToElement(_WFTableWithUnlimitedRows_TableTitle);
            Tap(_WFTableWithUnlimitedRows_TableTitle);

            return this;
        }


        public MobilePersonFormTableQuestionsEditAssessmentPage TapWFTableWithUnlimitedRows_AddNewRowButton()
        {
            ScrollToElement(_WFTableWithUnlimitedRows_AddNewRowButton);
            Tap(_WFTableWithUnlimitedRows_AddNewRowButton);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ScrollFormToVerticalEnd()
        {
            ScrollToVerticalEnd();

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ScrollUpForm()
        {
            ScrollUpWithinElement(scrollViewer);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage ScrollDownForm()
        {
            ScrollDownWithinElement(scrollViewer);

            return this;
        }



        public MobilePersonFormTableQuestionsEditAssessmentPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage TapLockButton()
        {
            Tap(_lockAssessmentButton);

            return this;
        }


        public MobilePersonFormTableQuestionsEditAssessmentPage TapOpenHierarchyButton()
        {
            Tap(_OpenHierarchy_Button);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage TapSection1HierarchyWindow()
        {
            Tap(_HierarchySelector_Section1);

            return this;
        }
        public MobilePersonFormTableQuestionsEditAssessmentPage TapSection2HierarchyWindow()
        {
            Tap(_HierarchySelector_Section2);

            return this;
        }



        public MobilePersonFormTableQuestionsEditAssessmentPage TapExpandAllButton()
        {
            Tap(_ExpandAll_Button);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage TapCollapseAllButton()
        {
            Tap(_CollapseAll_Button);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public MobilePersonFormTableQuestionsEditAssessmentPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }



        public MobilePersonFormTableQuestionsEditAssessmentPage ValidateHierarchyAreaVisibility(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                Assert.IsTrue(CheckIfElementVisible(_HierarchySelector_Label));
                Assert.IsTrue(CheckIfElementVisible(_HierarchySelector_CloseButton));
                Assert.IsTrue(CheckIfElementVisible(_HierarchySelector_Section1));
            }
            else
            {
                Assert.IsFalse(CheckIfElementVisible(_HierarchySelector_Label));
                Assert.IsFalse(CheckIfElementVisible(_HierarchySelector_CloseButton));
                Assert.IsFalse(CheckIfElementVisible(_HierarchySelector_Section1));
            }

            return this;
        }

    }
}
