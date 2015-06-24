using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MDS.Model;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace MDS.DataAccess
{
    public class MssqlDataAccess : IDataAccess
    {

        public MssqlDataAccess()
        {

        }

        public string getSelectSQL(Type type, int top = -1)
        {
            var propertys = type.GetProperties();
            List<string> fileds = new List<string>();
            foreach (var p in propertys)
            {
                fileds.Add(string.Format("[{0}]", p.Name));
            }
            if (top >= 0) return string.Format("SELECT TOP {2} {0} FROM [{1}] ", string.Join(",", fileds.ToArray()), type.Name, top);
            return string.Format(" SELECT {0} FROM [{1}] ", string.Join(",", fileds.ToArray()), type.Name);
        }

        public string getUpdateSQL(Type type)
        {
            var propertys = type.GetProperties();
            List<string> fileds = new List<string>();
            var idkey = ModelHelper.getIdentifyKey(type).FirstOrDefault();
            foreach (var p in propertys)
            {
                if (idkey.Name == p.Name) continue;
                fileds.Add(string.Format("[{0}]=@{0}", p.Name));
            }

            return string.Format(" UPDATE [{1}] SET {0}  ", string.Join(",", fileds.ToArray()), type.Name);
        }

        public string getUpdateFiledSQL(Type type, string filed)
        {
            return string.Format(" UPDATE [{1}] SET {0}  ", string.Format("[{0}]=@{0}", filed), type.Name);
        }

        public string getDeleteSQL(Type type)
        {
            return string.Format(" DELETE  FROM [{0}] ", type.Name);
        }

        public string getInsertSQL(Type type)
        {
            var propertys = type.GetProperties();
            List<string> fileds = new List<string>();
            List<string> values = new List<string>();
            var idkey = ModelHelper.getIdentifyKey(type).FirstOrDefault();
            foreach (var p in propertys)
            {
                if (idkey.Name == p.Name) continue;
                fileds.Add(string.Format("[{0}]", p.Name));
                values.Add(string.Format("@{0}", p.Name));
            }
            return string.Format("INSERT INTO [{0}]({1}) VALUES({2})", type.Name, string.Join(",", fileds.ToArray()), string.Join(",", values.ToArray()));
        }

        public string getWhereByModel(Type type)
        {
            var pkeys = ModelHelper.getPrimaryKeys(type);
            List<string> wheres = new List<string>();
            if (pkeys.Count() > 0)
            {
                foreach (var k in pkeys)
                {
                    wheres.Add(string.Format("[{0}]=@{0}", k.Name));
                }
                return string.Join(" AND ", wheres.ToArray());
            }

            var idkeys = ModelHelper.getIdentifyKey(type);
            if (idkeys.Count() > 0)
            {
                foreach (var k in idkeys)
                {
                    wheres.Add(string.Format("[{0}]=@{0}", k.Name));
                }
                return string.Join(" AND ", wheres.ToArray());
            }

            var p = type.GetProperties().FirstOrDefault();
            if (p != null)
            {
                return string.Format("[{0}]=@{0}", p.Name);
            }

            return string.Format("0=(-1)", p.Name);
        }

        public IDataParameter[] getParameters(Model.Model model)
        {
            var propertys = model.GetType().GetProperties();
            List<IDataParameter> fileds = new List<IDataParameter>();
            foreach (var p in propertys)
            {
                SqlParameter sp = new SqlParameter(p.Name, model.GetValue(p.Name));
                fileds.Add(sp);
            }
            return fileds.ToArray();
        }


        public bool Add(Model.Model model)
        {
            string sql = getInsertSQL(model.GetType());
            var parameters = getParameters(model);
            return ExcuteSQL(sql, parameters) > 0;
        }

        public bool Update(Model.Model model)
        {
            string sql = getUpdateSQL(model.GetType());
            string where = getWhereByModel(model.GetType());
            sql = string.Format("{0} Where {1}", sql, where);
            var parameters = getParameters(model);
            return ExcuteSQL(sql, parameters) > 0;
        }

        public bool Delete(Model.Model model)
        {
            string sql = getDeleteSQL(model.GetType());
            string where = getWhereByModel(model.GetType());
            sql = string.Format("{0} Where {1}", sql, where);
            var parameters = getParameters(model);
            return ExcuteSQL(sql, parameters) > 0;
        }

        public Model.Model GetModel(Type type, string where)
        {
            string sql = getSelectSQL(type, 1);
            sql = string.Format("{0} Where {1}", sql, where);
            var dt = Query(sql);
            var list = ModelHelper.getByDataTable(type, dt);
            return list.FirstOrDefault();
        }

        public IEnumerable<Model.Model> GetModelList(Type type, string where, string order, int top)
        {
            string sql = getSelectSQL(type, top);
            if (!string.IsNullOrWhiteSpace(where))
            {
                sql = string.Format("{0} Where {1} ", sql, where);
            }
            if (!string.IsNullOrWhiteSpace(order))
            {
                sql = string.Format("{0} Order By {1}", sql, order);
            }
            var dt = Query(sql);
            var list = ModelHelper.getByDataTable(type, dt);
            return list;
        }

        public string getOrderString(Type type)
        {
            var pkeys = ModelHelper.getPrimaryKeys(type);
            List<string> wheres = new List<string>();
            if (pkeys.Count() > 0)
            {
                foreach (var k in pkeys)
                {
                    wheres.Add(string.Format("[{0}]", k.Name));
                }
                return string.Join(" AND ", wheres.ToArray());
            }

            var idkeys = ModelHelper.getIdentifyKey(type);
            if (idkeys.Count() > 0)
            {
                foreach (var k in idkeys)
                {
                    wheres.Add(string.Format("[{0}]=@{0}", k.Name));
                }
                return string.Join(" AND ", wheres.ToArray());
            }

            var p = type.GetProperties().FirstOrDefault();
            if (p != null)
            {
                return string.Format("[{0}]=@{0}", p.Name);
            }

            return string.Format("0=(-1)", p.Name);
        }

        public PagedList<Model.Model> GetModelList(Type type, string where, string order, int page, int pagesize)
        {
            string sql = getSelectSQL(type);

            if (string.IsNullOrWhiteSpace(order))
            {
                order = getOrderString(type);
            }

            if (!string.IsNullOrWhiteSpace(where))
            {
                sql = string.Format("{0} Where {1} ", sql, where);
            }
            if (!string.IsNullOrWhiteSpace(order))
            {
                sql = string.Format("{0} Order By {1}", sql, order);
            }

            string countsql = CreateCountingSql(sql);
            object count = ExcuteSingle(sql);
            int recordcount = count == null ? 0 : (int)count;

            string pagesql = CreatePagingSql(recordcount, pagesize, page, sql, order);

            var dt = Query(sql);
            var list = ModelHelper.getByDataTable(type, dt);
            PagedList<Model.Model> PagedList = new PagedList<Model.Model>();
            PagedList.AddRange(list);
            PagedList.RecordCount = recordcount;
            PagedList.PageSize = pagesize;
            PagedList.PageIndex = page;
            return PagedList;
        }

        #region 分页处理方法
        /// <summary>
        /// 获取分页SQL语句，排序字段需要构成唯一记录
        /// </summary>
        /// <param name="_recordCount">记录总数</param>
        /// <param name="_pageSize">每页记录数</param>
        /// <param name="_pageIndex">当前页数</param>
        /// <param name="_safeSql">SQL查询语句</param>
        /// <param name="_orderField">排序字段，多个则用“,”隔开</param>
        /// <returns>分页SQL语句</returns>
        public string CreatePagingSql(int _recordCount, int _pageSize, int _pageIndex, string _safeSql, string _orderField)
        {
            //重新组合排序字段，防止有错误


            //计算总页数
            _pageSize = _pageSize <= 0 ? _recordCount : _pageSize;
            int pageCount = (_recordCount + _pageSize - 1) / _pageSize;

            //检查当前页数
            if (_pageIndex < 1)
            {
                _pageIndex = 1;
            }
            if (_pageIndex > pageCount)
            {
                _pageIndex = pageCount;
                _pageSize = 0;
            }
            int start = (_pageIndex - 1) * _pageSize;
            int end = 0;
            if ((_recordCount - start) >= _pageSize)
            {
                end = start + _pageSize;
            }
            else
            {
                end = _recordCount;
            }

            string sql = @"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER By {0}) AS rownumber,* FROM ({1}) T ) TT  WHERE rownumber BETWEEN {2} AND {3}";
            return string.Format(sql, _orderField, _safeSql, start, end);
        }

        /// <summary>
        /// 获取记录总数SQL语句
        /// </summary>
        /// <param name="_n">限定记录数</param>
        /// <param name="_safeSql">SQL查询语句</param>
        /// <returns>记录总数SQL语句</returns>
        public string CreateTopnSql(int _n, string _safeSql)
        {
            return string.Format(" SELECT TOP {0} * FROM ({1}) AS T ", _n, _safeSql);
        }

        /// <summary>
        /// 获取记录总数SQL语句
        /// </summary>
        /// <param name="_safeSql">SQL查询语句</param>
        /// <returns>记录总数SQL语句</returns>
        public string CreateCountingSql(string _safeSql)
        {
            return string.Format(" SELECT COUNT(1) AS RecordCount FROM ({0}) AS T ", _safeSql);
        }

        #endregion

        #region 数据库连接
        public static string _connectionString = null;
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    _connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                }
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        private void CheckConnection(SqlConnection conn)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }
        public int ExcuteSQL(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    CheckConnection(conn);
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public int ExcuteSQL(string sql, IDataParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    CheckConnection(conn);
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public object ExcuteSingle(string sql)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    CheckConnection(conn);
                    return cmd.ExecuteScalar();
                }
            }
        }

        public object ExcuteSingle(string sql, IDataParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    CheckConnection(conn);
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        public DataTable Query(string sql)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    CheckConnection(conn);
                    SqlDataAdapter command = new SqlDataAdapter(cmd);
                    command.Fill(ds, "ds");
                }
            }
            if (ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }
        public DataTable Query(string sql, IDataParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    CheckConnection(conn);
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter command = new SqlDataAdapter(cmd);
                    command.Fill(ds, "ds");
                }
            }
            if (ds.Tables.Count == 0) return null;
            return ds.Tables[0];
        }

        #endregion
    }
}
