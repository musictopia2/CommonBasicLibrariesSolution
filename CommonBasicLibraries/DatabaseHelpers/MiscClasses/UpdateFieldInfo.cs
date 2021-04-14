using CommonBasicLibraries.DatabaseHelpers.MiscInterfaces;
namespace CommonBasicLibraries.DatabaseHelpers.MiscClasses
{
    public class UpdateFieldInfo : IProperty
    {
        public string Property { get; set; } = "";
        public UpdateFieldInfo() { }
        public UpdateFieldInfo(string property)
        {
            Property = property;
        }
    }
    public class UpdateEntity : UpdateFieldInfo
    {
        public object Value { get; set; } //this time i need the value.  no way around it.  has to update one a time when its doing it this way.
        public UpdateEntity(string property, object value)
        {
            Property = property;
            Value = value;
        }
    }
}