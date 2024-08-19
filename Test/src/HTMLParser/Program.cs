using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;
using ExtensionMethods;

namespace HTMLParser
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var pageNamespace = "Phoenix.UITests.Framework.PageObjects";
            var className = "BookingPaymentRecordPage";
            var filePath = "C:\\Temp\\";
            var fileName = "Source1.htm";

            var validTags = new List<string>()
            {
                "input",
                "button",
                "textarea",
                "a",
                "select"
            };

            var listOfProperties = new List<string>();
            var listOfMethods = new List<string>();

            var options = new ChromeOptions();
            options.AddArguments("headless");

            using (var driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl(filePath + fileName);

                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);

                CloseAnyOpenPopup(driver);

                var elements = driver.FindElements(By.CssSelector("*"));

                Console.WriteLine("\n\n");

                foreach (var element in elements)
                {
                    try
                    {
                        if (element.Displayed && validTags.Contains(element.TagName))
                        {
                            var attributeId = element.GetAttribute("id");

                            if (attributeId.EndsWith("_cwname"))
                                continue;

                            var attributeName = attributeId;

                            if (attributeName.StartsWith("TI_"))
                                attributeName = attributeName.Replace("TI_", "");
                            if (attributeName.StartsWith("CWField_"))
                                attributeName = attributeName.Replace("CWField_", "");
                            if (attributeName.Contains("_Link"))
                                attributeName = attributeName.Replace("_Link", "Link");
                            if (attributeName.Contains("_DatePicker"))
                                attributeName = attributeName.Replace("_DatePicker", "DatePicker");
                            if (attributeName.StartsWith("CWLookupBtn_"))
                                attributeName = attributeName.Replace("CWLookupBtn_", "").AppendAnotherString("LookupButton");
                            if (attributeName.StartsWith("CWClearLookup_"))
                                attributeName = attributeName.Replace("CWClearLookup_", "").AppendAnotherString("ClearButton");
                            if (attributeName.Contains("_cwname"))
                                attributeName = attributeName.Replace("_cwname", "");
                            if (attributeName.StartsWith("ownerid"))
                                attributeName = attributeName.Replace("ownerid", "ResponsibleTeam");

                            if (!string.IsNullOrEmpty(attributeName))
                                attributeName = char.ToUpper(attributeName[0]) + attributeName.Substring(1);

                            var attributeType = element.GetAttribute("type");

                            if (!string.IsNullOrEmpty(attributeId))
                            {
                                var newProperty = "\t\treadonly By " + attributeName + " = By.XPath(\"//*[@id='" + attributeId + "']\");";
                                listOfProperties.Add(newProperty);

                                if (element.TagName.Equals("button"))
                                {
                                    #region button

                                    var newMethod = "\t\tpublic " + className + " Click" + attributeName + "()\r\n\t\t{\r\n\t\t\tWaitForElementToBeClickable(" + attributeName + ");\r\n\t\t\tClick(" + attributeName + ");\r\n\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    #endregion
                                }
                                else if (element.TagName.Equals("a"))
                                {
                                    #region a

                                    var newMethod = "\t\tpublic " + className + " Click" + attributeName + "()\r\n\t\t{\r\n\t\t\tWaitForElementToBeClickable(" + attributeName + ");\r\n\t\t\tClick(" + attributeName + ");\r\n\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    newMethod = "\t\tpublic " + className + " Validate" + attributeName + "Text(string ExpectedText)\r\n\t\t{\r\n\t\t\tWaitForElementToBeClickable(" + attributeName + ");\r\n\t\t\tValidateElementText(" + attributeName + ", ExpectedText);\r\n\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    #endregion
                                }
                                else if (element.TagName.Equals("input") && attributeType.Equals("text"))
                                {
                                    #region input (text)

                                    var newMethod = "\t\tpublic " + className + " Validate" + attributeName + "Text(string ExpectedText)\r\n\t\t{\r\n\t\t\tValidateElementValue(" + attributeName + ", ExpectedText);\r\n\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    newMethod = "\t\tpublic " + className + " InsertTextOn" + attributeName + "(string TextToInsert)\r\n\t\t{\r\n\t\t\tWaitForElementToBeClickable(" + attributeName + ");\r\n\t\t\tSendKeys(" + attributeName + ", TextToInsert);\r\n\t\t\t\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    #endregion
                                }
                                else if (element.TagName.Equals("input") && attributeType.Equals("radio"))
                                {
                                    #region input (radio)

                                    var newMethod = "\t\tpublic " + className + " Click" + attributeName + "()\r\n\t\t{\r\n\t\t\tWaitForElementToBeClickable(" + attributeName + ");\r\n\t\t\tClick(" + attributeName + ");\r\n\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    newMethod = "\t\tpublic " + className + " Validate" + attributeName + "Checked()\r\n\t\t{\r\n\t\t\tWaitForElement(" + attributeName + ");\r\n\t\t\tValidateElementChecked(" + attributeName + ");\r\n\t\t\t\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    newMethod = "\t\tpublic " + className + " Validate" + attributeName + "NotChecked()\r\n\t\t{\r\n\t\t\tWaitForElement(" + attributeName + ");\r\n\t\t\tValidateElementNotChecked(" + attributeName + ");\r\n\t\t\t\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    #endregion
                                }
                                else if (element.TagName.Equals("textarea"))
                                {
                                    #region textarea

                                    var newMethod = "\t\tpublic " + className + " Validate" + attributeName + "Text(string ExpectedText)\r\n\t\t{\r\n\t\t\tValidateElementText(" + attributeName + ", ExpectedText);\r\n\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    newMethod = "\t\tpublic " + className + " InsertTextOn" + attributeName + "(string TextToInsert)\r\n\t\t{\r\n\t\t\tWaitForElementToBeClickable(" + attributeName + ");\r\n\t\t\tSendKeys(" + attributeName + ", TextToInsert);\r\n\t\t\t\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    #endregion
                                }
                                else if (element.TagName.Equals("select"))
                                {
                                    #region select

                                    var newMethod = "\t\tpublic " + className + " Select" + attributeName + "(string TextToSelect)\r\n\t\t{\r\n\t\t\tWaitForElementToBeClickable(" + attributeName + ");\r\n\t\t\tSelectPicklistElementByText(" + attributeName + ", TextToSelect);\r\n\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    newMethod = "\t\tpublic " + className + " Validate" + attributeName + "SelectedText(string ExpectedText)\r\n\t\t{\r\n\t\t\tValidateElementText(" + attributeName + ", ExpectedText);\r\n\r\n\t\t\treturn this;\r\n\t\t}";
                                    listOfMethods.Add(newMethod);

                                    #endregion
                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("namespace " + pageNamespace + "\r\n{\r\n\tpublic class " + className + " : CommonMethods\r\n\t{\r\n\t\t");

                foreach (var prop in listOfProperties)
                {
                    sb.AppendLine(prop);
                }

                sb.AppendLine("");
                sb.AppendLine("");
                sb.AppendLine("");

                foreach (var method in listOfMethods)
                {
                    sb.AppendLine(method);
                    sb.AppendLine("");
                }

                sb.AppendLine("\t}\r\n}");

                Console.WriteLine(sb.ToString());

                System.IO.File.WriteAllText(filePath + className + ".cs", sb.ToString());

                Console.ReadKey();
            }
        }

        internal static void CloseAnyOpenPopup(WebDriver driver)
        {
            IAlert alertPopup = ExpectedConditions.AlertIsPresent().Invoke(driver);

            while (alertPopup != null)
            {
                alertPopup.Accept();
                alertPopup = ExpectedConditions.AlertIsPresent().Invoke(driver);
            }

            driver.SwitchTo().DefaultContent();
        }
    }
}
