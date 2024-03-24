using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pokusaj.Data;
using pokusaj.Models;
using System.Runtime.CompilerServices;
using System.Text;

namespace pokusaj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StudentContext>(options =>
                                        options.UseSqlite(builder.Configuration.GetConnectionString("dbString")));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            //Set up JWT
            SetUpJWT(builder);

            var app = builder.Build();

            //TestDatabaseConnection(app.Services);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void SetUpJWT(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddAuthorization();
        }

        private async static void TestDatabaseConnection(IServiceProvider services)
        {
            await Task.Delay(3000);

            using (var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<StudentContext>();
                try
                {
                    dbContext.Database.OpenConnection();
                    dbContext.Database.CloseConnection();
                    Console.WriteLine("Database connection successful.");
                    Student novi = new Student
                    {
                        ID = 12,
                        Name = "Foo",
                        Surname = "Bar",
                        Email = "email@gmail.com",
                        Password = "pass",
                        ProfilePicture = "slika.png"

                    };
                    dbContext.Add(novi);
                    await dbContext.SaveChangesAsync();
                    if (dbContext.Students != null)
                    {
                        Console.WriteLine("ima");
                        foreach (var user in dbContext.Students.ToList())
                        {
                            Console.WriteLine($"Name: {user.Name}, Surrname: {user.Surname}, ID: {user.ID}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("prazno");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database connection failed: {ex.Message}");
                }
            }
        }
    }
}
