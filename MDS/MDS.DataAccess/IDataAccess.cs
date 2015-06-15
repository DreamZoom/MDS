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

        IDataParameter[] getParameters(Model.Model model);

        bool Add(Model.Model model);

        bool Update(Model.Model model);

        bool Delete(Model.Model model);

        Model.Model GetModel(params object[] keys);

        List<Model.Model> GetModelList(string where,string order,string top);

        PagedList<Model.Model> GetModelList(string where, string order, int page,int pagesize);

    }
}
