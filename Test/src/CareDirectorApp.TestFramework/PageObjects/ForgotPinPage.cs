using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class ForgotPinPage
    {
        IApp _app;

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackBtn");
        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _pageLogo = e => e.Marked("ImgLogo");
        readonly Func<AppQuery, AppQuery> _forgotPinLabel = e => e.Marked("ForgotPin").Text("Forgot PIN? Enter Password");
        readonly Func<AppQuery, AppQuery> _passwordTextBox = e => e.Marked("Password");

        readonly Func<AppQuery, AppQuery> _checkPasswordButton = e => e.Marked("BtnPassword").Text("Check Password");
        readonly Func<AppQuery, AppQuery> _enterPinButton = e => e.Marked("EnterPin").Text("Enter Pin");


        public ForgotPinPage(IApp app)
        {
            this._app = app;
        }

        public ForgotPinPage WaitForForgotPinPageToLoad()
        {
            _app.WaitForElement(_backButton);
            _app.WaitForElement(_mainMenuButton);
            _app.WaitForElement(_applicationTopIcon);

            _app.WaitForElement(_pageLogo);
            _app.WaitForElement(_forgotPinLabel);
            _app.WaitForElement(_passwordTextBox);

            _app.WaitForElement(_checkPasswordButton);
            _app.WaitForElement(_enterPinButton);

            return this;
        }

        public ForgotPinPage InsertPassword(string Password)
        {
            _app.ClearText(_passwordTextBox);
            _app.EnterText(_passwordTextBox, Password);
            _app.DismissKeyboard();

            return this;
        }

        public PinPage TapCheckPasswordButton()
        {
            _app.Tap(_checkPasswordButton);

            return new PinPage(this._app);
        }

        public PinPage TapEnterPinButton()
        {
            _app.Tap(_enterPinButton);

            return new PinPage(this._app);
        }

    }
}
