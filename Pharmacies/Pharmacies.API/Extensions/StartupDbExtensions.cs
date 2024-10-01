using Microsoft.EntityFrameworkCore;
using Pharmacies.API.Database;

namespace Pharmacies.API.Extensions;

public static class StartupDbExtensions
{
	public static void CreateDatabaseIfNotExists(this IHost host)
	{
		using var scope = host.Services.CreateScope();
		var services = scope.ServiceProvider;

		var pharmaciesDbContext = services.GetRequiredService<PharmaciesDbContext>();

		if (pharmaciesDbContext.Database.EnsureCreated())
		{
			pharmaciesDbContext.Database.MigrateAsync();
		}
	}
}
