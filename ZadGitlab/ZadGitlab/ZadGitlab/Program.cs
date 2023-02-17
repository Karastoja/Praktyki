using System;
using System.Collections.Generic;

namespace ZadGitlab
{
    static class Program
    {
        static void Main()
        {
            var control = new Api();
            Console.Write("Nazwa: ");
            string username = Console.ReadLine();
            Console.Write("Hasło: ");
            string password = Console.ReadLine();
            string accessToken = control.getAccessToken(username, password);
            if (accessToken != "no_access_token")
            {
                Console.Write("\nId projektu: ");
                string projectId = Console.ReadLine();

                try
                {
                    List<Issue> issues = control.getIssues(projectId, accessToken);

                    foreach (var issue in issues)
                    {
                        Console.WriteLine("Id błedu: " + issue.iid +
                            "\nAutor: " + issue.author.name +
                            "\nTytuł: " + issue.title +
                            "\nOpis: " + issue.description + "\n");
                    }

                    Console.Write("Podaj identyfikator błedu, od którego chcesz zobaczyć notatki: ");
                    string issueId = Console.ReadLine();
                    List<Notes> notes = control.getNotes(projectId, issueId, accessToken);

                    foreach (var note in notes)
                    {
                        Console.WriteLine("Id notatki: " + note.id +
                            "\nTreść: " + note.body +
                            "\nAutor: " + note.author.name + "\n");
                    }

                    Console.Write("Dodaj notatke do błedów: ");
                    string noteBody = Console.ReadLine();
                    control.createNote(projectId, issueId, accessToken, noteBody);

                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    Console.WriteLine("\nBład podczas wprowadzania danych spróbuj ponownie");
                }
            }
        }
    }
}
