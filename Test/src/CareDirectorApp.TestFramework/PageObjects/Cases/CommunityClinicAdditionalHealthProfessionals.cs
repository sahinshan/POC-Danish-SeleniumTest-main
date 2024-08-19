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
    public class CommunityClinicAdditionalHealthProfessionals : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("healthappointmentadditionalprofessional_");
        readonly Func<AppQuery, AppQuery> _casesPageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("COMMUNITY/CLINIC ADDITIONAL HEALTH PROFESSIONALS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");

        
        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string CaseNoteRecordID) => e => e.Marked("healthappointmentadditionalprofessional_Row_" + CaseNoteRecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("healthappointmentadditionalprofessional_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");




        public CommunityClinicAdditionalHealthProfessionals(IApp app)
        {
            _app = app;
        }


        public CommunityClinicAdditionalHealthProfessionals WaitForCommunityClinicAdditionalHealthProfessionalsToLoad(string ViewText = "Community Clinic Appointments Additional Professionals View")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_addNewRecordButton);
            WaitForElement(_casesPageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            return this;
        }




        #region Methods used when displaying the APP in tablet mode

        public CommunityClinicAdditionalHealthProfessionals ValidateHealthProfessionalCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "healthprofessionalid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals ValidateStartDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "startdate", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals ValidateStartTimeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "starttime", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals ValidateEndDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "enddate", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals ValidateEndTimeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "endtime", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals ValidateProfessionalRemainingForFullDurationCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "professionalremainingforfullduration", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "professionalremainingforfullduration", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals ValidateReturnToBaseAfterAppointmentCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "returntobaseafterappointment", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "returntobaseafterappointment", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals ValidateTravelTimeInMinutesCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "traveltimeinminutes", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "traveltimeinminutes", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals ValidateTravelTimeBackToBaseInMinutesCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "traveltimebacktobaseinminutes", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "traveltimebacktobaseinminutes", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }


        #endregion


        public CommunityClinicAdditionalHealthProfessionals TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public CommunityClinicAdditionalHealthProfessionals ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
