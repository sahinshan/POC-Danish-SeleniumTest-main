using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Security.Policy;
using System.Runtime.InteropServices.ComTypes;

namespace CareCloudTestFramework.PageObjects
{
    public class RepositioningRecordPage : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _saveNCloseButton = e => e.XPath("//button/span[text()='Save and close']");
        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button/span[text()='Cancel']");

        Func<AppQuery, AppWebQuery> _PersonInfoHeader(string personfullname, int number) => e => e.XPath("//span[text()=' " + personfullname + " (ID " + number + ")']");
        Func<AppQuery, AppWebQuery> _RepositioningHeader => e => e.XPath("//div[text()='Repositioning']");
        Func<AppQuery, AppWebQuery> _ConsentGivenHeader => e => e.XPath("//div[text()='Consent given']");
        Func<AppQuery, AppWebQuery> _PreferencesTextFld => e => e.XPath("//div[@id='repositioning-preferences']/div/textarea");
        Func<AppQuery, AppWebQuery> _RepositioningFields(string option) => e => e.XPath("//input[@text='"+ option + "']/parent::label");

        Func<AppQuery, AppWebQuery> _RepositionedToFields(string option) => e => e.XPath("//input[@text='" + option + "' and @name='repositioning-ending-position']/parent::label");
        Func<AppQuery, AppWebQuery> _RepositioningRepositionedTo(string option) => e => e.XPath("//input[@text='" + option + "' and @name='repositioning-repositioned-to']/parent::label");
        Func<AppQuery, AppWebQuery> _RepositioningComfortable(string option) => e => e.XPath("//input[@text='" + option + "' and @name='repositioning-comfortable']/parent::label");

        Func<AppQuery, AppWebQuery> _MatressesInUse(string option) => e => e.XPath("//input[@text='" + option + "' and @name='repositioning-specialist-mattress']/parent::label");
        Func<AppQuery, AppWebQuery> _MatressesSwitchedOn(string option) => e => e.XPath("//input[@text='" + option + "'and @name='repositioning-mattress-switched-on']/parent::label");
        Func<AppQuery, AppWebQuery> _MatressesCorrectPostion(string option) => e => e.XPath("//input[@text='" + option + "'and @name='repositioning-mattress-correct-position']/parent::label");
        Func<AppQuery, AppWebQuery> _MatressesWorking(string option) => e => e.XPath("//input[@text='" + option + "'and @name='repositioning-is-mattress-working']/parent::label");

        Func<AppQuery, AppWebQuery> _ConcernsWithSkinRadioBtn(string option) => e => e.XPath("//input[@text='"+option+"'and @name='repositioning-are-there-any-new-concerns-with-the-resident-skin']/parent::label");
        Func<AppQuery, AppWebQuery> _WhereOnTheBodyTxtArea => e => e.XPath("//textarea[@id='repositioning-skin-condition-where']");
        Func<AppQuery, AppWebQuery> _SkinConditionBtn => e => e.XPath("//button[@id='add-repositioning-describe-skin-condition']");
        Func<AppQuery, AppWebQuery> _EditSkinConditionBtn => e => e.XPath("//button[@id='edit-repositioning-describe-skin-condition']");

