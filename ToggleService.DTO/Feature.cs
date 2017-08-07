using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleService.DTO
{
    public class Feature
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
    }
}
