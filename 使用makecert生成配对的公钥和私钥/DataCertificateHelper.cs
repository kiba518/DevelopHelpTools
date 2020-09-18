using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Utility;
namespace VanPeng.Desktop.Licence
{
    public  class DataCertificateHelper
    { 
        public static  void CopyPfxAndGetInfo()
        { 
            string keyName = "VanPeng.Desktop.Licence";
            var ret = DataCertificate.CreateCertWithPrivateKey(keyName, @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\makecert.exe");
            if(ret)
            {
                DataCertificate.ExportToPfxFile(keyName, "VanPeng.pfx", "123456", true);
                X509Certificate2 x509 = DataCertificate.GetCertificateFromPfxFile("VanPeng.pfx", "123456");
                string publickey = x509.PublicKey.Key.ToXmlString(false);
                string privatekey = x509.PrivateKey.ToXmlString(true);

                string myname = "my name is VanPeng.Desktop.Licence!";
                string enStr = RSAEncrypt(publickey, myname);
                MessageBox.Show("密文是：" + enStr);
                string deStr = RSADecrypt(privatekey, enStr);
                MessageBox.Show("明文是：" + deStr);
            }
        }
       
      
       
        /// <summary> 
        /// RSA解密 
        /// </summary> 
        /// <param name="xmlPrivateKey"></param> 
        /// <param name="m_strDecryptString"></param> 
        /// <returns></returns> 
        public static string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xmlPrivateKey);
            byte[] rgb = Convert.FromBase64String(m_strDecryptString);
            byte[] bytes = provider.Decrypt(rgb, false);
            return new UnicodeEncoding().GetString(bytes);
        }
        /// <summary> 
        /// RSA加密 
        /// </summary> 
        /// <param name="xmlPublicKey"></param> 
        /// <param name="m_strEncryptString"></param> 
        /// <returns></returns> 
        public static string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.FromXmlString(xmlPublicKey);
            byte[] bytes = new UnicodeEncoding().GetBytes(m_strEncryptString);
            return Convert.ToBase64String(provider.Encrypt(bytes, false));
        }
       
      
    }
}
