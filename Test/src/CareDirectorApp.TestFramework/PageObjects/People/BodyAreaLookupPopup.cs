using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;
using System.Collections.Generic;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class BodyAreaLookupPopup : CommonMethods
    {
        readonly Func<AppQuery, AppQuery> _lookupEntityImage = e => e.Marked("MainLayout").Descendant().Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _lookupEntityName = e => e.Marked("MainLayout").Descendant().Marked("EntityNameLabel");
        readonly Func<AppQuery, AppQuery> _lookupSearchCriteriaLabel = e => e.Marked("MainLayout").Descendant().Marked("SearchCriteriaLabel");
        readonly Func<AppQuery, AppQuery> _lookupSearchLabel = e => e.Marked("MainLayout").Descendant().Marked("SearchLabel");
        readonly Func<AppQuery, AppQuery> _lookupSearchTextbox = e => e.Marked("MainLayout").Descendant().Marked("SearchBy");
        readonly Func<AppQuery, AppQuery> _lookupSearchButton = e => e.Marked("MainLayout").Descendant().Marked("SearchByButton");
        readonly Func<AppQuery, AppQuery> _lookupRefreshButton = e => e.Marked("MainLayout").Descendant().Marked("RefreshButton");

        readonly Func<AppQuery, AppQuery> _nextPageButton = e => e.Marked("MainLayout").Descendant().Marked("Next");

        readonly Func<AppQuery, AppQuery> _closeButton = e => e.Marked("MainLayout").Descendant().Marked("CloseButtonLabel");

        Func<AppQuery, AppQuery> _recordImage(string BodyAreaID) => e => e.Marked("MainLayout").Descendant().Marked(BodyAreaID + "_ListElemImage");
        Func<AppQuery, AppQuery> _recordTitle(string RecordIdentifier) => e => e.Marked(RecordIdentifier);
        Func<AppQuery, AppQuery> _recordSubTitle(string BodyAreaID) => e => e.Marked("MainLayout").Descendant().Marked(BodyAreaID + "_ListElemLabel");




        public BodyAreaLookupPopup(IApp app)
        {
            _app = app;

        }


        public BodyAreaLookupPopup WaitForBodyAreaLookupPopupToLoad()
        {
            WaitForElement(_lookupEntityImage);
            WaitForElement(_lookupEntityName);
            WaitForElement(_lookupSearchCriteriaLabel);
            WaitForElement(_lookupSearchLabel);
            WaitForElement(_lookupSearchTextbox);
            WaitForElement(_lookupSearchButton);
            WaitForElement(_lookupRefreshButton);
            
            WaitForElement(_closeButton);

            return this;
        }


        public BodyAreaLookupPopup InsertSearchQuery(string SearchQuery)
        {
            EnterText(_lookupSearchTextbox, SearchQuery);

            return this;
        }

        public BodyAreaLookupPopup TapSearchButtonQuery()
        {
            Tap(_lookupSearchButton);

            return this;
        }

        public BodyAreaLookupPopup TapOnRecord(string RecordIdentifier)
        {
            Tap(_recordTitle(RecordIdentifier));

            return this;
        }

    }
}
