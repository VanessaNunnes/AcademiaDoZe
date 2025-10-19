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
					fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
				});

			ConfigurationHelper.ConfigureServices(builder.Services);

			builder.Services.AddTransient<DashboardListViewModel>();
			builder.Services.AddTransient<LogradouroListViewModel>();
			builder.Services.AddTransient<LogradouroViewModel>();
			// Registrar Views
			builder.Services.AddTransient<DashboardListPage>();
			builder.Services.AddTransient<LogradouroListPage>();
			builder.Services.AddTransient<LogradouroPage>();
			builder.Services.AddTransient<ConfigPage>();

			// Registrar ViewModels
			builder.Services.AddTransient<ColaboradorListViewModel>();
			builder.Services.AddTransient<ColaboradorViewModel>();
			// Registrar Views
			builder.Services.AddTransient<ColaboradorListPage>();
			builder.Services.AddTransient<ColaboradorPage>();

			builder.Services.AddTransient<AlunoListViewModel>();
			builder.Services.AddTransient<AlunoViewModel>();
			// Registrar Views
			builder.Services.AddTransient<AlunoListPage>();
			builder.Services.AddTransient<AlunoPage>();

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
