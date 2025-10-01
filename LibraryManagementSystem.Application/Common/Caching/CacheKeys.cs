namespace LibraryManagementSystem.Application.Common.Caching
{
    public static class CachingKeys
    {
        public static class Users
        {
            public const string All = "AllUsers";
        }
        public static class Authors
        {
            public const string All = "AllAuthors";
            public const string ByCategoryName = "AuthorsByCategoryName";
            public static string ById(int id) => $"AuthorById{id}";
        }
        public static class Books
        {
            public const string All = "AllBooks";
            public const string Available = "AllAvailableBooks";
            public const string ByCategoryName = "BooksByCategoryName";
            public const string ByAuthorName = "BooksByAuthorName";
            public static string ById(int id) => $"BookById_{id}";
        }
        public static class Categories
        {
            public const string All = "AllCategories";
            public static string ById(int id) => $"CategoryById{id}";
        }
        public static class BorrowedBook
        {
            public const string AllOverDue = "AllOverDueBorrowedBooks";
            public const string AllReturned = "AllReturnedBorrowedBooks";
            public const string ByUserId = "BorrowedBooksByUserId";
        }
    }
}
