using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pharmacies.API.Database;
using Pharmacies.API.Features.Pharmacies.GetPharmacies;
using Pharmacies.Database.Entities;

namespace Pharmacies.API.Tests.Pharmacies.GetPharmacies;

[TestClass]
public class GetPharmacyTests
{
	private PharmaciesDbContext dbContext;

	[TestInitialize]
	public void Setup()
	{
		var options = new DbContextOptionsBuilder<PharmaciesDbContext>()
			.UseInMemoryDatabase(databaseName: "GetPharmacies")
			.Options;

		this.dbContext = new PharmaciesDbContext(options);
	}

	[TestMethod]
	public async Task GetPharmacies_ReturnsListOfPharmacies_WhenThereArePharmacies()
	{
		var request = new GetPharmaciesRequest();
		var getPharmaciesHandler = new GetPharmaciesHandler(this.dbContext);
		var expectedPharmacies = new List<PharmacyListItem>()
		{
			new PharmacyListItem()
			{
				Id = 1, Name = "Pharmacy 1", Address = "Address 1", PostCode = "PostCode 1",
			},
			new PharmacyListItem()
			{
				Id = 2, Name = "Pharmacy 2", Address = "Address 2",
			},
		};

		this.PrepareData();

		var actualResponse = await getPharmaciesHandler.Handle(request, default);

		actualResponse.Pharmacies.Should().BeEquivalentTo(expectedPharmacies);
	}

	[TestMethod]
	public async Task GetPharmacies_ReturnsEmptyListOfPharmacies_WhenThereAreNoPharmacies()
	{
		// Arrange
		var request = new GetPharmaciesRequest();
		var getPharmaciesHandler = new GetPharmaciesHandler(this.dbContext);
		var expectedPharmacies = new List<PharmacyListItem>();

		this.ClearData();

		// Act
		var actualResponse = await getPharmaciesHandler.Handle(request, default);

		// Assert
		actualResponse.Pharmacies.Should().BeEquivalentTo(expectedPharmacies);
	}

	private void PrepareData()
	{
		this.dbContext.Pharmacies.Add(new Pharmacy()
		{
			Name = "Pharmacy 1",
			Address = "Address 1",
			PostCode = "PostCode 1",
		});

		this.dbContext.Pharmacies.Add(new Pharmacy()
		{
			Name = "Pharmacy 2",
			Address = "Address 2",
		});

		this.dbContext.SaveChanges();
	}

	private void ClearData()
	{
		this.dbContext.Pharmacies.RemoveRange(this.dbContext.Pharmacies);
		this.dbContext.SaveChanges();
	}
}
