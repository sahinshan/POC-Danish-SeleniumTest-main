using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;

namespace CareCloudTestFramework.PageObjects
{
    public class VisitDetailsPage : CommonMethods
    {

        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _VisitDetails = e => e.Marked("visit-details-card-body");

        readonly Func<AppQuery, AppWebQuery> _MileageField = e => e.XPath("//*[@class='mu-d-flex mu-justify-content-between mu-align-items-center']/h6[text()='Mileage (Calculated)']");

        readonly Func<AppQuery, AppWebQuery> _StaffOverrideMileageFieldLabel = e => e.XPath("//*[@class='mu-d-flex mu-justify-content-between mu-align-items-center']/h6[text()='Staff Override Mileage']");
        readonly Func<AppQuery, AppWebQuery> _ContactFieldLabel = e => e.XPath("//span[text()='Contact']");
        readonly Func<AppQuery, AppWebQuery> _HomePhoneFieldLabel = e => e.XPath("//div/h6[text()='Home Phone: ']");
        readonly Func<AppQuery, AppWebQuery> _MobilePhoneFieldLabel = e => e.XPath("//div/h6[text()='Mobile Phone: ']");

        readonly Func<AppQuery, AppWebQuery> _staffOverridemileage_Field = e => e.XPath("//*[@id='staff-override-mileage']");
        readonly Func<AppQuery, AppWebQuery> _problemsNNotesText_Field = e => e.XPath("//*[@id='problem-and-notes-textarea']");


        readonly Func<AppQuery, AppWebQuery> _miles_Field = e => e.XPath("//*[@id='staff-override-mileage']/following-sibling::span");

        Func<AppQuery, AppWebQuery> _RegularCareTask_ChkBox(String regularcaretaskid) => e => e.XPath("//*[@data-id='care-task-" + regularcaretaskid + "']//input[@name='careTasksCheckbox']");

        readonly Func<AppQuery, AppWebQuery> _AddCareTask_Btn = e => e.XPath("//*[@id='add-care-task-btn']");
        readonly Func<AppQuery, AppWebQuery> _EndVisit_Btn = e => e.XPath("//*[@id='end-visit-btn']");

        readonly Func<AppQuery, AppWebQuery> _DailyCareItem_Btn = e => e.XPath("//div[@id='non-scheduled-daily-care-btn-div']/button");

        Func<AppQuery, AppWebQuery> _DeleteCareTask_Btn(string caretaskid) => e => e.XPath("//*[@data-id='" + caretaskid + "']/parent::div/following-sibling::button");
        Func<AppQuery, AppWebQuery> _DailyCareRecordEdit_Btn(string activity) => e => e.XPath("//div[@linked-record-id='" + activity + "']");
        Func<AppQuery, AppWebQuery> _CareTab_Btn => e => e.XPath("//label/span[text()='Care']");



        public VisitDetailsPage(IApp app)
        {
            this._app = app;
        }
        public VisitDetailsPage WaitForVisitDetailsPagePageToLoad()
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            CheckIfElementVisible(_VisitDetails);


            return this;
        }

        public VisitDetailsPage ValidateMileageFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_MileageField);
                CheckIfElementVisible(_MileageField);
            }
            else
            {
                TryScrollToElement(_MileageField);
                Assert.IsFalse(CheckIfElementVisible(_MileageField));
            }

            return this;
        }

        public VisitDetailsPage ValidateStaffOverrideFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_StaffOverrideMileageFieldLabel);
                CheckIfElementVisible(_StaffOverrideMileageFieldLabel);

            }
            else
            {
                TryScrollToElement(_StaffOverrideMileageFieldLabel);
                Assert.IsFalse(CheckIfElementVisible(_StaffOverrideMileageFieldLabel));
            }

            return this;
        }

        public VisitDetailsPage ValidatecontactFieldVisible(bool ExpectFieldVisible)
        {
            if (ExpectFieldVisible)
            {
                ScrollToElement(_ContactFieldLabel);
                CheckIfElementVisible(_ContactFieldLabel);
                CheckIfElementVisible(_HomePhoneFieldLabel);
                CheckIfElementVisible(_MobilePhoneFieldLabel);


            }
            else
            {
                TryScrollToElement(_ContactFieldLabel);
                Assert.IsFalse(CheckIfElementVisible(_ContactFieldLabel));
                Assert.IsFalse(CheckIfElementVisible(_HomePhoneFieldLabel));
                Assert.IsFalse(CheckIfElementVisible(_MobilePhoneFieldLabel));


            }

            return this;
        }

        public VisitDetailsPage SetStaffOverRideMileage(string mileage)
        {


            _app.EnterText(_staffOverridemileage_Field, mileage);
            _app.DismissKeyboard();
            _app.Tap(_miles_Field);

            return this;
        }

        public VisitDetailsPage CheckRegualrCareTask(String caretaskid, bool RegularCareTaskChk)
        {

            if (RegularCareTaskChk)
            {
                ScrollToElement(_RegularCareTask_ChkBox(caretaskid));
                _app.Tap(_RegularCareTask_ChkBox(caretaskid));
            }
            else
            {

                Assert.IsTrue(CheckIfElementVisible(_RegularCareTask_ChkBox(caretaskid)));
            }

            return this;
        }

        public VisitDetailsPage TapAddCareTask()
        {
            ScrollToElement(_AddCareTask_Btn);
            WaitForElement(_AddCareTask_Btn);
            Tap(_AddCareTask_Btn);

            return this;
        }

        public VisitDetailsPage TapDeleteAdditionalCareTask(string caretaskid)
        {
            WaitForElement(_DeleteCareTask_Btn(caretaskid));
            Tap(_DeleteCareTask_Btn(caretaskid));

            return this;
        }


        public VisitDetailsPage ScrollAddDailyCareItem()
        {
            try
            {
                _app.ScrollDownTo(c => c.Css("mcc-button mcc-button--soft-primary mcc-button--sm mu-w-fit mu-mt-02"));
                //ScrollToElementWithAdditionalScrollDown(_DailyCareItem_Btn);
               
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            return this;
        }
        public VisitDetailsPage TapAddDailyCareItem()
        {
            Tap(_DailyCareItem_Btn);

            return this;
        }
       

        public VisitDetailsPage TapEndTheVisit()
        {
            WaitForElement(_EndVisit_Btn);
            Tap(_EndVisit_Btn);

            return this;
        }


        public VisitDetailsPage TapDailyCareRecord(String activity)
        {
            WaitForElement(_DailyCareRecordEdit_Btn(activity));
            Tap(_DailyCareRecordEdit_Btn(activity));

            return this;
        }

        public VisitDetailsPage SetProblesNNotesTextArea(string problemsNNotes)
        {


            _app.EnterText(_problemsNNotesText_Field, problemsNNotes);
            _app.DismissKeyboard();
            

            return this;
        }

        public VisitDetailsPage TapCareTab()
        {

            _app.Tap(_CareTab_Btn);

            return this;
        }

    }
}
