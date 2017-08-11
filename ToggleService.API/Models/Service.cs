using System.ComponentModel.DataAnnotations;

namespace ToggleService.API.Models
{
    public class ServiceModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}