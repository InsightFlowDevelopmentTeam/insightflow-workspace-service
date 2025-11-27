using CloudinaryDotNet;
using insightflow_workspace_service.src.data;
using insightflow_workspace_service.src.helpers;
using insightflow_workspace_service.src.interfaces;
using insightflow_workspace_service.src.repositories;
using insightflow_workspace_service.src.services;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IWorkspaceRepository, WorkspaceRepository>();
CloudinarySettings cloudinarySettings =
    new CloudinarySettings
    {
        CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME")!,
        ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY")!,
        ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")!
    };

var account = new Account(
    cloudinarySettings.CloudName, 
    cloudinarySettings.ApiKey, 
    cloudinarySettings.ApiSecret);
Cloudinary cloudinary = new Cloudinary(account);
builder.Services.AddSingleton(cloudinary);
builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();

builder.Services.AddOpenApi();

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

var app = builder.Build();
app.UseCors("AllowAll");

app.MapControllers();


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
// Run the application
app.Run();