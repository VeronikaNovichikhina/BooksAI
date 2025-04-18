﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BooksAI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public Genre Genre { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }
        public decimal Price { get; set; }

        public string Year { get; set; }

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
