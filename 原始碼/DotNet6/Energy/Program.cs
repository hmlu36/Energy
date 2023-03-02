using Autofac.Extensions.DependencyInjection;
using Autofac;
using Energy;

var builder = WebApplication.CreateBuilder(args);

//初始化並建立一個實例
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//註冊autofac這個容器
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister()));

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
