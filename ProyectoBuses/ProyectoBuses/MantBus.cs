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
    public partial class MantBus : Form
    {
        SqlDataAdapter da = new SqlDataAdapter();

        public MantBus()
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

            adapt = new SqlDataAdapter("select * from Bus", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;

            //Mostrar ComboBox
            con.Open();
            DataTable dt2 = new DataTable();
            adapt = new SqlDataAdapter("select * from Horario", con);
            adapt.Fill(dt2);

            cbHorario.DataSource = dt2;
            cbHorario.DisplayMember = "DESCRIPCION"; //campo que queres mostrar
            cbHorario.ValueMember = "COD_HORARIO"; //valor que capturas
            con.Close();

            //Mostrar ComboBox
            con.Open();
            DataTable dt3 = new DataTable();
            adapt = new SqlDataAdapter("select * from CIUDAD", con);
            adapt.Fill(dt3);

            cbDestino.DataSource = dt3;
            cbDestino.DisplayMember = "NOMBRE_CIUDAD"; //campo que queres mostrar
            cbDestino.ValueMember = "ID_CIUDAD"; //valor que capturas
            con.Close();

            //Mostrar ComboBox
            con.Open();
            DataTable dt4 = new DataTable();
            adapt = new SqlDataAdapter("select * from CONDUCTOR", con);
            adapt.Fill(dt4);

            cbConductor.DataSource = dt4;
            cbConductor.DisplayMember = "NOMBRE_CONDUCTOR"; //campo que queres mostrar
            cbConductor.ValueMember = "ID_CONDUCTOR"; //valor que capturas
            con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            txtPatente.Text = "";
            txtNombre.Text = "";
            ID = "";
            btnCrear.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        //dataGridView1 RowHeaderMouseClick Event  
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtPatente.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNombre.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbConductor.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTipo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cbHorario.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cbDestino.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            btnCrear.Enabled = false;
            btnEliminar.Enabled = true;
            btnActualizar.Enabled = true;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPatente.Text != "" && txtNombre.Text != "")
                {
                    
                        string codigo = "insert into BUS (PATENTE, NOMBRE_BUS, ID_CONDUCTOR, TIPO_BUS, COD_HORARIO, DESTINO_ID) values(@patente,@nombre,@conductor, @tipo, @horario,@destino) ";
                        cmd = new SqlCommand(codigo, con);
                        MessageBox.Show(codigo);
                        con.Open();
                        cmd.Parameters.AddWithValue("@patente", txtPatente.Text);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@conductor", cbConductor.SelectedValue);
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);
                        cmd.Parameters.AddWithValue("@horario", cbHorario.SelectedValue);
                        cmd.Parameters.AddWithValue("@destino", cbDestino.SelectedValue);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Datos Actualizados");
                        con.Close();
                        DisplayData();
                        ClearData();
                    
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

                if (txtPatente.Text != "" && txtNombre.Text != "")
                {
                   
                        string update = "update BUS set PATENTE = @patente, NOMBRE_BUS =@nombre, ID_CONDUCTOR=@conductor, TIPO_BUS =@tipo, COD_HORARIO =@horario, DESTINO_ID=@destino where PATENTE = @id";
                        cmd = new SqlCommand(update, con);

                        con.Open();
                        cmd.Parameters.AddWithValue("@patente", txtPatente.Text);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@conductor", cbConductor.SelectedValue);
                        cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);
                        cmd.Parameters.AddWithValue("@horario", cbHorario.SelectedValue);
                        cmd.Parameters.AddWithValue("@destino", cbDestino.SelectedValue);
                        cmd.Parameters.AddWithValue("@id", ID);
                        MessageBox.Show(ID + txtPatente.Text + update);
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
                if (txtPatente.Text != "")
                {
                    string codigo = "delete BUS where PATENTE=@id";
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
            MantConductor condu = new MantConductor();
            condu.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MantHorario hor = new MantHorario();
            hor.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MantCiudad ciud = new MantCiudad();
            ciud.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DisplayData();
        }
    }
}
