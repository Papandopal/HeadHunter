using Domain.Entities;
using EntityFrameworkCore.Triggered;
using Infrastructure.Database;
using Infrastructure.Database.Triggers.BeforeUpdating.ChangeVersion;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UseCases.Database;
using UseCases.Services;
using UseCases.Services.AuthServices;
using UseCases.Services.AuthServices.Interfaces;
using UseCases.Services.CandidateServices;
using UseCases.Services.CandidateServices.Interfaces;
using UseCases.Services.CategoryServices;
using UseCases.Services.CategoryServices.Interfaces;
using UseCases.Services.CVServices;
using UseCases.Services.CVServices.Interfaces;
using UseCases.Services.Entities.PositionSkillsServices;
using UseCases.Services.Entities.PositionSkillsServices.Interfaces;
using UseCases.Services.Entities.ValuedSkillServices.AccessRuleServices;
using UseCases.Services.Entities.ValuedSkillServices.AccessRuleServices.Interfaces;
using UseCases.Services.Formaters;
using UseCases.Services.Formaters.Interfaces;
using UseCases.Services.ImageServices;
using UseCases.Services.ImageServices.Interfaces;
using UseCases.Services.PositionServices;
using UseCases.Services.PositionServices.Interfaces;
using UseCases.Services.ProjectServices;
using UseCases.Services.ProjectServices.Interfaces;
using UseCases.Services.ProjectTagServices;
using UseCases.Services.ProjectTagServices.Interfaces;
using UseCases.Services.RecruterServices;
using UseCases.Services.RecruterServices.Interfaces;
using UseCases.Services.SkillServices;
using UseCases.Services.SkillServices.Interfaces;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices;
using UseCases.Services.ValuedSkillServices.CandidateSkillServices.Interfaces;

namespace ITransitionProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();

            builder.Services
               .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.Name = builder.Configuration["AuthCookieName"];
                   options.LoginPath = "/";
                   options.AccessDeniedPath = "/";
                   options.SlidingExpiration = true;
                   options.ExpireTimeSpan = TimeSpan.FromDays(30);
               })
               .AddGoogle(options=>
               {
                   options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                   options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
               });
            
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection"));
                options.UseTriggers();
            });

            builder.Services.AddAuthorization();

            builder.Services.AddAutoMapper(configAction =>
            {
                configAction.AddProfile<Mapper>();
            });

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<ICandidateService, CandidateService>();
            builder.Services.AddTransient<IRecruterService, RecruterService>();
            builder.Services.AddTransient<ICandidateSkillService, CandidateSkillService>();
            builder.Services.AddTransient<ISkillService, SkillService>();
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IPositionService, PositionService>();
            builder.Services.AddTransient<IPositionSkillService, PositionSkillService>();
            builder.Services.AddTransient<IAccessRuleService, AccessRuleService>();
            builder.Services.AddTransient<IProjectTagService, ProjectTagService>();
            builder.Services.AddTransient<ICVService, CVService>();
            builder.Services.AddTransient<IProjectService, ProjectService>();
            builder.Services.AddTransient<IImageService, ImageService>();

            builder.Services.AddTransient<IMarkdownTextFormater, MarkdownTextFormater>();
            builder.Services.AddTransient<IOneOfManyFormater, OneOfManyFormater>();
            builder.Services.AddTransient<IDatePeriodFormater, DatePeriodFormater>();

            builder.Services.AddTransient<IBeforeSaveTrigger<CandidateSkill>, ChangeCandidateSkillVersionTrigger>();
            builder.Services.AddTransient<IBeforeSaveTrigger<Position>, ChangePositionVersionTrigger>();
            builder.Services.AddTransient<IBeforeSaveTrigger<Skill>, ChangeSkillVersionTrigger>();
            builder.Services.AddTransient<IBeforeSaveTrigger<Project>, ChangeProjectVersionTrigger>();

            builder.Services.AddTransient<AccessValidator>();
            builder.Services.AddTransient<CryptService>();
            builder.Services.AddTransient<AlertService>();
            builder.Services.AddTransient<SupportedAccessRuleSkillTypes>();

            builder.Services.AddProblemDetails();

            builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "data-protection-keys")));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Auth/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}")
                .WithStaticAssets();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            app.Run();
        }
    }
}
