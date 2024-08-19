using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class StaffReviewRequirementsPopup : CommonMethods
    {
        public StaffReviewRequirementsPopup(IWebDriver driver, WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By staffReviewRequirementsPopupIFrame = By.Id("iframe_StaffReviewRequirementsPopup");

        readonly By popupHeader = By.XPath("//*[@id='CWHeaderTitle']");

        By recordCheckbox(string recordId) => By.XPath("//*[@id='" + recordId + "']/td/input");

        readonly By cancelButton = By.Id("CWCancelPopup");
        readonly By saveButton = By.Id("CWSave");




        public StaffReviewRequirementsPopup WaitForStaffReviewRequirementsPopupToLoad()
        {
            WaitForElement(staffReviewRequirementsPopupIFrame);
            SwitchToIframe(staffReviewRequirementsPopupIFrame);
            
            WaitForElement(cancelButton);
            WaitForElement(saveButton);

            return this;
        }


        public StaffReviewRequirementsPopup SelectRecord(string RecordId)
        {
            WaitForElementToBeClickable(recordCheckbox(RecordId));
            MoveToElementInPage(recordCheckbox(RecordId));
            Click(recordCheckbox(RecordId));

            return this;
        }

        public StaffReviewRequirementsPopup SelectRecord(Guid RecordId)
        {
            return SelectRecord(RecordId.ToString());
        }

        public StaffReviewRequirementsPopup ClickCancelButton()
        {
            WaitForElementToBeClickable(cancelButton);
            Click(cancelButton);

            return this;
        }

        public StaffReviewRequirementsPopup ClickSaveButton()
        {
            WaitForElementToBeClickable(saveButton);
            Click(saveButton);

            return this;
        }


    }
}
