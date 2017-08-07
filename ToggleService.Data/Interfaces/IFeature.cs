using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleService.Data
{
    public interface IFeature
    {
        bool Enabled { get; set; }
    }
}
