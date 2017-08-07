using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleService.Data.Entities
{
    public class Feature : IFeature
    {

        public int IdFeature { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
    }
}
