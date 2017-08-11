using System.ComponentModel.DataAnnotations;

namespace ToggleService.API.Models
{
    public class FeatureModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Version { get; set; }
    }
}