using Test66bit2.src.DbVersionators.CountVersionator;
using Test66bit2.src.DbVersionators;
using Microsoft.EntityFrameworkCore;
using Test66bit2.src.DbClients.EntityClient;
using Test66bit2.src.Db;
using Test66bit.src.DbClients.EntityClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDbVersionator<int>, CountVersionator>();
builder.Services.AddDbContext<EntityDbContext>(
options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultEntityConnection")));
builder.Services.AddSingleton<IDbClient, EntityDbClient>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/list", async context => {
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync("wwwroot\\list.html");
});

app.Run();
