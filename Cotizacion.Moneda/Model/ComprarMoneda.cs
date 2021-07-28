using System;

namespace Cotizacion.Moneda.Entity
{
    public class ComprarMoneda 
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public decimal MontoComprar { get; set; } 
        public string TipoMoneda { get; set; }
        public DateTime FechaCompra { get; set; }
    }
}
