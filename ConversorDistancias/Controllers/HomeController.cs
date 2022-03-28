using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConversorDistancias.Models;

namespace ConversorDistancias.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(DistanciasViewModel distancias)
    {
        if (ModelState.IsValid)
        {
            // FIXME: Simulação de falha
            distancias.DistanciaKm = distancias.DistanciaMilhas * 1.609;
            //distancias.DistanciaKm = Math.Round(distancias.DistanciaMilhas!.Value * 1.609, 3);
            _logger.LogInformation(
                $"{distancias.DistanciaMilhas} milhas = {distancias.DistanciaKm} Km");
            return View(distancias);
        }
        else
        {
            var mensagemErro =
                "Informe uma distância em milhas entre 0,01 e 9.999.999,99!";

            _logger.LogError(mensagemErro);
            ModelState.Clear();
            ModelState.AddModelError(
                nameof(distancias.DistanciaMilhas),
                mensagemErro);

            return View("Index", distancias);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}