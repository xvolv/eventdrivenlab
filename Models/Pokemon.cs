using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PokemonApi.Models;

public class Pokemon
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public int Level { get; private set; } = 1;
    public int Experience { get; private set; }

    public void GainExperience(int amount)
    {
        if (amount <= 0) return;
        Experience += amount;
        while (Experience >= 100)
        {
            Experience -= 100;
            Level++;
        }
    }
}
