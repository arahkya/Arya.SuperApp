using Arya.SuperApp.Application.Interfaces.Scene;
using Arya.SuperApp.Application.Scenes;
using Arya.SuperApp.Application.Scenes.WorkItem.CreateWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.DeleteWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.GetWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.LinkWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItem;
using Arya.SuperApp.Application.Scenes.WorkItem.ListWorkItemLinks;
using Arya.SuperApp.Application.Scenes.WorkItem.UpdateWorkItem;
using Arya.SuperApp.Domain;
using Arya.SuperApp.WebApi.ViewModels.WorkItem;
using Microsoft.AspNetCore.Mvc;

namespace Arya.SuperApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkItemController : ControllerBase
{
    [HttpPost(Name = "create-workitem")]
    [Route("create-workitem")]
    public async Task<ActionResult> PostAsync([FromBody] WorkItemCreateViewModel viewModel, [FromServices] ISceneHandler<CreateWorkItemRequest, Guid> handler)
    {
        var request = new CreateWorkItemRequest
        {
            RequestId = HttpContext.TraceIdentifier,
            Name = viewModel.Name
        };
        
        var createdWithId = await handler.HandleAsync(request,_ => Guid.Empty);
        
        return Ok(createdWithId);
    }

    [HttpGet(Name = "get-workitem")]
    [Route("get-workitem/{id:guid}")]
    public async Task<ActionResult> GetAsync([FromRoute] Guid id, [FromServices] ISceneHandler<GetWorkItemRequest, WorkItemEntity?> handler)
    {
        var request = new GetWorkItemRequest { RequestId = HttpContext.TraceIdentifier, WorkItemId = id};

        var workItem = await handler.HandleAsync(request, e => null);
        
        if (workItem.Result is null) return NotFound();
        
        return Ok(workItem);
    }
    
    [HttpGet(Name = "list-workitem")]
    [Route("list-workitem/{pageSize:int}/{page:int}")]
    public async Task<ActionResult> ListAsync(int pageSize, int page, [FromServices] ISceneHandler<ListWorkItemRequest, ListWorkItemResponse> handler)
    {
        var request = new ListWorkItemRequest
        {
            RequestId = HttpContext.TraceIdentifier,
            PageSize = pageSize,
            Page = page
        };
        
        var listWorkItems = await handler.HandleAsync(request, _ => new ListWorkItemResponse([]));
        
        return Ok(listWorkItems);
    }

    [HttpPatch(Name = "update-workitem")]
    [Route("update-workitem/{id:guid}")]
    public async Task<ActionResult> UpdateAsync(Guid id,[FromBody] WorkItemUpdateViewModel viewModel, [FromServices] ISceneHandler<UpdateWorkItemRequest, bool> handler)
    {
        var request = new UpdateWorkItemRequest { RequestId = HttpContext.TraceIdentifier, Id = id, Name = viewModel.Name };
        var isUpdated = await handler.HandleAsync(request, _ => false);
        
        return Ok(isUpdated);
    }
    
    [HttpDelete(Name = "delete-workitem")]
    [Route("delete-workitem/{id:guid}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid id, [FromServices] ISceneHandler<DeleteWorkItemRequest, bool> handler)
    {
        var request = new DeleteWorkItemRequest { RequestId =  HttpContext.TraceIdentifier, Id = id };
        var isDeleted = await handler.HandleAsync(request, _ => false);
        
        return Ok(isDeleted);
    }

    [HttpPatch(Name = "link-workitem")]
    [Route("link-workitem/{id:guid}/{type}/{targetId:guid}")]
    public async Task<ActionResult> LinkAsync([FromRoute] Guid id, [FromRoute] string type, [FromRoute] Guid targetId,
        [FromServices] ISceneHandler<LinkWorkItemRequest, bool> handler)
    {
        var request = new LinkWorkItemRequest
        {
            RequestId = HttpContext.TraceIdentifier,
            WorkItemId = id,
            LinkedWorkItemId =  targetId,
            LinkType = (LinkWorkItemRequest.WorkItemLinkTypes)Enum.Parse(typeof(LinkWorkItemRequest.WorkItemLinkTypes),type)
        };
        
        var isLinked = await handler.HandleAsync(request, e => false);

        if (!isLinked.Result)
        {
            return NotFound();
        }
        
        return Ok(isLinked);
    }

    [HttpGet(Name = "get-links")]
    [Route("get-links/{workItemId:guid}")]
    public async Task<ActionResult> GetLinksAsync([FromRoute] Guid workItemId, [FromServices] ISceneHandler<ListWorkItemLinksRequest, SceneCollectionResult<LinkedWorkItemEntity>> handler)
    {
        var request = new ListWorkItemLinksRequest
        {
            RequestId = HttpContext.TraceIdentifier,
            WorkItemId = workItemId
        };

        var links = await handler.HandleAsync(request, e => new SceneCollectionResult<LinkedWorkItemEntity>([]));
        
        return Ok(links);
    }
}