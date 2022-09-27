using Bookshop.Contracts.DataTransferObjects.Demo;

namespace Bookshop.Contracts.Services
{
    public interface ISomekindOfService // We will use interfaces for dependency injection.
        // We could not use them, although usually everyone does :)
    {
        Task CreateStuffAsync(CreateDemoDto createDemoDto);
    }
}
