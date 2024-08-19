using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace Phoenix.Portal.UITestsFramework
{
    public abstract class CommonMethods
    {
        internal IWebDriver driver;
        internal OpenQA.Selenium.Support.UI.WebDriverWait Wait;
        internal string appURL;

        internal string GetCurrentWindowIdentifier()
        {
            return this.driver.CurrentWindowHandle;
        }

        /// <summary>
        /// get the identifiers of all windows that are currently managed by this driver instance
        /// </summary>
        /// <returns></returns>
        internal List<string> GetAllWindowIdentifier()
        {
            return this.driver.WindowHandles.ToList();
        }

        internal void SwitchToWindow(string Window)
        {
            driver.SwitchTo().Window(Window);
        }

        internal void SwitchToIframe(By IframeElement)
        {
            driver.SwitchTo().Frame(driver.FindElement(IframeElement));
        }

        internal void SwitchToDefaultFrame()
        {
            this.driver.SwitchTo().DefaultContent();
        }

        internal void WaitForBrowserURL(string ExpectedURL)
        {
            Wait.Until(c => c.Url == ExpectedURL);
        }
        internal void WaitForBrowserWindowTitle(string ExpectedTitle)
        {
            Wait.Until(c => c.Title == ExpectedTitle);
        }

        /// <summary>
        /// Apply the Wait.Until method to the supplied Element
        /// </summary>
        /// <param name="Element"></param>
        internal void WaitForElement(By Element)
        {
            Wait.Until(c => c.FindElement(Element));
        }
        /// <summary>
        /// Apply the Wait.Until method to the supplied Element
        /// </summary>
        /// <param name="Element"></param>
        internal void WaitForElement(By Element, int Seconds)
        {
            Wait.Timeout = new TimeSpan(0 ,0, Seconds);
            Wait.Until(c => c.FindElement(Element));
        }

        /// <summary>
        /// Apply the Wait.Until method to the supplied Element is not found (or removed from the DOM)
        /// </summary>
        /// <param name="Element"></param>
        internal void WaitForElementRemoved(By Element)
        {
            Wait.Until(c => GetWebElements(Element).Count < 1);
        }

        internal void WaitForElementNotVisible(string id, int seconds)
        {
            try
            {
                var _wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
                _wait.Until(driver1 => !GetElementVisibilityByID(id));
                Console.WriteLine("Element is not visible..");
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Element is still visible...");
            }
        }

        internal void WaitForElementNotVisible(By Element, int seconds)
        {
            try
            {
                var _wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
                _wait.Until(driver1 => !GetElementVisibility(Element));
                Console.WriteLine("Element is not visible..");
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Element is still visible...");
            }
        }

        internal void WaitForElementVisible(By Element)
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Element));
            Wait.Until(c => c.FindElement(Element).Displayed);
        }

        internal void WaitForElementToBeClickable(By Element)
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Element));
            Wait.Until(ExpectedConditions.ElementToBeClickable(Element));
            Wait.Until(c => c.FindElement(Element));
        }


        


        /// <summary>
        /// Validate the visibility of the given element using it´s id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool GetElementVisibilityByID(string id)
        {
            bool flag;
            try
            {
                flag = driver.FindElement(By.Id(id)).Displayed;
            }
            catch (NoSuchElementException)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Validate the visibility of the given element using it´s XPath 
        /// </summary>
        /// <param name="Xpath"></param>
        /// <returns></returns>
        internal bool GetElementVisibilityByXPath(string Xpath)
        {
            bool flag;
            try
            {
                flag = driver.FindElement(By.XPath(Xpath)).Displayed;
            }
            catch (NoSuchElementException)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Validate the visibility of the given element
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        internal bool GetElementVisibility(By Element)
        {
            bool flag;
            try
            {
                flag = driver.FindElement(Element).Displayed;
            }
            catch (NoSuchElementException)
            {
                flag = false;
            }
            catch (StaleElementReferenceException)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Get the first IWebElement that can be found ising the By mechanism 
        /// </summary>
        /// <param name="Element"></param>
        /// <returns></returns>
        internal IWebElement GetWebElement(By Element)
        {
            return driver.FindElement(Element);
        }

        internal ReadOnlyCollection<IWebElement> GetWebElements(By Element)
        {
            return driver.FindElements(Element);
        }

        internal string GetCurrentFrameName()
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            return (string)jsExecutor.ExecuteScript("return window.frameElement.id");
        }

        /// <summary>
        /// Execute the javascript query "document.getElementById('ELEMENTID').value"
        /// </summary>
        /// <returns></returns>
        internal void ValidateElementValueByJavascript(string ElementId, string ExpectedValue)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            var elementValue = (string)jsExecutor.ExecuteScript("return document.getElementById('" + ElementId + "').value");

            if (elementValue != ExpectedValue)
                throw new Exception("Failed to validate the element value. Expected: " + ExpectedValue + " || Actual: " + elementValue + "");
        }


        internal void ValidateElementAttributeValue(By Element, string AttributeName, string ExpectedAttributeValue)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute(AttributeName);

            if (ExpectedAttributeValue != attributeValue)
            {
                throw new Exception("Failed to validate the attribute value. Expected: " + ExpectedAttributeValue + " || Actual: " + attributeValue + "");
            }
        }





        internal void Click(By Element)
        {
            int count = 0;
            while (count < 3)
            {
                try
                {
                    this.driver.FindElement(Element).Click();
                    return;
                }
                catch (OpenQA.Selenium.ElementClickInterceptedException ex)
                {
                    count++;

                    if (count == 3)
                        throw ex; // if after 3 tries we still get the same exception then lets throw it and stop trying to click

                    Thread.Sleep(1000);
                }
                catch (OpenQA.Selenium.ElementNotInteractableException ex)
                {
                    count++;

                    if (count == 3)
                        throw ex; // if after 3 tries we still get the same exception then lets throw it and stop trying to click

                    Thread.Sleep(1000);
                }
            }
            
        }

        internal void ScrollToElement(By Element)
        {
            var element = driver.FindElement(Element);
            OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        internal void MoveToElementInPage(By Element)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            IWebElement Elem = driver.FindElement(Element);
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView();", Elem);
        }

        internal void MoveToElementInPage(string ElementId)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("var scrollDiv = document.getElementById('"+ ElementId + "').offsetTop; window.scrollTo({ top: scrollDiv, behavior: 'smooth'});");
        }

        internal void SendKeys(By Element, string Keys)
        {
            System.Threading.Thread.Sleep(500);
            this.driver.FindElement(Element).Clear();
            this.driver.FindElement(Element).SendKeys(Keys);
        }

        internal void SendKeysWithoutClearing(By Element, string Keys)
        {
            System.Threading.Thread.Sleep(500);
            this.driver.FindElement(Element).SendKeys(Keys);
        }

        internal void SelectPicklistElementByText(By Element, string ElementTextToSelect)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);
            se.SelectByText(ElementTextToSelect);
        }

        internal void SelectPicklistElementByValue(By Element, string ElementValueToSelect)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);
            se.SelectByValue(ElementValueToSelect);
        }

        internal void SetElementAttribute(string ElementId, string attributeName, string attributeValue)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('" + ElementId + "').setAttribute('" + attributeName + "', '" + attributeValue + "')");
        }

        internal void SetElementDisplayStyleToInlineById(string ElementId)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('" + ElementId + "').style.display='inline'");
            js.ExecuteScript("document.getElementById('" + ElementId + "').type='text'");
        }

        internal void SetElementDisplayStyleToInline(string ElementName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementsByName('" + ElementName + "')[0].style.display='inline'");
        }
        internal void SetElementDisplayStyleToInline(string ElementName, int ResultToSelect)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementsByName('" + ElementName + "')[" + ResultToSelect + "].style.display='inline'");
        }

        internal void SetElementVisibilityStyleToVisible(string ElementName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementsByName('" + ElementName + "')[0].style.visibility='visible'");
        }



        internal void ValidateElementCSSPropertyValue(By Element, string cssPropertyName, string ExpectedValue)
        {
            var element = this.driver.FindElement(Element);
            var propertyValue = element.GetCssValue(cssPropertyName);
            
            if(propertyValue != ExpectedValue)
                throw new Exception("Failed to validate the css element value. Expected: " + ExpectedValue + " || Actual: " + propertyValue + "");
        }

        internal void ValidateElementText(By Element, string ExpectedText)
        {
            string elementText = this.driver.FindElement(Element).Text;
            if (ExpectedText != elementText)
            {
                throw new Exception("Failed to validate the element text. Expected: " + ExpectedText + " || Actual: " + elementText + "");
            }
        }

        internal void ValidateElementValue(By Element, string ExpectedValue)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute("value");
            if (ExpectedValue != attributeValue)
            {
                throw new Exception("Failed to validate the element value. Expected: " + ExpectedValue + " || Actual: " + attributeValue + "");
            }
        }

        /// <summary>
        /// Validate that the supplied element (e.g. Radio Button) have the "cheked" property set to true
        /// </summary>
        /// <param name="Element"></param>
        internal void ValidateElementChecked(By Element)
        {
            string attributeValue = driver.FindElement(Element).GetAttribute("checked");
            Assert.AreEqual("true", attributeValue);
        }

        /// <summary>
        /// Validate that the supplied element (e.g. Radio Button) have the "cheked" property NOT set to true
        /// </summary>
        /// <param name="Element"></param>
        internal void ValidateElementNotChecked(By Element)
        {
            string attributeValue = driver.FindElement(Element).GetAttribute("checked");
            Assert.AreNotEqual("true", attributeValue);
        }

        internal void ValidatePicklistSelectedText(By Element, string ExpectedSelectedText)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);

            Assert.AreEqual(ExpectedSelectedText, se.SelectedOption.Text);
        }

        internal void ValidatePicklistContainsElementByText(By Element, string TextToFind)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);

            bool optionExists = se.Options.Any(c => c.Text == TextToFind);

            Assert.IsTrue(optionExists);
        }

        internal void ValidatePicklistDoesNotContainsElementByText(By Element, string TextToFind)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);

            bool optionExists = se.Options.Any(c => c.Text == TextToFind);

            Assert.IsFalse(optionExists);
        }

        internal void ValidatePicklistContainsElementByValue(By Element, string ValueToFind)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);

            bool optionExists = se.Options.Any(c => c.GetAttribute("value") == ValueToFind);

            Assert.IsTrue(optionExists);
        }

        internal void ValidatePicklistDoesNotContainsElementByValue(By Element, string ValueToFind)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);

            bool optionExists = se.Options.Any(c => c.GetAttribute("value") == ValueToFind);

            Assert.IsFalse(optionExists);
        }


        /// <summary>
        /// Validate that no element for the given "By" mechanism
        /// </summary>
        /// <param name="Element"></param>
        internal void ValidateElementDoNotExist(By Element)
        {
            bool anyElementFound = driver.FindElements(Element).Any();
            Assert.IsFalse(anyElementFound);
        }

        /// <summary>
        /// Validate that the element is marked as disabled
        /// </summary>
        /// <param name="Element"></param>
        internal void ValidateElementDisabled(By Element)
        {
            string disabledAttribute = driver.FindElement(Element).GetAttribute("disabled");
            bool elementDisabled = disabledAttribute == "true" ? true : false;

            Assert.IsTrue(elementDisabled, "Assert.IsTrue failed: " + Element.ToString());
        }

        /// <summary>
        /// Validate that the element is marked as disabled
        /// </summary>
        /// <param name="Element"></param>
        internal void ValidateElementEnabled(By Element)
        {
            string disabledAttribute = driver.FindElement(Element).GetAttribute("disabled");
            bool elementDisabled = disabledAttribute == "true" ? true : false;

            Assert.IsFalse(elementDisabled);
        }

        internal string GetElementText(By Element)
        {
            System.Threading.Thread.Sleep(500);
            return this.driver.FindElement(Element).Text;
        }

    }
}
