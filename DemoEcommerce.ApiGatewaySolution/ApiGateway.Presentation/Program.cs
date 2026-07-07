
using eCommerce.SharedLibary.DI;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("ocelot.json",optional: false , reloadOnChange:true);
            builder.Services.AddOcelot().AddCacheManager(x => x.WithDictionaryHandle());
            // Add services to the container.
            JWTAuthenticationScheme.AddJWTAuthenticationScheme(builder.Services, builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });   
             });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
            app.UseCors();
            app.Use(async (context, next) =>
            {
                context.Request.Headers["Api-Gateway"] = "Signed";
                await next(context);
            });
            app.UseOcelot().Wait();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
