using System.ComponentModel.DataAnnotations;

namespace esqhinkamil.Book.ViewModel
{
    public class UpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public decimal? Price { get; set; }
    }
}
