using System;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static string AppendAnotherString(this string str, string NewString)
        {
            return str + NewString;
        }
    }
}


