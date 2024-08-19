using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Security.Policy;

namespace CareCloudTestFramework.PageObjects
{
    public class PersonDailyRecordPage : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _saveNCloseButton = e => e.XPath("//button[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--primary cc-person-daily-record-save-btn']/span[text()='Save and close']");
        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--soft-secondary cc-person-daily-record-cancel-btn']/span[text()='Cancel']");
        readonly Func<AppQuery, AppWebQuery> _deleteButton = e => e.XPath("//*[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--soft-danger cc-person-daily-record-delete-btn']/span[text()='Delete']");

        readonly Func<AppQuery, AppWebQuery> _notesTextArea = e => e.XPath("//*[@id='person-daily-record-notes']");

        readonly Func<AppQuery, AppWebQuery> _selectActivitiesBtn = e => e.XPath("//*[@id='add-careCloudADL']");
         Func<AppQuery, AppWebQuery> _includeInNextHandoverRadioBtn(int  position) => e => e.XPath("//*[@id='include-in-next-handover']/div/label[" + position + "]");
        Func<AppQuery, AppWebQuery> _flagforHandoverRadioBtn(int position) => e => e.XPath("//input[@name='flagRecordForHandover' and @value='"+ position + "']/parent::label");

        Func<AppQuery, AppWebQuery> _ActivityDeleteMessage(string message) => e => e.XPath("//*[@class='ccc-dialog-drawer-content']/h4[text()='" + message + "']");
        Func<AppQuery, AppWebQuery> _LookupDeleteBtn => e => e.XPath("//*[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--lg mcc-button--danger ccc-multi-select-footer-yes-btn']/span[text()='Delete']");


        public PersonDailyRecordPage(IApp app)
        {
            _app = app;

        }


        public PersonDailyRecordPage WaitForPersonDailyRecordPageLookupToLoad()
        {
            WaitForElement(_saveNCloseButton);
            WaitForElement(_cancelButton);

            return this;
        }

        public PersonDailyRecordPage WaitForPersonDailyRecordEditPageLookupToLoad()
        {
            WaitForElement(_saveNCloseButton);
            WaitForElement(_cancelButton);
            WaitForElement(_deleteButton);
            return this;
        }

        public PersonDailyRecordPage TypeInNotesAreaTextBox(string Text)
        {
            _app.ClearText(_notesTextArea);
            _app.DismissKeyboard();

            _app.EnterText(_notesTextArea, Text);
            _app.DismissKeyboard();

            return this;
        }

        public PersonDailyRecordPage TapActivitiesButton()
        {
            _app.Tap(_selectActivitiesBtn);

            return this;
        }

        public PersonDailyRecordPage TapIncludeInNextHandoverButton(int position)
        {
            _app.Tap(_includeInNextHandoverRadioBtn(position));///position=1 for exclude and 2 for include

            return this;
        }

        public PersonDailyRecordPage TapFlagRecordforHandoverButton(int position)
        {
            _app.Tap(_flagforHandoverRadioBtn(position));///position=1 for exclude and 2 for include

            return this;
        }

        public PersonDailyRecordPage TapSaveNCloseButton()
        {
            _app.Tap(_saveNCloseButton);

            return this;
        }

        public PersonDailyRecordPage TapDeleteButton()
        {
            _app.Tap(_deleteButton);

            return this;
        }

        public PersonDailyRecordPage ValidateDeleteMessage(String message, String ExpectText)
        {
            WaitForElement(_ActivityDeleteMessage(message));
            string fieldText = GetWebElementText(_ActivityDeleteMessage(message));
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }

        public PersonDailyRecordPage TapLookUpDeleteButton()
        {
            _app.Tap(_LookupDeleteBtn);

            return this;
        }
    }
}
