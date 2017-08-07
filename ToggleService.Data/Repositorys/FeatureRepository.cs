using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToggleService.Data.Entities;

namespace ToggleService.Data.Repositorys
{
    public class FeatureRepository: IFeatureRepository
    {
        private readonly FeatureContext _context; 

        public FeatureRepository(FeatureContext ctx)
        {
            _context = ctx;
        }

        public IEnumerable<Feature> GetAllFeatures()
        {
            return _context.Features;
        }

        public IEnumerable<Feature> GetAllEnabledFeatures()
        {
            return _context.Features.Where(x => x.Enabled);
        }

        public Feature GetFeature(int id)
        {
            return _context.Features.Find(id);
        }

        public Feature GetFeature(string description)
        {
            return _context.Features.FirstOrDefault(x => x.Description.ToLower().Contains(description.ToLower()));
        }
    }
}
