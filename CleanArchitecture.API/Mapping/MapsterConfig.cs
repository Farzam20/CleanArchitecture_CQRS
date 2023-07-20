using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Entities;
using Mapster;

namespace CleanArchitecture.API.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<Product, ProductDisplayDto>
                .NewConfig()
                .Map(destination => destination.Email, source => source.CreatedByUser.Email);
        }
    }
}
