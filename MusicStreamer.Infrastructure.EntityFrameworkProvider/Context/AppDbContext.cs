using Microsoft.EntityFrameworkCore;
using MusicStreamer.Domain.Entities;

namespace MusicStreamer.Infrastructure.EntityFrameworkProvider.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Banda> Bandas { get; set; }
    public DbSet<Album> Albuns { get; set; }
    public DbSet<Musica> Musicas { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Assinatura> Assinaturas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Nomes de tabelas explícitos (opcional)
        modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        modelBuilder.Entity<Banda>().ToTable("Bandas");
        modelBuilder.Entity<Album>().ToTable("Albuns");
        modelBuilder.Entity<Musica>().ToTable("Musicas");
        modelBuilder.Entity<Playlist>().ToTable("Playlists");
        modelBuilder.Entity<Assinatura>().ToTable("Assinaturas");

        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Assinatura)
            .WithOne(a => a.Usuario)
            .HasForeignKey<Assinatura>(a => a.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.MusicasFavoritas)
            .WithMany()
            .UsingEntity(j => j.ToTable("MusicasFavoritas"));

        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.BandasFavoritas)
            .WithMany()
            .UsingEntity(j => j.ToTable("BandasFavoritas"));

        modelBuilder.Entity<Playlist>()
            .HasMany(p => p.Musicas)
            .WithMany()
            .UsingEntity(j => j.ToTable("MusicasPlaylist"));

        modelBuilder.Entity<Album>()
            .HasOne(a => a.Banda)
            .WithMany(b => b.Albuns)
            .HasForeignKey(a => a.BandaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Musica>()
            .HasOne(m => m.Album)
            .WithMany(a => a.Musicas)
            .HasForeignKey(m => m.AlbumId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Playlist>()
            .HasOne(p => p.Usuario)
            .WithMany(u => u.Playlists)
            .HasForeignKey(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
