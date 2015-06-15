using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MDS.Model;
using System.Data.Sql;
using System.Data.SqlClient;

namespace MDS.DataAccess
{
    public class MssqlDataAccess : IDataAccess
    {
        public string getSelectSQL(Type type, int top = -1)
        {
            var propertys = type.GetProperties();
            List<string> fileds = new List<string>();
            foreach (var p in propertys)
            {
                fileds.Add(string.Format("[{0}]",p.Name));
            }
            if (top >= 0) return string.Format("SELECT TOP {2} {0} FROM [{1}] ", string.Join(",", fileds.ToArray()), type.Name,top);
            return string.Format(" SELECT {0} FROM [{1}] ",string.Join(",",fileds.ToArray()),type.Name);
        }

        public string getUpdateSQL(Type type)
        {
            var propertys = type.GetProperties();
            List<string> fileds = new List<string>();
            foreach (var p in propertys)
            {
                fileds.Add(string.Format("[{0}]=@{0}",p.Name));
            }
            
            return string.Format(" UPDATE {1} SET [{0}]  ", string.Join(",", fileds.ToArray()), type.Name);
        }

        public string getUpdateFiledSQL(Type type, string filed)
        {
            return string.Format(" UPDATE {1} SET [{0}]  ", string.Format("[{0}]=@{0}", filed), type.Name);
        }

        public string getDeleteSQL(Type type)
        {
            return string.Format(" DELETE  FORM [{0}] ", type.Name);
        }

        public string getInsertSQL(Type type)
        {
            var propertys = type.GetProperties();
            List<string> fileds = new List<string>();
            List<string> values = new List<string>();
            foreach (var p in propertys)
            {
                fileds.Add(string.Format("[{0}]", p.Name));
                values.Add(string.Format("@{0}", p.Name));
            }
            return string.Format("INSERT INTO [{0}]({1}) VALUES({2})", type.Name, string.Join(",", fileds.ToArray()), string.Join(",", values.ToArray()));
        }

        public IDataParameter[] getParameters(Model.Model model)
        {
            var propertys = model.GetType().GetProperties();
            List<IDataParameter> fileds = new List<IDataParameter>();
            foreach (var p in propertys)
            {
                SqlParameter sp = new SqlParameter(p.Name,model.GetValue(p.Name));
                fileds.Add(sp);
            }
            return fileds.ToArray();
        }

       
        bool Add(Model.Model model);

        bool Update(Model.Model model);

        bool Delete(Model.Model model);

        Model.Model GetModel(params object[] keys);

        List<Model.Model> GetModelList(string where, string order, string top);

        PagedList<Model.Model> GetModelList(string where, string order, int page, int pagesize);
    }
}
