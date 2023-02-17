using System;
using System.Collections.Generic;

namespace ZadGitlab
{
    class Program
    {
        static void Main()
        {
            var control = new Api();
            Console.Write("Nazwa użytkownika: ");
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
                    var issues = control.getIssues(projectId, accessToken);

                    foreach (var issue in issues)
                    {
                        Console.WriteLine("Id błędu: " + issue.iid +
                            "\nAutor: " + issue.author.name +
                            "\nTytuł: " + issue.title +
                            "\nOpis: " + issue.description + "\n");
                    }

                    Console.Write("Podaj identyfikator błędu, od którego chcesz zobaczyć notatki: ");
                    string issueId = Console.ReadLine();
                    var notes = control.getNotes(projectId, issueId, accessToken);

                    foreach (var note in notes)
                    {
                        Console.WriteLine("Id notatki: " + note.id +
                            "\nTreść: " + note.body +
                            "\nAutor: " + note.author.name + "\n");
                    }

                    Console.Write("Dodaj notatkę do błędu: ");
                    string noteBody = Console.ReadLine();
                    control.createNote(projectId, issueId, accessToken, noteBody);

                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    Console.WriteLine("\nBłąd podczas wprowadzania danych. Spróbuj ponownie.");
                }
            }
        }
    }
}


