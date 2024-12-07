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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection Conexao = new MySqlConnection("Persist Security Info= False; server=localhost ;database = pap; uid=root");

            try
            {

                MySqlCommand strSQL = new MySqlCommand("Select * from vendedor where Email= '" + textBox1.Text + "' and Passe= '" + textBox2.Text + "'", Conexao);


                Conexao.Open();

                string query = "Select * from vendedor where Email = '" + textBox1.Text + "' and Passe = '" + textBox2.Text + "'";
                MySqlCommand cmd = new MySqlCommand(query, Conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    global.cod_vendedor = Convert.ToInt32(dt.Rows[0]["cod_vendedor"].ToString());
                    global.Nome = dt.Rows[0]["Nome"].ToString();
                    global.Email = dt.Rows[0]["Email"].ToString();
                    Tela_carregamento tc = new Tela_carregamento();
                    tc.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Utilizador não encrontrado", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

            }
            catch (MySqlException a)
            {
                throw new Exception(a.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
                var checkBox = (CheckBox)sender;
                checkBox.Text = "Esconder Palavra-Passe";

            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                var checkBox = (CheckBox)sender;
                checkBox.Text = "Mostrar Palavra-Passe";

            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
