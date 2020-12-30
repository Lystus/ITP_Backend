using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRecommendationBackend.Model
{
    public class MovieGenre
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
