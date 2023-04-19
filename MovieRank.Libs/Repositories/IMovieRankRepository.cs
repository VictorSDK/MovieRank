using System;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Repositories
{
	public interface IMovieRankRepository
	{
        Task<IEnumerable<MovieDb>> GetAllItems();
        Task<MovieDb> GetMovie(int userId, string movieName);
        Task<IEnumerable<MovieDb>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);
    }
}

