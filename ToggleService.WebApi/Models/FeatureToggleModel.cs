namespace ToggleService.WebApi.Models
{
    public class FeatureToggleModel
    {
        public string AppKey { get; set; }
        public string FeatureName { get; set; }
        public bool Enabled { get; set; }
        public int Version { get; set; }

    }
}
