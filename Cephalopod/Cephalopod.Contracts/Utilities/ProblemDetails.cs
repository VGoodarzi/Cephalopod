namespace Cephalopod.Contracts.Utilities;

public class ProblemDetails
{
    public required string Type { get; set; }

    public required string Title { get; set; }

    public int Status { get; set; }

    public required string Detail { get; set; }
    
    public required string Instance { get; set; }

    public required string TraceId { get; set; }
    public IDictionary<string, object?> Extensions { get; set; } 
        = new Dictionary<string, object?>(StringComparer.Ordinal);
}