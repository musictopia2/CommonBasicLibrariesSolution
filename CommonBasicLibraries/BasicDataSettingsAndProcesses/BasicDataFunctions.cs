using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBasicLibraries.BasicDataSettingsAndProcesses
{
    public static class BasicDataFunctions
    {
        public enum EnumOS
        {
            None, WindowsDT, WindowsRT, Android, Linux, Macintosh, Wasm //this means can even account for webassembly.
        }
        public static EnumOS OS { get; set; } = EnumOS.WindowsDT;

        //looks like i need my random stuff now.
        //may need to rethink the random stuff now.
        //this may be needed afterall.
        public delegate Task ActionAsync<in T>(T obj); //this is used so if there is a looping, then it can await it if needed.
        public delegate void UpdateFunct<T>(T thisObj, int expectedItem); //the purpose of this is so i can update.  for this to work, the list has to determine the proper value

        public static bool AutoUseMainContainer { get; set; } = true;

        //for now, will not do anything for sql server.
        //will rethink later.  actually i like the idea of doing extensions for this instead.
        //that way i don't have to resolve.


        //public static string GetSQLServerConnectionString(string databaseOrPath) //this is intended for sql server.  this means can have several implementations of this.
        //{
        //    ISQLServer sqls = cons!.Resolve<ISQLServer>();
        //    return sqls.GetConnectionString(databaseOrPath);
        //}

    }
}
