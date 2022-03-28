using System.Globalization;
using FluentValidation;
using FluentValidation.AspNetCore;
using ConversorDistancias.Validators;
using ConversorDistancias.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddFluentValidation();

builder.Services.AddTransient<IValidator<DistanciasViewModel>,
    DistanciaMilhasValidator>();

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new (culture: "pt-BR", uiCulture: "pt-BR"),
    SupportedCultures = new[] { new CultureInfo("pt-BR") },
    SupportedUICultures = new[] { new CultureInfo("pt-BR") }
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();