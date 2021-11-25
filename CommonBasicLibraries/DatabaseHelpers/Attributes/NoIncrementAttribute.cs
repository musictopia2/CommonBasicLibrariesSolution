namespace CommonBasicLibraries.DatabaseHelpers.Attributes;
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class NoIncrementAttribute : Attribute { } //if this is marked, then ids will not be incremented. that is a better way to do it.  most of the time, will be incremented.