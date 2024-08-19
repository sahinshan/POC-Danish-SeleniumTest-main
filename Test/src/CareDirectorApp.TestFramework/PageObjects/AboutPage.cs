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
    public class AboutPage
    {

        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _aboutPageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("ABOUT");

        readonly Func<AppQuery, AppQuery> _topLabel = e => e.Marked("TopLabel").Text("v6.4.2.0");
        readonly Func<AppQuery, AppQuery> _bottomLabel = e => e.Marked("BottomLabel").Text("© 2020 One Advanced");

        

        readonly IApp _app;

        public AboutPage(IApp app)
        {
            _app = app;
        }

        public AboutPage WaitForAboutPageToLoad()
        {
            _app.WaitForElement(_caredirectorIcon);
            _app.WaitForElement(_backButton);
            _app.WaitForElement(_aboutPageIconButton);
            _app.WaitForElement(_pageTitle);
            _app.WaitForElement(_topLabel);
            _app.WaitForElement(_bottomLabel);

            return this;
        }
        

    }
}
