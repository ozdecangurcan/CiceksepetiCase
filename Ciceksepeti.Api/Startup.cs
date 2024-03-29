using Ciceksepeti.Api.Filters.ValidationFilter;
using Ciceksepeti.Business.ValidationRules;
using Ciceksepeti.DataAccess;
using Ciceksepeti.Dto.Cart;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Ciceksepeti.Api
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
            #region Db Connection

            services.AddDbContext<CartContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                options.UseInMemoryDatabase("CartDb")
            );

            #endregion

            #region Services DI

            services.AddTransient<Business.Interface.ICartService, Business.Service.CartService>();

            services.AddTransient<DataAccess.Interface.ICartRepository, DataAccess.Service.CartRepository>();

            #endregion

            #region Swagger

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CicekSepeti", Version = "v1" });
            });

            #endregion

            #region Fluent Validation DI

            services.AddTransient<IValidator<CartRequestDto>, CartRequestDtoValidation>();

            services.AddTransient<IValidator<UpdateCartUserRequestDto>, UpdateCartUserDtoValidation>();

            services.AddTransient<IValidator<CartUpdateRequestDto>, CartUpdateRequestDtoValidation>();

            services.AddTransient<IValidator<CartDeleteRequestDto>, CartDeleteRequestDtoValidation>();

            services.AddControllers(options=> { options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ciceksepeti");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
