using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Domain
{
    public class Genre
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
    }
}
