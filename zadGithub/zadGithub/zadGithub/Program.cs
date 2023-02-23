using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace zadGithub
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj nazwę użytkownika na GitHubie:");
            string username = Console.ReadLine();

            Console.WriteLine("Podaj nazwę repozytorium:");
            string repoName = Console.ReadLine();

            Console.WriteLine("Czy chcesz wykonać backup czy przywrócić issues? (backup/przywróć)");
            string operation = Console.ReadLine();

            if (operation == "backup")
            {
                Console.WriteLine("Podaj nazwę pliku, do którego chcesz zapisać backup:");
                string backupFile = Console.ReadLine();
                BackupIssues(username, repoName, backupFile);
            }
            else if (operation == "przywróć")
            {
                Console.WriteLine("Podaj nazwę pliku, z którego chcesz przywrócić issues:");
                string backupFile = Console.ReadLine();
                RestoreIssues(username, repoName, backupFile);
            }
            else
            {
                Console.WriteLine("Nieprawidłowa operacja.");
            }

            Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć program.");
            Console.ReadKey();
        }

        static void BackupIssues(string username, string repoName, string backupFile)
        {
            string apiUrl = $"https://api.github.com/repos/{username}/{repoName}/issues?state=all";

            HttpWebRequest request = WebRequest.CreateHttp(apiUrl);
            request.UserAgent = "GithubIssueBackup";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string jsonResponse = reader.ReadToEnd();
                        Issue[] issues = JsonConvert.DeserializeObject<Issue[]>(jsonResponse);

                        using (StreamWriter writer = new StreamWriter(backupFile))
                        {
                            writer.WriteLine(jsonResponse);
                        }

                        Console.WriteLine($"Backup issues dla repozytorium {username}/{repoName} został zapisany w pliku {backupFile}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Nie udało się pobrać issues dla repozytorium {username}/{repoName}.");
                }
            }
        }

        static void RestoreIssues(string username, string repoName, string backupFile)
        {
            string apiUrl = $"https://api.github.com/repos/{username}/{repoName}/issues";

            using (StreamReader reader = new StreamReader(backupFile))
            {
                string json = reader.ReadToEnd();
                Issue[] issues = JsonConvert.DeserializeObject<Issue[]>(json);

                foreach (Issue issue in issues)
                {
                    string postData = JsonConvert.SerializeObject(new
                    {
                        title = issue.Title,
                        body = issue.Body,
                        assignee = issue.Assignee?.Login,
                        milestone = issue.Milestone?.Title,
                        labels = issue.Labels
                    });

                    HttpWebRequest request = WebRequest.CreateHttp(apiUrl);
                    request.Method = "POST";
                    request.UserAgent = "GithubIssueBackup";
                    request.ContentType = "application/json";
                    request.Accept = "application/json";
                    request.Headers.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("GITHUB_TOKEN")}");
                    HttpWebResponse response = null;

                    try
                    {
                        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                        {
                            streamWriter.Write(postData);
                        }

                        response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            Console.WriteLine($"Przywrócono issue \"{issue.Title}\".");
                        }
                        else
                        {
                            Console.WriteLine($"Nie udało się przywrócić issue \"{issue.Title}\".");
                        }
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine($"Nie udało się przywrócić issue \"{issue.Title}\".");
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        if (response != null)
                        {
                            response.Close();
                        }
                    }
                }

                Console.WriteLine($"Przywrócono wszystkie issues do repozytorium {username}/{repoName}.");
            }
        }
    }
}

