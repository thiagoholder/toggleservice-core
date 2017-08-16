using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToggleService.Data.Entities
{
    public class Toggle
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("app_name")]
        public string AppName { get; set; }
        [BsonElement("toggle_update")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        [BsonElement("toggle_created")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [BsonElement("features")]
        public List<Feature> Features { get; set; }

        public void AddFeature(Feature feature)
        {
            var existingFeature = FindFeature(x => string.Equals(x.Name, feature.Name));
            if (existingFeature == null)
            {
                Features.Add(feature);
            }

        }

        public void RemoveFeature(Feature featureItem)
        {
            if (FindFeature(x => featureItem != null && x == featureItem) == null) return;
            Features.Remove(featureItem);
        }


        public Feature FindFeature(Func<Feature, bool> expression)
        {
            return Features.Where(expression).FirstOrDefault();
        }

    }
}
