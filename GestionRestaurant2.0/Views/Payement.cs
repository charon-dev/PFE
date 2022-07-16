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
    public partial class Payement : Form
    {
        SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-9B3DFH8\SQLEXPRESS;Initial Catalog=GestionRestaurant;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        float res = 0;
        public Payement()
        {
            InitializeComponent();
        }

        private void Payement_Load(object sender, EventArgs e)
        {
            //Ajout des elements du cb
            rjComboBox1.Items.Add("Espece");
            rjComboBox1.Items.Add("Carte bancaire");
            try
            {
                //Ouverture de la cnx
                cnx.Open();

                //Maj pf
                label3.Text = Consultation.pf;

                //Choix de la commande 
                cmd.Connection = cnx;
                cmd.CommandText = "select LibelleProduit,QuantiteProduit,PrixTotal from DetailCommande where NumeroCommande='" + ChoixServeur.numeroCommande + "'";
                dr = cmd.ExecuteReader();

                //Maj dgv
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView5.DataSource = dt;
                dr.Close();

                //Fermeture de la cnx
                cnx.Close();
            }
            catch (Exception ex) { errorProvider1.SetError(dataGridView5, ex.Message); }

        }

        private void label9_Click(object sender, EventArgs e)
        {
            //calcul prix a payer

            for (int i = 0; i < dataGridView5.Rows.Count; i++)
            {
                res += (float)Convert.ToDouble(dataGridView5.Rows[i].Cells[2].Value);

            }
            //Reduction selon point de fidelite
            if (Convert.ToInt64(label3.Text) > 50 && Convert.ToInt64(label3.Text) < 70)
            {

                MessageBox.Show("Selon les points de fidelites une reduction de 5% a ete appliquee", "Notification", MessageBoxButtons.OK);
                res = res - ((float)Convert.ToDouble(res * 0.05));

            }
            else if (Convert.ToInt64(label3.Text)>70 && Convert.ToInt64(label3.Text) < 80)
            {
                MessageBox.Show("Selon les points de fidelites une reduction de 7% a ete appliquee", "Notification", MessageBoxButtons.OK);
                res = res - ((float)Convert.ToDouble(res * 0.07));

            }
            else if (Convert.ToInt64(label3.Text)>80 && Convert.ToInt64(label3.Text) < 90)
            {
                MessageBox.Show("Selon les points de fidelites une reduction de 10% a ete appliquee", "Notification", MessageBoxButtons.OK);
                res = res - ((float)Convert.ToDouble(res * 0.10));

            }
            else if (Convert.ToInt64(label3.Text)>90 && Convert.ToInt64(label3.Text) < 95)
            {
                MessageBox.Show("Selon les points de fidelites une reduction de 15% a ete appliquee", "Notification", MessageBoxButtons.OK);
                res = res - ((float)Convert.ToDouble(res * 0.15));

            }
            else if (Convert.ToInt64(label3.Text) >= 95)
            {
                MessageBox.Show("Points de fidelites reinitiliases a 0", "Notification", MessageBoxButtons.OK);

            }
            
            textBox1.Text = res.ToString();

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            try
            {

                //Ouverture de la cnx
                cnx.Open();

                //Choix de la commande 
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter TypePayement = new SqlParameter("@TypePayement", SqlDbType.VarChar);
                SqlParameter PrixAPayer = new SqlParameter("@PrixAPayer", SqlDbType.VarChar);
                SqlParameter Numero = new SqlParameter("@Numero", SqlDbType.Int);

                TypePayement.Value = rjComboBox1.Text;
                PrixAPayer.Value = textBox1.Text;
                Numero.Value = ChoixServeur.numeroCommande;

                cmd.Parameters.Add(TypePayement);
                cmd.Parameters.Add(PrixAPayer);
                cmd.Parameters.Add(Numero);

                cmd.CommandText = "changement";

                cmd.ExecuteNonQuery();

                //Fermeture de la cnx
                cnx.Close();
                //Affichage du msg
                MessageBox.Show("Payement valide !", "Notification", MessageBoxButtons.OK);
            }
            catch (Exception ex) { errorProvider1.SetError(rjButton1, ex.Message); }

        }
    }
}
