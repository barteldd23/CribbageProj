namespace Cribbage.BL
{
    public abstract class GenericManager<T> where T : class, IEntity
    {
        protected DbContextOptions<CribbageEntities> options;

        public GenericManager(DbContextOptions<CribbageEntities> options)
        {
            this.options = options;
        }

        public List<T> Load()
        {
            try
            {
                return new CribbageEntities(options)
                    .Set<T>()
                    .ToList<T>();
            }
            catch (Exception e)
            {
                throw e;
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
