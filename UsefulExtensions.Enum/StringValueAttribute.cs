using System;

namespace UsefulExtensions.Enum
{
    public sealed class StringValueAttribute : Attribute
    {
        public string Value { get; private set; }

        public StringValueAttribute(string value)
        {
            this.Value = value;
        }
    }
}