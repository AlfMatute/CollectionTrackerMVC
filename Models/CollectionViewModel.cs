using System;
using System.Collections;
using System.Collections.Generic;
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
        public string ArticleName { get; set; }

        [Required]
        public DateTime AcquisitionDate { get; set; }

        public double ArticleCost { get; set; }

        //[Required]
       // public ConditionViewModel Condition { get; set; }
        public IEnumerable<ConditionViewModel> AllConditions { get; set; }
        [Required]
        public int ConditionId { get; set; }

        public DateTime FabricationDate { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> AllCategories { get; set; }

        [Required]
        public int BrandId { get; set; }
        public IEnumerable<BrandViewModel> AllBrands { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public CollectionUser CollectionUser { get; set; }
    }
}
