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
    public partial class clientes : Form
    {
        public int n_cliente;

        MySqlDataAdapter oDA ;

        DataSet  ds ;

        DataTable dt;

        int tamanhoPagina = 6;

        int inicio=0;

        int TotalRegistros;

        int pagatual=1;

     

        MySqlConnection oConn = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");

        public clientes()
        {
            InitializeComponent();

        }

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



        public void clientes_Load(object sender, EventArgs e)
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
              

                strSQL = ("Select cod_cliente,Nome_empresa,Morada,Localidade,cod_postal,Telefone,Email,NIF FROM clientes");
            
                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt= new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clientes";

                string strSQL_7 = "Select count(cod_cliente) FROM clientes";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);
            
                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_cliente = odr_3.GetInt32("count(cod_cliente)");
                }
                odr_3.Close();
                label5.Text= n_cliente.ToString();

                
                dataGridView1.Columns["Editar"].DisplayIndex = 8;
              

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 80;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 138;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 183;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 130;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 81;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 81;
                DataGridViewColumn column7 = dataGridView1.Columns[7];
                column7.Width = 160;
                DataGridViewColumn column8 = dataGridView1.Columns[8];
                column8.Width = 81;


                dataGridView1.Columns["cod_cliente"].HeaderText = "Código Cliente";
                dataGridView1.Columns["Nome_empresa"].HeaderText = "Nome Empresa";
                dataGridView1.Columns["Morada"].HeaderText = "Morada";
                dataGridView1.Columns["Localidade"].HeaderText = "Localidade";
                dataGridView1.Columns["cod_postal"].HeaderText = "Código Postal";
                dataGridView1.Columns["Telefone"].HeaderText = "Telefone";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["NIF"].HeaderText = "NIF";
      

                oConn.Close();

                 textBox1.Text = TotalRegistros.ToString();
                avançareretroceder();
                 label1.Text= "Página " + pagatual.ToString();

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
                else if(inicio>0)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Columns[e.ColumnIndex]==dataGridView1.Columns["Editar"])
            {
                string cod_cliente = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string Nome_empresa = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string Morada = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string Localidade = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                string cod_postal = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                string Telefone = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                string Email = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                string NIF = dataGridView1.CurrentRow.Cells[8].Value.ToString();


                atualizar_clientes ac = new atualizar_clientes();
                ac.Show();
             

               ac.textBox5.Text = cod_cliente; 
               ac.textBox2.Text=Nome_empresa;
               ac.textBox1.Text=Morada;
               ac.maskedTextBox1.Text = cod_postal;
               ac.textBox3.Text=Localidade;
               ac.maskedTextBox2.Text=Telefone;
               ac.textBox4.Text = Email;
               ac.maskedTextBox3.Text = NIF;


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

                    strSQL = ("Select cod_cliente,Nome_empresa,Morada,Localidade,cod_postal,Telefone,Email,NIF FROM clientes where Nome_empresa like '%" + textBox2.Text + "%'  ");

                    oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                    dt = new DataTable();
                    ds = new DataSet();
                    oDA.Fill(dt);
                    TotalRegistros = dt.Rows.Count;
                    oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                    dataGridView1.DataSource = ds;
                    dataGridView1.DataMember = "Clientes";


                    dataGridView1.Columns["Editar"].DisplayIndex = 8;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 80;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 138;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 183;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 130;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 81;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 81;
                DataGridViewColumn column7 = dataGridView1.Columns[7];
                column7.Width = 160;
                DataGridViewColumn column8 = dataGridView1.Columns[8];
                column8.Width = 81;


                dataGridView1.Columns["cod_cliente"].HeaderText = "Código Cliente";
                    dataGridView1.Columns["Nome_empresa"].HeaderText = "Nome Empresa";
                    dataGridView1.Columns["Morada"].HeaderText = "Morada";
                    dataGridView1.Columns["Localidade"].HeaderText = "Localidade";
                    dataGridView1.Columns["cod_postal"].HeaderText = "Código Postal";
                    dataGridView1.Columns["Telefone"].HeaderText = "Telefone";
                    dataGridView1.Columns["Email"].HeaderText = "Email";
                    dataGridView1.Columns["NIF"].HeaderText = "NIF";

                    string strSQL_7 = "Select count(cod_cliente) FROM clientes";
                    DataTable oTable_3 = new DataTable();
                    MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                    MySqlDataReader odr_3;

                    odr_3 = oCmd_6.ExecuteReader();

                    while (odr_3.Read())
                    {
                        n_cliente = odr_3.GetInt32("count(cod_cliente)");
                    }
                    odr_3.Close();
                    label5.Text = n_cliente.ToString();
                
           
              

                oConn.Close();

                textBox1.Text = TotalRegistros.ToString();
                label1.Text = "Página " + pagatual.ToString();
                avançareretroceder();
                textBox2.Clear();

                btnanterior.Enabled = false;
                btnavançar.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
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

                strSQL = ("Select cod_cliente,Nome_empresa,Morada,Localidade,cod_postal,Telefone,Email,NIF FROM clientes");

                oDA = new MySqlDataAdapter(strSQL.ToString(), oConn);
                dt = new DataTable();
                ds = new DataSet();
                oDA.Fill(dt);
                TotalRegistros = dt.Rows.Count;
                oDA.Fill(ds, inicio, tamanhoPagina, "Clientes");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Clientes";


                dataGridView1.Columns["Editar"].DisplayIndex = 8;

                DataGridViewColumn column = dataGridView1.Columns[0];
                column.Width = 50;
                DataGridViewColumn column1 = dataGridView1.Columns[1];
                column1.Width = 80;
                DataGridViewColumn column2 = dataGridView1.Columns[2];
                column2.Width = 138;
                DataGridViewColumn column3 = dataGridView1.Columns[3];
                column3.Width = 183;
                DataGridViewColumn column4 = dataGridView1.Columns[4];
                column4.Width = 130;
                DataGridViewColumn column5 = dataGridView1.Columns[5];
                column5.Width = 81;
                DataGridViewColumn column6 = dataGridView1.Columns[6];
                column6.Width = 81;
                DataGridViewColumn column7 = dataGridView1.Columns[7];
                column7.Width = 160;
                DataGridViewColumn column8 = dataGridView1.Columns[8];
                column8.Width = 81;

                dataGridView1.Columns["cod_cliente"].HeaderText = "Código Cliente";
                dataGridView1.Columns["Nome_empresa"].HeaderText = "Nome Empresa";
                dataGridView1.Columns["Morada"].HeaderText = "Morada";
                dataGridView1.Columns["Localidade"].HeaderText = "Localidade";
                dataGridView1.Columns["cod_postal"].HeaderText = "Código Postal";
                dataGridView1.Columns["Telefone"].HeaderText = "Telefone";
                dataGridView1.Columns["Email"].HeaderText = "Email";
                dataGridView1.Columns["NIF"].HeaderText = "NIF";

                string strSQL_7 = "Select count(cod_cliente) FROM clientes";
                DataTable oTable_3 = new DataTable();
                MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);

                MySqlDataReader odr_3;

                odr_3 = oCmd_6.ExecuteReader();

                while (odr_3.Read())
                {
                    n_cliente = odr_3.GetInt32("count(cod_cliente)");
                }
                odr_3.Close();
                label5.Text = n_cliente.ToString();

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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            inserir_clientes ic = new inserir_clientes();
            ic.Show();
        }

    
    }
}
