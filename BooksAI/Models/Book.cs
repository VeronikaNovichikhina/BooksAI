using System.ComponentModel.DataAnnotations;


namespace BooksAI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Выбор категории обязательный")]
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Ввести название книги")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ\s]+$", ErrorMessage = "Название книги должно содержать только буквы и пробелы.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Ввести автора книги")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым"), Range(0, 10000, ErrorMessage = "Цена должна быть от 0 до 10000"),]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым"), Range(0, 10000, ErrorMessage = "Год должен быть от 0 до 10000"),]
        public int Year { get; set; }


        [Required(ErrorMessage = "Ввести описание книги")]
        public string Description { get; set; }

        public byte[]? ImageData { get; set; }

        public string? ImageMimeType { get; set; }

    }

    public enum Genre
    {
        Fiction,
        NonFiction,
        Mystery,
        Fantasy,
        ScienceFiction,
        Biography,
        Romance,
        Thriller,
        Horror,
        HistoricalFiction
    }
}
