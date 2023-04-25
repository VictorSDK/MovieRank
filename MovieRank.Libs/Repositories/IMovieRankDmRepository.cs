using System;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Repositories
{
	public interface IMovieRankDmRepository // Document Model
    {
        Task<IEnumerable<Document>> GetAllItems();
        Task<Document> GetMovie(int userId, string movieName);
        Task<IEnumerable<Document>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName);
        Task AddMovie(Document documentModel);
        Task UpdateMovie(Document documentModel);
        Task<IEnumerable<Document>> GetMovieRank(string movieName);
    }
}

