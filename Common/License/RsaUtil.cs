using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.License
{
    internal class RsaUtil
    {
        private const string publicKey = @"<RSAKeyValue><Modulus>8NSayzoJuwHyuMyHrzy69TK9g35Fpv7Cgfv9aWpyJ2anntTUioD7SB2W6ULe5kKUrSwc9gkUmkv8AKKhn7obIUdzAXnwo9S1F5eckCceeq9fCDdNWMvVx1egpOJNTZrW/PwzgpjfhTM5oaIeF4wGgYJT21Rs4qjFD6woY8yXZWs=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        internal static bool VerifyLicenseCode(string verificationCode, string licenseCode)
        {
            try
            {
                byte[] codeBytes = Convert.FromBase64String(verificationCode);
                byte[] contentBytes =  Convert.FromBase64String(licenseCode);
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(publicKey);
                    bool valide = rsa.VerifyData(contentBytes, "MD5", codeBytes);
                    return valide;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
