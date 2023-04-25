using System;
namespace MovieRank.Libs.Repositories
{
	public interface ISetupRepository
	{
        Task CreateDynamoTable(string tableName);
        Task DeleteDynamoDbTable(string tableName);
    }
}

