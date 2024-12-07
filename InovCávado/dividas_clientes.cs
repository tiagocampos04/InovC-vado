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
    public partial class dividas_clientes : Form
    {


        Bitmap memoryImage;

        public dividas_clientes()
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

        public double soma_dividas_final;


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


        private void dividas_clientes_Load(object sender, EventArgs e)
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

                strSQL = ("Select cod_cliente,Nome_empresa,Email,Telefone,dividas FROM clientes");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clientes";



                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 120;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 180;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 220;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 220;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 244;


                dataGridView1.Columns["cod_cliente"].HeaderText = "Código Cliente";
                dataGridView1.Columns["Nome_empresa"].HeaderText = "Nome Empresa";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["Telefone"].HeaderText = "Telefone";
                dataGridView1.Columns["dividas"].HeaderText = "Saldo Corrente";

                soma_dividas_final = 0;

                foreach (DataGridViewRow linha in dataGridView1.Rows)
                {
                    soma_dividas_final += Convert.ToDouble(linha.Cells[4].Value);

                }

                label5.Text = soma_dividas_final.ToString() + "€";

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

                strSQL = ("Select cod_cliente,Nome_empresa,Email,Telefone,dividas FROM clientes");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clientes";



                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 120;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 180;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 220;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 220;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 244;


                dataGridView1.Columns["cod_cliente"].HeaderText = "Código Cliente";
                dataGridView1.Columns["Nome_empresa"].HeaderText = "Nome Empresa";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["Telefone"].HeaderText = "Telefone";
                dataGridView1.Columns["dividas"].HeaderText = "Saldo Corrente";

                soma_dividas_final = 0;

                foreach (DataGridViewRow linha in dataGridView1.Rows)
                {
                    soma_dividas_final += Convert.ToDouble(linha.Cells[4].Value);

                }

                label5.Text = soma_dividas_final.ToString() + "€";


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

                strSQL = ("Select cod_cliente,Nome_empresa,Email,Telefone,dividas FROM clientes where Nome_empresa like '%" + textBox2.Text + "%'");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clientes";



                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 120;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 180;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 220;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 220;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 244;


                dataGridView1.Columns["cod_cliente"].HeaderText = "Código Cliente";
                dataGridView1.Columns["Nome_empresa"].HeaderText = "Nome Empresa";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["Telefone"].HeaderText = "Telefone";
                dataGridView1.Columns["dividas"].HeaderText = "Saldo Corrente";

                soma_dividas_final = 0;

                foreach (DataGridViewRow linha in dataGridView1.Rows)
                {
                    soma_dividas_final += Convert.ToDouble(linha.Cells[4].Value);

                }

                label5.Text = soma_dividas_final.ToString() + "€";


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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawString("INOVCÁVADO-EQ. IND. E DE PROTEÇÃO, UNIPESSOAL, LDA", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(25, 5));


            e.Graphics.DrawString("______________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 10));

            e.Graphics.DrawString("Análise dos Devedores", new Font("Helvetica", 13, FontStyle.Bold), Brushes.Black, new Point(350, 35));

            e.Graphics.DrawString("______________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 50));

            e.Graphics.DrawString("Código Cliente", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(35, 75));
            e.Graphics.DrawString("Nome Empresa", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(150, 75));
            e.Graphics.DrawString("Email", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(320, 75));
            e.Graphics.DrawString("Telefone", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(520, 75));
            e.Graphics.DrawString("Saldo Corrente ", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(690, 75));



            e.Graphics.DrawString("______________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 90));

            int num = 115;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(35, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[1].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(150, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[2].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(320, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[3].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(520, num));
                e.Graphics.DrawString(Convert.ToString(dataGridView1.Rows[i].Cells[4].Value), new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(740, num));
                e.Graphics.DrawString("______________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.LightGray, new Point(25, num + 5));


                num = num + 30;
                e.HasMorePages = false;
            }

            label1.Text = DateTime.Now.ToString("HH:mm:ss");
            label2.Text = DateTime.Now.ToLongDateString();
            e.Graphics.DrawString("______________________________________________________________________________________", new Font("Helvetica", 12, FontStyle.Regular), Brushes.Black, new Point(25, 1000));
            e.Graphics.DrawString("Total:", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(25, 1020));
            e.Graphics.DrawString(Convert.ToString(soma_dividas_final) + " €", new Font("Helvetica", 9, FontStyle.Regular), Brushes.Black, new Point(740, 1020));
            e.Graphics.DrawString("Emitido em: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("HH:mm:ss"), new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(25, 1100));
            e.Graphics.DrawString("Empresa: " + "INOVCÁVADO-EQ. IND. E DE PROTEÇÃO, UNIPESSOAL, LDA", new Font("Helvetica", 9, FontStyle.Bold), Brushes.Black, new Point(25, 1120));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        
            printDocument1.DocumentName = "Saldo Clientes " + DateTime.Now.Year.ToString();
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
