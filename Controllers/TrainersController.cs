using Microsoft.AspNetCore.Mvc;
using PokemonApi.Models;
using PokemonApi.Services;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private static readonly List<Trainer> Trainers = new();

    private readonly PokemonService _pokemonService;

    public TrainersController(PokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }

    [HttpPost]
    public IActionResult Create(Trainer trainer)
    {
        Trainers.Add(trainer);
        return Ok(trainer);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Trainers);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var trainer = Trainers.FirstOrDefault(t => t.Id == id);
        return trainer is null ? NotFound() : Ok(trainer);
    }

    [HttpPost("{id:int}/pokemon/{pokemonName}")]
    public IActionResult AddPokemon(int id, string pokemonName)
    {
        var trainer = Trainers.FirstOrDefault(t => t.Id == id);
        if (trainer is null) return NotFound();

        var pokemon = _pokemonService.GetByName(pokemonName);
        if (pokemon is null) return NotFound($"Pokemon '{pokemonName}' not found.");

        if (!trainer.PokemonNames.Contains(pokemonName, StringComparer.OrdinalIgnoreCase))
        {
            trainer.PokemonNames.Add(pokemonName);
        }

        return Ok(trainer);
    }
}
