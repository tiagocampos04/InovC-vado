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
    public partial class inserir_produto : Form
    {
        public inserir_produto()
        {
            InitializeComponent();
        }

        MySqlConnection oConn = new MySqlConnection("Persist Security Info=False ; server=localhost; database=pap;uid=root");

        int verificacao;
        int verificacao_2;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if((textBox1.Text!="")&&(textBox2.Text!="")&&(textBox3.Text!=""))
                {
                    string strSQL_1 = "Select count(referencia) FROM produtos where referencia = '" + textBox2.Text + "'";

                    MySqlCommand oCmd_1 = new MySqlCommand(strSQL_1, oConn);

                    oConn.Open();
                    MySqlDataReader odr;

                    odr = oCmd_1.ExecuteReader();

                    while (odr.Read())
                    {
                        verificacao = odr.GetInt32("count(referencia)");
                    }
                    odr.Close();


                    string strSQL_2 = "Select count(referencia) FROM produtos where Nome_produto = '" + textBox1.Text + "'";

                    MySqlCommand oCmd_2 = new MySqlCommand(strSQL_2, oConn);


                    MySqlDataReader odr_1;

                    odr_1 = oCmd_2.ExecuteReader();

                    while (odr_1.Read())
                    {
                        verificacao_2 = odr_1.GetInt32("count(referencia)");
                    }
                    odr_1.Close();

                    if (verificacao > 0)
                    {
                        MessageBox.Show("Já existe um produto com essa referência na base de dados!", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        if(verificacao_2 > 0)
                        {
                            MessageBox.Show("Já existe um produto com esse nome na base de dados!", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            string strConexao = ("Persist Security Info=False ; server=localhost; database=pap;uid=root");
                            MySqlConnection oConn = new MySqlConnection(strConexao);
                            oConn.Open();
                            string strSQL = "Insert into produtos(referencia,Nome_produto,Descricao,cod_tipoproduto)values ('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox3.Text + "','" + comboBox1.SelectedValue + "')";
                            MySqlCommand oCmd = new MySqlCommand(strSQL, oConn);
                            oCmd.ExecuteNonQuery();
                            oConn.Close();

                            MessageBox.Show("Registos feitos com sucesso", " ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                        }


                    
                    }

                }
                else
                {
                    MessageBox.Show("Preencha todos os campos para inserir um novo produto", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                oConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void inserir_produto_Load(object sender, EventArgs e)
        {
            try
            {
                string strConexao = ("Persist Security Info =False;server = localhost;database= pap; uid=root");

                MySqlConnection oConn = new MySqlConnection(strConexao);

                DataTable oTable = new DataTable();

                oConn.Open();

                string strSQL = "Select * FROM tipo_produto";
                MySqlDataAdapter oDA = new MySqlDataAdapter(strSQL, oConn);
                oDA.Fill(oTable);
                comboBox1.DataSource = oTable;
                comboBox1.DisplayMember = "Nome_categoria";
                comboBox1.ValueMember = "cod_tipoproduto";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
