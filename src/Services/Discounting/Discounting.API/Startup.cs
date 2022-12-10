using FluentValidation.AspNetCore;
using Discounting.API.Controllers;
using Discounting.API.Extensions;
using Discounting.API.Options;
using Discounting.Domain.AggregateModel.DiscountAggregate;
using Discounting.Infrastructure.Repositories;
using Discounting.API.Application.Commands.CreateDiscount;

namespace Discounting.API
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
            services.AddDiscountContext(Configuration);
            services.AddScoped<IDiscountRepository, DiscountRepository>();



            services.AddControllers()
                 // Added for functional tests
                 .AddApplicationPart(typeof(DiscountController).Assembly)
                 .AddFluentValidation(options =>
                 {
                     options.RegisterValidatorsFromAssemblyContaining<CreateDiscountValidator>();
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
