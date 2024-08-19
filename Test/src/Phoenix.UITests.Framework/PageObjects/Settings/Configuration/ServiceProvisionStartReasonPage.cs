using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceProvisionStartReasonPage : CommonMethods
    {
        public ServiceProvisionStartReasonPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_ServiceProvisionStartReason = By.Id("iframe_serviceprovisionstartreason");

        readonly By ServiceProvisionStartReasonPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By defaultBrokerageStartReason_NoRadioButton = By.Id("CWField_defaultbrokeragestartreason_0");
        readonly By defaultBrokerageStartReason_YesRadioButton = By.Id("CWField_defaultbrokeragestartreason_1");
        
  


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_CreatedOnCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

        By RecordIdentifier(string RecordID) => By.XPath("//tr[@id='" + RecordID + "']/td[2]");



        public ServiceProvisionStartReasonPage WaitForServiceProvisionStartReasonPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(iframe_ServiceProvisionStartReason);
            SwitchToIframe(iframe_ServiceProvisionStartReason);

          

            return this;
        }

      


      

        public ServiceProvisionStartReasonPage SelectServiceProvisionStartReasonsRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

      

        public ServiceProvisionStartReasonPage ClickDefaultBrokerageStartReason_NoRadioButton()
        {
            WaitForElement(defaultBrokerageStartReason_NoRadioButton);

            Click(defaultBrokerageStartReason_NoRadioButton);

            return this;
        }

        public ServiceProvisionStartReasonPage ClickDefaultBrokerageStartReason_YesRadioButton()
        {
            WaitForElement(defaultBrokerageStartReason_YesRadioButton);

            Click(defaultBrokerageStartReason_YesRadioButton);

            return this;
        }

        public ServiceProvisionStartReasonPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);

            Click(saveAndCloseButton);

            return this;
        }

        public ServiceProvisionStartReasonPage OpenRecord(string RecordID)
        {
            WaitForElement(RecordIdentifier(RecordID));

            Click(RecordIdentifier(RecordID));

            return this;
        }




    }
}
