/*
* PROJECT:          HontelOS
* CONTENT:          Audio driver extension
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.HAL.Drivers.Audio;
using System;
using Cosmos.HAL;

namespace HontelOS.Drivers.Audio
{
    public static class AudioDriverExt
    {
        public static AudioDriver GetAudioDriver()
        {
            Console.WriteLine("Detecting audio devices...");
            foreach (var pci in PCI.Devices)
            {
                if (pci == null) continue;

                // Intel HD Audio devices
                if (pci.VendorID == 0x8086 &&
                    (pci.DeviceID == 0x293E || pci.DeviceID == 0x293F || pci.DeviceID == 0x1C20))
                {
                    Console.WriteLine("Found Intel HD Audio device");
                    return IntelHDAudio.Initialize(4096);
                }
                // AC'97 Audio devices
                else if (pci.VendorID == 0x8086 &&
                         (pci.DeviceID == 0x2415 || pci.DeviceID == 0x2445))
                {
                    Console.WriteLine("Found AC'97 Audio device");
                    return AC97.Initialize(4096);
                }
                // Sound Blaster 16 PCI devices
                else if (pci.VendorID == 0x1274 && pci.DeviceID == 0x5000)
                {
                    Console.WriteLine("Found Sound Blaster 16 PCI device");
                    return SoundBlaster16.Initialize(4096);
                }
            }
            Console.WriteLine("No audio devices found");
            return null;
        }
    }
}