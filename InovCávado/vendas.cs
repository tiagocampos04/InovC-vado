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
    public partial class vendas : Form
    {

        Bitmap memoryImage;
        public vendas()
        {
            InitializeComponent();

     

        }

        public double total_final2;
        public double preco_a_pagar;
        public double divida;
        public int quantidade;
        public int quantidade_comprada;
        public double total_liquido;


        MySqlConnection oConn = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");

        private void vendas_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Text = "";
                comboBox1.Text = "";
                pictureBox3.Enabled = false;
                pictureBox5.Enabled = false;

                oConn.Open();

                string strSQL = "Select max(cod_venda) FROM vendas";
                MySqlCommand cmd = new MySqlCommand(strSQL, oConn);

                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    string valor = dr[0].ToString();
                    if (valor == "")
                    {
                        label22.Text = "1";
                    }
                    else
                    {
                        int valorfatura;
                        valorfatura = int.Parse(dr[0].ToString());
                        valorfatura = valorfatura + 1;
                        label22.Text = valorfatura.ToString();
                    }
                }


                dr.Close();
                oConn.Close();

                oConn.Open();

                DataTable oTable = new DataTable();



                string strSQL_1 = "Select * FROM produtos";
                MySqlDataAdapter oDA = new MySqlDataAdapter(strSQL_1, oConn);
                oDA.Fill(oTable);
                comboBox2.DataSource = oTable;
                comboBox2.DisplayMember = "Nome_produto";
                comboBox2.ValueMember = "referencia";
                textBox3.Text = comboBox2.SelectedValue.ToString();

                oConn.Close();


                DataTable oTable_1 = new DataTable();


                oConn.Open();

                string strSQL_2 = "Select * FROM clientes";
                MySqlDataAdapter oDA_1 = new MySqlDataAdapter(strSQL_2, oConn);
                oDA_1.Fill(oTable_1);
                comboBox1.DataSource = oTable_1;
                comboBox1.DisplayMember = "Nome_empresa";
                comboBox1.ValueMember = "cod_cliente";
                textBox2.Text = comboBox1.SelectedValue.ToString();

                comboBox2.Text = "";
                comboBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
           

                DataGridViewColumn column = dataGridView2.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView2.Columns[1];
                column1.Width = 100;
                DataGridViewColumn column2 = dataGridView2.Columns[2];
                column2.Width = 100;
                DataGridViewColumn column3 = dataGridView2.Columns[3];
                column3.Width = 103;
                DataGridViewColumn column4 = dataGridView2.Columns[4];
                column4.Width = 100;
                DataGridViewColumn column5 = dataGridView2.Columns[5];
                column5.Width = 100;
                DataGridViewColumn column6 = dataGridView2.Columns[6];
                column6.Width = 100;
                DataGridViewColumn column7 = dataGridView2.Columns[7];
                column7.Width = 60;
                DataGridViewColumn column8 = dataGridView2.Columns[8];
                column8.Width = 60;

                textBox6.Text = String.Format("{0:#,##0.00}", 0d);
                textBox4.Text = String.Format("{0:#,##0.00}", 0d);
                textBox5.Text = String.Format("{0:#,##0.00}", 0d);

                label15.Text = 0.ToString("F");
                label16.Text = 0.ToString("F");
                label13.Text = 0.ToString("F");

                oConn.Close();
            }


       
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Visible = true;
            textBox2.Text = comboBox1.SelectedValue.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = comboBox2.SelectedValue.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Atualizar Produtos")
            {
                total_liquido = 0;

                dataGridView2.CurrentRow.Cells[0].Value = textBox3.Text;
                dataGridView2.CurrentRow.Cells[1].Value = comboBox2.Text;
                dataGridView2.CurrentRow.Cells[2].Value = textBox6.Text;
                dataGridView2.CurrentRow.Cells[3].Value = numericUpDown1.Value;
                dataGridView2.CurrentRow.Cells[4].Value = textBox4.Text;
                dataGridView2.CurrentRow.Cells[5].Value = textBox5.Text;
                total_liquido = 0;
                total_liquido = Convert.ToDouble(textBox6.Text) * Convert.ToDouble(numericUpDown1.Value);
                dataGridView2.CurrentRow.Cells[6].Value = total_liquido;

                total_final2 = 0;
                foreach (DataGridViewRow linha in dataGridView2.Rows)
                {
                    total_final2 += Convert.ToDouble(linha.Cells[6].Value);

                }

                label15.Text = total_final2.ToString("F");


                button2.Text = "Inserir Produto";

                textBox3.Text = "";
                comboBox2.Text = "";
                textBox6.Text = "";
                numericUpDown1.Value = 1;
                textBox4.Text = "";
                textBox5.Text = "";
                total_liquido = 0;


                DataGridViewColumn column = dataGridView2.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView2.Columns[1];
                column1.Width = 100;
                DataGridViewColumn column2 = dataGridView2.Columns[2];
                column2.Width = 100;
                DataGridViewColumn column3 = dataGridView2.Columns[3];
                column3.Width = 103;
                DataGridViewColumn column4 = dataGridView2.Columns[4];
                column4.Width = 100;
                DataGridViewColumn column5 = dataGridView2.Columns[5];
                column5.Width = 100;
                DataGridViewColumn column6 = dataGridView2.Columns[6];
                column6.Width = 100;
                DataGridViewColumn column7 = dataGridView2.Columns[7];
                column7.Width = 60;
                DataGridViewColumn column8 = dataGridView2.Columns[8];
                column8.Width = 60;


                textBox6.Text = String.Format("{0:#,##0.00}", 0d);
                textBox4.Text = String.Format("{0:#,##0.00}", 0d);
                textBox5.Text = String.Format("{0:#,##0.00}", 0d);



            }
            else
            {
                total_liquido = 0;
                total_liquido = Convert.ToDouble(textBox6.Text) * Convert.ToDouble(numericUpDown1.Value);
                dataGridView2.Rows.Add(textBox3.Text, comboBox2.Text, textBox6.Text, numericUpDown1.Value, textBox4.Text, textBox5.Text, total_liquido);


                total_final2 = 0;
                foreach (DataGridViewRow linha in dataGridView2.Rows)
                {
                    total_final2 += Convert.ToDouble(linha.Cells[6].Value);

                }

                label15.Text = total_final2.ToString("F");


                textBox3.Text = "";
                comboBox2.Text = "";
                textBox6.Text = "";
                numericUpDown1.Value = 1;
                textBox4.Text = "";
                textBox5.Text = "";
                total_liquido = 0;


                DataGridViewColumn column = dataGridView2.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView2.Columns[1];
                column1.Width = 100;
                DataGridViewColumn column2 = dataGridView2.Columns[2];
                column2.Width = 100;
                DataGridViewColumn column3 = dataGridView2.Columns[3];
                column3.Width = 103;
                DataGridViewColumn column4 = dataGridView2.Columns[4];
                column4.Width = 100;
                DataGridViewColumn column5 = dataGridView2.Columns[5];
                column5.Width = 100;
                DataGridViewColumn column6 = dataGridView2.Columns[6];
                column6.Width = 100;
                DataGridViewColumn column7 = dataGridView2.Columns[7];
                column7.Width = 60;
                DataGridViewColumn column8 = dataGridView2.Columns[8];
                column8.Width = 60;


                textBox6.Text = String.Format("{0:#,##0.00}", 0d);
                textBox4.Text = String.Format("{0:#,##0.00}", 0d);
                textBox5.Text = String.Format("{0:#,##0.00}", 0d);



            }


        }

        private void label15_TextChanged(object sender, EventArgs e)
        {
            double iva = total_final2 * 0.23;
            label16.Text = iva.ToString();
            preco_a_pagar = total_final2 + iva;
            label13.Text = preco_a_pagar.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                string data_venda = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
                string data_entrega = dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day;


                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {


                    MySqlCommand cmd_6 = new MySqlCommand();
                    cmd_6.Connection = oConn;
                    cmd_6.CommandText = "Insert into vendas(cod_venda,cod_produto,cod_cliente,preco_unitario,quantidade,desconto,iva,preco_final)values(@cod_venda,@cod_produto,@cod_cliente,@preco_unitario,@quantidade,@desconto,@iva,@preco_final)";
                    cmd_6.Parameters.AddWithValue("@cod_venda", label22.Text);
                    cmd_6.Parameters.AddWithValue("@cod_produto", dataGridView2.Rows[i].Cells[0].Value);
                    cmd_6.Parameters.AddWithValue("@cod_cliente", textBox2.Text);
                    cmd_6.Parameters.AddWithValue("@preco_unitario", dataGridView2.Rows[i].Cells[2].Value);
                    cmd_6.Parameters.AddWithValue("@quantidade", dataGridView2.Rows[i].Cells[3].Value);
                    cmd_6.Parameters.AddWithValue("@desconto", dataGridView2.Rows[i].Cells[4].Value);
                    cmd_6.Parameters.AddWithValue("@iva", dataGridView2.Rows[i].Cells[5].Value);
                    cmd_6.Parameters.AddWithValue("@preco_final", dataGridView2.Rows[i].Cells[6].Value);

                    MySqlCommand cmd_8 = new MySqlCommand();
                    cmd_8.Connection = oConn;
                    cmd_8.CommandText = "update produtos set quantidade = quantidade -  @quantidade_comprada  WHERE referencia=@cod_produto";
                    cmd_8.Parameters.AddWithValue("@quantidade_comprada", dataGridView2.Rows[i].Cells[3].Value);
                    cmd_8.Parameters.AddWithValue("@cod_produto", dataGridView2.Rows[i].Cells[0].Value);


                    oConn.Open();

                    cmd_6.ExecuteNonQuery();
                    cmd_8.ExecuteNonQuery();

                    MessageBox.Show("Venda inserida com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    oConn.Close();

                }

               

                MySqlCommand cmd_7 = new MySqlCommand();
                cmd_7.Connection = oConn;
                cmd_7.CommandText = "Insert into detalhe_venda(cod_venda,preco_sem_iva,iva,preco_final,data_venda,data_entrega)values(@cod_venda,@preco_sem_iva,@iva,@preco_final,@data_venda,@data_entrega)";
                cmd_7.Parameters.AddWithValue("@cod_venda", label22.Text);
                cmd_7.Parameters.AddWithValue("@preco_sem_iva", label15.Text);
                cmd_7.Parameters.AddWithValue("@iva", label16.Text);
                cmd_7.Parameters.AddWithValue("@preco_final", label16.Text);
                cmd_7.Parameters.AddWithValue("@data_venda", data_venda);
                cmd_7.Parameters.AddWithValue("@data_entrega", data_entrega);

                oConn.Open();

                cmd_7.ExecuteNonQuery();

                oConn.Close();


                string strSQL_2 = "Select dividas FROM clientes where cod_cliente like '" + textBox2.Text + "'";
                MySqlCommand oCmd_2 = new MySqlCommand(strSQL_2, oConn);
                MySqlDataReader odr;

                oConn.Open();

                odr = oCmd_2.ExecuteReader();

                while (odr.Read())
                {
                    divida = odr.GetDouble("dividas");
                }
                odr.Close();

                divida = divida + preco_a_pagar;


                string strSQL_3;


                strSQL_3 = ("update clientes set dividas='" + divida + "' Where cod_cliente= '" + textBox2.Text + "'");
                MySqlCommand oCmd_3 = new MySqlCommand(strSQL_3, oConn);

                oCmd_3 = oConn.CreateCommand();
                oCmd_3.CommandText = strSQL_3;

                oCmd_3.ExecuteNonQuery();


                oConn.Close();


                DataGridViewColumn column = dataGridView2.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView2.Columns[1];
                column1.Width = 100;
                DataGridViewColumn column2 = dataGridView2.Columns[2];
                column2.Width = 100;
                DataGridViewColumn column3 = dataGridView2.Columns[3];
                column3.Width = 103;
                DataGridViewColumn column4 = dataGridView2.Columns[4];
                column4.Width = 100;
                DataGridViewColumn column5 = dataGridView2.Columns[5];
                column5.Width = 100;
                DataGridViewColumn column6 = dataGridView2.Columns[6];
                column6.Width = 100;
                DataGridViewColumn column7 = dataGridView2.Columns[7];
                column7.Width = 60;
                DataGridViewColumn column8 = dataGridView2.Columns[8];
                column8.Width = 60;

                textBox6.Text = String.Format("{0:#,##0.00}", 0d);
                textBox4.Text = String.Format("{0:#,##0.00}", 0d);
                textBox5.Text = String.Format("{0:#,##0.00}", 0d);

                pictureBox3.Enabled = true;
                pictureBox5.Enabled= true;
                button1.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                oConn.Close();

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex] == dataGridView2.Columns["Editar"])
            {
                string cod_produto = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                string Nome = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                string preco_unitario = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                string quantidade = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                string desconto = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                string iva = dataGridView2.CurrentRow.Cells[5].Value.ToString();

                button2.Text = "Atualizar Produtos";

                textBox3.Text = cod_produto;
                comboBox2.Text = Nome;
                textBox6.Text = preco_unitario;
                numericUpDown1.Value = Convert.ToInt32(quantidade);
                textBox4.Text = desconto;
                textBox5.Text = iva;

                textBox3.Enabled = false;


                DataGridViewColumn column = dataGridView2.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView2.Columns[1];
                column1.Width = 100;
                DataGridViewColumn column2 = dataGridView2.Columns[2];
                column2.Width = 100;
                DataGridViewColumn column3 = dataGridView2.Columns[3];
                column3.Width = 103;
                DataGridViewColumn column4 = dataGridView2.Columns[4];
                column4.Width = 100;
                DataGridViewColumn column5 = dataGridView2.Columns[5];
                column5.Width = 100;
                DataGridViewColumn column6 = dataGridView2.Columns[6];
                column6.Width = 100;
                DataGridViewColumn column7 = dataGridView2.Columns[7];
                column7.Width = 60;
                DataGridViewColumn column8 = dataGridView2.Columns[8];
                column8.Width = 60;

            }
            else if (dataGridView2.Columns[e.ColumnIndex] == dataGridView2.Columns["Remover"])
            {
                dataGridView2.Rows.Remove(dataGridView2.CurrentRow);

                total_final2 = 0;
                foreach (DataGridViewRow linha in dataGridView2.Rows)
                {
                    total_final2 += Convert.ToDouble(linha.Cells[6].Value);

                }

                label15.Text = Convert.ToString(total_final2);


                DataGridViewColumn column = dataGridView2.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView2.Columns[1];
                column1.Width = 100;
                DataGridViewColumn column2 = dataGridView2.Columns[2];
                column2.Width = 100;
                DataGridViewColumn column3 = dataGridView2.Columns[3];
                column3.Width = 103;
                DataGridViewColumn column4 = dataGridView2.Columns[4];
                column4.Width = 100;
                DataGridViewColumn column5 = dataGridView2.Columns[5];
                column5.Width = 100;
                DataGridViewColumn column6 = dataGridView2.Columns[6];
                column6.Width = 100;
                DataGridViewColumn column7 = dataGridView2.Columns[7];
                column7.Width = 60;
                DataGridViewColumn column8 = dataGridView2.Columns[8];
                column8.Width = 60;

            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {


                string data_venda = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;

                string sql = "";
                string strConexao = ("Persist Security Info=False;server=localhost;database=pap;uid=root");
                MySqlConnection oConn = new MySqlConnection(strConexao);
                DataTable oTable = new DataTable();
                oConn.Open();
                sql = ("Select * FROM clientes Where cod_cliente = '" + textBox2.Text + "'");
                MySqlDataAdapter oDA = new MySqlDataAdapter(sql, oConn);
                oDA.Fill(oTable);


                 Bitmap bmp = Properties.Resources.download;
                Image newImage = bmp;
                e.Graphics.DrawImage(newImage,0,0, newImage.Width, newImage.Height);

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

                e.Graphics.DrawString("FATURA " + DateTime.Now.Year.ToString() + "/" + label22.Text, new Font("Helvetica", 11, FontStyle.Bold), Brushes.Black, new Point(25, 280));
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
                e.Graphics.DrawString(data_venda, new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(500, 355));
                e.Graphics.DrawString("Fatura a 30 dias", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(660, 355));

                e.Graphics.DrawString("_______________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 380));

                e.Graphics.DrawString("Referência", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(35, 410));
                e.Graphics.DrawString("Nome Produto", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(140, 410));
                e.Graphics.DrawString("Quantidade", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(280, 410));
                e.Graphics.DrawString("Preço Unitário", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(400, 410));
                e.Graphics.DrawString("IVA(%) ", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(530, 410));
                e.Graphics.DrawString("Desconto(%) ", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(590, 410));
                e.Graphics.DrawString("Total Liquido", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(680,410));

                e.Graphics.DrawString("_______________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.LightGray, new Point(25, 420));

                int num = 445;

                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[0].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(35, num));
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[1].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(140, num));
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[3].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(310, num));
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[2].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(435, num));
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[5].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(540, num));
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[4].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(625, num));
                    e.Graphics.DrawString(Convert.ToString(dataGridView2.Rows[i].Cells[6].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(715, num));

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


            

                e.Graphics.DrawString("Total S/IVA", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(530,900));
                e.Graphics.DrawString(label15.Text, new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730, 900));
                e.Graphics.DrawString("___________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.LightGray, new Point(530,901));

                e.Graphics.DrawString("Descontos", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(530, 930));
                e.Graphics.DrawString("0,00", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730,930));
                e.Graphics.DrawString("___________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.LightGray, new Point(530, 931));

                e.Graphics.DrawString("IVA", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(530, 960));
                e.Graphics.DrawString(label16.Text, new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730, 960));
                e.Graphics.DrawString("___________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.LightGray, new Point(530, 961));

                e.Graphics.DrawString("Outros Serviços", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(530, 990));
                e.Graphics.DrawString("0,00", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730, 990));
                e.Graphics.DrawString("___________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(530, 991));

                e.Graphics.DrawString("Total (EUR)", new Font("Helvetica", 12, FontStyle.Bold), Brushes.Black, new Point(530, 1020));
                e.Graphics.DrawString(label13.Text, new Font("Helvetica", 12, FontStyle.Bold), Brushes.Black, new Point(720, 1020));


                e.Graphics.DrawString("IBAN: " + "PT50 0035 0665 00009167 030 07", new Font("Helvetica", 12, FontStyle.Bold), Brushes.Black, new Point(470, 1060));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

  


        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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


        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            total_final2 = 0;
            label16.Text = 0.ToString("F");
            label16.Text = 0.ToString("F");
            label15.Text = 0.ToString("F");
            textBox6.Text = String.Format("{0:#,##0.00}", 0d);
            textBox4.Text = String.Format("{0:#,##0.00}", 0d);
            textBox5.Text = String.Format("{0:#,##0.00}", 0d);

            pictureBox3.Enabled = false;
            pictureBox5.Enabled = false;
            button1.Enabled = true;


            string strSQL = "Select max(cod_venda) FROM vendas";
            MySqlCommand cmd = new MySqlCommand(strSQL, oConn);
            oConn.Open();
            MySqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string valor = dr[0].ToString();
                if (valor == "")
                {
                    label22.Text = "1";
                }
                else
                {
                    int valorfatura;
                    valorfatura = int.Parse(dr[0].ToString());
                    valorfatura = valorfatura + 1;
                    label22.Text = valorfatura.ToString();
                }
                dr.Close();


            }
            oConn.Close();
        }
    }
}
