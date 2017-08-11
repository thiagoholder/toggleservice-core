

using ToggleService.Domain;

namespace ToggleService.Builders
{
    public class FeatureBuilder
    {
        public int id = 1;
        public string description = "Default Feature";
        public int version = 1;
        public string type = "Button";

        public Feature Build()
        {
            return new Feature {Id = id, Description = description, Type = type, Version = version};
        }

        public FeatureBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public FeatureBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public FeatureBuilder WithType(string type)
        {
            this.type = type;
            return this;
        }

        public FeatureBuilder WithVersion(int version)
        {
            this.version = version;
            return this;
        }

        public static implicit operator Feature(FeatureBuilder instance)
        {
            return instance.Build();
        }

    }
}
