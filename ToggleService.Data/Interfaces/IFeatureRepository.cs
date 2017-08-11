using System;
using System.Linq;
using System.Linq.Expressions;
using ToggleService.Data.Entities;
using ToggleService.Data.Interfaces;

namespace ToggleService.Data
{
    public interface IFeatureRepository: IRepository<Feature>
    {
    }
}
