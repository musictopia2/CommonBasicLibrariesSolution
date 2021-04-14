using System;
namespace CommonBasicLibraries.DatabaseHelpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        public ForeignKeyAttribute(string className)
        {
            ClassName = className;
        }
        public string ClassName { get; private set; }
    }
}