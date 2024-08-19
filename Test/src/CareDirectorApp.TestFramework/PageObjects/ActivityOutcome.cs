
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
    public class ActivityOutcome : CommonMethods
    {

        #region Top Area

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("activityoutcome_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("activityoutcome_TextToSpeechStopButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("MainStackLayout").Descendant().Marked("EntityImage");
        Func<AppQuery, AppQuery> _pageTitle(string name) => e => e.Text("ACTIVITY OUTCOME: " + name);

        #endregion

        #region Fields
        
        readonly Func<AppQuery, AppQuery> _NameLabel = e => e.Text("Name");
        readonly Func<AppQuery, AppQuery> _CodeLabel = e => e.Text("Code");
        readonly Func<AppQuery, AppQuery> _GovCodeLabel = e => e.Text("Gov Code");



        readonly Func<AppQuery, AppQuery> _NameField = e => e.Marked("Field_45b718dfb940e911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _CodeField = e => e.Marked("Field_47b718dfb940e911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _GovCodeField = e => e.Marked("Field_49b718dfb940e911a2c40050569231cf");

        #endregion



        public ActivityOutcome(IApp app)
        {
            _app = app;
        }




        public ActivityOutcome WaitForActivityOutcomeToLoad(string RecordName)
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);
            //_app.WaitForElement(_syncIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_startButton);
            _app.WaitForElement(_stopButton);

            _app.WaitForElement(_pageTitle(RecordName));

            return this;
        }

        public ActivityOutcome TapbackButton()
        {
            Tap(_backButton);

            return this;
        }




     
        
        public ActivityOutcome ValidateNameFieldTitleVisible()
        {
            ScrollToElement(_NameLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_NameLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public ActivityOutcome ValidateCodeTitleVisible()
        {
            ScrollToElement(_CodeLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_CodeLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }

        public ActivityOutcome ValidateGovCodeTitleVisible()
        {
            ScrollToElement(_GovCodeLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_GovCodeLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }





        
        
        public ActivityOutcome ValidateNameField(string ExpectedFieldText)
        {
            ScrollToElement(_NameField);

            string fieldText = this._app.Query(_NameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        
        public ActivityOutcome ValidateCodeField(string ExpectedFieldText)
        {
            ScrollToElement(_CodeField);

            string fieldText = this._app.Query(_CodeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public ActivityOutcome ValidateGovCodeField(string ExpectedFieldText)
        {
            ScrollToElement(_GovCodeField);

            string fieldText = this._app.Query(_GovCodeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

    }
}
