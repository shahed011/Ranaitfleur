using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ranaitfleur.ViewModels
{
    public class ItemViewModel
    {
        [Required]
        public int Id { get; set; }
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

        [Required]
        public int Size { get; set; }
        public List<SelectListItem> Sizes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "6", Text = "6"},
            new SelectListItem { Value = "8", Text = "8"},
            new SelectListItem { Value = "10", Text = "10"},
            new SelectListItem { Value = "12", Text = "12"}
        };
    }
}
