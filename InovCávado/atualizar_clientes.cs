using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace InovCávado
{

    
    public partial class atualizar_clientes : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        MySqlConnection oConn = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");

        public atualizar_clientes()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void atualizar_clientes_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

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
                        oConn.Open();
                        String strSQL = "";
                        MySqlCommand oCmd = new MySqlCommand();

                        oCmd = oConn.CreateCommand();



                        strSQL = ("update clientes set Nome_empresa='" + textBox2.Text + "', Morada='" + textBox1.Text + "', cod_postal='" + maskedTextBox1.Text + "', Localidade='" + textBox3.Text + "', Telefone='" + maskedTextBox2.Text + "',Email='" + textBox4.Text + "',NIF='" + maskedTextBox3.Text + "' Where cod_cliente=" + textBox5.Text + "");


                        oCmd.CommandText = strSQL;

                        oCmd.ExecuteNonQuery();


                        MessageBox.Show("Registos atualizados com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        maskedTextBox1.Clear();
                        maskedTextBox2.Clear();
                        maskedTextBox3.Clear();
                        this.Close();
                    }

                    else
                    {
                        MessageBox.Show("Email digitado inválido", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    MessageBox.Show("Preencha todos os campos para inserir um novo cliente", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                   
                }


                oConn.Close();

             
           
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
    }
}
