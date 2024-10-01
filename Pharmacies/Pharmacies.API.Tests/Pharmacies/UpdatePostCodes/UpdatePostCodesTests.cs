using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pharmacies.API.Database;
using Pharmacies.API.Features.Pharmacies.UpdatePostCodes;
using Pharmacies.Database.Entities;
using Pharmacies.PostIt.Client;

namespace Pharmacies.API.Tests.Pharmacies.UpdatePostCodes;

[TestClass]
public class UpdatePostCodesTests
{
	private PharmaciesDbContext dbContext;

	[TestInitialize]
	public void Setup()
	{
		var options = new DbContextOptionsBuilder<PharmaciesDbContext>()
			.UseInMemoryDatabase(databaseName: "UpdatePharmacies")
			.Options;

		this.dbContext = new PharmaciesDbContext(options);
		this.PrepareData();
	}

	[TestMethod]
	public async Task UpdatePharmacyPostCode_UpdatesPharmaciesPostCodes()
	{
		// Arrange
		var postCode = "TestPostCode";
		var expectedPharmacies = new List<Pharmacy>()
		{
			new Pharmacy()
			{
				Id = 1, Name = "Pharmacy 1", Address = "Address 1", PostCode = postCode,
			},
			new Pharmacy()
			{
				Id = 2, Name = "Pharmacy 2", Address = "Address 2", PostCode = postCode,
			},
		};
		var request = new UpdatePostCodesRequest();
		var postCodeClient = new Mock<IPostItClient>();
		postCodeClient.Setup(client => client.GetPostCode(It.IsAny<string>(), default)).ReturnsAsync(postCode);

		var updatePharmacyPostCodeHandler = new UpdatePostCodesHandler(this.dbContext, postCodeClient.Object);

		// Act
		await updatePharmacyPostCodeHandler.Handle(request, default);
		var updatedPharmacies = await this.dbContext.Pharmacies.ToListAsync();

		// Assert
		updatedPharmacies.Should().BeEquivalentTo(expectedPharmacies);
	}


	private void PrepareData()
	{
		this.dbContext.Pharmacies.Add(new Pharmacy()
		{
			Name = "Pharmacy 1",
			Address = "Address 1",
		});

		this.dbContext.Pharmacies.Add(new Pharmacy()
		{
			Name = "Pharmacy 2",
			Address = "Address 2",
			PostCode = "PostCode 2",
		});

		this.dbContext.SaveChanges();
	}
}
