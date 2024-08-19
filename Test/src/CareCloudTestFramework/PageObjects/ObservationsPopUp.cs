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
    public class ObservationsPopUp : CommonMethods
    {
        readonly Func<AppQuery, AppWebQuery> _Header = e => e.XPath("//div[text()='Select observations']");

        Func<AppQuery, AppWebQuery> _searchObservationTxtFld => e => e.XPath("//*[@id='search-lookup']");

        Func<AppQuery, AppWebQuery> _ObservationChkBox(string text) => e => e.XPath("//span[text()='"+text+"']/parent::label/parent::div");


         Func<AppQuery, AppWebQuery> _confirmObservationButton => e => e.XPath("//span[text()='Confirm observation(s)']");
         Func<AppQuery, AppWebQuery> _cancelButton => e => e.XPath("//span[text()='Cancel']");


       
        public ObservationsPopUp(IApp app)
        {
            _app = app;

        }


        public ObservationsPopUp WaitForObservationsPopUpToLoad()
        {
            _app.WaitForElement(_Header);

            _app.WaitForElement(_confirmObservationButton);
            _app.WaitForElement(_cancelButton);
          

            return this;
        }


       
        public ObservationsPopUp searchObservation(string observations)
        {
            _app.ClearText(_searchObservationTxtFld);
            _app.DismissKeyboard();

            _app.EnterText(_searchObservationTxtFld, observations);
            _app.DismissKeyboard();
            return this;
        }

        public ObservationsPopUp TapOnObservationsChk(string text)
        {
            _app.Tap(_ObservationChkBox(text));

            return this;
        }

        public ObservationsPopUp TapConfirmObservations()
        {
            _app.Tap(_confirmObservationButton);

            return this;
        }

        public ObservationsPopUp TapCancelButton()
        {
            _app.Tap(_cancelButton);

            return this;
        }


    }
}
