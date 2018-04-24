using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Standard.Model;

namespace Standard.IDAL
{
    public interface IRepositoryBase
    {

        //System.Data.Entity.DbContext db { get; set; }

        //dynamic ComplicateQuery(string sql, System.Data.Common.DbParameter[] parameters);


        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        T Add<T>(T entity) where T : class;


        /// <summary>
        /// 批量更新操作
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        bool BatchOperation(List<SaveEntityModel> dataList);

        /// <summary>
        /// 批量更新操作
        /// </summary>
        /// <param name="saveEntityModelSettings"></param>
        /// <returns></returns>
        // bool BatchOperation(params Action<SaveEntityModel>[] saveEntityModelSettings);

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="predicate">条件表达式</param>
        /// <returns>记录数</returns>
        int Count<T>(Func<T, bool> predicate) where T : class;

        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        //  int Count(string sql, params object[] parameters);

        /// <summary>
        ///  更新或新增，根据查询主键，若存在则为更新，否则为新增
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="primaryKey">主键名称</param>
        /// <returns></returns>
      //  bool UpdateOrAdd<T>(T entity, string primaryKey) where T : class;

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        bool Update<T>(T entity) where T : class;

        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        bool Update<T>(T entity, params string[] properties) where T : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        bool Delete<T>(T entity) where T : class;

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete<T>(object id) where T : class;

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="anyLambda">查询表达式</param>
        /// <returns>布尔值</returns>
        bool Exist<T>(Func<T, bool> anyLambda) where T : class;

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns>实体</returns>
        T Find<T>(Func<T, bool> whereLambda) where T : class;

        /// <summary>
        /// 根据主键查询实体
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        T Find<T>(object key) where T : class;
        /// <summary>
        /// 根据主键查询实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        object Find(Type type, object key);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns>实体</returns>
        T Find<T>(string sql, bool isWhere = false, params object[] parameters) where T : class;


        /// <summary>
        /// 查找所有数据列表
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <returns></returns>
        IEnumerable<T> FindAllList<T>() where T : class;

        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns></returns>
        IEnumerable<T> FindList<T>(Func<T, bool> whereLambda) where T : class;

        IEnumerable<T> FindList<T>(string sql, bool isWhere = false, params object[] parameters) where T : class;
       // IQueryable<T> FindQueryable<T>(string sql, bool isWhere = false, params object[] parameters) where T : class;

        //IEnumerable<T> FindQueryableWithProcedure<T>(string sql, params object[] parameters) where T : class;
        //IEnumerable<T> FindList<T>(string whereString, params object[] parameters) where T : class;

        /// <summary>
        /// 查找数据并排序
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <typeparam name="S">排序字段</typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
       // IEnumerable<T> FindListWithOrdered<T>(Func<T, bool> whereLambda, Func<T, object> orderLambda, bool isAsc = true) where T : class;

        /// <summary>
        /// 查找分页数据列表
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <typeparam name="S">排序</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="whereLambda">查询表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        IEnumerable<T> FindPageList<T>(int pageIndex, int pageSize, out int totalRecord, Func<T, bool> whereLambda = null, Func<T, object> orderLambda = null, bool isAsc = true) where T : class;

        IEnumerable<T> FindPageList<T>(string sql, int pageIndex, int pageSize, out int totalRecord, params object[] parameters) where T : class;

        /// <summary>
        /// 根据SQL返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataTable GetDataBySQL(string sql);

        object GetObjectBySql(string sql);

        object GetObjectBySql(string connectionString, string sql);

        /// <summary>
        /// 根据SQL返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataSet GetDataSetBySQL(string connectionString, string sql);
        DataSet GetDataSetByProc(string connectionString, string sql, SqlParameter[] cmdParas);
        /// <summary>
        /// 根据SQL返回条数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int CheckExistBySQL(string sql);
        int ExecuteNonQueryBySQL(string sql);
        int ExecuteNonQueryBySQL(string connectionString, string sql);

        #region dbHelper通用方法

        /// <summary>
        /// 根据SQL返回条数（DbHelper）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteDBHelperQueryBySQL(string strConnect, string dataType, string sql, params IDataParameter[] iParms);
        /// <summary>
        /// SQL事务
        /// </summary>
        /// <param name="SQLStringList"></param>
        /// <returns></returns>
        void ExecuteSqlTran(Dictionary<string, IDataParameter[]> SQLStringList, string strConnect, string dataType);

        #endregion



    }

}
