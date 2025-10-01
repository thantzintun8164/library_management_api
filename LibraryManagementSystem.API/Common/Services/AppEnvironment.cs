using LibraryManagementSystem.Application.Contracts.Services;

namespace LibraryManagementSystem.API.Common.Services
{
    public class AppEnvironment : IAppEnvironment
    {
        private readonly IWebHostEnvironment _env;

        public AppEnvironment(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string ContentRootPath => _env.ContentRootPath;
        public string WebRootPath => _env.WebRootPath;

    }
}
