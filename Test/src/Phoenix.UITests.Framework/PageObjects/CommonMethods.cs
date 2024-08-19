using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Runtime.CompilerServices;
using static UglyToad.PdfPig.PdfFonts.FontDescriptor;

namespace Phoenix.UITests.Framework.PageObjects
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

        internal string GetCurrentWindowTitle(string Window)
        {
            return driver.SwitchTo().Window(Window).Title;
        }

        internal void SwitchToIframe(By IframeElement)
        {
            driver.SwitchTo().Frame(driver.FindElement(IframeElement));
        }

        internal void SwitchToDefaultFrame()
        {
            try
            {
                this.driver.SwitchTo().DefaultContent();
            }
            catch (UnhandledAlertException)
            {
                this.driver.SwitchTo().DefaultContent();
            }
        }


        internal void WaitForBrowserURL(string ExpectedURL)
        {
            Wait.Until(c => c.Url == ExpectedURL);
        }

        internal void WaitForBrowserWindowTitle(string ExpectedTitle)
        {
            Wait.Until(c => c.Title == ExpectedTitle);
        }

        internal void NavigateToBrowserURL(string URL)
        {
            driver.Navigate().GoToUrl(URL);
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
            Wait.Timeout = new TimeSpan(0, 0, Seconds);
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
            catch (WebDriverTimeoutException ex)
            {
                throw new Exception("Element is still visible..." + ex.ToString());
            }
            catch (Exception e) { }
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
                throw new Exception("Element is still visible..." + Element.ToString());
            }
        }

        internal void WaitForElementVisible(By Element)
        {
            int i = 0;
            while (i < 10)
            {
                try
                {
                    Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Element));
                    Wait.Until(c => c.FindElement(Element).Displayed);
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    i = i = 1;
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        /// <summary>
        /// this method will not throw an exception if the element is not visible
        /// </summary>
        /// <param name="Element"></param>
        internal bool WaitForElementVisibleWithoutException(By Element, int timeoutSeconds)
        {
            WebDriverWait internalWait = new WebDriverWait(this.driver, new TimeSpan(0, 0, timeoutSeconds));
            int i = 0;
            while (i < 10)
            {
                try
                {
                    internalWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Element));
                    internalWait.Until(c => c.FindElement(Element).Displayed);

                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    i = i = 1;
                    System.Threading.Thread.Sleep(100);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        internal void WaitForElementWithSizeVisible(By Element)
        {


            int i = 0;
            while (i < 10)
            {
                try
                {
                    Wait.Until(c => c.FindElements(Element).Where(x => x.Size.Height > 0 && x.Size.Width > 0));

                    return;
                }
                catch (StaleElementReferenceException)
                {
                    i = i = 1;
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        internal void WaitForElementToBeClickable(By Element, bool RecursionActivated = false)
        {
            try
            {
                System.Threading.Thread.Sleep(100);
                Wait.Until(ExpectedConditions.ElementExists(Element));
                System.Threading.Thread.Sleep(100);
                Wait.Until(ExpectedConditions.ElementIsVisible(Element));
                System.Threading.Thread.Sleep(100);
                Wait.Until(ExpectedConditions.ElementToBeClickable(Element));
                System.Threading.Thread.Sleep(100);
                Wait.Until(c => c.FindElement(Element));
                System.Threading.Thread.Sleep(100);
            }
            catch (StaleElementReferenceException)
            {
                if (!RecursionActivated) //if the recursion was never activated we will wait for 1 second and will try to wait for the element again.
                {
                    System.Threading.Thread.Sleep(1000);
                    WaitForElementToBeClickable(Element, true);
                }
            }
        }



        internal bool CheckIfElementExists(By Element)
        {
            return driver.FindElements(Element).Any();
        }

        internal int CountElements(By Element)
        {
            return driver.FindElements(Element).Count();
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



        internal void Click(By Element)
        {
            WaitForElementToBeClickable(Element);
            try
            {
                this.driver.FindElement(Element).Click();
            }
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
                System.Threading.Thread.Sleep(1000);
                this.driver.FindElement(Element).Click();
            }
            catch (OpenQA.Selenium.ElementClickInterceptedException)
            {
                System.Threading.Thread.Sleep(3000);
                this.driver.FindElement(Element).Click();
            }
        }

        internal void ClickWithoutWaiting(By Element)
        {
            try
            {
                this.driver.FindElement(Element).Click();
            }
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
                System.Threading.Thread.Sleep(1000);
                this.driver.FindElement(Element).Click();
            }
        }

        internal void RightClick(By Element)
        {
            WaitForElementToBeClickable(Element);
            try
            {
                // Find the element on which to perform the right-click
                IWebElement element = this.driver.FindElement(Element);

                // Create an Actions object
                Actions actions = new Actions(driver);

                // Perform right-click action on the element
                actions.ContextClick(element).Build().Perform();
            }
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
                System.Threading.Thread.Sleep(1000);
                this.driver.FindElement(Element).Click();
            }
        }

        internal void ClickByJavascript(By Element)
        {
            var element = driver.FindElement(Element);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
            System.Threading.Thread.Sleep(500);
        }

        internal void ClickOnElementWithSize(By Element)
        {
            var webElement = this.driver.FindElements(Element).Where(x => x.Size.Height > 0 && x.Size.Width > 0).FirstOrDefault();

            int totalTries = 0;
            while (totalTries < 10)
            {
                if (webElement == null)
                {
                    totalTries++;
                    System.Threading.Thread.Sleep(1000);
                    webElement = this.driver.FindElements(Element).Where(x => x.Size.Height > 0 && x.Size.Width > 0).FirstOrDefault();
                }
                else
                {
                    webElement.Click();
                    return;
                }
            }

            throw new Exception("Unable to find the element " + Element.ToString());
        }

        internal void ClickAndDragAndRelese(By Element, int offsetX, int offsetY)
        {
            var drawingCanvasWebElement = GetWebElement(Element);

            OpenQA.Selenium.Interactions.Actions builder = new OpenQA.Selenium.Interactions.Actions(driver);

            builder
                .MoveToElement(drawingCanvasWebElement)
                .ClickAndHold(drawingCanvasWebElement)
                .MoveByOffset(offsetX, offsetY)
                .Release(drawingCanvasWebElement)
                .Perform();
        }

        internal void ClickAndDrag(By Element, int offsetX, int offsetY)
        {
            var drawingCanvasWebElement = GetWebElement(Element);

            OpenQA.Selenium.Interactions.Actions builder = new OpenQA.Selenium.Interactions.Actions(driver);

            var dragAction = builder
                .MoveToElement(drawingCanvasWebElement)
                .ClickAndHold(drawingCanvasWebElement)
                .MoveByOffset(offsetX, offsetY)
                .Click()
                .Build();

            dragAction.Perform();

            System.Threading.Thread.Sleep(2000);

        }

        internal void ClickSpecial(By Element)
        {
            var subMenu = driver.FindElement(Element);
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(subMenu);
            actions.Click().Build().Perform();
        }

        internal void ScrollToElement(By Element, int NumberOfTries = 1)
        {
            try
            {
                var element = driver.FindElement(Element);
                Actions actions = new Actions(driver);
                actions.MoveToElement(element);
                actions.Perform();
            }
            catch (StaleElementReferenceException ex)
            {
                if (NumberOfTries >= 10)
                    throw ex;

                System.Threading.Thread.Sleep(500);
                ScrollToElement(Element, NumberOfTries + 1);
            }
        }

        internal void ScrollToElementByPosition(By Element)
        {
            IWebElement Elem = driver.FindElement(Element);
            if (Elem.Location.Y > 200)
                ScrollTo(0, Elem.Location.Y - 100);
        }

        public void ScrollTo(int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            ((IJavaScriptExecutor)driver).ExecuteScript(js);
        }


        internal void ScrollToElementViaJavascript(By Element)
        {
            var element = driver.FindElement(Element);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            System.Threading.Thread.Sleep(500);
        }

        internal void SendKeys(By Element, string Keys)
        {
            System.Threading.Thread.Sleep(500);
            this.driver.FindElement(Element).Clear();
            this.driver.FindElement(Element).SendKeys(Keys);
        }

        internal void SendKeysViaJavascript(By Element, string ElementId, string Keys)
        {
            System.Threading.Thread.Sleep(500);
            this.driver.FindElement(Element).Clear();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('" + ElementId + "').setAttribute('value', '" + Keys + "')");
        }

        internal void SetAttributeValue(By Element, string AttributeName, string AttributeValue)
        {
            var element = driver.FindElement(Element);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].setAttribute('" + AttributeName + "', '" + AttributeValue + "')", element);
        }

        internal void SendKeysViaJavascript(string ElementId, string Keys)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('" + ElementId + "').value = '" + Keys + "'");
        }

        internal void ClearText(By Element)
        {
            System.Threading.Thread.Sleep(500);
            this.driver.FindElement(Element).Clear();
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

        internal void SetElementDisplayStyleToInline(string ElementName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementsByName('" + ElementName + "')[0].style.display='inline'");
        }

        internal void ChangeColorInputLineColorProperty(string InputId, string LineColor)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('" + InputId + "').value = '" + LineColor + "'");
            js.ExecuteScript("document.getElementById('" + InputId + "').onchange()");
        }

        internal void ClearInputElementViaJavascript(string InputId)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('" + InputId + "').value = ''");
        }

        internal void SetElementVisibilityStyleToVisible(string ElementName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementsByName('" + ElementName + "')[0].style.visibility='visible'");
        }

        internal void SetElementVisibilityStyleToVisibleByElementId(string ElementId)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('" + ElementId + "').style.display='inline'");
        }

        internal string GetElementTextByJavascript(By Element)
        {
            var element = driver.FindElement(Element);
            string text = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].innerText", element);
            System.Threading.Thread.Sleep(500);
            return text;
        }

        internal void ValidateElementText(By Element, string ExpectedText)
        {
            string elementText = "";

            int i = 0;
            while (i < 10)
            {
                try
                {
                    elementText = this.driver.FindElement(Element).Text;
                    //return;
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    i = i + 1;
                    System.Threading.Thread.Sleep(500);
                }
            }

            Console.WriteLine(elementText);
            if (ExpectedText != elementText)
            {
                throw new Exception("Failed to validate the element text. Expected: " + ExpectedText + " || Actual: " + elementText + "");
            }
        }

        internal void ValidateElementTextContainsText(By Element, string ExpectedText)
        {
            string elementText = this.driver.FindElement(Element).Text;
            if (!elementText.Contains(ExpectedText))
            {
                throw new Exception("Failed to validate the element text. Expected: " + ExpectedText + " || Actual: " + elementText + "");
            }
        }

        internal void WaitForElementToContainText(By Element, string ExpectedText)
        {
            int i = 0;
            while (i < 10)
            {
                try
                {
                    Wait.Until(c => c.FindElement(Element).Text.Contains(ExpectedText));
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    i = i + 1;
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        internal void ValidateElementValue(By Element, string ExpectedValue, int LoopFactor = 1)
        {
            IWebElement webElement = this.driver.FindElement(Element);

            string attributeValue = "";

            try
            {
                attributeValue = webElement.GetAttribute("value");
            }
            catch (StaleElementReferenceException ex)
            {
                if (LoopFactor > 10)
                    throw ex;

                System.Threading.Thread.Sleep(1000);
                ValidateElementValue(Element, ExpectedValue, LoopFactor + 1);
            }

            if (ExpectedValue != attributeValue)
            {
                throw new Exception("Failed to validate the element value. Expected: " + ExpectedValue + " || Actual: " + attributeValue + "");
            }
        }

        internal void ValidateElementValueNotEqual(By Element, string ExpectedValue)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute("value");
            if (ExpectedValue == attributeValue)
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
            int i = 0;
            string attributeValue = "";
            while (i < 10)
            {
                try
                {
                    attributeValue = driver.FindElement(Element).GetAttribute("checked");
                    Assert.AreEqual("true", attributeValue);
                    return;
                }
                catch (StaleElementReferenceException ex)
                {
                    i++;
                    if (i >= 10)
                        throw ex;
                }
            }
            return;
        }

        internal void ValidateElementCheckedUsingJavaScript(string Element)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            var elementValue = (string)jsExecutor.ExecuteScript("return document.querySelector('input[id = " + Element + "]:checked')");

            Assert.IsNotNull(elementValue);
        }

        /// <summary>
        /// Validate that the supplied element (e.g. Radio Button) have the "cheked" property NOT set to true
        /// </summary>
        /// <param name="Element"></param>

        internal string ValidateElementToolTip(By Element, string Attribute)
        {
            string attributeValue = driver.FindElement(Element).GetAttribute(Attribute);

            var element = driver.FindElement(Element);
            OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
            System.Threading.Thread.Sleep(2000);
            return attributeValue;
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

            if (string.IsNullOrEmpty(ExpectedSelectedText))
            {
                if (se.AllSelectedOptions.Count == 0)
                    return;
            }

            try
            {
                Assert.AreEqual(ExpectedSelectedText, se.SelectedOption.Text);
            }
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
                System.Threading.Thread.Sleep(2000);
                webElement = this.driver.FindElement(Element);
                se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);
                Assert.AreEqual(ExpectedSelectedText, se.SelectedOption.Text);
            }
        }

        internal void ValidatePicklistOptionDisabled(By Element, string OptionText, bool ExpectedDisabled)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(webElement);
            var picklistOption = selectElement.Options.Where(c => c.Text.Equals(OptionText)).FirstOrDefault();
            string disabledAttribute = picklistOption.GetAttribute("disabled");
            bool elementDisabled = disabledAttribute == "true" ? true : false;

            Assert.AreEqual(elementDisabled, ExpectedDisabled);
        }

        internal void ValidatePicklistContainsElementByText(By Element, string TextToFind)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);

            bool optionExists = se.Options.Any(c => c.Text == TextToFind);

            Assert.IsTrue(optionExists, "Option '" + TextToFind + "' does not exist in the element");
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

            Assert.IsTrue(elementDisabled);
        }

        internal void ValidateElementNotDisabled(By Element)
        {
            string disabledAttribute = driver.FindElement(Element).GetAttribute("disabled");
            bool elementDisabled = disabledAttribute == "true" ? true : false;

            Assert.IsFalse(elementDisabled);
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

        internal void ValidateElementReadonly(By Element)
        {
            string readonlyAttribute = driver.FindElement(Element).GetAttribute("readonly");
            bool elementReadonly = readonlyAttribute == "true" ? true : false;

            Assert.IsTrue(elementReadonly);
        }

        internal void ValidateElementNotReadonly(By Element)
        {
            string readonlyAttribute = driver.FindElement(Element).GetAttribute("readonly");
            bool elementReadonly = readonlyAttribute == "true" ? true : false;

            Assert.IsFalse(elementReadonly);
        }

        internal bool IsElementAttributePresent(By Element, string AttributeName)
        {
            string attributeValue = driver.FindElement(Element).GetAttribute(AttributeName);
            if (attributeValue != null)
            {
                return true;
            }
            else
            {
                return false;
            }

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

        internal string GetElementValueByJavascript(string ElementId)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            var elementValue = (string)jsExecutor.ExecuteScript("return document.getElementById('" + ElementId + "').value");

            return elementValue;
        }

        internal string GetElementTextByJavascript(string ElementId)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            var elementValue = (string)jsExecutor.ExecuteScript("return document.getElementById('" + ElementId + "').textContent");

            return elementValue;
        }

        /// <summary>
        /// Execute the javascript query "document.getElementById('ELEMENTID').checked"
        /// </summary>
        /// <returns></returns>
        internal void ValidateElementCheckedByJavascript(string ElementId, bool ExpectedChecked)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            bool elementChecked = (bool)jsExecutor.ExecuteScript("return document.getElementById('" + ElementId + "').checked");

            Assert.AreEqual(ExpectedChecked, elementChecked);
        }


        internal void ValidateElementMaxLength(By Element, string ExpectedValue)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute("maxlength");
            if (ExpectedValue != attributeValue)
            {
                throw new Exception("Failed to validate the element value. Expected: " + ExpectedValue + " || Actual: " + attributeValue + "");
            }
        }

        internal void ValidateElementAttribute(By Element, string AttributeName, string ExpectedAttributeValue)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute(AttributeName);
            if (!attributeValue.Contains(ExpectedAttributeValue))
            {
                throw new Exception("Failed to validate the element value. Expected: " + ExpectedAttributeValue + " || Actual: " + attributeValue + "");
            }
        }


        internal void ValidateElementMinLength(By Element, string ExpectedValue)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute("minlength");
            if (ExpectedValue != attributeValue)
            {
                throw new Exception("Failed to validate the element value. Expected: " + ExpectedValue + " || Actual: " + attributeValue + "");
            }
        }


        internal void ValidateElementFileType(By Element, string ExpectedValue)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute("extension");
            if (ExpectedValue != attributeValue)
            {
                throw new Exception("Failed to validate the element value. Expected: " + ExpectedValue + " || Actual: " + attributeValue + "");
            }
        }


        internal string GetElementText(By Element)
        {
            System.Threading.Thread.Sleep(500);
            return this.driver.FindElement(Element).Text;
        }

        internal string GetElementValue(By Element)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute("value");
            return attributeValue;

        }

        internal void ValidateElementByTitle(By Element, string ExpectedValue)
        {
            string attributeValue = driver.FindElement(Element).GetAttribute("title");
            Assert.AreEqual(ExpectedValue, attributeValue);
        }

        internal void AcceptAlert()
        {
            try
            {
                Wait.Until(ExpectedConditions.AlertIsPresent());
                driver.SwitchTo().Alert().Accept();
            }
            catch (Exception e)
            {
                Console.WriteLine("There is No Alert Present." + e.ToString());

            }
        }

        internal string GetElementByAttributeValue(By element, string attribute)
        {
            string attributeValue = driver.FindElement(element).GetAttribute(attribute);
            return attributeValue;
        }

        internal string GetPicklistSelectedText(By Element)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            var se = new OpenQA.Selenium.Support.UI.SelectElement(webElement);

            return se.SelectedOption.Text;


        }

        internal int GetCountOfElements(By Element)
        {
            return GetWebElements(Element).Count;
        }

        internal void WindowsFormSendKeys(By element, string keys)
        {
            System.Threading.Thread.Sleep(5000);
            WaitForElement(element);
            ScrollToElement(element);
            Click(element);
            System.Windows.Forms.SendKeys.SendWait(keys);
            System.Windows.Forms.SendKeys.SendWait("{ENTER}");
            System.Threading.Thread.Sleep(3000);
        }

        internal void MoveToElementInPage(By Element)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            IWebElement Elem = driver.FindElement(Element);
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView();", Elem);
        }


        internal void ValidateElementIconSRC(By Element, string ExpectedValue)
        {
            IWebElement webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute("src");
            if (!attributeValue.Contains(ExpectedValue))
            {
                throw new Exception("Failed to validate the element value. Expected: " + ExpectedValue + " || Actual: " + attributeValue + "");
            }
        }

        internal void MouseHover(By Element)
        {
            var element = driver.FindElement(Element);
            OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
        }

        internal void OpenNewTabViaJavascript()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
        }

        internal void WaitForElementToBeDisable(By Element)
        {
            int i = 0;
            while (i < 10)
            {
                try
                {
                    if (!driver.FindElement(Element).Enabled)
                        break;
                    else
                    {
                        i = i + 1;
                        System.Threading.Thread.Sleep(1000);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Element Still Enabled : " + e.ToString());
                }
            }
        }

        internal void DragAndDropToTargetElement(By elementToDrag, By targetElement)
        {
            var element1 = driver.FindElement(elementToDrag);
            var element2 = driver.FindElement(targetElement);

            var builder = new Actions(driver);

            builder.DragAndDrop(element1, element2).Build().Perform();
        }

    }
}
