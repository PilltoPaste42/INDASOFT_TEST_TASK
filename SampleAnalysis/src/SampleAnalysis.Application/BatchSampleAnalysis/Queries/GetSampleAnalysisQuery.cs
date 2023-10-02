namespace SampleAnalysis.Application.BatchSampleAnalysis.Queries;

using Mediator;

using SampleAnalysis.Application.Common.Interfaces;

public record GetSampleAnalysisQuery : IQuery<SampleAnalysisResultDto>

{
    public string? BatchNumber { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
}


public class GetSampleAnalysisQueryHandler : IQueryHandler<GetSampleAnalysisQuery, SampleAnalysisResultDto>
{
    private readonly IAppDbContext _context;

    public GetSampleAnalysisQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async ValueTask<SampleAnalysisResultDto> Handle(GetSampleAnalysisQuery query,
        CancellationToken cancellationToken)
    {
        var batchType = _context.EventFrameTypes.Single(t => t.Name == "Партия");
        var sampleType = _context.EventFrameTypes.Single(t => t.Name == "Образец");

        var batchNumberTypeValue = _context.EventFrameTypeValues
            .Single(t => t.Name == "Номер партии" && t.EventFrameType == batchType);
        var batchProdDateTypeValue = _context.EventFrameTypeValues
            .Single(t => t.Name == "Дата изготовления" && t.EventFrameType == batchType);
        var sampleTypeValue = _context.EventFrameTypeValues
            .Where(tv => tv.Name.Contains("%") && tv.EventFrameType == sampleType);

        var sampleResults =
            from link in _context.Links
            join batchF in _context.EventFrames
                on new { a = batchType, b = link.ParentEventFrame }
                equals new { a = batchF.EventFrameType, b = batchF }
            join sampleF in _context.EventFrames
                on new { a = sampleType, b = link.ChildEventFrame }
                equals new { a = sampleF.EventFrameType, b = sampleF }
            join batchNumberV in _context.EventFrameValues
                on new { a = batchF, b = batchNumberTypeValue }
                equals new { a = batchNumberV.EventFrame, b = batchNumberV.Userfield }
            join batchProdDateV in _context.EventFrameValues
                on new { a = batchF, b = batchProdDateTypeValue }
                equals new { a = batchProdDateV.EventFrame, b = batchProdDateV.Userfield }
            join sampleTV in sampleTypeValue
                on sampleType equals sampleTV.EventFrameType
            join sampleV in _context.EventFrameValues
                on new { a = sampleF, b = sampleTV }
                equals new { a = sampleV.EventFrame, b = sampleV.Userfield }
            where
                batchNumberV.ValueText == (query.BatchNumber ?? batchNumberV.ValueText)
                && batchProdDateV.ValueDatetime >= (query.BeginDate ?? batchProdDateV.ValueDatetime)
                && batchProdDateV.ValueDatetime <= (query.EndDate ?? batchProdDateV.ValueDatetime)
            group new
            {
                BatchNumber = batchNumberV.ValueText,
                SampleValue = sampleV.ValueFloat
            } by batchNumberV.ValueText
            into res
            select new SampleAnalysisDto
            {
                BatchNumber = res.Key,
                SampleValues = res.Where(r => r.BatchNumber == res.Key)
                    .Select(r => r.SampleValue)
            };

        var result = new SampleAnalysisResultDto
        {
            SampleParameters = sampleTypeValue.Select(tv => tv.Name),
            SampleResults = sampleResults
        };

        return await new ValueTask<SampleAnalysisResultDto>(result);

    }
}