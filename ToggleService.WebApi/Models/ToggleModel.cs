using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToggleService.Data.Entities;

namespace ToggleService.WebApi.Models
{
    public class ToggleModel
    {
        [Required]
        public string AppKey { get; set; }
        public List<Feature> Features { get; set; }
    }
}
