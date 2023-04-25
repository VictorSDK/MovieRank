using System;
using MovieRank.Contracts;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;

namespace MovieRank.Services
{
	public class MovieRankDmService : IMovieRankService
	{
		private readonly IMovieRankDmRepository _movieRankRepository;
        private readonly IMapper _mapper;

        public MovieRankDmService(
            IMovieRankDmRepository movieRankRepository,
            IMapper mapper)
        {
            _movieRankRepository = movieRankRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieResponse>> GetAllItemsFromDatabase()
        {
            var response = await _movieRankRepository.GetAllItems();
            return _mapper.ToMovieContract(response);
        }

        public async Task<MovieResponse> GetMovie(int userId, string movieName)
        {
            var response = await _movieRankRepository.GetMovie(userId, movieName);

            return _mapper.ToMovieContract(response);
        }

        public async Task<IEnumerable<MovieResponse>> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {
            var response = await _movieRankRepository.GetUsersRankedMoviesByMovieTitle(userId, movieName);

            return _mapper.ToMovieContract(response);
        }

        public async Task AddMovie(int userId, MovieRankRequest movieRankRequest)
        {
            var document = _mapper.ToDocumentModel(userId, movieRankRequest);

            await _movieRankRepository.AddMovie(document);
        }

        public async Task UpdateMovie(int userId, MovieUpdateRequest request)
        {
            var response = await GetMovie(userId, request.MovieName);

            var movieDb = _mapper.ToDocumentModel(userId, response, request);

            await _movieRankRepository.UpdateMovie(movieDb);
        }

        public async Task<MovieRankResponse> GetMovieRank(string movieName)
        {
            var response = await _movieRankRepository.GetMovieRank(movieName);

            var overallMovieRanking = Math.Round(response.Select(x => x["Ranking"].AsInt()).Average());

            return new MovieRankResponse
            {
                MovieName = movieName,
                OverallRanking = overallMovieRanking
            };
        }
    }
}

