using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;

namespace Pokedex.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private List<Pokemon> pokemons = [];
    private List<Tipo> tipos = [];
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        
        using(StreamReader leitor = new("Data\\pokemons.json"))
        {
            string dados = leitor.ReadToEnd();
            pokemons = JsonSerializer.Deserialize <List<Pokemon>>(dados);
        }
        
         using(StreamReader leitor = new("Data\\tipos.json"))
        {
            string dados = leitor.ReadToEnd();
            tipos = JsonSerializer.Deserialize <List<Tipo>>(dados);
        }
        ViewData["Tipos"] = tipos;
        return View(pokemons);
    }


    public IActionResult Details(int id)
    {
       using(StreamReader leitor = new("Data\\pokemons.json"))
        {
            string dados = leitor.ReadToEnd();
            pokemons = JsonSerializer.Deserialize <List<Pokemon>>(dados);
        }
        var pokemon = pokemons
        .Where(p => p.Numero == id)
        .FirstOrDefault();
        return View(pokemon);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
