using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Pharmacies.API.Database;
using Pharmacies.API.Features.Pharmacies.ImportPharmacies;
using Pharmacies.API.Infrastructure;
using Pharmacies.PostIt.Client;

namespace Pharmacies.API;

public static class ServicesRegistrar
{
	public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.AddControllers();

		services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
			});

		services.AddDbContext<PharmaciesDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database")));
		services.AddHttpClient();

		services.AddTransient<IPostItClient>(_ =>
			{
				var httpClient = new HttpClient
				{
					BaseAddress = new Uri(configuration.GetSection("PostIt")["BaseAddress"] ?? string.Empty),
				};
				return new PostItClient(httpClient, configuration.GetSection("PostIt")["key"] ?? string.Empty);

			});

		services.AddScoped<IValidator<ImportPharmaciesRequest>, ImportPharmaciesValidator>();
	}
}
