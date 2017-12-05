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
    public partial class Form1 : Form
    {
        SqlDataAdapter adapt;
        public static String vendedor;
        public static String tipo;
        public static String wea;
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show(" Ingresa el Usuario y Contraseña .");
                    return;
                }


                string ConString = @"Data Source=KOYUKI-PC\KOYUKI;Initial Catalog=dbEXAMEN;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(ConString))
                {

                    ///SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario where NOMBRE_USUARIO=@user_name and PASSWORD_USUARIO=@pswd",con);
                    SqlCommand cmd = new SqlCommand("SELECT NOMBRE_USUARIO,PASSWORD_USUARIO, HABILITADO, TIPO_USUARIO, NOMBRE_VENDEDOR FROM Usuario U Join VENDEDOR V ON(U.ID_VENDEDOR = V.ID_VENDEDOR) where NOMBRE_USUARIO = @user_name and PASSWORD_USUARIO = @pswd", con);
                    cmd.Parameters.Add(new SqlParameter("@user_name", textBox1.Text.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@pswd", textBox2.Text.Trim()));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    try
                    {
                        vendedor = ds.Tables[0].Rows[0]["NOMBRE_VENDEDOR"].ToString();
                        tipo = ds.Tables[0].Rows[0]["TIPO_USUARIO"].ToString();
                        wea = ds.Tables[0].Rows[0]["HABILITADO"].ToString();
                        MessageBox.Show("Bienvenido "+vendedor+"!!");

                    }
                    catch (Exception es)
                    {
                        MessageBox.Show("No Usuario Registrado O  Nombre/Password Erronea");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }


                    int i = ds.Tables[0].Rows.Count;
                    if (i == 1 && wea == "1")
                    {
                        if (tipo == "1")
                        {
                            this.Hide();
                            MenuNormal menu2 = new MenuNormal();
                            menu2.Show();
                        }
                        else
                        {
                            this.Hide();
                            Menu menu = new Menu();
                            menu.Show();
                        }
                        

                    }
                    if (i == 1 && wea != "1")
                    {
                        MessageBox.Show("El usuario no esta Habilitado");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
