using Cotizacion.Moneda.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cotizacion.Moneda.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<ComprarMoneda> ComprarMoneda { get; set; }
    }
}