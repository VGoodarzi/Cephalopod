using System.Net;

namespace Cephalopod.Contracts.Utilities;

public class ProblemDetails
{
    public required string Type { get; init; }

    public required string Title { get; init; }

    public int Status { get; init; }

    public required string Detail { get; init; }

    public required string Instance { get; init; }

    public string? TraceId { get; init; }
    public IDictionary<string, object?> Extensions { get; init; }
        = new Dictionary<string, object?>(StringComparer.Ordinal);


    private const string TypeBasedOn = "https://www.rfc-editor.org/rfc/rfc9457.html";
    public static ProblemDetails Error(ITranslator translator, string requestedUrl, HttpStatusCode status)
    {
        return status switch
        {
            HttpStatusCode.Unauthorized => Unauthorized(requestedUrl),
            HttpStatusCode.Forbidden => Forbidden(requestedUrl),
            HttpStatusCode.InternalServerError => ServerError(requestedUrl, (int)status),
            HttpStatusCode.NoContent => NoContent(requestedUrl),
            HttpStatusCode.NotFound => NotFound(requestedUrl),
            _ => ServerError(requestedUrl, (int)status),
        };
    }

    public static ProblemDetails Unauthorized(string requestedUrl)
    {
        return new()
        {
            Type = TypeBasedOn,
            Detail = "User is unauthorized",
            Instance = requestedUrl,
            Title = "Unauthorized",
            Status = 401
        };
    }

    public static ProblemDetails Forbidden(string requestedUrl)
    {
        return new()
        {
            Type = TypeBasedOn,
            Detail = "User has no permission to access to this requested data",
            Instance = requestedUrl,
            Title = "Forbidden",
            Status = 403
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

    public static ProblemDetails NoContent(string requestedUrl)
    {
        return new()
        {
            Type = TypeBasedOn,
            Detail = "Request has valid, but no data exists for that.",
            Instance = requestedUrl,
            Title = "NoContent",
            Status = 204
        };
    }

    public static ProblemDetails NotFound(string requestedUrl)
    {
        return new()
        {
            Type = TypeBasedOn,
            Detail = "Request has valid, but no data exists for that.",
            Instance = requestedUrl,
            Title = "NoContent",
            Status = 404
        };
    }
}