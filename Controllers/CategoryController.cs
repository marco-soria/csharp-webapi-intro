using Microsoft.AspNetCore.Mvc;
using csharp_webapi_intro.Models;
using csharp_webapi_intro.Services;

namespace csharp_webapi_intro.Controllers;

[Route("api/[controller]")]
public class CategoryController: ControllerBase
{
    
    protected readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService service)
    {
        _categoryService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _categoryService.Get());
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Category category)
    {
        await _categoryService.Save(category);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] Category category)
    {
        await _categoryService.Update(id, category);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.Delete(id);
        return Ok();
    }  

}