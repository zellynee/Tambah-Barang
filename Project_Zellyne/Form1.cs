using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Zellyne
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

         private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                datasiswaZellyneBindingSource.EndEdit();
                datasiswaZellyneTableAdapter.Update(this.appData.datasiswaZellyne);
                panel1.Enabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                datasiswaZellyneBindingSource.ResetBindings(false);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = true;
                txtMerkBarang.Focus();
                this.appData.datasiswaZellyne.AdddatasiswaZellyneRow(this.appData.datasiswaZellyne.NewdatasiswaZellyneRow());
                datasiswaZellyneBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                datasiswaZellyneBindingSource.ResetBindings(false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            datasiswaZellyneBindingSource.ResetBindings(false);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            txtMerkBarang.Focus();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            try
            {
                using(OpenFileDialog ofd=new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.datasiswaZellyne' table. You can move, or remove it, as needed.
            this.datasiswaZellyneTableAdapter.Fill(this.appData.datasiswaZellyne);
            datasiswaZellyneBindingSource.DataSource = this.appData.datasiswaZellyne;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ? ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    datasiswaZellyneBindingSource.RemoveCurrent();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                    dataGridView1.DataSource = datasiswaZellyneBindingSource;
                else
                {
                    var query = from o in this.appData.datasiswaZellyne
                                where o.Merk_Barang.Contains(txtSearch.Text) || o.Nama_Barang.Contains(txtSearch.Text) || o.Satuan.Contains(txtSearch.Text) || o.Quantity.Contains(txtSearch.Text) || o.Harga.Contains(txtSearch.Text)
                                select o;
                    dataGridView1.DataSource = query.ToList();
                }
            }
        }
    }
}
