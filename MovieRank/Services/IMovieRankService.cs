using System;
using MovieRank.Contracts;

namespace MovieRank.Services
{
	public interface IMovieRankService
	{
        Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase();
        Task<MovieResponse> GetMovie(int userId, string movieName);
        Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);
    }
}

