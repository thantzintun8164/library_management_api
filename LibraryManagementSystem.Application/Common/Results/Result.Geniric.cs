namespace LibraryManagementSystem.Application.Common.Results
{
    public class Result<T> : Result
    {
        public T? Data { get; init; }

        private Result(T? data, bool isSuccess, string message)
            : base(isSuccess, message)
        {
            Data = isSuccess ? data : default;
        }

        public static Result<T> Success(T data, string message = "")
            => new Result<T>(data, true, message);

        public static new Result<T> Fail(string error)
            => new Result<T>(default, false, error);
    }
}
