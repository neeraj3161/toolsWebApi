using toolsWebApi.IServices;
using toolsWebApi.Services;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Dialect;
using toolsWebApi.Entity;
using NHibernate;
using System.Reflection;
using NHibernate.Cfg;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IloginService, LoginService>();

var app = builder.Build();

var config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(builder.Configuration.GetConnectionString("DefaultConnection")).AdoNetBatchSize(100);

var nhConfig = Fluently.Configure().Database(config).BuildConfiguration();

var sessionFactory = nhConfig.AddAssembly(Assembly.GetExecutingAssembly()).BuildSessionFactory();


//ISessionFactory sessions = new Configuration().Configure().AddAssembly(Assembly.GetExecutingAssembly()).BuildSessionFactory();

var session = sessionFactory.OpenSession();

var query = session.Get<Users>(2);

sessionFactory.Dispose();

Console.WriteLine($"NHibernate configured successfully!! users: {query}");
Console.ReadLine();

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
