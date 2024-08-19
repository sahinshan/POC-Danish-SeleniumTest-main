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
    public class PersonFinancialDetailRecordPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("personfinancialdetail_SaveButton");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("personfinancialdetail_SaveAndCloseButton");
        readonly Func<AppQuery, AppQuery> _deleteButton = e => e.Marked("personfinancialdetail_DeleteRecordButton");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("personfinancialdetail_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("personfinancialdetail_TextToSpeechStopButton");

        Func<AppQuery, AppQuery> _pageTitle(string pageTitle) => e => e.Marked("MainStackLayout").Descendant().Marked(pageTitle);

        readonly Func<AppQuery, AppQuery> _topBannerArea = e => e.Marked("BannerStackLayout");


        #region Fields titles

        readonly Func<AppQuery, AppQuery> _Id_FieldTitle = e => e.Marked("Id");
        readonly Func<AppQuery, AppQuery> _Person_FieldTitle = e => e.Marked("Person");
        readonly Func<AppQuery, AppQuery> _FinancialDetailType_FieldTitle = e => e.Marked("Financial Detail Type");
        readonly Func<AppQuery, AppQuery> _FinancialDetail_FieldTitle = e => e.Marked("Financial Detail");
        readonly Func<AppQuery, AppQuery> _Amount_FieldTitle = e => e.Marked("Amount");
        readonly Func<AppQuery, AppQuery> _JointAmount_FieldTitle = e => e.Marked("Joint Amount");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_FieldTitle = e => e.Marked("Responsible Team");
        readonly Func<AppQuery, AppQuery> _FrequencyOfReceipt_FieldTitle = e => e.Marked("Frequency of Receipt");
        readonly Func<AppQuery, AppQuery> _StartDate_FieldTitle = e => e.Marked("Start Date");
        readonly Func<AppQuery, AppQuery> _EndDate_FieldTitle = e => e.Marked("End Date");
        readonly Func<AppQuery, AppQuery> _Verification_FieldTitle = e => e.Marked("Verification");
        readonly Func<AppQuery, AppQuery> _BeingReceived_FieldTitle = e => e.Marked("Being Received?");

        readonly Func<AppQuery, AppQuery> _Address_FieldTitle = e => e.Marked("Address");
        readonly Func<AppQuery, AppQuery> _PropertyDisregardType_FieldTitle = e => e.Marked("Property Disregard Type");
        readonly Func<AppQuery, AppQuery> _ExcludeFromDWPCalculation_FieldTitle = e => e.Marked("Exclude From DWP Calculation?");
        readonly Func<AppQuery, AppQuery> _GrossValue_FieldTitle = e => e.Marked("Gross Value");
        readonly Func<AppQuery, AppQuery> _OutstandingLoan_FieldTitle = e => e.Marked("Outstanding Loan");
        readonly Func<AppQuery, AppQuery> _Equity_FieldTitle = e => e.Marked("Equity");
        readonly Func<AppQuery, AppQuery> _Ownership_FieldTitle = e => e.Marked("% Ownership");

        readonly Func<AppQuery, AppQuery> _Reference_FieldTitle = e => e.Marked("Reference");
        readonly Func<AppQuery, AppQuery> _ApplicationDate_FieldTitle = e => e.Marked("Application Date");
        readonly Func<AppQuery, AppQuery> _Inactive_FieldTitle = e => e.Marked("Inactive");
        readonly Func<AppQuery, AppQuery> _ShowReferenceInSchedule_FieldTitle = e => e.Marked("Show Reference in Schedule");
        readonly Func<AppQuery, AppQuery> _Arrears_FieldTitle = e => e.Marked("Arrears");
        
        readonly Func<AppQuery, AppQuery> _createdOn_FieldTitle = e => e.All().Marked("CREATED ON");
        readonly Func<AppQuery, AppQuery> _createdBy_FieldTitle = e => e.All().Marked("CREATED BY");
        readonly Func<AppQuery, AppQuery> _modifiedOn_FieldTitle = e => e.All().Marked("MODIFIED ON");
        readonly Func<AppQuery, AppQuery> _modifiedBy_FieldTitle = e => e.All().Marked("MODIFIED BY");

        #endregion

        #region Fields

        readonly Func<AppQuery, AppQuery> _Id_Field = e => e.Marked("Field_245df3c3a7fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _Person_Field = e => e.Marked("Field_d8f0fcd3a7fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _Person_LookupEntry = e => e.Marked("d8f0fcd3a7fcea11a2cd0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Person_RemoveButton = e => e.Marked("d8f0fcd3a7fcea11a2cd0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _Person_LookupButton = e => e.Marked("d8f0fcd3a7fcea11a2cd0050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _FinancialDetailType_Field = e => e.Marked("Field_c29684e7a7fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _FinancialDetail_Field = e => e.Marked("Field_20c7a5ad17fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _FinancialDetail_LookupEntry = e => e.Marked("20c7a5ad17fcea11a2cd0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _FinancialDetail_RemoveButton = e => e.Marked("20c7a5ad17fcea11a2cd0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _FinancialDetail_LookupButton = e => e.Marked("20c7a5ad17fcea11a2cd0050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _Amount_Field = e => e.Marked("Field_3eb5cd10a8fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _JointAmount_Field = e => e.Marked("Field_4b59ac18a8fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_Field = e => e.Marked("Field_df7c1e89a7fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupEntry = e => e.Marked("df7c1e89a7fcea11a2cd0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_RemoveBUtton = e => e.Marked("df7c1e89a7fcea11a2cd0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ResponsibleTeam_LookupButton = e => e.Marked("df7c1e89a7fcea11a2cd0050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _FrequencyOfReceipt_Field = e => e.Marked("eee628a617fcea11a2cd0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _FrequencyOfReceipt_LookupButton = e => e.Marked("eee628a617fcea11a2cd0050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _FrequencyOfReceipt_RemoveButton = e => e.Marked("eee628a617fcea11a2cd0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _StartDate_Field = e => e.Marked("Field_917bc79ca7fcea11a2cd0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _EndDate_Field = e => e.Marked("Field_44b23ea5a7fcea11a2cd0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _Verification_Field = e => e.Marked("638bbbba17fcea11a2cd0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _Verification_LookupButton = e => e.Marked("638bbbba17fcea11a2cd0050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _BeingReceived_Field = e => e.Marked("Field_40d0cabba7fcea11a2cd0050569231cf");

        readonly Func<AppQuery, AppQuery> _Address_Field = e => e.Marked("Field_2da3b046a8fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _PropertyDisregardType_Field = e => e.Marked("d06e3c50a8fcea11a2cd0050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _PropertyDisregardType_LookupButton = e => e.Marked("d06e3c50a8fcea11a2cd0050569231cf_OpenLookup");
        readonly Func<AppQuery, AppQuery> _PropertyDisregardType_RemoveButton = e => e.Marked("d06e3c50a8fcea11a2cd0050569231cf_RemoveValue");
        readonly Func<AppQuery, AppQuery> _ExcludeFromDWPCalculation_Field = e => e.Marked("Field_584e1658a8fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _GrossValue_Field = e => e.Marked("Field_fe98de60a8fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _OutstandingLoan_Field = e => e.Marked("Field_0d6dc16ea8fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _Equity_Field = e => e.Marked("Field_d1f2de75a8fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _Ownership_Field = e => e.Marked("Field_e7e7c08ca8fcea11a2cd0050569231cf");

        readonly Func<AppQuery, AppQuery> _Reference_Field = e => e.Marked("Field_c9f095efa8fcea11a2cd0050569231cf");
        readonly Func<AppQuery, AppQuery> _ApplicationDate_Field = e => e.Marked("Field_7cd59d29a9fcea11a2cd0050569231cf_Date");
        readonly Func<AppQuery, AppQuery> _Inactive_Field = e => e.Marked("Field_e0c8c52fa9fcea11a2cd0050569231cf").Class("PickerEditText");
        readonly Func<AppQuery, AppQuery> _ShowReferenceInSchedule_Field = e => e.Marked("Field_5f364c37a9fcea11a2cd0050569231cf").Class("PickerEditText");
        readonly Func<AppQuery, AppQuery> _Arrears_Field = e => e.Marked("Field_718dfd3ea9fcea11a2cd0050569231cf");


        #endregion

        #region Related Items

        readonly Func<AppQuery, AppQuery> _relatedItemsButton = e => e.Marked("RelatedItemsButton");
        readonly Func<AppQuery, AppQuery> _relatedItemsLeftMenuButton = e => e.Marked("RelatedItems_CategoryLabel");
        readonly Func<AppQuery, AppQuery> _attachmentsButton = e => e.Marked("RelatedItems_Item_Attachments");

        #endregion


        public PersonFinancialDetailRecordPage(IApp app)
        {
            _app = app;
        }


        public PersonFinancialDetailRecordPage WaitForPersonFinancialDetailRecordPageToLoad(string PageTitleText)
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_saveButton);
            WaitForElement(_saveAndCloseButton);
            WaitForElement(_startButton);
            WaitForElement(_stopButton);
            WaitForElement(_pageTitle(PageTitleText));

            WaitForElement(_topBannerArea);

            


            return this;
        }



        public PersonFinancialDetailRecordPage ValidateIdFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Id_FieldTitle);
            ValidateElementVisibility(_Id_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidatePersonFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Person_FieldTitle);
            ValidateElementVisibility(_Person_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateFinancialDetailTypeFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_FinancialDetailType_FieldTitle);
            ValidateElementVisibility(_FinancialDetailType_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateFinancialDetailFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_FinancialDetail_FieldTitle);
            ValidateElementVisibility(_FinancialDetail_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateAmountFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Amount_FieldTitle);
            ValidateElementVisibility(_Amount_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateJointAmountFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_JointAmount_FieldTitle);
            ValidateElementVisibility(_JointAmount_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateResponsibleTeamFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_ResponsibleTeam_FieldTitle);
            ValidateElementVisibility(_ResponsibleTeam_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateFrequencyOfReceiptFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_FrequencyOfReceipt_FieldTitle);
            ValidateElementVisibility(_FrequencyOfReceipt_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateStartDateFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_StartDate_FieldTitle);
            ValidateElementVisibility(_StartDate_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateEndDateFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_EndDate_FieldTitle);
            ValidateElementVisibility(_EndDate_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateVerificationFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Verification_FieldTitle);
            ValidateElementVisibility(_Verification_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateBeingReceivedFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_BeingReceived_FieldTitle);
            ValidateElementVisibility(_BeingReceived_FieldTitle, ExpectElementVisible);
            return this;
        }
        
        public PersonFinancialDetailRecordPage ValidateAddressFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Address_FieldTitle);
            ValidateElementVisibility(_Address_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidatePropertyDisregardTypeFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_PropertyDisregardType_FieldTitle);
            ValidateElementVisibility(_PropertyDisregardType_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateExcludeFromDWPCalculationFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_ExcludeFromDWPCalculation_FieldTitle);
            ValidateElementVisibility(_ExcludeFromDWPCalculation_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateGrossValueFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_GrossValue_FieldTitle);
            ValidateElementVisibility(_GrossValue_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateOutstandingLoanFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_OutstandingLoan_FieldTitle);
            ValidateElementVisibility(_OutstandingLoan_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateEquityFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Equity_FieldTitle);
            ValidateElementVisibility(_Equity_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateOwnershipFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Ownership_FieldTitle);
            ValidateElementVisibility(_Ownership_FieldTitle, ExpectElementVisible);
            return this;
        }
        
        public PersonFinancialDetailRecordPage ValidateReferenceFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Reference_FieldTitle);
            ValidateElementVisibility(_Reference_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateApplicationDateFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_ApplicationDate_FieldTitle);
            ValidateElementVisibility(_ApplicationDate_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateInactiveFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Inactive_FieldTitle);
            ValidateElementVisibility(_Inactive_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateShowReferenceInScheduleFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_ShowReferenceInSchedule_FieldTitle);
            ValidateElementVisibility(_ShowReferenceInSchedule_FieldTitle, ExpectElementVisible);
            return this;
        }
        public PersonFinancialDetailRecordPage ValidateArrearsFieldTitleVisible(bool ExpectElementVisible)
        {
            TryScrollToElement(_Arrears_FieldTitle);
            ValidateElementVisibility(_Arrears_FieldTitle, ExpectElementVisible);
            return this;
        }




        public PersonFinancialDetailRecordPage ValidateIdFieldText(string ExpectedText)
        {
            ScrollToElement(_Id_Field);
            ValidateElementText(_Id_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidatePersonFieldText(string ExpectedText)
        {
            ScrollToElement(_Person_Field);
            ValidateElementText(_Person_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidatePersonLookupEntryFieldText(string ExpectedText)
        {
            ScrollToElement(_Person_LookupEntry);
            ValidateElementText(_Person_LookupEntry, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateFinancialDetailTypeFieldText(string ExpectedText)
        {
            ScrollToElement(_FinancialDetailType_Field);
            ValidateElementText(_FinancialDetailType_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateFinancialDetailFieldText(string ExpectedText)
        {
            ScrollToElement(_FinancialDetail_Field);
            ValidateElementText(_FinancialDetail_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateFinancialDetailLookupEntryFieldText(string ExpectedText)
        {
            ScrollToElement(_FinancialDetail_LookupEntry);
            ValidateElementText(_FinancialDetail_LookupEntry, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateAmountFieldText(string ExpectedText)
        {
            ScrollToElement(_Amount_Field);
            ValidateElementText(_Amount_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateJointAmountFieldText(string ExpectedText)
        {
            ScrollToElement(_JointAmount_Field);
            ValidateElementText(_JointAmount_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateResponsibleTeamFieldText(string ExpectedText)
        {
            ScrollToElement(_ResponsibleTeam_Field);
            ValidateElementText(_ResponsibleTeam_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateFrequencyOfReceiptFieldText(string ExpectedText)
        {
            ScrollToElement(_FrequencyOfReceipt_Field);
            ValidateElementText(_FrequencyOfReceipt_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateStartDateFieldText(string ExpectedText)
        {
            ScrollToElement(_StartDate_Field);
            ValidateElementText(_StartDate_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateEndDateFieldText(string ExpectedText)
        {
            ScrollToElement(_EndDate_Field);
            ValidateElementText(_EndDate_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateVerificationFieldText(string ExpectedText)
        {
            ScrollToElement(_Verification_Field);
            ValidateElementText(_Verification_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateBeingReceivedFieldText(string ExpectedText)
        {
            ScrollToElement(_BeingReceived_Field);
            ValidateElementText(_BeingReceived_Field, ExpectedText);

            return this;
        }

        public PersonFinancialDetailRecordPage ValidateAddressFieldText(string ExpectedText)
        {
            ScrollToElement(_Address_Field);
            ValidateElementText(_Address_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidatePropertyDisregardTypeFieldText(string ExpectedText)
        {
            ScrollToElement(_PropertyDisregardType_Field);
            ValidateElementText(_PropertyDisregardType_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateExcludeFromDWPCalculationFieldText(string ExpectedText)
        {
            ScrollToElement(_ExcludeFromDWPCalculation_Field);
            ValidateElementText(_ExcludeFromDWPCalculation_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateGrossValueFieldText(string ExpectedText)
        {
            ScrollToElement(_GrossValue_Field);
            ValidateElementText(_GrossValue_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateOutstandingLoanFieldText(string ExpectedText)
        {
            ScrollToElement(_OutstandingLoan_Field);
            ValidateElementText(_OutstandingLoan_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateEquityFieldText(string ExpectedText)
        {
            ScrollToElement(_Equity_Field);
            ValidateElementText(_Equity_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateOwnershipFieldText(string ExpectedText)
        {
            ScrollToElement(_Ownership_Field);
            ValidateElementText(_Ownership_Field, ExpectedText);

            return this;
        }

        public PersonFinancialDetailRecordPage ValidateReferenceFieldText(string ExpectedText)
        {
            ScrollToElement(_Reference_Field);
            ValidateElementText(_Reference_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateApplicationDateFieldText(string ExpectedText)
        {
            ScrollToElement(_ApplicationDate_Field);
            ValidateElementText(_ApplicationDate_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateInactiveFieldText(string ExpectedText)
        {
            ScrollToElement(_Inactive_Field);
            ValidateElementText(_Inactive_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateShowReferenceInScheduleFieldText(string ExpectedText)
        {
            ScrollToElement(_ShowReferenceInSchedule_Field);
            ValidateElementText(_ShowReferenceInSchedule_Field, ExpectedText);

            return this;
        }
        public PersonFinancialDetailRecordPage ValidateArrearsFieldText(string ExpectedText)
        {
            ScrollToElement(_Arrears_Field);
            ValidateElementText(_Arrears_Field, ExpectedText);

            return this;
        }


        public PersonFinancialDetailRecordPage TapPersonLookupButton()
        {
            ScrollToElement(_Person_LookupButton);
            Tap(_Person_LookupButton);

            return this;
        }
        public PersonFinancialDetailRecordPage TapPersonRemoveButton()
        {
            ScrollToElement(_Person_RemoveButton);
            Tap(_Person_RemoveButton);

            return this;
        }
        public PersonFinancialDetailRecordPage TapFinancialDetailTypeField()
        {
            ScrollToElement(_FinancialDetailType_Field);
            Tap(_FinancialDetailType_Field);

            return this;
        }
        public PersonFinancialDetailRecordPage TapFinancialDetailLookupButton()
        {
            ScrollToElement(_FinancialDetail_LookupButton);
            Tap(_FinancialDetail_LookupButton);

            return this;
        }
        public PersonFinancialDetailRecordPage TapFinancialDetailRemoveButton()
        {
            ScrollToElement(_FinancialDetail_RemoveButton);
            Tap(_FinancialDetail_RemoveButton);

            return this;
        }
        public PersonFinancialDetailRecordPage InsertAmount(string TextToInsert)
        {
            ScrollToElement(_Amount_Field);
            EnterText(_Amount_Field, TextToInsert);

            return this;
        }
        public PersonFinancialDetailRecordPage InsertJointAmount(string TextToInsert)
        {
            ScrollToElement(_JointAmount_Field);
            EnterText(_JointAmount_Field, TextToInsert);

            return this;
        }
        public PersonFinancialDetailRecordPage TapResponsibleTeamLookupButton()
        {
            ScrollToElement(_ResponsibleTeam_LookupButton);
            Tap(_ResponsibleTeam_LookupButton);

            return this;
        }
        public PersonFinancialDetailRecordPage TapFrequencyOfReceiptLookupButton()
        {
            ScrollToElement(_FrequencyOfReceipt_LookupButton);
            Tap(_FrequencyOfReceipt_LookupButton);

            return this;
        }
        public PersonFinancialDetailRecordPage TapFrequencyOfReceiptRemoveButton()
        {
            ScrollToElement(_FrequencyOfReceipt_RemoveButton);
            Tap(_FrequencyOfReceipt_RemoveButton);

            return this;
        }
        public PersonFinancialDetailRecordPage InsertStartDate(string TextToInsert)
        {
            ScrollToElement(_StartDate_Field);
            EnterText(_StartDate_Field, TextToInsert);

            return this;
        }
        public PersonFinancialDetailRecordPage InsertEndDate(string TextToInsert)
        {
            ScrollToElement(_EndDate_Field);
            EnterText(_EndDate_Field, TextToInsert);

            return this;
        }
        public PersonFinancialDetailRecordPage TapVerificationLookupButton()
        {
            ScrollToElement(_Verification_LookupButton);
            Tap(_Verification_LookupButton);

            return this;
        }
        public PersonFinancialDetailRecordPage TapBeingReceivedField()
        {
            ScrollToElement(_BeingReceived_Field);
            Tap(_BeingReceived_Field);

            return this;
        }

        public PersonFinancialDetailRecordPage NavigateToPersonFinancialDetailsAttachmentsPage()
        {
            Tap(_relatedItemsButton);
            Tap(_relatedItemsLeftMenuButton);
            Tap(_attachmentsButton);

            return this;
        }


        public PersonFinancialDetailRecordPage InsertAddress(string TextToInsert)
        {
            ScrollToElement(_Address_Field);
            EnterText(_Address_Field, TextToInsert);

            return this;
        }
        public PersonFinancialDetailRecordPage TapPropertyDisregardTypeLookupButton()
        {
            ScrollToElement(_PropertyDisregardType_LookupButton);
            Tap(_PropertyDisregardType_LookupButton);

            return this;
        }
        public PersonFinancialDetailRecordPage TapPropertyDisregardTypeRemoveButton()
        {
            ScrollToElement(_PropertyDisregardType_RemoveButton);
            Tap(_PropertyDisregardType_RemoveButton);

            return this;
        }
        public PersonFinancialDetailRecordPage TapExcludeFromDWPCalculationField()
        {
            ScrollToElement(_ExcludeFromDWPCalculation_Field);
            Tap(_ExcludeFromDWPCalculation_Field);

            return this;
        }
        public PersonFinancialDetailRecordPage InsertGrossValue(string TextToInsert)
        {
            ScrollToElement(_GrossValue_Field);
            EnterText(_GrossValue_Field, TextToInsert);

            return this;
        }
        public PersonFinancialDetailRecordPage InsertOutstandingLoan(string TextToInsert)
        {
            ScrollToElement(_OutstandingLoan_Field);
            EnterText(_OutstandingLoan_Field, TextToInsert);

            return this;
        }
        public PersonFinancialDetailRecordPage InsertOwnershipField(string TextToInsert)
        {
            ScrollToElement(_Ownership_Field);
            EnterText(_Ownership_Field, TextToInsert);

            return this;
        }


        public PersonFinancialDetailRecordPage InsertReference(string TextToInsert)
        {
            ScrollToElement(_Reference_Field);
            EnterText(_Reference_Field, TextToInsert);

            return this;
        }
        public PersonFinancialDetailRecordPage InsertApplicationDate(string TextToInsert)
        {
            ScrollToElement(_ApplicationDate_Field);
            EnterText(_ApplicationDate_Field, TextToInsert);

            return this;
        }
        public PersonFinancialDetailRecordPage TapInactiveField()
        {
            ScrollToElement(_Inactive_Field);
            Tap(_Inactive_Field);

            return this;
        }
        public PersonFinancialDetailRecordPage TapShowReferenceInScheduleField()
        {
            ScrollToElement(_ShowReferenceInSchedule_Field);
            Tap(_ShowReferenceInSchedule_Field);

            return this;
        }
        public PersonFinancialDetailRecordPage InsertArrears(string TextToInsert)
        {
            ScrollToElement(_Arrears_Field);
            EnterText(_Arrears_Field, TextToInsert);

            return this;
        }





        public PersonFinancialDetailRecordPage TapOnSaveButton()
        {
            Tap(_saveButton);

            return this;
        }

        public PersonFinancialDetailRecordPage TapOnSaveAndCloseButton()
        {
            Tap(_saveAndCloseButton);

            return this;
        }

        public PersonFinancialDetailRecordPage TapOnDeleteButton()
        {
            Tap(_deleteButton);

            return this;
        }


        public PersonFinancialDetailRecordPage WaitForSaveButtonNotVisible()
        {
            WaitForElementNotVisible(_saveButton);

            return this;
        }

        public PersonFinancialDetailRecordPage WaitOnSaveAndCloseButtonNotVisible()
        {
            WaitForElementNotVisible(_saveAndCloseButton);

            return this;
        }

        public PersonFinancialDetailRecordPage WaitForDeleteButtonNotVisible()
        {
            WaitForElementNotVisible(_deleteButton);

            return this;
        }
    }
}
