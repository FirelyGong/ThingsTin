using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.License
{
    public class LicenseInfo:NotificationObject
    {
        private string _target;
        private string _module;
        private string _seq;
        private string _startDate;
        private string _expireDate;
        private string _hardwareInfo;

        public string Target
        {
            get
            {
                return _target;
            }

            set
            {
                _target = value;
                NotifyPropertyChanged("Target");
                NotifyPropertyChanged("LicenseCode");
            }
        }

        public string Module
        {
            get
            {
                return _module;
            }

            set
            {
                _module = value;
                NotifyPropertyChanged("Module");
                NotifyPropertyChanged("LicenseCode");
            }
        }

        public string Seq
        {
            get
            {
                return _seq;
            }

            set
            {
                _seq = value;
                NotifyPropertyChanged("Seq");
                NotifyPropertyChanged("LicenseCode");
            }
        }

        public string StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                _startDate = value;
                NotifyPropertyChanged("StartDate");
                NotifyPropertyChanged("LicenseCode");
            }
        }

        public string ExpireDate
        {
            get
            {
                return _expireDate;
            }

            set
            {
                _expireDate = value;
                NotifyPropertyChanged("ExpireDate");
                NotifyPropertyChanged("LicenseCode");
            }
        }

        public string HardwareInfo
        {
            get
            {
                return _hardwareInfo;
            }

            set
            {
                _hardwareInfo = value;
                NotifyPropertyChanged("TargetId");
                NotifyPropertyChanged("LicenseCode");
            }
        }

        public string LicenseCode
        {
            get
            {
                string content = ToString();
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                string base64 = Convert.ToBase64String(bytes);
                return base64;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Target);
            sb.AppendLine(Module);
            sb.AppendLine(Seq);
            sb.AppendLine(StartDate);
            sb.AppendLine(ExpireDate);
            sb.Append(HardwareInfo);

            return sb.ToString();
        }

        public string LicenseContent
        {
            get
            {
                StringBuilder sb = new StringBuilder("授权电脑: ");
                sb.AppendLine(Target);
                sb.Append("授权模块: ");
                sb.AppendLine(Module);
                sb.Append("序列号: ");
                sb.AppendLine(Seq);
                sb.Append("有效期起: ");
                sb.AppendLine(StartDate);
                sb.Append("有效期止: ");
                sb.AppendLine(ExpireDate);
                sb.Append("硬件识别码: ");
                sb.Append(HardwareInfo);
                return sb.ToString();
            }
        }

        public static bool TryParse(string buffer, out LicenseInfo licenseInfo)
        {
            string[] arr = buffer.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            licenseInfo = null;

            string target;
            string module;
            string seq;
            string hardware;
            string startDate;
            string expiredDate;

            if(arr.Length==6)
            {
                target = arr[0];
                module = arr[1];
                seq = arr[2];
                hardware = arr[5];
                DateTime dt;
                if (!DateTime.TryParse(arr[3], out dt))
                {
                    return false;
                }

                startDate = dt.ToString("yyyy/MM/dd");

                if (!DateTime.TryParse(arr[4], out dt))
                {
                    return false;
                }

                expiredDate = dt.ToString("yyyy/MM/dd");

                licenseInfo = new LicenseInfo
                {
                    Target = target,
                    Module = module,
                    Seq = seq,
                    HardwareInfo = hardware,
                    StartDate = startDate,
                    ExpireDate = expiredDate     
                };

                return true;
            }

            return false;
        }
    }
}
