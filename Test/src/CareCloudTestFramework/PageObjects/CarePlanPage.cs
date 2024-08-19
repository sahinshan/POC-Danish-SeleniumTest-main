using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest.Queries;
using Xamarin.UITest;
using NUnit.Framework;
using System.Security.Policy;

namespace CareCloudTestFramework.PageObjects
{
    public class CarePlanPage : CommonMethods
    {
        public TimeSpan _timeoutTimeSpan = new TimeSpan(0, 1, 30);
        public TimeSpan _retryFrequency = new TimeSpan(0, 0, 1);

        readonly Func<AppQuery, AppWebQuery> _saveNCloseButton = e => e.XPath("//button/span[text()='Save and close']");
        readonly Func<AppQuery, AppWebQuery> _cancelButton = e => e.XPath("//button/span[text()='Cancel']");

        Func<AppQuery, AppWebQuery> _PersonInfoHeader(string personfullname, int number) => e => e.XPath("//span[text()=' " + personfullname + " (ID " + number + ")']");
        Func<AppQuery, AppWebQuery> _CarePlanHeader => e => e.XPath("//h1[text()='Care Plan']");
        Func<AppQuery, AppWebQuery> _CarePlanList => e => e.XPath("//div[@id='care-plan-list']");
        Func<AppQuery, AppWebQuery> _CareNeedText(string careneed) => e => e.XPath("//h2[contains(text(),'"+ careneed+"')]");
        Func<AppQuery, AppWebQuery> _AuthorisedHeader => e => e.XPath("//span[text()='AUTHORISED']");
        Func<AppQuery, AppWebQuery> _startDate => e => e.XPath("//span[text()='AUTHORISED']/parent::h2/parent::div/div/div");

        Func<AppQuery, AppWebQuery> _CareAndSupportNeeds => e => e.XPath("//h4[text()='What care and support needs do I currently have?']/parent::div/div");
        Func<AppQuery, AppWebQuery> _DesiredOutcomes => e => e.XPath("//h4[text()='What are my desired outcomes?']/parent::div/div");

        Func<AppQuery, AppQuery> _WebViewElement = e => e.Class("android.webkit.WebView");
        Func<AppQuery, AppWebQuery> _BackToResidentDetailsButton => e => e.XPath("//button[@id='back-to-resident-details-btn']");


        public CarePlanPage(IApp app)
        {
            _app = app;

        }



        public CarePlanPage WaitForCarePlanPageLookupToLoad(string fullname,int number)
        {
            WaitForElement(_PersonInfoHeader(fullname, number));
            WaitForElement(_CarePlanHeader);


            return this;
        }

        public CarePlanPage TapCarePlan()
        {

            Tap(_CarePlanList);

            return this;
        }

        public CarePlanPage validateCarePlanDetails(string ExpectText, string fullname, int number,string careneed)
        {

            WaitForElement(_PersonInfoHeader(fullname, number));
            WaitForElement(_CareNeedText(careneed));
            WaitForElement(_AuthorisedHeader);
            System.Threading.Thread.Sleep(1000);

            return this;

        }

        public CarePlanPage validateCareAndSupportNeeds(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_CareAndSupportNeeds, ExpectText);

            return this;

        }

        public CarePlanPage validateDesiredOutcomes(string ExpectText)
        {

            System.Threading.Thread.Sleep(1000);
            ValidateElementText(_DesiredOutcomes, ExpectText);

            return this;

        }
        public CarePlanPage TapBackToResidentDetailsButton()
        {

            Tap(_BackToResidentDetailsButton);

            return this;
        }

    }
}
