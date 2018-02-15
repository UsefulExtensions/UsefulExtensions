using System.Linq;

namespace UsefulExtensions.Enum
{

    public static class StringValueExtension
    {
        public static string ToStringValue(this System.Enum value)
        {
            var attributes = (StringValueAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(StringValueAttribute), false);

            if (attributes.Any())
                return attributes[0].Value;

            return value.ToString();
        }

        public static T ToEnum<T>(this string value) where T : class 
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }
    }
}