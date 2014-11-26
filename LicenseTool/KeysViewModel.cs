using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;

namespace RegistryTool
{
    public class KeysViewModel:NotificationObject
    {
        private string _privateKey;
        private string _publicKey;

        public KeysViewModel()
        {
            GenerateCommand = new SimpleCommand(GenerateKeys);
        }

        public ICommand GenerateCommand { get; set; }

        public string PrivateKey
        {
            get
            {
                return _privateKey;
            }

            set
            {
                _privateKey = value;
                NotifyPropertyChanged("PrivateKey");
            }
        }

        public string PublicKey
        {
            get
            {
                return _publicKey;
            }

            set
            {
                _publicKey = value;
                NotifyPropertyChanged("PublicKey");
            }
        }

        private void GenerateKeys(object obj)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            PrivateKey = rsa.ToXmlString(true);
            PublicKey = rsa.ToXmlString(false);
            //rsa.FromXmlString(PrivateKey);
            //rsa.Encrypt(null, true);
        }
    }
}
