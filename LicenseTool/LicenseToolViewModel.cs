using Common;
using Common.License;
using LicenseTool.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RegistryTool
{
    public class LicenseToolViewModel : NotificationObject
    {
        private string _licenseCode;
        private string _verificationCode;

        public LicenseToolViewModel()
        {
            GenerateCodeCommand = new SimpleCommand(GenerateRegistryCode);
            LicenseInfoCommand = new SimpleCommand(ShowLicenseInfo);
        }

        public ICommand LicenseInfoCommand { get; set; }
        public ICommand GenerateCodeCommand { get; set; }

        public string LicenseCode
        {
            get
            {
                return _licenseCode;
            }

            set
            {
                _licenseCode = value;
                NotifyPropertyChanged("LicenseCode");
            }
        }

        public string VerificationCode
        {
            get
            {
                return _verificationCode;
            }

            set
            {
                _verificationCode = value;
                NotifyPropertyChanged("VerificationCode");
            }
        }

        private void GenerateRegistryCode(object obj)
        {
            if (string.IsNullOrEmpty(LicenseCode))
            {
                MessageBox.Show("请输入注册码，系统会根据输入的注册码生成验证码");
                return;
            }

            string content = EncryptoKeys.RsaContent;

            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(content);
                    byte[] licenseBytes = Convert.FromBase64String(LicenseCode);
                    var verificationBytes = rsa.SignData(licenseBytes, "MD5");
                    VerificationCode = Convert.ToBase64String(verificationBytes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "生成注册验证码失败");
            }
        }

        private void ShowLicenseInfo(object obj)
        {
            if (string.IsNullOrEmpty(LicenseCode))
            {
                MessageBox.Show("请输入注册码");
                return;
            }

            try
            {
                byte[] licenseBytes = Convert.FromBase64String(LicenseCode);
                string str = Encoding.UTF8.GetString(licenseBytes);
                string[] arr = str.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                LicenseInfo license;
                string hardwareInfo = Encoding.UTF8.GetString(Convert.FromBase64String(arr[5]));
                if (LicenseInfo.TryParse(str, out license))
                {
                    license.HardwareInfo = hardwareInfo;
                }

                MessageBox.Show(license.LicenseContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "生成注册验证码失败");
            }
        }
    }
}
