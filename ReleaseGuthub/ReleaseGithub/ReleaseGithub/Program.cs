using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ReleaseGithub
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Podaj właściciela repozytorium: ");
            string owner = Console.ReadLine();
            Console.WriteLine("Podaj nazwę repozytorium: ");
            string repoName = Console.ReadLine();
            Console.WriteLine("Podaj numer wydania (np. 1.0.0): ");
            string releaseTag = Console.ReadLine();

            List<ReleaseAsset> assets = await GetReleaseAssets(owner, repoName, releaseTag);

            Console.WriteLine($"Ilość assetów: {assets.Count}");

            for (int i = 0; i < assets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {assets[i].Name} ({assets[i].Size} bytes)");
            }

            Console.WriteLine("Który asset chcesz pobrać? Podaj numer: ");
            int selectedAssetIndex = int.Parse(Console.ReadLine()) - 1;

            ReleaseAsset selectedAsset = assets[selectedAssetIndex];
            string downloadUrl = selectedAsset.DownloadUrl;

            Console.WriteLine($"Pobieranie assetu {selectedAsset.Name}...");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(downloadUrl);

                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    using (FileStream fileStream = new FileStream(selectedAsset.Name, FileMode.Create))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }
            }

            Console.WriteLine($"Pobrano {selectedAsset.Name}");
            Console.ReadLine();
        }

        private static async Task<List<ReleaseAsset>> GetReleaseAssets(string owner, string repoName, string releaseTag)
        {
            List<ReleaseAsset> assets = new List<ReleaseAsset>();

            string apiUrl = $"https://api.github.com/repos/{owner}/{repoName}/releases/tags/{releaseTag}";
            string accessToken = "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";

            using (HttpClient client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", accessToken);
                }

                client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue("GitHubReleaseAssetsDownloader", "1.0"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                string json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    Release release = JsonSerializer.Deserialize<Release>(json, options);

                    assets = release.Assets;
                }
                else
                {
                    Console.WriteLine($"Nie udało się pobrać assetów. Kod błędu: {response.StatusCode}");
                }
            }

            return assets;
        }
    }
}