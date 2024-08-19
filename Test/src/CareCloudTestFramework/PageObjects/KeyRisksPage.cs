using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace CareCloudTestFramework.PageObjects
{
    public class KeyRisksPage : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _closeButton = e => e.XPath("//button[@id='close-key-risk-drawer-btn']");
        readonly Func<AppQuery, AppWebQuery> _keyRisHeader = e => e.XPath("//div/h5[text()='Key Risks']");
        readonly Func<AppQuery, AppWebQuery> _alertsNHazardsButton = e => e.XPath("//div[text()='Alerts/Hazards']");
        readonly Func<AppQuery, AppWebQuery> _alergiesButton = e => e.XPath("//div[text()='Allergies']");
        readonly Func<AppQuery, AppWebQuery> _diagnosisButton = e => e.XPath("//div[text()='Diagnosis']");

        Func<AppQuery, AppWebQuery> _alertsNHazardsText(string text) => e => e.XPath("//div[@id='key-risks-list-container']//div[1]/div/div/div[1][text()='"+ text+"']");

        Func<AppQuery, AppWebQuery> _allergyText(string text) => e => e.XPath("//div[@id='key-risks-list-container']//div[1]/div/div/div[1]/span[text()='" + text + ": ']");
        Func<AppQuery, AppWebQuery> _allergyDescription(string text) => e => e.XPath("//div[@id='key-risks-list-container']//div[1]/div/div/div[1]/span[text()='" + text + "']");

        Func<AppQuery, AppWebQuery> _DiagnosisText(string text) => e => e.XPath("//div[@id='key-risks-list-container']//div[1]/div/div/div[1][text()='" + text + "']");



        public KeyRisksPage(IApp app)
        {
            _app = app;

        }


        public KeyRisksPage TapAlertsNHazards()
        {
            WaitForElement(_keyRisHeader);
            Tap(_alertsNHazardsButton);

            return this;
        }

        public KeyRisksPage ValidateAlertsNHazards(bool ExpectFieldVisible,string text)
        {

            if (ExpectFieldVisible)
            {
                Assert.IsTrue(CheckIfElementVisible(_alertsNHazardsText(text)));
            }
            else
            {
                Assert.IsFalse(CheckIfElementVisible(_alertsNHazardsText(text)));
            }

            return this;
        }

        public KeyRisksPage TapCloseButton()
        {
            WaitForElement(_closeButton);
            Tap(_closeButton);

            return this;
        }

        public KeyRisksPage TapAlergies()
        {
            WaitForElement(_keyRisHeader);
            Tap(_alergiesButton);

            return this;
        }

        public KeyRisksPage TapDiagnosis()
        {
            WaitForElement(_keyRisHeader);
            Tap(_diagnosisButton);

            return this;
        }

        public KeyRisksPage ValidateAllergy(bool ExpectFieldVisible, string text)
        {

            if (ExpectFieldVisible)
            {
                Assert.IsTrue(CheckIfElementVisible(_allergyText(text)));
            }
            else
            {
                Assert.IsFalse(CheckIfElementVisible(_allergyText(text)));
            }

            return this;
        }

        public KeyRisksPage ValidateAllergyDescription(bool ExpectFieldVisible, string text)
        {

            if (ExpectFieldVisible)
            {
                Assert.IsTrue(CheckIfElementVisible(_allergyDescription(text)));
            }
            else
            {
                Assert.IsFalse(CheckIfElementVisible(_allergyDescription(text)));
            }

            return this;
        }

        public KeyRisksPage ValidateDiagnosis(bool ExpectFieldVisible, string text)
        {

            if (ExpectFieldVisible)
            {
                Assert.IsTrue(CheckIfElementVisible(_DiagnosisText(text)));
            }
            else
            {
                Assert.IsFalse(CheckIfElementVisible(_DiagnosisText(text)));
            }

            return this;
        }
    }
}
