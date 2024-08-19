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
    public class PersonAppointmentsPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _pageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("APPOINTMENTS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _viewPickerSelectElement = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");


        Func<AppQuery, AppQuery> _recordText(string RecordText) => e => e.Marked(RecordText);
        Func<AppQuery, AppQuery> _toggleIcon(string RecordID) => e => e.Marked("appointment_Row_" + RecordID).Descendant().Marked("toggleIcon");


        Func<AppQuery, AppQuery> _record_Cell(string RecordID, string CellName, string CellText) => e => e.Marked("appointment_Row_" + RecordID + "_Cell_" + CellName).Descendant().Marked(CellText);

        readonly Func<AppQuery, AppQuery> _noRecordsLabel = e => e.Marked("NO RECORDS");
        readonly Func<AppQuery, AppQuery> _noRecordsMessage = e => e.Marked("No results were found for this screen.");


        #region Mobile mode labels

        Func<AppQuery, AppQuery> _subjectLabel(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("subject_0_Label");
        Func<AppQuery, AppQuery> _starttimeLabel(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("starttime_1_Label");
        Func<AppQuery, AppQuery> _endtimeLabel(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("endtime_2_Label");
        Func<AppQuery, AppQuery> _startdateLabel(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("startdate_3_Label");
        Func<AppQuery, AppQuery> _activitycategoryidLabel(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("activitycategoryid_cwname_4_Label");
        Func<AppQuery, AppQuery> _activityreasonidLabel(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("activityreasonid_cwname_5_Label");
        Func<AppQuery, AppQuery> _locationLabel(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("location_6_Label");
        Func<AppQuery, AppQuery> _responsibleTeamLabel(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("ownerid_cwname_7_Label");
        Func<AppQuery, AppQuery> _responsibleUserLabel(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("responsibleuserid_cwname_8_Label");

        #endregion

        #region Mobile mode field values

        Func<AppQuery, AppQuery> _subjectValue(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("subject_0_Value");
        Func<AppQuery, AppQuery> _starttimeValue(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("starttime_1_Value");
        Func<AppQuery, AppQuery> _endtimeValue(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("endtime_2_Value");
        Func<AppQuery, AppQuery> _startdateValue(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("startdate_3_Value");
        Func<AppQuery, AppQuery> _activitycategoryidValue(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("activitycategoryid_cwname_4_Value");
        Func<AppQuery, AppQuery> _activityreasonidValue(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("activityreasonid_cwname_5_Value");
        Func<AppQuery, AppQuery> _locationValue(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("location_6_Value");
        Func<AppQuery, AppQuery> _responsibleTeamValue(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("ownerid_cwname_7_Value");
        Func<AppQuery, AppQuery> _responsibleUserValue(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("responsibleuserid_cwname_8_Value");

        #endregion


        public PersonAppointmentsPage(IApp app)
        {
            _app = app;
        }


        public PersonAppointmentsPage WaitForPersonAppointmentsPageToLoad(string ViewText = "Active Appointments")
        {
            WaitForElement(_mainMenu);
            WaitForElement(_caredirectorIcon);

            WaitForElement(_backButton);
            WaitForElement(_pageIconButton);
            WaitForElement(_pageTitle);

            WaitForElement(_viewPicker(ViewText));

            WaitForElement(_searchTextBox);
            WaitForElement(_searchButton);
            WaitForElement(_refreshButton);

            return this;
        }


        #region Methods used when displaying the APP in mobile mode



        #endregion

        #region Methods used when displaying the APP in tablet mode

        public PersonAppointmentsPage ValidateSubjectCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "subject", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAppointmentsPage ValidateStartTimeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "starttime", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAppointmentsPage ValidateEndTimeCellText(string ExpectedText, string RecordID)
        {
            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "endtime", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAppointmentsPage ValidateStartDateCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "startdate", ExpectedText));

            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "startdate", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAppointmentsPage ValidateActivityCategoryCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "activitycategoryid", ExpectedText));

            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "activitycategoryid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAppointmentsPage ValidateActivityReasonCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "activityreasonid", ExpectedText));

            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "activityreasonid", ExpectedText));
            Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAppointmentsPage ValidateLocationCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "location", ExpectedText));

            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "location", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAppointmentsPage ValidateOwnerCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "ownerid", ExpectedText));

            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "ownerid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        public PersonAppointmentsPage ValidateResponsibleUserCellText(string ExpectedText, string RecordID)
        {
            TryScrollToElement(_record_Cell(RecordID, "responsibleuserid", ExpectedText));

            bool elementVisible = CheckIfElementVisible(_record_Cell(RecordID, "responsibleuserid", ExpectedText));

            if (string.IsNullOrEmpty(ExpectedText))
                Assert.IsFalse(elementVisible);
            else
                Assert.IsTrue(elementVisible);

            return this;
        }

        #endregion



        public PersonAppointmentsPage TapOnRecord(string recordText)
        {
            WaitForElement(_recordText(recordText));
            Tap(_recordText(recordText));

            return this;
        }

        public PersonAppointmentsPage TapOnViewPicker()
        {
            WaitForElement(_viewPickerSelectElement);
            Tap(_viewPickerSelectElement);

            return this;
        }

        public PersonAppointmentsPage ValidateNoRecordsMessageVisibility(bool NoRecordsMessageVisible)
        {
            bool elementVisible = CheckIfElementVisible(_noRecordsLabel);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            elementVisible = CheckIfElementVisible(_noRecordsMessage);
            Assert.AreEqual(NoRecordsMessageVisible, elementVisible);

            return this;
        }
    }
}
