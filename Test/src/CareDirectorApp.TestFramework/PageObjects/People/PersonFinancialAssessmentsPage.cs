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
    public class PersonFinancialAssessmentsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("financialassessment_NewRecordButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("FINANCIAL ASSESSMENTS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");



        #region Header Elements

        readonly Func<AppQuery, AppQuery> _IDHeader = e => e.Marked("financialassessment_HeaderCell_financialassessmentnumber").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _FINANCIALASSESSMENTSTATUSHeader = e => e.Marked("financialassessment_HeaderCell_financialassessmentstatusid").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _CALCULATIONREQUIREDHeader = e => e.Marked("financialassessment_HeaderCell_calculationrequired").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _STARTDATEHeader = e => e.Marked("financialassessment_HeaderCell_startdate").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _ENDDATEHeader = e => e.Marked("financialassessment_HeaderCell_enddate").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _CHARGINGRULEHeader = e => e.Marked("financialassessment_HeaderCell_chargingruleid").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _FINANCIALASSESSMENTTYPEHeader = e => e.Marked("financialassessment_HeaderCell_financialassessmenttypeid").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _INCOMESUPPORTTYPEHeader = e => e.Marked("financialassessment_HeaderCell_incomesupporttypeid").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _INCOMESUPPORTVALUEHeader = e => e.Marked("financialassessment_HeaderCell_incomesupportvalue").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _FINANCIALASSESSMENTCATEGORYHeader = e => e.Marked("financialassessment_HeaderCell_financialassessmentcategoryid").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _DAYSPROPERTYDISREGARDEDHeader = e => e.Marked("financialassessment_HeaderCell_dayspropertydisregarded").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _DEFERREDPAYMENTSCHEMEHeader = e => e.Marked("financialassessment_HeaderCell_deferredpaymentscheme").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _DEFERREDPAYMENTSCHEMETYPEHeader = e => e.Marked("financialassessment_HeaderCell_deferredpaymentschemetypeid").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _PERMITCHARGEUPDATESVIAFINANCIALASSESSMENTHeader = e => e.Marked("financialassessment_HeaderCell_permitchargeupdatesbyfinancialassessment").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _PERMITCHARGEUPDATESVIARECALCULATIONHeader = e => e.Marked("financialassessment_HeaderCell_permitchargeupdatesbyrecalculation").Descendant().Property("id").Contains("NoResourceEntry-");
        readonly Func<AppQuery, AppQuery> _SERVICEPROVISIONASSOCIATEDHeader = e => e.Marked("financialassessment_HeaderCell_hasserviceprovisionassociated").Descendant().Property("id").Contains("NoResourceEntry-");

        #endregion

        #region Body Elements

        Func<AppQuery, AppQuery> _IDCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_financialassessmentnumber").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _FinancialAssessmentStatusCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_financialassessmentstatusid").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _CalculationRequiredCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_calculationrequired").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _StartDateCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_startdate").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _EndDateCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_enddate").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _ChargingRuleCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_chargingruleid").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _FinancialAssessmentTypeCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_financialassessmenttypeid").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _IncomeSupportTypeCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_incomesupporttypeid").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _IncomeSupportValueCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_incomesupportvalue").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _FinancialAssessmentCategoryCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_financialassessmentcategoryid").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _DaysPropertyDisregardedCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_dayspropertydisregarded").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _DeferredPaymentSchemeCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_deferredpaymentscheme").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _DeferredPaymentSchemaTypeCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_deferredpaymentschemetypeid").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _PermitChargeUpdatesViaFinancialAssessmentCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_permitchargeupdatesbyfinancialassessment").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _PermitChargeUpdatesViaRecalculationCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_permitchargeupdatesbyrecalculation").Descendant().Property("id").Contains("NoResourceEntry-");
        Func<AppQuery, AppQuery> _ServiceProvisionAssociatedCell(string recordid) => e => e.Marked("financialassessment_Row_" + recordid + "_Cell_hasserviceprovisionassociated").Descendant().Property("id").Contains("NoResourceEntry-");

        #endregion



        public PersonFinancialAssessmentsPage(IApp app)
        {
            _app = app;
        }


        public PersonFinancialAssessmentsPage WaitForPersonFinancialAssessmentsPageToLoad(string ViewText = "Related Records")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_addNewRecordButton);
            WaitForElement(_peoplePageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            WaitForElement(_IDHeader);
            WaitForElement(_FINANCIALASSESSMENTSTATUSHeader);
            WaitForElement(_CALCULATIONREQUIREDHeader);
            WaitForElement(_STARTDATEHeader);
            WaitForElement(_ENDDATEHeader);

            return this;
        }

        #region Methods used when displaying the APP in tablet mode

        public PersonFinancialAssessmentsPage ValidateIDCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_IDHeader);
            ScrollToElement(_IDCell(RecordID));
            string elementText = GetElementText(_IDCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateFinancialAssessmentStatusCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_FINANCIALASSESSMENTSTATUSHeader);
            ScrollToElement(_FinancialAssessmentStatusCell(RecordID));
            string elementText = GetElementText(_FinancialAssessmentStatusCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateCalculationRequiredCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_CALCULATIONREQUIREDHeader);
            ScrollToElement(_CalculationRequiredCell(RecordID));
            string elementText = GetElementText(_CalculationRequiredCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateStartDateCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_STARTDATEHeader);
            ScrollToElement(_StartDateCell(RecordID));
            string elementText = GetElementText(_StartDateCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateEndDateCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_ENDDATEHeader);
            ScrollToElement(_EndDateCell(RecordID));
            string elementText = GetElementText(_EndDateCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateChargingRuleCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_CHARGINGRULEHeader);
            ScrollToElement(_ChargingRuleCell(RecordID));
            string elementText = GetElementText(_ChargingRuleCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateFinancialAssessmentTypeCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_FINANCIALASSESSMENTTYPEHeader);
            ScrollToElement(_FinancialAssessmentTypeCell(RecordID));
            string elementText = GetElementText(_FinancialAssessmentTypeCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateIncomeSupportTypeCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_INCOMESUPPORTTYPEHeader);
            ScrollToElement(_IncomeSupportTypeCell(RecordID));
            string elementText = GetElementText(_IncomeSupportTypeCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateIncomeSupportValueCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_INCOMESUPPORTVALUEHeader);
            ScrollToElement(_IncomeSupportValueCell(RecordID));
            string elementText = GetElementText(_IncomeSupportValueCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateFinancialAssessmentCategoryCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_FINANCIALASSESSMENTCATEGORYHeader);
            ScrollToElement(_FinancialAssessmentCategoryCell(RecordID));
            string elementText = GetElementText(_FinancialAssessmentCategoryCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateDaysPropertyDisregardedCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_DAYSPROPERTYDISREGARDEDHeader);
            ScrollToElement(_DaysPropertyDisregardedCell(RecordID));
            string elementText = GetElementText(_DaysPropertyDisregardedCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateDeferredPaymentSchemeCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_DEFERREDPAYMENTSCHEMEHeader);
            ScrollToElement(_DeferredPaymentSchemeCell(RecordID));
            string elementText = GetElementText(_DeferredPaymentSchemeCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateDeferredPaymentSchemaTypeCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_DEFERREDPAYMENTSCHEMETYPEHeader);
            ScrollToElement(_DeferredPaymentSchemaTypeCell(RecordID));
            string elementText = GetElementText(_DeferredPaymentSchemaTypeCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidatePermitChargeUpdatesViaFinancialAssessmentCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_PERMITCHARGEUPDATESVIAFINANCIALASSESSMENTHeader);
            ScrollToElement(_PermitChargeUpdatesViaFinancialAssessmentCell(RecordID));
            string elementText = GetElementText(_PermitChargeUpdatesViaFinancialAssessmentCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidatePermitChargeUpdatesViaRecalculationCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_PERMITCHARGEUPDATESVIARECALCULATIONHeader);
            ScrollToElement(_PermitChargeUpdatesViaRecalculationCell(RecordID));
            string elementText = GetElementText(_PermitChargeUpdatesViaRecalculationCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }
        public PersonFinancialAssessmentsPage ValidateServiceProvisionAssociatedCell(string ExpectedText, string RecordID)
        {
            ScrollToElement(_SERVICEPROVISIONASSOCIATEDHeader);
            ScrollToElement(_ServiceProvisionAssociatedCell(RecordID));
            string elementText = GetElementText(_ServiceProvisionAssociatedCell(RecordID));
            Assert.AreEqual(ExpectedText, elementText);

            return this;
        }

        #endregion


        public PersonFinancialAssessmentsPage TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }

        public PersonFinancialAssessmentsPage TapOnRecord(string RecordID)
        {
            WaitForElement(_IDCell(RecordID));
            Tap(_IDCell(RecordID));

            return this;
        }

        public PersonFinancialAssessmentsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public PersonFinancialAssessmentsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
