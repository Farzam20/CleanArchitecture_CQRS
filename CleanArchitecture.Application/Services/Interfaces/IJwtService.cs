namespace CleanArchitecture.Application.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> Generate(string userName);
    }
}
