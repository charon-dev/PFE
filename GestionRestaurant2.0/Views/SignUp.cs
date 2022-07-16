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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }
        
        private void SignUp_Load(object sender, EventArgs e)
        {
            
        }
        
        private void rjButton1_Click(object sender, EventArgs e)
        {
            string nom = rjTextBox1.Texts;
            string prenom = rjTextBox2.Texts;
            string tele = rjTextBox3.Texts;
            string mdp = rjTextBox4.Texts;
            //Declaration et initialisation d'objet client
            var client = new Client(nom, prenom, tele, mdp);
            //methode d'ajout
            SqlCommand cmd = new SqlCommand("Insert into client(Nom,Prenom,Telephone,Password) values('"+client.nom+"','"+client.prenom+"','"+client.telephone+"','"+client.password+"')");
            client.ajout(cmd);
            //Affichage du msg de validation
            MessageBox.Show("Insertion valide !", "Msg de validation", MessageBoxButtons.OK);
            //Vers l'auutre formulaire
            this.Hide();
            Login L1 = new Login();
            L1.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login L2 = new Login();
            L2.Show();
        }

        private void rjTextBox4__TextChanged(object sender, EventArgs e)
        {
            if (rjTextBox1.Texts != "" && rjTextBox2.Texts != "" && rjTextBox3.Texts != "" && rjTextBox4.Texts != "")
                rjButton1.Enabled = true;
        }
    }
}
