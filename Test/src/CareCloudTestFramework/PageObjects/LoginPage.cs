using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace CareCloudTestFramework.PageObjects
{
    public class LoginPage: CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _caredirectorLogo = e => e.Marked("ImgLogo");
        readonly Func<AppQuery, AppQuery> _userImage = e => e.Marked("UserImg");
        readonly Func<AppQuery, AppQuery> _usernameTextbox = e => e.Marked("Username");
        readonly Func<AppQuery, AppQuery> _passwordTextBox = e => e.Marked("Password");
        readonly Func<AppQuery, AppQuery> _environmentLabel = e => e.Marked("EnvironmentLabel");
        readonly Func<AppQuery, AppQuery> _environmentPicker = e => e.Marked("EnvironmentPicker");
        readonly Func<AppQuery, AppQuery> _loginButton = e => e.Marked("BtnLogin");
        readonly Func<AppQuery, AppQuery> _changeUserButton = e => e.Marked("ChangeUser").Text("Log in as a different user");
        readonly Func<AppQuery, AppQuery> _storedUserButton = e => e.Marked("StoredUser").Text("Use last logged account");
        Func<AppQuery, AppQuery> _usernameLabel(string UserName) => e =>
        {
            if (string.IsNullOrEmpty(UserName))
                return e.Marked("UsernameLabel");

            return e.Marked("UsernameLabel").Text(UserName);
        };
        Func<AppQuery, AppQuery> _environmentPickerWitName(string EnvironmentName) => e => e.Marked("EnvironmentPicker").Text(EnvironmentName);

        public LoginPage(IApp app)
        {
            this._app = app;
        }


        public LoginPage WaitForBasicLoginPageToLoad()
        {
            _app.WaitForElement(_mainMenuButton);
            _app.WaitForElement(_applicationTopIcon);

            _app.WaitForElement(_caredirectorLogo);
            _app.WaitForElement(_userImage);

            _app.WaitForElement(_passwordTextBox);
            _app.WaitForElement(_environmentLabel);
            _app.WaitForElement(_environmentPicker);
            _app.WaitForElement(_loginButton);

            return this;
        }

        public LoginPage TapChangeUserButton()
        {
            this.Tap(_changeUserButton);

            return this;
        }

        public bool GetChangeUserButtonVisibility()
        {
            TryScrollToElement(_changeUserButton);
            return this.CheckIfElementVisible(_changeUserButton);
        }

        public LoginPage InsertUserName(string UserName)
        {
            _app.ClearText(_usernameTextbox);
            _app.DismissKeyboard();
            System.Threading.Thread.Sleep(100);

            _app.EnterText(_usernameTextbox, UserName);
            _app.DismissKeyboard();
            System.Threading.Thread.Sleep(100);

            _app.Tap(_passwordTextBox); //we tap on the password textbox to automaticaly get the user environment

            return this;
        }

        public LoginPage WaitForLoginPageToLoad()
        {
            _app.WaitForElement(_mainMenuButton);
            _app.WaitForElement(_applicationTopIcon);

            _app.WaitForElement(_caredirectorLogo);
            _app.WaitForElement(_userImage);
            _app.WaitForElement(_passwordTextBox);
          //  _app.WaitForElement(_environmentLabel);
           // _app.WaitForElement(_environmentPicker);
            _app.WaitForElement(_loginButton);

            bool changeUserButtonVisible = _app.Query(_changeUserButton).Any();
            Assert.IsFalse(changeUserButtonVisible);

            bool usernameLableVisible = _app.Query(_usernameLabel(null)).Any();
            Assert.IsFalse(usernameLableVisible);

            return this;
        }

        public LoginPage WaitForLoginPageToLoadWithPreviousLoginInfoSaved(string UserFullName, string EnvironmentName)
        {
            _app.WaitForElement(_mainMenuButton);
            _app.WaitForElement(_applicationTopIcon);

            _app.WaitForElement(_caredirectorLogo);
            _app.WaitForElement(_userImage);
            _app.WaitForElement(_usernameLabel(UserFullName));

            _app.WaitForElement(_passwordTextBox);
            _app.WaitForElement(_environmentLabel);
            _app.WaitForElement(_environmentPickerWitName(EnvironmentName));
            _app.WaitForElement(_loginButton);

            _app.WaitForElement(_changeUserButton);

            bool usernameTextboxVisible = _app.Query(_usernameTextbox).Any();
            Assert.IsFalse(usernameTextboxVisible);

            return this;
        }



        public LoginPage ValidateUserNameText(string ExpectedUserNameText)
        {
            string username = _app.Query(_usernameTextbox).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedUserNameText, username);

            return this;
        }

        public LoginPage ValidateSelectedEnvironment(string ExpectedEnvironmentName)
        {
            string environmentName = _app.Query(_environmentPicker).FirstOrDefault().Text;
            Assert.AreEqual(ExpectedEnvironmentName, environmentName);

            return this;
        }

        public LoginPage CheckIfStoredUserButtonVisible(bool StoredUserButtonVisible)
        {
            bool buttonVisible = _app.Query(_storedUserButton).Any();
            Assert.AreEqual(StoredUserButtonVisible, buttonVisible);

            return this;
        }

        public LoginPage TapStoredUserButton()
        {
            _app.Tap(_storedUserButton);
            return this;
        }

        public LoginPage TapOnPasswordField()
        {
            Tap(_passwordTextBox);

            return this;
        }

        public LoginPage TapLoginButton()
        {
            WaitForElement(_loginButton);
            Tap(_loginButton);

            return this;
        }


        public LoginPage InsertPassword(string Password)
        {
            _app.DismissKeyboard();
            _app.ClearText(_passwordTextBox);
            _app.DismissKeyboard();
            System.Threading.Thread.Sleep(100);

            _app.EnterText(_passwordTextBox, Password);
            _app.DismissKeyboard();
            System.Threading.Thread.Sleep(100);

            return this;
        }

        

    }
}
