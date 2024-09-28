using Asp.Versioning;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dash.Infrastructure.Repositories.Sql;
using Dash.Persistence;
using Dash.Server.HostedServices;
using Dash.Server.Hubs;
using Dash.Server.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configure hosting server
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5051);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
         policy =>
         {
             policy.AllowAnyOrigin();
             policy.AllowAnyMethod();
             policy.AllowAnyHeader();
         });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


builder.Host.ConfigureContainer<ContainerBuilder>((context, cBuilder) =>
{
    cBuilder.RegisterType<DisplayRepository>().AsImplementedInterfaces();
    cBuilder.RegisterType<CalendarRepository>().AsImplementedInterfaces();
    cBuilder.RegisterType<ElementRepository>().AsImplementedInterfaces();
    cBuilder.RegisterType<InformationRepository>().AsImplementedInterfaces();
    cBuilder.RegisterType<UserRepository>().AsImplementedInterfaces();
});

// Configure DB factory
builder.Services.AddDbContextFactory<ApplicationDBContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DashDatabase"));
});


builder.Services.AddSignalR().AddJsonProtocol();
builder.Services.AddControllers();

// Api Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(o =>
{
    o.Title = "Dash";
    o.Version = "v1.0";
    o.DocumentName = o.Version;
}).AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddHostedService<IntegrationInitService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(o =>
    {
        o.DocumentTitle = "Dash Api Docs";
    });
    app.UseDeveloperExceptionPage();
}

app.UseApiKeyValidation();

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapHub<DisplayHub>("/hub/display");
app.MapHub<CalendarHub>("/hub/calendar");
app.MapHub<PinboardHub>("/hub/pinboard");
app.MapHub<ElementHub>("/hub/element");

// DB Migrations
{
    using var scope = app.Services.CreateScope();
    using var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    ctx.Database.Migrate();
}

app.Run();