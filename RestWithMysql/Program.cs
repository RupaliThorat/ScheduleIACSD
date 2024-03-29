
using RestWithMysql.dbContext;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*builder.Services.AddEntityFrameworkMySQL().AddDbContext<DotnetdbContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("Default"));
});*/
builder.Services.AddDbContextPool<DotnetdbContext>(opt =>
{
    opt.UseMySql(builder.Configuration.GetConnectionString("Default"),
     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default")),
    builder =>
    {
        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
