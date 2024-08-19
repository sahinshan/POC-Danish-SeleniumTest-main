using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Runtime.Remoting.Messaging;

namespace CareCloudTestFramework.PageObjects
{
    public class ProviderSelectionPage : CommonMethods
    {

        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _homepageLogo = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _homepageTitle = e => e.Text("HOME");
        Func<AppQuery, AppWebQuery> _header(string header) => e => e.XPath("//div[@id='provider-details']/div/h1[text()='" + header + "']");
        Func<AppQuery, AppWebQuery> _providerSelectionDropDown => e => e.XPath("//button[@id='provider-location-btn']");
        Func<AppQuery, AppWebQuery> _providerSelectionHeader => e => e.XPath("//h2[text()='Select available care home:']");
        Func<AppQuery, AppWebQuery> _Provider(string provider) => e => e.XPath("//*[@data-id='" + provider + "']/span[@id='select-provider']");
        Func<AppQuery, AppWebQuery> _PersonRecord(string person) => e => e.XPath("//*[@id='cc-basic-info-profile-picture-"+person+"']");
        Func<AppQuery, AppWebQuery> _HandoverButton=> e => e.XPath("//button[@id='handovers-button']");

        Func<AppQuery, AppQuery> _WebViewElement= e => e.Class("android.webkit.WebView");



        public ProviderSelectionPage(IApp app)
        {
            this._app = app;
        }
        public ProviderSelectionPage WaitForProviderSelectionPageToLoad()
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            TryScrollToElement(_homepageTitle);
            CheckIfElementVisible(_homepageTitle);
            //WaitForElement(_header);

            return this;
        }

        public ProviderSelectionPage TapProviderSelectionButton()
        {
            WaitForElement(_mainMenuButton);
            WaitForElement(_applicationTopIcon);
            TryScrollToElement(_homepageTitle);
            _app.Tap(_providerSelectionDropDown);

            return this;
        }

        public ProviderSelectionPage SelectProvider(string provider)
        {
            WaitForElement(_providerSelectionHeader);
            ScrollDownWithinElement(_Provider(provider), _WebViewElement);
            //TryScrollToElement(_Provider(provider));
            _app.Tap(_Provider(provider));

            return this;
        }

        public ProviderSelectionPage SelectPersonRecord(string header, string person)
        {
            WaitForElement(_header(header));
            ScrollDownWithinElement(_PersonRecord(person), _WebViewElement);
            _app.Tap(_PersonRecord(person));

            return this;
        }

        public ProviderSelectionPage TapHandovers(string header)
        {
            System.Threading.Thread.Sleep(1000);
            _app.Tap(_HandoverButton);
            WaitForElement(_header(header));

            return this;
        }
    }
}
