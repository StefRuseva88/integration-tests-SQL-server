using LibroConsoleAPI.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibroConsoleAPI.Data.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.AuthorMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.ISBNLength)]
        [RegularExpression(ValidationConstants.ISBNRegex)]
        public string ISBN { get; set; } = null!;

        [Range(ValidationConstants.YearPublishedMin,
               ValidationConstants.YearPublishedMax)]
        public int YearPublished { get; set; }

        [Required]
        [StringLength(ValidationConstants.GenreMaxLength)]
        public string Genre { get; set; } = null!;

        [Range(ValidationConstants.PagesMin, int.MaxValue,
               ErrorMessage = "Pages must be greater than 0")]
        public int Pages { get; set; }

        [Range(ValidationConstants.PriceMin, double.MaxValue,
               ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }
    }
}
