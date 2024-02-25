namespace LibraryCatalogParser
{
    /// <summary>
    /// Класс, представляющий книгу в библиотечном каталоге
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Заголовок книги
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Автор книги
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Жанр книги
        /// </summary>
        public Genre Genre { get; set; }

        /// <summary>
        /// Год издания книги
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Издатель книги
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Количество страниц в книге
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Количество книг в наличии
        /// </summary>
        public int Quantity { get; set; }
    }
}