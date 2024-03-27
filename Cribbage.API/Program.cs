using Microsoft.EntityFrameworkCore;
using Cribbage.API.Hubs;
using Cribbage.PL.Data;
using System.Reflection;
using Serilog.Ui.Web;
using Serilog;
using Serilog.Ui.MsSqlServerProvider;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddSignalR()
            .AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Cribbage API",
                Version = "v1",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Cribbage"
                    //Email = "rachel.groff2190@fvtc.edu",
                    //Url = new Uri("https://www.fvtc.edu")
                }
            });

            var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlfile);
            c.IncludeXmlComments(xmlpath);
        });


        // Add Connection information
        builder.Services.AddDbContextPool<CribbageEntities>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
            options.UseLazyLoadingProxies();
        });

        string connection = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddSerilogUi(options =>
        {
            options.UseSqlServer(connection, "Logs");
        });

        var configsettings = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configsettings)
            .CreateLogger();

        builder.Services
            .AddLogging(c => c.AddDebug())
            .AddLogging(c => c.AddSerilog())
            .AddLogging(c => c.AddEventLog())
            .AddLogging(c => c.AddConsole());

        var app = builder.Build();

        app.UseSerilogUi(options =>
        {
            options.RoutePrefix = "logs";
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        //app.MapControllers();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<CribbageHub>("/cribbageHub");
        });

        app.Run();
    }
}