using DigitalTwin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalTwin.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        // Constructor: Recibe las opciones de configuración (como la cadena de conexión)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // TABLAS: Aquí le dices a EF Core "Quiero una tabla para mis Motores"
        public DbSet<Motor> Motors { get; set; }

        // CONFIGURACIÓN AVANZADA (Opcional por ahora)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aquí podrías configurar reglas SQL específicas, 
            // pero por ahora EF Core es inteligente y lo hace solo.
            base.OnModelCreating(modelBuilder);
        }
    }
}
