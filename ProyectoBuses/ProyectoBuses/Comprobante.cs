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
    public partial class Comprobante : Form
    {
        public Comprobante()
        {
            InitializeComponent();
            DisplayData();
        }
        SqlConnection con = new SqlConnection(@"Data Source=KOYUKI-PC\Koyuki;Initial Catalog=dbEXAMEN;Integrated Security=True;");

        SqlCommand cmd;
        SqlDataAdapter adapt;
        int Total = 0;
        String ID = "";
        private void DisplayData()
        {
            
            //Traer Id del Chofer
            SqlCommand cmd = new SqlCommand("SELECT BO.ID_BOLETA,  BU.PATENTE, CO.NOMBRE_CONDUCTOR, CIU.NOMBRE_CIUDAD, CLI.NOMBRE_CLIENTE, CLI.RUT_CLIENTE, CLI.ESTUDIANTE, BO.FECHA, BO.TOTAL, HO.DESCRIPCION FROM BOLETA BO JOIN BUS BU ON(BO.ID_BUS = BU.PATENTE) JOIN CONDUCTOR CO ON(BO.ID_CONDUCTOR = CO.ID_CONDUCTOR) JOIN VENDEDOR VE ON(BO.ID_VENDEDOR = VE.ID_VENDEDOR) JOIN CLIENTE CLI ON(BO.ID_CLIENTE = CLI.ID_CLIENTE) JOIN CIUDAD CIU ON(BU.DESTINO_ID = CIU.ID_CIUDAD) Join HORARIO HO ON(BU.COD_HORARIO = HO.COD_HORARIO)WHERE ID_BOLETA = @idboleta ", con);
            cmd.Parameters.Add(new SqlParameter("@idboleta", Convert.ToInt32(AgregarBoleta.numeroboleta)));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds2 = new DataSet();
            da.Fill(ds2);

            string idboleta = ds2.Tables[0].Rows[0]["ID_BOLETA"].ToString();
            string patente = ds2.Tables[0].Rows[0]["PATENTE"].ToString();
            string conductor = ds2.Tables[0].Rows[0]["NOMBRE_CONDUCTOR"].ToString();
            string destino = ds2.Tables[0].Rows[0]["NOMBRE_CIUDAD"].ToString();
            string nombrecliente = ds2.Tables[0].Rows[0]["NOMBRE_CLIENTE"].ToString();
            string rutclient = ds2.Tables[0].Rows[0]["RUT_CLIENTE"].ToString();
            string estudiante = ds2.Tables[0].Rows[0]["ESTUDIANTE"].ToString();
            string fecha = ds2.Tables[0].Rows[0]["FECHA"].ToString();
            string total = ds2.Tables[0].Rows[0]["TOTAL"].ToString();
            string descripcion = ds2.Tables[0].Rows[0]["DESCRIPCION"].ToString();

            txtBoleta.Text = idboleta;
            txtPatente.Text = patente;
            txtConductor.Text = conductor;
            txtDestino.Text = destino;
            txtNombreCli.Text = nombrecliente;
            txtRut.Text = rutclient;
            if (estudiante == "1")
            {
                txtEstudiante.Text = "SI";
            }
            else
            {
                txtEstudiante.Text = "NO";
            }
            
            txtFecha.Text = fecha;
            txtTotal.Text = total;
            txtHorario.Text = descripcion;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuNormal menun = new MenuNormal();
            menun.Show();
        }
    }
}
