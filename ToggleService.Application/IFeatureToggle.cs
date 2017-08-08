using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToggleService.Application
{
    public interface IFeatureToggle
    {
        bool Disabled(string feature);
        bool Enabled(string feature);
    }
}
