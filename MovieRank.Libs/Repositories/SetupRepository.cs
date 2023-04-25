using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace MovieRank.Libs.Repositories
{
	public class SetupRepository : ISetupRepository
	{
        private readonly IAmazonDynamoDB _dynamoDbClient;

        public SetupRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task CreateDynamoTable(string tableName)
        {
            //Might want to do a check to wait for the table to be created before passing back a response

            var request = new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement
                    {
                        AttributeName = "Id",
                        KeyType = "HASH"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                }
            };

            await _dynamoDbClient.CreateTableAsync(request);
        }

        public async Task DeleteDynamoDbTable(string tableName)
        {
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };

            await _dynamoDbClient.DeleteTableAsync(request);
        }
    }
}

