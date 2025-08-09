using System.ComponentModel.DataAnnotations.Schema;

namespace B08C06_w01.Models
{
    public class EMployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PicturePath { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
    }
}
