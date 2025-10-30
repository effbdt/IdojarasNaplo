using Microsoft.Extensions.Logging;

namespace IdojarasNaplo
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
			builder.Services.AddSingleton<MainPageViewModel>();
			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddTransient<EditDiaryEntryViewModel>();
			builder.Services.AddSingleton<EditDiaryPage>();
			builder.Services.AddSingleton<IDiaryDatabase, SQLiteDiaryEntryDatabase>();


#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
