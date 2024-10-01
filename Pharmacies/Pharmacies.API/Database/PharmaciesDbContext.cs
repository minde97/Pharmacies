using Microsoft.EntityFrameworkCore;
using Pharmacies.Database.Entities;

namespace Pharmacies.API.Database;

public class PharmaciesDbContext(DbContextOptions<PharmaciesDbContext> options) : DbContext(options)
{
	public DbSet<Pharmacy> Pharmacies { get; set; }
}
