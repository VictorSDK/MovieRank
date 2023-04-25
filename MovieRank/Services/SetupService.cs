using System;
using MovieRank.Contracts;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;

namespace MovieRank.Services
{
	public class SetupService : ISetupService
	{
		private readonly ISetupRepository _setupRepository;
        private readonly IMapper _mapper;

        public SetupService(
            IMapper mapper,
            ISetupRepository setupRepository)
        {
            _mapper = mapper;
            _setupRepository = setupRepository;
        }

        public async Task CreateDynamoDbTable(string dynamoDbTableName)
        {
            await _setupRepository.CreateDynamoTable(dynamoDbTableName);
        }

        public async Task DeleteDynamoDbTable(string dynamoDbTableName)
        {
            await _setupRepository.DeleteDynamoDbTable(dynamoDbTableName);
        }
    }
}

