using FluentValidation;
using Magic8Ball.Api.Extensions;
using Magic8Ball.Api.Middleware;
using Magic8Ball.Application;
using Magic8Ball.Application.UseCases.Answer;
using Magic8Ball.Domain.Entities;
using Magic8Ball.Infra.Data;
using Magic8Ball.Infra.IoC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using MediatR;

namespace Magic8Ball.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Configuration
               .AddJsonFile("appsettings.json", false, true)
               .AddJsonFile("appsettings.Local.json", true, true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
               .AddEnvironmentVariables();

            //Serilog
            builder.ConfigureLog();



            //builder.Services
            //    .AddCustomMediatR(new[] { typeof(GetAnswerRequest) });
                //.AddCustomValidators(new[] { typeof(GetAnswerRequest.GetValidator) });
            //builder.Services.AddTransient<IValidator<GetAnswerRequest>, GetAnswerRequest.GetValidator>();

            builder.Services.AddDIConfiguration(builder.Configuration);

            builder.Services.AddApplication();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();



            // Add services to the container.
            builder.Services.AddControllers();


            //Compressão da requisição
            builder.Services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            builder.Services.Configure<BrotliCompressionProviderOptions>(
                options =>
                {
                    options.Level = CompressionLevel.Fastest;
                }
            );
            builder.Services.Configure<GzipCompressionProviderOptions>(
                options =>
                {
                    options.Level = CompressionLevel.Fastest;
                }
            );

            ConfigDatabase(builder.Services, builder.Configuration);

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors(opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
            });


            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();
            app.UseMiddleware<ExceptionMiddleware>();

            CreateDatabase(app);

            app.Run();
        }

        static void ConfigDatabase(IServiceCollection services, IConfiguration configuration)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            string DbPath = System.IO.Path.Join(path, "magic8ball.db");

            string connectionString
                        = $"Data Source={DbPath}";
            var optionBuilder = new DbContextOptionsBuilder<DefaultDbContext>();

            services.AddDbContext<DefaultDbContext>(options =>
            {
                options
                .UseSqlite(connectionString, (opt) => {
                    opt.MigrationsAssembly("Magic8Ball.Infra.Data"); 
                });
            });
        }

        static void EnsureDatabaseCreated(DefaultDbContext dbContext)
        {
            dbContext.Database.Migrate();
        }

        static void CreateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DefaultDbContext>())
                {
                    EnsureDatabaseCreated(context);
                }
            }
        }
    }
}
