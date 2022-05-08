using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project.Bll;
using Project.Dal.Abstract;
using Project.Dal.Concrete.EntityFramework.Context;
using Project.Dal.Concrete.EntityFramework.Repository;
using Project.Dal.Concrete.EntityFramework.UnitOfWork;
using Project.Entity.Dto;
using Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.WebAPI
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
            #region JWT Token Service
            services
                 .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(cfg =>
                 {
                     cfg.SaveToken = true;
                     cfg.RequireHttpsMetadata = false;

                     cfg.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidIssuer = Configuration["Tokens:Issuer"],
                         ValidAudience = Configuration["Tokens:Issuer"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                         RequireSignedTokens = true,
                         RequireExpirationTime = true
                     };
                 });

            #endregion

            #region Application Context
            services.AddDbContext<ProjectContext>
                (
                    ob => ob.UseSqlServer(Configuration.GetConnectionString("SqlServer"))
                );
            services.AddScoped<DbContext, ProjectContext>();
            #endregion

            #region Service Section
            services.AddScoped<IDepartmentService, DepartmentManager>();
            services.AddScoped<IMessageService, MessageManager>();
            services.AddScoped<IPriorityService, PriorityManager>();
            services.AddScoped<IRequestService, RequestManager>();
            services.AddScoped<IRoleService, RoleeManager>();
            services.AddScoped<IUserService, UserrManager>();
            #endregion

            #region Repository Section
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPriorityRepository, PriorityRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            #endregion

            #region Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region E-mail
            services.Configure<DtoMailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, MailManager>();
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.WebApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
