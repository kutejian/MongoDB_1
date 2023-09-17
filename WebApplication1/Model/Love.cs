using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Model
{
    public class Love
    {
        [BsonElement("emil")]
        public string Emil {  get; set; }
        [BsonElement("sex")]
        public string Sex { get; set; }

    }
}
