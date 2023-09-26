namespace SampleAnalysis.Application.BatchSampleAnalysis.Queries;

using Mediator;

using SampleAnalysis.Application.Common.Interfaces;

public record GetSampleAnalysisQuery : IQuery<IEnumerable<SampleAnalysisDto>>
{
    public string? BatchNumber { get; set; }
    public DateTime? BeginDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GetSampleAnalysisQueryHandler : IQueryHandler<GetSampleAnalysisQuery, IEnumerable<SampleAnalysisDto>>
{
    private readonly IAppDbContext _context;

    public GetSampleAnalysisQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async ValueTask<IEnumerable<SampleAnalysisDto>> Handle(GetSampleAnalysisQuery query,
        CancellationToken cancellationToken)
    {
        var result =
            (
                from links in _context.Links
                join batchT in _context.EventFrameTypes
                    on new { a = "Партия", b = links.ParentEventFrameTypeId }
                    equals new { a = batchT.Name, b = batchT.Id }
                join sampleT in _context.EventFrameTypes
                    on new { a = "Образец", b = links.ChildEventFrameTypeId }
                    equals new { a = sampleT.Name, b = sampleT.Id }
                join batchNumberTV in _context.EventFrameTypeValues
                    on new { a = "Номер партии", b = batchT.Id }
                    equals new { a = batchNumberTV.Name, b = batchNumberTV.EventFrameTypeId }
                join batchProdDateTV in _context.EventFrameTypeValues
                    on new { a = "Дата изготовления", b = batchT.Id }
                    equals new { a = batchProdDateTV.Name, b = batchProdDateTV.EventFrameTypeId }
                join sampleTV in _context.EventFrameTypeValues
                    on new { a = true, b = sampleT.Id }
                    equals new { a = sampleTV.Name.Contains("%"), b = sampleTV.EventFrameTypeId }
                join batches in _context.EventFrames
                    on new { a = batchT.Id, b = links.ParentEventFrameId }
                    equals new { a = batches.EventFrameTypeId, b = batches.Id }
                join samples in _context.EventFrames
                    on new { a = sampleT.Id, b = links.ChildEventFrameId }
                    equals new { a = samples.EventFrameTypeId, b = samples.Id }
                join batchNumber in _context.EventFrameValues
                    on new { a = batches.Id, b = batchNumberTV.Id }
                    equals new { a = batchNumber.EventFrameId, b = batchNumber.UserfieldId }
                join batchProdDate in _context.EventFrameValues
                    on new { a = batches.Id, b = batchProdDateTV.Id }
                    equals new { a = batchProdDate.EventFrameId, b = batchProdDate.UserfieldId }
                join sampleV in _context.EventFrameValues
                    on new { a = samples.Id, b = sampleTV.Id }
                    equals new { a = sampleV.EventFrameId, b = sampleV.UserfieldId }
                where
                    batchNumber.ValueText == (query.BatchNumber ?? batchNumber.ValueText)
                    && batchProdDate.ValueDatetime >= (query.BeginDate ?? batchProdDate.ValueDatetime)
                    && batchProdDate.ValueDatetime <= (query.EndDate ?? batchProdDate.ValueDatetime)
                group new
                    {
                        BatchNumber = batchNumber.ValueText,
                        Parameter = sampleTV.Name,
                        Value = sampleV.ValueFloat
                    }
                    by batchNumber.ValueText
                into res
                select new SampleAnalysisDto
                {
                    BatchNumber = res.Key,
                    Parameters = from r in res select r.Parameter,
                    Values = from r in res select r.Value
                }
            )
            .OrderBy(q => q.BatchNumber);


        return await new ValueTask<IEnumerable<SampleAnalysisDto>>(result);
    }
}