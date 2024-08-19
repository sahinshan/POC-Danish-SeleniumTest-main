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
    public class ErrorPopUp:CommonMethods
    {
        readonly Func<AppQuery, AppQuery> _parentPanel = e => e.Id("parentPanel");

        readonly Func<AppQuery, AppQuery> _topPanel = e => e.Id("topPanel");
        readonly Func<AppQuery, AppQuery> _contentPanel = e => e.Id("contentPanel");
        readonly Func<AppQuery, AppQuery> _buttonPanel = e => e.Id("buttonPanel");

        readonly Func<AppQuery, AppQuery> _okButton = e => e.Id("button2").Text("OK");


        Func<AppQuery, AppQuery> _errorWindowTitle(string Title) => e => e.Id("alertTitle").Text(Title);
        Func<AppQuery, AppQuery> _errorWindowMessage(string Message) => e => e.Id("message").Property("text").Contains(Message);


        public ErrorPopUp(IApp app)
        {
            _app = app;

        }


        public ErrorPopUp WaitForErrorPopUpToLoad()
        {
            _app.WaitForElement(_parentPanel);

            _app.WaitForElement(_topPanel);
            _app.WaitForElement(_contentPanel);
            _app.WaitForElement(_buttonPanel);

            _app.WaitForElement(_okButton);

            return this;
        }


        public ErrorPopUp ValidateErrorMessageTitleAndMessage(string expectedTitle, string expectedText)
        {
            _app.WaitForElement(_errorWindowTitle(expectedTitle));
            _app.WaitForElement(_errorWindowMessage(expectedText));

            return this;
        }

        public ErrorPopUp TapOnOKButton()
        {
            _app.Tap(_okButton);

            return this;
        }

        public ErrorPopUp ValidateErrorPopUpClosed()
        {
            bool elementExists = _app.Query(_parentPanel).Any();
            Assert.IsFalse(elementExists);

            elementExists = _app.Query(_topPanel).Any();
            Assert.IsFalse(elementExists);
            elementExists = _app.Query(_contentPanel).Any();
            Assert.IsFalse(elementExists);
            elementExists = _app.Query(_buttonPanel).Any();
            Assert.IsFalse(elementExists);

            elementExists = _app.Query(_okButton).Any();
            Assert.IsFalse(elementExists);

            return this;
        }

        public ErrorPopUp ClosePopupIfOpen()
        {
            TryWaitForElement(_okButton);

            bool okButtonVisible = CheckIfElementVisible(_okButton); ;

            if (!okButtonVisible)
                return this;

            _app.Tap(_okButton);

            return this;
        }
    }
}
