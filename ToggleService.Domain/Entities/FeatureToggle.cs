namespace ToggleService.Domain
{
    public class FeatureToggle
    {
        public int IdFeature { get; set; }
        public int IdService { get; set; }
     
        public bool Enabled { get; set; }
        public virtual Feature Feature { get; set; }
        public virtual Service Service { get; set; }
    }
}
