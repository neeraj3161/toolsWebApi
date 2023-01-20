using toolsWebApi.IServices;
using toolsWebApi.Services;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using toolsWebApi.Entity;
using System.Reflection;
using toolsWebApi.IServices.hibernateConfig;
using toolsWebApi.IServices.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IloginService, LoginService>();

builder.Services.AddScoped<IHibernateConfig, HibernateConfig>();
builder.Services.AddScoped<IEmailConfig, EmailConfig>();

builder.Services.AddScoped<IGenerateOtp, GenerateOtp>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();


builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(5));

builder.Services.AddMvc();

builder.Services.AddDataProtection();


var app = builder.Build();

app.UseSession();

var config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(builder.Configuration.GetConnectionString("DefaultConnection")).AdoNetBatchSize(100);

var nhConfig = Fluently.Configure().Database(config).BuildConfiguration();

var sessionFactory = nhConfig.AddAssembly(Assembly.GetExecutingAssembly()).BuildSessionFactory();


var session = sessionFactory.OpenSession();

var query = session.Get<Users>(2);

sessionFactory.Dispose();

Console.WriteLine($"NHibernate configured successfully!! users: {query}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
