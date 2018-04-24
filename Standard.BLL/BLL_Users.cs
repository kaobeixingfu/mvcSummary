using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Standard.DAL;
using Standard.IBLL.FunctoinIBLL;
using Standard.Model;

namespace Standard.BLL
{
    public class BLL_Users : BusinessBase, IUsers
    {
        public UsersViewModel GetDataSource()
        {
            string sql = @"SELECT Name,Account from Users where id='23C3E9FC-6D8A-4EA0-925A-0A0671D61378'";
            IEnumerable<Users> data = dbRepository.FindAllList<Users>();
            UsersViewModel dd = dbRepository.Find<UsersViewModel>(sql);
            IEnumerable<UsersViewModel> ddd = dbRepository.FindList<UsersViewModel>("SELECT * from Users");
            return dbRepository.Find<UsersViewModel>(sql);
        }

        #region 调用存储过程的sql语句
        public void Insert(Users obj)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@paperName", SqlDbType.NVarChar,50),
					new SqlParameter("@paperType", SqlDbType.NVarChar,30),
				
                    new SqlParameter("@deptID",SqlDbType.SmallInt,2)};
            parameters[0].Value = obj.ID;
            parameters[1].Value = obj.Name;
            parameters[2].Value = obj.Password;
            DBHelperProcedure.Insert("UP_T_Paper_ADD", parameters);
        }

        public void Update(Users obj)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("paperID",SqlDbType.Int,4),
					new SqlParameter("@paperName", SqlDbType.NVarChar,50),
					new SqlParameter("@paperType", SqlDbType.NVarChar,30),
					new SqlParameter("@content", SqlDbType.Text),
					new SqlParameter("@answer", SqlDbType.Text),
					new SqlParameter("@creator", SqlDbType.NVarChar,10)};
            parameters[0].Value = obj.ID;
            parameters[1].Value = obj.Name;
            parameters[2].Value = obj.Password;

            DBHelperProcedure.Update("UP_T_Paper_Update", parameters);
        }

        public void Delete(string paperID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@paperID", SqlDbType.Int,4)};
            parameters[0].Value = paperID;

            DBHelperProcedure.Delete("UP_T_Paper_Delete", parameters);
        }

        public Users SelectByID(string paperID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@paperID", SqlDbType.Int,4)};
            parameters[0].Value = paperID;

            Users paper = null;
            using (SqlDataReader dr = DBHelperProcedure.Select("UP_T_Paper_GetModel", parameters))
            {
                if (dr.Read())
                {
                    paper = new Users();
                    paper.Name = dr.GetString(dr.GetOrdinal("paperName"));

                }
            }
            return paper;
        }

        public List<Users> SelectList()
        {
            List<Users> paperList = new List<Users>();

            Users paper = null;
            using (SqlDataReader dr = DBHelperProcedure.Select("UP_Users_GetList", null))
            {
                while (dr.Read())
                {
                    paper = new Users();
                    paper.Name = dr.GetString(dr.GetOrdinal("name"));

                    paperList.Add(paper);
                }
            }
            return paperList;
        }

        public List<Users> SelectListByDeptID(int deptID)
        {
            SqlParameter[] parms ={
                new SqlParameter("@deptID",SqlDbType.Int)};
            parms[0].Value = deptID;

            List<Users> paperList = new List<Users>();
            Users paper = null;
            using (SqlDataReader dr = DBHelperProcedure.Select("UP_T_Paper_GetListByDeptID", parms))
            {
                while (dr.Read())
                {
                    paper = new Users();
                    paper.Name = dr.GetString(dr.GetOrdinal("paperName"));

                    paperList.Add(paper);
                }
            }
            return paperList;
        }

        #endregion

        #region 普通sql语句调用存储过程


        public void Insert1(Users obj)
        {
            string sql = "insert into T_PaperByManualSelection(paperName,deptID,paperType,creator,createdTime) values(@paperName,@deptID,@paperType,@creator,@createdTime); select @@identity";
            SqlParameter[] parms ={
                                     new SqlParameter("@paperName",obj.Name),
                                      new SqlParameter("@deptID",obj.Name),
                                     new SqlParameter("@paperType",obj.Name),
                                     new SqlParameter("@createdTime",obj.Name)
                                 };

            SqlConnection conn = new SqlConnection(DBHelperCommomSql.connStr);
            SqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction("TInsertPaperByManualSelection");
                using (SqlDataReader dr = DBHelperCommomSql.Select(trans, sql, parms))
                {
                    if (dr.Read())
                    {
                        int id = Convert.ToInt32(dr[0]);
                        dr.Close();
                        sql = "insert into T_PaperByManualSelection_Subject(paperID,subjectType,subjectID) values(@paperID,@subjectType,@subjectID)";

                        //1：填空题，2：判断题，3：单选题，4：多选题，5：简答题
                        //if (obj.FillBlankList != null)
                        //    foreach (SubjectOfFillBlank subject in obj.FillBlankList)
                        //    {
                        //        SqlParameter[] parms2 ={
                        //        new SqlParameter("@paperID",id),
                        //        new SqlParameter("@subjectType",1),
                        //        new SqlParameter("@subjectID",subject.Id)
                        //    };
                        //        DBHelperCommomSql.Insert(trans, sql, parms2);
                        //    }


                    }
                }

                trans.Commit();
            }
            catch
            {
                trans.Rollback();
            }
            finally
            {
                trans.Dispose();
                conn.Close();
            }
        }

        public void Update1(Users obj)
        {
            throw new NotImplementedException();
        }

        public void Delete1(string id)
        {
            string sql = "delete from T_PaperByManualSelection where paperID=@paperID";
            SqlParameter[] parms ={
                                     new SqlParameter("@paperID",id)};
            DBHelperCommomSql.Delete(sql, parms);
        }

        public Users SelectByID1(string id)
        {
            Users paper = new Users();

            string sql = "select * from T_PaperByManualSelection where paperID=@paperID";
            SqlParameter[] parms ={
                                     new SqlParameter("@paperID",id)};
            using (SqlDataReader dr = DBHelperCommomSql.Select(sql, parms))
            {
                if (dr.Read())
                {

                    paper.Name = dr["paperName"].ToString();

                    dr.Close();
                }

                //填空题
                sql = "select t1.* from T_SubjectOfFillBlank t1 inner join T_PaperByManualSelection_Subject t2 on t1.id=t2.subjectID where  t2.paperID=@paperID and t2.subjectType=1";
                using (SqlDataReader dr2 = DBHelperCommomSql.Select(sql, parms))
                {
                    List<Users> list = new List<Users>();
                    while (dr2.Read())
                    {
                        Users subject = new Users();

                        subject.Name = dr2["question"].ToString();


                        list.Add(subject);
                    }

                }








            }

            return paper;
        }


        #endregion

        #region 存储过程返回参数
        public void InsertProc()
        {
            SqlParameter[] parameters = {
					new SqlParameter("@deptID", SqlDbType.NVarChar,50),
                    new SqlParameter("@TestId", SqlDbType.Int,3),
					new SqlParameter("@deptName", SqlDbType.NVarChar,50),
                    new SqlParameter("@reutrnValue",SqlDbType.Int,4)};

            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Value = "李四";
            parameters[3].Direction = ParameterDirection.ReturnValue;
            DBHelperProcedure.Insert("UP_User_Name", parameters);
            int s = Convert.ToInt32(parameters[3].Value);
            string ss = parameters[0].Value.ToString();
            // int retValue = Convert.ToInt32(parameters["@RetValue"].Value.ToString());
        }

        // CREATE PROCEDURE [dbo].[UP_User_Name]
        //@deptID nvarchar(50) output,
        //@deptName nvarchar(50)

        // AS 
        //    SET @deptID = '冉陶'
        //GO
        #endregion

        #region 事务
        //public void Insert(Test obj)
        //{
        //    //------------插入test
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@testID", SqlDbType.Int,4),
        //            new SqlParameter("@testName", SqlDbType.NVarChar,50),
        //            new SqlParameter("@paperID", SqlDbType.Int,4),
        //            new SqlParameter("@TotalScores", SqlDbType.Int,4),
        //            new SqlParameter("@neededMinutes", SqlDbType.TinyInt,1),
        //            new SqlParameter("@enableDate", SqlDbType.SmallDateTime),
        //            new SqlParameter("@unableDate", SqlDbType.SmallDateTime),
        //            new SqlParameter("@autoSaveInterval", SqlDbType.TinyInt,1),
        //            new SqlParameter("@creatorUserID", SqlDbType.VarChar,30),
        //            new SqlParameter("@creatorName", SqlDbType.NChar,10),
        //            new SqlParameter("@createdTime", SqlDbType.SmallDateTime),
        //            new SqlParameter("@paperType",SqlDbType.Int),
        //            new SqlParameter("@passScores",SqlDbType.Int,4)
        //            };
        //    parameters[0].Direction = ParameterDirection.Output;
        //    parameters[1].Value = obj.TestName;
        //    parameters[2].Value = obj.Paper.PaperID;
        //    parameters[3].Value = obj.TotalScores;
        //    parameters[4].Value = obj.NeededMinutes;
        //    parameters[5].Value = obj.EnableDate;
        //    parameters[6].Value = obj.UnabaleDate;
        //    parameters[7].Value = obj.AutoSaveInterval;
        //    parameters[8].Value = obj.Creator.UserID;
        //    parameters[9].Value = obj.Creator.Name;
        //    parameters[10].Value = obj.CreatedTime;
        //    parameters[11].Value = (int)obj.PaperType;
        //    parameters[12].Value = obj.PassScores;

        //    SqlConnection conn = new SqlConnection(DBHelper.connStr);
        //    SqlTransaction trans = null;

        //    try
        //    {
        //        conn.Open();
        //        trans = conn.BeginTransaction("insertTest");
        //        //------------插入test
        //        DBHelper.Insert(trans, "UP_T_Test_ADD", parameters);
        //        obj.TestID = Convert.ToInt32(parameters[0].Value);

        //        //-------------插入testRecorder
        //        foreach (Tester tester in obj.TesterList)
        //        {
        //            SqlParameter[] parameters2 = {
        //            new SqlParameter("@testID", SqlDbType.Int,4),
        //            new SqlParameter("@userID", SqlDbType.VarChar,30),
        //            new SqlParameter("@beginTestTime", SqlDbType.SmallDateTime),
        //            new SqlParameter("@submitTestTime", SqlDbType.SmallDateTime),
        //            new SqlParameter("@hasUsedMinutes", SqlDbType.SmallInt,2),
        //            new SqlParameter("@submitType", SqlDbType.NVarChar,10),
        //            new SqlParameter("@testerAnswer", SqlDbType.Text),
        //            new SqlParameter("@marked", SqlDbType.Bit,1),
        //            };

        //            parameters2[0].Value = obj.TestID;
        //            parameters2[1].Value = tester.UserID;
        //            parameters2[2].Value = null;
        //            parameters2[3].Value = null;
        //            parameters2[4].Value = 0;
        //            parameters2[5].Value = "未提交";
        //            parameters2[6].Value = string.Empty;
        //            parameters2[7].Value = false;

        //            DBHelper.Insert(trans, "UP_T_TestRecorder_ADD", parameters2);
        //        }

        //        trans.Commit();
        //    }
        //    catch
        //    {
        //        trans.Rollback();
        //    }
        //    finally
        //    {
        //        trans.Dispose();
        //        if (conn.State == ConnectionState.Open)
        //            conn.Close();
        //    }
        //}
        #endregion

        #region 存储过程返回值
        public int UpdatePwd(string userID, string oldPwd, string newPwd)
        {
            SqlParameter[] parms ={
                new SqlParameter("@reutrnValue",SqlDbType.Int,4),
                new SqlParameter("@userID",SqlDbType.VarChar,30),
                new SqlParameter("@oldPwd",SqlDbType.VarChar,30),
                new SqlParameter("@newPwd",SqlDbType.VarChar,30)
            };
            parms[0].Direction = ParameterDirection.ReturnValue;
            parms[1].Value = userID;
            parms[2].Value = oldPwd;
            parms[3].Value = newPwd;

            DBHelperProcedure.ExecuteNonQuery("UP_T_User_ModifyPwd", parms);

            return (int)parms[0].Value;
        }

        //        ALTER PROCEDURE [dbo].[UP_T_User_ModifyPwd] 
        //(
        //@userID varchar(30),
        //@oldPwd varchar(30),
        //@newPwd varchar(30)
        //)
        //AS
        //declare @count int
        //set @count =0

        //select @count = count(*)
        //from T_User
        //where userID = @userID and userPwd = @oldPwd

        //if @count = 0
        //return -1;
        //else
        //begin
        //    update T_User set
        //    userPwd = @newPwd
        //    where userID = @userID

        //    return 1;
        //end
        //GO
        #endregion

        #region 存储过程分页
            //        ALTER  PROCEDURE [dbo].[proc_Pagination]
            //    @sql NVARCHAR(MAX) ,
            //    @pageNumber INT = 0 ,
            //    @pageSize INT = 0 ,
            //    @where VARCHAR(2000) = '1=1' ,
            //    @order VARCHAR(200) = '1' ,
            //    @count INT OUT -- 输入出参数 
            //AS
            //    BEGIN
            //        IF ( @where IS NULL )
            //            SET @where = '1=1'
            //        IF ( @order IS NULL )
            //            SET @order = 'create_time DESC'
            //        DECLARE @rtSql NVARCHAR(MAX)
            //        IF ( @pageNumber <> 0 AND @pageSize <> 0 )
            //            BEGIN
            //                SET @rtSql = 'SELECT @count=COUNT(1) FROM (' + @sql
            //                    + ') a WHERE ' + @where
            //                EXEC sp_executesql @rtSql, N'@count int out', @count OUT
            //                SET @sql = 'SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY '
            //                    + @order + ') rownum ,* FROM (' + +@sql + +') a WHERE '
            //                    + @where + ') b WHERE rownum BETWEEN '
            //                    + CAST(( @pageNumber - 1 ) * @pageSize + 1 AS VARCHAR(20))
            //                    + ' AND ' + CAST(@pageNumber * @pageSize AS VARCHAR(20))
            //                EXEC (@sql)
            //            END
            //        ELSE
            //            IF ( @pageNumber = 0 AND @pageSize = 0 )
            //                BEGIN
            //                    SET @rtSql = 'SELECT @count=COUNT(1) FROM (' + @sql
            //                        + ') a WHERE ' + @where
            //                    EXEC sp_executesql @rtSql, N'@count int out', @count OUT
            //                    SET @sql = 'SELECT * FROM (' + @sql + +') a WHERE '+@where
            //                    EXEC (@sql)
            //                END
            //            ELSE
            //                BEGIN			
            //                    EXEC (@sql)
            //                    SET @count = @@rowcount
            //                END
            
            //    END


            //GO

        #endregion
    }
}
