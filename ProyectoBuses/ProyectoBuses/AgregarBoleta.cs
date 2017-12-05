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
    public partial class AgregarBoleta : Form
    {
        SqlDataAdapter da = new SqlDataAdapter();
        public static String numeroboleta;
        public AgregarBoleta()
        {
            InitializeComponent();
            DisplayData();
        }
        ///string ConString = "Data Source=XE;User Id=system;Password=12345;";
        SqlConnection con = new SqlConnection(@"Data Source=KOYUKI-PC\Koyuki;Initial Catalog=dbEXAMEN;Integrated Security=True;");

        SqlCommand cmd;
        SqlDataAdapter adapt;
        int Total = 0;
        String ID = "";
        private void DisplayData()
        {
            //Mostrar ComboBox
            con.Open();
            DataTable dt2 = new DataTable();
            adapt = new SqlDataAdapter("select * from BUS", con);
            adapt.Fill(dt2);

            cbPatente.DataSource = dt2;
            cbPatente.DisplayMember = "PATENTE"; //campo que queres mostrar
            cbPatente.ValueMember = "PATENTE"; //valor que capturas
            con.Close();

            //Mostrar ComboBox
            con.Open();
            DataTable dt3 = new DataTable();
            adapt = new SqlDataAdapter("select * from VENDEDOR", con);
            adapt.Fill(dt3);

            cbVendedor.DataSource = dt3;
            cbVendedor.DisplayMember = "NOMBRE_VENDEDOR"; //campo que queres mostrar
            cbVendedor.ValueMember = "ID_VENDEDOR"; //valor que capturas
            con.Close();

            //Mostrar ComboBox
            con.Open();
            DataTable dt4 = new DataTable();
            adapt = new SqlDataAdapter("select * from CLIENTE", con);
            adapt.Fill(dt4);

            cbCliente.DataSource = dt4;
            cbCliente.DisplayMember = "NOMBRE_CLIENTE"; //campo que queres mostrar
            cbCliente.ValueMember = "ID_CLIENTE"; //valor que capturas
            con.Close();
            


        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                    //Traer Id del Chofer
                    SqlCommand cmd = new SqlCommand("SELECT B.ID_CONDUCTOR, C.NOMBRE_CONDUCTOR FROM BUS B JOIN CONDUCTOR C ON(B.ID_CONDUCTOR = C.ID_CONDUCTOR) where B.PATENTE = @idbus", con);
                    cmd.Parameters.Add(new SqlParameter("@idbus", cbPatente.SelectedValue));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds2 = new DataSet();
                    da.Fill(ds2);
                    string idconductor = ds2.Tables[0].Rows[0]["ID_CONDUCTOR"].ToString();
                    
                   //Traer el dato si es cliente
                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM CLIENTE WHERE ID_CLIENTE=@idcliente", con);
                    cmd2.Parameters.Add(new SqlParameter("@idcliente", cbCliente.SelectedValue));
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataSet ds3 = new DataSet();
                    da2.Fill(ds3);
                    string estudiante = ds3.Tables[0].Rows[0]["ESTUDIANTE"].ToString();

                    //MessageBox.Show(estudiante);
                    //MessageBox.Show(idconductor);
                    DateTime fechaactual = DateTime.Now.Date;
                    //MessageBox.Show(fechaactual.ToString());
                    //Calcular Monto
                    if (estudiante == "1")
                    {
                        Total = 210;
                    }
                     else
                    {
                        Total = 700;
                     }
                
                string codigo = "insert into BOLETA (ID_BUS, ID_CONDUCTOR, ID_VENDEDOR, ID_CLIENTE, FECHA, TOTAL) values(@bus,@conductor,@vendedor, @cliente, @fecha,@total) ";
                cmd = new SqlCommand(codigo, con);
                //MessageBox.Show(codigo);
                con.Open();
                cmd.Parameters.AddWithValue("@bus", cbPatente.SelectedValue);
                cmd.Parameters.AddWithValue("@conductor", idconductor);
                cmd.Parameters.AddWithValue("@vendedor", cbVendedor.SelectedValue);
                cmd.Parameters.AddWithValue("@cliente", cbCliente.SelectedValue);
                cmd.Parameters.AddWithValue("@fecha", fechaactual);
                cmd.Parameters.AddWithValue("@total", Total);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("SELECT @@IDENTITY as wea", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da5 = new SqlDataAdapter(cmd);
                DataSet ds5 = new DataSet();
                da5.Fill(ds5);
                numeroboleta = ds5.Tables[0].Rows[0]["wea"].ToString();
                MessageBox.Show("Creada... Su boleta es la numero :"+numeroboleta);
                
                con.Close();

                /*SqlCommand cmd = new SqlCommand("SELECT @@IDENTITY as wea", con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd);
                DataSet ds5 = new DataSet();
                da5.Fill(ds5);
                string numeroboleta = ds5.Tables[0].Rows[0]["wea"].ToString();
                MessageBox.Show(numeroboleta);*/

               IraComprobante();
            }
            catch (Exception esse)
            {
                con.Close();
                MessageBox.Show(esse.ToString());
            }
        }

        private void IraComprobante()
        {
            this.Close();
            Comprobante com = new Comprobante();
            com.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MantCliente cli = new MantCliente();
            cli.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.tipo == "1")
            {
                this.Hide();
                MenuNormal men = new MenuNormal();
                men.Show();
            }
            else
            {
                this.Hide();
                Menu men2 = new Menu();
                men2.Show();
            }
        }
    }
}
