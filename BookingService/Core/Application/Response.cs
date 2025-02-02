using System.Text.Json.Serialization;

namespace Application;

public enum ErrorCodes
{
    NOT_FOUND = 1,
    COULD_NOT_STORE_DATA = 2,
    INVALID_PERSON_ID = 3,
    MISSING_REQUIRED_INFORMATION = 4,
    INVALID_EMAIL = 5,
}

public abstract class Response<T>
{
    public bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ErrorCodes? ErrorCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }
}
