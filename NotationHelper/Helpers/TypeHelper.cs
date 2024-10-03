using NotationHelper.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NotationHelper.Helpers
{
    public static class TypeHelper
    {
        public static List<T> PassOrFill<T>(ref List<T> privateValues, Type staticType) where T: class
        {
            if(privateValues != null)
                return privateValues;
            privateValues = GetStaticProps<T>(staticType);
            privateValues.AddRange(GetStaticFields<T>(staticType));
            return privateValues;
        }
        public static List<TProp> GetStaticProps<TProp>(Type staticType) where TProp : class
        {
            var propValues = staticType
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(prop => prop.PropertyType.IsAssignableTo(typeof(TProp)))
                .Select(prop => prop.GetValue(null))
                .Where(v => v is TProp)
                .Select(v => v as TProp).ToList();

            return propValues;
        }

        public static List<TProp> GetStaticFields<TProp>(Type staticType) where TProp : class
        {
            var propValues = staticType
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(prop => prop.FieldType.IsAssignableTo(typeof(TProp)))
                .Select(prop => prop.GetValue(null))
                .Where(v => v is TProp)
                .Select(v => v as TProp).ToList();

            return propValues;
        }
    }
}
