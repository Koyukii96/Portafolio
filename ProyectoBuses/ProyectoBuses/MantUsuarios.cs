using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace ProyectoBuses
{
    public partial class MantUsuarios : Form
    {
        SqlDataAdapter da = new SqlDataAdapter();

        public MantUsuarios()
        {
            InitializeComponent();
            DisplayData();
        }
        ///string ConString = "Data Source=XE;User Id=system;Password=12345;";
        SqlConnection con = new SqlConnection(@"Data Source=KOYUKI-PC\Koyuki;Initial Catalog=dbEXAMEN;Integrated Security=True;");

        SqlCommand cmd;
        SqlDataAdapter adapt;
        
        String ID = "";
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();

            adapt = new SqlDataAdapter("select * from Usuario", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;

            //Mostrar ComboBox
            con.Open();
            DataTable dt2 = new DataTable();
            adapt = new SqlDataAdapter("select * from VENDEDOR", con);
            adapt.Fill(dt2);

            comboBox1.DataSource = dt2;
            comboBox1.DisplayMember = "NOMBRE_VENDEDOR"; //campo que queres mostrar
            comboBox1.ValueMember = "ID_VENDEDOR"; //valor que capturas
            con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            txtUsuario.Text = "";
            txtPass.Text = "";
            txtEstado.Text = "";
            ID = "";
            btnCrear.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        //dataGridView1 RowHeaderMouseClick Event  
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtUsuario.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtPass.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtEstado.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTipo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            comboBox1.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            btnCrear.Enabled = false;
            btnEliminar.Enabled = true;
            btnActualizar.Enabled = true;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsuario.Text != "" && txtPass.Text != "")
                {
                    if (txtEstado.Text == "1" || txtEstado.Text == "0")
                    {
                        string codigo = "insert into USUARIO (NOMBRE_USUARIO, PASSWORD_USUARIO, HABILITADO, TIPO_USUARIO, ID_VENDEDOR) values(@nombre,@pw,@estado, @tipo, @vendedor) ";
                        cmd = new SqlCommand(codigo, con);
                        MessageBox.Show(codigo);
                        con.Open();
                        cmd.Parameters.AddWithValue("@nombre", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@pw", txtPass.Text);
                        cmd.Parameters.AddWithValue("@estado", Convert.ToInt32(txtEstado.Text));
                        cmd.Parameters.AddWithValue("@tipo", Convert.ToInt32(txtEstado.Text));
                        cmd.Parameters.AddWithValue("@vendedor", comboBox1.SelectedValue);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Datos Actualizados");
                        con.Close();
                        DisplayData();
                        ClearData();
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("El estado tiene que ser 1 o 0");
                    }
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Porfavor Seleccione los datos");
                }
            }
            catch (Exception esse)
            {
                con.Close();
                MessageBox.Show(esse.ToString());
            }
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtUsuario.Text != "" && txtPass.Text != "")
                {
                    if (txtEstado.Text == "1" || txtEstado.Text == "0")
                    {


                        string update = "update Usuario set NOMBRE_USUARIO = @nombre, PASSWORD_USUARIO =@pass, HABILITADO=@estado, TIPO_USUARIO =@tipo, ID_VENDEDOR =@vendedor where NOMBRE_USUARIO = @id";
                        cmd = new SqlCommand(update, con);

                        con.Open();
                        cmd.Parameters.AddWithValue("@nombre", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                        cmd.Parameters.AddWithValue("@estado", Convert.ToInt32(txtEstado.Text));
                        cmd.Parameters.AddWithValue("@tipo", Convert.ToInt32(txtTipo.Text));
                        cmd.Parameters.AddWithValue("@vendedor", comboBox1.SelectedValue);
                        cmd.Parameters.AddWithValue("@id", ID);
                        MessageBox.Show(ID + txtUsuario.Text + update);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Datos Actualizados");
                        con.Close();
                        DisplayData();
                        ClearData();
                        btnCrear.Enabled = true;
                    }
                    else
                    {
                        con.Close();
                        MessageBox.Show("El estado tiene que ser 1 o 0");
                    }
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Porfavor Seleccione los datos");
                }
            }
            catch (Exception esse)
            {

                MessageBox.Show(esse.ToString());
                con.Close();
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsuario.Text != "")
                {
                    string codigo = "delete Usuario where NOMBRE_USUARIO=@id";
                    cmd = new SqlCommand(codigo, con);
                    MessageBox.Show(codigo);
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Borrado Correctamente!");
                    DisplayData();
                    ClearData();
                }
                else
                {
                    MessageBox.Show("Porfavor Seleccione el dato a borrar");
                }
            }
            catch (Exception essse)
            {
                MessageBox.Show(essse.ToString());
                con.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu menu = new Menu();
            menu.Show();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            ClearData();

            DisplayData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MantVendedor vende = new MantVendedor();
            vende.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
    }
}

