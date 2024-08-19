using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.PageObjects
{

    /// <summary>
    /// this class represents the dynamic dialog that is displayed with messages to the user.
    /// </summary>
    public class SpellCheckPopup : CommonMethods
    {
        public SpellCheckPopup(IWebDriver driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string appURL)
        {
            this.driver = driver;
            this.Wait = Wait;
            this.appURL = appURL;
        }

        readonly By iframe_CWSpellCheckDialog = By.Id("iframe_CWSpellCheckDialog");

        

        readonly By popupHeader = By.XPath("//*[@id='CWHeader']/header/h1");


        readonly By spellCheck_TextArea = By.Id("CWSpellCheckText");
        
        readonly By changeTo_Field = By.Id("CWField_changeto");
        readonly By language_Picklist = By.Id("CWField_language");

        readonly By suggestions_Select = By.Id("CWField_suggestions");

        readonly By replace_Button = By.Id("CWReplace");
        readonly By replaceAll_Button = By.Id("CWReplaceAll");
        readonly By ignore_Button = By.Id("CWIgnore");
        readonly By ignoreAll_Button = By.Id("CWIgnoreAll");

        readonly By finishChecking_Button = By.Id("CWFinishChecking");
        readonly By cancel_Button = By.Id("CWClose");

        




        public SpellCheckPopup WaitForSpellCheckPopupToLoad()
        {
            WaitForElement(iframe_CWSpellCheckDialog);
            SwitchToIframe(iframe_CWSpellCheckDialog);

            WaitForElement(popupHeader);

            WaitForElement(spellCheck_TextArea);
            WaitForElement(changeTo_Field);
            WaitForElement(language_Picklist);
            WaitForElement(suggestions_Select);
            WaitForElement(replace_Button);
            WaitForElement(replaceAll_Button);
            WaitForElement(ignore_Button);
            WaitForElement(ignoreAll_Button);

            WaitForElementNotVisible("CWRefreshPanel", 7);

            return this;
        }

        
        public SpellCheckPopup ClickReplaceButton()
        {
            Click(replace_Button);

            return this;
        }

        public SpellCheckPopup ClickReplaceAllButton()
        {
            Click(replaceAll_Button);

            return this;
        }

        public SpellCheckPopup ClickIgnoreButton()
        {
            Click(ignore_Button);

            return this;
        }

        public SpellCheckPopup ClickIgnoreAllButton()
        {
            Click(ignoreAll_Button);

            return this;
        }

        public SpellCheckPopup ClickFinishCheckingButton()
        {
            Click(finishChecking_Button);

            return this;
        }

        public SpellCheckPopup ClickCancelButton()
        {
            Click(cancel_Button);

            return this;
        }

    }
}
