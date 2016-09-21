using System;
using System.Collections;
using System.Collections.Generic;
using IC.DataModels;

namespace IC.DataAccess.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        /// <summary>
        /// Object of database Context.
        /// </summary>
        private IndianChopstixEntities dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            this.dbContext = new IndianChopstixEntities();
            this.dbContext.Configuration.AutoDetectChangesEnabled = false;
        }

        /// <summary>
        /// Gets the database interface.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="parameters">The parameters with database Commit as optional parameter.</param>
        /// <returns>Instance of DataContext</returns>
        public T GetDbInterface<T>(params object[] parameters)
        {
            try
            {
                Type type = typeof(T);
                List<Type> types = new List<Type>();
                ArrayList paramsList = new ArrayList();

                types.Add(typeof(IndianChopstixEntities));
                paramsList.Add(this.dbContext);

                for (int count = 0; count < parameters.Length; count++)
                {
                    if (parameters[count].GetType() == typeof(bool))
                    {
                        // override _dbCommit to user passed value.
                        paramsList[paramsList.IndexOf(true)] = parameters[count];
                    }
                    else
                    {
                        types.Add(parameters[count].GetType());
                        paramsList.Add(parameters[count]);
                    }
                }

                return (T)type.GetConstructor(types.ToArray()).Invoke(paramsList.ToArray());
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Commits the database changes.
        /// </summary>
        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.dbContext.Dispose();
        }
    }
}
