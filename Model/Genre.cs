using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRecommendationBackend.Model
{
    public class Genre
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid GenreId { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }

        public IList<MovieGenre> MovieGenres { get; set; }
    }
}