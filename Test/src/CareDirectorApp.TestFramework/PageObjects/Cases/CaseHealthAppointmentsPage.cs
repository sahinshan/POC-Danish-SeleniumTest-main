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
    public class CaseHealthAppointmentsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("healthappointment_");
        readonly Func<AppQuery, AppQuery> _casesPageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("HEALTH APPOINTMENTS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string CaseNoteRecordID) => e => e.Marked("healthappointment_Row_" + CaseNoteRecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("healthappointment_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");




        public CaseHealthAppointmentsPage(IApp app)
        {
            _app = app;
        }


        public CaseHealthAppointmentsPage WaitForCaseHealthAppointmentsPageToLoad(string ViewText = "Last Month Appointments")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            //WaitForElement(_addNewRecordButton);
            WaitForElement(_casesPageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            return this;
        }


        public CaseHealthAppointmentsPage WaitForCaseHealthAppointmentsPageToLoad1(string ViewText = "Active Appointments")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            //WaitForElement(_addNewRecordButton);
            WaitForElement(_casesPageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            return this;
        }




        #region Methods used when displaying the APP in tablet mode

        public CaseHealthAppointmentsPage ValidateResponsibleTeamCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "ownerid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateStartTimeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "starttime", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateEndTimeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "endtime", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateStartDateCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "startdate", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateFirstNameCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "firstname", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateLastNameCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "lastname", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "lastname", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateRelatedCaseCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "caseid", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "caseid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateLocationTypeCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "healthappointmentlocationtypeid", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "healthappointmentlocationtypeid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateLocationCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "providerid", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "providerid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateReasonCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "cancellationreasontypeid", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "cancellationreasontypeid", ExpectedText));
            if(string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateAdditionalProfessionalRequeiredCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "additionalprofessionalrequired", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "additionalprofessionalrequired", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateAppointmentReasonCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "healthappointmentreasonid", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "healthappointmentreasonid", ExpectedText));
            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateOutcomeCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "healthappointmentoutcometypeid", ExpectedText));
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "healthappointmentoutcometypeid", ExpectedText));
            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }


        #endregion


        public CaseHealthAppointmentsPage TapOnAddNewRecordButton()
        {
            this.Tap(_addNewRecordButton);

            return this;
        }

        public CaseHealthAppointmentsPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public CaseHealthAppointmentsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public CaseHealthAppointmentsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
