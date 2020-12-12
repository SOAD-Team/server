using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Server.Persistence;
using AutoMapper;
using Server.Mapping;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                  "CorsPolicy",
                  builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = long.MaxValue);

            services.AddDbContext<MoviesDB>();
            services.AddScoped<MoviesDB>();

            services.AddScoped<UserRepository>();

            services.AddScoped<ReviewRepository>();

            services.AddScoped<GenreRepository>();

            services.AddScoped<LanguageRepository>();

            services.AddScoped<StyleRepository>();

            services.Configure<ImagesDatabaseSettings>(
                Configuration.GetSection(nameof(ImagesDatabaseSettings)));

            services.AddSingleton<IImagesDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ImagesDatabaseSettings>>().Value);

            services.AddSingleton<ImagesDB>();
            services.AddSingleton<IImagesDB>(sp => sp.GetRequiredService<ImagesDB>());

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile(provider.GetService<IImagesDB>(), provider.GetService<MoviesDB>()));
            }).CreateMapper());

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
