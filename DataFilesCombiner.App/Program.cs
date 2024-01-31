using DataFileCombiner.ClassLibrary.Interfaces;
using DataFileCombiner.ClassLibrary.Mediatr.Notifications;
using DataFileCombiner.ClassLibrary.Services;
using DataFilesCombiner.App.Mediator;
using DataFilesCombiner.App.Services;
using DataFilesCombiner.ClassLibrary.ExtensionMethods;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;

namespace DataFilesCombiner.App;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		// Winform setup needs to be done at the beginning.
		ApplicationConfiguration.Initialize();

		var host = new HostBuilder()
			.ConfigureAppConfiguration((hostingContext, config) =>
			{
				config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			})
			.ConfigureServices((hostContext, services) => ConfigureServices(hostContext, services))
			.Build();

		// Running service for input checking.
		host.RunAsync();
		// Running UI part.
		var form = host.Services.GetRequiredService<Form1>();
		Application.Run(form);

		host.StopAsync();
		Log.CloseAndFlush();
	}

	private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
	{
		// Configure Serilog since i dont really provide messages for error cases.
		Log.Logger = new LoggerConfiguration()
			.ReadFrom.Configuration(hostContext.Configuration)
			.WriteTo.File(new CompactJsonFormatter(), Path.Combine(hostContext.Configuration.GetOutputDirectoryExistingPath(), "log.json"), rollingInterval: RollingInterval.Day)
			.CreateLogger();

		services.AddLogging(cfg => cfg.AddSerilog());
		services.AddSingleton<IProcessedIdRepository, ProcessedIdRepository>();
		services.AddSingleton<IMatchingDataService, MatchingDataService>();
		services.AddSingleton<Form1>();
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<NewFileNotification>());
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
		services.AddHostedService<FileWatcherService>();
	}
}