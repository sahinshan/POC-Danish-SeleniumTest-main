using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class CarePlanTypesRecordPage : CommonMethods
    {
        public CarePlanTypesRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CarePlantype = By.Id("iframe_careplantype");
        readonly By iframe_CarePlanTypesRecordtype = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=careplantype')]");

        readonly By CarePlanTypesRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");

        readonly By name_Field = By.Id("CWField_name");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By validforexportNo_Field = By.Id("CWField_validforexport_0");
       



        public CarePlanTypesRecordPage WaitForCarePlanTypesRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);


            WaitForElement(iframe_CarePlantype);
            SwitchToIframe(iframe_CarePlantype);

            WaitForElement(iframe_CarePlanTypesRecordtype);
            SwitchToIframe(iframe_CarePlanTypesRecordtype);

            WaitForElement(CarePlanTypesRecordPageHeader);

           

            return this;
        }

        public CarePlanTypesRecordPage InsertName(String TextToInsert)
        {
            WaitForElement(name_Field);

            SendKeys(name_Field, TextToInsert);

            return this;
        }

        public CarePlanTypesRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);

            Click(saveAndCloseButton);

            return this;
        }
        public CarePlanTypesRecordPage ValidatevalidforexportShoulBeFalse(bool ExpectVisible)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(validforexportNo_Field);
            }
            else
            {
                WaitForElementNotVisible(validforexportNo_Field, 3);
            }

            return this;
        }
    }
 }

