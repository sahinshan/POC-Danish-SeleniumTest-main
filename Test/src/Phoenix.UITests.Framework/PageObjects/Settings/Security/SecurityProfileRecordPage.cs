
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class SecurityProfileRecordPage : CommonMethods
    {
        public SecurityProfileRecordPage(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By cwContentIFrame = By.Id("CWContentIFrame");
        readonly By recordIFrame = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=securityprofile&')]");
        readonly By recordIFrameforsystemuser = By.XPath("//iframe[contains(@id,'iframe_CWDialog_')][contains(@src,'type=usersecurityprofile&')]");

        readonly By pageTitle = By.XPath("//*[@id='CWToolbar']/div/h1");

        

        readonly By RecordPrivilegesTab = By.XPath("//*[@id='CWRecordPrvLink']/a");
        readonly By SecurityProfile_Field = By.Id("CWField_securityprofileid_Link");



        #region Record Privileges Area

        readonly By ViewPrivilegeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[2]");
        readonly By CreatedPrivilegeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[3]");
        readonly By EditPrivilegeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[4]");
        readonly By DeletePrivilegeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[5]");
        readonly By SharePrivilegeHeader = By.XPath("//*[@id='CWGridHeaderRow']/th[6]");

        By businessObjectCell(string position) => By.XPath("//*[@id='CWGrid']/tbody/tr[" + position + "]/td[1]");
        By ViewPrivilegeCell(string position) => By.XPath("//*[@id='CWGrid']/tbody/tr[" + position + "]/td[2]");
        By CreatePrivilegeCell(string position) => By.XPath("//*[@id='CWGrid']/tbody/tr[" + position + "]/td[3]");
        By EditPrivilegeCell(string position) => By.XPath("//*[@id='CWGrid']/tbody/tr[" + position + "]/td[4]");
        By DeletePrivilegeCell(string position) => By.XPath("//*[@id='CWGrid']/tbody/tr[" + position + "]/td[5]");
        By SharePrivilegeCell(string position) => By.XPath("//*[@id='CWGrid']/tbody/tr[" + position + "]/td[6]");

        #endregion



        public SecurityProfileRecordPage WaitForSecurityProfileRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrame);
            SwitchToIframe(recordIFrame);

            WaitForElement(pageTitle);
            WaitForElement(RecordPrivilegesTab);

            return this;
        }

        public SecurityProfileRecordPage WaitForSystemUserSecurityProfileRecordPageToLoad()
        {
            driver.SwitchTo().DefaultContent();

            WaitForElement(cwContentIFrame);
            SwitchToIframe(cwContentIFrame);

            WaitForElement(recordIFrameforsystemuser);
            SwitchToIframe(recordIFrameforsystemuser);

            return this;
        }





        public SecurityProfileRecordPage ClickRecordPrivilegesTab()
        {
            WaitForElementToBeClickable(RecordPrivilegesTab);
            Click(RecordPrivilegesTab);
            
            return this;
        }

        public SecurityProfileRecordPage WaitForRecordPrivilegesTabToLoad()
        {
            WaitForElementVisible(ViewPrivilegeHeader);
            WaitForElementVisible(CreatedPrivilegeHeader);
            WaitForElementVisible(EditPrivilegeHeader);
            WaitForElementVisible(DeletePrivilegeHeader);
            WaitForElementVisible(SharePrivilegeHeader);

            return this;
        }

        public SecurityProfileRecordPage ValidateBusinessObjectName(string BusinessObjectPosition, string ExpectedText)
        {
            ValidateElementText(businessObjectCell(BusinessObjectPosition), ExpectedText);

            return this;
        }
        public SecurityProfileRecordPage ValidateBusinessObjectViewPrivilege(string BusinessObjectPosition, string ExpectedText)
        {
            ValidateElementText(ViewPrivilegeCell(BusinessObjectPosition), ExpectedText);

            return this;
        }
        public SecurityProfileRecordPage ValidateBusinessObjectCreatePrivilege(string BusinessObjectPosition, string ExpectedText)
        {
            ValidateElementText(CreatePrivilegeCell(BusinessObjectPosition), ExpectedText);

            return this;
        }
        public SecurityProfileRecordPage ValidateBusinessObjectEditPrivilege(string BusinessObjectPosition, string ExpectedText)
        {
            ValidateElementText(EditPrivilegeCell(BusinessObjectPosition), ExpectedText);

            return this;
        }
        public SecurityProfileRecordPage ValidateBusinessObjectDeletePrivilege(string BusinessObjectPosition, string ExpectedText)
        {
            ValidateElementText(DeletePrivilegeCell(BusinessObjectPosition), ExpectedText);

            return this;
        }
        public SecurityProfileRecordPage ValidateBusinessObjectSharePrivilege(string BusinessObjectPosition, string ExpectedText)
        {
            ValidateElementText(SharePrivilegeCell(BusinessObjectPosition), ExpectedText);

            return this;
        }

        public SecurityProfileRecordPage ValidateSecurityProfileField(string ExpectedText)
        {
            ValidateElementText(SecurityProfile_Field, ExpectedText);

            return this;
        }
    }
}
