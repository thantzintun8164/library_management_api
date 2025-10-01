namespace LibraryManagementSystem.API.Common.Responses
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public IDictionary<string, string[]>? Errors { get; set; }
    }
}
