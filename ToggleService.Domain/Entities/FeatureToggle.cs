namespace ToggleService.Domain
{
    public class FeatureToggle: Entity
    {
        public int IdFeature { get; set; }
        public int IdService { get; set; }
        public Feature Feature { get; set; }
        public Service Service { get; set; }
        public bool Enabled { get; set; }
    }
}
