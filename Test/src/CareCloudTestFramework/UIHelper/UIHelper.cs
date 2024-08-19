using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace CareCloudTestFramework
{
    public class UIHelper
    {
        public UIHelper()
        {

        }

        public IApp StartApp(string PathToApkFile, string DeviceSerial, AppDataMode DataMode = AppDataMode.DoNotClear)
        {
            return ConfigureApp
                .Android
                .ApkFile(PathToApkFile)
                .DeviceSerial(DeviceSerial)
                .StartApp(DataMode);
        }
    }
}