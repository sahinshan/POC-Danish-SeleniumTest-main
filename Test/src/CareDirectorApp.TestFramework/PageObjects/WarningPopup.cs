using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;
using System.Collections.Generic;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class WarningPopup: CommonMethods
    {
        readonly Func<AppQuery, AppQuery> _parentPanel = e => e.Id("parentPanel");

        readonly Func<AppQuery, AppQuery> _topPanel = e => e.Id("topPanel");
        readonly Func<AppQuery, AppQuery> _contentPanel = e => e.Id("contentPanel");
        readonly Func<AppQuery, AppQuery> _buttonPanel = e => e.Id("buttonPanel");

        readonly Func<AppQuery, AppQuery> _yesButton = e => e.Id("button1");
        readonly Func<AppQuery, AppQuery> _noButton = e => e.Id("button2");


        readonly Func<AppQuery, AppQuery> _errorWindowTitle = e => e.Id("alertTitle");
        readonly Func<AppQuery, AppQuery> _errorWindowMessage = e => e.Id("message");


        public WarningPopup(IApp app)
        {
            _app = app;

        }

        
        public WarningPopup WaitForWarningPopupToLoad()
        {
            _app.WaitForElement(_parentPanel);

            _app.WaitForElement(_topPanel);
            _app.WaitForElement(_contentPanel);
            _app.WaitForElement(_buttonPanel);

            _app.WaitForElement(_yesButton);
            _app.WaitForElement(_noButton);

            return this;
        }
        

        public WarningPopup ValidateErrorMessageTitleAndMessage(string expectedTitle, string expectedText)
        {
            ValidateElementText(_errorWindowTitle, expectedTitle);
            ValidateElementText(_errorWindowMessage, expectedText);

            return this;
        }

        public WarningPopup TapOnYesButton()
        {
            _app.Tap(_yesButton);

            return this;
        }

        public WarningPopup TapOnNoButton()
        {
            _app.Tap(_noButton);

            return this;
        }

        public WarningPopup ValidateErrorPopupClosed()
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

        public WarningPopup CloseWarningPopupIfOpen()
        {
            TryWaitForElement(_yesButton);

            bool yesButtonVisible = _app.Query(_yesButton).Any();

            if (yesButtonVisible)
                _app.Tap(_yesButton);

            return this;
        }

        public WarningPopup TapNoButtonIfPopupIsOpen()
        {
            TryWaitForElement(_noButton);

            bool yesButtonVisible = _app.Query(_noButton).Any();

            if (yesButtonVisible)
                _app.Tap(_noButton);

            return this;
        }
    }
}
