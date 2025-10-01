namespace LibraryManagementSystem.Application.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; init; }
        public string Message { get; init; } = string.Empty;

        protected Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public static Result Success(string message = "")
            => new Result(true, message);

        public static Result Fail(string error)
            => new Result(false, error);
    }
}
