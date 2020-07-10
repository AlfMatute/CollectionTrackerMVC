using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionTrackerMVC.Models
{
    public class BrandViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrandId { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Brand Name")]
        public string BrandName { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
    }
}
