using System;
namespace CommonBasicLibraries.DatabaseHelpers.Attributes
{
    /// <summary>
    /// The purpose of this would be to map to an interface.
    /// useful for cases where we are doing the where clause.
    /// there are cases where the interface classes are putting in the conditions
    /// since the interface does not know the real mapping, then this would work.
    /// does not care about any specific database engine or way of storing data.
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class InterfaceAttribute : Attribute
    {
        public InterfaceAttribute(string interfaceName)
        {
            InterfaceName = interfaceName;
        }
        public string InterfaceName { get; set; }
    }
}