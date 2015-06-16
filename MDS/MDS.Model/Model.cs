using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using System.Linq.Expressions;

namespace MDS.Model
{
    /// <summary>
    /// 模型类
    /// </summary>
    public class Model : IModel,ICloneable
    {
     
        protected static Func<object, string, object> funcGetValue { get; set; }
        protected static Action<object, string, object> funcSetValue { get; set; }
        public Model()
        {
            InitDelegates();
            
        }

        /// <summary>
        /// 初始化委托
        /// </summary>
        public void InitDelegates()
        {
            if (funcGetValue != null) return;
            funcGetValue = ModelHelper.CreateCaseGetDelagate(this.GetType());
            funcSetValue = ModelHelper.CreateCaseSetDelagate(this.GetType());
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public virtual void SetValue(string propertyName, object value)
        {
            funcSetValue(this, propertyName, value);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual object GetValue(string propertyName)
        {
            return funcGetValue(this, propertyName);
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
