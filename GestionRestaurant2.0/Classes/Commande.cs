using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionRestaurant2._0.Classes
{
    class Commande:ClasseMere
    {
        public int IdEmploye;
        public int IdClient;

        public override void ajout(SqlCommand cmd)
        {
            cnx.Open();
            cmd.Connection = cnx;
            cmd.ExecuteNonQuery();
            cnx.Close();
        }
        public Commande(int IdEmploye,int IdClient)
        {
            this.IdEmploye = IdEmploye;
            this.IdClient = IdClient;
        }
    }
}
