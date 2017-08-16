using MongoDB.Bson.Serialization.Attributes;

namespace ToggleService.Data.Entities
{
    public class Feature
    {
        [BsonElement("feature_name")]
        public string Name { get; set; }
        [BsonElement("feature_version")]
        public int Version { get; set; }
        [BsonElement("feature_enabled")]
        public bool Enabled { get; set; }
    }
}
