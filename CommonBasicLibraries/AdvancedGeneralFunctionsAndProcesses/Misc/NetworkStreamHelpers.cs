using System.Net.Sockets;
using System.Text; //not common enough to put under globals
namespace CommonBasicLibraries.AdvancedGeneralFunctionsAndProcesses.Misc
{
    public class NetworkStreamHelpers
    {
        public static byte[] ReadStream(NetworkStream ns)
        {
            byte[] data_buff;
            int b;
            string buff_length = "";
            while ((b = ns.ReadByte()) != 4)
            {
                buff_length += (char)b;
            }
            int data_length = Convert.ToInt32(buff_length);
            data_buff = new byte[data_length];
            int byte_read;
            int byte_offset = 0;
            while (byte_offset < data_length)
            {
                byte_read = ns.Read(data_buff, byte_offset, data_length - byte_offset);
                byte_offset += byte_read;
            }
            return data_buff;
        }
        public static byte[] CreateDataPacket(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            return CreateDataPacket(data);
        }
        public static byte[] CreateDataPacket(byte[] data)
        {
            byte[] initialize = new byte[1];
            initialize[0] = 2;
            byte[] separator = new byte[1];
            separator[0] = 4;
            byte[] datalength = Encoding.UTF8.GetBytes(Convert.ToString(data.Length));
            MemoryStream ms = new();
            ms.Write(initialize, 0, initialize.Length);
            ms.Write(datalength, 0, datalength.Length);
            ms.Write(separator, 0, separator.Length);
            ms.Write(data, 0, data.Length);
            byte[] ThisItem = ms.ToArray();
            ms.Close();
            ms.Flush();
            ms.Dispose();
            return ThisItem;
        }
    }
}