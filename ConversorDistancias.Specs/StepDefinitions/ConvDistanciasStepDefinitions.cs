using System.Globalization;
using Microsoft.Extensions.Configuration;
using ConversorDistancias.Specs.PageObjects;

namespace ConversorDistancias.Specs.StepDefinitions;

[Binding]
public sealed class ConvDistanciasStepDefinitions
{
    private static readonly IConfiguration _configuration;
    private double _distanciaMilhas;
    private double _resultadoKm;

    static ConvDistanciasStepDefinitions()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json")
            .AddEnvironmentVariables().Build();

        CultureInfo.DefaultThreadCurrentCulture = new("pt-BR");
        CultureInfo.DefaultThreadCurrentUICulture = new("pt-BR");
    }

    [Given("que o valor da distância é de (.*) milha\\(s\\)")]
    public void PreencherDistanciaMilhas(double distanciaMilhas)
    {
        _distanciaMilhas = distanciaMilhas;
    }

    [When("eu solicitar a conversão desta distância")]
    public void ProcessarConversao()
    {
        var conversor = new ConvDistanciasPageObject(_configuration);

        conversor.Load();
        conversor.SetVlMiles(_distanciaMilhas);
        conversor.ProcessConversion();
        _resultadoKm = conversor.GetVlKm();
        conversor.Close();
    }

    [Then("o resultado será (.*) Km")]
    public void ValidarResultadoKm(double distanciaKm)
    {
        _resultadoKm.Should().Be(distanciaKm, " *** provável problema de arredondamento *** ");
    }
}