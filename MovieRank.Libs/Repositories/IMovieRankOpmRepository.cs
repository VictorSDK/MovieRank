﻿using System;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Repositories
{
	public interface IMovieRankOpmRepository // Object Persistance Model
    {
        Task<IEnumerable<MovieDb>> GetAllItems();
        Task<MovieDb> GetMovie(int userId, string movieName);
        Task<IEnumerable<MovieDb>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);
        Task AddMovie(MovieDb movieDb);
        Task UpdateMovie(MovieDb request);
        Task<IEnumerable<MovieDb>> GetMovieRank(string movieName);
    }
}

