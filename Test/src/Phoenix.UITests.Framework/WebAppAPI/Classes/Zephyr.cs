using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using RestSharp;


namespace Phoenix.UITests.Framework.WebAppAPI.Classes
{
    internal class Zephyr: IZephyr
    {

        string authorization = "JWT eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJqb3NlLmJyYXpldGFAb25lYWR2YW5jZWQuY29tIiwicXNoIjoiN2NkNGQ1MzMwNTJkMDczMDBiMWY0OGQ4NTNiMGZhMjk3N2I4ZGViNTdkYjU0OTMwMzRmMWU1M2Y0NjFlY2U4NCIsImlzcyI6ImFtbHlZVG94TlRBek1URXhNeUExWlRjek9EZ3dZVE5sTkRZME9UQmpNemd3TVdOaU9UVWdWVk5GVWw5RVJVWkJWVXhVWDA1QlRVVSIsImlhdCI6MTYwMDk2MTgxMTI1NCwiZXhwIjoxNjAwOTYxODE0ODU0fQ.mTxYjO8xyyBF7PE2eiGuUgJ-wjf6EAq9bcxAonThzjk";
        string zapiAccessKey = "amlyYToxNTAzMTExMyA1ZTczODgwYTNlNDY0OTBjMzgwMWNiOTUgVVNFUl9ERUZBVUxUX05BTUU";
        
        
        string cyclesURL = "cycles/search?versionId={0}&projectId={1}";
        string testExecutionURL = "execution/66e7c582-68ff-4d7d-9cec-0ad8404c094a";


        
        public string UpdateTestExecutionStatus(string issueID, string projectID, string versionID)
        {
            string baseURL = "https://prod-api.zephyr4jiracloud.com/connect/public/rest/api/1.0";

            var client = new RestClient(baseURL);

            var request = new RestRequest(testExecutionURL);

            request.AddOrUpdateParameter(new Parameter("Authorization", authorization, ParameterType.HttpHeader));
            request.AddOrUpdateParameter(new Parameter("zapiAccessKey", zapiAccessKey, ParameterType.HttpHeader));
            request.AddOrUpdateParameter(new Parameter("Content-Type", "application/json", ParameterType.HttpHeader));
            string jsonBody = "{\"status\":{\"id\":1},\"id\":\"66e7c582-68ff-4d7d-9cec-0ad8404c094a\",\"projectId\":" + projectID + ",\"issueId\":" + issueID + ",\"cycleId\":\"877bb870-84c0-4310-93cd-7cb0dcb29cda\",\"versionId\":" + versionID + "}";
            request.AddJsonBody(jsonBody);

            var response = client.Put(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Content;
        }

        public string GetTestCycleID(string projectID, string versionID)
        {
            string baseURL = "https://prod-api.zephyr4jiracloud.com/connect/public/rest/api/1.0/";

            var client = new RestClient(baseURL);

            cyclesURL = string.Format(cyclesURL, versionID, projectID);
            var request = new RestRequest(cyclesURL);

            request.AddOrUpdateParameter(new Parameter("Authorization", authorization, ParameterType.HttpHeader));
            request.AddOrUpdateParameter(new Parameter("zapiAccessKey", zapiAccessKey, ParameterType.HttpHeader));
            request.AddOrUpdateParameter(new Parameter("Content-Type", "text/plain", ParameterType.HttpHeader));

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(response.Content);

            return response.Content;
        }


    }
}
