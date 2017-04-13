//=================================================================================================
// Class AppServices
// Application services
// Represents an abstract service layer providing access to data related operations.
//=================================================================================================
using System.Collections.Generic;
using System.Linq;
using vms;
using Vms.Db.Services;

namespace Vms.ViewModels
{
   /// <summary>
   /// Application services
   /// Represents an abstract service layer providing access to data related operations.
   /// </summary>
   internal static class AppServices
   {
      #region Internal methods

      /// <summary>
      /// Gets a list of models of the specified type.
      /// </summary>
      /// <typeparam name="T">The model type.</typeparam>
      /// <returns>A list of model instances.</returns>
      internal static IList<T> Get<T>() where T : PersistentEntity => PersistenceService.GetEntities<T>();

      /// <summary>
      /// Gets a user by the specified login.
      /// </summary>
      /// <param name="login">User login.</param>
      /// <returns>User account instance, or null, if no user for the specified login found.</returns>
      internal static UserAccount GetUser(string login)
      {
         var users = PersistenceService.GetEntities<UserAccount>();
         return users.FirstOrDefault(user => user.getUserId().ToLower() == login.ToLower());
      }



      /// <summary>
      /// Saves the specified model instance.
      /// </summary>
      /// <typeparam name="T">The actual model type.</typeparam>
      /// <param name="model">A model instance.</param>
      internal static void Save<T>(T model) where T : PersistentEntity
      {
         PersistenceService.Save(model);
      }



      /// <summary>
      /// Deletes the specified model instance.
      /// </summary>
      /// <typeparam name="T">The actual model type.</typeparam>
      /// <param name="model">A model instance.</param>
      internal static void Delete<T>(T model) where T : PersistentEntity
      {
         PersistenceService.Delete(model);
      }



      /// <summary>
      /// Determines whether any trip exist for the specified request.
      /// </summary>
      /// <param name="request">Request instance.</param>
      /// <returns>True, if any trip exists for the specified request; otherwise, false.</returns>
      internal static bool AnyTripFor(Request request) => 
         Get<Trip>().Any(trip => trip.getRequest().getId() == request.getId());

      #endregion

      #region Private properties

      /// <summary>
      /// Persistence service instance.
      /// </summary>
      private static PersistenceService PersistenceService { get; } = new PersistenceService();

      #endregion
   }
}
