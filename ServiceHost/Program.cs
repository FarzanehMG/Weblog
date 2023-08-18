using AccountManagement.Configuration;
using ArticleManagement.Configuration;
using Framework.Application;

namespace ServiceHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();


            BlogManagementBootstrapper.Configure(builder.Services,
                builder.Configuration.GetConnectionString("BlogDb"));

            AccountManagementBootstrapper.Configure(builder.Services,
                builder.Configuration.GetConnectionString("BlogDb"));


            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddTransient<IFileUploader, FileUploader>();
            builder.Services.AddTransient<IAuthHelper, AuthHelper>();


            builder.Services.AddHttpContextAccessor();


            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();

        }
    }
}