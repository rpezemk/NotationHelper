using System.Reflection;

namespace AudioTool.CsEvents
{
    public abstract class ACsEvent
    {
        public int InstrumentNo;

        public void SetInstrNo(int instrumentNo) => InstrumentNo = instrumentNo;
        public double[] GetParams() 
        {
            var props = GetType().GetProperties();
            List<double> values = [InstrumentNo];
            foreach (var prop in props.AllowTypes(typeof(double), typeof(float), typeof(int)))
            {
                var value = (double)prop.GetValue(this);
                values.Add(value);
            }
            return values.ToArray();
        }
    }

    public static class ReflectionHelper
    {
        public static PropertyInfo[] AllowTypes(this PropertyInfo[] propInfo, params Type[] types)
        {
            var resList = new List<PropertyInfo>();
            foreach(var p in propInfo)
            {
                if(p.IsAny(types))
                    resList.Add(p);
            }
            
            return resList.ToArray();
        }

        public static bool IsAny(this PropertyInfo propInfo, params Type[] types)
        {
            var res = types.Any(t => propInfo.PropertyType == t);
            return res;
        }
    }
}
