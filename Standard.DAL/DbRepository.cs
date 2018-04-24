using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.Entity;
using Standard.Model;
using System.Linq.Dynamic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Standard.DAL
{
    public class DbRepository : Standard.IDAL.IRepositoryBase
    {

        NLog.ILogger logger = NLog.LogManager.GetLogger("DAL-Exception");
        RoadFlowWebFormEntities db;
        public DbRepository(DbContext _db)
        {
            db = new RoadFlowWebFormEntities();
            //db.Configuration.LazyLoadingEnabled = true;
        }
        // UPSOFT_PMSEntities db = new UPSOFT_PMSEntities();
        //[Inject]
        //DbContext db { get; set; }

        //private static IKernel kernel = new StandardKernel(new BLLModule());
        //DbContext db = kernel.Get<DbContext>();


        public T Add<T>(T entity) where T : class
        {
            try
            {
                var o = db.Set<T>().Add(entity);
                return db.SaveChanges() > 0 ? o : null;

            }
            catch (DbEntityValidationException exx)
            {
                // var aa=  exx.EntityValidationErrors.ElementAt(0);
                logger.Error(exx.EntityValidationErrors.ElementAt(0).ValidationErrors.ToList()[0].ErrorMessage);
                throw exx;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }

        }


        public bool BatchOperation(List<SaveEntityModel> dataList)
        {
            using (var transaction = new TransactionScope())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                try
                {
                    foreach (SaveEntityModel item in dataList)
                    {
                        var entry = db.Entry(item.Entity);
                        if (item.State == EntityState.Modified && !string.IsNullOrEmpty(item.UpdateFields))
                        {
                            item.State = EntityState.Unchanged;
                            item.UpdateFields.Split(',').ToList().ForEach(f =>
                            {
                                entry.Property(f).IsModified = true;
                            });
                        }
                        entry.State = item.State;
                    }
                    db.SaveChanges();
                    transaction.Complete();
                    return true;
                }
                catch (DbEntityValidationException exx)
                {
                    var aa = exx.EntityValidationErrors.ElementAt(0);
                    if (aa != null && aa.ValidationErrors != null && aa.ValidationErrors.Any())
                    {
                        var errorMessage = string.Join(";", aa.ValidationErrors.ToList().Select(a => a.ErrorMessage).ToArray());
                        throw new Exception(errorMessage);
                    }
                    else
                    {
                        throw exx;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    db.Configuration.AutoDetectChangesEnabled = true;
                    //transaction.Dispose();
                }
            }
        }



        public int Count<T>(Func<T, bool> predicate) where T : class
        {
            return db.Set<T>().Count(predicate);
        }


        public bool Update<T>(T entity) where T : class
        {

            try
            {
                db.Entry<T>(entity).State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            catch (DbEntityValidationException exx)
            {
                // var aa=  exx.EntityValidationErrors.ElementAt(0);
                logger.Error(exx.EntityValidationErrors.ElementAt(0).ValidationErrors.ToList()[0].ErrorMessage);
                throw exx;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public bool UpdateOrAdd<T>(T entity) where T : class
        {
            try
            {
                db.Entry(entity).State = db.Entry(entity).State == EntityState.Detached ? EntityState.Added : EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null && ex.InnerException.InnerException is System.Data.SqlClient.SqlException && ex.InnerException.InnerException.HResult == -2146232060)
                {
                    db.Entry(entity).State = EntityState.Modified;
                    return db.SaveChanges() > 0;
                }
                throw ex;
            }

        }

        public bool Update<T>(T entity, params string[] properties) where T : class
        {
            try
            {
                var entry = db.Entry<T>(entity);
                entry.State = EntityState.Unchanged;
                if (properties != null)
                {
                    properties.ToList().ForEach(p =>
                    {
                        entry.Property(p).IsModified = true;
                    });
                }
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Delete<T>(T entity) where T : class
        {
            db.Entry<T>(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;
        }

        public bool Delete<T>(object id) where T : class
        {
            db.Set<T>().Remove(db.Set<T>().Find(id));
            return db.SaveChanges() > 0;
        }

        public bool Exist<T>(Func<T, bool> anyLambda) where T : class
        {
            return db.Set<T>().Any(anyLambda);
        }

        public T Find<T>(object key) where T : class
        {
            return db.Set<T>().Find(key);
        }

        public object Find(Type type, object key)
        {
            return db.Set(type).Find(key);
        }


        public T Find<T>(string sql, bool isWhere = false, params object[] parameters) where T : class
        {
            return isWhere ? db.Set<T>().Where(sql, parameters).SingleOrDefault<T>() : db.Database.SqlQuery<T>(sql, parameters).SingleOrDefault<T>();
        }


        public T Find<T>(Func<T, bool> whereLambda) where T : class
        {
            //return db.Set<T>().First(whereLambda);
            var objlist = db.Set<T>().Where(whereLambda);
            if (objlist != null && objlist.Count() > 0)
            {
                return objlist.First();
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<T> FindAllList<T>() where T : class
        {
            return db.Set<T>().ToList();
        }


        public IEnumerable<T> FindList<T>(Func<T, bool> whereLambda) where T : class
        {
            return db.Set<T>().Where(whereLambda);
        }

        // [Obsolete("此方法不建议用于分页")]
        public IEnumerable<T> FindList<T>(string sql, bool isWhere = false, params object[] parameters) where T : class
        {
            IEnumerable<T> list = null;
            try
            {
                list = isWhere ? db.Set<T>().Where(sql, parameters).AsEnumerable() : db.Database.SqlQuery<T>(sql, parameters).ToList();
            }
            catch (Exception ex)
            {

                throw;
            }

            return list ?? new List<T>();
        }

        public IEnumerable<T> FindListWithOrdered<T>(Func<T, bool> whereLambda, Func<T, object> orderLambda, bool isAsc = true) where T : class
        {
            return isAsc ? db.Set<T>().Where(whereLambda).OrderBy(orderLambda) : db.Set<T>().Where(whereLambda).OrderByDescending(orderLambda);
        }

        public IEnumerable<T> FindPageList<T>(int pageIndex, int pageSize, out int totalRecord, Func<T, bool> whereLambda = null,
            Func<T, object> orderLambda = null, bool isAsc = true) where T : class
        {
            var list = FindList<T>(whereLambda);
            totalRecord = list.Count();
            if (whereLambda == null) return list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            if (orderLambda == null) return list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return isAsc ? list.OrderBy(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize) :
                list.OrderByDescending(orderLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        //public IEnumerable<T> FindPageList<T>(string sql, int pageIndex, int pageSize, out int totalRecord, params object[] parameters) where T : class
        //{
        //    var list = FindList<T>(sql, parameters: parameters);
        //    totalRecord = list.Count();
        //    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //}

      

       

        public IEnumerable<T> FindPageList<T>(string sql, int pageIndex, int pageSize, out int totalRecord, params object[] parameters) where T : class
        {
            var list = FindList<T>(sql, parameters: parameters);
            totalRecord = list.Count();
            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        //public IQueryable<T> FindAllQueryable<T>() where T : class
        //{
        //    return db.Set<T>();
        //}

        //public IQueryable<T> FindQueryable<T>(string sql, bool isWhere = false, params object[] parameters) where T : class
        //{
        //    //count = Count("select count(1) Count from (" + sql + ") sqlquery");
        //    if (isWhere)
        //    {
        //        return db.Set<T>().Where(sql, parameters);
        //    }
        //    else
        //    {
        //        return db.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        //    }
        //}

        //public IEnumerable<T> FindQueryableWithProcedure<T>(string sql, params object[] parameters) where T : class
        //{
        //    return db.Database.SqlQuery<T>(sql, parameters).ToList();
        //}

        #region ADO.Net

        public object GetObjectBySql(string sql)
        {
            return SQLHelper.ExecuteScalar(db.Database.Connection.ConnectionString, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 根据SQL返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetDataBySQL(string sql)
        {
            return SQLHelper.ExecuteDataTable(db.Database.Connection.ConnectionString, CommandType.Text, sql, null);
        }

        public object GetObjectBySql(string connectionString, string sql)
        {
            return SQLHelper.ExecuteScalar(connectionString, CommandType.Text, sql, null);
        }

        /// <summary>
        /// 根据SQL返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSetBySQL(string connectionString, string sql)
        {
            return SQLHelper.ExecuteDataSet(connectionString, CommandType.Text, sql, null);
        }
        public DataSet GetDataSetByProc(string connectionString, string sql, SqlParameter[] cmdParas)
        {
            return SQLHelper.ExecuteDataSet(connectionString, CommandType.StoredProcedure, sql, cmdParas);
        }

        /// <summary>
        /// 根据SQL返回条数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int CheckExistBySQL(string sql)
        {
            return SQLHelper.ExecuteScalar(db.Database.Connection.ConnectionString, CommandType.Text, sql, null);
        }
        /// <summary>
        /// 根据SQL返回条数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQueryBySQL(string sql)
        {
            return SQLHelper.ExecuteNonQuery(db.Database.Connection.ConnectionString, CommandType.Text, sql, null);
        }

        public int ExecuteNonQueryBySQL(string connectionString, string sql)
        {
            return SQLHelper.ExecuteNonQuery(connectionString, CommandType.Text, sql, null);
        }

        #endregion
        #region 通用访问类型DbHelper
        public int ExecuteDBHelperQueryBySQL(string strConnect, string dataType, string sql, params IDataParameter[] iParms)
        {

            DBHelper dbHelper = new DBHelper(strConnect, dataType);
            return dbHelper.ExecuteSql(sql, iParms);
        }
        //执行事务
        public void ExecuteSqlTran(Dictionary<String, IDataParameter[]> pList, string strConnect, string dataType)
        {
            DBHelper dbHelper = new DBHelper(strConnect, dataType);
            dbHelper.ExecuteSqlTran(pList, strConnect, dataType);

        }
        #endregion

    }
}
