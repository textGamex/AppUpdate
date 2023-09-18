using System;
using System.Net.Http;
using System.Threading.Tasks;
using AppUpdate.Models;
using Newtonsoft.Json;

namespace AppUpdate.Services
{
    public class GiteeApi : ServiceBase
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly string _key;
        public GiteeApi(string userName, string repositoryName, string key, AppVersion currentAppVersion) 
            : base(userName, repositoryName, currentAppVersion)
        {
            _key = key;
        }

        public override async Task<bool> HasLatestAsync()
        {
            var url = new Uri($"https://gitee.com/api/v5/repos/{UserName}/{RepositoryName}/releases/latest?access_token={_key}");
            
            var result = await Client.GetStringAsync(url);
            var gitInfo = JsonConvert.DeserializeObject<GiteeReleases>(result);
            var webVersion = new AppVersion(gitInfo.Tag_name);
            
            return webVersion > CurrentAppVersion;
        }
    }
}