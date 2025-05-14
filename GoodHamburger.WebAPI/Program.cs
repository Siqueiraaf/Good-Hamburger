using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FluentValidation;
using FluentValidation.AspNetCore;
using GoodHamburger.Application.Interfaces;
using GoodHamburger.Application.Services;
using GoodHamburger.Application.Validators;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Infrastructure.Repositories;
using GoodHamburger.WebAPI.Filters;
using Microsoft.OpenApi.Models;

namespace GoodHamburger.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // MVC + Filtros
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<GlobalExceptionFilter>();
        });

        // FluentValidation
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<OrderDtoValidator>();

        // Injeção de dependências
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IMenuService, MenuService>();
        builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();

        // Logging
        builder.Services.AddLogging(options =>
        {
            options.AddConsole();
            options.AddDebug();
            options.SetMinimumLevel(LogLevel.Debug);
        });

        // API Versioning
        builder.Services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        // Swagger
        builder.Services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            var apiVersionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var desc in apiVersionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{desc.GroupName}/swagger.json",
                        $"Versão {desc.GroupName.ToUpperInvariant()}"
                    );
                }
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger");
                    return;
                }

                await next();
            });

            var swaggerGenOptions = app.Services
                .GetRequiredService<Microsoft.Extensions.Options.IOptions<Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions>>()
                .Value;

            foreach (var desc in apiVersionProvider.ApiVersionDescriptions)
            {
                swaggerGenOptions.SwaggerGeneratorOptions.SwaggerDocs[desc.GroupName] = new OpenApiInfo
                {
                    Title = "Gestão de Pedidos - Good Hamburger",
                    Version = desc.ApiVersion.ToString(),
                    Description = "API de pedidos para o WebApp Good Hamburger",
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael Siqueira",
                        Email = "dev.rafaelsiqueira@gmail.com",
                        Url = new Uri("https://github.com/Siqueiraaf")
                    }
                };
            }
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}
