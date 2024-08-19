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
    public class ServiceEndPointEditPage:CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenuIcon = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _saveButton = e => e.Marked("SaveEnvironmentToolbarItem");
        readonly Func<AppQuery, AppQuery> _saveAndCloseButton = e => e.Marked("SaveAndBackEnvironmentToolbarItem");
        readonly Func<AppQuery, AppQuery> _deletekButton = e => e.Marked("DeleteEnvironmentToolbarItem");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("SERVICE ENDPOINT");

        readonly Func<AppQuery, AppQuery> _nameLabel = e => e.Marked("NameLabel");
        readonly Func<AppQuery, AppQuery> _urlLabel = e => e.Marked("UrlLabel");
        readonly Func<AppQuery, AppQuery> _defaultLabel = e => e.Marked("DefaultLabel");

        readonly Func<AppQuery, AppQuery> _name = e => e.Property("contentDescription").Like("Name");
        readonly Func<AppQuery, AppQuery> _url = e => e.Property("contentDescription").Like("Url");
        readonly Func<AppQuery, AppQuery> _defaultURLUnEditable = e => e.Marked("LoginUrlLabel"); //this element is only visible when we open the default endpoint after the url is saved (no longer editable)
        readonly Func<AppQuery, AppQuery> _defaultDisabled = e => e.Property("contentDescription").Like("MarkAsDefaultLabel");
        readonly Func<AppQuery, AppQuery> _defaultEnabled = e => e.Property("contentDescription").Like("MarkAsDefault");


        public ServiceEndPointEditPage(IApp app)
        {
            this._app = app;
        }

        public ServiceEndPointEditPage WaitForServiceEndPointEditPageToLoad()
        {
            _app.WaitForElement(_mainMenuIcon);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_saveButton);
            _app.WaitForElement(_saveAndCloseButton);
            _app.WaitForElement(_pageTitle);

            _app.WaitForElement(_nameLabel);
            _app.WaitForElement(_urlLabel);

            return this;
        }

        public ServiceEndPointEditPage FindServiceEndpointValues(string ServiceEndpointName, string ServiceEndpointURL, string ServiceEndpointDefault)
        {
            string endpointName = _app.Query(_name).FirstOrDefault().Text;
            string endpointUrl = _app.Query(_url).FirstOrDefault().Text;

            AppResult endpointDisabled = _app.Query(_defaultDisabled).FirstOrDefault();
            AppResult endpointEnabled = _app.Query(_defaultEnabled).FirstOrDefault();

            Assert.AreEqual(ServiceEndpointName, endpointName);
            Assert.AreEqual(ServiceEndpointURL, endpointUrl);

            if (endpointDisabled == null && endpointEnabled == null)
                Assert.Fail("Not able to find the default endpoint information");

            if (endpointDisabled != null)
                Assert.AreEqual(ServiceEndpointDefault, endpointDisabled.Text);
            else
                Assert.AreEqual(ServiceEndpointDefault, endpointEnabled.Text);

            return this;
        }

        public ServiceEndPointEditPage InsertEndpointName(string EndpointName)
        {
            _app.ClearText(_name);
            _app.DismissKeyboard();

            if (!string.IsNullOrEmpty(EndpointName))
            {
                _app.EnterText(_name, EndpointName);
                _app.DismissKeyboard();
            }

            return this;
        }

        public ServiceEndPointEditPage InsertEndpointURL(string EndpointURL)
        {
            _app.ClearText(_url);
            _app.DismissKeyboard();

            if (!string.IsNullOrEmpty(EndpointURL))
            {
                _app.EnterText(_url, EndpointURL);
                _app.DismissKeyboard();
            }

            return this;
        }

        /// <summary>
        /// if the Endpoint URL field is empty set its value
        /// </summary>
        /// <param name="EndpointURL"></param>
        /// <returns></returns>
        public ServiceEndPointEditPage InsertEndpointURLIfEmpty(string EndpointURL)
        {
            if (this.CheckIfElementVisible(_defaultURLUnEditable))
                return this;

            _app.ClearText(_url);
            _app.DismissKeyboard();

            if (!string.IsNullOrEmpty(EndpointURL))
            {
                _app.EnterText(_url, EndpointURL);
                _app.DismissKeyboard();
            }

            return this;
        }

        public ServiceEndPointEditPage SelectDefaultField(bool IsDefault)
        {
            _app.Tap(_defaultEnabled);

            GenericPicklistPopUp popup = new GenericPicklistPopUp(this._app);

            popup.WaitForPicklistToLoad();

            if (IsDefault)
                popup.ScrollDownPickList(1);
            else
                popup.ScrollUpPickList(1);

            popup.TapOkButton();

            return this;
        }

        public ServiceEndPointEditPage TapServiceEndpointDeleteButton()
        {
            _app.Tap(_deletekButton);

            return this;
        }

        public ServiceEndpointsPage TapServiceEndpointSaveAndCloseButton()
        {
            _app.Tap(_saveAndCloseButton);

            return new ServiceEndpointsPage(this._app);
        }

        public ServiceEndPointEditPage TapServiceEndpointSaveButton()
        {
            _app.Tap(_saveButton);

            return this;
        }

        public ServiceEndpointsPage TapBackButtonButton()
        {
            _app.Tap(_backButton);

            return new ServiceEndpointsPage(this._app);
        }

        public ServiceEndPointEditPage CheckDefaultFieldDisabled()
        {
            AppResult defaultButton = _app.Query(_defaultDisabled).FirstOrDefault();
            Assert.IsNotNull(defaultButton);

            return this;
        }


        public ServiceEndPointEditPage CheckDeleteButtonVisibility(bool Visible)
        {
            bool buttonVisible = _app.Query(_deletekButton).Any();
            Assert.AreEqual(Visible, buttonVisible);

            return this;
        }
    }
}
