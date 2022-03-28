using FluentValidation;
using ConversorDistancias.Models;

namespace ConversorDistancias.Validators;

public class DistanciaMilhasValidator : AbstractValidator<DistanciasViewModel>
{
    public DistanciaMilhasValidator()
    {
        RuleFor(c => c.DistanciaMilhas)
            .NotNull()
            .GreaterThan(0)
            .LessThanOrEqualTo(9_999_999.99);
    }
}