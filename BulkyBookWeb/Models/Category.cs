using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {

        //Data Annotations

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //Display Name Range Validations
        [DisplayName("Display Order Range(Max-150)")]
        [Range(1, 150, ErrorMessage = "Display Order must be between 1 and 100 only!!!")]
        public int DisplayOrder { get; set; }

        public DateTime CreateedDateTime { get; set; } = DateTime.Now;
    }
}
