using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VaultApp.Crypto;
using VaultApp.Data;

namespace VaultApp
{
    public partial class VaultApp : Form
    {
        private bool Encrpyt = true;

        private bool Decrpyt = false;
        public VaultApp()
        {
            InitializeComponent();
        }

        private void VaultApp_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource =  VaultData.Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            AesManaged aesManaged = new AesManaged();
            byte[] key = Encoding.ASCII.GetBytes(@"qwr{@^h`h & _`50 / ja9!'dcmh3!uw<&=?");
            byte[] IV = Encoding.ASCII.GetBytes(@"9/\~V).A,lY&=t2b");

            if (Encrpyt)
            {
                foreach(var password in VaultData.Data().Rows)
                {
                    byte[] t = Encrypt.EncryptStringToBytes_Aes("NAME",aesManaged.Key,aesManaged.IV);

                    string someString = Encoding.ASCII.GetString(t);

                    string w = Decrypt.DecryptStringFromBytes_Aes(t, aesManaged.Key, aesManaged.IV);

                    //string result = Encoding.ASCII.GetString(w);
                }
                
            }

            if (Decrpyt)
            {
                foreach (var password in VaultData.Data().Rows)
                {
                    
                }

            }
        }
    }
}
