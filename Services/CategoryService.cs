using csharp_webapi_intro.Models;
namespace csharp_webapi_intro.Services;
using Microsoft.EntityFrameworkCore;


public class CategoryService : ICategoryService
{
    private readonly TasksContext _context;

    public CategoryService(TasksContext dbcontext)
    {
        _context = dbcontext;
    }
    
     public async Task<IEnumerable<Category>> Get()
    {
        return await _context.Categories.ToListAsync();
    }

    public async System.Threading.Tasks.Task Save(Category category)
    {
        _context.Add(category);
        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task Update(Guid id, Category category)
    {
        var currentCategory = await _context.Categories.FindAsync(id);

        if (currentCategory != null)
        {
            currentCategory.Name = category.Name;
            currentCategory.Description = category.Description;
            currentCategory.Weight = category.Weight;

            await _context.SaveChangesAsync();
        }
    }


    public async System.Threading.Tasks.Task Delete(Guid id)
    {
        var currentCategory = await _context.Categories.FindAsync(id);

        if (currentCategory != null)
        {
            _context.Remove(currentCategory);
            await _context.SaveChangesAsync();
        }
    }

}

public interface ICategoryService
{
    
    System.Threading.Tasks.Task<IEnumerable<Category>> Get();
    System.Threading.Tasks.Task Save(Category category);

    System.Threading.Tasks.Task Update(Guid id, Category category);

    System.Threading.Tasks.Task Delete(Guid id);
}