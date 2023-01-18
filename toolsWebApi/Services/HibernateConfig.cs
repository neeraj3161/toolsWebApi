using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using System.Reflection;
using toolsWebApi.Entity;
using toolsWebApi.IServices.hibernateConfig;
using NHibernate;
using ISession = NHibernate.ISession;

namespace toolsWebApi.Services
{
    public class HibernateConfig : IHibernateConfig
    {
        public IQueryable<T> Query<T>()
        {
            var result = _session.Query<T>();
            return result;
        }

        public ITenantEntity Read<ITenantEntity>(int id)
        {
            var result = _session.Get<ITenantEntity>(id);
            return result;
        }

        public void PersistData<ITenantEntity>(ITenantEntity entity)
        {
            _session.Save(entity);
        }


        public void DeleteData<ITenantEntity>(ITenantEntity entity)
        {
            _session.Delete(entity);
        }
        public void DisposeSession()
        { 
            _session.Dispose();
        }

        public void CloseSessionFactory()
        {
            _sessionFactory.Close();
        }

  
        public void OpenSessionFactory()
        { 
            _session = _sessionFactory.OpenSession();
        }

        public void FlushSession()
        {
            _session.Flush();
        }


        private  ISession _session;
        private readonly WebApplicationBuilder builder;
        private readonly PostgreSQLConfiguration config;
        private readonly ISessionFactory _sessionFactory;
        private readonly NHibernate.Cfg.Configuration nhConfig;

        public int Id => throw new NotImplementedException();

        public HibernateConfig() {

             builder = WebApplication.CreateBuilder();
             config = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(builder.Configuration.GetConnectionString("DefaultConnection")).AdoNetBatchSize(100);
             nhConfig = Fluently.Configure().Database(config).BuildConfiguration();
             _sessionFactory = nhConfig.AddAssembly(Assembly.GetExecutingAssembly()).BuildSessionFactory();


            //ISessionFactory sessions = new Configuration().Configure().AddAssembly(Assembly.GetExecutingAssembly()).BuildSessionFactory();

            //var _session = _sessionFactory.OpenSession();

            //var query = session.Get<Users>(2);

            //_sessionFactory.Dispose();

           
        }
    }
}
