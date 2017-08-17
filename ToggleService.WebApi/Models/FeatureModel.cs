using System.ComponentModel.DataAnnotations;

namespace ToggleService.WebApi.Models
{
    public class FeatureModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Enabled { get; set; }
        [Required]
        public int Version { get; set; }
    }
}
