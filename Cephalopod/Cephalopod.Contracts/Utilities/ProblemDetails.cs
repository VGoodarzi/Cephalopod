using System.Net;

namespace Cephalopod.Contracts.Utilities;

public class ProblemDetails
{
    public required string Type { get; set; }

    public required string Title { get; set; }

    public int Status { get; set; }

    public required string Detail { get; set; }

    public required string Instance { get; set; }

    public string? TraceId { get; set; }
    public IDictionary<string, object?> Extensions { get; set; }
        = new Dictionary<string, object?>(StringComparer.Ordinal);


    private const string TypeBasedOn = "https://www.rfc-editor.org/rfc/rfc9457.html";
    public static ProblemDetails Error(ITranslator translator, string requestedUrl, HttpStatusCode status)
    {
        return status switch
        {
            HttpStatusCode.Unauthorized => Unauthorized(requestedUrl, (int)status),
            HttpStatusCode.Forbidden => Forbidden(requestedUrl, (int)status),
            HttpStatusCode.InternalServerError => ServerError(requestedUrl, (int)status),
            _ => ServerError(requestedUrl, (int)status),
        };
    }

    public static ProblemDetails Unauthorized(string requestedUrl, int status)
    {
        return new()
        {
            Type = TypeBasedOn,
            Detail = "User is unauthorized",
            Instance = requestedUrl,
            Title = "Unauthorized",
            Status = status
        };
    }

    public static ProblemDetails Forbidden(string requestedUrl, int status)
    {
        return new()
        {
            Type = TypeBasedOn,
            Detail = "User has no permission to access this route",
            Instance = requestedUrl,
            Title = "Forbidden",
            Status = status
        };
    }

    public static ProblemDetails ServerError(string requestedUrl, int status)
    {
        return new()
        {
            Type = TypeBasedOn,
            Detail = "Something went wrong",
            Instance = requestedUrl,
            Title = "ServerError",
            Status = status
        };
    }
}