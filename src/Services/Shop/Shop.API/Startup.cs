using FluentValidation.AspNetCore;
using Shopping.API.Application.Commands.CreateShop;
using Shopping.API.Controllers;
using Shopping.API.Extensions;
using Shopping.API.Options;
using Shopping.Domain.AggregateModel.CategoryAggregate;
using Shopping.Domain.AggregateModel.ShopAggregate;
using Shopping.Infrastructure.Repositories;

namespace Shop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddShopContext(Configuration);
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            services.AddControllers()
                 // Added for functional tests
                 .AddApplicationPart(typeof(ShopController).Assembly)
                 .AddFluentValidation(options =>
                 {
                     options.RegisterValidatorsFromAssemblyContaining<CreateShopValidator>();
                 });


            services.AddCustomSwaggerGen();
            services.Configure<ConnectionStringsOptions>(Configuration.GetSection(ConnectionStringsOptions.ConnectionStrings));
            services.AddCustomMediatR();



            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });


            return services.BuildAutofacServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            string pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                logger.LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCustomSwagger(pathBase);
            }

            //app.UseApiExceptionHandler();

            app.UseRouting();
            app.UseCors("CorsPolicy");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });


        }



    }
}
