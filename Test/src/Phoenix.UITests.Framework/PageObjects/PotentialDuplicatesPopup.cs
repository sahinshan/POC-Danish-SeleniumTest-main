using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{
    public class PotentialDuplicatesPopup : CommonMethods
    {
        public PotentialDuplicatesPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        #region Potential Duplicates 

        readonly By CWContentIFrame = By.Id("CWContentIFrame");
        readonly By iframe_CWNewPerson = By.Id("iframe_CWNewPerson");
        readonly By iframe_CWPotentialDuplicatesDialog = By.Id("iframe_CWPotentialDuplicatesDialog");

        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/header/h1");
        
        readonly By secondPopupHeader = By.XPath("//*[@id='CWHeader']/h1");

        By duplicateRecord(string RecordID) => By.XPath("//*[@id='" + RecordID + "']/div/div/div");

        readonly By CWUpdateExisting = By.Id("CWUpdateExisting");
        readonly By CWCreateNew = By.Id("CWCreateNew");
        

        #endregion

        #region Edit Duplicated Data

        readonly By newRecordHeader = By.XPath("//*[@id='CWNewItemColumn']/h3[text()='New']");
        readonly By newRecordEthnicityCheckbox = By.XPath("//*[@id='CWNewItemColumn']/div/div/div/div/input[@name='ethnicityid']");
        readonly By newRecordEthnicityValue = By.XPath("//*[@id='CWNewItemColumn']/div/div/*[@id='ethnicityid']");
        readonly By newRecordDateOfBirthCheckbox = By.XPath("//*[@id='CWNewItemColumn']/div/div/div/div/input[@name='dateofbirth']");
        readonly By newRecordDateOfBirthValue = By.XPath("//*[@id='CWNewItemColumn']/div/div/*[@id='dateofbirth']");
        readonly By newRecordAgeGroupCheckbox = By.XPath("//*[@id='CWNewItemColumn']/div/div/div/div/input[@name='agegroupid']");
        readonly By newRecordAgeGroupValue = By.XPath("//*[@id='CWNewItemColumn']/div/div/*[@id='agegroupid']");

        readonly By existingRecordHeader = By.XPath("//*[@id='CWDuplicatedItemColumn']/h3[text()='Existing']");
        readonly By existingRecordEthnicityCheckbox = By.XPath("//*[@id='CWDuplicatedItemColumn']/div/div/div/div/input[@name='ethnicityid']");
        readonly By existingRecordEthnicityValue = By.XPath("//*[@id='CWDuplicatedItemColumn']/div/div/*[@id='ethnicityid']");
        readonly By existingRecordDateOfBirthCheckbox = By.XPath("//*[@id='CWDuplicatedItemColumn']/div/div/div/div/input[@name='dateofbirth']");
        readonly By existingRecordDateOfBirthValue = By.XPath("//*[@id='CWDuplicatedItemColumn']/div/div/*[@id='dateofbirth']");
        readonly By existingRecordAgeGroupCheckbox = By.XPath("//*[@id='CWDuplicatedItemColumn']/div/div/div/div/input[@name='agegroupid']");
        readonly By existingRecordAgeGroupValue = By.XPath("//*[@id='CWDuplicatedItemColumn']/div/div/*[@id='agegroupid']");


        readonly By SaveButton = By.Id("CWSave");
        readonly By BackButton = By.Id("CWBack");

        #endregion

        readonly By CWClose = By.Id("CWClose");

        public PotentialDuplicatesPopup WaitForPotentialDuplicatesPopupToLoad()
        {
            SwitchToDefaultFrame();

            WaitForElement(CWContentIFrame);
            SwitchToIframe(CWContentIFrame);

            WaitForElement(iframe_CWNewPerson);
            SwitchToIframe(iframe_CWNewPerson);

            WaitForElement(iframe_CWPotentialDuplicatesDialog);
            SwitchToIframe(iframe_CWPotentialDuplicatesDialog);

            WaitForElement(popupHeader);
            
            WaitForElement(CWUpdateExisting);
            WaitForElement(CWCreateNew);
            WaitForElement(CWClose);

            return this;
        }

        public PotentialDuplicatesPopup WaitForEditDuplicatesPopupToLoad()
        {

            WaitForElement(secondPopupHeader);

            WaitForElement(newRecordHeader);
            WaitForElement(existingRecordHeader);
            
            WaitForElement(SaveButton);
            WaitForElement(BackButton);
            WaitForElement(CWClose);

            return this;
        }



        public PotentialDuplicatesPopup ValidateDuplicateRecordVisibility(string RecordID, bool ExpectVisible)
        {
            if (ExpectVisible)
                WaitForElementVisible(duplicateRecord(RecordID));
            else
                WaitForElementNotVisible(duplicateRecord(RecordID), 7);

            return this;
        }

        public PotentialDuplicatesPopup CllickDuplicateRecord(string RecordID)
        {
            WaitForElementToBeClickable(duplicateRecord(RecordID));
            Click(duplicateRecord(RecordID));

            return this;
        }

        public PotentialDuplicatesPopup ClickUpdateExistingButton()
        {
            Click(CWUpdateExisting);

            return this;
        }

        public PotentialDuplicatesPopup ClickCreateButton()
        {
            Click(CWCreateNew);

            return this;
        }

        public PotentialDuplicatesPopup ClickCloseButton()
        {
            Click(CWClose);

            return this;
        }

        public PotentialDuplicatesPopup ValidateNewRecordDOBChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(newRecordDateOfBirthCheckbox);
            else
                ValidateElementNotChecked(newRecordDateOfBirthCheckbox);

            return this;
        }

        public PotentialDuplicatesPopup ValidateExistingRecordDOBChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(existingRecordDateOfBirthCheckbox);
            else
                ValidateElementNotChecked(existingRecordDateOfBirthCheckbox);

            return this;
        }

        public PotentialDuplicatesPopup ValidateNewRecordAgeGroupChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(newRecordAgeGroupCheckbox);
            else
                ValidateElementNotChecked(newRecordAgeGroupCheckbox);

            return this;
        }

        public PotentialDuplicatesPopup ValidateExistingRecordAgeGroupChecked(bool ExpectChecked)
        {
            if (ExpectChecked)
                ValidateElementChecked(existingRecordAgeGroupCheckbox);
            else
                ValidateElementNotChecked(existingRecordAgeGroupCheckbox);

            return this;
        }

        public PotentialDuplicatesPopup ValidateNewRecordDOBValue(string Expectedvalue)
        {
            ValidateElementValue(newRecordDateOfBirthValue, Expectedvalue);

            return this;
        }

        public PotentialDuplicatesPopup ValidateExistingRecordDOBValue(string Expectedvalue)
        {
            ValidateElementValue(existingRecordDateOfBirthValue, Expectedvalue);

            return this;
        }

        public PotentialDuplicatesPopup ValidateNewRecordAgeGroupValue(string Expectedvalue)
        {
            ValidateElementValue(newRecordAgeGroupValue, Expectedvalue);

            return this;
        }

        public PotentialDuplicatesPopup ValidateExistingRecordAgeGroupValue(string Expectedvalue)
        {
            ValidateElementValue(existingRecordAgeGroupValue, Expectedvalue);

            return this;
        }

    }
}
