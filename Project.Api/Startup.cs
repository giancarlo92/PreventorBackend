using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Project.Data;
using Project.Repository.Implementation;
using Project.Repository.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;
using Project.Api.GraphQL.Types;
using Project.Api.GraphQL;

namespace Project.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project.CoreApi", Version = "v1" });
                c.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<Startup>();

            services.AddDbContextFactory<PreventorDBContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("PreventorConnection"));
            }, ServiceLifetime.Transient);

            services.AddScoped<IStudentRepository, StudentRepository>();

            services
                .AddGraphQLServer()
                //.RegisterDbContext<PreventorDBContext>()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddType<StudentType>()
                .AddFiltering()
                .AddSorting()
                //.AddProjections()
                .AddInMemorySubscriptions()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.CoreApi v1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(options =>
            {
                options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
                endpoints.MapGraphQLVoyager("ui/voyager");
            });

            app.UseWebSockets();
        }
    }
}