
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
    public class SystemUserPage : CommonMethods
    {

        #region Top Area

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("systemuser_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("systemuser_TextToSpeechStopButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("MainStackLayout").Descendant().Marked("EntityImage");
        Func<AppQuery, AppQuery> _pageTitle(string name) => e => e.Text("SYSTEM USER: " + name);

        #endregion

        #region Fields
        
        readonly Func<AppQuery, AppQuery> _NameLabel = e => e.Text("Name");
        readonly Func<AppQuery, AppQuery> _UsernameLabel = e => e.Text("User Name");
        readonly Func<AppQuery, AppQuery> _BusinessUnitLabel = e => e.Text("Business Unit");



        readonly Func<AppQuery, AppQuery> _NameField = e => e.Marked("Field_1fb972c18e41e911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _UsernameField = e => e.Marked("Field_83a01df38e41e911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _BusinessUnitField = e => e.Marked("Field_9ab86bf98e41e911a2c40050569231cf");

        #endregion



        public SystemUserPage(IApp app)
        {
            _app = app;
        }




        public SystemUserPage WaitForSystemUserPageToLoad(string RecordName)
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

        public SystemUserPage TapbackButton()
        {
            Tap(_backButton);

            return this;
        }




     
        
        public SystemUserPage ValidateNameFieldTitleVisible()
        {
            ScrollToElement(_NameLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_NameLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public SystemUserPage ValidateUsernameTitleVisible()
        {
            ScrollToElement(_UsernameLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_UsernameLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }

        public SystemUserPage ValidateBusinessUnitTitleVisible()
        {
            ScrollToElement(_BusinessUnitLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_BusinessUnitLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }





        
        
        public SystemUserPage ValidateNameField(string ExpectedFieldText)
        {
            ScrollToElement(_NameField);

            string fieldText = this._app.Query(_NameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        
        public SystemUserPage ValidateProviderTypeField(string ExpectedFieldText)
        {
            ScrollToElement(_UsernameField);

            string fieldText = this._app.Query(_UsernameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public SystemUserPage ValidateBusinessUnitField(string ExpectedFieldText)
        {
            ScrollToElement(_BusinessUnitField);

            string fieldText = this._app.Query(_BusinessUnitField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

    }
}
