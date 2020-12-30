using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRecommendationBackend.Model
{
    public class Movie
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MovieId { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; }

        public IList<MovieGenre> MovieGenres { get; set; }
    }
}
