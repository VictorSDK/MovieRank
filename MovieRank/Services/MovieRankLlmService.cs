using System;
using MovieRank.Contracts;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;

namespace MovieRank.Services
{
	public class MovieRankLlmService : IMovieRankService
    {
		private readonly IMovieRankLlmRepository _movieRankRepository;
        private readonly IMapper _mapper;

        public MovieRankLlmService(
            IMovieRankLlmRepository movieRankRepository,
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
            await _movieRankRepository.AddMovie(userId, movieRankRequest);
        }

        public async Task UpdateMovie(int userId, MovieUpdateRequest request)
        {
            await _movieRankRepository.UpdateMovie(userId, request);
        }

        public async Task<MovieRankResponse> GetMovieRank(string movieName)
        {
            var response = await _movieRankRepository.GetMovieRank(movieName);

            var overallMovieRanking = Math.Round(response.Items.Select(item => Convert.ToInt32(item["Ranking"].N)).Average());

            return new MovieRankResponse
            {
                MovieName = movieName,
                OverallRanking = overallMovieRanking
            };
        }
    }
}

