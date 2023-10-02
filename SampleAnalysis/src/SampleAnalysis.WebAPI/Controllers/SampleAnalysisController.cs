namespace SampleAnalysis.WebAPI.Controllers;

using Mediator;

using Microsoft.AspNetCore.Mvc;

using SampleAnalysis.Application.BatchSampleAnalysis.Queries;
using SampleAnalysis.WebAPI.Filters;

[Route("api/[controller]")]
[ApiController]
[ApiExceptionFilter]
public class SampleAnalysisController : ControllerBase
{
    private readonly IMediator _mediator;

    public SampleAnalysisController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<SampleAnalysisResultDto>> Get(string? batchNumber, DateTime? beginDate,
        DateTime? endDate)
    {
        var query = new GetSampleAnalysisQuery
        {
            BatchNumber = batchNumber,
            BeginDate = beginDate,
            EndDate = endDate
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }
}