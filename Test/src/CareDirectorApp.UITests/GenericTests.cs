using System;
using System.Linq;
using System.Reflection;
using System.Text;
using CareDirectorApp.TestFramework;
using CareDirectorApp.TestFramework.PageObjects;
using NUnit.Framework;

namespace CareDirectorApp.UITests
{
    [TestFixture]
    public class GenericTests
    {

        [Test]
        public void GetAllMobileTestNamesAndJiraIds()
        {
            StringBuilder sb = new StringBuilder();
            System.Collections.Generic.List<string> allJiraids = new System.Collections.Generic.List<string>();

            Assembly UITestsAssembly = this.GetType().Assembly;

            Type[] types = UITestsAssembly.GetTypes().Where(c => c.FullName.StartsWith("CareDirectorApp")).OrderBy(z => z.Name).ToArray();


            foreach (Type t in types)
            {
                if (t.CustomAttributes.Count() == 0 || t.GetCustomAttributes(typeof(TestFixtureAttribute), true).Count() == 0)
                    continue;

                MethodInfo[] mi = t.GetMethods();

                foreach (MethodInfo method in mi)
                {

                    if (method.GetCustomAttributes(typeof(TestAttribute), true).Any())
                    {
                        var allProperties = method.GetCustomAttributes<PropertyAttribute>(true).ToList();
                        var hasJiraId = allProperties.Where(c => c.Properties["JiraIssueID"] != null).Any();

                        if (hasJiraId)
                        {
                            string jiraIdToAdd = (string)allProperties.Where(c => c.Properties["JiraIssueID"] != null).FirstOrDefault().Properties["JiraIssueID"];

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


    }
}
