using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToggleService.API.Models
{
    public class ServiceDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ToggleModel> Toggles { get; set; }
    }
}