namespace ToggleService.Data
{
    public interface IFeature
    {
        bool Enabled { get; set; }
        string Type { get; set; }
    }
}
