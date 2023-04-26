// #define OPM
// #define DM
#define LL
using Amazon.DynamoDBv2;
using MovieRank;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;
using MovieRank.Services;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup();

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();