        Func<AppQuery, AppWebQuery> _SkinConditionOtherTxtArea => e => e.XPath("//textarea[@id='repositioning-skin-conditions-other']");
        Func<AppQuery, AppWebQuery> _LocationRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='repositioning-care-physical-locations-list']/parent::label");
        Func<AppQuery, AppWebQuery> _LocationOtherTxtArea => e => e.XPath("//textarea[@id='repositioning-location-if-other']");
        Func<AppQuery, AppWebQuery> _EquipmentRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='repositioning-care-equipment-list']/parent::label");
        Func<AppQuery, AppWebQuery> _WellBeingRadioBtn(string dataid) => e => e.XPath("//label[@data-id='label-" + dataid + "']");
        Func<AppQuery, AppWebQuery> _AssistanceNeededRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='repositioning-care-assistance-needed']/parent::label");
        Func<AppQuery, AppWebQuery> _Occuredtimefld => e => e.XPath("//*[@id='repositioning-occurred-time']");
        Func<AppQuery, AppWebQuery> _Add5MinTimeSpentButton=> e => e.XPath("//*[@id='add-5min-time-spent-btn']");
        Func<AppQuery, AppWebQuery> _AdditionalNotesTxtFld => e => e.XPath("//*[@id='repositioning-additional-notes-text']");
        Func<AppQuery, AppWebQuery> _CareNotesTxtFld => e => e.XPath("//*[@id='careNotes']");
        Func<AppQuery, AppWebQuery> _FlagRecordForHandoverRadioBtn(int value) => e => e.XPath("//input[@value='" + value + "' and @name='flagRecordForHandover']/parent::label");
        Func<AppQuery, AppWebQuery> _HandoverComentsTxtFld => e => e.XPath("//*[@id='careNotes']");
        Func<AppQuery, AppWebQuery> _TimeCareGivenValidation1 => e => e.XPath("//div[@id='repositioning-occurred-error']/span[text()='You cannot select future time.']");
        Func<AppQuery, AppWebQuery> _TimeCareGivenValidation2 => e => e.XPath("//div[@id='repositioning-occurred-2-error']/span[text()='Time care was given and time spent with person cannot be greater than the current time when combined.']");
        Func<AppQuery, AppWebQuery> _TimeSpentValidation => e => e.XPath("//div[@id='repositioning-time-spent-with-client-2-error']/span[text()='Time care was given and time spent with person cannot be greater than the current time when combined.']");

        Func<AppQuery, AppQuery> _WebViewElement = e => e.Class("android.webkit.WebView");


        public RepositioningRecordPage(IApp app)
        {
            _app = app;

        }



        public RepositioningRecordPage WaitForRepositioningRecordPageLookupToLoad(string fullname,int number)
        {
            //WaitForElement(_saveNCloseButton);
            //WaitForElement(_cancelButton);
            WaitForElement(_PersonInfoHeader(fullname, number));
            WaitForElement(_RepositioningHeader);
            WaitForElement(_ConsentGivenHeader);

            return this;
        }

        public RepositioningRecordPage WaitForRepositioningRecordPageLookupToReLoad(string fullname, int number)
        {
         
            WaitForElement(_PersonInfoHeader(fullname, number));
          
            return this;
        }

        public RepositioningRecordPage validatePreferencesText(string personname)
        {
            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_PreferencesTextFld,"No preferences recorded, please check with " + personname +"");

            return this;

        }

        public RepositioningRecordPage TapStartingPosition(String Position)
        {
            _app.Tap(_RepositioningFields(Position));

            return this;
        }

        public RepositioningRecordPage SelectIsRepositioningRequired(String Position)
        {
            _app.Tap(_RepositioningFields(Position));

            return this;
        }

        public RepositioningRecordPage TapRepositionedTo(String Position)
        {
            _app.Tap(_RepositionedToFields(Position));

            return this;
        }

        public RepositioningRecordPage SelectRepositionedToSide(String Position)
        {
            _app.Tap(_RepositioningRepositionedTo(Position));

            return this;
        }

        public RepositioningRecordPage SelectAreTheyComfortable(String Position)
        {
            ScrollDownWithinElement(_RepositioningComfortable(Position), _WebViewElement);
            _app.Tap(_RepositioningComfortable(Position));

            return this;
        }

        public RepositioningRecordPage SelectTypeOfMatressInUse(String option)
        {
            ScrollDownWithinElement(_MatressesInUse(option), _WebViewElement);
            _app.Tap(_MatressesInUse(option));

            return this;
        }

        public RepositioningRecordPage SelectIsTheMatressOn(String option)
        {
            ScrollDownWithinElement(_MatressesSwitchedOn(option), _WebViewElement);
            _app.Tap(_MatressesSwitchedOn(option));

            return this;
        }

        public RepositioningRecordPage SelectIsTheMatressInPosition(String option)
        {
            ScrollDownWithinElement(_MatressesCorrectPostion(option), _WebViewElement);
            _app.Tap(_MatressesCorrectPostion(option));

            return this;
        }

        public RepositioningRecordPage SelectIsTheMatressWorking(String option)
        {
            ScrollDownWithinElement(_MatressesWorking(option), _WebViewElement);
            _app.Tap(_MatressesWorking(option));

            return this;
        }

        public RepositioningRecordPage SelectConcernsWithSkinCondition(String option)
        {
            ScrollDownWithinElement(_ConcernsWithSkinRadioBtn(option), _WebViewElement);
            _app.Tap(_ConcernsWithSkinRadioBtn(option));

            return this;
        }

        public RepositioningRecordPage SetWhereonTheBodyTxtArea(String Text)
        {
            ScrollDownWithinElement(_WhereOnTheBodyTxtArea, _WebViewElement);
            _app.ClearText(_WhereOnTheBodyTxtArea);
            _app.DismissKeyboard();
            _app.EnterText(_WhereOnTheBodyTxtArea, Text);
            _app.DismissKeyboard(); 

            return this;
        }

        public RepositioningRecordPage TapSelectSkinCondition()
        {
            ScrollDownWithinElement(_SkinConditionBtn, _WebViewElement);
            _app.Tap(_SkinConditionBtn);

            return this;
        }


        public RepositioningRecordPage TapEditSkinCondition()
        {
            ScrollDownWithinElement(_EditSkinConditionBtn, _WebViewElement);
            _app.Tap(_EditSkinConditionBtn);

            return this;
        }

        public RepositioningRecordPage SetSkinConditionOtherTxtArea(String Text)
        {
            _app.ClearText(_SkinConditionOtherTxtArea);
            _app.DismissKeyboard();

            _app.EnterText(_SkinConditionOtherTxtArea, Text);
            _app.DismissKeyboard();

            return this;
        }

        public RepositioningRecordPage TapLocationButton(string dataid)
        {
            ScrollDownWithinElement(_LocationRadioBtn(dataid), _WebViewElement);
            _app.Tap(_LocationRadioBtn(dataid));

            return this;
        }

        public RepositioningRecordPage SetLocationOtherTxtArea(String Text)
        {
            _app.ClearText(_LocationOtherTxtArea);
            _app.DismissKeyboard();

            _app.EnterText(_LocationOtherTxtArea, Text);
            _app.DismissKeyboard();

            return this;
        }
        public RepositioningRecordPage TapEquipmentButton(string dataid)
        {
            ScrollDownWithinElement(_EquipmentRadioBtn(dataid), _WebViewElement);
            _app.Tap(_EquipmentRadioBtn(dataid));

            return this;
        }

        public RepositioningRecordPage TapWellbeingButton(string dataid)
        {
            ScrollDownWithinElement(_WellBeingRadioBtn(dataid), _WebViewElement);
            _app.Tap(_WellBeingRadioBtn(dataid));

            return this;
        }

        public RepositioningRecordPage TapAssistanceButton(string dataid)
        {
            ScrollDownWithinElement(_AssistanceNeededRadioBtn(dataid), _WebViewElement);
            _app.Tap(_AssistanceNeededRadioBtn(dataid));

            return this;
        }


        public RepositioningRecordPage InsertTimeCareGiven()
        {
            ScrollDownWithinElement(_Occuredtimefld, _WebViewElement);
            _app.Tap(_Occuredtimefld);

            return this;
        }

        public RepositioningRecordPage InsertTimeSpent()
        {
            ScrollDownWithinElement(_Add5MinTimeSpentButton, _WebViewElement);
            _app.Tap(_Add5MinTimeSpentButton);

            return this;
        }

        public RepositioningRecordPage SetAdditionalNotesTxtArea(String Text)
        {
            _app.ClearText(_AdditionalNotesTxtFld);
            _app.DismissKeyboard();

            _app.EnterText(_AdditionalNotesTxtFld, Text);
            _app.DismissKeyboard();

            return this;
        }

        public RepositioningRecordPage TapsaveNClose()
        {
            ScrollDownWithinElement(_saveNCloseButton, _WebViewElement);
            _app.Tap(_saveNCloseButton);

            return this;
        }

        public RepositioningRecordPage validateTimeCareGivenValidation1(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeCareGivenValidation1, ExpectText);

            return this;

        }

        public RepositioningRecordPage validateTimeCareGivenValidation2(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeCareGivenValidation2, ExpectText);

            return this;

        }

        public RepositioningRecordPage validateTimeSpentValidation(string ExpectText)
        {
            System.Threading.Thread.Sleep(1000);
            ScrollDownWithinElement(_AdditionalNotesTxtFld, _WebViewElement);
            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeSpentValidation, ExpectText);

            return this;

        }
    }
}
