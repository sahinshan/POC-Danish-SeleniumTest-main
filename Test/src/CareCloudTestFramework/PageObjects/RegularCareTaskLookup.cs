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
    public class RegularCareTaskLookup : CommonMethods
    {
        readonly Func<AppQuery, AppWebQuery> _regularCareTasklookupEntity = e => e.XPath("//*[@class='mcc-drawer mcc-drawer--bottom mu-h-auto mcc-drawer--open']");
        Func<AppQuery, AppWebQuery> _regularCareTaskMessage(string message) => e => e.XPath("//*[@class='mcc-drawer__body']/h4[text()='" + message + "']");
        readonly Func<AppQuery, AppWebQuery> _saveNCloseBtn = e => e.XPath("//button[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--primary cc-drawer-footer-yes-btn']");
        readonly Func<AppQuery, AppWebQuery> _CloseBtn = e => e.XPath("//button[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--outline cc-drawer-footer-no-btn']");
        readonly Func<AppQuery, AppWebQuery> _removeTaskBtn = e => e.XPath("//button[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--danger cc-drawer-footer-yes-btn']");
        readonly Func<AppQuery, AppWebQuery> _EndVisitBtn = e => e.XPath("//button[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--primary cc-drawer-footer-yes-btn']/span[text()='End the Visit']");


        Func<AppQuery, AppQuery> _recordTitle(string recordText) => e => e.Property("text").Contains(recordText);
        Func<AppQuery, AppQuery> _recordTitleExactMatch(string recordText) => e => e.Marked(recordText);




        public RegularCareTaskLookup(IApp app)
        {
            _app = app;

        }


        public RegularCareTaskLookup WaitForLookupPopupToLoad()
        {
            WaitForElement(_regularCareTasklookupEntity);

            return this;
        }

        public RegularCareTaskLookup ValidateRegualarCareTaskMessage(String message, String ExpectText)
        {
            WaitForElement(_regularCareTaskMessage(message));
            string fieldText = GetWebElementText(_regularCareTaskMessage(message));
            Assert.AreEqual(ExpectText, fieldText);

            return this;
        }




        public RegularCareTaskLookup TapOnSaveNClose()
        {
            Tap(_saveNCloseBtn);

            return this;
        }

        public RegularCareTaskLookup TapOnRemoveTask()
        {
            Tap(_removeTaskBtn);

            return this;
        }


        public RegularCareTaskLookup TapCloseButton()
        {
            Tap(_CloseBtn);

            return this;
        }

        public RegularCareTaskLookup TapEndTheVisitButton()
        {
            Tap(_EndVisitBtn);

            return this;
        }


    }
}
