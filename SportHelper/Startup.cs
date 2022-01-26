using AutoMapper;
using Business.Repositories.DataRepositories;
using Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Data;
using Repository.Repositories;
using System.Globalization;


namespace Lab1
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfigurationRoot _configuration;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
            _configuration = new ConfigurationBuilder()
                .SetBasePath(_environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{_environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_configuration);

           services.AddDbContext<Context>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")), contextLifetime: ServiceLifetime.Singleton, optionsLifetime: ServiceLifetime.Transient);

            services.AddScoped<INutritionRepository, NutritionRepository>();
            services.AddScoped<ISleepRepository, SleepRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<ITrainingExerciseRepository, TrainingExerciseRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new ServiceMappingProfile()));

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllersWithViews();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INutritionService, NutritionService>();
            services.AddScoped<ISleepService, SleepService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<ITrainingExerciseService, TrainingExerciseService>();
            services.AddScoped<IExerciseService, ExerciseService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.Name);
            cultureInfo.NumberFormat.NumberDecimalSeparator = "."; 
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}"));
        }
    }
}
