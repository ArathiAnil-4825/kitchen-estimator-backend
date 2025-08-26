using KitchenEstimatorAPI.Models;
using KitchenEstimatorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind MongoDb settings
builder.Services.Configure<KitchenEstimatorDatabaseSettings>(
    builder.Configuration.GetSection("KitchenEstimatorDatabase"));

// Register services
builder.Services.AddSingleton<ApprovalService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<MaterialService>();
builder.Services.AddSingleton<LaborRateService>();
builder.Services.AddSingleton<ProjectService>();

builder.Services.AddControllers();

// CORS configured via appsettings Frontend:AllowedOrigins
var allowedOrigins = builder.Configuration.GetSection("Frontend:AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:5173" };
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowFrontend");

app.MapControllers();
app.Run();
