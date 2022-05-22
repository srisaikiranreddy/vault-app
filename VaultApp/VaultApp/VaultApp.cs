using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VaultApp.Crypto;
using VaultApp.Data;
using System.Collections.Specialized;

namespace VaultApp
{
    public partial class VaultApp : Form
    {
        private DataTable EnDecrypt;
        private NameValueCollection appSettings = ConfigurationManager.AppSettings;

        private Dictionary<byte[],byte[]> encrypt;

        public VaultApp()
        {
            InitializeComponent();
        }

        private void VaultApp_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = VaultData.Data();
            dataGridView2.DataSource = VaultData.Data();
            Encrypt.Enabled = true;
            Decrypt.Enabled = false;

            //dataGridView1.Dock = DockStyle.Fill;
            //this.Controls.Add(dataGridView1);
            //InitializeDataGridView();
        }

        private void Encrypt_Click(object sender, EventArgs e)
        {
            var data = VaultData.Data().Rows;
            encrypt = new Dictionary<byte[], byte[]>();
            EnDecrypt = new DataTable();
            EnDecrypt.Columns.Add("UserName", typeof(string));
            EnDecrypt.Columns.Add("Password", typeof(string));
            StatusBar.Minimum = 0;
            StatusBar.Value = 0;
            StatusBar.Maximum = data.Count;
            for (int i = 0; i < data.Count; i++)
            {
                byte[] userNameEnCrypted = Crypto.Encrypt.EncryptStringToBytes_Aes(data[i].ItemArray[0].ToString(),
                    Convert.FromBase64String(appSettings["AesKey"]), Convert.FromBase64String(appSettings["AesIV"]));

                byte[] passwordEnCrypted = Crypto.Encrypt.EncryptStringToBytes_Aes(data[i].ItemArray[1].ToString(),
                    Convert.FromBase64String(appSettings["AesKey"]), Convert.FromBase64String(appSettings["AesIV"]));

                StatusBar.PerformStep();

                encrypt.Add(userNameEnCrypted, passwordEnCrypted);

                EnDecrypt.Rows.Add(new object[]
                {
                        Encoding.ASCII.GetString(userNameEnCrypted, 0, userNameEnCrypted.Length) ,
                        Encoding.ASCII.GetString(passwordEnCrypted, 0, passwordEnCrypted.Length)
                });
            }

            dataGridView2.DataSource = EnDecrypt;
            AutoResize();
            Encrypt.Enabled = false;
            Decrypt.Enabled = true;

        }

        private void Decrypt_Click(object sender, EventArgs e)
        {   
            EnDecrypt = new DataTable();
            EnDecrypt.Columns.Add("UserName", typeof(string));
            EnDecrypt.Columns.Add("Password", typeof(string));
            StatusBar.Value = 0;
            StatusBar.Minimum = 0;
            StatusBar.Maximum = encrypt.Count;
            for (int i = 0; i < encrypt.Count; i++)
            {
                string userNameDeCrypted = Crypto.Decrypt.DecryptStringFromBytes_Aes(encrypt.ElementAt(i).Key,
                   Convert.FromBase64String(appSettings["AesKey"]), Convert.FromBase64String(appSettings["AesIV"]));

                string passwordDeCrypted = Crypto.Decrypt.DecryptStringFromBytes_Aes(encrypt.ElementAt(i).Value,
                   Convert.FromBase64String(appSettings["AesKey"]), Convert.FromBase64String(appSettings["AesIV"]));

                StatusBar.PerformStep();

                EnDecrypt.Rows.Add(new object[]
                {
                    userNameDeCrypted ,
                     passwordDeCrypted
                });
            }

            dataGridView2.DataSource = EnDecrypt;
            AutoResize();
            Encrypt.Enabled = true;
            Decrypt.Enabled = false;
            
        }

        private void AutoResize()
        {
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void InitializeDataGridView()
        {
            // Set up the DataGridView.
            dataGridView1.Dock = DockStyle.Fill;

            // Automatically generate the DataGridView columns.
            dataGridView1.AutoGenerateColumns = true;

            // Set up the data source.
            dataGridView1.DataSource = VaultData.Data();

            // Automatically resize the visible rows.
            dataGridView1.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            // Set the DataGridView control's border.
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;

            // Put the cells in edit mode when user enters them.
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
        }
    }
}
