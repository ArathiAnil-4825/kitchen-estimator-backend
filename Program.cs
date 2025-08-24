using KitchenEstimatorAPI.Models;
using KitchenEstimatorAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind MongoDb settings
builder.Services.Configure<KitchenEstimatorDatabaseSettings>(
    builder.Configuration.GetSection("KitchenEstimatorDatabase"));

// Register services
builder.Services.AddSingleton<ApprovalService>();
builder.Services.AddSingleton<UserService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // your React dev server
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
