using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace InovCávado
{
    public partial class atualizar_fornecedor : Form
    {
        public atualizar_fornecedor()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        MySqlConnection oConn = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string email = textBox4.Text;
                bool validacaoemail = email.Contains("@") && email.Contains(".");

                if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != "") && (textBox4.Text != "") && (maskedTextBox1.Text.Length == 8) && (maskedTextBox2.Text.Length == 9) && (maskedTextBox3.Text.Length == 11))
                {
                    if (validacaoemail == true)
                    {
                        String strSQL = "";
                        MySqlCommand oCmd = new MySqlCommand();

                        oCmd = oConn.CreateCommand();

                        oConn.Open();

                        strSQL = ("update fornecedor set Nome='" + textBox2.Text + "', Morada='" + textBox1.Text + "', cod_postal='" + maskedTextBox1.Text + "', Localidade='" + textBox3.Text + "', Telefone='" + maskedTextBox2.Text + "',Email='" + textBox4.Text + "',NIF='" + maskedTextBox3.Text + "' Where cod_fornecedor=" + textBox5.Text + "");


                        oCmd.CommandText = strSQL;

                        oCmd.ExecuteNonQuery();


                        MessageBox.Show("Registos atualizados com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Email digitado inválido", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    MessageBox.Show("Preencha todos os campos para inserir um novo fornecedor", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                oConn.Close();


                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void atualizar_fornecedor_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
