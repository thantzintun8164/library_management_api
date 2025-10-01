namespace LibraryManagementSystem.Application.Contracts.Services
{
    public interface IAppEnvironment
    {
        string ContentRootPath { get; }
        string WebRootPath { get; }
    }
}
