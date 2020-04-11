using Ciceksepeti.Business.ValidationRules;
using Ciceksepeti.Core.Filters.ValidationFilter;
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

namespace Ciceksepeti.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO Db Con
            var asd = "Server=localhost\\MSSQLSERVER01;Database = CartDb;Trusted_Connection = True;Integrated Security = True;";
            services.AddDbContext<CartContext>(options => options.UseSqlServer(asd));
            //services.AddDbContext<CartContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<Business.Interface.ICartService, Business.Service.CartService>();
            services.AddTransient<DataAccess.Interface.ICartRepository, DataAccess.Service.CartRepository>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CicekSepeti", Version = "v1" });
            });

            services.AddTransient<IValidator<CartRequestDto>, CartRequestDtoValidation>();

            services.AddControllers(options=> { options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
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
