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
    public partial class ChoixServeur : Form
    {
        public static string numeroCommande = "";
        public SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-9B3DFH8\SQLEXPRESS;Initial Catalog=GestionRestaurant;Integrated Security=True");
        public ChoixServeur()
        {
            InitializeComponent();
        }
        
        private Form activeForm = null;
        private void openChildForm(Form children)
        {
            //Fermer la forme ouverte
            if (activeForm != null)
                activeForm.Close();
            activeForm = children;
            //Affiche la forme comme niveau superieur
            children.TopLevel = false;
            //Elever les bordures
            children.FormBorderStyle = FormBorderStyle.None;
            //Remplir l'espace vide
            children.Dock = DockStyle.Fill;
            //Ajout de la forme dans le panel
            panel4.Controls.Add(children);
            //Association du forme avec le panel
            panel4.Tag = children;
            children.BringToFront();
            //Affichage de la forme
            children.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ouverture du choix menu
            openChildForm(new ChoixMenu());
            button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Changement de label et ouverture du payement
            label1.Text = "Payement";
            //Changement de la photo
            pictureBox2.Visible = true;
            pictureBox1.Visible = false;
            //
            pictureBox3.Enabled = true;
            button4.Enabled = false;
            openChildForm(new Payement());
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ChoixServeur_Load(object sender, EventArgs e)
        {
            try
            {   //Maj Id
                label4.Text = Consultation.id;
                cnx.Open();
                //Maj dgv
                SqlCommand cmd = new SqlCommand("Select Id,Nom,Prenom from employe where Fonction='Serveur'",cnx);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                dr.Close();
                //Maj cb
                SqlCommand cmd1 = new SqlCommand("select Id from employe where Fonction='Serveur'", cnx);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    rjComboBox1.Items.Add(dr1[0]);
                }
                dr1.Close();
                cnx.Close();
            }
            catch(Exception ex) { errorProvider1.SetError(dataGridView1, ex.Message); }
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            button4.Enabled = true;
            button2.Enabled = true;
            rjButton1.Enabled = false;
            //Desactivation du combobox
            rjComboBox1.Enabled = false;
            try
            {
                int IdC = int.Parse(label4.Text);
                int IdEmp = int.Parse(rjComboBox1.Texts);
                //Ouverture de la cnx
                cnx.Open();
                //Declaration et initialisation de l'objejt commande
                var commande = new Commande(IdEmp, IdC);
                //Choix de la commande 
                SqlCommand cmd = new SqlCommand("insert into commande(DateCommande,IdEmploye,IdClient) values(getdate(),'" + commande.IdEmploye + "','" + commande.IdClient + "')", cnx);
                //Methode d'ajout
                commande.ajout(cmd);
                //validation du choix 
                MessageBox.Show("Choix valide", "notification", MessageBoxButtons.OK);
                //Stockage du numero cmd dans la variable
                SqlCommand cmd1 = new SqlCommand("select Numero from commande where IdEmploye='" + rjComboBox1.Texts + "' and IdClient='" + label4.Text + "'", cnx);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    numeroCommande = dr[0].ToString();
                }
                dr.Close();
                cnx.Close();
            }
            catch(Exception o) { errorProvider1.SetError(rjButton1, o.Message); }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
