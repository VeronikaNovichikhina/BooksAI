namespace BooksAI.Models.ViewModel
{
    public class BookFilterModel
    {
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public string? Search { get; set; }
    }

}
