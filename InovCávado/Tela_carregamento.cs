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
    public partial class Tela_carregamento : Form
    {
        public Tela_carregamento()
        {
            InitializeComponent();
        }



        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 15;

            if (panel2.Width > 785)
            {

                menu m = new menu();
                m.Show();
                this.Close();
            }

            if (panel2.Width < 400)
            {
                label1.Text = "Carregar a interface";
            }
            else if ((panel2.Width > 400) && (panel2.Width <700))
            {
                label1.Text = "Carregar a base de dados";
            }
            else if (panel2.Width >= 700)
            {
                label1.Text = "Aguarde mais um momento";
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Tela_carregamento_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Tela_carregamento_Load(object sender, EventArgs e)
        {

        }
    }
}
