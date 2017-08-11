﻿using ToggleService.Data.Entities;
using ToggleService.Data.Interfaces;
using ToggleService.Domain;

namespace ToggleService.Data.Repositorys
{
    public class FeatureRepository : Repositoy<Feature>, IFeatureRepository
    {
        public FeatureRepository(FeatureContext ctx) : base(ctx)
        {
        }
    }
}
