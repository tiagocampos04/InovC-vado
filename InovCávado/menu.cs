using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace InovCávado
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void AbrirFormulario(object form)
        {
            if (panelvisualizar.Controls.Count > 0)
            {
                panelvisualizar.Controls.Clear();
                Form fh = form as Form;
                fh.TopLevel = false;
                fh.Dock = DockStyle.Fill;
                panelvisualizar.Controls.Add(fh);
                panelvisualizar.Tag = fh;
                fh.Show();
   
            }
            else
            {

                panelvisualizar.Controls.Clear();
                Form fh = form as Form;
                fh.TopLevel = false;
                fh.Dock = DockStyle.Fill;
                panelvisualizar.Controls.Add(fh);
                panelvisualizar.Tag = fh;
                fh.Show();
            }
       
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
            label2.Text = DateTime.Now.ToLongDateString();
        }

        private void menu_Load(object sender, EventArgs e)
        {
            label7.Text = global.Nome;
            escondersubmenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mostrar_panel(submenuclientes);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mostrar_panel(submenufornecedores);

        }

        private void costumizardesign()
        {
            submenuclientes.Visible = false;
            submenufornecedores.Visible = false;
            submenuprodutos.Visible = false;
        }

        private void escondersubmenu()
        {
            if (submenuclientes.Visible==true)
           
                submenuclientes.Visible = false;

            if(submenufornecedores.Visible==true)

               submenufornecedores.Visible=false;


            if (submenuprodutos.Visible == true)

                submenuprodutos.Visible = false;

            if (submenucompra.Visible == true)

                submenucompra.Visible = false;

            if(panel8.Visible==true)
               panel8.Visible = false;

            if(panel6.Visible==true)
                panel6.Visible = false;

        }

        private void mostrar_panel(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                escondersubmenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }

        }

        public void button3_Click(object sender, EventArgs e)
        {
     
            AbrirFormulario(new clientes());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
         
            AbrirFormulario(new dividas_clientes());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
            Form1 f1 = new Form1();
            this.Close();
            f1.Show();
            
        }

        private void btnvisualizarfornecedores_Click(object sender, EventArgs e)
        {
          
            AbrirFormulario(new fornecedores());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();
        }

        private void btninserirclientes_Click(object sender, EventArgs e)
        {
           
            AbrirFormulario(new inserir_fornecedores());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();
        
        }

        private void btncategoria_Click(object sender, EventArgs e)
        {
         
            AbrirFormulario(new categoria());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();

        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            mostrar_panel(submenuprodutos);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            AbrirFormulario(new visualizar_produtos());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            AbrirFormulario(new inserir_produto());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();

        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            AbrirFormulario(new inserir_compra());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            mostrar_panel(submenucompra);
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
            AbrirFormulario(new dividas_aos_fornecedores());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();

        }

        private void button7_Click(object sender, EventArgs e)
        {
           
            AbrirFormulario(new stock());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();

        }

        private void button10_Click(object sender, EventArgs e)
        {
          
            AbrirFormulario(new despesas());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            AbrirFormulario(new categoria_despesas());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();

        }

        private void button12_Click(object sender, EventArgs e)
        {
        
            AbrirFormulario(new inserir_despesa());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();
       
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            mostrar_panel(panel6);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mostrar_panel(panel8);
        }

        private void btninserirvendas_Click(object sender, EventArgs e)
        {
           
            AbrirFormulario(new vendas());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            AbrirFormulario(new visualizar_compras());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new visualizar_vendas());
            label6.Visible = false;
            label7.Visible = false;
            escondersubmenu();
        }

        private void panelvisualizar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
