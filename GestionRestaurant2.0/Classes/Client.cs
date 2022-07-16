using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionRestaurant2._0.Classes
{
    class Client:ClasseMere
    {
        public string nom;
        public string prenom;
        public string telephone;
        public string password;

        public override void ajout(SqlCommand cmd)
        {
            cnx.Open();
            cmd.Connection = cnx;
            cmd.ExecuteNonQuery();
            cnx.Close();
            
        }
        public Client(string nom,string prenom,string telephone,string password)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.telephone = telephone;
            this.password = password;
        }
    }
}
