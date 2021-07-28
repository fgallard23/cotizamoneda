using Cotizacion.Moneda.Entity;
using FluentValidation;

namespace Cotizacion.Moneda.Validator
{
    public class ComprarMonedaValidator : AbstractValidator<ComprarMoneda>
    {
        public ComprarMonedaValidator()
        {
            RuleFor(x => x.TipoMoneda)
                .NotNull()
                .NotEmpty()
                .IsEnumName(typeof(TipoMoneda))
                .WithMessage("Tipo de Moneda debe ser Dolar o Real");
        }
    }
}