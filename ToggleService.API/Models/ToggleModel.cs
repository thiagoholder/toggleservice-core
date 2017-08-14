using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToggleService.DataMongoDB.Entities;

namespace ToggleService.API.Models
{
    public class ToggleModel
    {
           public string AppKey { get; set; }
           public List<Feature> Features { get; set; }
    }
}