using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.RSA
{
    public interface IRsaHelper
    {
        string Encrypt(string text);
        string Decrypt(string encrypted);
    }
}