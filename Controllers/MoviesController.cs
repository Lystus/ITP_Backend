using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieRecommendationBackend.Model;

namespace MovieRecommendationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        //{
        //    return await _context.Movies.ToListAsync();
        //}

        // GET: api/Movies?limit=<int>&offset=<int>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesPage
            (int? limit = null, int? offset = null, bool? asc = null)
        {
            if(limit.HasValue && offset.HasValue)
            {
                int skip = (offset.Value - 1) * limit.Value;
                if(asc.HasValue && asc.Value == true)
                {
                    return await _context.Movies.Skip(skip).Take(limit.Value).OrderBy(item => item.Title).ToListAsync();
                }
            }
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(Guid id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MovieExists(movie.MovieId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        private bool MovieExists(Guid id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
        //GoogleSearchAPI
        public async string GetMovieImage(string MName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://google-search3.p.rapidapi.com/api/v1/images/q="+MName+"&num=1"),
                Headers =
    {
        { "x-rapidapi-key", "cd3c41e77amshd84e637ee978158p1ae55ejsn4ebb80283a0a" },
        { "x-rapidapi-host", "google-search3.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(body);
                string hv = json.SelectToken("image_results").ToString().Replace("[", "");
                hv = hv.Replace("]", "");
                string[] strarr = hv.Split("}\r\n  },\r\n");
                JObject json2 = JObject.Parse(strarr[1]);
                JObject json3 = JObject.Parse(json2.SelectToken("image").ToString());
                string src = json3.SelectToken("src").ToString();
                return src;
            }
        }
    }
}
