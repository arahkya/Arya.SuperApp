using Arya.SuperApp.Application.Interfaces.Data;
using Arya.SuperApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arya.SuperApp.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddSuperAppInfrastructure(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, SuperAppDbContext>();
        serviceCollection.AddScoped<IRepository<WorkItemEntity>, SuperAppDbContext.GenericRepository<WorkItemEntity>>();

        serviceCollection.AddDbContext<SuperAppDbContext>(options =>
        {
            options.UseInMemoryDatabase("Arya.SuperApp");
        });
        
        return serviceCollection;
    }
}