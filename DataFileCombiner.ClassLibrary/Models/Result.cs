namespace DataFileCombiner.ClassLibrary.Models;
public class Result<T>
{
    public Result(bool isSuccess, T? value)
    {
        IsSuccess = isSuccess;
        Value = value;
    }
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public T? Value { get; }

    public static Result<T> Failure() => new(false, value: default);

    public static Result<T> Success(T value) => new(true, value);

}
