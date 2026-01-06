using Core.Entities.Abstract;

namespace DataAccess.Abstract
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        // * --- OKUMA İŞLEMLERİ --- *

        /// <summary>
        ///  Tek bir varlığı ID'sine göre getirir.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter);
        /// <summary>
        ///  Bir varlık listesini filtreye göre veya tümünü getirir.
        ///  (Filtere boşsa tüm varlıkları getirir.)
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> filter = null);

        // * --- YAZMA İŞLEMLERİ --- *
        /// <summary>
        ///     Yeni bir varlık ekler.
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);
        /// <summary>
        ///  Mevcut bir varlığı günceller.
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        ///  Bir varlığı siler.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        /// <summary>
        /// Toplu varlık silme işlemi
        /// </summary>
        void DeleteAll();

    }
}


