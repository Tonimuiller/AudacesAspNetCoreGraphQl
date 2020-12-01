using BackendChallenge.Core.Repository;
using BackendChallenge.Core.Services;
using BackendChallenge.Dal.Repository;
using BackendChallenge.Graphql.Queries;
using BackendChallenge.Graphql.Schemas;
using BackendChallenge.Graphql.Types;
using BackendChallenge.Services;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackendChallenge.Graphql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureAppDependencies(services);
            services.AddControllers();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseGraphQL<ISchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }

        private void ConfigureAppDependencies(IServiceCollection services)
        {            
            services.AddScoped<ICheckTargetRepository, CheckTargetRepository>();
            services.AddScoped<ICheckTargetService, CheckTargetService>();
            services.AddScoped<CheckTargetQuery>();
            services.AddScoped<CheckTargetType>();
            services.AddScoped<ISchema, CheckTargetSchema>();
            services.AddGraphQL((options, provider) => 
            {
                options.EnableMetrics = true;
            })
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
            .AddSystemTextJson();
        }
    }
}
