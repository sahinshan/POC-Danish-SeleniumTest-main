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
    public class ServiceEndpointsPage:CommonMethods
    {
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewServiceEndpointButton = e => e.Marked("NewEnvironmentToolbarItem");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("SERVICE ENDPOINTS");

        readonly Func<AppQuery, AppQuery> _defaultEndpointNameLabel = e => e.Text("Name: ");
        readonly Func<AppQuery, AppQuery> _defaultEndpointUrlLabel = e => e.Text("URL: ");
        readonly Func<AppQuery, AppQuery> _defaultEndpointDefaultLabel = e => e.Text("Default: ");

        Func<AppQuery, AppQuery> _servieEndpointName(string endpointName) => e => e.Marked(endpointName);
        Func<AppQuery, AppQuery> _servieEndpointURL(string endpointURL) => e => e.Marked(endpointURL);



        readonly IApp _app;

        public ServiceEndpointsPage(IApp app)
        {
            _app = app;
        }

        public ServiceEndpointsPage WaitForServiceEndpointsPageToLoad()
        {
            _app.WaitForElement(_caredirectorIcon);
            _app.WaitForElement(_backButton);
            _app.WaitForElement(_addNewServiceEndpointButton);
            _app.WaitForElement(_pageTitle);

            return this;
        }

        public ServiceEndpointsPage FindDefaultServiceEndpointLabels()
        {
            _app.WaitForElement(_defaultEndpointNameLabel);
            _app.WaitForElement(_defaultEndpointUrlLabel);
            _app.WaitForElement(_defaultEndpointDefaultLabel);

            return this;
        }

        public ServiceEndpointsPage FindServiceEndpointValues(string ServiceEndpointName, string ServiceEndpointURL, string ServiceEndpointDefault)
        {
            _app.WaitForElement(c => c.Text(ServiceEndpointName));

            if (!string.IsNullOrEmpty(ServiceEndpointURL))
                _app.WaitForElement(c => c.Text(ServiceEndpointURL));

            _app.WaitForElement(c => c.Text(ServiceEndpointDefault));

            return this;
        }

        public ServiceEndpointsPage FindServiceEndpointValues(int ServiceEndpointIndex, string ServiceEndpointName, string ServiceEndpointURL, string ServiceEndpointDefault)
        {
            string nameIdentifier = "Name_" + ServiceEndpointIndex.ToString() + "_Value";
            string urlIdentifier = "Url_" + ServiceEndpointIndex.ToString() + "_Value";
            string defaultIdentifier = "Default_" + ServiceEndpointIndex.ToString() + "_Value";

            _app.WaitForElement(nameIdentifier);

            if (!string.IsNullOrEmpty(ServiceEndpointURL))
                _app.WaitForElement(urlIdentifier);

            _app.WaitForElement(defaultIdentifier);

            return this;
        }

        public ServiceEndpointsPage ValidateServiceEndpointNotPresent(string EndpointName, string EndpointURL)
        {
            bool endpointNameFound = _app.Query(_servieEndpointName(EndpointName)).Any();
            bool endpointUrlFound = _app.Query(_servieEndpointURL(EndpointURL)).Any();

            Assert.IsFalse(endpointNameFound);
            Assert.IsFalse(endpointUrlFound);

            return this;
        }

        public LoginPage tapBackButton()
        {
            _app.Tap(_backButton);

            return new LoginPage(_app);
        }

        public ServiceEndpointsPage tapAddNewServiceEndpointButton()
        {
            _app.Tap(_addNewServiceEndpointButton);

            return new ServiceEndpointsPage(_app);
        }

        public ServiceEndpointsPage TapOnServiceEndpoint(string ServiceEndpointName)
        {
            _app.Tap(_servieEndpointName(ServiceEndpointName));

            return new ServiceEndpointsPage(_app);
        }
    }
}
