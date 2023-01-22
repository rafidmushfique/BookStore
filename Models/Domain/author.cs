using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Domain
{
    public class author
    {
        public int id { get; set; }
        [Required]
        public string authorname { get; set; }
    }
}
