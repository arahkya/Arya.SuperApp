using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Application.Interfaces.Scene;
using Arya.SuperApp.Application.Providers;
using Arya.SuperApp.Application.Scenes.CRUD.CreateWorkItem;
using Arya.SuperApp.Application.Scenes.CRUD.DeleteWorkItem;
using Arya.SuperApp.Application.Scenes.CRUD.ListWorkItem;
using Arya.SuperApp.Application.Scenes.CRUD.UpdateWorkItem;
using Microsoft.Extensions.DependencyInjection;

namespace Arya.SuperApp.Application;

public static class Startup
{
    public static IServiceCollection AddSuperAppApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDateTimeProvider, DateTimeProvider>();
        serviceCollection.AddScoped<ISceneHandler<CreateWorkItemRequest, Guid>, CreateWorkItemWriteHandler>();
        serviceCollection.AddScoped<ISceneHandler<ListWorkItemRequest, ListWorkItemResponse>, ListWorkItemWriteHandler>();
        serviceCollection.AddScoped<ISceneHandler<DeleteWorkItemRequest, bool>, DeleteWorkItemWriteHandler>();
        serviceCollection.AddScoped<ISceneHandler<UpdateWorkItemRequest, bool>, UpdateWorkItemWriteHandler>();
        
        return serviceCollection;
    }
}