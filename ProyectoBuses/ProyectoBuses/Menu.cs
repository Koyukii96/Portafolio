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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            textBox1.Text = "Hola, "+Form1.vendedor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MantConductor cond = new MantConductor();
            cond.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            MantUsuarios usu = new MantUsuarios();
            usu.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MantVendedor ven = new MantVendedor();
            ven.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sesion Cerrada Correctamente");
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MantCiudad ciudad = new MantCiudad();
            this.Hide();
            ciudad.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MantHorario horario = new MantHorario();
            this.Hide();
            horario.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MantBus bus = new MantBus();
            this.Hide();
            bus.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MantCliente cli = new MantCliente();
            this.Hide();
            cli.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ListaBoletas list = new ListaBoletas();
            this.Hide();
            list.Show();
        }
    }
}
