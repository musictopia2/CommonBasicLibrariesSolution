using System;
namespace CommonBasicLibraries.DatabaseHelpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcludeUpdateListenerAttribute : Attribute
    {
        //this will be used for fields you don't want to listen for updates.
        //this may have to be put in the common classes (well see).
        //trying to decide whether the change tracking will be here or the common libraries.  maybe here.
    }
}