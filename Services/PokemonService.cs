using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PokemonApi.Models;

namespace PokemonApi.Services;

public class PokemonService
{
    private readonly IMongoCollection<Pokemon> _pokemonCollection;

    public PokemonService(IOptions<DBSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        _pokemonCollection = database.GetCollection<Pokemon>("Pokemon");
    }

    public List<Pokemon> GetAll() => _pokemonCollection.Find(_ => true).ToList();

    public Pokemon? GetByName(string name) =>
        _pokemonCollection.Find(p => p.Name == name).FirstOrDefault();

    public Pokemon Add(Pokemon pokemon)
    {
        _pokemonCollection.InsertOne(pokemon);
        return pokemon;
    }

    public bool Delete(string name)
    {
        var deleteResult = _pokemonCollection.DeleteOne(p => p.Name == name);
        return deleteResult.DeletedCount > 0;
    }

    public Pokemon? Train(string name, int amount)
    {
        var pokemon = GetByName(name);
        if (pokemon is null) return null;

        pokemon.GainExperience(amount);
        _pokemonCollection.ReplaceOne(p => p.Id == pokemon.Id, pokemon);
        return pokemon;
    }
}
