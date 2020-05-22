using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SideNavSample
{
    public class password
    {

        public string Encode(string data)

        {

            SHA1 sha = new SHA1CryptoServiceProvider();

            string passdata = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(data));

            return passdata;
        }

    }
}

