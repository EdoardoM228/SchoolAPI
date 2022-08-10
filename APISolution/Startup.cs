using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APISolution.Authentication;
using APISolution.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace APISolution
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
            services.AddDbContext<ClassmateContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("ServerConnection")));

            IConfiguration jwtSection = Configuration.GetSection("Jwt");

            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
                //setupAction.ApiVersionReader = new HeaderApiVersionReader("api-version");
                //setupAction.ApiVersionReader = new MediaTypeApiVersionReader();
            });

            services.AddVersionedApiExplorer(setupAction =>
            {
                setupAction.GroupNameFormat = "'v'VV";
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(setupAction =>
            {
                setupAction.RequireHttpsMetadata = false;
                setupAction.SaveToken = true;
                setupAction.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSection.GetSection("Audience").Value,
                    ValidIssuer = jwtSection.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.GetSection("Key").Value)),
                };
            });
            //services.AddAuthentication("Basic")
            //.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            services.AddScoped<IClassmateRepo, ServerClassmateRepo>();

            var apiVersionDescriptionProvider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(setupAction =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerDoc($"ClassmatesOpenAPISpecification{description.GroupName}", new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "School API",
                        Version = description.ApiVersion.ToString()
                    });

                    //setupAction.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme()
                    //{
                    //    Type = SecuritySchemeType.Http,
                    //    Scheme = "basic",
                    //    Description = "Input your username and password to access this"
                    //});

                    setupAction.DocInclusionPredicate((documentName, ApiDescription) =>
                    {
                        var actionApiVersionModel = ApiDescription.ActionDescriptor
                        .GetApiVersionModel(ApiVersionMapping.Explicit | ApiVersionMapping.Implicit);

                        if (actionApiVersionModel == null)
                        {
                            return true;
                        }

                        if (actionApiVersionModel.DeclaredApiVersions.Any())
                        {
                            return actionApiVersionModel.DeclaredApiVersions.Any(v =>
                            $"ClassmatesOpenAPISpecificationv{v.ToString()}" == documentName);
                        }
                        return actionApiVersionModel.ImplementedApiVersions.Any(v =>
                        $"ClassmatesOpenAPISpecificationv{v.ToString()}" == documentName);
                    });
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerEndpoint($"/swagger/ClassmatesOpenAPISpecification{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                    setupAction.RoutePrefix = "";
                }
            });

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
