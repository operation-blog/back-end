using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GC.BLL.Abstractions;
using GC.BLL.Services;
using GF.DAL;
using GF.DAL.Abstractions;
using GF.DAL.Entities;
using GF.DAL.Repositories;
using AutoMapper;
using GC.API.Mappings;
using GC.API.Middlewares;
using System.Text;
using GC.DTO.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using GC.API.Filters;

namespace GC.API
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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:3000", "http://localhost:3000")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowAnyOrigin();
                    });
            });


            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:ConnectionString"]);
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Blog>, GenericRepository<Blog>>();
            services.AddScoped<IGenericRepository<AccessToken>, GenericRepository<AccessToken>>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();

            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            services.AddControllers().ConfigureApiBehaviorOptions(options => { options.InvalidModelStateResponseFactory = actionContext => { return new BadRequestObjectResult(new { status = 0, message = actionContext.ModelState.First().Value.Errors.First().ErrorMessage }); }; });

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<OptionalFilter>();

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GC.API", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                c.AddSecurityRequirement(securityRequirement);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GC.API v1"));
            }
             else
            {
                app.UseStatusCodePages(async context =>
                {
                    var response = context.HttpContext.Response;

                    ErrorResponseDTO errorMessage = null;

                    if (response.StatusCode == 404)
                        errorMessage = new ErrorResponseDTO { Status = 0, Message = "Invalid Endpoint" };

                    else if (response.StatusCode == 405)
                        errorMessage = new ErrorResponseDTO { Status = 0, Message = "Invalid Method" };

                    var serializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
                    serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

                    var responeString = Newtonsoft.Json.JsonConvert.SerializeObject(errorMessage, serializerSettings);

                    await response.Body.WriteAsync(Encoding.ASCII.GetBytes(responeString));
                });
            }

            app.UseHttpsRedirection();

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                var response = new { status = 0, message = "Internal Server Error" };

                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
