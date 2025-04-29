using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Application.Features.Identity.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse>(
		IEnumerable<IValidator<TRequest>> _validators) : IPipelineBehavior<TRequest, TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (_validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);
				var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
				var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

				if (failures.Count != 0)
				{
					var message = failures.Select(X => $"{X.PropertyName}" + ": " + X.ErrorMessage).FirstOrDefault();

					throw new ValidationException(message);

				}
			}
			return await next();
		}
	}
}
