using Microsoft.Extensions.Configuration;
using TodoApp.Server.Models;
using TodoApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<TodoDatabaseSettings>(
    builder.Configuration.GetSection(nameof(TodoDatabaseSettings)));
builder.Services.AddSingleton<TodoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

// Add CORS services
builder.Services.AddCors(options =>
{
    // Define your CORS policies here
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Or use WithOrigins() to specify allowed origins
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll"); // Use the policy name you defined

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowLocalhost49919",
//        builder => builder
//            .WithOrigins("http://localhost:49919")
//            .AllowAnyMethod()
//            .AllowAnyHeader());
//});

//Then in the Configure method:
//app.UseCors("AllowLocalhost49919");

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
