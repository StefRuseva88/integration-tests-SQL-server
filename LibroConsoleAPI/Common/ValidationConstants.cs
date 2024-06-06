namespace LibroConsoleAPI.Common
{
    public static class ValidationConstants
    {
        public const int TitleMaxLength = 255;
        public const int AuthorMaxLength = 100;
        public const string ISBNRegex = @"^(\d{13})$";
        public const int ISBNLength = 13;
        public const int YearPublishedMin = 1700;
        public const int YearPublishedMax = 2024;
        public const int GenreMaxLength = 50;
        public const int PagesMin = 1;
        public const double PriceMin = 0.01;
    }
}
