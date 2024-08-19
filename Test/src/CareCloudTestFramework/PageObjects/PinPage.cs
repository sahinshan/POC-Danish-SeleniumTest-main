using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;

namespace CareCloudTestFramework.PageObjects
{
    public class PinPage
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 2, 00);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _applicationTopIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackBtn");

        readonly Func<AppQuery, AppQuery> _caredirectorLogo = e => e.Marked("ImgLogo");
        readonly Func<AppQuery, AppQuery> _insertNewPinInfoLabel = e => e.Marked("InfoLabel").Text("Insert new PIN");
        readonly Func<AppQuery, AppQuery> _confirmNewPinInfoLabel = e => e.Marked("InfoLabel").Text("Confirm new PIN");

        Func<AppQuery, AppQuery> _pinLabel(string Text) => e =>
        {
            if (string.IsNullOrEmpty(Text))
                return e.Marked("PinLabel");

            return e.Marked("PinLabel").Text(Text);
        };

        readonly Func<AppQuery, AppQuery> _btn1 = e => e.Marked("btn1").Text("1");
        readonly Func<AppQuery, AppQuery> _btn2 = e => e.Marked("btn2").Text("2");
        readonly Func<AppQuery, AppQuery> _btn3 = e => e.Marked("btn3").Text("3");
        readonly Func<AppQuery, AppQuery> _btn4 = e => e.Marked("btn4").Text("4");
        readonly Func<AppQuery, AppQuery> _btn5 = e => e.Marked("btn5").Text("5");
        readonly Func<AppQuery, AppQuery> _btn6 = e => e.Marked("btn6").Text("6");
        readonly Func<AppQuery, AppQuery> _btn7 = e => e.Marked("btn7").Text("7");
        readonly Func<AppQuery, AppQuery> _btn8 = e => e.Marked("btn8").Text("8");
        readonly Func<AppQuery, AppQuery> _btn9 = e => e.Marked("btn9").Text("9");
        readonly Func<AppQuery, AppQuery> _btnDel = e => e.Marked("btnDel").Text("Del.");
        readonly Func<AppQuery, AppQuery> _btn0 = e => e.Marked("btn0").Text("0");
        readonly Func<AppQuery, AppQuery> _btnOk = e => e.Marked("btnOk").Text("OK");

        readonly Func<AppQuery, AppQuery> _explanation = e => e.Marked("Explanation").Text("This code will be used to unlock the app when it has been idle for some time");

        readonly Func<AppQuery, AppQuery> _insertOldPinInfoLabel = e => e.Marked("InfoLabel").Text("Insert old PIN");
        readonly Func<AppQuery, AppQuery> _forgotPinButton = e => e.Marked("ForgotPin").Text("Forgot PIN? Enter Password");


        readonly IApp _app;

        public PinPage(IApp app)
        {
            _app = app;

        }

        public PinPage WaitForPinPageToLoad()
        {
            _app.WaitForElement(_mainMenuButton, "Unable to find the element: MenuBtn", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_applicationTopIcon, "Unable to find the element: AppIcon", _timeoutTimeSpan, _retryFrequency);

            _app.WaitForElement(_caredirectorLogo, "Unable to find the element: ImgLogo", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_insertNewPinInfoLabel, "Unable to find the element: InfoLabel", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_pinLabel(null), "Unable to find the element: PinLabel", _timeoutTimeSpan, _retryFrequency);

            _app.WaitForElement(_btn1, "Unable to find the element: btn1", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn2, "Unable to find the element: btn2", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn3, "Unable to find the element: btn3", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn4, "Unable to find the element: btn4", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn5, "Unable to find the element: btn5", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn6, "Unable to find the element: btn6", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn7, "Unable to find the element: btn7", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn8, "Unable to find the element: btn8", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn9, "Unable to find the element: btn9", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btnDel, "Unable to find the element: btnDel", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn0, "Unable to find the element: btn0", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btnOk, "Unable to find the element: btnOk", _timeoutTimeSpan, _retryFrequency);

            _app.WaitForElement(_explanation, "Unable to find the element: Explanation", _timeoutTimeSpan, _retryFrequency);


            bool backButtonPresent = _app.Query(_backButton).Any();


            return this;
        }

        public PinPage WaitForPinPageToLoad(bool ExpectBackButtonVisible, bool ExpectExplanationMessageVisible)
        {
            _app.WaitForElement(_mainMenuButton, "Unable to find the element: MenuBtn", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_applicationTopIcon, "Unable to find the element: AppIcon", _timeoutTimeSpan, _retryFrequency);

            _app.WaitForElement(_caredirectorLogo, "Unable to find the element: ImgLogo", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_insertNewPinInfoLabel, "Unable to find the element: InfoLabel", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_pinLabel(null), "Unable to find the element: PinLabel", _timeoutTimeSpan, _retryFrequency);

            _app.WaitForElement(_btn1, "Unable to find the element: btn1", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn2, "Unable to find the element: btn2", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn3, "Unable to find the element: btn3", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn4, "Unable to find the element: btn4", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn5, "Unable to find the element: btn5", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn6, "Unable to find the element: btn6", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn7, "Unable to find the element: btn7", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn8, "Unable to find the element: btn8", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn9, "Unable to find the element: btn9", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btnDel, "Unable to find the element: btnDel", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btn0, "Unable to find the element: btn0", _timeoutTimeSpan, _retryFrequency);
            _app.WaitForElement(_btnOk, "Unable to find the element: btnOk", _timeoutTimeSpan, _retryFrequency);

            if (ExpectExplanationMessageVisible)
                _app.WaitForElement(_explanation, "Unable to find the element: Explanation", _timeoutTimeSpan, _retryFrequency);

            if (ExpectBackButtonVisible)
                _app.WaitForElement(_backButton, "Unable to find the element: BackBtn", _timeoutTimeSpan, _retryFrequency);


            return this;
        }

        public PinPage WaitForConfirmationPinPageToLoad()
        {
            _app.WaitForElement(_mainMenuButton);
            _app.WaitForElement(_applicationTopIcon);

            _app.WaitForElement(_backButton);

            _app.WaitForElement(_caredirectorLogo);
            _app.WaitForElement(_confirmNewPinInfoLabel);
            _app.WaitForElement(_pinLabel(null));

            _app.WaitForElement(_btn1);
            _app.WaitForElement(_btn2);
            _app.WaitForElement(_btn3);
            _app.WaitForElement(_btn4);
            _app.WaitForElement(_btn5);
            _app.WaitForElement(_btn6);
            _app.WaitForElement(_btn7);
            _app.WaitForElement(_btn8);
            _app.WaitForElement(_btn9);
            _app.WaitForElement(_btnDel);
            _app.WaitForElement(_btn0);
            _app.WaitForElement(_btnOk);

            _app.WaitForElement(_explanation);

            return this;
        }

        public PinPage WaitForConfirmationPinPageToLoad(bool ExpectExplanationMessageVisible)
        {
            _app.WaitForElement(_mainMenuButton);
            _app.WaitForElement(_applicationTopIcon);

            _app.WaitForElement(_backButton);

            _app.WaitForElement(_caredirectorLogo);
            _app.WaitForElement(_confirmNewPinInfoLabel);
            _app.WaitForElement(_pinLabel(null));

            _app.WaitForElement(_btn1);
            _app.WaitForElement(_btn2);
            _app.WaitForElement(_btn3);
            _app.WaitForElement(_btn4);
            _app.WaitForElement(_btn5);
            _app.WaitForElement(_btn6);
            _app.WaitForElement(_btn7);
            _app.WaitForElement(_btn8);
            _app.WaitForElement(_btn9);
            _app.WaitForElement(_btnDel);
            _app.WaitForElement(_btn0);
            _app.WaitForElement(_btnOk);

            if (ExpectExplanationMessageVisible)
                _app.WaitForElement(_explanation);

            return this;
        }

        /// <summary>
        /// This is the page a user is redirected after he taps on the change PIN button
        /// </summary>
        /// <returns></returns>
        public PinPage WaitForChangePinPageToLoad()
        {
            _app.WaitForElement(_backButton);
            _app.WaitForElement(_mainMenuButton);
            _app.WaitForElement(_applicationTopIcon);

            _app.WaitForElement(_caredirectorLogo);
            _app.WaitForElement(_insertOldPinInfoLabel);
            _app.WaitForElement(_pinLabel(null));

            _app.WaitForElement(_btn1);
            _app.WaitForElement(_btn2);
            _app.WaitForElement(_btn3);
            _app.WaitForElement(_btn4);
            _app.WaitForElement(_btn5);
            _app.WaitForElement(_btn6);
            _app.WaitForElement(_btn7);
            _app.WaitForElement(_btn8);
            _app.WaitForElement(_btn9);
            _app.WaitForElement(_btnDel);
            _app.WaitForElement(_btn0);
            _app.WaitForElement(_btnOk);

            _app.WaitForElement(_forgotPinButton);

            return this;
        }


        public PinPage TapButton1()
        {
            _app.Tap(_btn1);
            return this;
        }
        public PinPage TapButton2()
        {
            _app.Tap(_btn2);
            return this;
        }
        public PinPage TapButton3()
        {
            _app.Tap(_btn3);
            return this;
        }
        public PinPage TapButton4()
        {
            _app.Tap(_btn4);
            return this;
        }
        public PinPage TapButton5()
        {
            _app.Tap(_btn5);
            return this;
        }
        public PinPage TapButton6()
        {
            _app.Tap(_btn6);
            return this;
        }
        public PinPage TapButton7()
        {
            _app.Tap(_btn7);
            return this;
        }
        public PinPage TapButton8()
        {
            _app.Tap(_btn8);
            return this;
        }
        public PinPage TapButton9()
        {
            _app.Tap(_btn9);
            return this;
        }
        public PinPage TapButtonDel()
        {
            _app.Tap(_btnDel);
            return this;
        }
        public PinPage TapButton0()
        {
            _app.Tap(_btn0);
            return this;
        }
        public PinPage TapButtonOK()
        {
            _app.Tap(_btnOk);
            return this;
        }


        public PinPage TapButtonBackButton()
        {
            _app.Tap(_backButton);
            return this;
        }

        public ForgotPinPage TapForgotPinButton()
        {
            _app.Tap(_forgotPinButton);

            return new ForgotPinPage(this._app);
        }

        public PinPage ValidatePinLabel(string ExpectedText)
        {
            _app.WaitForElement(_pinLabel(ExpectedText));

            return this;
        }

    }
}
