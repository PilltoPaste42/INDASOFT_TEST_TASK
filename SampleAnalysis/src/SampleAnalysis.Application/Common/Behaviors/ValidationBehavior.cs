namespace SampleAnalysis.Application.Common.Behaviors;

using FluentValidation;

using Mediator;

using SampleAnalysis.Application.Common.Exceptions;

public class ValidationBehavior<TMessage, TResponse> : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
{
    private readonly IEnumerable<IValidator> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TMessage>> validators)
    {
        _validators = validators;
    }

    public async ValueTask<TResponse> Handle(TMessage message, CancellationToken cancellationToken,
        MessageHandlerDelegate<TMessage, TResponse> next)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TMessage>(message);

            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
            {
                throw new AppValidationException(failures);
            }
        }

        return await next(message, cancellationToken);
    }
}