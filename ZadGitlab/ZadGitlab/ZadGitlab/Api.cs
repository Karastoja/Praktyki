using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace ZadGitlab
{
    public class Api
    {
        public string getAccessToken(string username, string password)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://gitlab.com/oauth/token?grant_type=password&username={username}&password={password}");
                var responseMessage = httpClient.SendAsync(request).Result;
                var responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                var tokenInfo = JsonConvert.DeserializeObject<Token>(responseContent);

                if (tokenInfo.access_token != null)
                {
                    Console.WriteLine("Logowanie zakoczone pomyślnie");
                    return tokenInfo.access_token;
                }
                else
                {
                    Console.WriteLine("Logowanie nie powiodło się");
                }
                return "no_access_token";
            }
        }

        public List<Issue> getIssues(string projectId, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://gitlab.com/api/v4/projects/{projectId}/issues?access_token={accessToken}");
                var responseMessage = httpClient.SendAsync(request).Result;
                var responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                var issueInfo = JsonConvert.DeserializeObject<List<Issue>>(responseContent);

                return issueInfo;
            }
        }

        public List<Notes> getNotes(string projectId, string issueId, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://gitlab.com/api/v4/projects/{projectId}/issues/{issueId}/notes?access_token={accessToken}&sort=asc&order_by=updated_at");
                var responseMessage = httpClient.SendAsync(request).Result;
                var responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                var notesInfo = JsonConvert.DeserializeObject<List<Notes>>(responseContent);

                return notesInfo;
            }
        }

        public void createNote(string projectId, string issueId, string accessToken, string noteBody)
        {
            using (var httpClient = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://gitlab.com/api/v4/projects/{projectId}/issues/{issueId}/notes?access_token={accessToken}");
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "body", noteBody }
                });
                var responseMessage = httpClient.SendAsync(request).Result;

                Console.WriteLine("Dodano notatkę");
            }
        }
    }
}
