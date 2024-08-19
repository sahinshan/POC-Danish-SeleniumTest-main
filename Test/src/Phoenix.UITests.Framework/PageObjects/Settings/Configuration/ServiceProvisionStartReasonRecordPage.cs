using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class ServiceProvisionStartReasonRecordPage : CommonMethods
    {
        public ServiceProvisionStartReasonRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By contentIFrame = By.Id("CWContentIFrame");
        readonly By serviceProvisionStartReason = By.Id("iframe_serviceprovisionstartreason");
        readonly By iframe_ServiceProvisionStartRecordReason = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=serviceprovisionstartreason&')]");

        readonly By ServiceProvisionStartReasonRecordPageHeader = By.XPath("//*[@id='CWToolbar']/div/h1");
        readonly By saveAndCloseButton = By.Id("TI_SaveAndCloseButton");

        readonly By defaultBrokerageStartReason_NoRadioButton = By.Id("CWField_defaultbrokeragestartreason_0");
        readonly By defaultBrokerageStartReason_YesRadioButton = By.Id("CWField_defaultbrokeragestartreason_1");
        
  


        By recordRowCheckBox(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[1]/input");
        By recordRow_CreatedOnCell(string recordID) => By.XPath("//table[@id='CWGrid']/tbody/tr[@id='" + recordID + "']/td[2]");

       



        public ServiceProvisionStartReasonRecordPage WaitForServiceProvisionStartReasonRecordPageToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(contentIFrame);
            SwitchToIframe(contentIFrame);

            WaitForElement(serviceProvisionStartReason);
            SwitchToIframe(serviceProvisionStartReason);

            WaitForElement(iframe_ServiceProvisionStartRecordReason);
            SwitchToIframe(iframe_ServiceProvisionStartRecordReason);

          

            return this;
        }

      


      

        public ServiceProvisionStartReasonRecordPage SelectServiceProvisionStartReasonsRecord(string RecordId)
        {
            WaitForElement(recordRowCheckBox(RecordId));
            Click(recordRowCheckBox(RecordId));

            return this;
        }

      

        public ServiceProvisionStartReasonRecordPage ClickDefaultBrokerageStartReason_NoRadioButton()
        {
            WaitForElement(defaultBrokerageStartReason_NoRadioButton);

            Click(defaultBrokerageStartReason_NoRadioButton);

            return this;
        }

        public ServiceProvisionStartReasonRecordPage ClickDefaultBrokerageStartReason_YesRadioButton()
        {
            WaitForElement(defaultBrokerageStartReason_YesRadioButton);

            Click(defaultBrokerageStartReason_YesRadioButton);

            return this;
        }

        public ServiceProvisionStartReasonRecordPage ClickSaveAndCloseButton()
        {
            WaitForElement(saveAndCloseButton);

            Click(saveAndCloseButton);

            return this;
        }





    }
}
