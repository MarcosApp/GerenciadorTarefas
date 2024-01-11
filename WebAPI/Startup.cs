using Application.UseCases.AddProject;
using Application.UseCases.AddReport;
using Application.UseCases.AddTask;
using Application.UseCases.AddUser;
using Domain.Contracts.Repositories.AddProject;
using Domain.Contracts.Repositories.AddReport;
using Domain.Contracts.Repositories.AddTask;
using Domain.Contracts.Repositories.AddUser;
using Domain.Contracts.UseCases.AddProject;
using Domain.Contracts.UseCases.AddReport;
using Domain.Contracts.UseCases.AddTask;
using Domain.Contracts.UseCases.AddUser;
using FluentValidation;
using Infra.Repository.DbContext;
using Infra.Repository.Repository.AddProject;
using Infra.Repository.Repository.AddTask;
using Infra.Repository.Repository.AddUser;
using Infra.Repository.Repository.Reports;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebAPI.Models.Project;
using WebAPI.Models.Task;
using WebAPI.Models.User;

namespace WebAPI
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
            services.AddSingleton<IDbContext, DbContext>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

            services.AddScoped<IProjectUseCase, ProjectUseCase>();
            services.AddScoped<ITaskUseCase, TaskUseCase>();
            services.AddScoped<IUserUseCase, UserUseCase>();
            services.AddScoped<IReportUseCase, ReportUseCase>();

            services.AddTransient<IValidator<AddProjectInput>, AddProjectInputValidator>();
            services.AddTransient<IValidator<AddTaskInput>, AddTaskInputValidator>();
            services.AddTransient<IValidator<UpdateTaskInput>, UpdateTaskInputValidator>();
            services.AddTransient<IValidator<AddUserInput>, AddUserInputValidator>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
