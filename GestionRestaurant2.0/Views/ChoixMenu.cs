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
using GestionRestaurant2._0.Classes;

namespace GestionRestaurant2._0.Views
{
    public partial class ChoixMenu : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-9B3DFH8\SQLEXPRESS;Initial Catalog=GestionRestaurant;Integrated Security=True");
        public ChoixMenu()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //Maj datasource dgv:
        public void LoadDgv(SqlDataReader d, DataGridView dgv)
        {
            DataTable dt = new DataTable();
            dt.Load(d);
            dgv.DataSource = dt;
            d.Close();
        }
        private void ChoixMenu_Load(object sender, EventArgs e)
        {
            try
            {
                cnx.Open();
                //Prechargement des dgv
                SqlCommand cmd = new SqlCommand("select libelle,Prix from produit where type='entree'", cnx);
                SqlDataReader dr = cmd.ExecuteReader();
                LoadDgv(dr, dataGridView1);

                SqlCommand cmd1 = new SqlCommand("select libelle,Prix from produit where type='plat'", cnx);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                LoadDgv(dr1, dataGridView2);

                SqlCommand cmd2 = new SqlCommand("select libelle,Prix from produit where type='dessert'", cnx);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                LoadDgv(dr2, dataGridView3);

                SqlCommand cmd3 = new SqlCommand("select libelle,Prix from produit where type='Jus'", cnx);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                LoadDgv(dr3, dataGridView4);

                SqlCommand cmd4 = new SqlCommand("select libelle,Prix from produit where type='Boisson'", cnx);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                LoadDgv(dr4, dataGridView5);

                cnx.Close();
            }
            catch(Exception o) { errorProvider1.SetError(dataGridView1, o.Message); }
        }
        //Recuperation des valeurs du dgv:
        public void recupValue(DataGridView dgv, DataGridViewCellEventArgs e)
        {
            label7.Visible = true;
            label7.Text = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
            label10.Text = dgv.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            recupValue(dataGridView1, e);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            recupValue(dataGridView2, e);
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            recupValue(dataGridView3, e);
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            recupValue(dataGridView4, e);
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            recupValue(dataGridView5, e);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            //Calcul prixtotale
            rjTextBox2.Texts= (Convert.ToInt64(label10.Text) * Convert.ToInt64(rjTextBox1.Texts)).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
            string LibProd = label7.Text;
            int NumCommande = int.Parse(ChoixServeur.numeroCommande);
            int QuantiteProd = int.Parse(rjTextBox1.Texts);
            int prixtt = int.Parse(rjTextBox2.Texts);
            //ouverture d la cnx
            cnx.Open();
            //Initialisation et declaration d'onjet:
            var detailCommande = new DetailCommande(LibProd,NumCommande,QuantiteProd,prixtt);
            //Apelle de la methode d'ajout
            SqlCommand cmd = new SqlCommand("insert into DetailCommande values('" + detailCommande.libelleProd + "','" + detailCommande.NumeroCmd + "','" + detailCommande.QuantiteProd + "','" + detailCommande.Prixtt + "')", cnx);
            detailCommande.ajout(cmd);
            //Affichafe du msg d'ajout
            MessageBox.Show("produit ajoute !", "Notification", MessageBoxButtons.OK);
            //Vider les zones de saise
            label7.Text = "";
            rjTextBox1.Texts = "";
            rjTextBox2.Texts = "";

            cnx.Close();
            }
            catch(Exception o) { errorProvider1.SetError(button4, o.Message); }
            
        }
    }
}
