namespace SampleAnalysis.Application.BatchSampleAnalysis.Queries;

public class SampleAnalysisDto
{
    public string BatchNumber { get; set; }
    public IEnumerable<double?> SampleValues { get; set; }
}