using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Fortune
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);
			await using ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

			var instance = serviceProvider.GetService<App>();

			instance.Run();
		}

		private static void ConfigureServices(IServiceCollection serviceCollection)
		{
			serviceCollection
				.AddTransient<App>()
				.AddSingleton<IDateTimeOffset, DateTimeOffsetWrapper>()
				.AddTransient<FortuneCookie>();
		}
	}
}
