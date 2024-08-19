using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Phoenix.UITests
{
    [TestClass]
    public class TestNameGenerator
    {
        [TestMethod()]
        public void GetAllTestNamesAndDescriptions()
        {
            StringBuilder sb = new StringBuilder();

            Assembly UITestsAssembly = this.GetType().Assembly;

            Type[] types = UITestsAssembly.GetTypes().Where(c => c.FullName.StartsWith("Phoenix.UITests")).ToArray();


            foreach (Type t in types)
            {
                if (t.CustomAttributes.Count() == 0 || t.GetCustomAttributes(typeof(TestClassAttribute), true).Count() == 0)
                    continue;

                sb.AppendLine("Type Name: " + t.FullName);


                MethodInfo[] mi = t.GetMethods();

                foreach (MethodInfo method in mi)
                {

                    if (method.GetCustomAttributes(typeof(TestMethodAttribute), true).Any())
                    {
                        DescriptionAttribute descriptionAttribute = (DescriptionAttribute)method.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
                        if (descriptionAttribute != null)
                            sb.AppendLine("TestName: " + method.Name + " || Description: " + descriptionAttribute.Description);
                        else
                            sb.AppendLine("TestName: " + method.Name + " || Description: NO DESCRIPTION AVAILABLE");
                    }
                }
            }
            Console.WriteLine(sb.ToString());
        }

        [TestMethod()]
        public void GetAllTestNamesAndJiraIds()
        {
            StringBuilder sb = new StringBuilder();
            System.Collections.Generic.List<string> allJiraids = new System.Collections.Generic.List<string>();

            Assembly UITestsAssembly = this.GetType().Assembly;

            Type[] types = UITestsAssembly.GetTypes().Where(c => c.FullName.StartsWith("Phoenix.UITests")).OrderBy(z => z.Name).ToArray();


            foreach (Type t in types)
            {
                if (t.CustomAttributes.Count() == 0 || t.GetCustomAttributes(typeof(TestClassAttribute), true).Count() == 0)
                    continue;

                MethodInfo[] mi = t.GetMethods();

                foreach (MethodInfo method in mi)
                {

                    if (method.GetCustomAttributes(typeof(TestMethodAttribute), true).Any())
                    {
                        var allProperties = method.GetCustomAttributes<TestPropertyAttribute>(true).ToList();
                        var hasJiraId = allProperties.Where(c => c.Name.ToLower().Contains("jiraissueid")).Any();

                        if (hasJiraId)
                        {
                            string jiraIdToAdd = allProperties.Where(c => c.Name.ToLower().Contains("jiraissueid")).FirstOrDefault().Value;

                            if (!allJiraids.Contains(jiraIdToAdd))
                                allJiraids.Add(jiraIdToAdd);
                        }
                    }
                }
            }

            foreach (var jiraId in allJiraids)
                sb.AppendLine(jiraId);

            Console.WriteLine(sb.ToString());

        }

        [TestMethod()]
        public void GetAllTestNamesWithoutJiraIds()
        {
            StringBuilder sb = new StringBuilder();

            Assembly UITestsAssembly = this.GetType().Assembly;

            Type[] types = UITestsAssembly.GetTypes().Where(c => c.FullName.StartsWith("Phoenix.UITests")).OrderBy(z => z.Name).ToArray();


            foreach (Type t in types)
            {
                if (t.CustomAttributes.Count() == 0 || t.GetCustomAttributes(typeof(TestClassAttribute), true).Count() == 0)
                    continue;

                MethodInfo[] mi = t.GetMethods();

                foreach (MethodInfo method in mi)
                {

                    if (method.GetCustomAttributes(typeof(TestMethodAttribute), true).Any())
                    {
                        var allProperties = method.GetCustomAttributes<TestPropertyAttribute>(true).ToList();
                        var hasJiraId = allProperties.Where(c => c.Name.ToLower().Contains("jiraissueid")).Any();

                        if (!hasJiraId)
                        {
                            if (!method.Name.Equals("GetTestNames"))
                                sb.AppendLine(method.Name + "," + t.Name);
                        }
                    }
                }
                sb.AppendLine();
            }


            Console.WriteLine(sb.ToString());

        }

        [TestMethod()]
        public void GetAllTestNamesWithEmptyJiraIds()
        {
            StringBuilder sb = new StringBuilder();

            Assembly UITestsAssembly = this.GetType().Assembly;

            Type[] types = UITestsAssembly.GetTypes().Where(c => c.FullName.StartsWith("Phoenix.UITests")).OrderBy(z => z.Name).ToArray();


            foreach (Type t in types)
            {
                if (t.CustomAttributes.Count() == 0 || t.GetCustomAttributes(typeof(TestClassAttribute), true).Count() == 0)
                    continue;

                MethodInfo[] mi = t.GetMethods();

                foreach (MethodInfo method in mi)
                {

                    if (method.GetCustomAttributes(typeof(TestMethodAttribute), true).Any())
                    {
                        var allProperties = method.GetCustomAttributes<TestPropertyAttribute>(true).ToList();
                        var hasJiraId = allProperties.Where(c => c.Name.ToLower().Contains("jiraissueid")).Any();

                        if (hasJiraId)
                        {
                            TestPropertyAttribute attr = allProperties.Where(c => c.Name.ToLower().Contains("jiraissueid")).First();

                            if (string.IsNullOrEmpty(attr.Value))
                                sb.AppendLine(method.Name + "," + t.Name);
                        }
                    }
                }
                sb.AppendLine();
            }


            Console.WriteLine(sb.ToString());

        }

    }
}
