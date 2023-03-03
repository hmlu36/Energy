using Autofac.Extensions.DependencyInjection;
using Autofac;
using Energy;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json;
using Energy.Models.DB;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Starting web host");

    //註冊autofac
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister()));

    //找到所有繼承profile
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddDbContext<EnergyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectonString")));
    builder.Services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                options.JsonSerializerOptions.WriteIndented = true;
            }); ;
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "能源局 API",
            Description = "An ASP.NET Core Web API for managing",
        });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    //controller可以使用ILogger介面來寫入log紀錄
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