
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
    public class ProviderPage : CommonMethods
    {

        #region Top Area

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("provider_TextToSpeechStartButton");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("provider_TextToSpeechStopButton");
        readonly Func<AppQuery, AppQuery> _directionButton = e => e.Marked("provider_MapNavigation");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("MainStackLayout").Descendant().Marked("EntityImage");
        Func<AppQuery, AppQuery> _pageTitle(string name) => e => e.Text("PROVIDER: " + name);

        #endregion

        #region Fields
        readonly Func<AppQuery, AppQuery> _IdLabel = e => e.Text("Id");
        readonly Func<AppQuery, AppQuery> _NameLabel = e => e.Text("Name");
        readonly Func<AppQuery, AppQuery> _ProviderTypeLabel = e => e.Text("Provider Type");


        readonly Func<AppQuery, AppQuery> _IdField = e => e.Marked("Field_6ddd67d5db85e911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _NameField = e => e.Marked("Field_0103186a20ace6119bd61866da1e4209").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _ProviderTypeField = e => e.Marked("Field_642af8e1db85e911a2c50050569231cf").Class("FormsTextView");

        #endregion

        readonly Func<AppQuery, AppQuery> _errorTitle = e => e.Marked("ErrorTitle").Text("Unauthorized Access");
        readonly Func<AppQuery, AppQuery> _errorDesc = e => e.Marked("ErrorDesc").Text("This record is not available in offline mode.");

        public ProviderPage(IApp app)
        {
            _app = app;
        }




        public ProviderPage WaitForProviderPageToLoad(string RecordName)
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);
            _app.WaitForElement(_syncIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_directionButton);
            _app.WaitForElement(_startButton);
            _app.WaitForElement(_stopButton);

            _app.WaitForElement(_pageTitle(RecordName));

            return this;
        }

        public ProviderPage WaitForUnauthorizedAccessPageToLoad()
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);

            _app.WaitForElement(_errorTitle);
            _app.WaitForElement(_errorDesc);

            return this;
        }

        public ProviderPage TapbackButton()
        {
            Tap(_backButton);

            return this;
        }




        public ProviderPage ValidateIdFieldTitleVisible()
        {
            ScrollToElement(_IdField);
            bool fieldTitleVisible = CheckIfElementVisible(_IdLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public ProviderPage ValidateNameFieldTitleVisible()
        {
            ScrollToElement(_NameField);
            bool fieldTitleVisible = CheckIfElementVisible(_NameLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public ProviderPage ValidateProviderTypeTitleVisible()
        {
            ScrollToElement(_ProviderTypeField);
            bool fieldTitleVisible = CheckIfElementVisible(_ProviderTypeLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        




        public ProviderPage ValidateIdField(string ExpectedFieldText)
        {
            ScrollToElement(_IdField);

            string fieldText = this._app.Query(_IdField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        
        public ProviderPage ValidateNameField(string ExpectedFieldText)
        {
            ScrollToElement(_NameField);

            string fieldText = this._app.Query(_NameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        
        public ProviderPage ValidateProviderTypeField(string ExpectedFieldText)
        {
            ScrollToElement(_ProviderTypeField);

            string fieldText = this._app.Query(_ProviderTypeField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        

    
    }
}
