using csharp_webapi_intro;
using csharp_webapi_intro.Services;
using DotNetEnv; //needed for connection
using Microsoft.EntityFrameworkCore; //needed for connection

var builder = WebApplication.CreateBuilder(args);

// Cargar el archivo .env
Env.Load();

// Obtener la cadena de conexión
var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION_STRING");

// Imprimir la cadena de conexión para verificación
//Console.WriteLine($"Connection String from .env: {connectionString}");

// Validar que la cadena de conexión no esté vacía
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("The connection string was not found in the environment variables.");
}

// Configurar el contexto de base de datos para usar SQL Server con la cadena de conexión
builder.Services.AddDbContext<TasksContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHelloWorldService, HelloWorldService>();
//builder.Services.AddScoped<IHelloWorldService>(p=> new HelloWorldService());
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseWelcomePage();

//app.UseTimeMiddleware();

app.MapControllers();

app.Run();
