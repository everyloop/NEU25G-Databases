using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace L008_DataGrid_and_TreeView.Models;

public partial class MusicContext : DbContext
{
    public MusicContext()
    {
    }

    public MusicContext(DbContextOptions<MusicContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<PlaylistTrack> PlaylistTracks { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Database=everyloop;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Finnish_Swedish_CI_AS");

        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable("albums", "music");

            entity.Property(e => e.AlbumId).ValueGeneratedNever();
            entity.Property(e => e.Title).HasMaxLength(160);

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_albums_artists");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.ToTable("artists", "music");

            entity.HasIndex(e => e.ArtistId, "NonClusteredIndex-20251204-235009");

            entity.Property(e => e.ArtistId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("genres", "music");

            entity.Property(e => e.GenreId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.ToTable("media_types", "music");

            entity.Property(e => e.MediaTypeId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.ToTable("playlists", "music");

            entity.HasIndex(e => e.Name, "NonClusteredIndex-20251204-235549");

            entity.Property(e => e.PlaylistId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<PlaylistTrack>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("playlist_track", "music");

            entity.HasIndex(e => e.TrackId, "NonClusteredIndex-20251204-234928");

            entity.HasIndex(e => e.PlaylistId, "NonClusteredIndex-PlaylistID");

            entity.HasOne(d => d.Playlist).WithMany()
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_playlist_track_playlists");

            entity.HasOne(d => d.Track).WithMany()
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_playlist_track_tracks");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.ToTable("tracks", "music");

            entity.HasIndex(e => e.AlbumId, "NonClusteredIndex-20251204-234656");

            entity.HasIndex(e => e.GenreId, "NonClusteredIndex-20251204-234729");

            entity.Property(e => e.TrackId).ValueGeneratedNever();
            entity.Property(e => e.Composer).HasMaxLength(220);
            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Album).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK_tracks_albums");

            entity.HasOne(d => d.Genre).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_tracks_genres");

            entity.HasOne(d => d.MediaType).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.MediaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tracks_media_types");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
