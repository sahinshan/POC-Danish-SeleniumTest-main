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
    public class TasksPage : CommonMethods
    {

        readonly Func<AppQuery, AppQuery> _mainMenu = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirectorIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _syncIcon = e => e.Marked("SyncToolbarItem");

        readonly Func<AppQuery, AppQuery> _backButton = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _addNewRecordButton = e => e.Marked("NewRecordButton");
        readonly Func<AppQuery, AppQuery> _peoplePageIconButton = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Text("TASKS");

        Func<AppQuery, AppQuery> _viewPicker(string ViewText) => e => e.Marked("ViewPicker").Text(ViewText);
        readonly Func<AppQuery, AppQuery> _genericViewPicker = e => e.Marked("ViewPicker");

        readonly Func<AppQuery, AppQuery> _searchTextBox = e => e.Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _searchButton = e => e.Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _refreshButton = e => e.Marked("RefreshButton");



        #region Top Banner

        readonly Func<AppQuery, AppQuery> _personNameAndId_TopBanner = e => e.Marked("text_heading");
        readonly Func<AppQuery, AppQuery> _bornLabel_TopBanner = e => e.Marked("label_Born:");
        readonly Func<AppQuery, AppQuery> _bornText_TopBanner = e => e.Marked("text_Born:");
        readonly Func<AppQuery, AppQuery> _genderLabel_TopBanner = e => e.Marked("label_Gender:");
        readonly Func<AppQuery, AppQuery> _genderText_TopBanner = e => e.Marked("text_Gender:");
        readonly Func<AppQuery, AppQuery> _nhsLabel_TopBanner = e => e.Marked("label_NHS No:");
        readonly Func<AppQuery, AppQuery> _nhsText_TopBanner = e => e.Marked("text_NHS No:");
        readonly Func<AppQuery, AppQuery> _toogleIcon_TopBanner = e => e.Marked("toggleIcon");
        readonly Func<AppQuery, AppQuery> _preferredNameLabel_TopBanner = e => e.Marked("label_Preferred Name:");
        readonly Func<AppQuery, AppQuery> _preferredNameText_TopBanner = e => e.Marked("text_Preferred Name:");

        readonly Func<AppQuery, AppQuery> _primaryAddressLabel_TopBanner = e => e.Marked("label_Address (Primary)");
        readonly Func<AppQuery, AppQuery> _primaryAddressText_TopBanner = e => e.Marked("text_Address (Primary)");
        readonly Func<AppQuery, AppQuery> _phoneAndEmailLabel_TopBanner = e => e.Marked("label_Phone and Email");
        readonly Func<AppQuery, AppQuery> _homeLabel_TopBanner = e => e.Marked("label_Home:");
        readonly Func<AppQuery, AppQuery> _homeText_TopBanner = e => e.Marked("text_Home:");
        readonly Func<AppQuery, AppQuery> _workLabel_TopBanner = e => e.Marked("label_Work:");
        readonly Func<AppQuery, AppQuery> _workText_TopBanner = e => e.Marked("text_Work:");
        readonly Func<AppQuery, AppQuery> _mobileLabel_TopBanner = e => e.Marked("label_Mobile:");
        readonly Func<AppQuery, AppQuery> _mobileText_TopBanner = e => e.Marked("text_Mobile:");
        readonly Func<AppQuery, AppQuery> _emailLabel_TopBanner = e => e.Marked("label_Email:");
        readonly Func<AppQuery, AppQuery> _emailText_TopBanner = e => e.Marked("text_Email:");

        #endregion



        public TasksPage(IApp app)
        {
            _app = app;
        }


        public TasksPage WaitForTasksPageToLoad(string ViewText = "Active Tasks")
        {
            _app.WaitForElement(_mainMenu);
            _app.WaitForElement(_caredirectorIcon);

            _app.WaitForElement(_backButton);
            _app.WaitForElement(_addNewRecordButton);
            _app.WaitForElement(_peoplePageIconButton);
            _app.WaitForElement(_pageTitle);

            _app.WaitForElement(_toogleIcon_TopBanner);

            _app.WaitForElement(_viewPicker(ViewText));

            _app.WaitForElement(_searchTextBox);
            _app.WaitForElement(_searchButton);
            _app.WaitForElement(_refreshButton);

            return this;
        }



    }
}
