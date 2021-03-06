// <auto-generated />
using System;
using Cotizacion.Moneda.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cotizacion.Moneda.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("Cotizacion.Moneda.Entity.ComprarMoneda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaCompra")
                        .HasColumnType("TEXT");

                    b.Property<string>("IdUsuario")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MontoComprar")
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoMoneda")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ComprarMoneda");
                });
#pragma warning restore 612, 618
        }
    }
}
