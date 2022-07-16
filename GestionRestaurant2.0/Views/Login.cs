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

namespace GestionRestaurant2._0.Views
{
    public partial class Login : Form
    {
        public static SqlConnection cnx= new SqlConnection(@"Data Source=DESKTOP-9B3DFH8\SQLEXPRESS;Initial Catalog=GestionRestaurant;Integrated Security=True");
        public static SqlDataReader dr;
        public static SqlCommand cmd = new SqlCommand();

        public Login()
        {
            InitializeComponent();
        }
        
        private void rjButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if(rjTextBox1.Texts=="Direction" && rjTextBox2.Texts == "0000")
                {
                    //Vers le formulaire d'impression
                    this.Hide();
                    Impression imp = new Impression();
                    imp.Show();
                }
                else
                {
                    cnx.Open();
                    //definition de la commande et le chemin de connexion
                    cmd.CommandText = "select Login,Password from client where Login='" + rjTextBox1.Texts + "' and Password='" + rjTextBox2.Texts + "'";
                    cmd.Connection = cnx;
                    dr = cmd.ExecuteReader();
                    //Test si le login et le mdp sont identique dans la bd
                    while (dr.Read())
                    {
                        if(rjTextBox1.Texts==dr[0].ToString() && rjTextBox2.Texts == dr[1].ToString())
                        {
                            //Vers le formulaire suivant
                            this.Hide();
                            Consultation c = new Consultation();
                            c.Show();
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Nom d'utilisateur ou mdp incorrecte", "notification", MessageBoxButtons.OK);
                            break;
                        }
                    }
                    dr.Close();
                    cnx.Close();
                }
            }
            catch(Exception o) { errorProvider1.SetError(rjButton1, o.Message); }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Votre nom d'utilisateur est comme suivant : premiere lettre de votre Prenmom suivit d'un point suivit de votre Nom", "Notification", MessageBoxButtons.OK);
        }
    }
}
