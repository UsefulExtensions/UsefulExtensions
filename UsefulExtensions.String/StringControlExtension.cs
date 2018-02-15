using System.Globalization;

namespace UsefulExtensions.String
{
    public static class StringControlExtension
    {
        public static bool IsNumeric(this string value)
        {
            long retNum;
            return long.TryParse(value, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out retNum);
        }
    }
}