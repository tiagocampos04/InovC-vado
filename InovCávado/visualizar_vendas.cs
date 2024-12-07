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
    public partial class visualizar_vendas : Form
    {
        public visualizar_vendas()
        {
            InitializeComponent();
        }

        Bitmap memoryImage;

        MySqlDataAdapter oDA;

        DataSet ds;

        DataTable dt;

        int tamanhoPagina = 6;

        int inicio = 0;

        int TotalRegistros;

        int pagatual = 1;

        int n_compras;

        int cod_cliente;
        string data_venda;


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

        private void visualizar_vendas_Load(object sender, EventArgs e)
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

                strSQL = ("Select cod_detalhe,vendas.cod_venda,Nome_empresa,preco_sem_iva,detalhe_venda.iva,detalhe_venda.preco_final,data_venda,data_entrega,estado_de_pagamento FROM detalhe_venda,clientes,vendas where detalhe_venda.cod_venda=vendas.cod_venda and vendas.cod_cliente=clientes.cod_cliente");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clientes";

                string strSQL_7 = "Select count(cod_detalhe) FROM detalhe_venda";
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
                dataGridView1.Columns["cod_venda"].DisplayIndex = 1;
                dataGridView1.Columns["Nome_empresa"].DisplayIndex = 2;
                dataGridView1.Columns["preco_sem_iva"].DisplayIndex = 3;
                dataGridView1.Columns["iva"].DisplayIndex = 4;
                dataGridView1.Columns["preco_final"].DisplayIndex = 5;
                dataGridView1.Columns["data_venda"].DisplayIndex = 6;
                dataGridView1.Columns["data_entrega"].DisplayIndex = 7;
                dataGridView1.Columns["estado_de_pagamento"].DisplayIndex = 8;



                dataGridView1.Columns["Eliminar"].DisplayIndex = 9;
                dataGridView1.Columns["Pagamento"].DisplayIndex = 10;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 60;
                DataGridViewColumn column_9 = dataGridView1.Columns[1];
                column.Width = 60;
                DataGridViewColumn column_10 = dataGridView1.Columns[2];
                column.Width = 60;
                DataGridViewColumn column_11 = dataGridView1.Columns[3];
                column.Width = 60;
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



                dataGridView1.Columns["cod_detalhe"].HeaderText = "Código Detalhe";
                dataGridView1.Columns["cod_venda"].HeaderText = "Fatura";
                dataGridView1.Columns["preco_sem_iva"].HeaderText = "Preço sem Iva";
                dataGridView1.Columns["iva"].HeaderText = "IVA";
                dataGridView1.Columns["data_venda"].HeaderText = "Data Compra";
                dataGridView1.Columns["data_entrega"].HeaderText = "Data Entrega";
                dataGridView1.Columns["estado_de_pagamento"].HeaderText = "Estado do Pagamento";
                dataGridView1.Columns["Nome_empresa"].HeaderText = "Cliente";
                dataGridView1.Columns["preco_final"].HeaderText = "Preço Final";



                MySqlConnection oConn_1 = new MySqlConnection(strConexao);

                DataTable oTable_10 = new DataTable();



                string strSQL_1 = "Select * FROM clientes";
                MySqlDataAdapter oDA_10 = new MySqlDataAdapter(strSQL_1, oConn);
                oDA_10.Fill(oTable_10);
                comboBox1.DataSource = oTable_10;
                comboBox1.DisplayMember = "Nome_emresa";
                comboBox1.ValueMember = "Nome_empresa";

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
                if (dataGridView1.CurrentRow.Cells[12].Value.ToString() == "Pago")
                {

                    MessageBox.Show("Esta Fatura já se encontra paga", " ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    string pago = "Pago";
                  
                    String strSQL = "";
                    MySqlCommand oCmd = new MySqlCommand();

                    oCmd = oConn.CreateCommand();

                   
                    strSQL = ("update detalhe_venda set estado_de_pagamento='" + pago + "' Where cod_detalhe=" + dataGridView1.CurrentRow.Cells[4].Value.ToString() + "");

                    oConn.Open();

                    oCmd.CommandText = strSQL;

                    oCmd.ExecuteNonQuery();

                    MySqlDataReader odr_3;

             
                    string strSQL_4 = "Select cod_cliente FROM clientes where Nome_empresa = '" + dataGridView1.CurrentRow.Cells[6].Value + "'";
                    DataTable oTable = new DataTable();
                    MySqlCommand oCmd_6 = new MySqlCommand(strSQL_4, oConn);
                    MySqlDataAdapter oDA = new MySqlDataAdapter(strSQL_4.ToString(), oConn);
                    oDA.Fill(oTable);

                    odr_3 = oCmd_6.ExecuteReader();

                    while (odr_3.Read())
                    {
                        cod_cliente = odr_3.GetInt32("cod_cliente");
                    }
                    odr_3.Close();

                    MySqlCommand cmd_1 = new MySqlCommand();
                    cmd_1.Connection = oConn;
                    cmd_1.CommandText = "update clientes set dividas = dividas - @valorpago WHERE cod_cliente = @cod_cliente";
                    cmd_1.Parameters.AddWithValue("@valorpago", dataGridView1.CurrentRow.Cells[9].Value);
                    cmd_1.Parameters.AddWithValue("@cod_cliente", cod_cliente);

                    cmd_1.ExecuteNonQuery();


                    MessageBox.Show("Pagamento confirmado com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    strSQL = ("Select cod_detalhe,vendas.cod_venda,Nome_empresa,preco_sem_iva,detalhe_venda.iva,detalhe_venda.preco_final,data_venda,data_entrega,estado_de_pagamento FROM detalhe_venda,clientes,vendas where detalhe_venda.cod_venda=vendas.cod_venda and vendas.cod_cliente=clientes.cod_cliente");

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


                    string strSQL_7 = "Select count(cod_detalhe) FROM detalhe_venda";
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
                if (dataGridView1.CurrentRow.Cells[12].Value.ToString() == "Pago")
                {

                    MessageBox.Show("Este Fatura já se encontra paga logo não a pode eliminar", " ", MessageBoxButtons.OK, MessageBoxIcon.Hand);


                }
                else
                {
                    MySqlDataReader odr_3;


                    string strSQL_4 = "Select cod_cliente FROM clientes where Nome_empresa = '" + dataGridView1.CurrentRow.Cells[6].Value + "'";
                    DataTable oTable = new DataTable();
                    MySqlCommand oCmd_6 = new MySqlCommand(strSQL_4, oConn);
                    MySqlDataAdapter oDA = new MySqlDataAdapter(strSQL_4.ToString(), oConn);
                    oDA.Fill(oTable);

                    odr_3 = oCmd_6.ExecuteReader();

                    while (odr_3.Read())
                    {
                        cod_cliente = odr_3.GetInt32("cod_cliente");
                    }
                    odr_3.Close();

                    MySqlCommand cmd_1 = new MySqlCommand();
                    cmd_1.Connection = oConn;
                    cmd_1.CommandText = "update clientes set dividas = dividas - @valorpago WHERE cod_cliente = @cod_cliente";
                    cmd_1.Parameters.AddWithValue("@valorpago", dataGridView1.CurrentRow.Cells[9].Value);
                    cmd_1.Parameters.AddWithValue("@cod_cliente", cod_cliente);

                    cmd_1.ExecuteNonQuery();



                    string strSQL_5 = "Select cod_produto,quantidade FROM vendas,detalhe_venda where detalhe_venda.cod_venda=vendas.cod_venda and vendas.cod_venda = '" + dataGridView1.CurrentRow.Cells[5].Value + "'";
                    DataTable oTable_1 = new DataTable();
                    MySqlCommand oCmd_7 = new MySqlCommand(strSQL_5, oConn);
                    MySqlDataAdapter oDA_1 = new MySqlDataAdapter(strSQL_5.ToString(), oConn);
                    oDA_1.Fill(oTable_1);

                    for (int i = 0; i < oTable_1.Rows.Count; i++)
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
                    cmd_3.CommandText = "Delete From detalhe_venda  WHERE cod_detalhe = @cod_detalhe";
                    cmd_3.Parameters.AddWithValue("@cod_detalhe", dataGridView1.CurrentRow.Cells[4].Value);


                    cmd_3.ExecuteNonQuery();


                    MessageBox.Show("Fatura eliminada com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    String strSQL = "";
                    MySqlCommand oCmd = new MySqlCommand();

                    oCmd = oConn.CreateCommand();



                    strSQL = ("Select cod_detalhe,vendas.cod_venda,Nome_empresa,preco_sem_iva,detalhe_venda.iva,detalhe_venda.preco_final,data_venda,data_entrega,estado_de_pagamento FROM detalhe_venda,clientes,vendas where detalhe_venda.cod_venda=vendas.cod_venda and vendas.cod_cliente=clientes.cod_cliente");

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
                    string strSQL_7 = "Select count(cod_detalhe) FROM detalhe_venda";
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
            else if (dataGridView1.Columns[e.ColumnIndex] == dataGridView1.Columns["preview"])
            {
                Graphics myGraphics = this.CreateGraphics();
                Size s = this.Size;
                memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
                Graphics memoryGraphics = Graphics.FromImage(memoryImage);
                memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else if (dataGridView1.Columns[e.ColumnIndex] == dataGridView1.Columns["Imprimir"])
            {
                printDocument1.DocumentName = "Stock " + DateTime.Now.Year.ToString();
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.Document = printDocument1;
                DialogResult result = printDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            oConn.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            MySqlConnection con = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");


            con.Open();
            string strSQL_4 = "Select cod_cliente FROM clientes where Nome_empresa = '" + dataGridView1.CurrentRow.Cells[6].Value + "'";
            DataTable oTable_2 = new DataTable();
            MySqlCommand oCmd_6 = new MySqlCommand(strSQL_4, con);
            MySqlDataAdapter oDA_2 = new MySqlDataAdapter(strSQL_4.ToString(), con);
            oDA_2.Fill(oTable_2);
            MySqlDataReader odr_5;
            odr_5 = oCmd_6.ExecuteReader();

            while (odr_5.Read())
            {
                cod_cliente = odr_5.GetInt32("cod_cliente");
            }
            odr_5.Close();


            string strSQL_5 = "Select data_venda FROM detalhe_venda where cod_detalhe = '" + dataGridView1.CurrentRow.Cells[4].Value + "'";
            DataTable oTable_3 = new DataTable();
            MySqlCommand oCmd_7 = new MySqlCommand(strSQL_5, con);
            MySqlDataAdapter oDA_3 = new MySqlDataAdapter(strSQL_5.ToString(), con);
            oDA_3.Fill(oTable_3);
          
            data_venda = oTable_3.Rows[0]["data_venda"].ToString();


            string sql = "";
            string strConexao = ("Persist Security Info=False;server=localhost;database=pap;uid=root");
            MySqlConnection oConn = new MySqlConnection(strConexao);
            DataTable oTable = new DataTable();
            oConn.Open();
            sql = ("Select * FROM clientes Where cod_cliente = '" + cod_cliente + "'");
            MySqlDataAdapter oDA = new MySqlDataAdapter(sql, oConn);
            oDA.Fill(oTable);

            string sql_1 = "";
            string strConexao_1 = ("Persist Security Info=False;server=localhost;database=pap;uid=root");
            MySqlConnection oConn_1 = new MySqlConnection(strConexao_1);
            DataTable oTable_1 = new DataTable();
            oConn_1.Open();
            sql_1 = ("Select referencia,Nome_produto,vendas.quantidade,preco_unitario,iva,desconto,preco_final FROM vendas,produtos Where vendas.cod_produto=produtos.referencia and cod_venda = '" + dataGridView1.CurrentRow.Cells[5].Value + "'");
            MySqlDataAdapter oDA_1 = new MySqlDataAdapter(sql_1, oConn_1);
            oDA_1.Fill(oTable_1);


            Bitmap bmp = Properties.Resources.download;
            Image newImage = bmp;
            e.Graphics.DrawImage(newImage, 0, 0, newImage.Width, newImage.Height);

            e.Graphics.DrawString("INOVCÁVADO-EQ.IND.E DE PROTEÇÃO,UNIPESSOAL,LDA", new Font("Helvetica", 12, FontStyle.Bold), Brushes.Black, new Point(25, 150));
            e.Graphics.DrawString("Rua João Francisco da Silva Novais nº61", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(25, 170));
            e.Graphics.DrawString("Macieira de Rates", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(25, 190));
            e.Graphics.DrawString("4755-270 Macieira de Rates", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(25, 210));
            e.Graphics.DrawString("Telefone: 924015280", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(25, 230));
            e.Graphics.DrawString("Capital Social: 5 000,00 €", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(25, 250));


            e.Graphics.DrawString("Exmo.(s) Sr.(s)", new Font("Helvetica", 11, FontStyle.Bold), Brushes.Black, new Point(450, 180));
            e.Graphics.DrawString(oTable.Rows[0]["Nome_empresa"].ToString(), new Font("Helvetica", 11, FontStyle.Regular), Brushes.Black, new Point(450, 200));
            e.Graphics.DrawString(oTable.Rows[0]["Morada"].ToString(), new Font("Helvetica", 11, FontStyle.Regular), Brushes.Black, new Point(450, 220));
            e.Graphics.DrawString(oTable.Rows[0]["cod_postal"].ToString() + " " + oTable.Rows[0]["Localidade"].ToString(), new Font("Helvetica", 11, FontStyle.Regular), Brushes.Black, new Point(450, 240));

            e.Graphics.DrawString("FATURA " + DateTime.Now.Year.ToString() + "/" + dataGridView1.CurrentRow.Cells[6].Value, new Font("Helvetica", 11, FontStyle.Bold), Brushes.Black, new Point(25, 280));
            e.Graphics.DrawString("_______________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 285));
            e.Graphics.DrawString("Nº Contribuinte", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(35, 320));
            e.Graphics.DrawString("Cliente Nº", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(180, 320));
            e.Graphics.DrawString("Moeda", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(280, 320));
            e.Graphics.DrawString("Câmbio", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(380, 320));
            e.Graphics.DrawString("Data Encomenda", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(480, 320));
            e.Graphics.DrawString("Condição Pagamento", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(640, 320));

            e.Graphics.DrawString("_______________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Italic), Brushes.LightGray, new Point(25, 330));

            e.Graphics.DrawString(oTable.Rows[0]["NIF"].ToString(), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(45, 355));
            e.Graphics.DrawString(oTable.Rows[0]["cod_cliente"].ToString(), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(200, 355));
            e.Graphics.DrawString("EUR", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(285, 355));
            e.Graphics.DrawString("1,00", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(390, 355));
            e.Graphics.DrawString("28/06/2018",new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(500, 355));
            e.Graphics.DrawString("Fatura a 30 dias", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(660, 355));

            e.Graphics.DrawString("_______________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 380));

            e.Graphics.DrawString("Referência", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(35, 410));
            e.Graphics.DrawString("Nome Produto", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(140, 410));
            e.Graphics.DrawString("Quantidade", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(280, 410));
            e.Graphics.DrawString("Preço Unitário", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(400, 410));
            e.Graphics.DrawString("IVA(%) ", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(530, 410));
            e.Graphics.DrawString("Desconto(%) ", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(590, 410));
            e.Graphics.DrawString("Total Liquido", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(680, 410));

            e.Graphics.DrawString("_______________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.LightGray, new Point(25, 420));

            int num = 445;

            for (int i = 0; i < oTable_1.Rows.Count; i++)
            {
                e.Graphics.DrawString(Convert.ToString(oTable_1.Rows[i]["referencia"].ToString()), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(35, num));
                e.Graphics.DrawString(Convert.ToString(oTable_1.Rows[i]["Nome_produto"].ToString()), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(140, num));
                e.Graphics.DrawString(Convert.ToString(oTable_1.Rows[i]["quantidade"].ToString()), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(310, num));
                e.Graphics.DrawString(Convert.ToString(oTable_1.Rows[i]["preco_unitario"].ToString()), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(435, num));
                e.Graphics.DrawString(Convert.ToString(oTable_1.Rows[i]["iva"].ToString()), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(540, num));
                e.Graphics.DrawString(Convert.ToString(oTable_1.Rows[i]["desconto"].ToString()), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(625, num));
                e.Graphics.DrawString(Convert.ToString(oTable_1.Rows[i]["preco_final"].ToString()), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(715, num));

                num = num + 20;
                e.HasMorePages = false;
            }
            e.Graphics.DrawString("________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 850));

            e.Graphics.DrawString("Local de Carga", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(25, 900));
            e.Graphics.DrawString("_____________________________", new Font("Helvetica", 10, FontStyle.Regular), Brushes.LightGray, new Point(25, 905));
            e.Graphics.DrawString("Rua João Francisco da Silva Novais nº61", new Font("Helvetica", 7, FontStyle.Regular), Brushes.Black, new Point(25, 930));
            e.Graphics.DrawString("Macieira de Rates", new Font("Helvetica", 7, FontStyle.Regular), Brushes.Black, new Point(25, 955));
            e.Graphics.DrawString("4755-270 Macieira de Rates", new Font("Helvetica", 7, FontStyle.Regular), Brushes.Black, new Point(25, 980));

            e.Graphics.DrawString("Local de Descarga", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(280, 900));
            e.Graphics.DrawString("_____________________________", new Font("Helvetica", 10, FontStyle.Regular), Brushes.LightGray, new Point(280, 905));
            e.Graphics.DrawString(oTable.Rows[0]["Morada"].ToString(), new Font("Helvetica", 7, FontStyle.Regular), Brushes.Black, new Point(280, 930));
            e.Graphics.DrawString(oTable.Rows[0]["Localidade"].ToString(), new Font("Helvetica", 7, FontStyle.Regular), Brushes.Black, new Point(280, 955));
            e.Graphics.DrawString(oTable.Rows[0]["cod_postal"].ToString() + " " + oTable.Rows[0]["Localidade"].ToString(), new Font("Helvetica", 7, FontStyle.Regular), Brushes.Black, new Point(280, 980));




            e.Graphics.DrawString("Total S/IVA", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(530, 900));
            e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[8].Value.ToString(), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730, 900));
            e.Graphics.DrawString("___________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.LightGray, new Point(530, 901));

            e.Graphics.DrawString("Descontos", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(530, 930));
            e.Graphics.DrawString("0,00", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730, 930));
            e.Graphics.DrawString("___________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.LightGray, new Point(530, 931));

            e.Graphics.DrawString("IVA", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(530, 960));
            e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[9].Value.ToString(), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730, 960));
            e.Graphics.DrawString("___________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.LightGray, new Point(530, 961));

            e.Graphics.DrawString("Outros Serviços", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(530, 990));
            e.Graphics.DrawString("0,00", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730, 990));
            e.Graphics.DrawString("___________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(530, 991));

            e.Graphics.DrawString("Total (EUR)", new Font("Helvetica", 12, FontStyle.Bold), Brushes.Black, new Point(530, 1020));
            e.Graphics.DrawString(dataGridView1.CurrentRow.Cells[9].Value.ToString(), new Font("Helvetica", 12, FontStyle.Bold), Brushes.Black, new Point(720, 1020));


            e.Graphics.DrawString("IBAN: " + "PT50 0035 0665 00009167 030 07", new Font("Helvetica", 12, FontStyle.Bold), Brushes.Black, new Point(470, 1060));

            con.Close();
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

                strSQL = ("Select cod_detalhe,vendas.cod_venda,Nome_empresa,preco_sem_iva,detalhe_venda.iva,detalhe_venda.preco_final,data_venda,data_entrega,estado_de_pagamento FROM detalhe_venda,clientes,vendas where detalhe_venda.cod_venda=vendas.cod_venda and vendas.cod_cliente=clientes.cod_cliente");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clientes";

                string strSQL_7 = "Select count(cod_detalhe) FROM detalhe_venda";
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
                dataGridView1.Columns["cod_venda"].DisplayIndex = 1;
                dataGridView1.Columns["Nome_empresa"].DisplayIndex = 2;
                dataGridView1.Columns["preco_sem_iva"].DisplayIndex = 3;
                dataGridView1.Columns["iva"].DisplayIndex = 4;
                dataGridView1.Columns["preco_final"].DisplayIndex = 5;
                dataGridView1.Columns["data_venda"].DisplayIndex = 6;
                dataGridView1.Columns["data_entrega"].DisplayIndex = 7;
                dataGridView1.Columns["estado_de_pagamento"].DisplayIndex = 8;



                dataGridView1.Columns["Eliminar"].DisplayIndex = 10;
                dataGridView1.Columns["Pagamento"].DisplayIndex = 11;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 60;
                DataGridViewColumn column_9 = dataGridView1.Columns[1];
                column.Width = 60;
                DataGridViewColumn column_10 = dataGridView1.Columns[2];
                column.Width = 60;
                DataGridViewColumn column_11 = dataGridView1.Columns[3];
                column.Width = 60;
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



                dataGridView1.Columns["cod_detalhe"].HeaderText = "Código Detalhe";
                dataGridView1.Columns["cod_venda"].HeaderText = "Fatura";
                dataGridView1.Columns["preco_sem_iva"].HeaderText = "Preço sem Iva";
                dataGridView1.Columns["iva"].HeaderText = "IVA";
                dataGridView1.Columns["data_venda"].HeaderText = "Data Compra";
                dataGridView1.Columns["data_entrega"].HeaderText = "Data Entrega";
                dataGridView1.Columns["estado_de_pagamento"].HeaderText = "Estado do Pagamento";
                dataGridView1.Columns["Nome_empresa"].HeaderText = "Cliente";
                dataGridView1.Columns["preco_final"].HeaderText = "Preço Final";



                MySqlConnection oConn_1 = new MySqlConnection(strConexao);

                DataTable oTable_10 = new DataTable();



                string strSQL_1 = "Select * FROM clientes";
                MySqlDataAdapter oDA_10 = new MySqlDataAdapter(strSQL_1, oConn);
                oDA_10.Fill(oTable_10);
                comboBox1.DataSource = oTable_10;
                comboBox1.DisplayMember = "Nome_emresa";
                comboBox1.ValueMember = "cod_cliente";

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

            strSQL = ("Select cod_detalhe,vendas.cod_venda,Nome_empresa,preco_sem_iva,detalhe_venda.iva,detalhe_venda.preco_final,data_venda,data_entrega,estado_de_pagamento FROM detalhe_venda,clientes,vendas where detalhe_venda.cod_venda=vendas.cod_venda and vendas.cod_cliente=clientes.cod_cliente and Nome_empresa='" + comboBox1.Text + "'");

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
    


