using FluentValidation;
using MediatR;

namespace Pharmacies.API.Infrastructure;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> validators;

	public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
	{
		this.validators = validators;
	}

	public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		foreach (var validator in this.validators)
		{
			validator.ValidateAndThrow(request);
		}

		return next();
	}
}
