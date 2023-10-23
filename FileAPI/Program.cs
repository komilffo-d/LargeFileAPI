using Database;
using FileAPI.Misc;
using FileAPI.Misc.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
namespace FileAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();
            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());


            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FileAppAPI")));

            builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScopes();
            /*����� �� ����������� ����� - 1 GiB (��������)*/
            builder.Services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 1_024_000_000;
            });
            builder.Services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 1_024_000_000;
            });
            builder.Services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = 1_024_000_000;
                options.MultipartBodyLengthLimit = 1_024_000_000;
                options.MultipartHeadersLengthLimit = 1_024_000_000;
            });
            var app = builder.Build();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
/*            app.UseRouting();*/
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}