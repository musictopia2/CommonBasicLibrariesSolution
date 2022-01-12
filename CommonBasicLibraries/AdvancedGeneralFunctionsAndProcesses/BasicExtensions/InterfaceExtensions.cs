namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
//eventually can do without reflection but not now.
public static class InterfaceExtensions
{
    public static T AutoMap<T>(this IMappable payLoad)
        where T: new()
    {
        var maps = MapHelpers<T>.Maps;
        Type t = payLoad.GetType();
        foreach (var item in maps)
        {
            if (item.Key == t)
            {
                return item.Value.Invoke(payLoad);
            }
        }
        return payLoad.OldAutoMap<T>();
    }
    private static T OldAutoMap<T>(this IMappable payLoad)
        where T : new()
    {
        //typeof(IMyInterface).IsAssignableFrom(typeof(MyType));
        //from this link
        //https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
        Type payType = payLoad.GetType(); //because if it gets to this, must do old fashioned reflection because you did not register for mappings via source generators.
        //Type originals = typeof(IViewModelBase);
        if (payLoad is IViewModelBase)
        {
            //eventually try to use source generators.
            BasicList<PropertyInfo> oldProperties = payType.GetMappableProperties();
            Type newType = typeof(T);
            BasicList<PropertyInfo> newProperties = newType.GetMappableProperties(); //i think if new ones has some not mappable, will ignore.
            T output = new();
            newProperties.ForEach(newP =>
            {
                PropertyInfo? old = oldProperties.SingleOrDefault(item => item.Name == newP.Name);
                if (old is not null)
                {
                    newP.SetValue(output, old.GetValue(payLoad)); //hopefully this simple.
                    }
            });
            return output;
        }
        else
        {
            string results = js.SerializeObject(payLoad);
            return js.DeserializeObject<T>(results);
        }
    }
}