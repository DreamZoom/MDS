using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDS.Model;

namespace MDS.Service
{
    public class Service : IService
    {
        /// <summary>
        /// 绑定的模型类型
        /// </summary>
        public Type ModelType { get; set; }

        public DataAccess.MssqlDataAccess DataAccess { get; set; }

        public Service(Type modeltype)
        {
            this.ModelType = modeltype;
            DataAccess = new DataAccess.MssqlDataAccess();
        }

        public bool Add(Model.Model model)
        {
           return DataAccess.Add(model);
        }

        public bool Update(Model.Model model)
        {
            return DataAccess.Update(model);
        }

        public bool Delete(Model.Model model)
        {
            return DataAccess.Delete(model);
        }

        public Model.Model GetModel(string where)
        {
            return DataAccess.GetModel(ModelType, where);
        }

        public IEnumerable<Model.Model> GetModelList(string where, string order, int top=-1)
        {
            return DataAccess.GetModelList(ModelType, where,order,top);
        }

        public PagedList<Model.Model> GetModelList(Type type, string where, string order, int page, int pagesize)
        {
            return DataAccess.GetModelList(ModelType, where, order, page, pagesize);
        }
    }
}
