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
    public partial class atualizar_produto : Form
    {
        public atualizar_produto()
        {
            InitializeComponent();
        }

        private void atualizar_produto_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox5.Text != ""))
                {
                    string strConexao = ("Persist Security Info=False ; server=localhost; database=pap;uid=root");

                    MySqlConnection oConn = new MySqlConnection(strConexao);
                    String strSQL = "";
                    MySqlCommand oCmd = new MySqlCommand();

                    oCmd = oConn.CreateCommand();

                    oConn.Open();

                    strSQL = ("update produtos set Nome_produto='" + textBox2.Text + "', Descricao='" + textBox1.Text + "', cod_tipoproduto='" + comboBox1.SelectedValue + "' Where referencia=" + textBox5.Text + "");


                    oCmd.CommandText = strSQL;

                    oCmd.ExecuteNonQuery();

                    oConn.Close();

                    MessageBox.Show("Registo atualizado com sucesso!");

                }
                else
                {
                    MessageBox.Show("Preencha todos os campos para inserir um novo produto", "Mensagem de Erro", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }


                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
