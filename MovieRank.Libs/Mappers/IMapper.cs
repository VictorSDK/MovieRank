using Amazon.DynamoDBv2.DocumentModel;
using MovieRank.Contracts;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Mappers
{
    public interface IMapper
    {
        IEnumerable<MovieResponse> ToMovieContract(IEnumerable<MovieDb> response);       
        MovieResponse ToMovieContract(MovieDb movie);        
        MovieDb ToMovieDbModel(int userId, MovieRankRequest movieRankRequest);
        MovieDb ToMovieDbModel(int userId, MovieDb movieDbRequest, MovieUpdateRequest movieUpdateRequest);

        IEnumerable<MovieResponse> ToMovieContract(IEnumerable<Document> items);
        MovieResponse ToMovieContract(Document item);
        Document ToDocumentModel(int userId, MovieRankRequest addRequest);
        Document ToDocumentModel(int userId, MovieResponse movieResponse, MovieUpdateRequest movieUpdateRequest);
    }
}