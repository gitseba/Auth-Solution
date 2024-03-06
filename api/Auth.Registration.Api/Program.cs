using Auth.Sqlite.Contexts;
using Auth.Sqlite.Entities;
using Auth.Sqlite.Repositories;
using Auth.Sqlite.Repositories.Base;
using EmailService.Papercut;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Db
builder.Services.AddDbContext<AccountDbContext>(options
    => options.UseSqlite(b => b.MigrationsAssembly("Auth.Sqlite")));
builder.Services.AddScoped<IRepository<AccountEntity>, AccountRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Papercut Test Email Service
builder.Services.AddPapercutEmailService();

//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
