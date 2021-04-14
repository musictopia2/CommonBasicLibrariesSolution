using System;
namespace CommonBasicLibraries.DatabaseHelpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryJoinedDataAttribute : Attribute
    {
        //this is used for cases like  artist or album where we only want some other pieces of information.
    }
}