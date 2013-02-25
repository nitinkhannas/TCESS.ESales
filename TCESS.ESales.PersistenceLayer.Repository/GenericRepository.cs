using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using TCESS.ESales.PersistenceLayer.Interfaces;
using TCESS.ESales.PersistenceLayer.EF;

namespace TCESS.ESales.PersistenceLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        public IObjectSet<T> GetObjectSet()
        {
            return ObjectContextManager.GetObjectContext().CreateObjectSet<T>();
        }
        
        /// <summary>
        /// Returns the active object context
        /// </summary>
        public ObjectContext ObjectContext
        {
            get
            {
                return ObjectContextManager.GetObjectContext();
            }
        }

        public EntitySetBase GetBase<TEntity>(ObjectContext context)
        {
            EntityContainer container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);

            EntitySetBase entitySet = container.BaseEntitySets.Where(item => item.ElementType.Name.Equals(typeof(TEntity).Name))
                                                              .FirstOrDefault();

            return entitySet;
        }

        #region IGenericRepository<T> Members

        ///<summary>
        /// Save entity to the repository
        ///</summary>
        /// <param name="entity">The entity to save</param>
        public void Save(T entity)
        {
            ObjectContext.AddObject(GetBase<T>(ObjectContext).Name, entity);
            SaveChanges();
        }

        ///<summary>
        /// Mark entity to be deleted within the repository
        ///</summary>
        /// <param name="entity">The entity to delete</param>
        public void Delete(T entity)
        {
            GetObjectSet().Attach(entity);
            ObjectContext.DeleteObject(entity);
            SaveChanges();
        }

        ///<summary>
        /// Returns all entities for a given type
        ///</summary>
        ///<returns>All entities</returns>
        public IList<T> RetrieveAll()
        {
            //return the value
            return GetQuery().ToList();
        }        

        public bool Update(T entity)
        {
            object originalItem = null;
            EntityKey key = ObjectContext.CreateEntityKey(GetBase<T>(ObjectContext).Name.ToString(), entity);
                        
            if (ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                ObjectContext.ApplyCurrentValues(key.EntitySetName, entity);
            }

            SaveChanges();

            //return the value
            return true;
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="whereCondition">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        /// <remarks></remarks>
        public T GetSingle(Expression<Func<T, bool>> whereCondition)
        {
            return GetQuery().FirstOrDefault(whereCondition);
        }

        ///<summary>
        /// Save all changes from repository to store
        ///</summary>
        ///<returns>Total number of objects affected</returns>
        public int SaveChanges()
        {
            return ObjectContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetQuery()
        {
            //return the value
            return this.ObjectContext.CreateObjectSet<T>();
        }
        public IQueryable<T> GetQuery(string includeTableName)
        {
            //return the value
            return this.ObjectContext.CreateObjectSet<T>().Include(includeTableName);
        }
        /// <summary>
        /// Load entity from the repository (always query store)
        /// </summary>
        /// <param name="whereCondition">where condition</param>
        /// <returns>The loaded entity</returns>
        public T Load(Expression<Func<T, bool>> whereCondition)
        {
            //return the value
            return this.GetObjectSet().Where(whereCondition).FirstOrDefault<T>();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IList<T> RetrieveAll(string query, ObjectParameter[] parameters)
        {
            //return the value
            return ObjectContext.CreateQuery<T>(query, parameters).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public IQueryable<T> GetQuery(Expression<Func<T, bool>> whereCondition)
        {
            //return the value
            return ObjectContext.CreateObjectSet<T>().Where(whereCondition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public int SaveChanges(SaveOptions options)
        {
            return this.ObjectContext.SaveChanges(options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            IQueryable<T> m_querybase = GetObjectSet();

            //return the value
            return m_querybase.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> whereCondition)
        {
            IQueryable<T> m_querybase = GetObjectSet();

            //return the value
            return m_querybase.Where(whereCondition).Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public IList<T> LoadList(Expression<Func<T, bool>> whereCondition)
        {
            var entityList = this.GetObjectSet().Where(whereCondition).ToList();

            //return the value
            return entityList;
        }

        /// <summary>
        /// This is an Update method which is beneficial to be used inside the Transaction, where you only want to Update an Entity and do the Save changes later.
        /// </summary>
        /// <typeparam name="T">Type of Entity</typeparam>
        /// <param name="currententity">Object to be Updated.</param>
        /// <returns>Whether the updated object is updated or not.</returns>

        public bool TransactionalUpdate<T>(T entity) where T : class
        {
            object originalItem = null;
            EntityKey key = ObjectContext.CreateEntityKey(GetBase<T>(ObjectContext).Name.ToString(), entity);

            if (ObjectContext.TryGetObjectByKey(key, out originalItem))
            {
                ObjectContext.ApplyCurrentValues(key.EntitySetName, entity);
            }

         

            //return the value
            return true;
           
        }


        /// <summary>
        /// This is an Insert method which is beneficial to be used inside the Transaction, where you only want to Insert an Entity and do the Save changes later.
        /// </summary>
        /// <typeparam name="T">Type of Entity</typeparam>
        /// <param name="currententity">Object to be inserted</param>
        /// <returns>Whether the object is inserted or not.</returns>

        public bool TransactionalInsert<T>(T currententity) where T : class
        {
            ObjectContext.AddObject(GetBase<T>(ObjectContext).Name, currententity);
            return true;
        }


        public bool SaveAndUpdateSMSDetailsList<T>(List<T> list) where T : class
        {
            foreach (T obj in list)
                {
                    object originalItem = default(T);
                    EntityKey key = ObjectContext.CreateEntityKey(GetBase<T>(ObjectContext).Name, obj);
                    if (ObjectContext.TryGetObjectByKey(key, out originalItem))
                    {
                        ObjectContext.ApplyCurrentValues(key.EntitySetName, obj);
                        ObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);
                    }
                }
            
            return true;
        }

        public bool SaveAndUpdateListData<T>(List<T> list) where T : class
        {
            foreach (T obj in list)
            {
                object originalItem = default(T);
                EntityKey key = ObjectContext.CreateEntityKey(GetBase<T>(ObjectContext).Name, obj);
                if (ObjectContext.TryGetObjectByKey(key, out originalItem))
                {
                    ObjectContext.ApplyCurrentValues(key.EntitySetName, obj);
                    ObjectContext.SaveChanges(SaveOptions.DetectChangesBeforeSave);
                }
            }

            return true;
        }

    }
}