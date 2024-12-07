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
    public partial class inserir_clientes : Form
    {
        public inserir_clientes()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        MySqlConnection oConn = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");

        int verificacao;
        int verificacao_2;
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
                        string strSQL_1 = "Select count(cod_cliente) FROM clientes where Nome_empresa = '" + textBox2.Text + "'";

                        MySqlCommand oCmd_1 = new MySqlCommand(strSQL_1, oConn);

                        oConn.Open();
                        MySqlDataReader odr;

                        odr = oCmd_1.ExecuteReader();

                        while (odr.Read())
                        {
                            verificacao = odr.GetInt32("count(cod_cliente)");
                        }
                        odr.Close();

                        string strSQL_2 = "Select count(cod_cliente) FROM clientes where NIF = '" + maskedTextBox3.Text + "'";

                        MySqlCommand oCmd_2 = new MySqlCommand(strSQL_2, oConn);


                        MySqlDataReader odr_1;

                        odr_1 = oCmd_2.ExecuteReader();

                        while (odr_1.Read())
                        {
                            verificacao_2 = odr_1.GetInt32("count(cod_cliente)");
                        }
                        odr_1.Close();

                        if (verificacao > 0)
                        {
                            MessageBox.Show("Já existe um cliente com esse nome na base de dados!", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            if (verificacao_2 > 0)
                            {
                                MessageBox.Show("Já existe um cliente com o mesmo NIF na base de dados!", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            else
                            {
                                string strConexao = ("Persist Security Info=False ; server=localhost; database=pap;uid=root");
                                MySqlConnection oConn = new MySqlConnection(strConexao);
                                oConn.Open();
                                string strSQL = "Insert into clientes(Nome_empresa,Morada,Localidade,cod_postal,Telefone,Email,NIF)values ('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox3.Text + "','" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + textBox4.Text + "','" + maskedTextBox3.Text + "')";
                                MySqlCommand oCmd = new MySqlCommand(strSQL, oConn);
                                oCmd.ExecuteNonQuery();
                                oConn.Close();

                                MessageBox.Show("Registos feitos com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                textBox1.Clear();
                                textBox2.Clear();
                                textBox3.Clear();
                                textBox4.Clear();
                                maskedTextBox1.Clear();
                                maskedTextBox2.Clear();
                                maskedTextBox3.Clear();


                            }

                        }


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


        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void inserir_clientes_MouseDown(object sender, MouseEventArgs e)
        { 
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
