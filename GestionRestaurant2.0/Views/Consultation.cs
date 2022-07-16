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
    public partial class Consultation : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-9B3DFH8\SQLEXPRESS;Initial Catalog=GestionRestaurant;Integrated Security=True");
        public static string id;
        public static string pf;
        public Consultation()
        {
            InitializeComponent();
            cacher();
        }
        //Cacher le menu deroulant
        public void cacher()
        {
            button2.Visible = false;
            button3.Visible = false;
        }
        //Afficher le menu deroulant
        public void afficher()
        {
            button2.Visible = true;
            button3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            afficher();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cacher();
            groupBox1.Visible = true;
            groupBox2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cacher();
            groupBox1.Visible = false;
            groupBox2.Visible = true;
        }
        
        private void Consultation_Load(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                //Choix de la commande et le chemin de cnx
                SqlCommand cmd = new SqlCommand("select * from client where Login='" + Login.dr[0].ToString() + "' and Password='" + Login.dr[1].ToString() + "'",cnx);
                SqlDataReader dr1 = cmd.ExecuteReader();
                //MAj DGV
                DataTable dt = new DataTable();
                dt.Load(dr1);
                DGV.DataSource = dt;
                dr1.Close();
                //Stockage de la valeur id pour et pf pour l'usage dans un autre formulaire
                id = DGV.Rows[0].Cells[0].Value.ToString();
                pf = DGV.Rows[0].Cells[4].Value.ToString();
                cnx.Close();
            }
            catch (Exception o) { errorProvider1.SetError(button1, o.Message); }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChoixServeur cs = new ChoixServeur();
            cs.Show();
        }
    }
}
