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
    public partial class stock : Form
    {


        Bitmap memoryImage;

        public stock()
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

        public double valor_stock;
        public double preco_stock_final;


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
        private void stock_Load(object sender, EventArgs e)
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

                strSQL = ("Select referencia,Nome_produto,Descricao,quantidade,pc_max,pc_min,pc_med,valor_stock FROM produtos");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Produtos");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Produtos";

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 150;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 150;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 80;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 130;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 130;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 130;
                DataGridViewColumn column7 = dataGridView1.Columns[7];
                column7.Width = 134;


                dataGridView1.Columns["referencia"].HeaderText = "Referência";
                dataGridView1.Columns["Nome_produto"].HeaderText = "Nome Produto";
                dataGridView1.Columns["Descricao"].HeaderText = "Descriçao";
                dataGridView1.Columns["quantidade"].HeaderText = "Stock";
                dataGridView1.Columns["pc_max"].HeaderText = "Preço Compra Máximo";
                dataGridView1.Columns["pc_min"].HeaderText = "Preço Compra Mínimo";
                dataGridView1.Columns["pc_med"].HeaderText = "Preço Compra Médio";
                dataGridView1.Columns["valor_stock"].HeaderText = "Valor Stock";



                preco_stock_final = 0;

                foreach (DataGridViewRow linha in dataGridView1.Rows)
                {
                    preco_stock_final += Convert.ToDouble(linha.Cells[7].Value);

                }

             

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

                strSQL = ("Select referencia,Nome_produto,Descricao,quantidade,pc_max,pc_min,pc_med,valor_stock FROM produtos");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Produtos");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Produtos";

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 150;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 150;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 80;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 130;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 130;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 130;
                DataGridViewColumn column7 = dataGridView1.Columns[7];
                column7.Width = 134;


                dataGridView1.Columns["referencia"].HeaderText = "Referência";
                dataGridView1.Columns["Nome_produto"].HeaderText = "Nome Produto";
                dataGridView1.Columns["Descricao"].HeaderText = "Descriçao";
                dataGridView1.Columns["quantidade"].HeaderText = "Stock";
                dataGridView1.Columns["pc_max"].HeaderText = "Preço Compra Máximo";
                dataGridView1.Columns["pc_min"].HeaderText = "Preço Compra Mínimo";
                dataGridView1.Columns["pc_med"].HeaderText = "Preço Compra Médio";
                dataGridView1.Columns["valor_stock"].HeaderText = "Valor Stock";

                preco_stock_final = 0;

                foreach (DataGridViewRow linha in dataGridView1.Rows)
                {
                    preco_stock_final += Convert.ToDouble(linha.Cells[7].Value);

                }

           

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

                strSQL = ("Select referencia,Nome_produto,Descricao,quantidade,pc_max,pc_min,pc_med,valor_stock FROM produtos where Nome_produto like '%" + textBox2.Text + "%'");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Produtos");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Produtos";


                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 150;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 150;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 80;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 130;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 130;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 130;
                DataGridViewColumn column7 = dataGridView1.Columns[7];
                column7.Width = 134;



                dataGridView1.Columns["referencia"].HeaderText = "Referência";
                dataGridView1.Columns["Nome_produto"].HeaderText = "Nome Produto";
                dataGridView1.Columns["Descricao"].HeaderText = "Descriçao";
                dataGridView1.Columns["quantidade"].HeaderText = "Stock";
                dataGridView1.Columns["pc_max"].HeaderText = "Preço Compra Máximo";
                dataGridView1.Columns["pc_min"].HeaderText = "Preço Compra Mínimo";
                dataGridView1.Columns["pc_med"].HeaderText = "Preço Compra Médio";
                dataGridView1.Columns["valor_stock"].HeaderText = "Valor Stock";


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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        { 

            if (e.ColumnIndex==3)
            {
                int quantidade = Convert.ToInt32( e.Value);

                if (quantidade <= 10)
                {
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.BackColor= Color.IndianRed;
                }
                else if ((quantidade > 10) && (quantidade <=20))
                {
                   e.CellStyle.BackColor= Color.DarkOrange;
                    e.CellStyle.ForeColor = Color.Black;

                }
                else if (quantidade > 20)
                {
                    e.CellStyle.BackColor = Color.ForestGreen;
                    e.CellStyle.ForeColor = Color.Black;

                }
              
            }
            

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            preco_stock_final = 0;
            
            foreach (DataGridViewRow linha in dataGridView1.Rows)
            {
                preco_stock_final += Convert.ToDouble( linha.Cells[7].Value);

            }



            e.Graphics.DrawString("INOVCÁVADO-EQ. IND. E DE PROTEÇÃO, UNIPESSOAL, LDA", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(25, 5));


            e.Graphics.DrawString("______________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25,10));

            e.Graphics.DrawString("Análise de Stock", new Font("Helvetica", 13, FontStyle.Bold), Brushes.Black, new Point(350, 35));

            e.Graphics.DrawString("______________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 50));

            e.Graphics.DrawString("Referência", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(35, 75));
            e.Graphics.DrawString("Nome Produto", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(140, 75));
            e.Graphics.DrawString("Quantidade", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(280, 75));
            e.Graphics.DrawString("PC Máximo", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(410, 75));
            e.Graphics.DrawString("PC Mínimo", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(530, 75));
            e.Graphics.DrawString("PC Médio ", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(640, 75));
            e.Graphics.DrawString("Valor Stock", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(730,75));


            e.Graphics.DrawString("______________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 90));

            int num = 115;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(35, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(140, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[3].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(310, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(430, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[5].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(560, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[6].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(640, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[7].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730, num));

                num = num + 20;
                e.HasMorePages = false;
            }

            label1.Text = DateTime.Now.ToString("HH:mm:ss");
            label2.Text = DateTime.Now.ToLongDateString();
            e.Graphics.DrawString("______________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 1000));
            e.Graphics.DrawString("Total:", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(25, 1020));
            e.Graphics.DrawString(Convert.ToString(preco_stock_final) + " €", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(730,1020));
            e.Graphics.DrawString("Emitido em: " + DateTime.Now.ToLongDateString()  + " " + DateTime.Now.ToString("HH:mm:ss"), new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(25, 1100));
            e.Graphics.DrawString("Empresa: " + "INOVCÁVADO-EQ. IND. E DE PROTEÇÃO, UNIPESSOAL, LDA", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(25, 1120));
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
    }
}
