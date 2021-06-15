using CommonBasicLibraries.BasicDataSettingsAndProcesses;
using CommonBasicLibraries.CollectionClasses;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions
{
    //still needed.  was used for the meal planner to automap models.
    //can almost automap lists.
    public static class InterfaceExtensions
    {
        public static T AutoMap<T>(this IMappable payLoad)
            where T : new()
        {
            //typeof(IMyInterface).IsAssignableFrom(typeof(MyType));
            //from this link
            //https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
            Type payType = payLoad.GetType();
            Type originals = typeof(IViewModelBase);
            if (originals.IsAssignableFrom(payType))
            {
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
                string results = JsonConvert.SerializeObject(payLoad);
                return JsonConvert.DeserializeObject<T>(results)!;

            }
        }
    }
}