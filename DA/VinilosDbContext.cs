
using System;
using DA.Entidades;
using Microsoft.EntityFrameworkCore;

namespace DA
{
    public class VinilosDbContext : DbContext
    {
        public VinilosDbContext(
            DbContextOptions<VinilosDbContext> options)
            : base(options)
        {
        }

        public DbSet<ArtistaEntidad> Artistas { get; set; }

        public DbSet<AlbumEntidad> Albumes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==========================================
            // CONFIGURACIÓN DE ARTISTA
            // ==========================================
            modelBuilder.Entity<ArtistaEntidad>(entidad =>
            {
                entidad.HasKey(a => a.Id);

                entidad.Property(a => a.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entidad.Property(a => a.Pais)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            // ==========================================
            // CONFIGURACIÓN DE ÁLBUM
            // ==========================================
            modelBuilder.Entity<AlbumEntidad>(entidad =>
            {
                entidad.HasKey(a => a.Id);

                entidad.Property(a => a.Titulo)
                    .IsRequired()
                    .HasMaxLength(150);

                entidad.Property(a => a.Genero)
                    .IsRequired()
                    .HasMaxLength(50);

                entidad.Property(a => a.AnioLanzamiento)
                    .IsRequired();

                entidad.Property(a => a.CantidadCanciones)
                    .IsRequired();

                // Relación uno a muchos:
                // Un artista puede tener varios álbumes.
                entidad.HasOne(a => a.Artista)
                    .WithMany(a => a.Albumes)
                    .HasForeignKey(a => a.ArtistaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ==========================================
            // DATOS INICIALES DE ARTISTAS
            // ==========================================
            modelBuilder.Entity<ArtistaEntidad>().HasData(
                new ArtistaEntidad
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Nombre = "Michael Jackson",
                    Pais = "Estados Unidos"
                },
                new ArtistaEntidad
                {
                    Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Nombre = "The Beatles",
                    Pais = "Reino Unido"
                },
                new ArtistaEntidad
                {
                    Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    Nombre = "Bad Bunny",
                    Pais = "Puerto Rico"
                }
            );

            // ==========================================
            // DATOS INICIALES DE ÁLBUMES
            // ==========================================
            modelBuilder.Entity<AlbumEntidad>().HasData(
                new AlbumEntidad
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Titulo = "Thriller",
                    Genero = "Pop",
                    AnioLanzamiento = 1982,
                    CantidadCanciones = 9,
                    ArtistaId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
                },
                new AlbumEntidad
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Titulo = "Abbey Road",
                    Genero = "Rock",
                    AnioLanzamiento = 1969,
                    CantidadCanciones = 17,
                    ArtistaId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")
                },
                new AlbumEntidad
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Titulo = "Un Verano Sin Ti",
                    Genero = "Reguetón",
                    AnioLanzamiento = 2022,
                    CantidadCanciones = 23,
                    ArtistaId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc")
                }
            );
        }
    }
}
