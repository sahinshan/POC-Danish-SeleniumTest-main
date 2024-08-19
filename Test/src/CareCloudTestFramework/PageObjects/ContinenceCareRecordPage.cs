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
    public class ContinenceCareRecordPage : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _saveNCloseButton = e => e.XPath("//button/span[text()='Save and close']");
        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button/span[text()='Cancel']");

        Func<AppQuery, AppWebQuery> _PersonInfoHeader(string personfullname, int number) => e => e.XPath("//span[text()=' " + personfullname + " (ID " + number + ")']");
        Func<AppQuery, AppWebQuery> _ContinenceCareHeader => e => e.XPath("//div[text()='Continence Care']");
        Func<AppQuery, AppWebQuery> _ConsentGivenHeader => e => e.XPath("//div[text()='Consent given']");
        Func<AppQuery, AppWebQuery> _PreferencesTextFld => e => e.XPath("//div[@id='continence-care-preferences']");
        Func<AppQuery, AppWebQuery> _CatherRequiredLabel => e => e.XPath("//div[@data-id='continence-care-catheter-care-required-label']");

        Func<AppQuery, AppWebQuery> _PersonNeedCatheter=> e => e.XPath("//input[@text='Catheter care needed']/parent::label");
        Func<AppQuery, AppWebQuery> _CatheterNotNeeded => e => e.XPath("//input[@text='Catheter care NOT needed']/parent::label");
        Func<AppQuery, AppWebQuery> _CatherPatentandDrainingLabel => e => e.XPath("//div[@data-id='continence-care-catheter-patent-and-draining-label']");
        Func<AppQuery, AppWebQuery> _CatheterDraining => e => e.XPath("//input[@text='Catheter is draining']/parent::label");
        Func<AppQuery, AppWebQuery> _CatheterNotDraining => e => e.XPath("//input[@text='Catheter is NOT draining']/parent::label");
        Func<AppQuery, AppWebQuery> _CatherBagBeenEmptiedLabel => e => e.XPath("//div[@data-id='continence-care-catheter-bag-emptied-label']");
        Func<AppQuery, AppWebQuery> _CatheterHasEmptied => e => e.XPath("//input[@text='Catheter bag has been emptied']/parent::label");
        Func<AppQuery, AppWebQuery> _CatheterHasNotEmptied => e => e.XPath("//input[@text='Catheter bag has NOT been emptied']/parent::label");
        Func<AppQuery, AppWebQuery> _CatherAreaProperlyCleanedLabel => e => e.XPath("//div[@data-id='continence-care-catheter-area-properly-cleaned-label']");
        Func<AppQuery, AppWebQuery> _CatheterAreaProperlyCleaned => e => e.XPath("//input[@text='Catheter area is properly cleaned']/parent::label");
        Func<AppQuery, AppWebQuery> _CatheterAreaNotCleanedProperly => e => e.XPath("//input[@text='Catheter area is NOT properly cleaned']/parent::label");
        Func<AppQuery, AppWebQuery> _CatherPositionedOrSecuredLabel => e => e.XPath("//div[@data-id='continence-care-catheter-positioned-secured-label']");
        Func<AppQuery, AppWebQuery> _CatheterPositioned => e => e.XPath("//input[@text='Catheter is positioned well and there is no pressure']/parent::label");
        Func<AppQuery, AppWebQuery> _CatheterNotPositioned => e => e.XPath("//input[@text='Catheter is NOT positioned well and there is no pressure']/parent::label");

        Func<AppQuery, AppWebQuery> _IsTherAnyMalodourLabel => e => e.XPath("//div[@data-id='continence-care-malodour-label']");
        Func<AppQuery, AppWebQuery> _MalodourPresent => e => e.XPath("//input[@text='There is malodour.']/parent::label");
        Func<AppQuery, AppWebQuery> _MalodourNotPresent => e => e.XPath("//input[@text='There is NOT any malodour.']/parent::label");
        Func<AppQuery, AppWebQuery> _PassedUrineLabel => e => e.XPath("//div[@data-id='continence-care-passed-urine-label']");
        Func<AppQuery, AppWebQuery> _PassedUrine => e => e.XPath("//input[@text='Urine passed']/parent::label");
        Func<AppQuery, AppWebQuery> _UrineNotPassed => e => e.XPath("//input[@text='Urine NOT passed']/parent::label");
        Func<AppQuery, AppWebQuery> _UrineOutPutAmountTxt => e => e.XPath("//input[@id='continence-care-urine-output-amount']");
        Func<AppQuery, AppWebQuery> _UrineColorTxt(string option) => e => e.XPath("//input[@text='" + option + "' and @name='continence-care-urine-colour']/parent::label");

        Func<AppQuery, AppWebQuery> _BowelsOpenedLabel => e => e.XPath("//div[@data-id='continence-care-bowels-opened-label']");
        Func<AppQuery, AppWebQuery> _StoolPassed => e => e.XPath("//input[@text='Stool passed']/parent::label");
        Func<AppQuery, AppWebQuery> _StoolNotPassed => e => e.XPath("//input[@text='Stool NOT passed']/parent::label");
        Func<AppQuery, AppWebQuery> _StoolAmount(string option) => e => e.XPath("//input[@text='" + option + "' and @name='continence-care-stool-amount']/parent::label");
        Func<AppQuery, AppWebQuery> _StoolType(string option) => e => e.XPath("//input[@text='" + option + "' and @name='continence-care-stool-type']/parent::label");
        Func<AppQuery, AppWebQuery> _BloodIsPresent => e => e.XPath("//input[@text='Blood is present' and @name='continence-care-blood-present']/parent::label");
        Func<AppQuery, AppWebQuery> _MucusIsPresent => e => e.XPath("//input[@text='Mucus is present' and @name='continence-care-mucus-present']/parent::label");

        Func<AppQuery, AppWebQuery> _ContinencePadBeenChangedLabel => e => e.XPath("//div[@data-id='continence-care-has-continence-pad-been-changed-label']");
        Func<AppQuery, AppWebQuery> _ContinencePadBeenChanged(string option) => e => e.XPath("//input[@text='" + option + "']/parent::label");
        Func<AppQuery, AppWebQuery> _SkinConditionLabel => e => e.XPath("//div[@data-id='continence-care-skin-concern-is-any-label']");

        Func<AppQuery, AppWebQuery> _ConcernsWithSkinRadioBtn(string option) => e => e.XPath("//input[@text='"+option+ "'and @name='continence-care-skin-concern-is-any']/parent::label");
        Func<AppQuery, AppWebQuery> _WhereOnTheBodyTxtArea => e => e.XPath("//textarea[@id='continence-care-skin-concern-where']");
        Func<AppQuery, AppWebQuery> _SkinConditionBtn => e => e.XPath("//button[@id='add-continence-care-skin-conditions']");
        Func<AppQuery, AppWebQuery> _EditSkinConditionBtn => e => e.XPath("//button[@id='edit-continence-care-describe-skin-condition']");

        Func<AppQuery, AppWebQuery> _SkinConditionOtherTxtArea => e => e.XPath("//textarea[@id='continence-care-skin-condition-other']");
        Func<AppQuery, AppWebQuery> _LocationRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='continence-care-care-physical-locations-list']/parent::label");
        Func<AppQuery, AppWebQuery> _LocationOtherTxtArea => e => e.XPath("//textarea[@id='repositioning-location-if-other']");
        Func<AppQuery, AppWebQuery> _EquipmentRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='continence-care-care-equipment-list']/parent::label");
        Func<AppQuery, AppWebQuery> _WellBeingRadioBtn(string dataid) => e => e.XPath("//label[@data-id='label-" + dataid + "']");
        Func<AppQuery, AppWebQuery> _AssistanceNeededRadioBtn(string dataid) => e => e.XPath("//input[@value='" + dataid + "' and @name='continence-care-care-assistance-needed']/parent::label");
        Func<AppQuery, AppWebQuery> _Occuredtimefld => e => e.XPath("//*[@id='continence-care-occurred-time']");
        Func<AppQuery, AppWebQuery> _Add5MinTimeSpentButton=> e => e.XPath("//*[@id='add-5min-time-spent-btn']");
        Func<AppQuery, AppWebQuery> _AdditionalNotesTxtFld => e => e.XPath("//*[@id='continence-care-additional-notes-text']");
        Func<AppQuery, AppWebQuery> _CareNotesTxtFld => e => e.XPath("//*[@id='careNotes']");
        Func<AppQuery, AppWebQuery> _FlagRecordForHandoverRadioBtn(int value) => e => e.XPath("//input[@value='" + value + "' and @name='flagRecordForHandover']/parent::label");
        Func<AppQuery, AppWebQuery> _HandoverComentsTxtFld => e => e.XPath("//*[@id='careNotes']");
        Func<AppQuery, AppWebQuery> _TimeCareGivenValidation1 => e => e.XPath("//div[@id='continence-care-occurred-error']/span[text()='You cannot select future time.']");
        Func<AppQuery, AppWebQuery> _TimeCareGivenValidation2 => e => e.XPath("//div[@id='continence-care-occurred-2-error']/span[text()='Time care was given and time spent with resident cannot be greater than the current time when combined.']");
        Func<AppQuery, AppWebQuery> _TimeSpentValidation => e => e.XPath("//div[@id='continence-care-time-spent-with-client-2-error']/span[text()='Time care was given and time spent with resident cannot be greater than the current time when combined.']");
        Func<AppQuery, AppWebQuery> _LocationLabel => e => e.XPath("//div[@data-id='continence-care-care-physical-locations-list-label']");
        Func<AppQuery, AppWebQuery> _EquipmentLabel => e => e.XPath("//div[@data-id='continence-care-care-equipment-list-label']");
        Func<AppQuery, AppWebQuery> _WellBeingLabel => e => e.XPath("//div[@data-id='continence-care-care-wellbeing-label']");
        Func<AppQuery, AppWebQuery> _AssistanceNeededLabel => e => e.XPath("//div[@data-id='continence-care-care-assistance-needed-label']");
        Func<AppQuery, AppWebQuery> _StaffRequiredLabel => e => e.XPath("//div[@data-id='continence-care-staff-required-label']");

        Func<AppQuery, AppQuery> _WebViewElement = e => e.Class("android.webkit.WebView");


        public ContinenceCareRecordPage(IApp app)
        {
            _app = app;

        }



        public ContinenceCareRecordPage WaitForContinenceCareRecordPageLookupToLoad(string fullname,int number)
        {
            //WaitForElement(_saveNCloseButton);
            //WaitForElement(_cancelButton);
            WaitForElement(_PersonInfoHeader(fullname, number));
            WaitForElement(_ContinenceCareHeader);
            WaitForElement(_ConsentGivenHeader);

            return this;
        }

        public ContinenceCareRecordPage WaitForContinenceCareRecordPageLookupToReLoad(string fullname, int number)
        {
         
            WaitForElement(_PersonInfoHeader(fullname, number));
          
            return this;
        }

        public ContinenceCareRecordPage validatePreferencesText(string personname)
        {
            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_PreferencesTextFld, "\n\r\r"+" No preferences recorded, please check with '" + personname + "'");

            return this;

        }

        public ContinenceCareRecordPage VerfiyCatheterRequired()
        {

            CheckIfElementVisible(_CatherRequiredLabel);
            CheckIfElementVisible(_PersonNeedCatheter);
            CheckIfElementVisible(_CatheterNotNeeded);

            return this;
        }

        public ContinenceCareRecordPage VerfiyCatheterPatentAndDraining()
        {

            CheckIfElementVisible(_CatherPatentandDrainingLabel);
            CheckIfElementVisible(_CatheterDraining);
            CheckIfElementVisible(_CatheterNotDraining);

            return this;
        }

        public ContinenceCareRecordPage VerfiyCatheterBagEmptied()
        {

            CheckIfElementVisible(_CatherBagBeenEmptiedLabel);
            CheckIfElementVisible(_CatheterHasEmptied);
            CheckIfElementVisible(_CatheterHasNotEmptied);

            return this;
        }

        public ContinenceCareRecordPage VerfiyCatheterPositioned()
        {

            CheckIfElementVisible(_CatherPositionedOrSecuredLabel);
            CheckIfElementVisible(_CatheterPositioned);
            CheckIfElementVisible(_CatheterNotPositioned);

            return this;
        }

        public ContinenceCareRecordPage VerfiyCatheterAreaCleaned()
        {
            ScrollDownWithinElement(_CatherAreaProperlyCleanedLabel,_WebViewElement);
            CheckIfElementVisible(_CatherAreaProperlyCleanedLabel);
            CheckIfElementVisible(_CatheterAreaProperlyCleaned);
            CheckIfElementVisible(_CatheterAreaNotCleanedProperly);

            return this;
        }

        public ContinenceCareRecordPage VerfiyIsThereAnyMalodour()
        {
            ScrollDownWithinElement(_IsTherAnyMalodourLabel, _WebViewElement);
            CheckIfElementVisible(_IsTherAnyMalodourLabel);
            CheckIfElementVisible(_MalodourPresent);
            CheckIfElementVisible(_MalodourNotPresent);

            return this;
        }

        public ContinenceCareRecordPage VerfiyPassedUrine()
        {
            ScrollDownWithinElement(_PassedUrineLabel, _WebViewElement);
            CheckIfElementVisible(_PassedUrineLabel);
            CheckIfElementVisible(_PassedUrine);
            CheckIfElementVisible(_UrineNotPassed);

            return this;
        }

        public ContinenceCareRecordPage VerfiyBowelsOpened()
        { 
            ScrollDownWithinElement(_BowelsOpenedLabel, _WebViewElement);
            CheckIfElementVisible(_BowelsOpenedLabel);
            CheckIfElementVisible(_StoolPassed);
            CheckIfElementVisible(_StoolNotPassed);

            return this;
        }

        public ContinenceCareRecordPage VerfiyContinencePadChanged()
        {
            ScrollDownWithinElement(_ContinencePadBeenChangedLabel, _WebViewElement);
            CheckIfElementVisible(_ContinencePadBeenChangedLabel);
          
            return this;
        }

        public ContinenceCareRecordPage VerfiySkinConditionLabel()
        {
            ScrollDownWithinElement(_SkinConditionLabel, _WebViewElement);
            CheckIfElementVisible(_SkinConditionLabel);

            return this;
        }

        public ContinenceCareRecordPage VerfiySkinConditionFields()
        {
            ScrollDownWithinElement(_WhereOnTheBodyTxtArea, _WebViewElement);
            CheckIfElementVisible(_WhereOnTheBodyTxtArea);
            CheckIfElementVisible(_SkinConditionBtn);

            return this;
        }

      

        public ContinenceCareRecordPage TapCatheterCareNeeded()
        {
            _app.Tap(_PersonNeedCatheter);

            return this;
        }

        public ContinenceCareRecordPage TapIsCatheterDraining()
        {
            _app.Tap(_CatheterDraining);

            return this;
        }

        public ContinenceCareRecordPage TapCatheterBagBeenEmptied()
        {
            _app.Tap(_CatheterHasEmptied);

            return this;
        }

        public ContinenceCareRecordPage TapCatheterBeingPositioned()
        {
            _app.Tap(_CatheterPositioned);

            return this;
        }

        public ContinenceCareRecordPage TapCatheterAreaCleaned()
        {
            _app.Tap(_CatheterAreaProperlyCleaned);

            return this;
        }

        public ContinenceCareRecordPage TapThereIsMalodour()
        {
            ScrollDownWithinElement(_MalodourPresent, _WebViewElement);
            _app.Tap(_MalodourPresent);

            return this;
        }

        public ContinenceCareRecordPage TapUrinePassed()
        {
            ScrollDownWithinElement(_PassedUrine, _WebViewElement);
            _app.Tap(_PassedUrine);

            return this;
        }

        public ContinenceCareRecordPage SetUrineOutputAmount(string Text)
        {
            ScrollDownWithinElement(_UrineOutPutAmountTxt, _WebViewElement);
            _app.ClearText(_UrineOutPutAmountTxt);
            _app.DismissKeyboard();
            _app.EnterText(_UrineOutPutAmountTxt, Text);
            _app.DismissKeyboard();

            return this;
        }

        public ContinenceCareRecordPage TapUrineColor(string option)
        {
            ScrollDownWithinElement(_UrineColorTxt(option), _WebViewElement);
            _app.Tap(_UrineColorTxt(option));

            return this;
        }

        public ContinenceCareRecordPage TapStoolPassed()
        {
            ScrollDownWithinElement(_StoolPassed, _WebViewElement);
            _app.Tap(_StoolPassed);

            return this;
        }

        public ContinenceCareRecordPage TapStoolAmount(string option)
        {
            ScrollDownWithinElement(_StoolAmount(option), _WebViewElement);
            _app.Tap(_StoolAmount(option));

            return this;
        }

        public ContinenceCareRecordPage TapStoolType(string option)
        {
            ScrollDownWithinElement(_StoolType(option), _WebViewElement);
            _app.Tap(_StoolType(option));

            return this;
        }

        public ContinenceCareRecordPage TapBloodIsPresent()
        {
            ScrollDownWithinElement(_BloodIsPresent, _WebViewElement);
            _app.Tap(_BloodIsPresent);

            return this;
        }

        public ContinenceCareRecordPage TapMucusIsPresent()
        {
            ScrollDownWithinElement(_MucusIsPresent, _WebViewElement);
            _app.Tap(_MucusIsPresent);

            return this;
        }

        public ContinenceCareRecordPage TapContinencePadBeenChanged(String option)
        {
            ScrollDownWithinElement(_ContinencePadBeenChanged(option), _WebViewElement);
            _app.Tap(_ContinencePadBeenChanged(option));

            return this;
        }

        public ContinenceCareRecordPage SelectConcernsWithSkinCondition(String option)
        {
            ScrollDownWithinElement(_ConcernsWithSkinRadioBtn(option), _WebViewElement);
            _app.Tap(_ConcernsWithSkinRadioBtn(option));

            return this;
        }

        public ContinenceCareRecordPage SetWhereonTheBodyTxtArea(String Text)
        {
            ScrollDownWithinElement(_WhereOnTheBodyTxtArea, _WebViewElement);
            _app.ClearText(_WhereOnTheBodyTxtArea);
            _app.DismissKeyboard();
            _app.EnterText(_WhereOnTheBodyTxtArea, Text);
            _app.DismissKeyboard(); 

            return this;
        }

        public ContinenceCareRecordPage TapSelectSkinCondition()
        {
            ScrollDownWithinElement(_SkinConditionBtn, _WebViewElement);
            _app.Tap(_SkinConditionBtn);

            return this;
        }


        public ContinenceCareRecordPage TapEditSkinCondition()
        {
            ScrollDownWithinElement(_EditSkinConditionBtn, _WebViewElement);
            _app.Tap(_EditSkinConditionBtn);

            return this;
        }

        public ContinenceCareRecordPage SetSkinConditionOtherTxtArea(String Text)
        {
            _app.ClearText(_SkinConditionOtherTxtArea);
            _app.DismissKeyboard();

            _app.EnterText(_SkinConditionOtherTxtArea, Text);
            _app.DismissKeyboard();

            return this;
        }

        public ContinenceCareRecordPage TapLocationButton(string dataid)
        {
            ScrollDownWithinElement(_LocationRadioBtn(dataid), _WebViewElement);
            _app.Tap(_LocationRadioBtn(dataid));

            return this;
        }

        public ContinenceCareRecordPage SetLocationOtherTxtArea(String Text)
        {
            _app.ClearText(_LocationOtherTxtArea);
            _app.DismissKeyboard();

            _app.EnterText(_LocationOtherTxtArea, Text);
            _app.DismissKeyboard();

            return this;
        }
        public ContinenceCareRecordPage TapEquipmentButton(string dataid)
        {
            ScrollDownWithinElement(_EquipmentRadioBtn(dataid), _WebViewElement);
            _app.Tap(_EquipmentRadioBtn(dataid));

            return this;
        }

        public ContinenceCareRecordPage TapWellbeingButton(string dataid)
        {
            ScrollDownWithinElement(_WellBeingRadioBtn(dataid), _WebViewElement);
            _app.Tap(_WellBeingRadioBtn(dataid));

            return this;
        }

        public ContinenceCareRecordPage TapAssistanceButton(string dataid)
        {
            ScrollDownWithinElement(_AssistanceNeededRadioBtn(dataid), _WebViewElement);
            _app.Tap(_AssistanceNeededRadioBtn(dataid));

            return this;
        }


        public ContinenceCareRecordPage InsertTimeCareGiven()
        {
            ScrollDownWithinElement(_Occuredtimefld, _WebViewElement);
            _app.Tap(_Occuredtimefld);

            return this;
        }

        public ContinenceCareRecordPage InsertTimeSpent()
        {
            ScrollDownWithinElement(_Add5MinTimeSpentButton, _WebViewElement);
            _app.Tap(_Add5MinTimeSpentButton);

            return this;
        }

        public ContinenceCareRecordPage SetAdditionalNotesTxtArea(String Text)
        {
            ScrollDownWithinElement(_AdditionalNotesTxtFld, _WebViewElement);
            _app.ClearText(_AdditionalNotesTxtFld);
            _app.DismissKeyboard();

            _app.EnterText(_AdditionalNotesTxtFld, Text);
            _app.DismissKeyboard();

            return this;
        }

        public ContinenceCareRecordPage TapsaveNClose()
        {
            ScrollDownWithinElement(_saveNCloseButton, _WebViewElement);
            _app.Tap(_saveNCloseButton);

            return this;
        }

        public ContinenceCareRecordPage validateTimeCareGivenValidation1(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeCareGivenValidation1, ExpectText);

            return this;

        }

        public ContinenceCareRecordPage validateTimeCareGivenValidation2(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeCareGivenValidation2, ExpectText);

            return this;

        }

        public ContinenceCareRecordPage validateTimeSpentValidation(string ExpectText)
        {
            System.Threading.Thread.Sleep(1000);
            ScrollDownWithinElement(_AdditionalNotesTxtFld, _WebViewElement);
            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_TimeSpentValidation, ExpectText);

            return this;

        }
    }
}
