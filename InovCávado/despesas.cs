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
    public partial class despesas : Form
    {
        public despesas()
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

        int n_despesas;

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


        private void despesas_Load(object sender, EventArgs e)
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

                strSQL = ("Select cod_despesa,Estabelecimento,descricao,custo,categoria,Nome FROM despesas,vendedor,tipo_despesa where despesas.cod_vendedor=vendedor.cod_vendedor and despesas.cod_tipodespesa=tipo_despesa.cod_tipodespesa");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Despesas");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Despesas";



                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 120;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 180;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 180;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 120;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 120;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 184;

                dataGridView1.Columns["Editar"].DisplayIndex = 6;


                dataGridView1.Columns["cod_despesa"].HeaderText = "Número de Despesa";
                dataGridView1.Columns["Estabelecimento"].HeaderText = "Nome Empresa Compra";
                dataGridView1.Columns["descricao"].HeaderText = "Descrição";
                dataGridView1.Columns["custo"].HeaderText = "Custo";
                dataGridView1.Columns["categoria"].HeaderText = "Categoria";
                dataGridView1.Columns["Nome"].HeaderText = "Nome Vendedor";


                string strSQL_7 = "Select count(cod_despesa) FROM despesas";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_despesas = odr_3.GetInt32("count(cod_despesa)");
                }
                odr_3.Close();
                label7.Text = n_despesas.ToString();

                oConn.Close();


                oConn.Open();

                DataTable oTable = new DataTable();



                string strSQL_1 = "Select * FROM vendedor";
                MySqlDataAdapter oDA_1 = new MySqlDataAdapter(strSQL_1, oConn);
                oDA_1.Fill(oTable);
                comboBox1.DataSource = oTable;
                comboBox1.DisplayMember = "Nome";
                comboBox1.ValueMember = "cod_vendedor";


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

                strSQL = ("Select cod_despesa,Estabelecimento,descricao,custo,categoria,Nome FROM despesas,vendedor,tipo_despesa where despesas.cod_vendedor=vendedor.cod_vendedor and despesas.cod_tipodespesa=tipo_despesa.cod_tipodespesa");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Despesas");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Despesas";



                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 120;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 180;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 180;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 120;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 120;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 184;

                dataGridView1.Columns["Editar"].DisplayIndex = 6;


                dataGridView1.Columns["cod_despesa"].HeaderText = "Número de Despesa";
                dataGridView1.Columns["Estabelecimento"].HeaderText = "Nome Empresa Compra";
                dataGridView1.Columns["descricao"].HeaderText = "Descrição";
                dataGridView1.Columns["custo"].HeaderText = "Custo";
                dataGridView1.Columns["categoria"].HeaderText = "Categoria";
                dataGridView1.Columns["Nome"].HeaderText = "Nome Vendedor";

                string strSQL_7 = "Select count(cod_despesa) FROM despesas";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_despesas = odr_3.GetInt32("count(cod_despesa)");
                }
                odr_3.Close();
                label7.Text = n_despesas.ToString();


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
                oDA.Fill(ds, inicio, tamanhoPagina, "clientes");
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
                oDA.Fill(ds, inicio, tamanhoPagina, "clientes");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string strConexao = ("Persist Security Info=False;server = localhost;database = pap ;uid=root");

            MySqlConnection oConn = new MySqlConnection(strConexao);
            String strSQL = "";
            MySqlCommand oCmd = new MySqlCommand();

            oCmd = oConn.CreateCommand();

            oConn.Open();

            strSQL = ("Select cod_despesa,Estabelecimento,descricao,custo,categoria,Nome FROM despesas,vendedor,tipo_despesa where despesas.cod_vendedor=vendedor.cod_vendedor and despesas.cod_tipodespesa=tipo_despesa.cod_tipodespesa and vendedor.cod_vendedor like '" + comboBox1.SelectedValue + "'");

            oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
            dt = new DataTable();
            ds = new DataSet();
            oDA.Fill(dt);
            TotalRegistros = dt.Rows.Count;
            oDA.Fill(ds, inicio, tamanhoPagina, "Despesas");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Despesas";



            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 80;
            DataGridViewColumn column1 = dataGridView1.Columns[1];
            column1.Width = 120;
            DataGridViewColumn column2 = dataGridView1.Columns[2];
            column2.Width = 180;
            DataGridViewColumn column3 = dataGridView1.Columns[3];
            column3.Width = 180;
            DataGridViewColumn column4 = dataGridView1.Columns[4];
            column4.Width = 120;
            DataGridViewColumn column5 = dataGridView1.Columns[5];
            column5.Width = 120;
            DataGridViewColumn column6 = dataGridView1.Columns[6];
            column6.Width = 184;

            dataGridView1.Columns["Editar"].DisplayIndex = 6;


            dataGridView1.Columns["cod_despesa"].HeaderText = "Número de Despesa";
            dataGridView1.Columns["Estabelecimento"].HeaderText = "Nome Empresa Compra";
            dataGridView1.Columns["descricao"].HeaderText = "Descrição";
            dataGridView1.Columns["custo"].HeaderText = "Custo";
            dataGridView1.Columns["categoria"].HeaderText = "Categoria";
            dataGridView1.Columns["Nome"].HeaderText = "Nome Vendedor";




            oConn.Close();


            textBox1.Text = TotalRegistros.ToString();
            avançareretroceder();
            label1.Text = "Página " + pagatual.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] == dataGridView1.Columns["Editar"])
            {
                string cod_despesa = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string Estabelcimento = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string descricao = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string custo = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                string categoria = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                string Nome = dataGridView1.CurrentRow.Cells[6].Value.ToString();

                atualizar_despesa ad = new atualizar_despesa();
                ad.Show();

                ad.textBox2.Text = Estabelcimento;
                ad.textBox1.Text = descricao;
                ad.textBox3.Text = custo;
                ad.comboBox1.Text = categoria;
                ad.comboBox2.Text = Nome;
            
            }
        }

            private void pictureBox5_Click(object sender, EventArgs e)
        {
            inserir_despesa id = new inserir_despesa();
            id.Show();  
        }
    }
    }
