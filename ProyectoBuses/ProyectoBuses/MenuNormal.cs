using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBuses
{
    public partial class MenuNormal : Form
    {
        public MenuNormal()
        {
            InitializeComponent();
            textBox1.Text = "Hola, " + Form1.vendedor;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sesion Cerrada Correctamente");
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AgregarBoleta agre = new AgregarBoleta();
            this.Hide();
            agre.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListaBoletas list = new ListaBoletas();
            list.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MantCliente cli = new MantCliente();
            cli.Show();
        }
    }
}
