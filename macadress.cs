using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace SideNavSample
{
    class macadress
    {
        public string MACAdresiAl()
        {

            string mm;
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty) sMacAddress = adapter.GetPhysicalAddress().ToString();
                mm = (sMacAddress);

            }
            return sMacAddress;


        }
    }
}
