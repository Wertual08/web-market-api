using Api.Authorization;
using Api.Database;
using Api.Domain.Repositories;
using Api.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nest;
using System;
using Api.Domain.Services;
using Api.FullTextSearch;
using System.Collections.Generic;
using Api.FullTextSearch.Models;
using System.Linq;
using System.Threading;

namespace Api {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                options.UseSnakeCaseNamingConvention();
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

            services.AddSingleton<IElasticsearchContext>(new ElasticsearchContext(
                Configuration.GetValue<string>("ElasticSearchConnection")
            ));

            services.AddSingleton<TokenManager>(
                new TokenManager(Configuration.GetSection("Jwt").Get<TokenManagerConfig>()
            ));

            services.AddSingleton<HashManager>(
                new HashManager(Configuration.GetSection("Hash").Get<HashServiceConfig>()
            ));

            services.AddScoped<RecordsRepository>();
            services.AddScoped<ProfileRepository>();
            services.AddScoped<RefreshTokensRepository>();
            services.AddScoped<UsersRepository>();
            services.AddScoped<ProductsRepository>();
            services.AddScoped<OrdersRepository>();
            services.AddScoped<CategoriesRepository>();
            services.AddScoped<SectionsRepository>();
            services.AddScoped<OrderProductsRepository>();
            services.AddScoped<CartProductsRepository>();
            services.AddScoped<AdminProductsRepository>();
            services.AddScoped<AdminCategoriesRepository>();
            services.AddScoped<AdminSectionsRepository>();
            services.AddScoped<SearchRepository>();

            services.AddScoped<RecordsService>();
            services.AddScoped<ProfileService>();
            services.AddScoped<OrdersService>();
            services.AddScoped<CategoriesService>();
            services.AddScoped<SectionsService>();
            services.AddScoped<CartService>();
            services.AddScoped<AdminProductsService>();
            services.AddScoped<AdminCategoriesService>();
            services.AddScoped<AdminSectionsService>();
            services.AddScoped<SearchService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (!env.IsProduction()) {
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
                // --- Wipe database ---
                //db.Database.ExecuteSqlRaw(
                //    @"DO $$ DECLARE
                //    r RECORD;
                //    BEGIN
                //    FOR r IN (SELECT tablename FROM pg_tables WHERE schemaname = current_schema()) LOOP
                //        EXECUTE 'DROP TABLE ' || quote_ident(r.tablename) || ' CASCADE';
                //    END LOOP;
                //    END $$;"
                //);
                // --- ------------- ---
                db.Database.Migrate();
                db.SeedConstants();

                // --- Index products ---
                //Thread.Sleep(1000 * 80);
                //var adminProductsRepository = scope.ServiceProvider.GetRequiredService<AdminProductsRepository>();
                //var searchRepository = scope.ServiceProvider.GetRequiredService<SearchRepository>();
                //int page = 0;
                //bool running;
                //do {
                //    running = false;
                //    var products = await adminProductsRepository.ListAsync(page++ * 100, 100);
                //    foreach (var product in products) {
                //        var categories = new List<long>();
                //        foreach (var category in product.Categories) {
                //            categories.Add(category.Id);
                //        }
                //
                //        var sections = new List<long>();
                //        foreach (var section in product.Sections) {
                //            sections.Add(section.Id);
                //        }
                //        
                //        await searchRepository.IndexAsync(new FTSProduct {
                //            Id = product.Id,
                //            Name = product.Name,
                //            Description = product.Description,
                //            Price = product.Price, 
                //            Image = product.Records.FirstOrDefault()?.Identifier.ToString("N"),
                //            Categories = categories,
                //            Sections = sections,
                //        });
                //        running = true;
                //    }
                //} while (running);
                // --- -------------- ---
            }
        }
    }
}
