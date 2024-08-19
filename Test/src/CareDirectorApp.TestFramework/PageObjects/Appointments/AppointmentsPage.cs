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
    public class AppointmentsPage: CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _AppointmentsPageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("APPOINTMENTS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker_Container").Descendant().Text(ViewText);
        readonly Func<AppQuery, AppQuery> _genericViewPicker = e => e.Marked("ViewPicker");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");

        readonly Func<AppQuery, AppQuery> _LoadingSymbol = e => e.Marked("ProgressControl");


                
        Func<AppQuery, AppQuery> _activityAppointmentRecord_Cell(string RecordID) => e => e.Marked("appointment_Row_" + RecordID);
        Func<AppQuery, AppQuery> _activityAppointmentRecord_MonthAndDate(string RecordID, string MonthAndDate) => e => e.Marked("appointment_Row_" + RecordID).Descendant().Marked(MonthAndDate);
        Func<AppQuery, AppQuery> _activityAppointmentRecord_Year(string RecordID, string Year) => e => e.Marked("appointment_Row_" + RecordID).Descendant().Marked(Year);
        Func<AppQuery, AppQuery> _activityAppointmentRecord_ToogleIcon(string RecordID) => e => e.Marked("appointment_Row_" + RecordID).Descendant().Marked("toggleIcon");



        Func<AppQuery, AppQuery> _healthAppointmentRecord_Cell(string RecordID) => e => e.Marked("healthappointment_Row_" + RecordID);
        Func<AppQuery, AppQuery> _healthAppointmentRecord_MonthAndDate(string RecordID, string MonthAndDate) => e => e.Marked("healthappointment_Row_" + RecordID).Descendant().Marked(MonthAndDate);
        Func<AppQuery, AppQuery> _healthAppointmentRecord_Year(string RecordID, string Year) => e => e.Marked("healthappointment_Row_" + RecordID).Descendant().Marked(Year);
        Func<AppQuery, AppQuery> _healthAppointmentRecord_ToogleIcon(string RecordID) => e => e.Marked("healthappointment_Row_" + RecordID).Descendant().Marked("toggleIcon");



        Func<AppQuery, AppQuery> _additionalHealthProfessionalRecord_Cell(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_Row_" + RecordID);
        Func<AppQuery, AppQuery> _additionalHealthProfessionalRecord_MonthAndDate(string RecordID, string MonthAndDate) => e => e.Marked("healthappointmentadditionalprofessional_Row_" + RecordID).Descendant().Marked(MonthAndDate);
        Func<AppQuery, AppQuery> _additionalHealthProfessionalRecord_Year(string RecordID, string Year) => e => e.Marked("healthappointmentadditionalprofessional_Row_" + RecordID).Descendant().Marked(Year);
        Func<AppQuery, AppQuery> _additionalHealthProfessionalRecord_ToogleIcon(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_Row_" + RecordID).Descendant().Marked("toggleIcon");




        #region Labels

        Func<AppQuery, AppQuery> _activityAppointment_subject_Label(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("subject_0_Label");
        Func<AppQuery, AppQuery> _activityAppointment_startTime_Label(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("starttime_1_Label");
        Func<AppQuery, AppQuery> _activityAppointment_EndTime_Label(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("endtime_2_Label");
        Func<AppQuery, AppQuery> _activityAppointment_startDate_Label(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("startdate_3_Label");
        Func<AppQuery, AppQuery> _activityAppointment_category_Label(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("activitycategoryid_cwname_4_Label");
        Func<AppQuery, AppQuery> _activityAppointment_reason_Label(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("activityreasonid_cwname_5_Label");
        Func<AppQuery, AppQuery> _activityAppointment_location_Label(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("location_6_Label");
        Func<AppQuery, AppQuery> _activityAppointment_responsibleTeam_Label(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("ownerid_cwname_7_Label");
        Func<AppQuery, AppQuery> _activityAppointment_responsibleUser_Label(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("responsibleuserid_cwname_8_Label");


        Func<AppQuery, AppQuery> _healthAppointment_person_Label(string RecordID) => e => e.Marked("healthappointment_PrimaryRow_" + RecordID).Descendant().Marked("personid_cwname_0_Label");
        Func<AppQuery, AppQuery> _healthAppointment_startTime_Label(string RecordID) => e => e.Marked("healthappointment_PrimaryRow_" + RecordID).Descendant().Marked("starttime_1_Label");
        Func<AppQuery, AppQuery> _healthAppointment_EndTime_Label(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("endtime_2_Label");
        Func<AppQuery, AppQuery> _healthAppointment_startDate_Label(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("startdate_3_Label");
        Func<AppQuery, AppQuery> _healthAppointment_firstName_Label(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("personid_firstname_4_Label");
        Func<AppQuery, AppQuery> _healthAppointment_lastName_Label(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("personid_lastname_5_Label");
        Func<AppQuery, AppQuery> _healthAppointment_locationType_Label(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("healthappointmentlocationtypeid_cwname_6_Label");
        Func<AppQuery, AppQuery> _healthAppointment_location_Label(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("providerid_cwname_7_Label");
        Func<AppQuery, AppQuery> _healthAppointment_reason_Label(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("cancellationreasontypeid_cwname_8_Label");
        Func<AppQuery, AppQuery> _healthAppointment_additionalProfessionalRequired_Label(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("additionalprofessionalrequired_9_Label");
        Func<AppQuery, AppQuery> _healthAppointment_appointmentReason_Label(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("healthappointmentreasonid_cwname_10_Label");



        Func<AppQuery, AppQuery> _additionalHealthProfessional_person_Label(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_PrimaryRow_" + RecordID).Descendant().Marked("personid_cwname_0_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_startTime_Label(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_PrimaryRow_" + RecordID).Descendant().Marked("starttime_1_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_EndTime_Label(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("endtime_2_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_startDate_Label(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("startdate_3_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_endDate_Label(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("enddate_4_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_communityClinicAppointment_Label(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("healthappointmentid_cwname_5_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_responsibleTeam_Label(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("ownerid_cwname_6_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_leadProfessional_Label(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("healthappointmentid_healthprofessionalid_cwname_7_Label");
        #endregion

        #region Values

        Func<AppQuery, AppQuery> _activityAppointment_subject_Value(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("subject_0_Label");
        Func<AppQuery, AppQuery> _activityAppointment_startTime_Value(string RecordID) => e => e.Marked("appointment_PrimaryRow_" + RecordID).Descendant().Marked("starttime_1_Label");
        Func<AppQuery, AppQuery> _activityAppointment_EndTime_Value(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("endtime_2_Label");
        Func <AppQuery, AppQuery> _activityAppointment_startDate_Value(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("startdate_3_Label");
        Func<AppQuery, AppQuery> _activityAppointment_category_Value(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("activitycategoryid_cwname_4_Label");
        Func<AppQuery, AppQuery> _activityAppointment_reason_Value(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("activityreasonid_cwname_5_Label");
        Func<AppQuery, AppQuery> _activityAppointment_location_Value(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("location_6_Label");
        Func<AppQuery, AppQuery> _activityAppointment_responsibleTeam_Value(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("ownerid_cwname_7_Label");
        Func<AppQuery, AppQuery> _activityAppointment_responsibleUser_Value(string RecordID) => e => e.Marked("appointment_SecondaryRow_" + RecordID).Descendant().Marked("responsibleuserid_cwname_8_Label");


        Func<AppQuery, AppQuery> _healthAppointment_person_Value(string RecordID) => e => e.Marked("healthappointment_PrimaryRow_" + RecordID).Descendant().Marked("personid_cwname_0_Label");
        Func<AppQuery, AppQuery> _healthAppointment_startTime_Value(string RecordID) => e => e.Marked("healthappointment_PrimaryRow_" + RecordID).Descendant().Marked("starttime_1_Label");
        Func<AppQuery, AppQuery> _healthAppointment_EndTime_Value(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("endtime_2_Label");
        Func<AppQuery, AppQuery> _healthAppointment_startDate_Value(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("startdate_3_Label");
        Func<AppQuery, AppQuery> _healthAppointment_firstName_Value(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("personid_firstname_4_Label");
        Func<AppQuery, AppQuery> _healthAppointment_lastName_Value(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("personid_lastname_5_Label");
        Func<AppQuery, AppQuery> _healthAppointment_locationType_Value(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("healthappointmentlocationtypeid_cwname_6_Label");
        Func<AppQuery, AppQuery> _healthAppointment_location_Value(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("providerid_cwname_7_Label");
        Func<AppQuery, AppQuery> _healthAppointment_reason_Value(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("cancellationreasontypeid_cwname_8_Label");
        Func<AppQuery, AppQuery> _healthAppointment_additionalProfessionalRequired_Value(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("additionalprofessionalrequired_9_Label");
        Func<AppQuery, AppQuery> _healthAppointment_appointmentReason_Value(string RecordID) => e => e.Marked("healthappointment_SecondaryRow_" + RecordID).Descendant().Marked("healthappointmentreasonid_cwname_10_Label");



        Func<AppQuery, AppQuery> _additionalHealthProfessional_person_Value(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_PrimaryRow_" + RecordID).Descendant().Marked("personid_cwname_0_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_startTime_Value(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_PrimaryRow_" + RecordID).Descendant().Marked("starttime_1_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_EndTime_Value(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("endtime_2_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_startDate_Value(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("startdate_3_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_endDate_Value(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("enddate_4_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_communityClinicAppointment_Value(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("healthappointmentid_cwname_5_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_responsibleTeam_Value(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("ownerid_cwname_6_Label");
        Func<AppQuery, AppQuery> _additionalHealthProfessional_leadProfessional_Value(string RecordID) => e => e.Marked("healthappointmentadditionalprofessional_SecondaryRow_" + RecordID).Descendant().Marked("healthappointmentid_healthprofessionalid_cwname_7_Label");
        #endregion



        Func<AppQuery, AppQuery> _screenText (string MatchText) => e => e.Marked(MatchText);





        public AppointmentsPage(IApp app)
        {
            _app = app;
        }




        public AppointmentsPage WaitForAppointmentsPageToLoad(string ViewText = "Today's Appointments")
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_AppointmentsPageIconButton);
            _app.WaitForElement(_pageTitle);

            _app.WaitForElement(_viewPicker(ViewText));

            _app.WaitForElement(_refreshButton);

            return this;
        }

        public AppointmentsPage WaitForLoadSymbolToBeRemoved()
        {
            WaitForElementNotVisible(_LoadingSymbol);

            return this;
        }

        public AppointmentsPage TapViewPicker()
        {
            Tap(_genericViewPicker);
            return this;
        }

        public AppointmentsPage TapRefreshButton()
        {
            Tap(_refreshButton);

            return this;
        }



        public AppointmentsPage TapOnActivityAppointmenToogleIcon(string AppointmentID)
        {
            Tap(_activityAppointmentRecord_ToogleIcon(AppointmentID));

            return this;
        }

        public AppointmentsPage TapOnActivityAppointmentRecord(string AppointmentID)
        {
            Tap(_activityAppointmentRecord_Cell(AppointmentID));

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentRecordMonthAndDateInfo(string AppointmentID, string MonthAndDateInfo)
        {
            WaitForElement(_activityAppointmentRecord_MonthAndDate(AppointmentID, MonthAndDateInfo));

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentRecordYearInfo(string AppointmentID, string YearInfo)
        {
            WaitForElement(_activityAppointmentRecord_Year(AppointmentID, YearInfo));

            return this;
        }


        public AppointmentsPage TapOnHealthAppointmentToogleIcon(string AppointmentID)
        {
            Tap(_healthAppointmentRecord_ToogleIcon(AppointmentID));

            return this;
        }

        public AppointmentsPage TapOnHealthAppointmentRecord(string AppointmentID)
        {
            Tap(_healthAppointmentRecord_Cell(AppointmentID));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentRecordMonthAndDateInfo(string AppointmentID, string MonthAndDateInfo)
        {
            WaitForElement(_healthAppointmentRecord_MonthAndDate(AppointmentID, MonthAndDateInfo));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentRecordYearInfo(string AppointmentID, string YearInfo)
        {
            WaitForElement(_healthAppointmentRecord_Year(AppointmentID, YearInfo));

            return this;
        }


        public AppointmentsPage TapOnAdditionalHealthProfessionalRecordToogleIcon(string AppointmentID)
        {
            Tap(_additionalHealthProfessionalRecord_ToogleIcon(AppointmentID));

            return this;
        }

        public AppointmentsPage TapOnAdditionalHealthProfessionalRecord(string AppointmentID)
        {
            Tap(_additionalHealthProfessionalRecord_Cell(AppointmentID));

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalRecordMonthAndDateInfo(string AppointmentID, string MonthAndDateInfo)
        {
            WaitForElement(_additionalHealthProfessionalRecord_MonthAndDate(AppointmentID, MonthAndDateInfo));

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalRecordYearInfo(string AppointmentID, string YearInfo)
        {
            WaitForElement(_additionalHealthProfessionalRecord_Year(AppointmentID, YearInfo));

            return this;
        }





        public AppointmentsPage ValidateActivityAppointmentSubjectLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_activityAppointment_subject_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_activityAppointment_subject_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_activityAppointment_subject_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_activityAppointment_subject_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentStartTimeLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_activityAppointment_startTime_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_activityAppointment_startTime_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_activityAppointment_startTime_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_activityAppointment_startTime_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentEndTimeLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_activityAppointment_EndTime_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_activityAppointment_EndTime_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_activityAppointment_EndTime_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_activityAppointment_EndTime_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentStartDateLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_activityAppointment_startDate_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_activityAppointment_startDate_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_activityAppointment_category_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_activityAppointment_category_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentCategoryLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_activityAppointment_category_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_activityAppointment_category_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_activityAppointment_category_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_activityAppointment_category_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentReasonLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_activityAppointment_reason_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_activityAppointment_reason_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_activityAppointment_reason_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_activityAppointment_reason_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentLocationLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_activityAppointment_location_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_activityAppointment_location_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_activityAppointment_location_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_activityAppointment_location_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentResponsibleTeamLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_activityAppointment_responsibleTeam_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_activityAppointment_responsibleTeam_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_activityAppointment_responsibleTeam_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_activityAppointment_responsibleTeam_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentResponsibleUserLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_activityAppointment_responsibleUser_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_activityAppointment_responsibleUser_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_activityAppointment_responsibleUser_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_activityAppointment_responsibleUser_Label(AppointmentID)));
            }
                
            return this;
        }





        public AppointmentsPage ValidateHealthAppointmentPersonLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_person_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_person_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_person_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_person_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentStartTimeLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_startTime_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_startTime_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_startTime_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_startTime_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentEndTimeLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_EndTime_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_EndTime_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_EndTime_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_EndTime_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentStartDateLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_startDate_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_startDate_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_startDate_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_startDate_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentFirstNameLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_firstName_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_firstName_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_firstName_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_firstName_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentlastNameLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_lastName_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_lastName_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_lastName_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_lastName_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentLocationTypeLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_locationType_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_locationType_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_locationType_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_locationType_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentLocationLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_location_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_location_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_location_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_location_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentReasonLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_reason_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_reason_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_reason_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_reason_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentAdditionalProfessionalRequiredLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_additionalProfessionalRequired_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_additionalProfessionalRequired_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_additionalProfessionalRequired_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_additionalProfessionalRequired_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentAppointmentReasonLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_healthAppointment_appointmentReason_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_healthAppointment_appointmentReason_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_healthAppointment_appointmentReason_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_healthAppointment_appointmentReason_Label(AppointmentID)));
            }

            return this;
        }





        public AppointmentsPage ValidateAdditionalHealthProfessionalPersonLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_person_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_additionalHealthProfessional_person_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_additionalHealthProfessional_person_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_additionalHealthProfessional_person_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalStartTimeLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_startTime_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_additionalHealthProfessional_startTime_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_additionalHealthProfessional_startTime_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_additionalHealthProfessional_startTime_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalEndTimeLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_EndTime_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_additionalHealthProfessional_EndTime_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_additionalHealthProfessional_EndTime_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_additionalHealthProfessional_EndTime_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalStartDateLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_startDate_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_additionalHealthProfessional_startDate_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_additionalHealthProfessional_startDate_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_additionalHealthProfessional_startDate_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalEndDateLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_endDate_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_additionalHealthProfessional_endDate_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_additionalHealthProfessional_endDate_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_additionalHealthProfessional_endDate_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalCommunityClinicAppointmentLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_communityClinicAppointment_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_additionalHealthProfessional_communityClinicAppointment_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_additionalHealthProfessional_communityClinicAppointment_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_additionalHealthProfessional_communityClinicAppointment_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalResponsibleTeamLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_responsibleTeam_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_additionalHealthProfessional_responsibleTeam_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_additionalHealthProfessional_responsibleTeam_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_additionalHealthProfessional_responsibleTeam_Label(AppointmentID)));
            }

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalLeadProfessionalLabelVisibility(string AppointmentID, bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_leadProfessional_Label(AppointmentID));
                Assert.IsTrue(CheckIfElementVisible(_additionalHealthProfessional_leadProfessional_Label(AppointmentID)));
            }
            else
            {
                TryScrollToElement(_additionalHealthProfessional_leadProfessional_Label(AppointmentID));
                Assert.IsFalse(CheckIfElementVisible(_additionalHealthProfessional_leadProfessional_Label(AppointmentID)));
            }

            return this;
        }








        public AppointmentsPage ValidateActivityAppointmentSubjectValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_activityAppointment_subject_Value(AppointmentID));
            Assert.AreEqual("Subject: " + ExpectedValue, GetElementText(_activityAppointment_subject_Value(AppointmentID)));
            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentStartTimeValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_activityAppointment_startTime_Value(AppointmentID));
            Assert.AreEqual("Start Time: " + ExpectedValue, GetElementText(_activityAppointment_startTime_Value(AppointmentID)));
            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentEndTimeValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_activityAppointment_EndTime_Value(AppointmentID));
            Assert.AreEqual("End Time: " + ExpectedValue, GetElementText(_activityAppointment_EndTime_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentStartDateValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_activityAppointment_startDate_Value(AppointmentID));
            Assert.AreEqual("Start Date: " + ExpectedValue, GetElementText(_activityAppointment_startDate_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentCategoryValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_activityAppointment_category_Value(AppointmentID));
            Assert.AreEqual("Category: " + ExpectedValue, GetElementText(_activityAppointment_category_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentReasonValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_activityAppointment_reason_Value(AppointmentID));
            Assert.AreEqual("Reason: " + ExpectedValue, GetElementText(_activityAppointment_reason_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentLocationValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_activityAppointment_location_Value(AppointmentID));
            Assert.AreEqual("Location: " + ExpectedValue, GetElementText(_activityAppointment_location_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentResponsibleTeamValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_activityAppointment_responsibleTeam_Value(AppointmentID));
            Assert.AreEqual("Responsible Team: " + ExpectedValue, GetElementText(_activityAppointment_responsibleTeam_Value(AppointmentID)));


            return this;
        }

        public AppointmentsPage ValidateActivityAppointmentResponsibleUserValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_activityAppointment_responsibleUser_Value(AppointmentID));
            Assert.AreEqual("Responsible User: " + ExpectedValue, GetElementText(_activityAppointment_responsibleUser_Value(AppointmentID)));

            return this;
        }





        public AppointmentsPage ValidateHealthAppointmentPersonValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_person_Value(AppointmentID));
            Assert.AreEqual("Person: " + ExpectedValue, GetElementText(_healthAppointment_person_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentStartTimeValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_startTime_Value(AppointmentID));
            Assert.AreEqual("Start Time: " + ExpectedValue, GetElementText(_healthAppointment_startTime_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentEndTimeValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_EndTime_Value(AppointmentID));
            Assert.AreEqual("End Time: " + ExpectedValue, GetElementText(_healthAppointment_EndTime_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentStartDateValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_startDate_Value(AppointmentID));
            Assert.AreEqual("Start Date: " + ExpectedValue, GetElementText(_healthAppointment_startDate_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentFirstNameValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_firstName_Value(AppointmentID));
            Assert.AreEqual("First Name: " + ExpectedValue, GetElementText(_healthAppointment_firstName_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentlastNameValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_lastName_Value(AppointmentID));
            Assert.AreEqual("Last Name: " + ExpectedValue, GetElementText(_healthAppointment_lastName_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentLocationTypeValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_locationType_Value(AppointmentID));
            Assert.AreEqual("Location Type: " + ExpectedValue, GetElementText(_healthAppointment_locationType_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentLocationValue(string AppointmentID, string ExpectedValue)
        {
            ScrollToElementWithWidthAndHeight(_healthAppointment_location_Value(AppointmentID));
            Assert.AreEqual("Location: " + ExpectedValue, GetElementText(_healthAppointment_location_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentReasonValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_reason_Value(AppointmentID));
            Assert.AreEqual("Reason: " + ExpectedValue, GetElementText(_healthAppointment_reason_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentAdditionalProfessionalRequiredValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_additionalProfessionalRequired_Value(AppointmentID));
            Assert.AreEqual("Additional Professional Required?: " + ExpectedValue, GetElementText(_healthAppointment_additionalProfessionalRequired_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateHealthAppointmentAppointmentReasonValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_healthAppointment_appointmentReason_Value(AppointmentID));
            Assert.AreEqual("Appointment Reason: " + ExpectedValue, GetElementText(_healthAppointment_appointmentReason_Value(AppointmentID)));

            return this;
        }





        public AppointmentsPage ValidateAdditionalHealthProfessionalPersonValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_person_Value(AppointmentID));
            Assert.AreEqual("Person: " + ExpectedValue, GetElementText(_additionalHealthProfessional_person_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalStartTimeValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_startTime_Value(AppointmentID));
            Assert.AreEqual("Start Time: " + ExpectedValue, GetElementText(_additionalHealthProfessional_startTime_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalEndTimeValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_EndTime_Value(AppointmentID));
            Assert.AreEqual("End Time: " + ExpectedValue, GetElementText(_additionalHealthProfessional_EndTime_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalStartDateValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_startDate_Value(AppointmentID));
            Assert.AreEqual("Start Date: " + ExpectedValue, GetElementText(_additionalHealthProfessional_startDate_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalEndDateValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_endDate_Value(AppointmentID));
            Assert.AreEqual("End Date: " + ExpectedValue, GetElementText(_additionalHealthProfessional_endDate_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalCommunityClinicAppointmentValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_communityClinicAppointment_Value(AppointmentID));
            Assert.AreEqual("Community/Clinic Appointment: " + ExpectedValue, GetElementText(_additionalHealthProfessional_communityClinicAppointment_Value(AppointmentID)));

            return this;
        }


        public AppointmentsPage ValidateAdditionalHealthProfessionalResponsibleTeamValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_responsibleTeam_Value(AppointmentID));
            Assert.AreEqual("Responsible Team: " + ExpectedValue, GetElementText(_additionalHealthProfessional_responsibleTeam_Value(AppointmentID)));

            return this;
        }

        public AppointmentsPage ValidateAdditionalHealthProfessionalLeadProfessionalValue(string AppointmentID, string ExpectedValue)
        {

            ScrollToElementWithWidthAndHeight(_additionalHealthProfessional_leadProfessional_Value(AppointmentID));
            Assert.AreEqual("Lead Professional: " + ExpectedValue, GetElementText(_additionalHealthProfessional_leadProfessional_Value(AppointmentID)));

            return this;
        }





    }
}
