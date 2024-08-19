using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CareDirectorApp.TestFramework.PageObjects
{
    public class SettingsPage : CommonMethods
    {
        readonly Func<AppQuery, AppQuery> _mainMenuButton = e => e.Marked("MenuBtn");
        readonly Func<AppQuery, AppQuery> _caredirecotrIcon = e => e.Marked("AppIcon");
        readonly Func<AppQuery, AppQuery> _noConnectivityIcon = e => e.Marked("NoConnectivityToolbarItem");

        readonly Func<AppQuery, AppQuery> _backIcon = e => e.Marked("BackImage");
        readonly Func<AppQuery, AppQuery> _pageIcon = e => e.Marked("EntityImage");
        readonly Func<AppQuery, AppQuery> _pageTitle = e => e.Marked("SETTINGS");

        readonly Func<AppQuery, AppQuery> _connectivityLabel = e => e.Marked("ConnectivityLabel").Text("Connectivity & Synchronisation");

        readonly Func<AppQuery, AppQuery> _offlineModeLabel = e => e.Marked("ManualOfflineLabel").Text("Offline Mode");
        readonly Func<AppQuery, AppQuery> _offlineSwitch = e => e.Marked("ManualOffline");

        readonly Func<AppQuery, AppQuery> _syncDataLabel = e => e.Marked("SynchronizeNowLabel").Text("Sync Data");
        readonly Func<AppQuery, AppQuery> _lastSyndDateLabel = e => e.Marked("LastSyncDate");
        readonly Func<AppQuery, AppQuery> _syncNowButton = e => e.Marked("SyncNow").Text("Sync Now");

        readonly Func<AppQuery, AppQuery> _syncProgressionBar = e => e.Marked("SynchronizationProgressBar");
        readonly Func<AppQuery, AppQuery> _syncTypeLabel = e => e.Marked("SynchronizationTypeLabel");
        readonly Func<AppQuery, AppQuery> _syncStatusLabel = e => e.Marked("SynchronizationStatusLabel");
        readonly Func<AppQuery, AppQuery> _syncStepLabel = e => e.Marked("SynchronizationStepLabel");
        readonly Func<AppQuery, AppQuery> _syncLoadingLabel = e => e.Marked("LoadingLabel");
        readonly Func<AppQuery, AppQuery> _syncProgressLabel = e => e.Marked("ProgressLabel");

        readonly Func<AppQuery, AppQuery> _securityLabel = e => e.Marked("SettingsLabel").Text("Security");

        readonly Func<AppQuery, AppQuery> _changePinImage = e => e.Marked("ChangePinImage");
        readonly Func<AppQuery, AppQuery> _changePinLabel = e => e.Marked("ChangePinLabel");


        public SettingsPage(IApp app)
        {
            this._app = app;
        }

        public SettingsPage WaitForSettingsPageToLoad()
        {
            this.WaitForElement(_mainMenuButton);
            this.WaitForElement(_caredirecotrIcon);

            this.WaitForElement(_backIcon);
            this.WaitForElement(_pageIcon);
            this.WaitForElement(_pageTitle);

            this.WaitForElement(_connectivityLabel);

            this.WaitForElement(_offlineModeLabel);
            this.WaitForElement(_offlineSwitch);

            this.WaitForElement(_syncDataLabel);
            this.WaitForElement(_lastSyndDateLabel);
            this.WaitForElement(_syncNowButton);

            this.WaitForElement(_securityLabel);

            this.WaitForElement(_changePinImage);
            this.WaitForElement(_changePinLabel);

            return this;
        }

        public SettingsPage WaitForSyncProcessToFinish()
        {
            this.WaitForElement(_backIcon);
            this.WaitForElement(_pageIcon);
            this.WaitForElement(_pageTitle);

            TryWaitForElement(_syncProgressionBar);

            this.WaitForElementNotVisible(_syncProgressionBar, new TimeSpan(0, 2, 30));
            this.WaitForElementNotVisible(_syncTypeLabel, new TimeSpan(0, 0, 60));
            this.WaitForElementNotVisible(_syncStatusLabel, new TimeSpan(0, 0, 60));
            this.WaitForElementNotVisible(_syncStepLabel, new TimeSpan(0, 0, 60));
            this.WaitForElementNotVisible(_syncLoadingLabel, new TimeSpan(0, 0, 60));
            this.WaitForElementNotVisible(_syncProgressLabel, new TimeSpan(0, 0, 60));

            return this;
        }

        public SettingsPage TapOfflineSwitch()
        {
            this.Tap(_offlineSwitch);

            return this;
        }

        public PinPage TapChangePinButton()
        {
            this.Tap(_changePinLabel);

            return new PinPage(this._app);
        }

        public SettingsPage TapSyncNowButton()
        {
            this.Tap(_syncNowButton);

            WaitForSyncProcessToFinish();

            return this;
        }

        public SettingsPage SetTheAppInOfflineMode()
        {
            WaitForSyncProcessToFinish();

            bool offlinneModeActive = CheckIfElementVisible(_noConnectivityIcon);

            if (offlinneModeActive)
                return this;

            WaitForElement(_offlineSwitch);
            this.Tap(_offlineSwitch);

            return WaitForSyncProcessToFinish();
        }

        public SettingsPage SetTheAppInOnlineMode()
        {
            WaitForSyncProcessToFinish();

            bool offlinneModeActive = CheckIfElementVisible(_noConnectivityIcon);

            if (!offlinneModeActive)
                return this;

            WaitForElement(_offlineSwitch);
            this.Tap(_offlineSwitch);

            return WaitForSyncProcessToFinish();
        }

    }
}
