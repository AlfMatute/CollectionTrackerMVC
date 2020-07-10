using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionTrackerMVC.Models
{
    public class CategoryViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(150)]
        [DisplayName("Category Description")]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
