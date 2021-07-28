#region  - Librerias -
using System;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using Cotizacion.Moneda.Data;
using Cotizacion.Moneda.Entity;
using Cotizacion.Moneda.Util;
using Cotizacion.Moneda.Validator;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
#endregion

namespace Cotizacion.Moneda.Controllers
{
    [Route("api/[controller]")]
    public class MonedaController : Controller
    {
        private readonly ILogger<MonedaController> _logger;
        private readonly DataContext _context;
        
        // allow 
        public static int ALLOW_DOLAR = 200;
        public static int ALLOW_REAL = 300;
        
        // constructor
        public MonedaController(DataContext context, ILogger<MonedaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("/cotizar/{tipoMoneda}")]
        public async Task<ActionResult<string>> CotizarMoneda(string tipoMoneda)
        {
            try
            {
                // custom 
                var currency = new ComprarMoneda();
                currency.TipoMoneda = tipoMoneda.ToUpper();
                var validator = new ComprarMonedaValidator();
                var results = validator.Validate(currency);
                
                results.AddToModelState(ModelState, null);
                if (!results.IsValid) //validation ok
                    return BadRequest(ModelState);
                
                // cotizacion 
                var cotizacion = await MonedaServicio.CotizarMoneda();
                
                // tipo moneda
                switch (tipoMoneda.ToUpper())
                {
                    case "DOLAR":
                        return Ok(cotizacion);
                    case "REAL":
                        for (var i = 0; i < cotizacion.Count; i++)
                        {
                            if (!decimal.TryParse(cotizacion[i], out _)) continue;
                            var valor =  Convert.ToDecimal(cotizacion[i], new CultureInfo("us-US"));
                            cotizacion[i] = Convert.ToString(valor/4, CultureInfo.InvariantCulture);
                        }

                        return Ok(cotizacion);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(CotizarMoneda), ex);
                return (ex.Message);
            }

            return ("ok");
        }
        
        [HttpPost("[action]")]
        public ActionResult<string> ComprarMoneda([FromBody] ComprarMoneda moneda)
        {
            try
            {
                if (!ModelState.IsValid) // re-render the view when validation failed.
                    return BadRequest(ModelState);
                
                // compras x usuario
                var compraList = _context.ComprarMoneda
                        .Where(s => s.IdUsuario == moneda.IdUsuario)
                        .ToList();
                
                // date month
                var year = moneda.FechaCompra.Year;
                var dtInicial = new DateTime(year, moneda.FechaCompra.Month, 1);
                var dtFinal = new DateTime(year, moneda.FechaCompra.AddMonths(1).Month, 1);
                
                // monto x moneda
                var monto = compraList
                    .Where(w => w.FechaCompra >= dtInicial && w.FechaCompra < dtFinal &&
                                w.TipoMoneda == moneda.TipoMoneda).Sum(x => x.MontoComprar);

                // cotizacion 
                var cotizacion = MonedaServicio.CotizarMoneda();
                var valor =  Convert.ToDecimal(cotizacion.Result.FirstOrDefault(), new CultureInfo("us-US"));
                
                switch (moneda.TipoMoneda.ToUpper())
                {
                    case "DOLAR":
                        moneda.MontoComprar /= valor;
                        if (moneda.MontoComprar + monto > ALLOW_DOLAR)
                        {
                            ModelState.AddModelError("DOLAR", "Monto No Permitido");
                            return BadRequest(ModelState);
                        }

                        moneda.TipoMoneda = "DOLAR";
                        break;
                    case "REAL":
                        moneda.MontoComprar /= (valor / 4);
                        if (moneda.MontoComprar + monto > ALLOW_REAL)
                        {
                            ModelState.AddModelError("REAL", "Monto No Permitido");
                            return BadRequest(ModelState);
                        }
                        
                        moneda.TipoMoneda = "REAL";
                        break;
                }
                moneda.FechaCompra = DateTime.Now;
                _context.ComprarMoneda.Add(moneda);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(ComprarMoneda), ex);
                return (ex.Message);
            }

            return ("ok");
        }
    }
}