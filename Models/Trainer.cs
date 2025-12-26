namespace PokemonApi.Models;

public class Trainer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string City { get; set; } = string.Empty;
    public List<string> PokemonNames { get; set; } = new();
}
