using DebtusTestTask.DB;
using Microsoft.EntityFrameworkCore;
using DebtusTestTask.DB.DiContainer;
using DebtusTestTask.API.AutoMapper;

namespace DebtusTestTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlite("Data Source=debtustesttask.db");
            });
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddRepositories();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
