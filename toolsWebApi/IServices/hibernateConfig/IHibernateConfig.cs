using System.Linq;

namespace toolsWebApi.IServices.hibernateConfig
{
    public interface IHibernateConfig :IEntity
    {
        public IEntity Read<IEntity>(int id);

        IQueryable<T> Query<T>();

        void DisposeSession();

        void OpenSessionFactory();

        void PersistData<ITenantEntity>(ITenantEntity entity);

        void DeleteData<ITenantEntity>(ITenantEntity entity);




    }
}
