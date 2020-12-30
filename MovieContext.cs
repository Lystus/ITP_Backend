using Microsoft.EntityFrameworkCore;
using MovieRecommendationBackend.Model;

namespace MovieRecommendationBackend
{
    public class MovieContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=MovieRecommender;User Id=sa;Password=veryLongPassword!");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieGenre>().HasKey(tg => new { tg.MovieId, tg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne<Movie>(tg => tg.Movie)
                .WithMany(s => s.MovieGenres)
                .HasForeignKey(tg => tg.MovieId);


            modelBuilder.Entity<MovieGenre>()
                .HasOne<Genre>(tg => tg.Genre)
                .WithMany(s => s.MovieGenres)
                .HasForeignKey(tg => tg.GenreId);
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
    }
}
