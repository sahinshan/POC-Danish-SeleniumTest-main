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
    public class KeyWorkerNotesRecordPage : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _saveNCloseButton = e => e.XPath("//button/span[text()='Save and close']");
        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button/span[text()='Cancel']");

        Func<AppQuery, AppWebQuery> _PersonInfoHeader(string personfullname, int number) => e => e.XPath("//span[text()=' " + personfullname + " (ID " + number + ")']");
        Func<AppQuery, AppWebQuery> _KeyWorkerNotesRecordPageHeader => e => e.XPath("//div[text()='Keyworker Note']");
        Func<AppQuery, AppWebQuery> _KeyWorkerNotesText => e => e.XPath("//*[@id='key-worker-notes-keyworker-note']");
        Func<AppQuery, AppWebQuery> _KeyWorkerNotesOccuredtimefld => e => e.XPath("//*[@id='key-worker-notes-occurred-time']");
        Func<AppQuery, AppWebQuery> _Add5MinTimeSpentButton => e => e.XPath("//*[@id='add-5min-time-spent-btn']");

        Func<AppQuery, AppQuery> _WebViewElement = e => e.Class("android.webkit.WebView");



        public KeyWorkerNotesRecordPage(IApp app)
        {
            _app = app;

        }



        public KeyWorkerNotesRecordPage WaitForKeyWorkerNotesRecordPageToLoad(string fullname, int number)
        {
           
            WaitForElement(_PersonInfoHeader(fullname, number));
            WaitForElement(_KeyWorkerNotesRecordPageHeader);

            return this;
        }

        public KeyWorkerNotesRecordPage WaitForKeyWorkerNotesRecordPageToReLoad(string fullname, int number)
        {
            
            WaitForElement(_PersonInfoHeader(fullname, number));
            

            return this;
        }

       

        public KeyWorkerNotesRecordPage InsertSpecifyOtherConversationTextBox(string KeyWorkerNotesTextFld)
        {
            _app.ClearText(_KeyWorkerNotesText);
            _app.DismissKeyboard();

            _app.EnterText(_KeyWorkerNotesText, KeyWorkerNotesTextFld);
            _app.DismissKeyboard();

            return this;
        }

       
      
       
        public KeyWorkerNotesRecordPage InsertTimeCareGiven()
        {
            ScrollDownWithinElement(_KeyWorkerNotesOccuredtimefld, _WebViewElement);
            _app.Tap(_KeyWorkerNotesOccuredtimefld);

            return this;
        }

        public KeyWorkerNotesRecordPage InsertTimeSpent()
        {
            ScrollDownWithinElement(_Add5MinTimeSpentButton, _WebViewElement);
            _app.Tap(_Add5MinTimeSpentButton);

            return this;
        }

        public KeyWorkerNotesRecordPage TapsaveNClose()
        {
            ScrollDownWithinElement(_saveNCloseButton, _WebViewElement);
            _app.Tap(_saveNCloseButton);

            return this;
        }

       

    }
}
