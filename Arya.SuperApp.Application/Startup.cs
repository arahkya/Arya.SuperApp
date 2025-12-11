using Arya.SuperApp.Application.Interfaces.Date;
using Arya.SuperApp.Application.Interfaces.Scene;
using Arya.SuperApp.Application.Providers;
using Arya.SuperApp.Application.Scenes;
using Arya.SuperApp.Application.Scenes.WorkItem.CreateWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.DeleteWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.DeleteWorkItemLink;
using Arya.SuperApp.Application.Scenes.WorkItem.GetWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.LinkWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItemLinks;
using Arya.SuperApp.Application.Scenes.WorkItem.UpdateWorkItem;
using Arya.SuperApp.Domain;
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
        serviceCollection.AddScoped<ISceneHandler<GetWorkItemRequest, WorkItemEntity?>, GetWorkItemHandler>();
        serviceCollection.AddScoped<ISceneHandler<LinkWorkItemRequest, bool>, LinkWorkItemHandler>();
        serviceCollection.AddScoped<ISceneHandler<ListWorkItemLinksRequest, SceneCollectionResult<LinkedWorkItemEntity>>, ListWorkItemLinksHandler>();
        serviceCollection.AddScoped<ISceneHandler<DeleteWorkItemLinkRequest, bool>, DeleteWorkItemLinkHandler>();
        return serviceCollection;
    }
}