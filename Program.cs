using CloudinaryDotNet;
using insightflow_workspace_service.src.data;
using insightflow_workspace_service.src.helpers;
using insightflow_workspace_service.src.interfaces;
using insightflow_workspace_service.src.repositories;
using insightflow_workspace_service.src.services;
using DotNetEnv;
// Carga las variables de entorno desde el archivo .env
Env.Load();
// Crea el constructor de la aplicación web
var builder = WebApplication.CreateBuilder(args);
// Agrega los controladores de la api
builder.Services.AddControllers();
// Agrega el repositorio de workspaces singleton
builder.Services.AddSingleton<IWorkspaceRepository, WorkspaceRepository>();
// Configura Cloudinary
CloudinarySettings cloudinarySettings =
    new CloudinarySettings
    {
        CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME")!,
        ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY")!,
        ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")!
    };
// Registra Cloudinary como un servicio singleton
var account = new Account(
    cloudinarySettings.CloudName, 
    cloudinarySettings.ApiKey, 
    cloudinarySettings.ApiSecret);
Cloudinary cloudinary = new Cloudinary(account);
builder.Services.AddSingleton(cloudinary);
builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();
// Agrega la API 
builder.Services.AddOpenApi();
// Configura CORS para permitir solicitudes desde cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
// Construye la aplicación
var app = builder.Build();
// Configura el middleware de CORS
app.UseCors("AllowAll");
// Mapea los controladores
app.MapControllers();
// Inicializa el seeder de datos
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    _ = DataSeeder.InitializeAsync(services);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
// Ejecuta la aplicación
app.Run();