using AcademiaDoZe.Presentation.AppMauii.Configuration;
using AcademiaDoZe.Presentation.AppMauii.ViewModels;
using AcademiaDoZe.Presentation.AppMauii.Views;
using Microsoft.Extensions.Logging;

namespace AcademiaDoZe.Presentation.AppMauii
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			ConfigurationHelper.ConfigureServices(builder.Services);

			builder.Services.AddTransient<DashboardListViewModel>();
			builder.Services.AddTransient<LogradouroListViewModel>();
			builder.Services.AddTransient<LogradouroViewModel>();
			// Registrar Views
			builder.Services.AddTransient<DashboardListPage>();
			builder.Services.AddTransient<LogradouroListPage>();
			builder.Services.AddTransient<LogradouroPage>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
