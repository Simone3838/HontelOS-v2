/*
* PROJECT:          HontelOS
* CONTENT:          Cosmos extensions
* PROGRAMMERS:      Jort van Dalen
* 
* EXTENSIONS:       PCI
*/

using Cosmos.HAL;

namespace HontelOS.System
{
    public static class PCIExt
    {
        public static PCIDevice GetDeviceClass(byte classCode, byte subClassCode)
        {
            foreach (var device in PCI.Devices)
                if (device.ClassCode == classCode && device.Subclass == subClassCode)
                    return device;
            return null;
        }
    }
}
