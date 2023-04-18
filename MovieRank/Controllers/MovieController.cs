using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRank.Contracts;
using MovieRank.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieRank.Controllers
{
    [Route("movies")]
    public class MovieController : Controller
    {
        private readonly IMovieRankService _movieRankService;

        public MovieController(IMovieRankService movieRankService)
        {
            _movieRankService = movieRankService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase()
        {
            var results = await _movieRankService.GetAllItemsFromDatabase();

            return results;
        }
    }
}

