using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ToggleService.Data.Entities;

namespace ToggleService.Data.Repositorys
{
    public class FeatureRepository : Repositoy<Feature>, IFeatureRepository
    {
        public FeatureRepository(FeatureContext ctx) : base(ctx)
        {
        }
    }
}
