using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Service
{
    public class AppDbContext : DbContext
    {
        public DbSet<Entrenador> Entrenadores { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<Clase> Clases { get; set; }
        public DbSet<HorarioClase> HorariosClases { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        // Nuevas tablas
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones
            modelBuilder.Entity<HorarioClase>()
                .HasOne(hc => hc.Clase)
                .WithMany(c => c.HorariosClases)
                .HasForeignKey(hc => hc.ClaseId);

            modelBuilder.Entity<HorarioClase>()
                .HasOne(hc => hc.Entrenador)
                .WithMany(e => e.HorariosClases)
                .HasForeignKey(hc => hc.EntrenadorId);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Membresia)
                .WithMany(m => m.Pagos)
                .HasForeignKey(p => p.MembresiaId);

            // Relaciones para nuevas tablas
            modelBuilder.Entity<User>()
                .HasOne(u => u.Status)
                .WithMany(s => s.Users)
                .HasForeignKey(u => u.StatusId);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.UserId);

            // Configuración de clave primaria para PasswordReset
            modelBuilder.Entity<PasswordReset>()
                .HasKey(pr => pr.Email);
        }
    }
}
