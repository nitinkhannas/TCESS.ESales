#region Namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TCESS.ESales.PersistenceLayer;

#endregion

namespace TCESS.ESales.PersistenceLayer.Interfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        bool SaveAndUpdateSMSDetailsList<T>(List<T> list) where T : class;

        bool SaveAndUpdateListData<T>(List<T> list) where T : class;

        ///<summary>
        /// Save entity to the repository
        ///</summary>
        /// <param name="entity">The entity to save</param>
        void Save(T entity);

        ///<summary>
        /// Mark entity to be deleted within the repository
        ///</summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);
        
        ///<summary>
        /// Returns all entities for a given type
        ///</summary>
        ///<returns>All entities</returns>
        IList<T> RetrieveAll();

        ///<summary>
        /// Update entity to the repository
        ///</summary>
        /// <param name="entity">The entity to update</param>
        bool Update(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> whereCondition);
        
        ///<summary>
        /// Save all changes from repository to store
        ///</summary>
        ///<returns>Total number of objects affected</returns>
        int SaveChanges();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetQuery();

        /// <summary>
        /// Load entity from the repository (always query store)
        /// </summary>
        /// <typeparam name="T">the entity type to load</typeparam>
        /// <param name="where">where condition</param>
        /// <returns>the loaded entity</returns>
        T Load(Expression<Func<T, bool>> whereCondition);

        IList<T> LoadList(Expression<Func<T, bool>> whereCondition);

        /// <summary>
        /// This is an Update method which is beneficial to be used inside the Transaction, where you only want to Update an Entity and do the Save changes later.
        /// </summary>
        /// <typeparam name="T">Type of Entity</typeparam>
        /// <param name="currententity">Object to be Updated.</param>
        /// <returns>Whether the updated object is updated or not.</returns>
        bool TransactionalUpdate<T>(T currententity) where T : class;

        /// <summary>
        /// This is an Insert method which is beneficial to be used inside the Transaction, where you only want to Insert an Entity and do the Save changes later.
        /// </summary>
        /// <typeparam name="T">Type of Entity</typeparam>
        /// <param name="currententity">Object to be inserted</param>
        /// <returns>Whether the object is inserted or not.</returns>
        bool TransactionalInsert<T>(T currententity) where T : class;
    }
}