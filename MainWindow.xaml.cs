using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace LibraryCatalogParser
{
    /// <summary>
    /// Главное окно приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Book> books;

        public string xmlFilePath = "C:\\Users\\Lexa\\Desktop\\Lab1oop\\Books.xml";

        public MainWindow()
        {
            InitializeComponent();
            books = new List<Book>();
            dataGrid.ItemsSource = books;
        }

        /// <summary>
        /// Загрузка данных из XML
        /// </summary>
        public void LoadBooksFromXml()
        {
            books.Clear(); // Очистка существующих книг

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            foreach (XmlNode bookNode in xmlDoc.DocumentElement.ChildNodes)
            {
                // Создание нового экземпляра Book
                Book book = new Book();

                // Заполнение свойства Title из элемента <Title>
                book.Title = bookNode.SelectSingleNode("Title").InnerText;

                // Заполнение свойства Author из элемента <Author>
                book.Author = bookNode.SelectSingleNode("Author").InnerText;

                // Заполнение свойства Genre из элемента <Genre>
                // Преобразование строки в значение перечисления Genre
                book.Genre = (Genre)Enum.Parse(typeof(Genre), bookNode.SelectSingleNode("Genre").InnerText);

                // Заполнение свойства Year из элемента <Year>
                // Преобразование строки в целочисленное значение
                book.Year = int.Parse(bookNode.SelectSingleNode("Year").InnerText);

                // Заполнение свойства Publisher из элемента <Publisher>
                book.Publisher = bookNode.SelectSingleNode("Publisher").InnerText;

                // Заполнение свойства PageCount из элемента <PageCount>
                // Преобразование строки в целочисленное значение
                book.PageCount = int.Parse(bookNode.SelectSingleNode("PageCount").InnerText);

                // Заполнение свойства Quantity из элемента <Quantity>
                // Преобразование строки в целочисленное значение
                book.Quantity = int.Parse(bookNode.SelectSingleNode("Quantity").InnerText);

                // Добавление созданной книги в список books
                books.Add(book);
            }

            // Обновление DataGrid после загрузки книг
            dataGrid.Items.Refresh(); // Обновление DataGrid после загрузки книг
        }
        /// <summary>
        /// Сохранение данных в XML
        /// </summary>
        public void SaveBooksToXml()
        {
            // Создание нового XML-документа
            XmlDocument xmlDoc = new XmlDocument();

            // Создание корневого элемента
            XmlElement rootElement = xmlDoc.CreateElement("Books");
            xmlDoc.AppendChild(rootElement);

            // Добавление элементов книг в корневой элемент
            foreach (Book book in books)
            {
                // Создание элемента <Book> и добавление его в XML-документ
                XmlElement bookElement = xmlDoc.CreateElement("Book");
                rootElement.AppendChild(bookElement);

                // Создание элемента <Title> и добавление его в <Book>
                XmlElement titleElement = xmlDoc.CreateElement("Title");
                titleElement.InnerText = book.Title;
                bookElement.AppendChild(titleElement);

                // Создание элемента <Author> и добавление его в <Book>
                XmlElement authorElement = xmlDoc.CreateElement("Author");
                authorElement.InnerText = book.Author;
                bookElement.AppendChild(authorElement);

                // Создание элемента <Genre> и добавление его в <Book>
                XmlElement genreElement = xmlDoc.CreateElement("Genre");
                genreElement.InnerText = book.Genre.ToString();
                bookElement.AppendChild(genreElement);

                // Создание элемента <Year> и добавление его в <Book>
                XmlElement yearElement = xmlDoc.CreateElement("Year");
                yearElement.InnerText = book.Year.ToString();
                bookElement.AppendChild(yearElement);

                // Создание элемента <Publisher> и добавление его в <Book>
                XmlElement publisherElement = xmlDoc.CreateElement("Publisher");
                publisherElement.InnerText = book.Publisher;
                bookElement.AppendChild(publisherElement);

                // Создание элемента <PageCount> и добавление его в <Book>
                XmlElement pageCountElement = xmlDoc.CreateElement("PageCount");
                pageCountElement.InnerText = book.PageCount.ToString();
                bookElement.AppendChild(pageCountElement);

                // Создание элемента <Quantity> и добавление его в <Book>
                XmlElement quantityElement = xmlDoc.CreateElement("Quantity");
                quantityElement.InnerText = book.Quantity.ToString();
                bookElement.AppendChild(quantityElement);
            }

            // Сохранение XML-документа по текущему пути xmlFilePath
            xmlDoc.Save(xmlFilePath);
        }

        /// <summary>
        /// Обработчик нажатия кнопки добавления книги
        /// </summary>
        public void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка данных перед добавлением новой книги
            if (ValidateData())
            {
                try
                {
                    // Обновление DataGrid
                    dataGrid.Items.Refresh();
                   
                    // Сохранение книг в XML
                    SaveBooksToXml();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении книг: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста введите валидные данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Проверка введенных данных
        /// </summary>
        public bool ValidateData()
        {
            foreach (Book book in books)
            {
                // Проверка, является ли заголовок пустым или состоящим только из пробелов
                if (string.IsNullOrWhiteSpace(book.Title) ||
                    // Проверка, является ли автор пустым или состоящим только из пробелов
                    string.IsNullOrWhiteSpace(book.Author) ||
                    // Проверка, является ли жанр пустым или состоящим только из пробелов
                    string.IsNullOrWhiteSpace(book.Genre.ToString()) ||
                    // Проверка, является ли год издания неположительным числом
                    book.Year <= 0 ||
                    // Проверка, является ли издатель пустым или состоящим только из пробелов
                    string.IsNullOrWhiteSpace(book.Publisher) ||
                    // Проверка, является ли количество страниц неположительным числом
                    book.PageCount <= 0 ||
                    // Проверка, является ли количество книг в наличии неположительным числом
                    book.Quantity <= 0)
                {
                    // Если хотя бы одно из условий не выполняется, возвращается false
                    return false;
                }
            }

            // Если все книги в списке проходят проверку, возвращается true
            return true;
        }
        /// <summary>
        /// Выбор XML-файла
        /// </summary>
        public void SelectXmlFile()
        {
            // Создание нового диалогового окна для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Установка фильтра для показа только XML-файлов
            openFileDialog.Filter = "XML files (*.xml)|*.xml";

            // Установка начальной директории для диалогового окна
            openFileDialog.InitialDirectory = @"C:\Users\Lexa\Desktop\Lab1oop\";

            // Открытие диалогового окна для выбора файла
            if (openFileDialog.ShowDialog() == true)
            {
                // Получение пути выбранного XML-файла
                xmlFilePath = openFileDialog.FileName;

                // Загрузка книг из XML-файла
                LoadBooksFromXml();

                // Установка источника данных для DataGrid
                dataGrid.ItemsSource = books;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки выбора XML-файла
        /// </summary>
        public void SelectXmlFileButton_Click(object sender, RoutedEventArgs e)
        {
            SelectXmlFile();
        }
    }
}