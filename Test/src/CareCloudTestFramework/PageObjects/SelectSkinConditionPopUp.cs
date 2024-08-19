using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest.Configuration;
using Xamarin.UITest;
using Xamarin.UITest.Queries;


namespace CareCloudTestFramework.PageObjects
{
    public class SelectSkinConditionPopUp : CommonMethods
    {
        readonly Func<AppQuery, AppWebQuery> _Header = e => e.XPath("//div[text()='Select Skin Condition']");

        Func<AppQuery, AppWebQuery> _searchSkinConditionsTxtFld => e => e.XPath("//*[@id='search-lookup']");

        Func<AppQuery, AppWebQuery> _SkinConditionsChkBox(string text) => e => e.XPath("//span[text()='"+text+"']/parent::label/parent::div");


         Func<AppQuery, AppWebQuery> _confirmSkinConditions => e => e.XPath("//span[text()='Confirm Skin Condition']");
         Func<AppQuery, AppWebQuery> _cancelButton => e => e.XPath("//span[text()='Cancel']");


       
        public SelectSkinConditionPopUp(IApp app)
        {
            _app = app;

        }


        public SelectSkinConditionPopUp WaitForSkinConditionPopUpToLoad()
        {
            _app.WaitForElement(_Header);

            _app.WaitForElement(_confirmSkinConditions);
            _app.WaitForElement(_cancelButton);
          

            return this;
        }


       
        public SelectSkinConditionPopUp searchObservation(string observations)
        {
            _app.ClearText(_searchSkinConditionsTxtFld);
            _app.DismissKeyboard();

            _app.EnterText(_searchSkinConditionsTxtFld, observations);
            _app.DismissKeyboard();
            return this;
        }

        public SelectSkinConditionPopUp TapOnObservationsChk(string text)
        {
            System.Threading.Thread.Sleep(1000);
            _app.Tap(_SkinConditionsChkBox(text));

            return this;
        }

        public SelectSkinConditionPopUp TapConfirmObservations()
        {
            _app.Tap(_confirmSkinConditions);

            return this;
        }

        public SelectSkinConditionPopUp TapCancelButton()
        {
            _app.Tap(_cancelButton);

            return this;
        }


    }
}
