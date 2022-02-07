namespace CommonBasicLibraries.BasicDataSettingsAndProcesses;
//this is needed so if its mappable but can't just serialize/deserialize because it has constructors, can still do mappings.
//would do shallow copies of complex objects within these.
public interface IViewModelBase { }