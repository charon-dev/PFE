using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionRestaurant2._0.Classes
{
    class DetailCommande:ClasseMere
    {
        public string libelleProd;
        public int NumeroCmd;
        public int QuantiteProd;
        public int Prixtt;

        public override void ajout(SqlCommand cmd)
        {
            cnx.Open();
            cmd.Connection = cnx;
            cmd.ExecuteNonQuery();
            cnx.Close();
        }
        public DetailCommande(string libelleProd,int NumeroCmd,int QuantiteProd,int Prixtt)
        {
            this.libelleProd = libelleProd;
            this.NumeroCmd = NumeroCmd;
            this.QuantiteProd = QuantiteProd;
            this.Prixtt = Prixtt;
        }
    }
}
