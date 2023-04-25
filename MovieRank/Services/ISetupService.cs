using System;
using MovieRank.Contracts;

namespace MovieRank.Services
{
	public interface ISetupService
	{
        Task CreateDynamoDbTable(string dynamoDbTableName);
        Task DeleteDynamoDbTable(string dynamoDbTableName);
    }
}

