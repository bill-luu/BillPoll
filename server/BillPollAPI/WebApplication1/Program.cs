using Microsoft.EntityFrameworkCore;
using System.IO;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

// Get the path from command line argument or default to AppData folder
var DbPath = "";
if (args.Length > 0 && args[0] == "-dev")
{
    var outputDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
    DbPath = System.IO.Path.Join(outputDirectory, "poll.db");
} 
else
{
    DbPath = System.IO.Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "poll.db");
}

builder.Services.AddDbContext<PollContext>(
        options => options.UseSqlite($"Data Source={DbPath}")
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("default");
app.UseAuthorization();

app.MapControllers();

app.Run();
