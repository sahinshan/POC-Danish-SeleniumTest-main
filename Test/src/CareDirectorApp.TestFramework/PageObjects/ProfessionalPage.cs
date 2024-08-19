
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
    public class ProfessionalPage : CommonMethods
    {

        #region Top Area

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _startButton = e => e.Marked("professional_SpeechStart");
        readonly Func<AppQuery, AppQuery> _stopButton = e => e.Marked("professional_SpeechStop");
        readonly Func<AppQuery, AppQuery> _directionButton = e => e.Marked("professional_Directions");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("MainStackLayout").Descendant().Marked("EntityImage");
        Func<AppQuery, AppQuery> _pageTitle(string name) => e => e.Text("PROFESSIONAL: " + name);

        #endregion

        #region Fields
        readonly Func<AppQuery, AppQuery> _ProfessionalIdLabel = e => e.Text("Professional Id");
        readonly Func<AppQuery, AppQuery> _titleLabel = e => e.Text("Title");
        readonly Func<AppQuery, AppQuery> _firstNameLabel = e => e.Text("First Name");
        readonly Func<AppQuery, AppQuery> _lastNameLabel = e => e.Text("Last Name");
        readonly Func<AppQuery, AppQuery> _ProfessionLabel = e => e.Text("Profession");

        readonly Func<AppQuery, AppQuery> _businessPhoneLabel = e => e.All().Text("Business Phone");
        readonly Func<AppQuery, AppQuery> _mobilePhoneLabel = e => e.All().Text("Mobile Phone");


        readonly Func<AppQuery, AppQuery> _ProfessionalIdField = e => e.Marked("Field_e4b7ede9ea82e911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _titleField = e => e.Marked("Field_d15651f8ea82e911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _firstNameField = e => e.Marked("Field_c99dc808eb82e911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _lastNameField = e => e.Marked("Field_07f50616eb82e911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _ProfessionField = e => e.Marked("Field_dab89c3ceb82e911a2c50050569231cf").Class("FormsTextView");

        readonly Func<AppQuery, AppQuery> _businessPhoneField = e => e.Marked("Field_f05b75d6eb82e911a2c50050569231cf").Class("FormsTextView");
        readonly Func<AppQuery, AppQuery> _mobilePhoneField = e => e.Marked("Field_9a348b36ec82e911a2c50050569231cf").Class("FormsTextView");

        #endregion

        #region Phone and SMS buttons

        readonly Func<AppQuery, AppQuery> _businessPhoneSMSButton = e => e.Marked("SMSImage_businessphone");
        readonly Func<AppQuery, AppQuery> _mobilePhoneSMSButton = e => e.Marked("SMSImage_mobilephone");

        readonly Func<AppQuery, AppQuery> _businessPhoneCallButton = e => e.Marked("CallImage_businessphone");
        readonly Func<AppQuery, AppQuery> _mobilePhoneCallButton = e => e.Marked("CallImage_mobilephone");

        #endregion

        #region Footer
        readonly Func<AppQuery, AppQuery> _createdByFooterLabel = e => e.Marked("FooterLabel_createdby");
        readonly Func<AppQuery, AppQuery> _createdOnFooterLabel = e => e.Marked("FooterLabel_createdon");
        readonly Func<AppQuery, AppQuery> _modifiedByFooterLabel = e => e.Marked("FooterLabel_modifiedby");
        readonly Func<AppQuery, AppQuery> _modifiedOnFooterLabel = e => e.Marked("FooterLabel_modifiedon");
        #endregion



        readonly Func<AppQuery, AppQuery> _errorTitle = e => e.Marked("ErrorTitle").Text("Unauthorized Access");
        readonly Func<AppQuery, AppQuery> _errorDesc = e => e.Marked("ErrorDesc").Text("This record is not available in offline mode.");


        public ProfessionalPage(IApp app)
        {
            _app = app;
        }




        public ProfessionalPage WaitForProfessionalPageToLoad(string RecordName)
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

        public ProfessionalPage WaitForUnauthorizedAccessPageToLoad()
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);

            _app.WaitForElement(_errorTitle);
            _app.WaitForElement(_errorDesc);

            return this;
        }

        public ProfessionalPage TapbackButton()
        {
            Tap(_backButton);

            return this;
        }




        public ProfessionalPage ValidateProfessionalIdFieldTitleVisible()
        {
            ScrollToElement(_ProfessionalIdLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_ProfessionalIdLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public ProfessionalPage ValidateTitleFieldTitleVisible()
        {
            ScrollToElement(_titleLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_titleLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public ProfessionalPage ValidateFirstNameFieldTitleVisible()
        {
            ScrollToElement(_firstNameLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_firstNameLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public ProfessionalPage ValidateLastNameFieldTitleVisible()
        {
            ScrollToElement(_lastNameLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_lastNameLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public ProfessionalPage ValidateProfessionFieldTitleVisible()
        {
            ScrollToElement(_ProfessionLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_ProfessionLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public ProfessionalPage ValidateBusinessPhoneFieldTitleVisible()
        {
            ScrollToElement(_businessPhoneLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_businessPhoneLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }
        
        public ProfessionalPage ValidateMobilePhoneTitleVisible()
        {
            ScrollToElement(_mobilePhoneLabel);
            bool fieldTitleVisible = CheckIfElementVisible(_mobilePhoneLabel);
            Assert.IsTrue(fieldTitleVisible);

            return this;
        }




        public ProfessionalPage ValidateProfessionalIdField(string ExpectedFieldText)
        {
            ScrollToElement(_ProfessionalIdField);

            string fieldText = this._app.Query(_ProfessionalIdField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        
        public ProfessionalPage ValidateTitleField(string ExpectedFieldText)
        {
            ScrollToElement(_titleField);

            string fieldText = this._app.Query(_titleField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        
        public ProfessionalPage ValidateFirstNameField(string ExpectedFieldText)
        {
            ScrollToElement(_firstNameField);

            string fieldText = this._app.Query(_firstNameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        
        public ProfessionalPage ValidateLastNameField(string ExpectedFieldText)
        {
            ScrollToElement(_lastNameField);

            string fieldText = this._app.Query(_lastNameField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }
        
        public ProfessionalPage ValidateProfessionField(string ExpectedFieldText)
        {
            ScrollToElement(_ProfessionField);

            string fieldText = this._app.Query(_ProfessionField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }




        public ProfessionalPage ValidateBusinessPhoneField(string ExpectedFieldText)
        {
            ScrollToElement(_businessPhoneField);

            string fieldText = this._app.Query(_businessPhoneField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public ProfessionalPage ValidateMobilePhoneField(string ExpectedFieldText)
        {
            ScrollToElement(_mobilePhoneField);

            string fieldText = this._app.Query(_mobilePhoneField).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public ProfessionalPage ValidateBusinessPhoneSMSButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_businessPhoneSMSButton);

            bool enabled = this._app.Query(_businessPhoneSMSButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }

        public ProfessionalPage ValidateMobilePhoneSMSButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_mobilePhoneSMSButton);

            bool enabled = this._app.Query(_mobilePhoneSMSButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }

        public ProfessionalPage ValidateBusinessPhoneCallButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_businessPhoneCallButton);

            bool enabled = this._app.Query(_businessPhoneCallButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }
        
        public ProfessionalPage ValidateMobilePhoneCallButtonEnabled(bool ExpectEnabled)
        {
            ScrollToElement(_mobilePhoneCallButton);

            bool enabled = this._app.Query(_mobilePhoneCallButton).FirstOrDefault().Enabled;
            Assert.AreEqual(ExpectEnabled, enabled);

            return this;
        }



        public ProfessionalPage ValidateCreatedOnFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_createdOnFooterLabel);

            string fieldText = this._app.Query(_createdOnFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public ProfessionalPage ValidateCreatedByFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_createdByFooterLabel);

            string fieldText = this._app.Query(_createdByFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public ProfessionalPage ValidateModifiedOnFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_modifiedOnFooterLabel);

            string fieldText = this._app.Query(_modifiedOnFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

        public ProfessionalPage ValidateModifiedByFooterField(string ExpectedFieldText)
        {
            ScrollToElement(_modifiedByFooterLabel);

            string fieldText = this._app.Query(_modifiedByFooterLabel).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedFieldText, fieldText);

            return this;
        }

    
    }
}
