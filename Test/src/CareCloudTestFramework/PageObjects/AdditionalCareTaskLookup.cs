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
    public class AdditionalCareTaskLookup : CommonMethods
    {
        readonly Func<AppQuery, AppWebQuery> _additionalCareTasklookupEntity = e => e.XPath("//*[@class='mu-d-flex mu-flex-column mu-mb-04']");
        Func<AppQuery, AppWebQuery> _regularCareTaskMessage(string message) => e => e.XPath("//*[@class='mcc-drawer__body']/h4[text()='"+message+"']");
        readonly Func<AppQuery, AppWebQuery> _addTaskBtn = e => e.XPath("//span[text()='Add Task(s)']");
        readonly Func<AppQuery, AppWebQuery> _CloseBtn = e => e.XPath("//button[@class='mcc-button mcc-button--lg mcc-button--block mcc-button--outline cc-drawer-footer-no-btn']");
       
        Func<AppQuery, AppWebQuery> _AdditionaRegularCareTask_ChkBox(String regularcaretaskid) => e => e.XPath("//input[@value='" + regularcaretaskid + "']");


                

        public AdditionalCareTaskLookup(IApp app)
        {
            _app = app;

        }


        public AdditionalCareTaskLookup WaitForAdditionalCareTaskLookupPopupToLoad()
        {
            WaitForElement(_additionalCareTasklookupEntity);

            return this;
        }

        public AdditionalCareTaskLookup CheckAdditionalRegualrCareTask(String caretaskid, bool RegularCareTaskChk)
        {

            if (RegularCareTaskChk)
            {
                ScrollToElement(_AdditionaRegularCareTask_ChkBox(caretaskid));
                _app.Tap(_AdditionaRegularCareTask_ChkBox(caretaskid));
            }
            else
            {

                Assert.IsTrue(CheckIfElementVisible(_AdditionaRegularCareTask_ChkBox(caretaskid)));
            }

            return this;
        }




        public AdditionalCareTaskLookup TapOnAddTask()
        {
            Tap(_addTaskBtn);

            return this;
        }

       

        public AdditionalCareTaskLookup TapCloseButton()
        {
            Tap(_CloseBtn);

            return this;
        }

       
    }
}
