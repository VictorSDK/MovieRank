using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using MovieRank.Contracts;
using MovieRank.Libs.Models;

namespace MovieRank.Libs.Repositories
{
	public class MovieRankLlmRepository : IMovieRankLlmRepository // Low Level Model
	{
        private const string TableName = "MovieRank";
        private readonly IAmazonDynamoDB _dynamoDbClient;

        public MovieRankLlmRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public async Task<ScanResponse> GetAllItems()
        {
            var scanRequest = new ScanRequest(TableName);
            return await _dynamoDbClient.ScanAsync(scanRequest);
        }

        public async Task<GetItemResponse> GetMovie(int userId, string movieName)
        {
            var request = new GetItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"UserId", new AttributeValue {N = userId.ToString()}},
                    {"MovieName", new AttributeValue {S = movieName}}
                }
            };

            return await _dynamoDbClient.GetItemAsync(request);
        }

        public async Task<QueryResponse> GetUsersRankedMoviesByMovieTitle(int userId, string movieName)
        {

            var request = new QueryRequest
            {
                TableName = TableName,
                KeyConditionExpression = "UserId = :userId and begins_with (MovieName, :movieName)",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {":userId", new AttributeValue { N =  userId.ToString() }},
                    {":movieName", new AttributeValue { S = movieName }}
            }
            };

            return await _dynamoDbClient.QueryAsync(request);
        }

        public async Task AddMovie(int userId, MovieRankRequest movieRankRequest)
        {
            var request = new PutItemRequest
            {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"UserId", new AttributeValue {N = userId.ToString()}},
                    {"MovieName", new AttributeValue {S = movieRankRequest.MovieName}},
                    {"Description", new AttributeValue {S = movieRankRequest.Description}},
                    {"Actors", new AttributeValue{SS = movieRankRequest.Actors}},
                    {"Ranking", new AttributeValue {N = movieRankRequest.Ranking.ToString()}},
                    {"RankedDateTime", new AttributeValue {S = DateTime.UtcNow.ToString()}}
                }
            };

            await _dynamoDbClient.PutItemAsync(request);
        }

        public async Task UpdateMovie(int userId, MovieUpdateRequest updateRequest)
        {
            var request = new UpdateItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"UserId", new AttributeValue {N = userId.ToString()}},
                    {"MovieName", new AttributeValue {S = updateRequest.MovieName}}
                },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    { "Ranking", new AttributeValueUpdate
                        {
                            Action = AttributeAction.PUT,
                            Value = new AttributeValue {N = updateRequest.Ranking.ToString()}
                        }
                    },
                    { "RankedDateTime", new AttributeValueUpdate
                        {
                            Action = AttributeAction.PUT,
                            Value = new AttributeValue {S = DateTime.UtcNow.ToString()}
                        }
                    }
                }
            };

            await _dynamoDbClient.UpdateItemAsync(request);
        }

        public async Task<QueryResponse> GetMovieRank(string movieName)
        {
            var request = new QueryRequest
            {
                TableName = TableName,
                IndexName = "MovieName-index",
                KeyConditionExpression = "MovieName = :movieName",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {":movieName", new AttributeValue { S =  movieName }}}
            };

            return await _dynamoDbClient.QueryAsync(request);
        }
    }
}

