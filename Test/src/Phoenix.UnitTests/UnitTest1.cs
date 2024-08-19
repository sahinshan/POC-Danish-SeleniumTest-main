using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Phoenix.UnitTests
{
    [TestClass]
    public class TestNameGenerator
    {
        [TestMethod()]
        public void GetAllTestNamesAndDescriptions()
        {
            StringBuilder sb = new StringBuilder();

            Assembly UITestsAssembly = this.GetType().Assembly;

            Type[] types = UITestsAssembly.GetTypes().Where(c => c.FullName.StartsWith("Phoenix.UnitTests")).ToArray();


            foreach (Type t in types)
            {
                if (t.CustomAttributes.Count() == 0 || t.GetCustomAttributes(typeof(TestClassAttribute), true).Count() == 0)
                    continue;

                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine();
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

        }
    }
}
