using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Domain
{
    public class Publisher
    {
        public int id { get; set; }
        [Required]
        public string publishername { get; set; }
    }
}

