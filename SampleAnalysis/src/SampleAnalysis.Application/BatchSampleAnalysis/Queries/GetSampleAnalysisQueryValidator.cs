namespace SampleAnalysis.Application.BatchSampleAnalysis.Queries;

using FluentValidation;

public class GetSampleAnalysisQueryValidator : AbstractValidator<GetSampleAnalysisQuery>
{
    public GetSampleAnalysisQueryValidator()
    {
        RuleFor(q => q.BatchNumber)
            .Matches(@"^[0-9]*\-[0-9]*$")
            .WithMessage("Invalid batch number string");
        RuleFor(q => q.EndDate)
            .GreaterThan(q => q.BeginDate)
            .WithMessage("The end date must not be less than the start date");
    }
}