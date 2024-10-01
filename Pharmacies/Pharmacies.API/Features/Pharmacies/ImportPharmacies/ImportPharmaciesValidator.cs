using FluentValidation;

namespace Pharmacies.API.Features.Pharmacies.ImportPharmacies;

public class ImportPharmaciesValidator : AbstractValidator<ImportPharmaciesRequest>
{
	public ImportPharmaciesValidator()
	{
		this.RuleFor(x => x.File).Must(this.HaveJsonExtension).WithMessage("File should have json extension");
	}

	private bool HaveJsonExtension(IFormFile file)
	{
		var fileExtension = Path.GetExtension(file.FileName);
		return fileExtension.Equals(".json", System.StringComparison.OrdinalIgnoreCase);
	}
}
