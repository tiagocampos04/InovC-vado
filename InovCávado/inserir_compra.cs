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
    public partial class inserir_compra : Form
    {


        public inserir_compra()
        {
            InitializeComponent();

            textBox6.Text = String.Format("{0:#,##0.00}", 0d);
            textBox4.Text = String.Format("{0:#,##0.00}", 0d);
            textBox5.Text = String.Format("{0:#,##0.00}", 0d);
            
        }


       public double total_final2;
        public double preco_a_pagar;
        public double divida;
        public int quantidade;
       public  int quantidade_comprada;
       public double total_liquido;
        public double pc_med;
        public double quantidade_em_stock;
        public double valor_stock;
        public double preco_medio;
        public double preco_minimo;
        public double preco_maximo;
        public double preco_final_stock;
      

        MySqlConnection oConn = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");
        private void inserir_compra_Load(object sender, EventArgs e)
        {
       
            try
            {
                comboBox2.Text = "";
                comboBox1.Text = "";
              
               
                oConn.Open();

                string strSQL = "Select max(cod_compra) FROM compra";
                MySqlCommand cmd = new MySqlCommand(strSQL, oConn);
                
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                if(dr.Read())
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

                   string strSQL_2 = "Select * FROM fornecedor";
                   MySqlDataAdapter oDA_1 = new MySqlDataAdapter(strSQL_2, oConn);
                   oDA_1.Fill(oTable_1);
                   comboBox1.DataSource = oTable_1;
                   comboBox1.DisplayMember = "Nome";
                   comboBox1.ValueMember = "cod_fornecedor";
                   textBox2.Text= comboBox1.SelectedValue.ToString();

                comboBox2.Text = "";
                comboBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                label17.Text = Convert.ToString(0);
                label16.Text = Convert.ToString(0);
                label15.Text = Convert.ToString(0);

                DataGridViewColumn column = dataGridView2.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView2.Columns[1];
                column1.Width = 100;
                DataGridViewColumn column2 = dataGridView2.Columns[2];
                column2.Width = 100;
                DataGridViewColumn column3 = dataGridView2.Columns[3];
                column3.Width = 92;
                DataGridViewColumn column4 = dataGridView2.Columns[4];
                column4.Width = 100;
                DataGridViewColumn column5 = dataGridView2.Columns[5];
                column5.Width = 100;
                DataGridViewColumn column6 = dataGridView2.Columns[6];
                column6.Width = 100;
                DataGridViewColumn column7 = dataGridView2.Columns[7];
                column7.Width = 60;
                DataGridViewColumn column8= dataGridView2.Columns[8];
                column8.Width = 60;

                label15.Text = 0.ToString("F");

                oConn.Close();
            }
            catch(Exception ex)
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
            if (button2.Text== "Atualizar Produtos")
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

                label15.Text = Convert.ToString(total_final2);


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
                column3.Width = 92;
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
                column3.Width = 92;
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
            label16.Text = iva.ToString("F");
            preco_a_pagar =  total_final2 +  iva;
            label17.Text = preco_a_pagar.ToString("F");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
              
              
                string data_compra = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
                string data_venda = dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day;


                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
               
 
                    MySqlCommand cmd_6 = new MySqlCommand();
                    cmd_6.Connection = oConn;
                    cmd_6.CommandText = "Insert into compra(cod_compra,cod_produto,cod_fornecedor,preco_unitario,quantidade,desconto,iva,preco_final)values(@cod_compra,@cod_produto,@cod_fornecedor,@preco_unitario,@quantidade,@desconto,@iva,@preco_final)";
                    cmd_6.Parameters.AddWithValue("@cod_compra", label22.Text);
                    cmd_6.Parameters.AddWithValue("@cod_produto", dataGridView2.Rows[i].Cells[0].Value);
                    cmd_6.Parameters.AddWithValue("@cod_fornecedor", textBox2.Text);
                    cmd_6.Parameters.AddWithValue("@preco_unitario", dataGridView2.Rows[i].Cells[2].Value);
                    cmd_6.Parameters.AddWithValue("@quantidade", dataGridView2.Rows[i].Cells[3].Value);
                    cmd_6.Parameters.AddWithValue("@desconto", dataGridView2.Rows[i].Cells[4].Value);
                    cmd_6.Parameters.AddWithValue("@iva", dataGridView2.Rows[i].Cells[5].Value);
                    cmd_6.Parameters.AddWithValue("@preco_final", dataGridView2.Rows[i].Cells[6].Value);
                 
                     MySqlCommand cmd_8 = new MySqlCommand();
                     cmd_8.Connection = oConn;
                     cmd_8.CommandText = "update produtos set quantidade = quantidade +  @quantidade_comprada  WHERE referencia=@cod_produto";
                     cmd_8.Parameters.AddWithValue("@quantidade_comprada", dataGridView2.Rows[i].Cells[3].Value);
                    cmd_8.Parameters.AddWithValue("@cod_produto", dataGridView2.Rows[i].Cells[0].Value);


                   oConn.Open();
                  
                    cmd_6.ExecuteNonQuery();
                    cmd_8.ExecuteNonQuery();

                    MessageBox.Show("Compra inserida com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    oConn.Close();

                    string strSQL_4 = "Select avg(preco_unitario) FROM compra, produtos where compra.cod_produto = produtos.referencia and referencia = '" + dataGridView2.Rows[i].Cells[0].Value + "'";
                    MySqlCommand oCmd_4 = new MySqlCommand(strSQL_4, oConn);
                    DataTable oTable = new DataTable();
                    MySqlDataReader oda ;

                    string strSQL_5 = "Select min(preco_unitario) FROM compra, produtos where compra.cod_produto = produtos.referencia and referencia = '" + dataGridView2.Rows[i].Cells[0].Value + "'";
                    MySqlCommand oCmd_5 = new MySqlCommand(strSQL_5, oConn);
                    DataTable oTable_1 = new DataTable();
                    MySqlDataReader oda_1;


                    string strSQL_7 = "Select max(preco_unitario) FROM compra, produtos where compra.cod_produto = produtos.referencia and referencia = '" + dataGridView2.Rows[i].Cells[0].Value + "'";
                    MySqlCommand oCmd_6 = new MySqlCommand(strSQL_7, oConn);
                    DataTable oTable_2 = new DataTable();
                    MySqlDataReader oda_2;


                    string strSQL_9 = "Select quantidade FROM  produtos where referencia = '" + dataGridView2.Rows[i].Cells[0].Value + "'";
                    MySqlCommand oCmd_8 = new MySqlCommand(strSQL_9, oConn);
                    DataTable oTable_4 = new DataTable();
                    MySqlDataReader oda_4;

                    oConn.Open();

                    oda_1 = oCmd_5.ExecuteReader();

                    while (oda_1.Read())
                    {
                        preco_minimo = oda_1.GetDouble("min(preco_unitario)");
                    }
                    oda_1.Close();


                    oda = oCmd_4.ExecuteReader();

                    while (oda.Read())
                    {
                       preco_medio  = oda.GetDouble("avg(preco_unitario)");
                    }
                    oda.Close();

                    oda_2 = oCmd_6.ExecuteReader();

                    while (oda_2.Read())
                    {
                        preco_maximo = oda_2.GetDouble("max(preco_unitario)");
                    }
                    oda_2.Close();

                 

                    oda_4 = oCmd_8.ExecuteReader();

                    while (oda_4.Read())
                    {
                        quantidade_em_stock = oda_4.GetDouble("quantidade");
                    }
                    oda_4.Close();

                    preco_final_stock = quantidade_em_stock * preco_medio;


                    MySqlCommand cmd_10 = new MySqlCommand();
                    cmd_10.Connection = oConn;
                    cmd_10.CommandText = "update produtos set pc_med =  @quantidade_comprada  WHERE referencia=@cod_produto";
                    cmd_10.Parameters.AddWithValue("@quantidade_comprada", preco_medio);
                    cmd_10.Parameters.AddWithValue("@cod_produto", dataGridView2.Rows[i].Cells[0].Value);


                    MySqlCommand cmd_11 = new MySqlCommand();
                    cmd_11.Connection = oConn;
                    cmd_11.CommandText = "update produtos set pc_min =  @quantidade_comprada  WHERE referencia=@cod_produto";
                    cmd_11.Parameters.AddWithValue("@quantidade_comprada", preco_minimo );
                    cmd_11.Parameters.AddWithValue("@cod_produto", dataGridView2.Rows[i].Cells[0].Value);

                    MySqlCommand cmd_12 = new MySqlCommand();
                    cmd_12.Connection = oConn;
                    cmd_12.CommandText = "update produtos set pc_max =  @quantidade_comprada  WHERE referencia=@cod_produto";
                    cmd_12.Parameters.AddWithValue("@quantidade_comprada", preco_maximo);
                    cmd_12.Parameters.AddWithValue("@cod_produto", dataGridView2.Rows[i].Cells[0].Value);

                    MySqlCommand cmd_13 = new MySqlCommand();
                    cmd_13.Connection = oConn;
                    cmd_13.CommandText = "update produtos set pc_max =  @quantidade_comprada  WHERE referencia=@cod_produto";
                    cmd_13.Parameters.AddWithValue("@quantidade_comprada", preco_maximo);
                    cmd_13.Parameters.AddWithValue("@cod_produto", dataGridView2.Rows[i].Cells[0].Value);

                    MySqlCommand cmd_14 = new MySqlCommand();
                    cmd_14.Connection = oConn;
                    cmd_14.CommandText = "update produtos set valor_stock =  @quantidade_comprada  WHERE referencia=@cod_produto";
                    cmd_14.Parameters.AddWithValue("@quantidade_comprada", preco_final_stock);
                    cmd_14.Parameters.AddWithValue("@cod_produto", dataGridView2.Rows[i].Cells[0].Value);

                    cmd_10.ExecuteNonQuery();
                    cmd_11.ExecuteNonQuery();
                    cmd_12.ExecuteNonQuery();
                    cmd_13.ExecuteNonQuery();
                    cmd_14.ExecuteNonQuery();
                    oConn.Close();
                }

                MySqlCommand cmd_7 = new MySqlCommand();
                cmd_7.Connection = oConn;
                cmd_7.CommandText = "Insert into detalhes_compra(cod_compra,preco_sem_iva,iva,preco_final,data_compra,data_entrega)values(@cod_compra,@preco_sem_iva,@iva,@preco_final,@data_compra,@data_entrega)";
                cmd_7.Parameters.AddWithValue("@cod_compra", label22.Text);
                cmd_7.Parameters.AddWithValue("@preco_sem_iva", label15.Text);
                cmd_7.Parameters.AddWithValue("@iva", label16.Text);
                cmd_7.Parameters.AddWithValue("@preco_final", label17.Text);
                cmd_7.Parameters.AddWithValue("@data_compra", data_compra);
                cmd_7.Parameters.AddWithValue("@data_entrega", data_venda);

                oConn.Open();

                cmd_7.ExecuteNonQuery();

                oConn.Close();
              

                string strSQL_2 = "Select dividas FROM fornecedor where cod_fornecedor like '" + textBox2.Text + "'";
                MySqlCommand oCmd_2 = new MySqlCommand(strSQL_2, oConn);
                MySqlDataReader odr;

             

                oConn.Open();



              

                MySqlCommand cmd_9= new MySqlCommand();
                cmd_9.Connection = oConn;
                cmd_9.CommandText = "update fornecedor set dividas = dividas +  @quantidade_comprada  WHERE cod_fornecedor=@cod_fornecedor";
                cmd_9.Parameters.AddWithValue("@quantidade_comprada", preco_a_pagar);
                cmd_9.Parameters.AddWithValue("@cod_fornecedor", textBox2.Text);

              cmd_9.ExecuteNonQuery();

               dataGridView2.Rows.Clear();
               total_final2 = 0;
               label17.Text = Convert.ToString(0);
               label16.Text = Convert.ToString(0);
               label15.Text = Convert.ToString(0);

                string strSQL = "Select max(cod_compra) FROM compra";
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
                    dr.Close();
                }

                oConn.Close();


                DataGridViewColumn column = dataGridView2.Columns[0];
                column.Width = 80;
                DataGridViewColumn column1 = dataGridView2.Columns[1];
                column1.Width = 100;
                DataGridViewColumn column2 = dataGridView2.Columns[2];
                column2.Width = 100;
                DataGridViewColumn column3 = dataGridView2.Columns[3];
                column3.Width = 92;
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
                column3.Width = 92;
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
                column3.Width = 92;
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((char.IsDigit(e.KeyChar)) || (e.KeyChar.Equals((char)Keys.Back)))
            {
                TextBox t = (TextBox)sender;
                string w = Regex.Replace(t.Text, "[^0-9]",string.Empty);
                if (w == string.Empty) w = "00";

                if (e.KeyChar.Equals((char)Keys.Back))
                    w = w.Substring(0, w.Length - 1);
                else
                    w += e.KeyChar;

                t.Text = String.Format("{0:#,##0.00}", double.Parse(w) / 100);
                t.Select(t.Text.Length,0);
               

            }
            e.Handled = true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           
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

        private void label16_MouseDown(object sender, MouseEventArgs e)
        {
          
        }
    }
}
