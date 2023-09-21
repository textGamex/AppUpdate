using System;
using System.Net.Http;
using System.Threading.Tasks;
using AppUpdate.Models;
using Newtonsoft.Json;

namespace AppUpdate.Services
{
    public class GitHubApi : UpdateServiceBase
    {
        private static readonly HttpClient Client = new HttpClient();

        static GitHubApi()
        {
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("User-Agent", 
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36");
        }

        public GitHubApi(string userName, string repositoryName, AppVersion currentAppVersion) 
            : base(userName, repositoryName, currentAppVersion)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public override async Task<bool> HasLatestAsync()
        {
            var url = new Uri($"https://api.github.com/repos/{UserName}/{RepositoryName}/releases/latest");
            
            var result = await Client.GetStringAsync(url);
            var model = JsonConvert.DeserializeObject<GitHub>(result);
            var version = new AppVersion(model.TagName);
            
            return version > CurrentAppVersion;
        }
    }
}