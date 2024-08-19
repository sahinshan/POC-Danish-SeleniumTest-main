using AtlassianServiceAPI.Models;
using AtlassianServicesAPI;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JiraZephyr.Console
{
    class Program
    {
        public static string GetQSH(string qstring)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new StringBuilder();
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(qstring), 0, Encoding.UTF8.GetByteCount(qstring));
            foreach (var theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        static void Main(string[] args)
        {
            Zapi zapi = new Zapi();
            zapi.AccessKey = "amlyYToxNTAzMTExMyA1ZTczODgwYTNlNDY0OTBjMzgwMWNiOTUgVVNFUl9ERUZBVUxUX05BTUU";
            zapi.SecretKey = "dQWP7wYjw3yelZgZTfQKYVVMS1SAOfBsudChK8OVDqQ";
            //zapi.User = "jose.brazeta@oneadvanced.com";
            zapi.User = "5e73880a3e46490c3801cb95";
            
            JiraApi jiraAPI = new JiraApi();
            jiraAPI.Authentication = "Basic am9zZS5icmF6ZXRhQG9uZWFkdmFuY2VkLmNvbTplSldaSDhMZjU0dUs1UWlaWHRpczRFOTE=";
            jiraAPI.JiraCloudUrl = "https://advancedcsg.atlassian.net/";
            jiraAPI.ProjectKey = "CDV6";

            AtlassianService atlassianService = new AtlassianService(zapi, jiraAPI);


            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            List<string> jiraIDS = new List<string>();

            //jiraIDS.AddRange(System.IO.File.ReadAllLines(currentDirectory + "\\JiraIds\\Phoenix_Workflow_Tests.txt").ToList());
            jiraIDS.AddRange(System.IO.File.ReadAllLines(currentDirectory + "\\JiraIds\\Phoenix_Portal_UI_Tests.txt").ToList());
            //jiraIDS.AddRange(System.IO.File.ReadAllLines(currentDirectory + "\\JiraIds\\Phoenix_UI_Tests.txt").ToList());
            //jiraIDS.AddRange(System.IO.File.ReadAllLines(currentDirectory + "\\JiraIds\\Phoenix_Mobile_UI_Tests.txt").ToList());



            foreach (var jiraID in jiraIDS)
            {
                try
                {
                    atlassianService.AddTestCaseToCycle(jiraID, "Automated Testing Portal", "6.2.9", atlassianService.ProjectId);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            

        }

    }
}
