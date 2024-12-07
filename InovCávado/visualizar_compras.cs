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
    public partial class visualizar_compras : Form
    {
        public visualizar_compras()
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

        int n_compras;

        int cod_fornecedor;

        MySqlConnection oConn = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");

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

        private void visualizar_compras_Load(object sender, EventArgs e)
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

                strSQL = ("Select cod_detalhe,compra.cod_compra,Nome,preco_sem_iva,detalhes_compra.iva,detalhes_compra.preco_final,data_compra,data_entrega,estado_pagamento FROM detalhes_compra,fornecedor,compra where detalhes_compra.cod_compra=compra.cod_compra and compra.cod_fornecedor=fornecedor.cod_fornecedor");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clientes";

                string strSQL_7 = "Select count(cod_detalhe) FROM detalhes_compra";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_compras = odr_3.GetInt32("count(cod_detalhe)");
                }
                odr_3.Close();
                label5.Text = n_compras.ToString();


                dataGridView1.Columns["cod_detalhe"].DisplayIndex = 0;
                dataGridView1.Columns["Cod_Compra"].DisplayIndex = 1;
                dataGridView1.Columns["Nome"].DisplayIndex = 2;
                dataGridView1.Columns["preco_sem_iva"].DisplayIndex = 3;
                dataGridView1.Columns["iva"].DisplayIndex = 4;
                dataGridView1.Columns["preco_final"].DisplayIndex = 5;
                dataGridView1.Columns["data_compra"].DisplayIndex = 6;
                dataGridView1.Columns["data_entrega"].DisplayIndex = 7;
                dataGridView1.Columns["estado_pagamento"].DisplayIndex = 8;


                dataGridView1.Columns["Eliminar"].DisplayIndex = 9;
                dataGridView1.Columns["Pagamento"].DisplayIndex = 10;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 60;
                DataGridViewColumn column_9 = dataGridView1.Columns[1];
                column.Width = 60;
                DataGridViewColumn column_10 = dataGridView1.Columns[2];
                column.Width = 60;
                DataGridViewColumn column_11 = dataGridView1.Columns[3];
                column.Width = 50;
                DataGridViewColumn column_12 = dataGridView1.Columns[4];
                column.Width = 100;
                DataGridViewColumn column1 = dataGridView1.Columns[5];
                column1.Width = 86;
                DataGridViewColumn column2 = dataGridView1.Columns[6];
                column2.Width = 80;
                DataGridViewColumn column3 = dataGridView1.Columns[7];
                column3.Width = 64;
                DataGridViewColumn column4 = dataGridView1.Columns[8];
                column4.Width = 100;
                DataGridViewColumn column5 = dataGridView1.Columns[9];
                column5.Width = 100;
                DataGridViewColumn column6 = dataGridView1.Columns[10];
                column6.Width = 100;

             

                MySqlConnection oConn_1 = new MySqlConnection(strConexao);

                DataTable oTable_10 = new DataTable();

               

                string strSQL_1 = "Select * FROM fornecedor";
                MySqlDataAdapter oDA_10 = new MySqlDataAdapter(strSQL_1, oConn);
                oDA_10.Fill(oTable_10);
                comboBox1.DataSource = oTable_10;
                comboBox1.DisplayMember = "Nome";
                comboBox1.ValueMember = "cod_fornecedor";



                dataGridView1.Columns["cod_detalhe"].HeaderText = "Código Detalhe";
                dataGridView1.Columns["Cod_Compra"].HeaderText = "Fatura";
                dataGridView1.Columns["preco_sem_iva"].HeaderText = "Preço sem Iva";
                dataGridView1.Columns["iva"].HeaderText = "IVA";
                dataGridView1.Columns["data_compra"].HeaderText = "Data Compra";
                dataGridView1.Columns["data_entrega"].HeaderText = "Data Entrega";
                dataGridView1.Columns["estado_pagamento"].HeaderText = "Estado do Pagamento";
                dataGridView1.Columns["Nome"].HeaderText = "Fornecedor";
                dataGridView1.Columns["preco_final"].HeaderText = "Preço Final";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] == dataGridView1.Columns["Pagamento"])
            {
                if(dataGridView1.CurrentRow.Cells[10].Value.ToString() == "Pago")
                {

                    MessageBox.Show("Esta Fatura já se encontra pago", " ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    string pago = "Pago";
                    oConn.Open();
                    String strSQL = "";
                    MySqlCommand oCmd = new MySqlCommand();

                    oCmd = oConn.CreateCommand();



                    strSQL = ("update detalhes_compra set estado_pagamento='" + pago + "' Where cod_detalhe=" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "");


                    oCmd.CommandText = strSQL;

                    oCmd.ExecuteNonQuery();

                    MySqlDataReader odr_3;


                    string strSQL_4 = "Select cod_fornecedor FROM fornecedor where Nome = '" + dataGridView1.CurrentRow.Cells[4].Value + "'";
                    DataTable oTable = new DataTable();
                    MySqlCommand oCmd_6 = new MySqlCommand(strSQL_4, oConn);
                    MySqlDataAdapter oDA = new MySqlDataAdapter(strSQL_4.ToString(), oConn);
                    oDA.Fill(oTable);

                    odr_3 = oCmd_6.ExecuteReader();

                    while (odr_3.Read())
                    {
                        cod_fornecedor = odr_3.GetInt32("cod_fornecedor");
                    }
                    odr_3.Close();

                  

                    MySqlCommand cmd_1 = new MySqlCommand();
                    cmd_1.Connection = oConn;
                    cmd_1.CommandText = "update fornecedor set dividas = dividas - @valorpago WHERE cod_fornecedor = @cod_fornecedor";
                    cmd_1.Parameters.AddWithValue("@valorpago", dataGridView1.CurrentRow.Cells[7].Value);
                    cmd_1.Parameters.AddWithValue("@cod_fornecedor", cod_fornecedor);

                    cmd_1.ExecuteNonQuery();

                  
                    MessageBox.Show("Pagamento confirmado com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    strSQL = ("Select cod_detalhe,compra.cod_compra,Nome,preco_sem_iva,detalhes_compra.iva,detalhes_compra.preco_final,data_compra,data_entrega,estado_pagamento FROM detalhes_compra,fornecedor,compra where detalhes_compra.cod_compra=compra.cod_compra and compra.cod_fornecedor=fornecedor.cod_fornecedor");

                    oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                    dt = new DataTable();
                    ds = new DataSet();
                    oDA.Fill(dt);
                    TotalRegistros = dt.Rows.Count;
                    oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Clientes";

                    avançareretroceder();
                    textBox1.Text = TotalRegistros.ToString();


                    string strSQL_7 = "Select count(cod_detalhe) FROM detalhes_compra";
                    DataTable oTable_3 = new DataTable();
                    MySqlCommand oCmd_7 = new MySqlCommand(strSQL_7, oConn);

                    MySqlDataReader odr_4;

                    odr_4 = oCmd_7.ExecuteReader();

                    while (odr_4.Read())
                    {
                        n_compras = odr_4.GetInt32("count(cod_detalhe)");
                    }
                    odr_4.Close();
                    label5.Text = n_compras.ToString();
                }

              

                oConn.Close();
            }
            else if (dataGridView1.Columns[e.ColumnIndex] == dataGridView1.Columns["Eliminar"])
            {
                oConn.Open();
                if (dataGridView1.CurrentRow.Cells[10].Value.ToString() == "Pago")
                {

                    MessageBox.Show("Este Fatura já se encontra paga logo não a pode eliminar", " ", MessageBoxButtons.OK, MessageBoxIcon.Hand);


                }
                else 
                {
                    MySqlDataReader odr_3;


                    string strSQL_4 = "Select cod_fornecedor FROM fornecedor where Nome = '" + dataGridView1.CurrentRow.Cells[4].Value + "'";
                    DataTable oTable = new DataTable();
                    MySqlCommand oCmd_6 = new MySqlCommand(strSQL_4, oConn);
                    MySqlDataAdapter oDA = new MySqlDataAdapter(strSQL_4.ToString(), oConn);
                    oDA.Fill(oTable);

                    odr_3 = oCmd_6.ExecuteReader();

                    while (odr_3.Read())
                    {
                        cod_fornecedor = odr_3.GetInt32("cod_fornecedor");
                    }
                    odr_3.Close();

                    MySqlCommand cmd_1 = new MySqlCommand();
                    cmd_1.Connection = oConn;
                    cmd_1.CommandText = "update fornecedor set dividas = dividas - @valorpago WHERE cod_fornecedor = @cod_fornecedor";
                    cmd_1.Parameters.AddWithValue("@valorpago", dataGridView1.CurrentRow.Cells[7].Value);
                    cmd_1.Parameters.AddWithValue("@cod_fornecedor", cod_fornecedor);

                    cmd_1.ExecuteNonQuery();

                  

                    string strSQL_5 = "Select cod_produto,quantidade FROM compra,detalhes_compra where detalhes_compra.cod_compra=compra.cod_compra and compra.cod_compra = '" + dataGridView1.CurrentRow.Cells[3].Value + "'";
                    DataTable oTable_1 = new DataTable();
                    MySqlCommand oCmd_7 = new MySqlCommand(strSQL_5, oConn);
                    MySqlDataAdapter oDA_1 = new MySqlDataAdapter(strSQL_5.ToString(), oConn);
                    oDA_1.Fill(oTable_1);

                    for(int i = 0; i < oTable_1.Rows.Count; i++)
                    {
                        MySqlCommand cmd_2 = new MySqlCommand();
                        cmd_2.Connection = oConn;
                        cmd_2.CommandText = "update produtos set quantidade = quantidade - @quantidadecomprada WHERE referencia = @cod_produto";
                        cmd_2.Parameters.AddWithValue("@quantidadecomprada", oTable_1.Rows[0]["quantidade"].ToString());
                        cmd_2.Parameters.AddWithValue("@cod_produto", oTable_1.Rows[0]["cod_produto"].ToString());


                        cmd_2.ExecuteNonQuery();

                    }

                    MySqlCommand cmd_3 = new MySqlCommand();
                    cmd_3.Connection = oConn;
                    cmd_3.CommandText = "Delete From detalhes_compra  WHERE cod_detalhe = @cod_detalhe";
                    cmd_3.Parameters.AddWithValue("@cod_detalhe", dataGridView1.CurrentRow.Cells[2].Value);
                 

                    cmd_3.ExecuteNonQuery();


                    MessageBox.Show("Fatura eliminada com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    String strSQL = "";
                    MySqlCommand oCmd = new MySqlCommand();

                    oCmd = oConn.CreateCommand();

                   

                    strSQL = ("Select cod_detalhe,compra.cod_compra,Nome,preco_sem_iva,detalhes_compra.iva,detalhes_compra.preco_final,data_compra,data_entrega,estado_pagamento FROM detalhes_compra,fornecedor,compra where detalhes_compra.cod_compra=compra.cod_compra and compra.cod_fornecedor=fornecedor.cod_fornecedor");

                    oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                    dt = new DataTable();
                    ds = new DataSet();
                    oDA.Fill(dt);
                    TotalRegistros = dt.Rows.Count;
                    oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Clientes";

                    avançareretroceder();
                    textBox1.Text = TotalRegistros.ToString();
                    string strSQL_7 = "Select count(cod_detalhe) FROM detalhes_compra";
                    DataTable oTable_3 = new DataTable();
                    MySqlCommand oCmd_8 = new MySqlCommand(strSQL_7, oConn);

                    MySqlDataReader odr_4;

                    odr_4 = oCmd_8.ExecuteReader();

                    while (odr_4.Read())
                    {
                        n_compras = odr_4.GetInt32("count(cod_detalhe)");
                    }
                    odr_4.Close();
                    label5.Text = n_compras.ToString();
                }
            }
            oConn.Close();
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

                strSQL = ("Select cod_detalhe,compra.cod_compra,Nome,preco_sem_iva,detalhes_compra.iva,detalhes_compra.preco_final,data_compra,data_entrega,estado_pagamento FROM detalhes_compra,fornecedor,compra where detalhes_compra.cod_compra=compra.cod_compra and compra.cod_fornecedor=fornecedor.cod_fornecedor");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clientes";

                string strSQL_7 = "Select count(cod_detalhe) FROM detalhes_compra";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_compras = odr_3.GetInt32("count(cod_detalhe)");
                }
                odr_3.Close();
                label5.Text = n_compras.ToString();


                dataGridView1.Columns["cod_detalhe"].DisplayIndex = 0;
                dataGridView1.Columns["Cod_Compra"].DisplayIndex = 1;
                dataGridView1.Columns["Nome"].DisplayIndex = 2;
                dataGridView1.Columns["preco_sem_iva"].DisplayIndex = 3;
                dataGridView1.Columns["iva"].DisplayIndex = 4;
                dataGridView1.Columns["preco_final"].DisplayIndex = 5;
                dataGridView1.Columns["data_compra"].DisplayIndex = 6;
                dataGridView1.Columns["data_entrega"].DisplayIndex = 7;
                dataGridView1.Columns["estado_pagamento"].DisplayIndex = 8;


                dataGridView1.Columns["Eliminar"].DisplayIndex = 9;
                dataGridView1.Columns["Pagamento"].DisplayIndex = 10;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 60;
                DataGridViewColumn column_9 = dataGridView1.Columns[1];
                column.Width = 60;
                DataGridViewColumn column_10 = dataGridView1.Columns[2];
                column.Width = 60;
                DataGridViewColumn column_11 = dataGridView1.Columns[3];
                column.Width = 50;
                DataGridViewColumn column_12 = dataGridView1.Columns[4];
                column.Width = 100;
                DataGridViewColumn column1 = dataGridView1.Columns[5];
                column1.Width = 86;
                DataGridViewColumn column2 = dataGridView1.Columns[6];
                column2.Width = 80;
                DataGridViewColumn column3 = dataGridView1.Columns[7];
                column3.Width = 64;
                DataGridViewColumn column4 = dataGridView1.Columns[8];
                column4.Width = 100;
                DataGridViewColumn column5 = dataGridView1.Columns[9];
                column5.Width = 100;
                DataGridViewColumn column6 = dataGridView1.Columns[10];
                column6.Width = 100;



                MySqlConnection oConn_1 = new MySqlConnection(strConexao);

                DataTable oTable_10 = new DataTable();



                string strSQL_1 = "Select * FROM fornecedor";
                MySqlDataAdapter oDA_10 = new MySqlDataAdapter(strSQL_1, oConn);
                oDA_10.Fill(oTable_10);
                comboBox1.DataSource = oTable_10;
                comboBox1.DisplayMember = "Nome";
                comboBox1.ValueMember = "cod_fornecedor";



                dataGridView1.Columns["cod_detalhe"].HeaderText = "Código Detalhe";
                dataGridView1.Columns["Cod_Compra"].HeaderText = "Fatura";
                dataGridView1.Columns["preco_sem_iva"].HeaderText = "Preço sem Iva";
                dataGridView1.Columns["iva"].HeaderText = "IVA";
                dataGridView1.Columns["data_compra"].HeaderText = "Data Compra";
                dataGridView1.Columns["data_entrega"].HeaderText = "Data Entrega";
                dataGridView1.Columns["estado_pagamento"].HeaderText = "Estado do Pagamento";
                dataGridView1.Columns["Nome"].HeaderText = "Fornecedor";
                dataGridView1.Columns["preco_final"].HeaderText = "Preço Final";

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
            string strConexao = ("Persist Security Info=False;server = localhost;database = pap ;uid=root");

            MySqlConnection oConn = new MySqlConnection(strConexao);
            String strSQL = "";
            MySqlCommand oCmd = new MySqlCommand();

            oCmd = oConn.CreateCommand();

            oConn.Open();

            strSQL = ("Select cod_detalhe,compra.cod_compra,Nome,preco_sem_iva,detalhes_compra.iva,detalhes_compra.preco_final,data_compra,data_entrega,estado_pagamento FROM detalhes_compra,fornecedor,compra where detalhes_compra.cod_compra=compra.cod_compra and compra.cod_fornecedor=fornecedor.cod_fornecedor and Nome='" + comboBox1.Text  + "'");

            oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
            dt = new DataTable();
            ds = new DataSet();
            oDA.Fill(dt);
            TotalRegistros = dt.Rows.Count;
            oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Clientes";
            textBox1.Text = TotalRegistros.ToString();
            avançareretroceder();
        }
    }
}
