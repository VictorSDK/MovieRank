using System;
using Amazon.DynamoDBv2.DocumentModel;
using MovieRank.Contracts;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Mappers
{
	public class Mapper : IMapper
	{
		public IEnumerable<MovieResponse> ToMovieContract(IEnumerable<MovieDb> items)
		{
			return items.Select(ToMovieContract);
		}

		public MovieResponse ToMovieContract(MovieDb movie)
		{
			return new MovieResponse
			{
				MovieName = movie.MovieName,
				Description = movie.Description,
				Actors = movie.Actors,
				Ranking = movie.Ranking,
				TimeRanked = movie.RankedDateTime
			};
		}

        public MovieDb ToMovieDbModel(int userId, MovieRankRequest movieRankRequest)
        {
            return new MovieDb
            {
                UserId = userId,
                MovieName = movieRankRequest.MovieName,
                Description = movieRankRequest.Description,
                Actors = movieRankRequest.Actors,
                Ranking = movieRankRequest.Ranking,
                RankedDateTime = DateTime.UtcNow.ToString()
            };
        }

        public MovieDb ToMovieDbModel(int userId, MovieDb movieDbRequest, MovieUpdateRequest movieUpdateRequest)
        {
            return new MovieDb
            {
                UserId = movieDbRequest.UserId,
                MovieName = movieDbRequest.MovieName,
                Description = movieDbRequest.Description,
                Actors = movieDbRequest.Actors,
                Ranking = movieUpdateRequest.Ranking,
                RankedDateTime = DateTime.UtcNow.ToString()
            };
        }

        public IEnumerable<MovieResponse> ToMovieContract(IEnumerable<Document> items)
        {
            return items.Select(ToMovieContract);
        }

        public MovieResponse ToMovieContract(Document item)
        {
            return new MovieResponse
            {
                MovieName = item["MovieName"],
                Description = item["Description"],
                Actors = item["Actors"].AsListOfString(),
                Ranking = Convert.ToInt32(item["Ranking"]),
                TimeRanked = item["RankedDateTime"],
            };
        }

        public Document ToDocumentModel(int userId, MovieRankRequest addRequest)
        {
            return new Document
            {
                ["UserId"] = userId,
                ["MovieName"] = addRequest.MovieName,
                ["Description"] = addRequest.Description,
                ["Actors"] = addRequest.Actors,
                ["RankedDateTime"] = DateTime.UtcNow.ToString(),
                ["Ranking"] = addRequest.Ranking
            };
        }

        public Document ToDocumentModel(int userId, MovieResponse movieResponse, MovieUpdateRequest movieUpdateRequest)
        {
            return new Document
            {
                ["UserId"] = userId,
                ["MovieName"] = movieResponse.MovieName,
                ["Description"] = movieResponse.Description,
                ["Actors"] = movieResponse.Actors,
                ["Ranking"] = movieUpdateRequest.Ranking,
                ["RankedDateTime"] = DateTime.UtcNow.ToString(),
            };
        }
    }
}

