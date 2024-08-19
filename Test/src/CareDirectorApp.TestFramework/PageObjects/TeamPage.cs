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
    public class TeamPage
    {

        readonly Func<AppQuery, AppQuery> mainMenuIcon = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _aboutPageIconButton = e => e.Marked("EntityImage");
        Func<AppQuery, AppQuery> _pageTitle(string TeamName) => e => e.Text("TEAM: " + TeamName);

        readonly Func<AppQuery, AppQuery> _generalSectionLable = e => e.Text("General");

        readonly Func<AppQuery, AppQuery> _nameLabel = e => e.Text("General");
        readonly Func<AppQuery, AppQuery> _teamManagerLabel = e => e.Text("Team Manager");
        readonly Func<AppQuery, AppQuery> _businessUnitLabel = e => e.Text("Business Unit");
        readonly Func<AppQuery, AppQuery> _emailAddressLabel = e => e.Text("Email Address");
        readonly Func<AppQuery, AppQuery> _descriptionLabel = e => e.Text("Description");
        readonly Func<AppQuery, AppQuery> _referenceDataOwnerLabel = e => e.Text("Reference Data Owner");

        readonly Func<AppQuery, AppQuery> _nameField = e => e.Text("Field_833725ec8d41e911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _teamManagerField = e => e.Text("caa2ca518e41e911a2c40050569231cf_LookupEntry");
        readonly Func<AppQuery, AppQuery> _businessUnitField = e => e.Text("Field_a5c46f5c8e41e911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _emailAddressField = e => e.Text("Field_16b68c698e41e911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _descriptionField = e => e.Text("Field_4bbed5628e41e911a2c40050569231cf");
        readonly Func<AppQuery, AppQuery> _referenceDataOwnerField = e => e.Text("Field_1ab68c698e41e911a2c40050569231cf");



        readonly IApp _app;

        public TeamPage(IApp app)
        {
            _app = app;
        }

        public TeamPage WaitForTeamPageToLoad(string TeamName)
        {
            _app.WaitForElement(mainMenuIcon);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_aboutPageIconButton);
            _app.WaitForElement(_pageTitle(TeamName));

            _app.WaitForElement(_generalSectionLable);

            _app.WaitForElement(_nameLabel);
            _app.WaitForElement(_teamManagerLabel);
            _app.WaitForElement(_businessUnitLabel);
            _app.WaitForElement(_emailAddressLabel);
            _app.WaitForElement(_descriptionLabel);
            _app.WaitForElement(_referenceDataOwnerLabel);
            
            return this;
        }

        public TeamPage TapBackButton()
        {
            _app.Tap(_backButton);

            return this;
        }


    }
}
