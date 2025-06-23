using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Infrastructure.EntityFrameworkProvider.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Banda> Bandas { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Assinatura> Assinaturas { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        modelBuilder.Entity<Banda>().ToTable("Bandas");
        modelBuilder.Entity<Musica>().ToTable("Musicas");
        modelBuilder.Entity<Assinatura>().ToTable("Assinaturas");
        modelBuilder.Entity<Transacao>().ToTable("Transacoes");

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Assinatura)
            .WithOne(a => a.Usuario)
            .HasForeignKey<Assinatura>(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.MusicasFavoritas)
            .WithMany(m => m.FavoritadaPorUsuarios)
            .UsingEntity(j => j.ToTable("UsuariosMusicasFavoritas"));

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.BandasFavoritas)
            .WithMany(b => b.FavoritadaPorUsuarios)
            .UsingEntity(j => j.ToTable("UsuariosBandasFavoritas"));

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Valor)
                .HasColumnType("decimal(10,2)");

            entity.HasOne(t => t.Usuario)
                .WithMany(u => u.Transacoes)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Musica>()
            .HasOne(m => m.Banda)
            .WithMany(b => b.Musicas)
            .HasForeignKey(m => m.BandaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Musica>()
            .HasOne(m => m.Banda)
            .WithMany(b => b.Musicas)
            .HasForeignKey(m => m.BandaId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
