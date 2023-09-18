using System.Threading.Tasks;

namespace AppUpdate.Services
{
    public abstract class ServiceBase
    {
        protected readonly string UserName;
        protected readonly string RepositoryName;
        protected readonly AppVersion CurrentAppVersion;

        protected ServiceBase(string userName, string repositoryName, AppVersion currentAppVersion)
        {
            UserName = userName;
            RepositoryName = repositoryName;
            CurrentAppVersion = currentAppVersion;
        }

        public abstract Task<bool> HasLatestAsync();
    }
}