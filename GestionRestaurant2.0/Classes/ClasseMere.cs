using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GestionRestaurant2._0.Classes
{
    abstract class ClasseMere
    {
        //Chaine de connexion
        public static SqlConnection cnx = new SqlConnection(@"Data Source=DESKTOP-9B3DFH8\SQLEXPRESS;Initial Catalog=GestionRestaurant;Integrated Security=True");

        public abstract void ajout(SqlCommand cmd);
        
    }
}
