using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DemoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Se agrega en generador de Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    { Title = "Api Demo REST", Version = "v1" });
    // Obtenemos el directorio actual
    var basePath = AppContext.BaseDirectory;
    //Obtenemos el nombre de la dll por medio de reflexión
    var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
    //Al nombre del assembly le agregamos la extensión xml
    var fileName = System.IO.Path
                  .GetFileName(assemblyName + ".xml");
    var xmlPath = Path.Combine(basePath, fileName);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost");
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Demo REST");
    });
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
