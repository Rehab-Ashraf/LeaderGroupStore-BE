using AutoMapper;
using LeaderGroupStore.Core.DomainEntities;
using LeaderGroupStore.Repositories.Categories;
using LeaderGroupStore.Repositories.Products;
using LeaderGroupStore.Repositories.Users;
using LeaderGroupStore.Services.Categories;
using LeaderGroupStore.Services.Products;
using LeaderGroupStore.Services.Users;
using LeaderGroupStore.Web.Api.Controllers.Categories;
using LeaderGroupStore.Web.Api.Controllers.Products;
using LeaderGroupStore.Web.Api.Controllers.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LeaderGroupStore.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UsersMapper());
                mc.AddProfile(new CategoryMapper());
                mc.AddProfile(new ProductMapper());
            });


            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoriesService>();
            services.AddScoped<ICategoriesRepostiory, CategoriesRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddControllers();
            var connectionString = Configuration.GetConnectionString("LeaderGroupDbConextion");
            services.AddDbContext<LeaderGroupStore_dbContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            services.AddIdentity<User, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["IdentitySettings:Issuer"],
                        ValidAudience = Configuration["IdentitySettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["IdentitySettings:SecurityKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                    builder =>
                                    {
                                        builder.WithOrigins("*");
                                        builder.AllowAnyOrigin();
                                        builder.AllowAnyHeader();
                                        builder.AllowAnyMethod();

                                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Leader Group Store",
                    Description = "Leader Group Store - APIs documentation ",
                    TermsOfService = null,
                    Contact = new OpenApiContact
                    {
                        Name = "Leader Group Store Team.",
                        Email = "rehabashraf063@gmail.com",
                        Url = new Uri("http://c-systems.com")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
              {
                  {
                        new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                          Array.Empty<string>()
                  }
              });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Leader Group Store APIs ver 1.0");
                c.RoutePrefix = "docs";
            });
            
            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
