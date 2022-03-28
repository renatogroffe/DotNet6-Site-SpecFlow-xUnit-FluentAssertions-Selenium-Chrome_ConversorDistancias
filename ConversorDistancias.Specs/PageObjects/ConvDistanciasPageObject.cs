using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ConversorDistancias.Specs.Support;

namespace ConversorDistancias.Specs.PageObjects;

public class ConvDistanciasPageObject
{
    private readonly IConfiguration _configuration;
    private IWebDriver? _driver;

    public ConvDistanciasPageObject(
        IConfiguration configuration)
    {
        _configuration = configuration;

        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--headless");

        chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.Off);
        chromeOptions.SetLoggingPreference(LogType.Driver, LogLevel.Off);

        if (!String.IsNullOrWhiteSpace(_configuration["PathDriverChrome"]))
            _driver = new ChromeDriver(_configuration["PathDriverChrome"], chromeOptions);
        else
            _driver = new ChromeDriver(chromeOptions);
    }

    public void Load()
    {
        _driver!.LoadPage(
            TimeSpan.FromSeconds(Convert.ToInt32(_configuration["Timeout"])),
            _configuration["UrlConversaoDistancias"]);
    }

    public void SetVlMiles(double valor)
    {
        _driver!.SetText(
            By.Name("DistanciaMilhas"),
            valor.ToString());
    }

    public void ProcessConversion()
    {
        _driver!.Submit(By.Id("btnConverter"));

        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until((d) => d.FindElement(By.Id("DistanciaKm")) != null);
    }

    public double GetVlKm()
    {
        return Convert.ToDouble(
            _driver!.GetText(By.Id("DistanciaKm")));
    }

    public void Close()
    {
        _driver!.Quit();
        _driver = null;
    }
}