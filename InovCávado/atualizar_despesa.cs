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

namespace InovCávado
{
    public partial class atualizar_despesa : Form
    {
        public atualizar_despesa()
        {
            InitializeComponent();
            textBox3.Text = String.Format("{0:#,##0.00}", 0d);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void atualizar_despesa_Load(object sender, EventArgs e)
        {
            comboBox2.Text = global.Nome;

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
    }
}
