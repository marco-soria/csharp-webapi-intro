using csharp_webapi_intro.Models;
namespace csharp_webapi_intro.Services;
using Microsoft.EntityFrameworkCore;

public class TaskService : ITaskService
{
     private readonly TasksContext _context;

    public TaskService(TasksContext dbcontext)
    {
        _context = dbcontext;
    }

    //  public IEnumerable<csharp_webapi_intro.Models.Task> Get()
    // {
    //     return context.Tasks;
    // }

    public async Task<IEnumerable<csharp_webapi_intro.Models.Task>> Get()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async System.Threading.Tasks.Task Save(csharp_webapi_intro.Models.Task task)
    {
        task.CreationDate = DateTime.Now;
        _context.Add(task);
        await _context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task Update(Guid id, csharp_webapi_intro.Models.Task task)
    {
        var currentTask = await _context.Tasks.FindAsync(id);

        if (currentTask != null)
        {
            currentTask.Title = task.Title;
            currentTask.Description = task.Description;
            currentTask.TaskPriority = task.TaskPriority;
            currentTask.CategoryId = task.CategoryId;

            await _context.SaveChangesAsync();
        }
    }

    public async System.Threading.Tasks.Task Delete(Guid id)
    {
        var currentTask = await _context.Tasks.FindAsync(id);

        if (currentTask != null)
        {
            _context.Remove(currentTask);
            await _context.SaveChangesAsync();
        }
    }

}

public interface ITaskService
{
    //IEnumerable<csharp_webapi_intro.Models.Task> Get();
    Task<IEnumerable<csharp_webapi_intro.Models.Task>> Get();
    System.Threading.Tasks.Task Save(csharp_webapi_intro.Models.Task Task);

    System.Threading.Tasks.Task Update(Guid id, csharp_webapi_intro.Models.Task Task);

    System.Threading.Tasks.Task Delete(Guid id);
}