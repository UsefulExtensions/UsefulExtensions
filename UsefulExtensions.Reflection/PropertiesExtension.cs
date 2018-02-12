using System.Collections.Generic;
using System.Reflection;

namespace UsefulExtensions.Reflection
{
    public static class PropertiesExtension
    {
        public static List<string> GetProperties(this object obj)
        {
            var propertyList = new List<string>();
            if (obj != null)
            {
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    propertyList.Add(prop.Name);
                }
            }

            return propertyList;
        }
    }
}
