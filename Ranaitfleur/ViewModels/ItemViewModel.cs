using System.ComponentModel.DataAnnotations;

namespace Ranaitfleur.ViewModels
{
    public class ItemViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
