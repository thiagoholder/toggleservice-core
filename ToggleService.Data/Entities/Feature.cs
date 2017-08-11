using ToggleService.Data.Interfaces;

namespace ToggleService.Data.Entities
{
    public class Feature: Entity
    {
        public string Description { get; set; }
        public int Version { get; set; }
        public string Type { get; set; }
    }
}
