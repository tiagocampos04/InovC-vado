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

namespace InovCávado
{
    public partial class fornecedores : Form
    {
        public fornecedores()
        {
            InitializeComponent();
        }

        MySqlDataAdapter oDA;

        DataSet ds;

        DataTable dt;

        int tamanhoPagina = 6;

        int inicio = 0;

        int TotalRegistros;

        int pagatual = 1;

        public int n_fornecedores;

        public void avançareretroceder()
        {
            if (TotalRegistros <= tamanhoPagina)
            {
                btnanterior.Enabled = false;
                btnavançar.Enabled = false;
            }
            else
            {
                btnavançar.Enabled = true;
            }
        }
        private void fornecedores_Load(object sender, EventArgs e)
        {
            btnanterior.Enabled = false;
            try
            {
                string strConexao = ("Persist Security Info=False;server = localhost;database = pap ;uid=root");

                MySqlConnection oConn = new MySqlConnection(strConexao);
                String strSQL = "";
                MySqlCommand oCmd = new MySqlCommand();

                oCmd = oConn.CreateCommand();

                oConn.Open();

                strSQL = ("Select cod_fornecedor,Nome,Morada,Localidade,cod_postal,Telefone,Email,NIF FROM fornecedor");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Fornecedores");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Fornecedores";

                string strSQL_7 = "Select count(cod_fornecedor) FROM fornecedor";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_fornecedores = odr_3.GetInt32("count(cod_fornecedor)");
                }
                odr_3.Close();
                label5.Text = n_fornecedores.ToString();


                dataGridView1.Columns["Editar"].DisplayIndex = 8;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 91;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 150;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 160;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 91;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 120;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 101;
                DataGridViewColumn column7 = dataGridView1.Columns[7];
                column7.Width = 120;
                DataGridViewColumn column8 = dataGridView1.Columns[8];
                column8.Width = 101;


                dataGridView1.Columns["cod_fornecedor"].HeaderText = "Código Fornecedor";
                dataGridView1.Columns["Nome"].HeaderText = "Nome Fornecedor";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["Telefone"].HeaderText = "Telefone";
                dataGridView1.Columns["Morada"].HeaderText = "Morada";
                dataGridView1.Columns["cod_postal"].HeaderText = "Código Postal";
                dataGridView1.Columns["Localidade"].HeaderText = "Localidade";
                dataGridView1.Columns["NIF"].HeaderText = "NIF";



                oConn.Close();

                textBox1.Text = TotalRegistros.ToString();
                avançareretroceder();
                label1.Text = "Página " + pagatual.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnanterior_Click(object sender, EventArgs e)
        {
            try
            {
                if (inicio <= 0)
                {
                    inicio = 0;
                    MessageBox.Show("Não é possível retroceder mais");
                    pagatual = pagatual + 0;
                    btnanterior.Enabled = false;
                    label1.Text = "Página " + pagatual.ToString();
                }
                else if (inicio > 0)
                {
                    inicio = inicio - tamanhoPagina;
                    pagatual = pagatual - 1;
                    label1.Text = "Página " + pagatual.ToString();
                    btnavançar.Enabled = true;
                }
                ds.Clear();
                oDA.Fill(ds, inicio, tamanhoPagina, "Fornecedores");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnavançar_Click(object sender, EventArgs e)
        {
            try
            {
                if (inicio >= tamanhoPagina)
                {

                    btnavançar.Enabled = false;
                    pagatual = pagatual + 0;
                    btnanterior.Enabled = true;
                    label1.Text = "Página " + pagatual.ToString();
                    MessageBox.Show("Não é possível avançar mais");

                }
                else if (inicio < TotalRegistros)
                {
                    btnanterior.Enabled = true;
                    inicio = inicio + tamanhoPagina;
                    pagatual = pagatual + 1;
                    label1.Text = "Página " + pagatual.ToString();
                }

                ds.Clear();
                oDA.Fill(ds, inicio, tamanhoPagina, "Fornecedores");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            btnanterior.Enabled = false;
            btnavançar.Enabled = true;
            try
            {
                string strConexao = ("Persist Security Info=False;server = localhost;database = pap ;uid=root");

                MySqlConnection oConn = new MySqlConnection(strConexao);
                String strSQL = "";
                MySqlCommand oCmd = new MySqlCommand();

                oCmd = oConn.CreateCommand();

                oConn.Open();

                strSQL = ("Select cod_fornecedor,Nome,Morada,Localidade,cod_postal,Telefone,Email,NIF FROM fornecedor");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Fornecedores");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Fornecedores";


                dataGridView1.Columns["Editar"].DisplayIndex = 8;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 91;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 150;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 160;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 91;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 120;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 101;
                DataGridViewColumn column7 = dataGridView1.Columns[7];
                column7.Width = 120;
                DataGridViewColumn column8 = dataGridView1.Columns[8];
                column8.Width = 101;

                dataGridView1.Columns["cod_fornecedor"].HeaderText = "Código Fornecedor";
                dataGridView1.Columns["Nome"].HeaderText = "Nome Fornecedor";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["Telefone"].HeaderText = "Telefone";
                dataGridView1.Columns["Morada"].HeaderText = "Morada";
                dataGridView1.Columns["cod_postal"].HeaderText = "Código Postal";
                dataGridView1.Columns["Localidade"].HeaderText = "Localidade";
                dataGridView1.Columns["NIF"].HeaderText = "NIF";


                string strSQL_7 = "Select count(cod_fornecedor) FROM fornecedor";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_fornecedores = odr_3.GetInt32("count(cod_fornecedor)");
                }
                odr_3.Close();
                label5.Text = n_fornecedores.ToString();



                oConn.Close();

                textBox1.Text = TotalRegistros.ToString();
                avançareretroceder();
                label1.Text = "Página " + pagatual.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            btnanterior.Enabled = false;
            try
            {
                string strConexao = ("Persist Security Info=False;server = localhost;database = pap ;uid=root");

                MySqlConnection oConn = new MySqlConnection(strConexao);
                String strSQL = "";
                MySqlCommand oCmd = new MySqlCommand();

                oCmd = oConn.CreateCommand();

                oConn.Open();

                strSQL = ("Select cod_fornecedor,Nome,Morada,Localidade,cod_postal,Telefone,Email,NIF FROM fornecedor where Nome like '" + textBox2.Text + "'");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Fornecedores");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Fornecedores";


                dataGridView1.Columns["Editar"].DisplayIndex = 8;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 91;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 150;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 160;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 91;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 120;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 101;
                DataGridViewColumn column7 = dataGridView1.Columns[7];
                column7.Width = 120;
                DataGridViewColumn column8 = dataGridView1.Columns[8];
                column8.Width = 101;

                dataGridView1.Columns["cod_fornecedor"].HeaderText = "Código Fornecedor";
                dataGridView1.Columns["Nome"].HeaderText = "Nome Fornecedor";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["Telefone"].HeaderText = "Telefone";
                dataGridView1.Columns["Morada"].HeaderText = "Morada";
                dataGridView1.Columns["cod_postal"].HeaderText = "Código Postal";
                dataGridView1.Columns["Localidade"].HeaderText = "Localidade";
                dataGridView1.Columns["NIF"].HeaderText = "NIF";


                string strSQL_7 = "Select count(cod_fornecedor) FROM fornecedor";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_fornecedores = odr_3.GetInt32("count(cod_fornecedor)");
                }
                odr_3.Close();
                label5.Text = n_fornecedores.ToString();


                oConn.Close();

                textBox1.Text = TotalRegistros.ToString();
                avançareretroceder();
                label1.Text = "Página " + pagatual.ToString();

                btnanterior.Enabled = false;
                btnavançar.Enabled = false;
                textBox2.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] == dataGridView1.Columns["Editar"])
            {
                string cod_fornecedor = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string Nome_fornecedor = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string Email = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                string Telefone = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                string Morada = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string cod_postal = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                string Localidade = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                string NIF = dataGridView1.CurrentRow.Cells[8].Value.ToString();


         
                atualizar_fornecedor af = new atualizar_fornecedor();
                af.Show();


                af.textBox5.Text = cod_fornecedor;
                af.textBox2.Text = Nome_fornecedor;
                af.textBox1.Text = Morada;
                af.maskedTextBox1.Text = cod_postal;
                af.textBox3.Text = Localidade;
                af.maskedTextBox2.Text = Telefone;
                af.textBox4.Text = Email;
                af.maskedTextBox3.Text = NIF;


            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            inserir_fornecedores ifc = new inserir_fornecedores();
            ifc.Show();
        }
    }
}
