using BankDevTrail.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankDevTrail.Api.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Cpf).IsRequired().HasMaxLength(14);
                entity.Property(e => e.DataNascimento).IsRequired(false);
                
                entity.HasIndex(e => e.Cpf).IsUnique();

                entity.HasMany(e => e.Contas)
                      .WithOne() 
                      .HasForeignKey(c => c.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Conta>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Numero).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Saldo).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Tipo).IsRequired();
            });

            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Valor).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.DataHora).IsRequired(false);
                entity.Property(e => e.Tipo).IsRequired();

                entity.HasOne<Conta>()
                      .WithMany()
                      .HasForeignKey(nameof(Transacao.ContaOrigemId))
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Conta>()
                      .WithMany()
                      .HasForeignKey(nameof(Transacao.ContaDestinoId))
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
