
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace Phoenix.UITests.SmokeTests
{
    [TestClass]
    public class ProviderDiary_Scripts : FunctionalTest
    {
        private Guid userid;
        private string username;
        private string password;
        private string DataEncoded;

        [TestInitialize()]
        public void SmokeTests_SetupTest()
        {
            try
            {
                #region Authentication Data

                username = ConfigurationManager.AppSettings["Username"];
                password = ConfigurationManager.AppSettings["Password"];
                DataEncoded = ConfigurationManager.AppSettings["DataEncoded"];

                if (DataEncoded.Equals("true"))
                {
                    var base64EncodedBytes = System.Convert.FromBase64String(username);
                    username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                    base64EncodedBytes = System.Convert.FromBase64String(password);
                    password = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                }

                userid = dbHelper.systemUser.GetSystemUserByUserName(username).FirstOrDefault();
                dbHelper.systemUser.UpdateLastPasswordChangedDate(userid, DateTime.Now.Date);

                #endregion

            }
            catch
            {
                if (driver != null)
                    driver.Quit();

                this.ShutDownAllProcesses();

                throw;
            }
        }


        //[TestMethod]
        public void ProviderDiary_Scripts_UITestMethod01()
        {
            Stopwatch sw = new Stopwatch();

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Iteration: " + (i + 1));

                loginPage
                .GoToLoginPage()
                .Login("ProviderDiaryUser1", "Passw0rd_!")
                .WaitFormHomePageToLoad(false, false, false);

                mainMenu
                    .WaitForMainMenuToLoad()
                    .NavigateToProviderDiarySection();

                sw.Reset();
                sw.Start();

                providerDiaryPage
                    .WaitForProviderDiaryPageToLoad()
                    .SearchProviderRecord("Abbott - No Address");

                sw.Stop();
                Console.WriteLine(sw.Elapsed.TotalSeconds);

                mainMenu
                    .WaitForMainMenuToLoad()
                    .ClickSignOutButton();
            }


        }

    }
}
