using Api.Authorization;
using Api.Contexts;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nest;
using System;
using System.Text;

namespace Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAuthorization(options => {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtAuthenticationScheme";
                options.DefaultScheme = "JwtAuthenticationScheme";
                options.DefaultChallengeScheme = "JwtAuthenticationScheme";
            })
            .AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>("JwtAuthenticationScheme", options => {});

            services.AddSingleton<IElasticClient>(new ElasticClient(
                new ConnectionSettings(new Uri(
                    $"http://{Configuration.GetValue<string>("ElasticSearchConnection")}"
                ))
            ));

            services.AddSingleton<TokenService>(
                new TokenService(Configuration.GetSection("Jwt").Get<TokenServiceConfig>()
            ));

            services.AddSingleton<HashService>(
                new HashService(Configuration.GetSection("Hash").Get<HashServiceConfig>()
            ));

            services.AddScoped<RefreshTokensRepository>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<ProductsRepository>();
            services.AddScoped<AdminProductsRepository>();
            
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Api v1");
                });
            }
            
            app.UseAuthentication();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.CreateScope()) {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.ExecuteSqlRaw(
                    @"DO $$ DECLARE
                    r RECORD;
                    BEGIN
                    FOR r IN (SELECT tablename FROM pg_tables WHERE schemaname = current_schema()) LOOP
                        EXECUTE 'DROP TABLE ' || quote_ident(r.tablename) || ' CASCADE';
                    END LOOP;
                    END $$; "
                );
                db.Database.Migrate();
            }
        }
    }
}
