using Microsoft.AspNetCore.Mvc;
using csharp_webapi_intro.Models;
using csharp_webapi_intro.Services;

namespace csharp_webapi_intro.Controllers;

[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    
    protected readonly ITaskService _taskService;

    public TaskController(ITaskService service)
    {
        _taskService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _taskService.Get());
    }

     [HttpPost]
    public async Task<IActionResult> Post([FromBody] csharp_webapi_intro.Models.Task task)
    {
        await _taskService.Save(task);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] csharp_webapi_intro.Models.Task task)
    {
        await _taskService.Update(id, task);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _taskService.Delete(id);
        return Ok();
    }
}