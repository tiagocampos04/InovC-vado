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
    public partial class visualizar_produtos : Form
    {
        public visualizar_produtos()
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

        int n_produtos;

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

        private void visualizar_produtos_Load(object sender, EventArgs e)
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

                strSQL = ("Select referencia,Nome_produto,Descricao,Nome_categoria FROM produtos,tipo_produto where produtos.cod_tipoproduto=tipo_produto.cod_tipoproduto");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Produtos");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Produtos";


                dataGridView1.Columns["Editar"].DisplayIndex = 4;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 230;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 250;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 230;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 224;

                dataGridView1.Columns["referencia"].HeaderText = "Referência";
                dataGridView1.Columns["Nome_produto"].HeaderText = "Nome Produto";
                dataGridView1.Columns["Descricao"].HeaderText = "Descrição";
                dataGridView1.Columns["Nome_categoria"].HeaderText = "Categoria";


                string strSQL_7 = "Select count(referencia) FROM produtos";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_produtos = odr_3.GetInt32("count(referencia)");
                }
                odr_3.Close();
                label5.Text = n_produtos.ToString();


                oConn.Close();

                textBox1.Text = TotalRegistros.ToString();
                avançareretroceder();
                label1.Text = "Página " + pagatual.ToString();
                textBox2.Clear();
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
                oDA.Fill(ds, inicio, tamanhoPagina, "Categoria");
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
                oDA.Fill(ds, inicio, tamanhoPagina, "Categoria");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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

                strSQL = ("Select referencia,Nome_produto,Descricao,Nome_categoria FROM produtos,tipo_produto where produtos.cod_tipoproduto=tipo_produto.cod_tipoproduto");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Produtos");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Produtos";


                dataGridView1.Columns["Editar"].DisplayIndex = 4;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 230;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 250;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 230;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 224;

                dataGridView1.Columns["referencia"].HeaderText = "Referência";
                dataGridView1.Columns["Nome_produto"].HeaderText = "Nome Produto";
                dataGridView1.Columns["Descricao"].HeaderText = "Descrição";
                dataGridView1.Columns["Nome_categoria"].HeaderText = "Categoria";

                string strSQL_7 = "Select count(referencia) FROM produtos";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_produtos = odr_3.GetInt32("count(referencia)");
                }
                odr_3.Close();
                label5.Text = n_produtos.ToString();

                oConn.Close();

                textBox1.Text = TotalRegistros.ToString();
                avançareretroceder();
                label1.Text = "Página " + pagatual.ToString();
                textBox2.Clear();
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

                strSQL = ("Select referencia,Nome_produto,Descricao,Nome_categoria FROM produtos,tipo_produto where produtos.cod_tipoproduto=tipo_produto.cod_tipoproduto and referencia like '%" + textBox2.Text + "%'");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Produtos");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Produtos";


                dataGridView1.Columns["Editar"].DisplayIndex = 4;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 230;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 250;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 230;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 224;

                dataGridView1.Columns["referencia"].HeaderText = "Referência";
                dataGridView1.Columns["Nome_produto"].HeaderText = "Nome Produto";
                dataGridView1.Columns["Descricao"].HeaderText = "Descrição";
                dataGridView1.Columns["Nome_categoria"].HeaderText = "Categoria";

                string strSQL_7 = "Select count(referencia) FROM produtos";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_produtos = odr_3.GetInt32("count(referencia)");
                }
                odr_3.Close();
                label5.Text = n_produtos.ToString();

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
            string referencia = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string Nome = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string descricao = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            string categoria = dataGridView1.CurrentRow.Cells[4].Value.ToString();
           

            atualizar_produto ap = new atualizar_produto();
            ap.Show();

            ap.textBox5.Text = referencia;
            ap.textBox2.Text = Nome;
            ap.textBox1.Text = descricao;
            ap.comboBox1.Text = categoria;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            inserir_produto ip = new inserir_produto();
            ip.Show();
        }
    }
}
