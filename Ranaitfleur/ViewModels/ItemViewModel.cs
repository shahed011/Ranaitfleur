using System.ComponentModel.DataAnnotations;

namespace Ranaitfleur.ViewModels
{
    public class ItemViewModel
    {
        [Required]
        [Range(0, 100)]
        public int ItemType { get; set; }
        [Required]
        public string Name { get; set; }
        public int NoOfItemInStock { get; set; } = 5;
        [Required]
        [Range(0, 100000)]
        public int Price { get; set; }
        [Required]
        [StringLength(100000, MinimumLength = 5)]
        public string Description1 { get; set; }
        public string Description2 { get; set; } = string.Empty;
        public float Weight { get; set; } = 0.0f;
        public string Dimentions { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
    }
}
