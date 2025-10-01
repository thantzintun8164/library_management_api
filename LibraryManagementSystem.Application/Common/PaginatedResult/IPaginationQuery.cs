namespace LibraryManagementSystem.Application.Common.PaginatedResult
{
    public interface IPaginationQuery
    {
        int PageNumber { get; }
        int PageSize { get; }
        string BaseUrl { get; }
    }

}
