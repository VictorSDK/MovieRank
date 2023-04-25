// #define OPM
#define DM
// #define LL
using Amazon.DynamoDBv2;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;
using MovieRank.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddDefaultAWSOptions(new Amazon.Extensions.NETCore.Setup.AWSOptions
{
    Region = Amazon.RegionEndpoint.GetBySystemName("us-west-2")
});

#if OPM
builder.Services.AddSingleton<IMovieRankService, MovieRankOpmService>();
builder.Services.AddSingleton<IMovieRankOpmRepository, MovieRankOpmRepository>();
#elif DM
builder.Services.AddSingleton<IMovieRankService, MovieRankDmService>();
builder.Services.AddSingleton<IMovieRankDmRepository, MovieRankDmRepository>();
#endif
builder.Services.AddSingleton<IMapper, Mapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

