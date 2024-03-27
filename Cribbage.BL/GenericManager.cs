using Microsoft.Extensions.Logging;

namespace Cribbage.BL
{
    public abstract class GenericManager<T> where T : class, IEntity
    {
        protected DbContextOptions<CribbageEntities> options;
        protected readonly ILogger logger;

        public GenericManager(DbContextOptions<CribbageEntities> options)
        {
            this.options = options;
        }

        public GenericManager(ILogger logger,
                                DbContextOptions<CribbageEntities> options)
        {
            this.options = options;
            this.logger = logger;
        }

        public GenericManager() { }

        public static string[,] ConvertData<U>(List<U> entities, string[] columns) where U : class
        {
            string[,] data = new string[entities.Count + 1, columns.Length];

            int counter = 0;
            for (int i = 0; i < columns.Length; i++)
            {
                data[counter, i] = columns[i];
            }
            counter++;


            foreach (var entity in entities)
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    data[counter, i] = entity.GetType().GetProperty(columns[i]).GetValue(entity, null).ToString();
                }
                counter++;
            }
            return data;
        }

        public List<T> Load()
        {
            try
            {
                if (logger != null) logger.LogWarning($"Get {typeof(T).Name}s");
                return new CribbageEntities(options)
                    .Set<T>()
                    .ToList<T>()
                    .OrderBy(x => x.SortField)
                    .ToList<T>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<T> Load(string storedproc)
        {
            try
            {
                return new CribbageEntities(options)
                    .Set<T>()
                    .FromSqlRaw($"exec {storedproc}")
                    .ToList<T>()
                    .OrderBy(x => x.SortField)
                    .ToList<T>();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public T LoadById(Guid id)
        {
            try
            {
                return new CribbageEntities(options).Set<T>().Where(t => t.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Insert(T entity, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    // also need to check if it already exists

                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    entity.Id = Guid.NewGuid();

                    dc.Set<T>().Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) dbTransaction.Rollback();

                }

                return results;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Update(T entity, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    dc.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    results = dc.SaveChanges();

                    if (rollback) dbTransaction.Rollback();

                }

                return results;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Delete(Guid id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (CribbageEntities dc = new CribbageEntities(options))
                {
                    IDbContextTransaction dbTransaction = null;
                    if (rollback) dbTransaction = dc.Database.BeginTransaction();

                    T row = dc.Set<T>().FirstOrDefault(t => t.Id == id);

                    if (row != null)
                    {
                        dc.Set<T>().Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) dbTransaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row does not exist.");
                    }
                }
                return results;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
