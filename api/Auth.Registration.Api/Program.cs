using Auth.Sqlite.Contexts;
using Auth.Sqlite.Entities;
using Auth.Sqlite.Repositories.Base;
using Auth.Sqlite.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Db
builder.Services.AddDbContext<AccountDbContext>(options
    => options.UseSqlite(b => b.MigrationsAssembly("Auth.Sqlite")));
builder.Services.AddScoped<IRepository<AccountEntity>, AccountRepository>();


builder.Services.AddAuthentication(
               config =>
               {
                   /* In this section I select Schemes I want to use for Default/Challenge/Forbid 
                    * (think of this section as the MainPanel for Authentication Schemes) */
                   config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                   // An authentication scheme's authenticate action is responsible for constructing the user's identity based on request context. 
                   config.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                   //An authentication challenge is invoked by Authorization when an unauthenticated user requests an endpoint that requires authentication.
                   config.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.Cookie.Name = "Sebs.Cookie";
                    o.LoginPath = new PathString("/api/Account/Login");
                    o.Events.OnSigningIn += (context) =>
                    {
                        return Task.CompletedTask;
                    };
                });

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
