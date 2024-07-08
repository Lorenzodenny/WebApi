using WebApi.MiddleWare;
using WebApi.Services;
using WebApi.Design_Pattern.Factory;
using WebApi.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Web API",
        Version = "v1"
    });
});

// Registra il FilmService nel container di Dependency Injection
builder.Services.AddScoped<FilmService>();
// Registra la FilmFactory come implementazione di IFilmFactory
builder.Services.AddScoped<IFilmFactory, FilmFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1");
        c.RoutePrefix = "swagger"; // Cambia a "swagger" se vuoi accedere tramite /swagger
    });
}

// Middleware personalizzato
app.UseMiddleware<MyCustomMiddleware>();

// app.UseHttpsRedirection(); // Commenta questa linea

app.UseAuthorization();

app.MapControllers();

app.Run();
