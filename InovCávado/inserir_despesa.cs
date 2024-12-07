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
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace InovCávado
{
    public partial class inserir_despesa : Form
    {
        public inserir_despesa()
        {
            InitializeComponent();
        }

        MySqlDataAdapter oDA;

        DataSet ds;

        DataTable dt;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void inserir_despesa_Load(object sender, EventArgs e)
        {
          comboBox2.Text= global.Nome;

            string strConexao = ("Persist Security Info =False;server = localhost;database= pap; uid=root");

            MySqlConnection oConn = new MySqlConnection(strConexao);

            DataTable oTable = new DataTable();

            oConn.Open();

            string strSQL = "Select * FROM tipo_despesa";
            MySqlDataAdapter oDA = new MySqlDataAdapter(strSQL, oConn);
            oDA.Fill(oTable);
            comboBox1.DataSource = oTable;
            comboBox1.DisplayMember = "categoria";
            comboBox1.ValueMember = "cod_tipodespesa";

            textBox3.Text = String.Format("{0:#,##0.00}", 0d);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string strConexao = ("Persist Security Info=False ; server=localhost; database=pap;uid=root");
                MySqlConnection oConn = new MySqlConnection(strConexao);
                oConn.Open();
                string strSQL = "Insert into despesas(Estabelecimento,descricao,custo,cod_vendedor,cod_tipodespesa)values ('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox3.Text + "','" + global.cod_vendedor+ "','" + comboBox1.SelectedValue + "')";
                MySqlCommand oCmd = new MySqlCommand(strSQL, oConn);
                oCmd.ExecuteNonQuery();
                oConn.Close();

                MessageBox.Show("Registos feitos com sucesso ");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                textBox3.Text = String.Format("{0:#,##0.00}", 0d);

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsDigit(e.KeyChar)) || (e.KeyChar.Equals((char)Keys.Back)))
            {
                TextBox t = (TextBox)sender;
                string w = Regex.Replace(t.Text, "[^0-9]", string.Empty);
                if (w == string.Empty) w = "00";

                if (e.KeyChar.Equals((char)Keys.Back))
                    w = w.Substring(0, w.Length - 1);
                else
                    w += e.KeyChar;

                t.Text = String.Format("{0:#,##0.00}", double.Parse(w) / 100);
                t.Select(t.Text.Length, 0);


            }
            e.Handled = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
