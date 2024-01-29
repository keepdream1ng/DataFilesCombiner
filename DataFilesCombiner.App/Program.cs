using DataFileCombiner.ClassLibrary.Mediatr.Notifications;
using DataFilesCombiner.App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
	}

	private static void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
	{
		services.AddSingleton<Form1>();
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<NewFileNotification>());
		services.AddHostedService<FileWatcherService>();
	}
}