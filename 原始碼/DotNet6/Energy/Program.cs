﻿using Autofac.Extensions.DependencyInjection;
using Autofac;
using Energy;
using Microsoft.EntityFrameworkCore;
using Energy.Models.DB;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Serilog;
using Energy.Utils;
using System.Xml.Linq;
using System.Text.Json.Serialization;
using Energy.Models.Constants;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    //Autofac
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister()));

    //AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Dapper
    builder.Services.AddSingleton<DapperContext>();

    // Context
    builder.Services.AddDbContext<EnergyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectonString")));

    // WebApi中Enum轉換
    builder.Services.AddControllers()
        .AddJsonOptions(
        options =>
        {
            var enumConverter = new JsonStringEnumConverter();
            options.JsonSerializerOptions.Converters.Add(enumConverter);

            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

    builder.Services.AddEndpointsApiExplorer();

    // Swagger
    builder.Services.AddSwaggerGen(options =>
    {
        //options.AddEnumsWithDescriptions();
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "能源局 API",
            Description = "An ASP.NET Core Web API for managing <br/> 驗證apiKey為 test123!",
        });

        options.AddSecurityDefinition(ApiKeyAuthenticationConst.AuthenticationScheme,
            new OpenApiSecurityScheme
            {
                Description = "API key used in the Authorization header.",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = ApiKeyAuthenticationConst.AuthenticationScheme,
                //Scheme = ApiKeyAuthenticationConst.AuthenticationScheme,
            });

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = ApiKeyAuthenticationConst.AuthenticationScheme
                        }
                    },
                    new string[] {}
                }
            });

        // using System.Reflection;
        
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var docPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
        var doc = XDocument.Load(docPath);
        options.IncludeXmlComments(docPath);
        options.SchemaFilter<DescribeEnumMembers>(doc);
    });

    //controller可以使用ILogger介面來寫入log紀錄
    builder.Host.UseSerilog();

    var app = builder.Build();

    var apiKey = builder.Configuration.GetValue<string>("ApiKey");

    app.Use(async (context, next) =>
    {
        var found = context.Request.Headers.TryGetValue(ApiKeyAuthenticationConst.AuthenticationScheme, out var key);

        if (context.Request.Path.StartsWithSegments("/swagger") || (found && key == apiKey))
        {
            await next(context);
        }
        else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("授權失敗");
            return;
        }
    });




    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    app.UseSwagger();
    app.UseSwaggerUI();
    //}

    app.UseAuthorization();

    app.MapControllers();

    // 每一個 Request 使用 Serilog 記錄下來
    app.UseSerilogRequestLogging(options =>
    {
        // 如果要自訂訊息的範本格式，可以修改這裡，但修改後並不會影響結構化記錄的屬性
        options.MessageTemplate = "Handled {RequestPath}";

        // 預設輸出的紀錄等級為 Information，你可以在此修改記錄等級
        // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        // 你可以從 httpContext 取得 HttpContext 下所有可以取得的資訊！
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("UserID", httpContext.User.Identity?.Name);
        };
    });
    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}