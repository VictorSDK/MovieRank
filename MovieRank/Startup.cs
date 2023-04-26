using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;
using MovieRank.Services;

namespace MovieRank
{
	public class Startup
	{
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAWSService<IAmazonDynamoDB>();
            services.AddDefaultAWSOptions(
                new AWSOptions
                {
                    Region = RegionEndpoint.GetBySystemName("us-west-2")
                });
#if OPM
            services.AddSingleton<IMovieRankService, MovieRankOpmService>();
            services.AddSingleton<IMovieRankOpmRepository, MovieRankOpmRepository>();
#elif DM
            services.AddSingleton<IMovieRankService, MovieRankDmService>();
            services.AddSingleton<IMovieRankDmRepository, MovieRankDmRepository>();
#else
            services.AddSingleton<IMovieRankService, MovieRankLlmService>();
            services.AddSingleton<IMovieRankLlmRepository, MovieRankLlmRepository>();
#endif
            services.AddSingleton<ISetupService, SetupService>();
            services.AddSingleton<ISetupRepository, SetupRepository>();
            services.AddSingleton<IMapper, Mapper>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {                
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

