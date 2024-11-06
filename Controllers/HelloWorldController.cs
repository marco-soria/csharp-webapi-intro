using Microsoft.AspNetCore.Mvc;

namespace csharp_webapi_intro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloWorldController:  ControllerBase
{
    protected readonly IHelloWorldService _helloWorldService;

    TasksContext dbcontext;

    public HelloWorldController(IHelloWorldService helloWorld, TasksContext db)
    {
        _helloWorldService = helloWorld;
        dbcontext = db;
    }

     [HttpGet]
    public IActionResult Get()
    {
        return Ok(_helloWorldService.GetHelloWorld());
    }

    [HttpGet]
    [Route("createdb")]
    public IActionResult CreateDatabase()
    {
        dbcontext.Database.EnsureCreated();

        return Ok();
    }
}