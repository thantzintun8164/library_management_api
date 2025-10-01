namespace LibraryManagementSystem.Application.Common.PaginatedResult
{
    public class PaginatedResult<T> where T : class
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string? NextPageUrl { get; set; }
        public string? PrevPageUrl { get; set; }

        private PaginatedResult() { }

        public static PaginatedResult<T> Create(List<T> items, int totalCount, int currentPage, int pageSize, string baseUrl)
        {
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PaginatedResult<T>
            {
                Items = items,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                NextPageUrl = currentPage < totalPages
                    ? BuildPageUrl(baseUrl, currentPage + 1, pageSize) : null,
                PrevPageUrl = currentPage > 1
                    ? BuildPageUrl(baseUrl, currentPage - 1, pageSize) : null
            };
        }
        private static string BuildPageUrl(string baseUrl, int pageNumber, int pageSize)
        {
            return $"{baseUrl}?pageNumber={pageNumber}&pageSize={pageSize}";
        }
    }

}
