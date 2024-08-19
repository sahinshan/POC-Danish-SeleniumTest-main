using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WorkflowTestFramework
{
    public static class ExtensionMethods
    {
        public static DateTime WithoutMilliseconds(this System.DateTime TheDate)
        {
            return DateTime.Parse(TheDate.ToString("dd/MM/yyyy HH:mm:ss"));
        }
    }
}
