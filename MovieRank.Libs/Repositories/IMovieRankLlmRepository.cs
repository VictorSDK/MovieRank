using System;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using MovieRank.Contracts;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Repositories
{
	public interface IMovieRankLlmRepository // Low Level Model
    {
        Task<ScanResponse> GetAllItems();
        Task<GetItemResponse> GetMovie(int userId, string movieName);
        Task<QueryResponse> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);
        Task AddMovie(int userId, MovieRankRequest movieRankRequest);
        Task UpdateMovie(int userId, MovieUpdateRequest updateRequest);
        Task<QueryResponse> GetMovieRank(string movieName);
    }
}

