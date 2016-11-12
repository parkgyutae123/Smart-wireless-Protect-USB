using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace ConnectHID_EX_
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagementObjectSearcher theSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE InterfaceType='USB'");

            foreach(ManagementObject currentObject in theSearcher.Get())
            {
                ManagementObject theSerialNumberObjectQuery = new ManagementObject("Win32_PhysicalMedia.Tag='" + currentObject["DeviceID"] + "'");
                Console.WriteLine(theSerialNumberObjectQuery["SerialNumber"].ToString());
            }
        }
    }
}
