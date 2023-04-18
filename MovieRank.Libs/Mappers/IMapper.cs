using MovieRank.Contracts;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Mappers
{
    public interface IMapper
    {
        IEnumerable<MovieResponse> ToMovieContract(IEnumerable<MovieDb> response);
        MovieResponse ToMovieContract(MovieDb movie);
    }
}