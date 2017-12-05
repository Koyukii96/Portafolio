﻿using System;
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
    public partial class MantVendedor : Form
    {
        SqlDataAdapter da = new SqlDataAdapter();

        public MantVendedor()
        {
            InitializeComponent();
            DisplayData();
        }
        ///string ConString = "Data Source=XE;User Id=system;Password=12345;";
        SqlConnection con = new SqlConnection(@"Data Source=KOYUKI-PC\Koyuki;Initial Catalog=dbEXAMEN;Integrated Security=True;");

        SqlCommand cmd;
        SqlDataAdapter adapt;

        int ID = 0;
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();

            adapt = new SqlDataAdapter("select * from VENDEDOR", con);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        //Clear Data  
        private void ClearData()
        {
            txtRut.Text = "";
            txtNombre.Text = "";
            ID = 0;
            btnCrear.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }
        //dataGridView1 RowHeaderMouseClick Event  
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtRut.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtNombre.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            btnCrear.Enabled = false;
            btnEliminar.Enabled = true;
            btnActualizar.Enabled = true;
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRut.Text != "" && txtNombre.Text != "")
                {
                        string codigo = "insert into Vendedor (RUT_VENDEDOR, NOMBRE_VENDEDOR) values(@rut,@nombre) ";
                        cmd = new SqlCommand(codigo, con);
                        MessageBox.Show(codigo);
                        con.Open();
                        cmd.Parameters.AddWithValue("@rut", txtRut.Text);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
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

                if (txtRut.Text != "" && txtNombre.Text != "")
                {
                        string update = "update VENDEDOR set RUT_VENDEDOR = @rut, NOMBRE_VENDEDOR =@nombre where ID_VENDEDOR = @id";
                        cmd = new SqlCommand(update, con);

                        con.Open();
                        cmd.Parameters.AddWithValue("@rut", txtRut.Text);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@id", ID);
                        MessageBox.Show(ID + txtRut.Text + update);
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
                if (txtRut.Text != "")
                {
                    string codigo = "delete VENDEDOR where ID_VENDEDOR=@id";
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
        }
    }
}
