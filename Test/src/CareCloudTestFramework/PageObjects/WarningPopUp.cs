using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest.Configuration;
using Xamarin.UITest;
using Xamarin.UITest.Queries;


namespace CareCloudTestFramework.PageObjects
{
    public class WarningPopUp:CommonMethods
    {
        readonly Func<AppQuery, AppQuery> _parentPanel = e => e.Id("parentPanel");

        readonly Func<AppQuery, AppQuery> _topPanel = e => e.Id("topPanel");
        readonly Func<AppQuery, AppQuery> _contentPanel = e => e.Id("contentPanel");
        readonly Func<AppQuery, AppQuery> _buttonPanel = e => e.Id("buttonPanel");

        readonly Func<AppQuery, AppQuery> _yesButton = e => e.Id("button1");
        readonly Func<AppQuery, AppQuery> _noButton = e => e.Id("button2");


        readonly Func<AppQuery, AppQuery> _errorWindowTitle = e => e.Id("alertTitle");
        readonly Func<AppQuery, AppQuery> _errorWindowMessage = e => e.Id("message");

        public WarningPopUp(IApp app)
        {
            _app = app;

        }


        public WarningPopUp WaitForWarningPopUpToLoad()
        {
            _app.WaitForElement(_parentPanel);

            _app.WaitForElement(_topPanel);
            _app.WaitForElement(_contentPanel);
            _app.WaitForElement(_buttonPanel);

            _app.WaitForElement(_yesButton);
            _app.WaitForElement(_noButton);

            return this;
        }


        public WarningPopUp ValidateErrorMessageTitleAndMessage(string expectedTitle, string expectedText)
        {
            ValidateElementText(_errorWindowTitle, expectedTitle);
            ValidateElementText(_errorWindowMessage, expectedText);

            return this;
        }

        public WarningPopUp TapOnYesButton()
        {
            _app.Tap(_yesButton);

            return this;
        }

        public WarningPopUp TapOnNoButton()
        {
            _app.Tap(_noButton);

            return this;
        }

        public WarningPopUp ValidateErrorPopupClosed()
        {
            bool elementExists = _app.Query(_parentPanel).Any();
            Assert.IsFalse(elementExists);

            elementExists = _app.Query(_topPanel).Any();
            Assert.IsFalse(elementExists);
            elementExists = _app.Query(_contentPanel).Any();
            Assert.IsFalse(elementExists);
            elementExists = _app.Query(_buttonPanel).Any();
            Assert.IsFalse(elementExists);

            elementExists = _app.Query(_yesButton).Any();
            Assert.IsFalse(elementExists);

            return this;
        }

        public WarningPopUp CloseWarningPopUpIfOpen()
        {
            TryWaitForElement(_yesButton);

            bool yesButtonVisible = _app.Query(_yesButton).Any();

            if (yesButtonVisible)
                _app.Tap(_yesButton);

            return this;
        }

        public WarningPopUp TapNoButtonIfPopupIsOpen()
        {
            TryWaitForElement(_noButton);

            bool yesButtonVisible = _app.Query(_noButton).Any();

            if (yesButtonVisible)
                _app.Tap(_noButton);

            return this;
        }
    }
}
