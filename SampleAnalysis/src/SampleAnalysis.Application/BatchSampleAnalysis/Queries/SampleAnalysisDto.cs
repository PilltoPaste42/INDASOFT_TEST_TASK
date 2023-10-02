namespace SampleAnalysis.Application.BatchSampleAnalysis.Queries;

public class SampleAnalysisDto
{
    public string BatchNumber { get; set; }
    public IEnumerable<string> Parameters { get; set; }
    public IEnumerable<double?> Values { get; set; }
}