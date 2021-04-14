namespace CommonBasicLibraries.DatabaseHelpers.Attributes
{
    using System;
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        public ColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ColumnName { get; private set; }

    }
}