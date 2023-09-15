using System.Threading.Tasks;

namespace AppUpdate.Services
{
    public abstract class ServiceBase
    {
        protected readonly string UserName;
        protected readonly string RepositoryName;
        protected readonly Version CurrentVersion;

        protected ServiceBase(string userName, string repositoryName, Version currentVersion)
        {
            UserName = userName;
            RepositoryName = repositoryName;
            CurrentVersion = currentVersion;
        }

        public abstract Task<bool> HasLatestAsync();
    }
}