using Core.Entities.Abstract;
using DataAccess.Abstract;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRepositoryBase<T> : IRepository<T> where T : class, IEntity, new()
    {
        private ECommerceDbContext context;

        public EfRepositoryBase()
        {
            context = new ECommerceDbContext();
        }

        // * --- Temel CRUD İşlemleri --- *
        public void Add(T entity)
        {
            // Eklenen kod bloğu using ifadesi ile DbContext'in işi bitince hafızadan silinmesini sağlar.
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
            context.SaveChanges();
        }

        public void DeleteAll()
        {
            // 1. Tüm varlıkları belleğe çek
            var entitiesToDelete = context.Set<T>().ToList();

            // 2. Change Tracker'a silinmesi gerektiğini söyle
            context.RemoveRange(entitiesToDelete);

            // 3. Değişiklikleri veritabanına uygula (N kadar DELETE sorgusu gönderebilir)
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Update(entity);
            context.SaveChanges();
        }

        // * --- Sorgulama İşlemleri --- *
        public T Get(Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().SingleOrDefault(filter); // Tek bir kayıt döner
        }

        public List<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            // Filtre yoksa tüm kayıtları döner, varsa filtreye göre döner
            return filter == null
                ? context.Set<T>().ToList()
                : context.Set<T>().Where(filter).ToList();
        }

    }
}
