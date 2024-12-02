using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Service
{
    public class AppDbContext : DbContext
    {
        // Declaración de DbSets (tablas)
        public DbSet<Entrenador> Entrenadores { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<HorarioClase> HorariosClases { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "gym.db3");
                optionsBuilder.UseSqlite($"Filename={dbPath}");
            }
        }

        public async Task<List<string>> GetTablesAsync()
        {
            var tables = new List<string>();
            var connection = this.Database.GetDbConnection();

            try
            {
                await connection.OpenAsync();
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    tables.Add(reader.GetString(0));
                }
            }
            finally
            {
                await connection.CloseAsync();
            }

            return tables;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relación User -> Status
            modelBuilder.Entity<User>()
                .HasOne(u => u.Status)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HorarioClase>()
                .HasOne(hc => hc.Clase)
                .WithMany(c => c.HorariosClases)
                .HasForeignKey(hc => hc.ClaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HorarioClase>()
                .HasOne(hc => hc.Entrenador)
                .WithMany(e => e.HorariosClases)
                .HasForeignKey(hc => hc.EntrenadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Membresia)
                .WithMany(m => m.Pagos)
                .HasForeignKey(p => p.MembresiaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
