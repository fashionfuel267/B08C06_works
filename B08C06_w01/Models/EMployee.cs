using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B08C06_w01.Models
{
    public class EMployee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Email { get; set; }
        [StringLength(150)]
        public string? PicturePath { get; set; }
        [NotMapped]
        public IFormFile? Picture { get; set; }
    }
}
