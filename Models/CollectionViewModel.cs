using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionTrackerMVC.Models
{
    public class CollectionViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollectionId { get; set; }

        [Required]
        [MaxLength(250)]
        [DisplayName("Item name")]
        public string ArticleName { get; set; }

        [Required]
        [DisplayName("Acquisition Date")]
        public DateTime AcquisitionDate { get; set; }

        [DisplayName("Cost")]
        public double ArticleCost { get; set; }

        public ConditionViewModel Condition { get; set; }
        public IEnumerable<ConditionViewModel> AllConditions { get; set; }
        [Required]
        [DisplayName("Condition")]
        public int ConditionId { get; set; }

        [DisplayName("Fabrication Date")]
        public DateTime FabricationDate { get; set; }

        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public CategoryViewModel Category { get; set; }
        public IEnumerable<CategoryViewModel> AllCategories { get; set; }

        [Required]
        [DisplayName("Brand")]
        public int BrandId { get; set; }
        public BrandViewModel Brand { get; set; }
        public IEnumerable<BrandViewModel> AllBrands { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public CollectionUser CollectionUser { get; set; }

        public string UserEmail { get; set; }
    }
}
