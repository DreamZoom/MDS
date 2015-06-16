using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MDS.Model;

namespace MDS.DataAccess
{
    public interface IDataAccess
    {
        string getSelectSQL(Type type, int top = -1);

        string getUpdateSQL(Type type);

        string getUpdateFiledSQL(Type type,string filed);

        string getDeleteSQL(Type type);

        string getInsertSQL(Type type);

        string getWhereByModel(Type type);

        IDataParameter[] getParameters(Model.Model model);

        bool Add(Model.Model model);

        bool Update(Model.Model model);

        bool Delete(Model.Model model);

        Model.Model GetModel(Type type, string where);

        IEnumerable<Model.Model> GetModelList(Type type, string where, string order, int top);

        PagedList<Model.Model> GetModelList(Type type, string where, string order, int page, int pagesize);


        int ExcuteSQL(string sql);

        int ExcuteSQL(string sql, IDataParameter[] parameters);

        object ExcuteSingle(string sql);

        object ExcuteSingle(string sql, IDataParameter[] parameters);

        DataTable Query(string sql);
        DataTable Query(string sql, IDataParameter[] parameters);

    }
}
