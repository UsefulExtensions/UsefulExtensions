using System;

namespace UsefulExtensions.String
{
    public static class StringConversionExtension
    {
        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }
    }
}