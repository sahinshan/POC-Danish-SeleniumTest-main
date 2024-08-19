﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using AtlassianService.Models;
using Version = AtlassianService.Models.Version;
using AtlassianService.Interfaces;

namespace AtlassianService
{
    /// <summary>
    /// Jira API related methods
    /// </summary>
    public class JiraCloudApi : IJiraCloudApi
    {
        private const string Id = "id";
        private const string Key = "key";
        private const string Name = "name";
        private const string ProjectId = "projectId";

        private const string JiraSource = "/rest/api/2";


        private readonly RestClient _restClient;
        private readonly JiraApi _jiraApi;

        private Project _project;

        public JiraCloudApi(JiraApi jiraApi)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            
            _jiraApi = jiraApi;
            _restClient = new RestClient(jiraApi.JiraCloudUrl);
            _restClient.AddDefaultHeader("Authorization", jiraApi.Authentication);
        }

        public Project Project => _project ?? (_project = GetProject());

        public Project GetProject() => GetProject(_jiraApi.ProjectKey);

        public Project GetProject(string projectKey)
        {
            var source = $"{JiraSource}/project/{projectKey}";
            var response = SendHttpRequest(source, Method.Get);
            if (response == null) return null;

            return new Project
            {
                Id = (long)response[Id],
                Key = (string)response[Key],
                Name = (string)response[Name]
            };
        }

        public Project SetProject(string projectKey)
        {
            _jiraApi.ProjectKey = projectKey;
            _project = GetProject(projectKey);
            return _project;
        }

        public IList<Version> GetVersions()
        {
            var source = $"{JiraSource}/project/{_jiraApi.ProjectKey}/version?maxResults=500&orderBy=-sequence";
            var response = SendHttpRequest(source, Method.Get);
            if (response == null) return null;
            var jsonVersions = (JArray)response["values"];
            return (from v in jsonVersions
                    select new Version
                    {
                        Id = (long)v[Id],
                        Name = (string)v[Name],
                        ProjectId = (long)v[ProjectId]
                    }).ToList();
        }

        public Version GetVersion(string versionName)
        {
            var version = GetVersions()
                .FirstOrDefault(v => v.Name.Equals(versionName, StringComparison.CurrentCultureIgnoreCase));

            return version;
        }

        public Issue GetIssue(string issueKey)
        {
            var source = $"{JiraSource}/issue/{issueKey}";
            var response = SendHttpRequest(source, Method.Get);
            if (response == null) return null;
            return new Issue
            {
                Id = (long)response[Id],
                Key = (string)response[Key],
                Summary = (string)response["fields"]["summary"],
                ProjectId = (long)response["fields"]["project"][Id]
            };
        }

        public IList<string> QueryIssueKeys(string queryString, bool exactMatch = false)
        {
            List<string> keys = null;
            var source = $"{JiraSource}/issue/picker?query={queryString}";
            var response = SendHttpRequest(source, Method.Get);
            if (response == null) return null;
            var sectionIssues = (from s in response["sections"][0]["issues"] as JArray
                                 select new
                                 {
                                     Key = (string)s["key"],
                                     Summary = (string)s["summaryText"]
                                 }).ToList();

            if (exactMatch)
            {
                if (sectionIssues.Count > 0)
                {
                    keys =
                        sectionIssues.
                            Where(s => queryString.Trim().
                                Equals(s.Summary?.Trim(), StringComparison.CurrentCultureIgnoreCase)).
                            Select(s => s.Key).
                            ToList();
                }
            }
            else
            {
                keys = sectionIssues.Select(s => s.Key).ToList();
            }

            return keys;
        }

        public JContainer Search(string jql, List<string> fields, int startAt = 0, int maxResults = 100)
        {
            var source = $"{JiraSource}/search";
            return SendHttpRequest(source, Method.Post, new
            {
                jql = jql,
                startAt = startAt,
                maxResults = maxResults,
                fields = fields
            });
        }

        public IList<Issue> SearchIssues(string issueSummary, bool exactMatch = false)
        {
            List<Issue> issues = null;
            var jql = $"Summary ~ '{issueSummary}'";
            var result = Search(jql, new List<string> { "id", "key", "summary", "project" });

            if ((int)result["total"] == 0) return null;

            var sectionIssues = (from s in result["issues"] as JArray
                                 select new
                                 {
                                     Id = (long)s["id"],
                                     Key = (string)s["key"],
                                     Summary = (string)s["fields"]["summary"],
                                     ProjectId = (long)s["fields"]["project"]["id"]
                                 }).ToList();

            if (exactMatch)
            {
                if (sectionIssues.Count > 0)
                {
                    issues =
                        sectionIssues
                            .Where(s => issueSummary.Trim()
                                .Equals(s.Summary?.Trim(), StringComparison.CurrentCultureIgnoreCase))
                            .Select(s =>
                                new Issue
                                {
                                    Id = s.Id,
                                    Key = s.Key,
                                    Summary = s.Summary,
                                    ProjectId = s.ProjectId
                                }).ToList();
                }
            }
            else
            {
                issues = sectionIssues.Select(s => new Issue
                {
                    Id = s.Id,
                    Key = s.Key,
                    Summary = s.Summary,
                    ProjectId = s.ProjectId
                }).ToList();
            }

            return issues;
        }

        public Issue CreateIssue(IssueCreation issueCreation)
        {
            var source = $"{JiraSource}/issue";
            var response = SendHttpRequest(source, Method.Post, issueCreation);
            if (response == null) return null;
            return new Issue
            {
                Id = (long)response[Id],
                Key = (string)response[Key],
                Summary = issueCreation.fields.summary,
                ProjectId = Project.Id
            };
        }

        public void UpdateIssue(string issueKey, IssueUpdate issueUpdate)
        {
            var source = $"{JiraSource}/issue/{issueKey}";
            SendHttpRequest(source, Method.Put, issueUpdate);
        }

        public Version GetVersion(long versionId)
        {
            var source = $"{JiraSource}/version/{versionId}";
            var response = SendHttpRequest(source, Method.Get);
            if (response == null) return null;
            return new Version
            {
                Id = (long)response[Id],
                Name = (string)response[Name],
                ProjectId = (long)response[ProjectId]
            };
        }

        public Version CreateVersion(VersionCreation versionCreation)
        {
            var source = $"{JiraSource}/version";
            var response = SendHttpRequest(source, Method.Post, versionCreation);
            if (response == null) return null;
            return new Version
            {
                Id = (long)response[Id],
                Name = (string)response[Name],
                ProjectId = (long)response[ProjectId]
            };
        }

        private JContainer SendHttpRequest(string source, Method method, object requestPayload = null)
        {
            RestResponse response = null;
            try
            {
                var request = new RestRequest(source, method);
                if (method == Method.Post || method == Method.Put)
                {
                    var json = JsonConvert.SerializeObject(requestPayload, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    request.AddParameter("application/json", json, ParameterType.RequestBody);
                }

                response = _restClient.Execute(request);
                if (response.StatusCode.Equals(HttpStatusCode.NotFound))
                    return null;

                var data = (JContainer)JToken.Parse(response.Content);
                return data;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Jira - The http status is:{response?.StatusCode}\nThe http content is:{response?.Content}\\nThe exception is:{e.Message}");
                return null;
            }
        }
    }
}
