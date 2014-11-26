using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.License
{
    public class LicenseController
    {
        private LicenseStorageUtil _storageUtil;

        public LicenseController(Guid appId, string assemblyCode)
        {
            _storageUtil = new LicenseStorageUtil(appId, assemblyCode);
        }

        public string GetProductLicenseInfo(out bool isLicenseValid)
        {
            string base64stringCode = _storageUtil.LoadVerificationInformation();
            string licenseContent = _storageUtil.LoadLicenseInformation();
            isLicenseValid = ValidateLicense(base64stringCode, licenseContent);
            if (!string.IsNullOrEmpty(licenseContent))
            {
                byte[] bytes = Convert.FromBase64String(licenseContent);
                string info = Encoding.UTF8.GetString(bytes);
                return info;
            }

            return string.Empty;
        }

        public bool MakeLicense(string verificationCode, string licenseContent)
        {
            bool bln = ValidateLicense(verificationCode, licenseContent);
            if (!bln)
            {
                return false;
            }

            _storageUtil.WriteVerificationInformation(verificationCode);
            _storageUtil.WriteLicenseInformation(licenseContent);

            return true;
        }

        private bool ValidateLicense(string verificationCode, string licenseContent)
        {
            return RsaUtil.VerifyLicenseCode(verificationCode, licenseContent);
        }
    }
}
