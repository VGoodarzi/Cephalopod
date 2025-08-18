namespace Cephalopod.Contracts.Utilities;

public struct Result<TSuccess, TFailure>
    where TSuccess : notnull
    where TFailure : notnull
{
    public TSuccess Value { get; private set; }
    public TFailure Error { get; private set; }

    public bool IsSuccess { get; private init; }
    public bool IsFailure => !IsSuccess;

    public static Result<TSuccess, TFailure> Success(TSuccess value)
    {
        return new Result<TSuccess, TFailure>()
        {
            IsSuccess = true,
            Value = value
        };
    }

    public static Result<TSuccess, TFailure> Failure(TFailure error)
    {
        return new Result<TSuccess, TFailure>()
        {
            IsSuccess = false,
            Error = error
        };
    }

    public static implicit operator Result<TSuccess, TFailure>(TSuccess value) => Success(value);
    public static implicit operator Result<TSuccess, TFailure>(TFailure error) => Failure(error);
}

public struct Result<TFailure>
    where TFailure : notnull
{
    public TFailure Error { get; private set; }

    public bool IsSuccess { get; private init; }
    public bool IsFailure => !IsSuccess;

    public static Result<TFailure> Success()
    {
        return new Result<TFailure>()
        {
            IsSuccess = true
        };
    }

    public static Result<TFailure> Failure(TFailure error)
    {
        return new Result<TFailure>()
        {
            IsSuccess = false,
            Error = error
        };
    }

    public static implicit operator Result<TFailure>(bool value) => Success();
    public static implicit operator Result<TFailure>(TFailure error) => Failure(error);
}