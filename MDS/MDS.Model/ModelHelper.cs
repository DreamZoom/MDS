using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;

namespace MDS.Model
{
    public class ModelHelper
    {
        
        /// <summary>
        /// 创建设置属性值委托，构造switch函数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Func<object, string, object> CreateCaseGetDelagate(Type type)
        {
            var target = Expression.Parameter(typeof(object));
            var propertyName = Expression.Parameter(typeof(string));
            var propertys = type.GetProperties();
            List<SwitchCase> caselist = new List<SwitchCase>();

            var labeltarget = Expression.Label();
            foreach(var property in propertys){
                MethodInfo mi = property.GetSetMethod(true);
                if (mi.GetParameters().Length > 1) continue;

                var castTarget = Expression.Convert(target, property.DeclaringType);
                MemberExpression member = Expression.Property(castTarget, property.Name);
                Expression objectMember = Expression.Convert(member, typeof(object));

                caselist.Add(Expression.SwitchCase(objectMember, Expression.Constant(property.Name)));
            }
            var switchs = Expression.Switch(propertyName,Expression.Constant(null), caselist.ToArray());

            return Expression.Lambda<Func<object, string, object>>(switchs, target,propertyName).Compile();
        }


        public static Action<object, string, object> CreateCaseSetDelagate(Type type)
        {
            var target = Expression.Parameter(typeof(object));
            var propertyValue = Expression.Parameter(typeof(object));
            var propertyName = Expression.Parameter(typeof(string));

            var propertys = type.GetProperties();
            List<SwitchCase> caselist = new List<SwitchCase>();
            foreach (var property in propertys)
            {
                MethodInfo mi = property.GetSetMethod(true);
                if (mi.GetParameters().Length > 1) continue;
                var castTarget = Expression.Convert(target, property.DeclaringType);
                var castPropertyValue = Expression.Convert(propertyValue, property.PropertyType);

                caselist.Add(Expression.SwitchCase(Expression.Call(castTarget, property.GetSetMethod(), castPropertyValue), Expression.Constant(property.Name)));
            }
            var switchs = Expression.Switch(propertyName, caselist.ToArray());

            return Expression.Lambda<Action<object, string, object>>(switchs, target, propertyName, propertyValue).Compile();
        }


        public static IEnumerable<PropertyInfo> getPrimaryKeys(Type type)
        {
            var primarys = getPropertysByAttribute(type, typeof(Attributes.PrimaryKey));
            return primarys;
        }

        public static IEnumerable<PropertyInfo> getIdentifyKey(Type type)
        {
            var primarys = getPropertysByAttribute(type, typeof(Attributes.PrimaryKey));
            return primarys;
        }

        public static IEnumerable<PropertyInfo> getPropertysByAttribute(Type type,Type attributeType)
        {
            var propertys = type.GetProperties().Where(m => m.GetCustomAttributes(attributeType, false).Length > 0);
            return propertys;
        }
    }
}
