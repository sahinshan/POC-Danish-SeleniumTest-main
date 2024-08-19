using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SelectCaseTypePopUp : CommonMethods
    {


        public SelectCaseTypePopUp(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By personRecordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=case&')]");

        readonly By selectCaseTypePopUp = By.Id("CWDataFormSelectionPanel");
        readonly By caseTypes = By.Id("dataFormList");
        readonly By popUpHeader = By.Id("CWDataFormListHeader");
        readonly By nextButton = By.Id("btnChooseDataForm");
        readonly By closeButton = By.Id("CWCloseButton");
        
        public SelectCaseTypePopUp WaitForSelectCaseTypePopUpToLoad()
        {


            this.SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(personRecordIFrame);
            SwitchToIframe(personRecordIFrame);

         

            WaitForElement(popUpHeader);

            return this;
        }

       

        public SelectCaseTypePopUp TapNextButton()
        {
            Click(nextButton);
           
            return this;
        }

        private IAlert WaitForAlert()
        {
            int i = 0;
            while (i++ < 15)
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException e)
                {
                    Thread.Sleep(1000);
                    continue;
                }
            }

            throw new Exception("No Alert was displayed after 5 seconds");
        }

        public SelectCaseTypePopUp SelectViewByText(string TextToSelect)
        {

            SelectPicklistElementByText(caseTypes, TextToSelect);
            WaitForElementNotVisible("CWRefreshPanel", 7);


            return this;
        }


    }
}
