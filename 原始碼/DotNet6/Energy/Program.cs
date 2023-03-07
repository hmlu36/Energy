using Autofac.Extensions.DependencyInjection;
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
using Autofac.Core;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

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

    // WebApi��Enum�ഫ
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
            Title = "�෽�� API",
            Description = "An ASP.NET Core Web API for managing",
        });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var docPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
        var doc = XDocument.Load(docPath);
        options.IncludeXmlComments(docPath);
        options.SchemaFilter<DescribeEnumMembers>(doc);
    });

    //controller�i�H�ϥ�ILogger�����Ӽg�Jlog����
    builder.Host.UseSerilog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    // �C�@�� Request �ϥ� Serilog �O���U��
    app.UseSerilogRequestLogging(options =>
    {
        // �p�G�n�ۭq�T�����d���榡�A�i�H�ק�o�̡A���ק��ä��|�v�T���c�ưO�����ݩ�
        options.MessageTemplate = "Handled {RequestPath}";

        // �w�]��X���������Ŭ� Information�A�A�i�H�b���ק�O������
        // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        // �A�i�H�q httpContext ���o HttpContext �U�Ҧ��i�H���o����T�I
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