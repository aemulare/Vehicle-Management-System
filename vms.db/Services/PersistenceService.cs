using System.Collections.Generic;
using System.Linq;
using NHibernate.Cfg;
using vms;

namespace Vms.Db.Services
{
   public class PersistenceService
   {
      /// <summary>
      /// Gets a collection of all entities from DB.
      /// </summary>
      /// <returns>A collection of all entities of the specified type in the database.</returns>
      public IList<T> GetEntities<T>() where T : PersistentEntity
      {
         var factory = new Configuration().Configure().BuildSessionFactory();
         using(var session = factory.OpenSession())
         {
            var criteria = session.CreateCriteria<T>();
            return criteria.List<T>();
         }
      }



      public T GetEntity<T>(int id) where T : PersistentEntity
      {
         var entities = GetEntities<T>();
         return entities.FirstOrDefault(item => item.getId() == id);
      }



      /// <summary>
      /// Saves the entity instance in the database.
      /// </summary>
      /// <param name="entity">Persistent entity instance.</param>
      public void Save<T>(T entity) where T : PersistentEntity
      {
         var factory = new Configuration().Configure().BuildSessionFactory();
         using(var session = factory.OpenSession())
         using(var trans = session.BeginTransaction())
         {
            session.SaveOrUpdate(entity);
            trans.Commit();
         }
      }



      /// <summary>
      /// Deletes the persistent entity.
      /// </summary>
      /// <param name="entity">Persistent entity instance.</param>
      public void Delete<T>(T entity) where T : PersistentEntity
      {
         var factory = new Configuration().Configure().BuildSessionFactory();
         using(var session = factory.OpenSession())
         using(var trans = session.BeginTransaction())
         {
            session.Delete(entity);
            trans.Commit();
         }
      }
   }
}
