namespace ToggleService.Data.Entities
{
    public class Feature : IFeature
    {
        public int IdFeature { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public string Type { get; set; }
    }
}
