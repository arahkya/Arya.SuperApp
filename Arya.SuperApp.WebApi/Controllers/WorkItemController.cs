using Arya.SuperApp.Application.Interfaces.Scene;
using Arya.SuperApp.Application.Scenes.CRUD.CreateWorkItem;
using Arya.SuperApp.Application.Scenes.CRUD.DeleteWorkItem;
using Arya.SuperApp.Application.Scenes.CRUD.ListWorkItem;
using Arya.SuperApp.Application.Scenes.CRUD.UpdateWorkItem;
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
}