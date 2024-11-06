using Microsoft.EntityFrameworkCore;
using csharp_webapi_intro.Models;

namespace csharp_webapi_intro;

public class TasksContext: DbContext
{
    public DbSet<Category> Categories {get;set;}
    public DbSet<csharp_webapi_intro.Models.Task> Tasks {get;set;}

    public TasksContext(DbContextOptions<TasksContext> options) :base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(category=> 
        {
            List<Category> categoriesInit = new List<Category>();
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), Name = "Pending Activities", Weight = 20});
        categoriesInit.Add(new Category() { CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), Name = "Personal Activities", Weight = 50});

            category.ToTable("Category");
            category.HasKey(p=> p.CategoryId);

            category.Property(p=> p.Name).IsRequired().HasMaxLength(150);

            category.Property(p=> p.Description).IsRequired(false);

            category.Property(p=> p.Weight);

            category.HasData(categoriesInit);
        });

        List<csharp_webapi_intro.Models.Task> tasksInit = new List<csharp_webapi_intro.Models.Task>();

        tasksInit.Add(new csharp_webapi_intro.Models.Task() { TaskId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb410"), CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), TaskPriority = Priority.Medium, Title = "Pay Utilities", CreationDate = DateTime.Now });
        tasksInit.Add(new csharp_webapi_intro.Models.Task() { TaskId  = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb411"), CategoryId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), TaskPriority = Priority.Low, Title = "Finish watching a movie on Netflix", CreationDate = DateTime.Now });

         modelBuilder.Entity<csharp_webapi_intro.Models.Task>(task=>
        {
            task.ToTable("Task");
            task.HasKey(p=> p.TaskId);

            task.HasOne(p=> p.Category).WithMany(p=> p.Tasks).HasForeignKey(p=> p.CategoryId);

            task.Property(p=> p.Title).IsRequired().HasMaxLength(200);

            task.Property(p=> p.Description).IsRequired(false);

            task.Property(p=> p.TaskPriority);

            task.Property(p=> p.CreationDate);

            task.Ignore(p=> p.Summary);

            task.HasData(tasksInit);

        });

    }
}