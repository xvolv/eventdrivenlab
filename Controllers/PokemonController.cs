using Microsoft.AspNetCore.Mvc;
using PokemonApi.Models;
using PokemonApi.Services;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokemonService _pokemonService;

    public PokemonController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_pokemonService.GetAll());
    }

    [HttpGet("{name}")]
    public IActionResult GetByName(string name)
    {
        var pokemon = _pokemonService.GetByName(name);
        if (pokemon == null)
            return NotFound();

        return Ok(pokemon);
    }

    [HttpPost]
    public IActionResult Create(Pokemon pokemon)
    {
        _pokemonService.Add(pokemon);
        return CreatedAtAction(nameof(GetByName), new { name = pokemon.Name }, pokemon);
    }

    [HttpPost("{name}/train/{amount}")]
    public IActionResult Train(string name, int amount)
    {
        var pokemon = _pokemonService.Train(name, amount);
        if (pokemon == null)
            return NotFound();

        return Ok(pokemon);
    }

    [HttpDelete("{name}")]
    public IActionResult Delete(string name)
    {
        var deleted = _pokemonService.Delete(name);
        return deleted ? NoContent() : NotFound();
    }
}
