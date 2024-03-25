using System.Reflection;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Robot Controller API",
        Description = "New backend service that provides resources for the Moon robot simulator.",
        Contact = new OpenApiContact
        {
            Name = "Anshpreet Singh",
            Email = "anshpreet4762.be22@deakin.edu.au"
        },
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(setup => setup.InjectStylesheet("/styles/theme-monokai.css"));

app.UseStaticFiles();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
