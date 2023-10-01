namespace SampleAnalysis.Application.BatchSampleAnalysis.Queries;

public class SampleAnalysisResultDto
{
    public IEnumerable<string> SampleParameters { get; set; }
    public IEnumerable<SampleAnalysisDto> SampleResults { get; set; }
}