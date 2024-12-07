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
    public partial class categoria : Form
    {
        public categoria()
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

        int n_tipoproduto = 0;

        int verificacao;
        int verificacao_1;

        MySqlConnection conexao = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");


        public void avançareretroceder()
        {
            if(TotalRegistros<=tamanhoPagina)
            {
                btnanterior.Enabled = false;
                btnavançar.Enabled = false;
            }
            else
            {
                btnavançar.Enabled=true;
            }
        }

        public void carregar_dados()
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

                strSQL = ("Select * FROM tipo_produto");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Categoria");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Categoria";

                dataGridView1.Columns["Editar"].DisplayIndex = 2;
                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 180;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 395;


                string strSQL_7 = "Select count(cod_tipoproduto) FROM tipo_produto";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_tipoproduto = odr_3.GetInt32("count(cod_tipoproduto)");
                }
                odr_3.Close();
                label7.Text = n_tipoproduto.ToString();


                dataGridView1.Columns["cod_tipoproduto"].HeaderText = "Código Cliente";
                dataGridView1.Columns["Nome_categoria"].HeaderText = "Nome categoria";



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

        private void button1_Click(object sender, EventArgs e)
        {
            conexao.Open();
            if((textBox4.Text==""))
            {
                if(textBox3.Text!="")
                {

                    string strSQL_1 = "Select count(cod_tipoproduto) FROM tipo_produto where Nome_categoria = '" + textBox3.Text + "'";


                    MySqlCommand oCmd_1 = new MySqlCommand(strSQL_1, conexao);



                  
                    MySqlDataReader odr;

                    odr = oCmd_1.ExecuteReader();

                    while (odr.Read())
                    {
                        verificacao = odr.GetInt32("count(cod_tipoproduto)");
                    }
                    odr.Close();

                    if (verificacao > 0)
                    {
                        MessageBox.Show("Já existe uma categoria com esse nome na base de dados!", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {

                        string strConexao = ("Persist Security Info=False ; server=localhost; database=pap;uid=root");
                        MySqlConnection oConn = new MySqlConnection(strConexao);
                        oConn.Open();
                        string strSQL = "Insert into tipo_produto(Nome_categoria)values ('" + textBox3.Text + "')";
                        MySqlCommand oCmd = new MySqlCommand(strSQL, oConn);
                        oCmd.ExecuteNonQuery();
                        oConn.Close();


                        MessageBox.Show("Registos atualizados com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregar_dados();
                        textBox3.Clear();
                        avançareretroceder();


                        string strSQL_7 = "Select count(cod_tipoproduto) FROM tipo_produto";
                        DataTable oTable_3 = new DataTable();
                        MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, conexao);

                        MySqlDataReader odr_3;

                        odr_3 = oCmd_6.ExecuteReader();

                        while (odr_3.Read())
                        {
                            n_tipoproduto = odr_3.GetInt32("count(cod_tipoproduto)");
                        }
                        odr_3.Close();
                        label7.Text = n_tipoproduto.ToString();
                    }
                }
                else
                {

                    MessageBox.Show("Preencha todos os campos para inserir uma nova categoria", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                 
                }


                conexao.Close();

            }
       
            else if (textBox4.Text!="")
             {
                if (textBox3.Text != "")
                {
                    string strConexao = ("Persist Security Info=False ; server=localhost; database=pap;uid=root");

                    MySqlConnection oConn = new MySqlConnection(strConexao);
                    String strSQL = "";
                    MySqlCommand oCmd = new MySqlCommand();

                    oCmd = oConn.CreateCommand();

                    oConn.Open();

                    strSQL = ("update tipo_produto set Nome_categoria='" + textBox3.Text + "' Where cod_tipoproduto=" + textBox4.Text + "");


                    oCmd.CommandText = strSQL;

                    oCmd.ExecuteNonQuery();

                    oConn.Close();


                    MessageBox.Show("Registos atualizados com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    carregar_dados();

                    textBox3.Clear();
                    textBox4.Visible = false;
                    label6.Visible = false;
                    label5.Text = "Adicionar Nova Categoria";
                    avançareretroceder();


                    string strSQL_8 = "Select count(cod_tipoproduto) FROM tipo_produto";
                    DataTable oTable_4 = new DataTable();
                    MySqlCommand oCmd_7 = new MySqlCommand(strSQL_8, conexao);

                    MySqlDataReader odr_4;

                    odr_4 = oCmd_7.ExecuteReader();

                    while (odr_4.Read())
                    {
                        n_tipoproduto = odr_4.GetInt32("count(cod_tipoproduto)");
                    }
                    odr_4.Close();
                    label7.Text = n_tipoproduto.ToString();


                }
                else
                {

                    MessageBox.Show("Preencha todos os campos para inserir um novo categoria", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);


                   
                }


            }
         
            conexao.Close();
        }

        public void categoria_Load(object sender, EventArgs e)
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

                strSQL = ("Select * FROM tipo_produto");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Categoria");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Categoria";


                dataGridView1.Columns["Editar"].DisplayIndex = 2;
                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 180;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 395;





                dataGridView1.Columns["cod_tipoproduto"].HeaderText = "Código Cliente";
                dataGridView1.Columns["Nome_categoria"].HeaderText = "Nome categoria";


                string strSQL_7 = "Select count(cod_tipoproduto) FROM tipo_produto";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_tipoproduto = odr_3.GetInt32("count(cod_tipoproduto)");
                }
                odr_3.Close();
                label7.Text = n_tipoproduto.ToString();

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

                strSQL = ("Select * FROM tipo_produto");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Categoria");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Categoria";


                dataGridView1.Columns["Editar"].DisplayIndex = 2;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 180;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 395;



                string strSQL_7 = "Select count(cod_tipoproduto) FROM tipo_produto";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_tipoproduto = odr_3.GetInt32("count(cod_tipoproduto)");
                }
                odr_3.Close();
                label7.Text = n_tipoproduto.ToString();

                dataGridView1.Columns["cod_tipoproduto"].HeaderText = "Código Cliente";
                dataGridView1.Columns["Nome_categoria"].HeaderText = "Nome categoria";



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

                strSQL = ("Select * FROM tipo_produto where Nome_categoria like '%" + textBox2.Text + "%'");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Categoria");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Categoria";


                dataGridView1.Columns["Editar"].DisplayIndex = 2;
                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 180;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 395;



                string strSQL_7 = "Select count(cod_tipoproduto) FROM tipo_produto";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_tipoproduto = odr_3.GetInt32("count(cod_tipoproduto)");
                }
                odr_3.Close();
                label7.Text = n_tipoproduto.ToString();


                dataGridView1.Columns["cod_tipoproduto"].HeaderText = "Código Cliente";
                dataGridView1.Columns["Nome_categoria"].HeaderText = "Nome categoria";



                oConn.Close();

                textBox1.Text = TotalRegistros.ToString();
                avançareretroceder();
                label1.Text = "Página " + pagatual.ToString();

                btnanterior.Enabled = false;
                btnavançar.Enabled = false;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string cod_tipoproduto = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string Nome_categoria = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            label5.Text = "Atualizar Categoria";
            textBox4.Text = cod_tipoproduto;
            textBox3.Text = Nome_categoria;
            textBox4.Visible = true;
            label6.Visible = true;
         
        }
    }
}
