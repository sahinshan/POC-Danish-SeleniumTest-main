using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;

namespace CareCloudTestFramework.PageObjects
{
    public class LookupPopUp:CommonMethods
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

        Func<AppQuery, AppQuery> _recordTitle(string recordText) => e => e.Property("text").Contains(recordText);
        Func<AppQuery, AppQuery> _recordTitleExactMatch(string recordText) => e => e.Marked(recordText);




        public LookupPopUp(IApp app)
        {
            _app = app;

        }


        public LookupPopUp WaitForLookupPopupToLoad()
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


        public LookupPopUp InsertSearchQuery(string SearchQuery)
        {
            EnterText(_lookupSearchTextbox, SearchQuery);

            return this;
        }

        public LookupPopUp TapSearchButtonQuery()
        {
            Tap(_lookupSearchButton);

            return this;
        }

        public LookupPopUp TapOnRecord(string recordText)
        {
            Tap(_recordTitle(recordText));

            return this;
        }

        public LookupPopUp TapOnRecordWithExactText(string recordText)
        {
            Tap(_recordTitleExactMatch(recordText));

            return this;
        }

        public LookupPopUp ValidateElementPresent(string RecordText)
        {
            WaitForElement(_recordTitle(RecordText));

            return this;
        }

        public LookupPopUp ValidateElementNotPresent(string RecordText)
        {
            WaitForElementNotVisible(_recordTitle(RecordText));

            return this;
        }

        public LookupPopUp TapCloseButton()
        {
            Tap(_closeButton);

            return this;
        }

        public LookupPopUp ClosePopupIfOpen()
        {
            TryWaitForElement(_closeButton);

            bool buttonVisible = _app.Query(_closeButton).Any();

            if (buttonVisible)
                _app.Tap(_closeButton);

            return this;
        }
    }
}
