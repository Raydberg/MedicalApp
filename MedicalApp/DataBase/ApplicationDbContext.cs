using Microsoft.EntityFrameworkCore;
using MedicalApp.Models;

namespace MedicalApp.DataBase;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Cita> Citas { get; set; }
    public DbSet<Especialidad> Especialidades { get; set; }
    public DbSet<Distrito> Distritos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Medico>().HasKey(m => m.Codmed);
        modelBuilder.Entity<Especialidad>().HasKey(e => e.Codesp);
        modelBuilder.Entity<Distrito>().HasKey(d => d.Coddis);
        
        modelBuilder.Entity<Medico>()
            .HasOne(m => m.Especialidad)
            .WithMany(e => e.Medicos)
            .HasForeignKey(m => m.Codesp);
            
        modelBuilder.Entity<Medico>()
            .HasOne(m => m.Distrito)
            .WithMany(d => d.Medicos)
            .HasForeignKey(m => m.Coddis);
            
        modelBuilder.Entity<Paciente>()
            .HasOne(p => p.Distrito)
            .WithMany(d => d.Pacientes)
            .HasForeignKey(p => p.Coddis);
            
        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Paciente)
            .WithMany(p => p.Citas)
            .HasForeignKey(c => c.Codpac);
            
        modelBuilder.Entity<Cita>()
            .HasOne(c => c.Medico)
            .WithMany(m => m.Citas)
            .HasForeignKey(c => c.Codmed);
    }
}