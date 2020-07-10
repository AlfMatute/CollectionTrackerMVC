using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionTrackerMVC.Models
{
    public class ConditionViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConditionId { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Condition Description")]
        public string Description { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
